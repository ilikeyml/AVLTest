using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AvlNet.Designers;
using AvlNet;
using AvlNet.ExecutorSerialization;
using HMI.Controls;
using HMI.Localization;
using PoinCloudLib;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Xml.Serialization;
using AltSerialize;


namespace AVLTest
{
    public partial class AvlTest : Form
    {
        public AvlTest()
        {
            InitializeComponent();
        }

        PointCloud pc = new PointCloud();
        
        SynchronizationContext _context = new SynchronizationContext();
        List<AvlNet.Point3D> listPoints = new List<AvlNet.Point3D>();

        private void buttonLoad_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null)
            {


                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.DoWork += BgWorker_DoWork;
                bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;

                bgWorker.RunWorkerAsync();
                panel1.Enabled = false;

            }



        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            _context.Post(delegate
            {

                //view3DBox1.Data1.SetPointCloud(_surface.Data, listPoints.Count);
                //view3DBox1.Data1.SetLine(0, 0, 0, 10, 10, 10);
                //view3DBox1.Data1.SetSurface(_surface);
                //view3DBox1.Data1.SetPoint3DGrid(point3DGrid);


                MsgBox.AppendText("Creating Surface Done" + Environment.NewLine);
                this.Cursor = Cursors.Default;
                panel1.Enabled = true;

            }, null);


        }
        /// <summary>
        /// Display Surface
        /// </summary>
        Surface _surface = new Surface();
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            _context.Post(delegate
            {
                MsgBox.AppendText("Creating Surface...." + Environment.NewLine);
                this.Cursor = Cursors.WaitCursor;
            }, null);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Stream stream = File.OpenRead(openFileDialog1.FileName);
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(PointCloud));
            //pc = (PointCloud)xmlSerializer.Deserialize(stream);
            
            AltSerialize.AltSerializer altSerializer = new AltSerializer(stream);
            pc = (PointCloud)altSerializer.Deserialize(typeof(PoinCloudLib.PointCloud));
            stream.Flush();
            stream.Close();
            sw.Stop();
            _context.Post(delegate
            {
                MsgBox.AppendText("Deserialize time consuming...."+sw.ElapsedMilliseconds.ToString() + Environment.NewLine);
                //this.Cursor = Cursors.WaitCursor;
            }, null);


            //AVL.ArrangePoint3DArray(DataTrans(pc), pc.XResolution, pc.YResolution, 0, pc.ZResolution, PlainType.Real, out _surface);

            Point3DGrid point3DGrid = new Point3DGrid();

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _context = SynchronizationContext.Current;
        }

        private void buttonROI_Click(object sender, EventArgs e)
        {

        }

        AvlNet.Point3D[] DataTrans(PoinCloudLib.PointCloud pc)
        {
            Stream stream = File.OpenWrite(@"C:\points.csv");
            BinaryWriter bw = new BinaryWriter(stream, Encoding.ASCII);
            AvlNet.Point3D[] _3Dpoints = new AvlNet.Point3D[pc.Width * pc.Height];       
            int index = 0;
            for (int i = 0; i < pc.ProfileList.Count; i++)
            {
                for (int j = 0; j < pc.ProfileList[0].Count; j++)
                {
                    _3Dpoints[index].X = pc.ProfileList[i][j].X;
                    _3Dpoints[index].Y = pc.ProfileList[i][j].Y;
                    _3Dpoints[index].Z = pc.ProfileList[i][j].Z;
                    bw.Write(" "+ _3Dpoints[index].X.ToString() + "," + _3Dpoints[index].Y.ToString() + "," + _3Dpoints[index].Z.ToString() + Environment.NewLine);
                    listPoints.Add(_3Dpoints[index]);
                    index++;
                }
            }
            stream.Flush();
            bw.Close();
            
            return _3Dpoints;



        }

    }
}
