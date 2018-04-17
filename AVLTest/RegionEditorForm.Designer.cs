namespace AVLTest
{
    partial class RegionEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.imageBoxEx1 = new LMI.Gocator.Tools.ImageBoxEx();
            this.buttonThreshold = new System.Windows.Forms.Button();
            this.buttonFlatness = new System.Windows.Forms.Button();
            this.buttonRect = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(867, 595);
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
            this.splitContainer1.Panel1.Controls.Add(this.imageBoxEx1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonThreshold);
            this.splitContainer1.Panel2.Controls.Add(this.buttonFlatness);
            this.splitContainer1.Panel2.Controls.Add(this.buttonRect);
            this.splitContainer1.Size = new System.Drawing.Size(867, 595);
            this.splitContainer1.SplitterDistance = 669;
            this.splitContainer1.TabIndex = 0;
            // 
            // imageBoxEx1
            // 
            this.imageBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxEx1.Location = new System.Drawing.Point(0, 0);
            this.imageBoxEx1.Name = "imageBoxEx1";
            this.imageBoxEx1.Size = new System.Drawing.Size(669, 595);
            this.imageBoxEx1.TabIndex = 0;
            // 
            // buttonThreshold
            // 
            this.buttonThreshold.Location = new System.Drawing.Point(25, 374);
            this.buttonThreshold.Name = "buttonThreshold";
            this.buttonThreshold.Size = new System.Drawing.Size(145, 104);
            this.buttonThreshold.TabIndex = 2;
            this.buttonThreshold.Text = "Threshold";
            this.buttonThreshold.UseVisualStyleBackColor = true;
            // 
            // buttonFlatness
            // 
            this.buttonFlatness.Location = new System.Drawing.Point(25, 187);
            this.buttonFlatness.Name = "buttonFlatness";
            this.buttonFlatness.Size = new System.Drawing.Size(145, 104);
            this.buttonFlatness.TabIndex = 1;
            this.buttonFlatness.Text = "Flatness";
            this.buttonFlatness.UseVisualStyleBackColor = true;
            this.buttonFlatness.Click += new System.EventHandler(this.buttonFlatness_Click);
            // 
            // buttonRect
            // 
            this.buttonRect.Location = new System.Drawing.Point(25, 38);
            this.buttonRect.Name = "buttonRect";
            this.buttonRect.Size = new System.Drawing.Size(145, 104);
            this.buttonRect.TabIndex = 0;
            this.buttonRect.Text = "Rect";
            this.buttonRect.UseVisualStyleBackColor = true;
            this.buttonRect.Click += new System.EventHandler(this.buttonRect_Click);
            // 
            // RegionEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 595);
            this.Controls.Add(this.panel1);
            this.Name = "RegionEditorForm";
            this.Text = "RegionEditorForm";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonThreshold;
        private System.Windows.Forms.Button buttonFlatness;
        private System.Windows.Forms.Button buttonRect;
        private LMI.Gocator.Tools.ImageBoxEx imageBoxEx1;
    }
}