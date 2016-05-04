namespace QLBH.Controls.Common_Controls
{
    partial class c0_khach_hang_simple
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(c0_khach_hang_simple));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.m_sle_khach_hang = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_lbl_so_dien_thoai = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.m_lbl_email = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.m_imgl_anh_dai_dien = new System.Windows.Forms.ImageList(this.components);
            this.m_isl_anh_dai_dien = new DevExpress.XtraEditors.Controls.ImageSlider();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sle_khach_hang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_isl_anh_dai_dien)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.m_lbl_email);
            this.layoutControl1.Controls.Add(this.m_lbl_so_dien_thoai);
            this.layoutControl1.Controls.Add(this.m_sle_khach_hang);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(93, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(271, 95);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(271, 95);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // m_sle_khach_hang
            // 
            this.m_sle_khach_hang.Location = new System.Drawing.Point(12, 12);
            this.m_sle_khach_hang.Name = "m_sle_khach_hang";
            this.m_sle_khach_hang.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_sle_khach_hang.Properties.Appearance.Options.UseFont = true;
            this.m_sle_khach_hang.Properties.Appearance.Options.UseTextOptions = true;
            this.m_sle_khach_hang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.m_sle_khach_hang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_sle_khach_hang.Properties.NullText = "Khách vãng lai";
            this.m_sle_khach_hang.Properties.View = this.searchLookUpEdit1View;
            this.m_sle_khach_hang.Size = new System.Drawing.Size(247, 28);
            this.m_sle_khach_hang.StyleController = this.layoutControl1;
            this.m_sle_khach_hang.TabIndex = 4;
            this.m_sle_khach_hang.ToolTip = "Chọn khách hàng";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.m_sle_khach_hang;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(251, 32);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // m_lbl_so_dien_thoai
            // 
            this.m_lbl_so_dien_thoai.Location = new System.Drawing.Point(105, 46);
            this.m_lbl_so_dien_thoai.Name = "m_lbl_so_dien_thoai";
            this.m_lbl_so_dien_thoai.Size = new System.Drawing.Size(60, 13);
            this.m_lbl_so_dien_thoai.StyleController = this.layoutControl1;
            this.m_lbl_so_dien_thoai.TabIndex = 5;
            this.m_lbl_so_dien_thoai.Text = "0912009112";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.m_lbl_so_dien_thoai;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.OptionsToolTip.ToolTip = "Số điện thoại";
            this.layoutControlItem2.Size = new System.Drawing.Size(251, 21);
            this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // m_lbl_email
            // 
            this.m_lbl_email.Location = new System.Drawing.Point(89, 67);
            this.m_lbl_email.Name = "m_lbl_email";
            this.m_lbl_email.Size = new System.Drawing.Size(92, 13);
            this.m_lbl_email.StyleController = this.layoutControl1;
            this.m_lbl_email.TabIndex = 6;
            this.m_lbl_email.Text = "abc123@gmail.com";
            this.m_lbl_email.ToolTip = "Email";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.m_lbl_email;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 53);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(251, 22);
            this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // m_imgl_anh_dai_dien
            // 
            this.m_imgl_anh_dai_dien.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgl_anh_dai_dien.ImageStream")));
            this.m_imgl_anh_dai_dien.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgl_anh_dai_dien.Images.SetKeyName(0, "blank_avatar.png");
            // 
            // m_isl_anh_dai_dien
            // 
            this.m_isl_anh_dai_dien.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_isl_anh_dai_dien.Images.Add(((System.Drawing.Image)(resources.GetObject("m_isl_anh_dai_dien.Images"))));
            this.m_isl_anh_dai_dien.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.Stretch;
            this.m_isl_anh_dai_dien.Location = new System.Drawing.Point(0, 0);
            this.m_isl_anh_dai_dien.Name = "m_isl_anh_dai_dien";
            this.m_isl_anh_dai_dien.Size = new System.Drawing.Size(93, 95);
            this.m_isl_anh_dai_dien.TabIndex = 0;
            // 
            // c0_khach_hang_simple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.m_isl_anh_dai_dien);
            this.Name = "c0_khach_hang_simple";
            this.Size = new System.Drawing.Size(364, 95);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sle_khach_hang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_isl_anh_dai_dien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Controls.ImageSlider m_isl_anh_dai_dien;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LabelControl m_lbl_email;
        private DevExpress.XtraEditors.LabelControl m_lbl_so_dien_thoai;
        private DevExpress.XtraEditors.SearchLookUpEdit m_sle_khach_hang;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.ImageList m_imgl_anh_dai_dien;
    }
}
