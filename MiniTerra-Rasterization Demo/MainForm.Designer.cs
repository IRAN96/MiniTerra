
namespace MiniTerra
{
    partial class RasterizeDemo
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MapDisplay = new System.Windows.Forms.PictureBox();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenVectorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenRasterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFileStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveRasterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.RasterizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecoLabel1 = new System.Windows.Forms.Label();
            this.VectorVisibleBox = new System.Windows.Forms.CheckBox();
            this.RasterVisibleBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapDisplay
            // 
            this.MapDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MapDisplay.Location = new System.Drawing.Point(0, 35);
            this.MapDisplay.Name = "MapDisplay";
            this.MapDisplay.Size = new System.Drawing.Size(1059, 766);
            this.MapDisplay.TabIndex = 0;
            this.MapDisplay.TabStop = false;
            this.MapDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.MapDisplay_Paint);
            this.MapDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapDisplay_MouseDown);
            this.MapDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapDisplay_MouseMove);
            this.MapDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapDisplay_MouseUp);
            this.MapDisplay.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MapDisplay_MouseWheel);
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileStrip,
            this.ToolStrip});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1292, 32);
            this.MainMenu.TabIndex = 4;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileStrip
            // 
            this.FileStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileStrip,
            this.SaveFileStrip});
            this.FileStrip.Name = "FileStrip";
            this.FileStrip.Size = new System.Drawing.Size(62, 28);
            this.FileStrip.Text = "文件";
            // 
            // OpenFileStrip
            // 
            this.OpenFileStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenVectorMenuItem,
            this.OpenRasterMenuItem});
            this.OpenFileStrip.Name = "OpenFileStrip";
            this.OpenFileStrip.Size = new System.Drawing.Size(146, 34);
            this.OpenFileStrip.Text = "打开";
            // 
            // OpenVectorMenuItem
            // 
            this.OpenVectorMenuItem.Name = "OpenVectorMenuItem";
            this.OpenVectorMenuItem.Size = new System.Drawing.Size(230, 34);
            this.OpenVectorMenuItem.Text = "打开矢量文件...";
            this.OpenVectorMenuItem.Click += new System.EventHandler(this.LoadVectorMenuItem_Clicked);
            // 
            // OpenRasterMenuItem
            // 
            this.OpenRasterMenuItem.Name = "OpenRasterMenuItem";
            this.OpenRasterMenuItem.Size = new System.Drawing.Size(230, 34);
            this.OpenRasterMenuItem.Text = "打开栅格文件...";
            this.OpenRasterMenuItem.Click += new System.EventHandler(this.LoadRasterMenuItem_Clicked);
            // 
            // SaveFileStrip
            // 
            this.SaveFileStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveRasterMenuItem});
            this.SaveFileStrip.Name = "SaveFileStrip";
            this.SaveFileStrip.Size = new System.Drawing.Size(146, 34);
            this.SaveFileStrip.Text = "保存";
            // 
            // SaveRasterMenuItem
            // 
            this.SaveRasterMenuItem.Name = "SaveRasterMenuItem";
            this.SaveRasterMenuItem.Size = new System.Drawing.Size(230, 34);
            this.SaveRasterMenuItem.Text = "保存栅格文件...";
            this.SaveRasterMenuItem.Click += new System.EventHandler(this.SaveRasterMenuItem_Clicked);
            // 
            // ToolStrip
            // 
            this.ToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RasterizeMenuItem});
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(62, 28);
            this.ToolStrip.Text = "工具";
            // 
            // RasterizeMenuItem
            // 
            this.RasterizeMenuItem.Name = "RasterizeMenuItem";
            this.RasterizeMenuItem.Size = new System.Drawing.Size(164, 34);
            this.RasterizeMenuItem.Text = "栅格化";
            this.RasterizeMenuItem.Click += new System.EventHandler(this.RasterizeMenuItem_Clicked);
            // 
            // DecoLabel1
            // 
            this.DecoLabel1.AutoSize = true;
            this.DecoLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DecoLabel1.Location = new System.Drawing.Point(1088, 477);
            this.DecoLabel1.Name = "DecoLabel1";
            this.DecoLabel1.Size = new System.Drawing.Size(158, 31);
            this.DecoLabel1.TabIndex = 5;
            this.DecoLabel1.Text = "图层可见性：";
            // 
            // VectorVisibleBox
            // 
            this.VectorVisibleBox.AutoSize = true;
            this.VectorVisibleBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.VectorVisibleBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.VectorVisibleBox.Location = new System.Drawing.Point(1110, 529);
            this.VectorVisibleBox.Name = "VectorVisibleBox";
            this.VectorVisibleBox.Size = new System.Drawing.Size(136, 35);
            this.VectorVisibleBox.TabIndex = 6;
            this.VectorVisibleBox.Text = "矢量图层";
            this.VectorVisibleBox.UseVisualStyleBackColor = true;
            this.VectorVisibleBox.CheckStateChanged += new System.EventHandler(this.VectorVisibleBox_CheckedStateChanged);
            // 
            // RasterVisibleBox
            // 
            this.RasterVisibleBox.AutoSize = true;
            this.RasterVisibleBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RasterVisibleBox.Location = new System.Drawing.Point(1110, 579);
            this.RasterVisibleBox.Name = "RasterVisibleBox";
            this.RasterVisibleBox.Size = new System.Drawing.Size(136, 35);
            this.RasterVisibleBox.TabIndex = 7;
            this.RasterVisibleBox.Text = "栅格图层";
            this.RasterVisibleBox.UseVisualStyleBackColor = true;
            this.RasterVisibleBox.CheckStateChanged += new System.EventHandler(this.RasterVisibleBox_CheckStateChanged);
            // 
            // RasterizeDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 813);
            this.Controls.Add(this.RasterVisibleBox);
            this.Controls.Add(this.VectorVisibleBox);
            this.Controls.Add(this.DecoLabel1);
            this.Controls.Add(this.MapDisplay);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "RasterizeDemo";
            this.Text = "MiniTerra - Rasterization Demo";
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MapDisplay;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenFileStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenVectorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenRasterMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveFileStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveRasterMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RasterizeMenuItem;
        private System.Windows.Forms.Label DecoLabel1;
        private System.Windows.Forms.CheckBox VectorVisibleBox;
        private System.Windows.Forms.CheckBox RasterVisibleBox;
    }
}

