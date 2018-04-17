using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoinCloudLib;

namespace AVLTest
{
    public partial class RegionEditorForm : Form
    {


        PointCloud pc;

        public RegionEditorForm(Bitmap _inputImage, PointCloud pointCloud)
        {
            InitializeComponent();
            pc = pointCloud;
            imageBoxEx1.Image = _inputImage;
        }


        Point3D[] TransRegionToDataPoints(RegionClass _region, PointCloud pc )
        {


            Point3D[] dataPoints = new Point3D[_region.RegionSize];
            List<List<Point3D>> profileData = pc.ProfileList;
            int dataIndex = 0;
            foreach (var item in _region.RegionStrides)
            {
                //Profile Index
                int profileIndex = item.StartPoint.Y;
                int dataStart = item.StartPoint.X;
                int dataEnd = item.EndPoint.X;
                int dataLength = (int)item.RunLength;
                for (int i = dataStart; i < dataEnd; i++)
                {
                    dataPoints[dataIndex] = profileData[profileIndex][i];
                    dataIndex++;
                }
            }

            return dataPoints;
        }

        private void buttonFlatness_Click(object sender, EventArgs e)
        {
            RectangleF rectangleF = imageBoxEx1.SelectionRegion;
            if (rectangleF.Width >10)
            {
                Rectangle trans = new Rectangle((int)rectangleF.Left, (int)rectangleF.Top, (int)rectangleF.Width, (int)rectangleF.Height);
                RegionClass rc = new RegionClass();
                rc = rc.GetRegionFromRect(trans);

                Point3D[] dataPoints = TransRegionToDataPoints(rc, pc);
                FitPlaneClass fitPlaneClass = new FitPlaneClass();
                List<double> result1 = fitPlaneClass.FitPlaneCalculate(dataPoints);

                List<double> result2 = fitPlaneClass.FitPlaneAlter(dataPoints);

                string planeEquation1 = " plane equation01: " + result1[0].ToString("F4") + "*X+" + result1[1].ToString("F4") + "*Y+" + result1[2].ToString("F4") + "=Z";
                string planeEquation2 = " plane equation02: " + result2[0].ToString("F4") + "*X+" + result2[1].ToString("F4") + "*Y+" + result2[2].ToString("F4") + "=Z";
                string RR = "Fit quality – Coefficient of determination = " + result2[3].ToString("F4");
                richTextBox1.AppendText(planeEquation1 + Environment.NewLine);
                richTextBox1.AppendText(planeEquation2 + Environment.NewLine);

                richTextBox1.AppendText(RR + Environment.NewLine); 
            }
        }

        private void buttonRect_Click(object sender, EventArgs e)
        {
            imageBoxEx1.SelectionMode = LMI.Gocator.Tools.ImageBoxSelectionMode.Rectangle;


        }
    }
}
