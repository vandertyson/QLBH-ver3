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
using QLBH.Controls.Common_Controls;

namespace QLBH.Controls.Hóa_đơn
{
    public partial class c02_hoa_don_chi_tiet : UserControl
    {
        #region Public Data Member 
        public HoaDonChiTiet m_hoa_don_chi_tiet;
        public KhuyenMaiDangApDung m_current_km;
        public SizeSoLuongHienTai m_current_size_sl;
        public HangHoa m_current_hang_hoa;
        public string m_current_gia_ban;
        public List<HangHoa> m_list_hang_hoa;
        #endregion

        #region Public Event Handler
        public event EventHandler ButtonAClick;
        #endregion

        #region Public Methods
        public c02_hoa_don_chi_tiet()
        {
            InitializeComponent();
        }
        public c02_hoa_don_chi_tiet(decimal id_cua_hang, DateTime now)
        {
            LayDanhSachHangHoa(id_cua_hang, now, this.TopLevelControl as Form, data => {
                InitializeComponent();
                set_define_event();
                m_list_hang_hoa = data.Data.Where(s => s.san_co != null).ToList();
                m_c0_hang_hoa_simple = new c01_hang_hoa_simple(id_cua_hang, now, m_list_hang_hoa);
                m_c0_size_so_luong = new c02_size_so_luong(m_c0_hang_hoa_simple.m_hang_hoa.san_co);
                m_c0_gia_tien = new c0_gia_tien(m_c0_hang_hoa_simple.m_hang_hoa.gia_hien_tai.ToString());
                m_c02_khuyen_mai = new c02_khuyen_mai_simple(m_c0_hang_hoa_simple.m_hang_hoa);
            });
        }
        public c02_hoa_don_chi_tiet(decimal id_cua_hang, DateTime now, List<HangHoa> m_list_hang_hoa)
        {
                InitializeComponent();
                set_define_event();
                m_list_hang_hoa = m_list_hang_hoa.Where(s => s.san_co != null).ToList();
                m_c0_hang_hoa_simple = new c01_hang_hoa_simple(id_cua_hang, now, m_list_hang_hoa);
                m_c0_size_so_luong = new c02_size_so_luong(m_c0_hang_hoa_simple.m_hang_hoa.san_co);
                m_c0_gia_tien = new c0_gia_tien(m_c0_hang_hoa_simple.m_hang_hoa.gia_hien_tai.ToString());
                m_c02_khuyen_mai = new c02_khuyen_mai_simple(m_c0_hang_hoa_simple.m_hang_hoa);
        }
        private void set_define_event()
        {
            m_c0_hang_hoa_simple.ChonHangHoa += M_c0_hang_hoa_simple_ChonHangHoa;
            m_btn_remove.Click += M_btn_remove_Click;
            m_c0_size_so_luong.ChonSize += M_c0_size_so_luong_ChonSize;
            m_c0_size_so_luong.ChonSoLuong += M_c0_size_so_luong_ChonSoLuong;
            m_c02_khuyen_mai.ChonKhuyenMai += M_c02_khuyen_mai_ChonKhuyenMai;
        }

        private void M_c02_khuyen_mai_ChonKhuyenMai(object sender, EventArgs e)
        {
            try
            {
                m_current_km = sender as KhuyenMaiDangApDung;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void M_c0_size_so_luong_ChonSoLuong(object sender, EventArgs e)
        {
            try
            {
                m_current_size_sl.so_luong = (int)sender;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_c0_size_so_luong_ChonSize(object sender, EventArgs e)
        {
            m_current_size_sl.ten_size = (string)sender;
        }

        private void M_btn_remove_Click(object sender, EventArgs e)
        {
            try
            {
                this.Parent.Controls.Remove(this);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void M_c0_hang_hoa_simple_ChonHangHoa(object sender, EventArgs e)
        {
            try
            {
                m_current_hang_hoa = sender as HangHoa;
            }
            catch (Exception)
            {
                throw;
            }
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
