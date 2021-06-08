
namespace MiniTerra
{
    partial class AttributeView
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
            this.AttributesGridView = new System.Windows.Forms.DataGridView();
            this.AddFieldBtn = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.RemoveFieldBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AttributesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AttributesGridView
            // 
            this.AttributesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AttributesGridView.Location = new System.Drawing.Point(405, 12);
            this.AttributesGridView.Name = "AttributesGridView";
            this.AttributesGridView.RowHeadersWidth = 62;
            this.AttributesGridView.RowTemplate.Height = 30;
            this.AttributesGridView.Size = new System.Drawing.Size(694, 803);
            this.AttributesGridView.TabIndex = 0;
            // 
            // AddFieldBtn
            // 
            this.AddFieldBtn.Location = new System.Drawing.Point(287, 681);
            this.AddFieldBtn.Name = "AddFieldBtn";
            this.AddFieldBtn.Size = new System.Drawing.Size(112, 32);
            this.AddFieldBtn.TabIndex = 1;
            this.AddFieldBtn.Text = "添加字段";
            this.AddFieldBtn.UseVisualStyleBackColor = true;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(16, 684);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(265, 28);
            this.nameBox.TabIndex = 2;
            // 
            // RemoveFieldBtn
            // 
            this.RemoveFieldBtn.Location = new System.Drawing.Point(287, 747);
            this.RemoveFieldBtn.Name = "RemoveFieldBtn";
            this.RemoveFieldBtn.Size = new System.Drawing.Size(112, 32);
            this.RemoveFieldBtn.TabIndex = 3;
            this.RemoveFieldBtn.Text = "删除字段";
            this.RemoveFieldBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 660);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "新字段：";
            // 
            // AttributeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 827);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RemoveFieldBtn);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.AddFieldBtn);
            this.Controls.Add(this.AttributesGridView);
            this.Name = "AttributeView";
            this.Text = "属性表";
            ((System.ComponentModel.ISupportInitialize)(this.AttributesGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AttributesGridView;
        private System.Windows.Forms.Button AddFieldBtn;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button RemoveFieldBtn;
        private System.Windows.Forms.Label label1;
    }
}