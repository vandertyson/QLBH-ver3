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
    public partial class c02_hoa_don : UserControl
    {
        #region Public Data Member 
        //public string ma_hoa_don { get; set; }
        //public DateTime thoi_gian_tao { get; set; }
        //public CuaHang cua_hang { get; set; }
        //public KhachHang khach { get; set; }
        //public string loai_thanh_toan { get; set; }
        //public decimal giam_tru { get; set; }
        //public decimal tong_gia_tri_hoa_don { get; set; }
        //public decimal tong_tien_giam_tru_km { get; set; }
        public HoaDon m_hoa_don;
        #endregion

        #region Public Event Handler
        public event EventHandler ButtonAClick;
        #endregion

        #region Public Methods
        public c02_hoa_don()
        {
            InitializeComponent();
        }

        public c02_hoa_don(DateTime now)
        {
            LayMaHoaDon(this.TopLevelControl as Form, data =>
             {
                 InitializeComponent();
                 set_define_event();
                 m_hoa_don = new HoaDon();
                 m_hoa_don.cua_hang = new CuaHang();
                 m_hoa_don.cua_hang.ten_cua_hang = m_lbl_ten_cua_hang.Text = SystemInfo.ten_cua_hang;
                 m_hoa_don.cua_hang.dia_chi = m_lbl_dia_chi.Text = SystemInfo.dia_chi_cua_hang;
                 m_hoa_don.cua_hang.so_dien_thoai = m_lbl_sdt.Text = SystemInfo.so_dien_thoai;
                 m_lbl_thoi_gian.Text = SystemInfo.nhan_vien;
                 m_dat_thoi_gian_hoa_don.EditValue = DateTime.Now;
                 m_c0_khach_hang_simple = new c0_khach_hang_simple(DateTime.Now);
                 m_hoa_don.ma_hoa_don = m_lbl_ma_hoa_don.Text = data.Data;
                 data_to_hinh_thuc_thanh_toan();
             });
        }
        public c02_hoa_don(DateTime now, string ma_hoa_don)
        {
            InitializeComponent();
            set_define_event();
            m_hoa_don = new HoaDon();
            m_hoa_don.cua_hang = new CuaHang();
            m_hoa_don.cua_hang.ten_cua_hang = m_lbl_ten_cua_hang.Text = SystemInfo.ten_cua_hang;
            m_hoa_don.cua_hang.dia_chi = m_lbl_dia_chi.Text = SystemInfo.dia_chi_cua_hang;
            m_hoa_don.cua_hang.so_dien_thoai = m_lbl_sdt.Text = SystemInfo.so_dien_thoai;
            m_lbl_thoi_gian.Text = SystemInfo.nhan_vien;
            m_dat_thoi_gian_hoa_don.EditValue = DateTime.Now;
            m_c0_khach_hang_simple = new c0_khach_hang_simple(DateTime.Now);
            m_hoa_don.ma_hoa_don = m_lbl_ma_hoa_don.Text = ma_hoa_don;
            data_to_hinh_thuc_thanh_toan();
        }
        public void refresh()
        {

        }
        #endregion

        #region Private Methods
        private void data_to_hinh_thuc_thanh_toan()
        {
            DataTable tb = new DataTable();
            DataColumn col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "";
            tb.Columns.Add(col);
            tb.Rows.Add(new object[] { "VND" });

            m_sle_hinh_thuc_thanh_toan.Properties.DataSource = tb;
            m_sle_hinh_thuc_thanh_toan.Properties.DisplayMember = "ten";
        }
        #endregion

        #region Event Handlers
        private void set_define_event()
        {
            m_sle_hinh_thuc_thanh_toan.EditValueChanged += M_sle_hinh_thuc_thanh_toan_EditValueChanged;
            m_c0_khach_hang_simple.ChonKhachHang += M_c0_khach_hang_simple_ChonKhachHang;
            m_dat_thoi_gian_hoa_don.EditValueChanged += M_dat_thoi_gian_hoa_don_EditValueChanged;
        }

        private void M_dat_thoi_gian_hoa_don_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_dat_thoi_gian_hoa_don.EditValue == null)
                {
                    m_dat_thoi_gian_hoa_don.EditValue = DateTime.Now.Date;
                }
                m_hoa_don.thoi_gian_tao = m_dat_thoi_gian_hoa_don.DateTime;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_c0_khach_hang_simple_ChonKhachHang(object sender, EventArgs e)
        {
            try
            {
                m_hoa_don.khach = sender as KhachHang;
                m_txt_giam_tru_thanh_vien.Text = m_hoa_don.khach.diem_giam_tru.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_sle_hinh_thuc_thanh_toan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_hinh_thuc_thanh_toan.EditValue == null | String.IsNullOrEmpty(m_sle_hinh_thuc_thanh_toan.Text) | String.IsNullOrWhiteSpace(m_sle_hinh_thuc_thanh_toan.Text))
                {
                    m_sle_hinh_thuc_thanh_toan.Text = "TT";
                }
                m_hoa_don.loai_thanh_toan = m_sle_hinh_thuc_thanh_toan.Text;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion








    }
}
