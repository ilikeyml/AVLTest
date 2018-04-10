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
using HMI.Controls;
using HMI.Localization;
using PoinCloudLib;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;


namespace AVLTest
{
    public partial class AvlTest : Form
    {
        public AvlTest()
        {
            InitializeComponent();
        }

        PointCloud pc = new PointCloud();

        private void buttonLoad_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            if(openFileDialog1.FileName!=null)
            {

                Stream stream = File.OpenRead(openFileDialog1.FileName);
                BinaryFormatter bf = new BinaryFormatter();
                pc = (PointCloud)bf.Deserialize(stream);
                stream.Close();
                Surface _surface = new Surface();
             
                AVL.ArrangePoint3DArray(pc.Point3DArray, 1, 1, 0, 1, PlainType.UInt8, out _surface);
                view3DBox1.Data1.SetSurface(_surface);

            }


            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonROI_Click(object sender, EventArgs e)
        {
           
        }

    }
}
