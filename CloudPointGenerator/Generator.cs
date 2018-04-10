using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Lmi3d.GoSdk;
using Lmi3d.GoSdk.Messages;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using PoinCloudLib;

namespace CloudPointGenerator
{
    public partial class Generator : Form
    {
        public Generator()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;

        }

        #region Private 

        GoSystem _system;
        GoSensor _sensor;
        static string IPAddr = "127.0.0.1";
        List<KObject> _dataList = new List<KObject>();
        SynchronizationContext _context;

        #endregion


        void initialApi()
        {


            KApiLib.Construct();
            GoSdkLib.Construct();
            KIpAddress addr = KIpAddress.Parse(IPAddr);
            _system = new GoSystem();

            _sensor = _system.FindSensorByIpAddress(addr);
            _system.Connect();

            if (_sensor.State == GoState.Ready)
            {

                _context.Post(delegate
                {

                    richTextBox1.AppendText("Connected" + Environment.NewLine);
                }, null);
            }
            else
            {

                _context.Post(delegate
                {
                    richTextBox1.AppendText(@"Connect failed" + Environment.NewLine);
                }, null);
            }

            _system.EnableData(true);
            _system.SetDataHandler(OnDataReceived);
        }

        private void OnDataReceived(KObject data)
        {
            _dataList.Add(data);
            if (_dataList.Count == 6)
            {

                _context.Post(delegate
                {
                    richTextBox1.AppendText(@"Data Receive Finished" + Environment.NewLine);
                    bgWorker.RunWorkerAsync();
                }, null);

            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            initialApi();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;

            _system.Start();



        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            _context.Post(delegate
            {
                richTextBox1.AppendText(@"Completed" + Environment.NewLine);
            }, null);

        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var item in _dataList)
            {
                Point3DGenerator(item);
            }
        }

        BackgroundWorker bgWorker;



        /// <summary>
        /// Get Point Cloud from Gocator
        /// </summary>
        /// <returns></returns>
        private void Point3DGenerator(KObject data)
        {

            GoDataSet goDataSource = (GoDataSet)data;
            PointCloud pc = new PointCloud();
            long count = goDataSource.Count;


            for (int i = 0; i < count; i++)
            {

                GoDataMsg dataMsg = (GoDataMsg)goDataSource.Get(i);
                switch (dataMsg.MessageType)
                {
                    case GoDataMessageType.Surface:

                        GoSurfaceMsg goSurfaceMsg = (GoSurfaceMsg)(dataMsg);
                        pc.Width = goSurfaceMsg.Width;
                        pc.Length = goSurfaceMsg.Length;

                        //GoSurfaceMsg surfaceMsg = (GoSurfaceMsg)dataObj;
                        //long width = surfaceMsg.Width;
                        //long height = surfaceMsg.Length;
                        long bufferSize = pc.Width * pc.Length;
                        IntPtr bufferPointer = goSurfaceMsg.Data;
                        pc.Range = new short[bufferSize];
                        Marshal.Copy(bufferPointer, pc.Range, 0, pc.Range.Length);
                        SerializePointCloud(pc);

                        break;
                    default:
                        break;
                }
            }
        }


        void SerializePointCloud(PointCloud pc)
        {

            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            BinaryFormatter bf = new BinaryFormatter();
            Stream stream = File.Open("C:\\PointCloudData\\"+ timeStamp + ".pcd", FileMode.OpenOrCreate);
            bf.Serialize(stream, pc);
        }

        private void Generator_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sensor.Stop();
            _system.Disconnect();
            _system.Dispose();
        }
    }
}
