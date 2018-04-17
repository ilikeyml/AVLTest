using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using AvlNet.Designers;
using AvlNet;
using PoinCloudLib;
using System.IO;
using System.Threading;
using System.Diagnostics;
using AltSerialize;
using System.Drawing;



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
        string filename = "";
        private void buttonLoad_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null)
            {

                filename = openFileDialog1.FileName;
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
                view3DBox1.Data1.SetSurface(_surface);
                //view3DBox1.Data1.SetPoint3DGrid(point3DGrid);

                surfaceToHeightImage();
                view2DBox1.SetImage(image);
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
            Stream stream = File.OpenRead(filename);
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(PointCloud));
            //pc = (PointCloud)xmlSerializer.Deserialize(stream);

            AltSerialize.AltSerializer altSerializer = new AltSerializer(stream);
            pc = (PointCloud)altSerializer.Deserialize(typeof(PoinCloudLib.PointCloud));
            stream.Flush();
            stream.Close();
            sw.Stop();
            _context.Post(delegate
            {
                MsgBox.AppendText("Deserialize time consuming...." + sw.ElapsedMilliseconds.ToString() + Environment.NewLine);
                //this.Cursor = Cursors.WaitCursor;
                MsgBox.AppendText("X Resolution:" + pc.XResolution.ToString() + Environment.NewLine);
                MsgBox.AppendText("Y Resolution:" + pc.YResolution.ToString() + Environment.NewLine);
                MsgBox.AppendText("Z Resolution:" + pc.ZResolution.ToString() + Environment.NewLine);


            }, null);

            float xscale = pc.XResolution * 10;
            float yscale = pc.YResolution * 10;
            float zscale = pc.ZResolution * 1000;


            _context.Post(delegate
            {
                MsgBox.AppendText("X Scale:" + xscale.ToString() + Environment.NewLine);
                MsgBox.AppendText("Y Scale:" + yscale.ToString() + Environment.NewLine);
                MsgBox.AppendText("Z Scale:" + zscale.ToString() + Environment.NewLine);

                //view3DBox1.Data1.SetSurface(DataTrans(pc));


            }, null);

            try
            {
                AVL.ArrangePoint3DArray(DataTrans(pc), xscale, yscale, 0, zscale, PlainType.Real, out _surface);

            }
            catch (AvlNet.DomainException ex)
            {

                MessageBox.Show(ex.ToString());
            }



            //Point3DGrid point3DGrid = new Point3DGrid();



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _context = SynchronizationContext.Current;
        }

        AvlNet.Region roi;
        private void buttonROI_Click(object sender, EventArgs e)
        {
            RegionDesigner regionDesigner = new RegionDesigner();
            regionDesigner.Backgrounds = new AvlNet.Image[] { image };
            regionDesigner.Region = roi;
            if (regionDesigner.ShowDialog() == DialogResult.OK)
            {

                roi = regionDesigner.Region;

                view2DBox1.Data1.SetRegion(roi);

            }


        }

        AvlNet.Point3D[] DataTrans(PoinCloudLib.PointCloud pc)
        {

            string csvFilename = filename.Replace("pcd", "csv");
            if (File.Exists(csvFilename))
            {
                File.Delete(csvFilename);
            }
            Stream stream = File.OpenWrite(csvFilename);
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

                    string csvStr = _3Dpoints[index].X.ToString() + "," + _3Dpoints[index].Y.ToString() + "," + _3Dpoints[index].Z.ToString() + Environment.NewLine;

                    bw.Write(Encoding.Default.GetBytes(csvStr), 0, csvStr.Length);
                    listPoints.Add(_3Dpoints[index]);
                    index++;
                }
            }
            stream.Flush();
            bw.Close();

            return _3Dpoints;



        }
        /// <summary>
        /// Caculate Plane Flatness
        /// 
        /// Grab Image -> FitPlaneToSurface->SurfaceFlatness
        /// </summary>

        void CaculateFlatness()
        {




        }

        /// <summary>
        /// Surface to 2D image
        /// </summary>
        AvlNet.Image image = new AvlNet.Image();
        void surfaceToHeightImage()
        {


            AVL.SurfaceToImage(_surface, out image);

        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {




        }



        private void buttonTest_Click(object sender, EventArgs e)
        {
            //Bitmap bmp =   ImageGenerator.GrayScaleImageGenerator(pc);
            Bitmap bmp = ImageGenerator.GrayScaleImageGeneratorKeepRatio(pc);
            
            view2DBox1.SetImage(new AvlNet.Image(bmp));

            //Draw Region

            //AvlNet.Region region = new AvlNet.Region();
            //RegionDesigner reDesigner = new RegionDesigner();

            //reDesigner.Backgrounds = new AvlNet.Image[] { new AvlNet.Image(bmp) };
            //if (reDesigner.ShowDialog() == DialogResult.OK)
            //{
            //    region = reDesigner.Region;
            //}

            //PointRun[] pointRun = new PointRun[region.Size];
            //pointRun = region.ToArray();


            RegionEditorForm regionForm = new RegionEditorForm(bmp, pc);
            regionForm.ShowDialog();


        }
    }
}
