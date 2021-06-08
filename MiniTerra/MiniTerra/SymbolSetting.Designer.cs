
namespace MiniTerra
{
    partial class SymbolSetting
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
            this.SymbolTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SymbolTable = new System.Windows.Forms.DataGridView();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SymbolTable)).BeginInit();
            this.SuspendLayout();
            // 
            // SymbolTypeComboBox
            // 
            this.SymbolTypeComboBox.FormattingEnabled = true;
            this.SymbolTypeComboBox.Items.AddRange(new object[] {
            "单一符号",
            "唯一值符号",
            "分级符号"});
            this.SymbolTypeComboBox.Location = new System.Drawing.Point(148, 12);
            this.SymbolTypeComboBox.Name = "SymbolTypeComboBox";
            this.SymbolTypeComboBox.Size = new System.Drawing.Size(301, 26);
            this.SymbolTypeComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "符号类型：";
            // 
            // SymbolTable
            // 
            this.SymbolTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SymbolTable.Location = new System.Drawing.Point(12, 73);
            this.SymbolTable.Name = "SymbolTable";
            this.SymbolTable.RowHeadersWidth = 62;
            this.SymbolTable.RowTemplate.Height = 30;
            this.SymbolTable.Size = new System.Drawing.Size(776, 492);
            this.SymbolTable.TabIndex = 2;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(653, 575);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(135, 32);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Location = new System.Drawing.Point(520, 575);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(127, 32);
            this.ConfirmBtn.TabIndex = 8;
            this.ConfirmBtn.Text = "确定";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            // 
            // SymbolSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 619);
            this.Controls.Add(this.ConfirmBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SymbolTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SymbolTypeComboBox);
            this.Name = "SymbolSetting";
            this.Text = "符号设置";
            ((System.ComponentModel.ISupportInitialize)(this.SymbolTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SymbolTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView SymbolTable;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button ConfirmBtn;
    }
}