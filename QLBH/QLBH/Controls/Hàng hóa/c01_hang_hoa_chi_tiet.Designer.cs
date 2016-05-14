namespace QLBH.Controls
{
    partial class c01_hang_hoa_chi_tiet
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_xtra_scroll = new DevExpress.XtraEditors.XtraScrollableControl();
            this.nhanXetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.m_lbl_tag = new DevExpress.XtraEditors.LabelControl();
            this.m_lbl_hang_hoa_master = new DevExpress.XtraEditors.LabelControl();
            this.m_lbl_ten_san_pham = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_btn_xoa_san_pham = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.nhanXetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_xtra_scroll
            // 
            this.m_xtra_scroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_xtra_scroll.Location = new System.Drawing.Point(0, 96);
            this.m_xtra_scroll.Name = "m_xtra_scroll";
            this.m_xtra_scroll.Size = new System.Drawing.Size(934, 450);
            this.m_xtra_scroll.TabIndex = 9;
            // 
            // nhanXetBindingSource
            // 
            this.nhanXetBindingSource.DataSource = typeof(LibraryApi.NhanXet);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.m_lbl_tag);
            this.panelControl2.Controls.Add(this.m_lbl_hang_hoa_master);
            this.panelControl2.Controls.Add(this.m_lbl_ten_san_pham);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(5);
            this.panelControl2.Size = new System.Drawing.Size(647, 96);
            this.panelControl2.TabIndex = 11;
            // 
            // m_lbl_tag
            // 
            this.m_lbl_tag.Appearance.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbl_tag.Appearance.ForeColor = System.Drawing.Color.OldLace;
            this.m_lbl_tag.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lbl_tag.Location = new System.Drawing.Point(5, 67);
            this.m_lbl_tag.Name = "m_lbl_tag";
            this.m_lbl_tag.Size = new System.Drawing.Size(0, 20);
            this.m_lbl_tag.TabIndex = 16;
            // 
            // m_lbl_hang_hoa_master
            // 
            this.m_lbl_hang_hoa_master.Appearance.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbl_hang_hoa_master.Appearance.ForeColor = System.Drawing.Color.OldLace;
            this.m_lbl_hang_hoa_master.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lbl_hang_hoa_master.Location = new System.Drawing.Point(5, 47);
            this.m_lbl_hang_hoa_master.Name = "m_lbl_hang_hoa_master";
            this.m_lbl_hang_hoa_master.Size = new System.Drawing.Size(0, 20);
            this.m_lbl_hang_hoa_master.TabIndex = 15;
            // 
            // m_lbl_ten_san_pham
            // 
            this.m_lbl_ten_san_pham.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.m_lbl_ten_san_pham.Appearance.Font = new System.Drawing.Font("Sitka Text", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbl_ten_san_pham.Appearance.ForeColor = System.Drawing.Color.White;
            this.m_lbl_ten_san_pham.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lbl_ten_san_pham.Location = new System.Drawing.Point(5, 5);
            this.m_lbl_ten_san_pham.Name = "m_lbl_ten_san_pham";
            this.m_lbl_ten_san_pham.Size = new System.Drawing.Size(0, 42);
            this.m_lbl_ten_san_pham.TabIndex = 11;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_btn_xoa_san_pham);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(934, 96);
            this.panelControl1.TabIndex = 8;
            // 
            // m_btn_xoa_san_pham
            // 
            this.m_btn_xoa_san_pham.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.m_btn_xoa_san_pham.Appearance.Options.UseBackColor = true;
            this.m_btn_xoa_san_pham.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.m_btn_xoa_san_pham.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btn_xoa_san_pham.Image = global::QLBH.Properties.Resources.Delete_Filled_50;
            this.m_btn_xoa_san_pham.Location = new System.Drawing.Point(876, 0);
            this.m_btn_xoa_san_pham.Name = "m_btn_xoa_san_pham";
            this.m_btn_xoa_san_pham.Size = new System.Drawing.Size(58, 96);
            this.m_btn_xoa_san_pham.TabIndex = 16;
            this.m_btn_xoa_san_pham.Visible = false;
            // 
            // c01_hang_hoa_chi_tiet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(67)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.m_xtra_scroll);
            this.Controls.Add(this.panelControl1);
            this.Name = "c01_hang_hoa_chi_tiet";
            this.Size = new System.Drawing.Size(934, 546);
            ((System.ComponentModel.ISupportInitialize)(this.nhanXetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource nhanXetBindingSource;
        private DevExpress.XtraEditors.XtraScrollableControl m_xtra_scroll;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl m_lbl_tag;
        private DevExpress.XtraEditors.LabelControl m_lbl_hang_hoa_master;
        private DevExpress.XtraEditors.LabelControl m_lbl_ten_san_pham;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton m_btn_xoa_san_pham;
    }
}
