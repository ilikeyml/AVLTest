namespace AVLTest
{
    partial class AvlTest
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvlTest));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.view2DBox1 = new HMI.Controls.View2DBox();
            this.view3DBox1 = new HMI.Controls.View3DBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.buttonCalc = new System.Windows.Forms.Button();
            this.buttonROI = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.MsgBox = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonTest = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 741);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(818, 741);
            this.splitContainer1.SplitterDistance = 582;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.view2DBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.view3DBox1);
            this.splitContainer2.Size = new System.Drawing.Size(582, 741);
            this.splitContainer2.SplitterDistance = 368;
            this.splitContainer2.TabIndex = 0;
            // 
            // view2DBox1
            // 
            this.view2DBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.view2DBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view2DBox1.Location = new System.Drawing.Point(0, 0);
            this.view2DBox1.Name = "view2DBox1";
            this.view2DBox1.Size = new System.Drawing.Size(582, 368);
            this.view2DBox1.TabIndex = 0;
            // 
            // view3DBox1
            // 
            this.view3DBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view3DBox1.Location = new System.Drawing.Point(0, 0);
            this.view3DBox1.Name = "view3DBox1";
            this.view3DBox1.Size = new System.Drawing.Size(582, 369);
            this.view3DBox1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.buttonTest);
            this.splitContainer3.Panel1.Controls.Add(this.buttonCalc);
            this.splitContainer3.Panel1.Controls.Add(this.buttonROI);
            this.splitContainer3.Panel1.Controls.Add(this.buttonLoad);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.MsgBox);
            this.splitContainer3.Size = new System.Drawing.Size(232, 741);
            this.splitContainer3.SplitterDistance = 371;
            this.splitContainer3.TabIndex = 0;
            // 
            // buttonCalc
            // 
            this.buttonCalc.Location = new System.Drawing.Point(48, 215);
            this.buttonCalc.Name = "buttonCalc";
            this.buttonCalc.Size = new System.Drawing.Size(134, 56);
            this.buttonCalc.TabIndex = 2;
            this.buttonCalc.Text = "Flatness";
            this.buttonCalc.UseVisualStyleBackColor = true;
            this.buttonCalc.Click += new System.EventHandler(this.buttonCalc_Click);
            // 
            // buttonROI
            // 
            this.buttonROI.Location = new System.Drawing.Point(48, 153);
            this.buttonROI.Name = "buttonROI";
            this.buttonROI.Size = new System.Drawing.Size(134, 56);
            this.buttonROI.TabIndex = 1;
            this.buttonROI.Text = "Draw ROI";
            this.buttonROI.UseVisualStyleBackColor = true;
            this.buttonROI.Click += new System.EventHandler(this.buttonROI_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonLoad.BackgroundImage")));
            this.buttonLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonLoad.Location = new System.Drawing.Point(48, 23);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(134, 107);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // MsgBox
            // 
            this.MsgBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MsgBox.Location = new System.Drawing.Point(0, 0);
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.Size = new System.Drawing.Size(232, 366);
            this.MsgBox.TabIndex = 0;
            this.MsgBox.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(48, 291);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(134, 56);
            this.buttonTest.TabIndex = 3;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // AvlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 741);
            this.Controls.Add(this.panel1);
            this.Name = "AvlTest";
            this.Text = "AVLDemo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private HMI.Controls.View2DBox view2DBox1;
        private HMI.Controls.View3DBox view3DBox1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.RichTextBox MsgBox;
        private System.Windows.Forms.Button buttonROI;
        private System.Windows.Forms.Button buttonCalc;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonTest;
    }
}

