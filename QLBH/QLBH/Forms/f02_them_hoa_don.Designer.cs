namespace QLBH.Forms
{
    partial class f02_them_hoa_don
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f02_them_hoa_don));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lbl_date_time = new DevExpress.XtraEditors.LabelControl();
            this.m_btn_them_hoa_don = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.m_tab_control_hoa_don = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.c02_hoa_don_full1 = new QLBH.Controls.Hóa_đơn.c02_hoa_don_full();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_tab_control_hoa_don)).BeginInit();
            this.m_tab_control_hoa_don.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lbl_date_time);
            this.panelControl1.Controls.Add(this.m_btn_them_hoa_don);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1253, 56);
            this.panelControl1.TabIndex = 2;
            // 
            // m_lbl_date_time
            // 
            this.m_lbl_date_time.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbl_date_time.Location = new System.Drawing.Point(868, 12);
            this.m_lbl_date_time.Name = "m_lbl_date_time";
            this.m_lbl_date_time.Size = new System.Drawing.Size(385, 32);
            this.m_lbl_date_time.TabIndex = 1;
            this.m_lbl_date_time.Text = "Thứ 6 ngày 13 năm 2016 - 09:00:01 ";
            // 
            // m_btn_them_hoa_don
            // 
            this.m_btn_them_hoa_don.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.m_btn_them_hoa_don.Appearance.Options.UseBackColor = true;
            this.m_btn_them_hoa_don.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.m_btn_them_hoa_don.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btn_them_hoa_don.Image = ((System.Drawing.Image)(resources.GetObject("m_btn_them_hoa_don.Image")));
            this.m_btn_them_hoa_don.Location = new System.Drawing.Point(0, 0);
            this.m_btn_them_hoa_don.Name = "m_btn_them_hoa_don";
            this.m_btn_them_hoa_don.Size = new System.Drawing.Size(118, 56);
            this.m_btn_them_hoa_don.TabIndex = 0;
            this.m_btn_them_hoa_don.Text = "Hóa đơn mới";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.m_tab_control_hoa_don);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 56);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1253, 575);
            this.panelControl2.TabIndex = 3;
            // 
            // m_tab_control_hoa_don
            // 
            this.m_tab_control_hoa_don.Appearance.BackColor = System.Drawing.Color.White;
            this.m_tab_control_hoa_don.Appearance.Options.UseBackColor = true;
            this.m_tab_control_hoa_don.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_tab_control_hoa_don.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tab_control_hoa_don.Location = new System.Drawing.Point(2, 2);
            this.m_tab_control_hoa_don.Name = "m_tab_control_hoa_don";
            this.m_tab_control_hoa_don.SelectedTabPage = this.xtraTabPage1;
            this.m_tab_control_hoa_don.Size = new System.Drawing.Size(1249, 571);
            this.m_tab_control_hoa_don.TabIndex = 4;
            this.m_tab_control_hoa_don.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.c02_hoa_don_full1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1243, 543);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // c02_hoa_don_full1
            // 
            this.c02_hoa_don_full1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c02_hoa_don_full1.Location = new System.Drawing.Point(0, 0);
            this.c02_hoa_don_full1.Name = "c02_hoa_don_full1";
            this.c02_hoa_don_full1.Size = new System.Drawing.Size(1243, 543);
            this.c02_hoa_don_full1.TabIndex = 0;
            // 
            // f02_them_hoa_don
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 631);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "f02_them_hoa_don";
            this.Text = "F02 - Bán hàng";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_tab_control_hoa_don)).EndInit();
            this.m_tab_control_hoa_don.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraTab.XtraTabControl m_tab_control_hoa_don;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Controls.Hóa_đơn.c02_hoa_don_full c02_hoa_don_full1;
        private DevExpress.XtraEditors.LabelControl m_lbl_date_time;
        private DevExpress.XtraEditors.SimpleButton m_btn_them_hoa_don;
    }
}