namespace QLBH.Forms
{
    partial class Test
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
            this.btnThem = new System.Windows.Forms.Button();
            this.m_btn_phieu_nhap = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.table = new QLBH.Controls.Common_Controls.c0_table();
            this.SuspendLayout();
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(417, 518);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 9;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // m_btn_phieu_nhap
            // 
            this.m_btn_phieu_nhap.Location = new System.Drawing.Point(631, 518);
            this.m_btn_phieu_nhap.Name = "m_btn_phieu_nhap";
            this.m_btn_phieu_nhap.Size = new System.Drawing.Size(75, 23);
            this.m_btn_phieu_nhap.TabIndex = 10;
            this.m_btn_phieu_nhap.Text = "Phiếu nhập";
            this.m_btn_phieu_nhap.UseVisualStyleBackColor = true;
            this.m_btn_phieu_nhap.Click += new System.EventHandler(this.m_btn_phieu_nhap_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(148, 518);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // table
            // 
            this.table.AutoScroll = true;
            this.table.Dock = System.Windows.Forms.DockStyle.Top;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(997, 503);
            this.table.Style = QLBH.Controls.Common_Controls.c0_table.ScrollStyle.Vertical;
            this.table.TabIndex = 13;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 553);
            this.Controls.Add(this.table);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_btn_phieu_nhap);
            this.Controls.Add(this.btnThem);
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button m_btn_phieu_nhap;
        private System.Windows.Forms.Button button1;
        private Controls.Common_Controls.c0_table table;
    }
}