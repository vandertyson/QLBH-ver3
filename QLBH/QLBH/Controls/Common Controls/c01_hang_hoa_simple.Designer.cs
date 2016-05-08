namespace QLBH.Controls
{
    partial class c01_hang_hoa_simple
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(c01_hang_hoa_simple));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.m_lbl_gia = new DevExpress.XtraEditors.LabelControl();
            this.m_lbl_ma_hang_hoa = new DevExpress.XtraEditors.LabelControl();
            this.m_sle_hang_hoa = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.m_imgl_anh_hang_hoa = new System.Windows.Forms.ImageList(this.components);
            this.m_isl_hinh_anh = new DevExpress.XtraEditors.Controls.ImageSlider();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_sle_hang_hoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_isl_hinh_anh)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.m_lbl_gia);
            this.layoutControl1.Controls.Add(this.m_lbl_ma_hang_hoa);
            this.layoutControl1.Controls.Add(this.m_sle_hang_hoa);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(67, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(352, 432, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(242, 64);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // m_lbl_gia
            // 
            this.m_lbl_gia.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.TopRight;
            this.m_lbl_gia.Location = new System.Drawing.Point(117, 38);
            this.m_lbl_gia.Name = "m_lbl_gia";
            this.m_lbl_gia.Size = new System.Drawing.Size(59, 13);
            this.m_lbl_gia.StyleController = this.layoutControl1;
            this.m_lbl_gia.TabIndex = 6;
            this.m_lbl_gia.Text = "1.000.000 đ";
            this.m_lbl_gia.ToolTip = "Giá bán hiện tại";
            // 
            // m_lbl_ma_hang_hoa
            // 
            this.m_lbl_ma_hang_hoa.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbl_ma_hang_hoa.Location = new System.Drawing.Point(12, 38);
            this.m_lbl_ma_hang_hoa.Name = "m_lbl_ma_hang_hoa";
            this.m_lbl_ma_hang_hoa.Size = new System.Drawing.Size(48, 13);
            this.m_lbl_ma_hang_hoa.StyleController = this.layoutControl1;
            this.m_lbl_ma_hang_hoa.TabIndex = 5;
            this.m_lbl_ma_hang_hoa.Text = "BCS004XL";
            this.m_lbl_ma_hang_hoa.ToolTip = "Mã hàng hóa";
            // 
            // m_sle_hang_hoa
            // 
            this.m_sle_hang_hoa.Location = new System.Drawing.Point(12, 12);
            this.m_sle_hang_hoa.Name = "m_sle_hang_hoa";
            this.m_sle_hang_hoa.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_sle_hang_hoa.Properties.Appearance.Options.UseFont = true;
            this.m_sle_hang_hoa.Properties.Appearance.Options.UseTextOptions = true;
            this.m_sle_hang_hoa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.m_sle_hang_hoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_sle_hang_hoa.Properties.NullText = "Chọn hàng hóa";
            this.m_sle_hang_hoa.Properties.View = this.searchLookUpEdit1View;
            this.m_sle_hang_hoa.Size = new System.Drawing.Size(218, 22);
            this.m_sle_hang_hoa.StyleController = this.layoutControl1;
            this.m_sle_hang_hoa.TabIndex = 4;
            this.m_sle_hang_hoa.ToolTip = "Chọn sản phẩm";
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(242, 64);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.m_sle_hang_hoa;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(222, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.m_lbl_ma_hang_hoa;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(52, 18);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.m_lbl_gia;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(52, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(170, 18);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup1";
            this.layoutControlGroup2.Size = new System.Drawing.Size(271, 95);
            this.layoutControlGroup2.Text = "layoutControlGroup1";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup1";
            this.layoutControlGroup3.Size = new System.Drawing.Size(271, 95);
            this.layoutControlGroup3.Text = "layoutControlGroup1";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // m_imgl_anh_hang_hoa
            // 
            this.m_imgl_anh_hang_hoa.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgl_anh_hang_hoa.ImageStream")));
            this.m_imgl_anh_hang_hoa.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgl_anh_hang_hoa.Images.SetKeyName(0, "003_5.jpg");
            // 
            // m_isl_hinh_anh
            // 
            this.m_isl_hinh_anh.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.m_isl_hinh_anh.Appearance.Options.UseBackColor = true;
            this.m_isl_hinh_anh.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_isl_hinh_anh.Images.Add(((System.Drawing.Image)(resources.GetObject("m_isl_hinh_anh.Images"))));
            this.m_isl_hinh_anh.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.Stretch;
            this.m_isl_hinh_anh.Location = new System.Drawing.Point(2, 2);
            this.m_isl_hinh_anh.Name = "m_isl_hinh_anh";
            this.m_isl_hinh_anh.Padding = new System.Windows.Forms.Padding(2);
            this.m_isl_hinh_anh.Size = new System.Drawing.Size(65, 64);
            this.m_isl_hinh_anh.TabIndex = 0;
            this.m_isl_hinh_anh.Text = "imageSlider1";
            this.m_isl_hinh_anh.ToolTip = "Hình ảnh minh họa";
            // 
            // c01_hang_hoa_master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.m_isl_hinh_anh);
            this.Name = "c01_hang_hoa_master";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(311, 68);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_sle_hang_hoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_isl_hinh_anh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Controls.ImageSlider m_isl_hinh_anh;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.LabelControl m_lbl_ma_hang_hoa;
        private DevExpress.XtraEditors.SearchLookUpEdit m_sle_hang_hoa;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.ImageList m_imgl_anh_hang_hoa;
        private DevExpress.XtraEditors.LabelControl m_lbl_gia;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}
