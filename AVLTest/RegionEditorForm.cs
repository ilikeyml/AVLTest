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
            



            return null;
        }

        private void buttonFlatness_Click(object sender, EventArgs e)
        {
            RectangleF rectangleF = imageBoxEx1.SelectionRegion;



        }

        private void buttonRect_Click(object sender, EventArgs e)
        {
            imageBoxEx1.SelectionMode = LMI.Gocator.Tools.ImageBoxSelectionMode.Rectangle;


        }
    }
}
