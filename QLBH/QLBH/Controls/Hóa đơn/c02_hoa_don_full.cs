using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LibraryApi.QuanLyHoaDon;
using DevExpress.XtraEditors;
using QLBH.Common;
using DevExpress.XtraTab;


namespace QLBH.Controls.Hóa_đơn
{
    public partial class c02_hoa_don_full : UserControl
    {
        #region Public Data Member 
        public HoaDon m_hoa_don;
        public decimal m_id_cua_hang;
        public DateTime m_ngay_hien_tai;
        public List<HangHoa> m_list_hang_hoa;
        #endregion

        #region Public Event Handler
        public event EventHandler ButtonAClick;
        #endregion

        #region Public Methods
        public c02_hoa_don_full()
        {
            InitializeComponent();
        }
        public c02_hoa_don_full(decimal id_cua_hang, DateTime now, string ma_hoa_don)
        {
            LayDanhSachHangHoa(id_cua_hang, now, this.TopLevelControl as Form, data =>
            {
                InitializeComponent();
                m_list_hang_hoa = data.Data;
                m_id_cua_hang = id_cua_hang;
                m_ngay_hien_tai = now;
                set_define_event();
                m_c02_hoa_don = new c02_hoa_don(now, ma_hoa_don);
                m_c02_hoa_don_chi_tiet = new c02_hoa_don_chi_tiet(id_cua_hang, now, m_list_hang_hoa);
                m_hoa_don = m_c02_hoa_don.m_hoa_don;
                m_hoa_don.chi_tiet = new List<HoaDonChiTiet>();
            });
        }

        private void set_define_event()
        {
            m_btn_them_hang_hoa.Click += M_btn_them_hang_hoa_Click;
            m_btn_xoa_het.Click += M_btn_xoa_het_Click;
            m_btn_save.Click += M_btn_save_Click;
            m_btn_xuat_hoa_don.Click += M_btn_xuat_hoa_don_Click;
        }

        private void M_btn_xuat_hoa_don_Click(object sender, EventArgs e)
        {

        }

        private void M_btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                ThemHoaDon(m_hoa_don, this.TopLevelControl as Form, data =>
                  {
                      if (data.Success)
                      {
                          XtraMessageBox.Show("Đã thêm hóa đơn thành công");
                          (this.Parent as XtraTabControl).TabPages.Remove(this.Parent as XtraTabPage);//lol
                      }
                      else
                      {
                          XtraMessageBox.Show("Có 1 số lỗi xảy ra");
                      }
                  });
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
                throw;
            }

        }

        private void M_btn_xoa_het_Click(object sender, EventArgs e)
        {
            try
            {
                m_xtra_hoa_don_chi_tiet.Controls.Clear();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_btn_them_hang_hoa_Click(object sender, EventArgs e)
        {
            c02_hoa_don_chi_tiet ct = new c02_hoa_don_chi_tiet(m_id_cua_hang, m_ngay_hien_tai);
            ct.Dock = DockStyle.Top;
            m_xtra_hoa_don_chi_tiet.Controls.Add(ct);
            m_hoa_don.chi_tiet.Add(ct.m_hoa_don_chi_tiet);
            tinh_lai_tien();

        }

        private void tinh_lai_tien()
        {
            decimal tong_tien = 0;
            decimal tong_giam_tru_km = 0;
            foreach (var item in m_xtra_hoa_don_chi_tiet.Controls)
            {
                var p = item as c02_hoa_don_chi_tiet;
                tong_giam_tru_km += p.m_current_hang_hoa.gia_hien_tai * p.m_current_km.muc_khuyen_mai;
                tong_tien += p.m_current_hang_hoa.gia_hien_tai * p.m_current_size_sl.so_luong;
            }
            m_c02_hoa_don.m_txt_giam_tru_khuyen_mai.Text = tong_giam_tru_km.ToString() + "VND";
            m_c02_hoa_don.m_txt_tong_gia_tri_hoa_don.Text = tong_tien.ToString() + "VND";
            m_c02_hoa_don.m_txt_thanh_tien.Text = (tong_tien - tong_giam_tru_km - Convert.ToDecimal(m_c02_hoa_don.m_txt_giam_tru_thanh_vien.Text)).ToString() + "VND";
        }

        public void refresh()
        {

        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion



    }
}
