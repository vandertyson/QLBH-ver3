namespace QLBH.Forms
{
    partial class Test2
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
            this.c01_quan_ly_hang_hoa1 = new QLBH.Controls.c01_quan_ly_hang_hoa();
            this.SuspendLayout();
            // 
            // c01_quan_ly_hang_hoa1
            // 
            this.c01_quan_ly_hang_hoa1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c01_quan_ly_hang_hoa1.list_hang_hoa = null;
            this.c01_quan_ly_hang_hoa1.list_hang_hoa_master = null;
            this.c01_quan_ly_hang_hoa1.Location = new System.Drawing.Point(0, 0);
            this.c01_quan_ly_hang_hoa1.Name = "c01_quan_ly_hang_hoa1";
            this.c01_quan_ly_hang_hoa1.Size = new System.Drawing.Size(761, 467);
            this.c01_quan_ly_hang_hoa1.TabIndex = 0;
            // 
            // Test2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 467);
            this.Controls.Add(this.c01_quan_ly_hang_hoa1);
            this.Name = "Test2";
            this.Text = "Test2";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.c01_quan_ly_hang_hoa c01_quan_ly_hang_hoa1;
    }
}