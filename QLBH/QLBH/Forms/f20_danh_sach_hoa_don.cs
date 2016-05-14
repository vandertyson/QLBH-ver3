using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LibraryApi.QuanLyHoaDon;
using QLBH.Common;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;

namespace QLBH.Forms
{
    public partial class f20_danh_sach_hoa_don : Form
    {
        #region Public Interfaces
        public f20_danh_sach_hoa_don()
        {
            InitializeComponent();
            this.CenterToScreen();
            set_define_event();
        }
        #endregion
        #region Members

        private List<HoaDon> m_data_hoa_don;
       

        #endregion
        #region Data Structures
        #endregion

        #region Private Methods

        private void load_data_to_grid()
        {

            if (m_dat_ngay_hien_tai.EditValue != null)
            {
                LayDanhSachHoaDon(m_dat_ngay_hien_tai.DateTime, this, data =>
                {
                    m_data_hoa_don = data.Data;
                    List<string> prop_name = new List<string> { "ma_hoa_don", "thoi_gian_tao", "ten_cua_hang", "tai_khoan_tao" };
                    m_grc_hoa_don.DataSource = CommonFunction.convert_list_to_data_table<HoaDon>(prop_name, m_data_hoa_don);

                });
            }
            else
            {
                LayDanhSachHoaDon(this, data =>
                {
                    m_data_hoa_don = data.Data;
                    List<string> prop_name = new List<string> { "ma_hoa_don", "thoi_gian_tao", "ten_cua_hang", "tai_khoan_tao" };
                    m_grc_hoa_don.DataSource = CommonFunction.convert_list_to_data_table<HoaDon>(prop_name, m_data_hoa_don);

                });
            }
        }

        #endregion

        #region Event Handlers

        private void set_define_event()
        {
            this.Load += F14_danh_sach_phieu_nhap_xuat_Load;
            m_btn_xem.Click += M_btn_xem_Click;
            m_btn_them_phieu.Click += M_btn_them_phieu_Click;
            m_btn_sua.Click += M_btn_sua_Click;
            m_btn_xoa.Click += M_btn_xoa_Click;
            m_btn_xem_chi_tiet.Click += M_btn_xem_chi_tiet_Click;

        }

        private void M_btn_xem_chi_tiet_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_grv_hoa_don.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Chọn hóa đơn để xem chi tiết");
                    return;
                }
                HoaDon p = m_data_hoa_don[m_grv_hoa_don.FocusedRowHandle];
                f02_them_hoa_don_v2 v_f = new f02_them_hoa_don_v2();
                v_f.display_chi_tiet(p);
                load_data_to_grid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.InnerException.Message);
            }
        }

        private void M_btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_grv_hoa_don.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Chọn hóa đơn để xóa");
                    return;
                }
                if (CommonFunction.MsgBox_Yes_No_Cancel("Bạn có chắc chắn muốn xóa dữ liệu này?", "Xác nhận xóa dữ liệu") == DialogResult.Yes)
                {
                    HoaDon p = m_data_hoa_don[m_grv_hoa_don.FocusedRowHandle];
                    XoaHoaDon(p.ma_hoa_don, this, data =>
                    {
                        if (data.Success)
                        {
                            XtraMessageBox.Show(data.Message);
                            load_data_to_grid();
                        }
                    });
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.InnerException.Message);
            }
        }

        private void M_btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_grv_hoa_don.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Chọn hóa đơn để sửa");
                    return;
                }
                HoaDon p = m_data_hoa_don[m_grv_hoa_don.FocusedRowHandle];
                f02_them_hoa_don_v2 v_f = new f02_them_hoa_don_v2();
                v_f.display_update(p);
                load_data_to_grid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.InnerException.Message);
            }
        }

        private void M_btn_them_phieu_Click(object sender, EventArgs e)
        {
            try
            {
                f02_them_hoa_don_v2 v_f = new f02_them_hoa_don_v2();
                v_f.display_them_moi();
                load_data_to_grid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.InnerException.Message);
            }
        }

        private void M_btn_xem_Click(object sender, EventArgs e)
        {
            try
            {
                load_data_to_grid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.InnerException.Message);
            }
        }

        private void F14_danh_sach_phieu_nhap_xuat_Load(object sender, EventArgs e)
        {

        }
        #endregion
      
    }
}
