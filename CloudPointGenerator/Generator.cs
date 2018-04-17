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
using System.Threading.Tasks;
using System.Diagnostics;
using AltSerialize;
using System.Text;

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

        int QueueSize = 0;

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

            if (_dataList.Count == QueueSize)
            {

                _context.Post(delegate
                {
                    richTextBox1.AppendText(@"Data Receive Finished, Serialization starting......." + Environment.NewLine);
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
                sw.Stop();
                richTextBox1.AppendText(@"Time Consuming:" + sw.ElapsedMilliseconds.ToString() + Environment.NewLine);
            }, null);

        }
        Stopwatch sw = new Stopwatch();
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            sw.Start();

            //ParallelLoopResult result = Parallel.For(0, _dataList.Count, (i, state) =>
            //{
            //    Point3DGenerator(_dataList[i]);

            //});

            //Parallel.ForEach(_dataList, (i) =>
            //{
            //    Point3DGenerator(i);
            //});

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
                        pc.Width = (int)goSurfaceMsg.Width;
                        pc.Height = (int)goSurfaceMsg.Length;
                        pc.XResolution = (float)goSurfaceMsg.XResolution / 1000000;
                        pc.YResolution = (float)goSurfaceMsg.YResolution / 1000000;
                        pc.ZResolution = (float)goSurfaceMsg.ZResolution / 1000000;
                        pc.XOffset = (float)goSurfaceMsg.XOffset / 1000;
                        pc.ZOffset = (float)goSurfaceMsg.ZOffset / 1000;
                        pc.YOffset = (float)goSurfaceMsg.YOffset / 1000;

                        GoSetup goSetup = _sensor.Setup;
                        GoRole goRole = new GoRole();
                        pc.ZStart = (float)goSetup.GetActiveAreaZ(goRole);
                        pc.XStart = (float)goSetup.GetActiveAreaX(goRole);
                        pc.ZRange = (float)goSetup.GetActiveAreaHeight(goRole);
                        ///
                        //generate csv file for point data save
                        ///
                        pc.ProfileList = new List<List<Point3D>>();
                        //int pointsCount = 0;
                        pc.Point3DArray = new Point3D[pc.Width * pc.Height];
                        int pointsCount = 0;
                        for (int j = 0; j < pc.Height; j++)
                        {
                            List<Point3D> profileList = new List<Point3D>();
                            for (int k = 0; k < pc.Width; k++)
                            {

                                Point3D tempPoint = new Point3D();

                                tempPoint.X = pc.XOffset + k * pc.XResolution;
                                tempPoint.Y = pc.YOffset + j * pc.YResolution;
                                //tempPoint.Z = (goSurfaceMsg.Get(j, k) == -32768) ? 0 : (pc.ZOffset + goSurfaceMsg.Get(j, k) * pc.ZResolution);
                                //No Z  Start
                                //tempPoint.X = k * pc.XResolution;
                                //tempPoint.Y = j * pc.YResolution;
                                tempPoint.Z = (goSurfaceMsg.Get(j, k) == -32768) ? 0 : (pc.ZOffset + goSurfaceMsg.Get(j, k) * pc.ZResolution - pc.ZStart);
                                profileList.Add(tempPoint);
                                pc.Point3DArray[pointsCount] = tempPoint;
                                pointsCount++;

                            }


                            pc.ProfileList.Add(profileList);

                        }


                        //data.Destroy();
                        dataMsg.Destroy();
                        goSurfaceMsg.Destroy();
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
            Stream stream = File.Open("C:\\PointCloudData\\" + timeStamp + ".pcd", FileMode.OpenOrCreate);
            AltSerialize.AltSerializer altSerializer = new AltSerializer(stream);
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(PointCloud));
            //xmlSerializer.Serialize(stream, pc);
            altSerializer.Serialize(pc);
            stream.Flush();
            stream.Close();

        }

        private void Generator_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (_sensor != null || _system != null)
            {
                _sensor.Stop();
                _system.Disconnect();
                _system.Dispose();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out QueueSize);
        }
    }
}
