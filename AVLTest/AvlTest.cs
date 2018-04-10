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


namespace AVLTest
{
    public partial class AvlTest : Form
    {
        public AvlTest()
        {
            InitializeComponent();
        }

        List<PointCloud> cloudBuffer = new List<PointCloud>();

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if(folderBrowserDialog1.SelectedPath != null)
            {
                string filePath = folderBrowserDialog1.SelectedPath;
                string[] filenames = Directory.GetFiles(filePath);
                BinaryFormatter bf = new BinaryFormatter();
                foreach (var item in filenames)
                {
                    PointCloud pc = new PointCloud();
                    pc = (PointCloud)bf.Deserialize(File.OpenRead(item));
                    cloudBuffer.Add(pc);
                }

                MsgBox.AppendText("Total Buffer Loaded:" + cloudBuffer.Count.ToString() + Environment.NewLine);





            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




    }
}
