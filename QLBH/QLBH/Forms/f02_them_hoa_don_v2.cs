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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.Utils;

namespace QLBH.Forms
{

    public partial class f02_them_hoa_don_v2 : Form
    {
        #region Public Interface
        public f02_them_hoa_don_v2()
        {
            InitializeComponent();
            this.CenterToParent();
            this.WindowState = FormWindowState.Maximized;
            set_define_event();
        }
        #endregion

        #region Members
        private List<HangHoa> m_list_hang_hoa;
        private List<KhachHang> m_list_khach_hang;
        private HoaDon m_hoa_don;
        private HangHoa m_current_hang_hoa;
        #endregion

        #region Private Methods

        private void save_data()
        {

        }

        private void form_to_hoa_don()
        {
            //to master

            //to detail

        }

        private bool data_ready_to_import()
        {
            return true;
        }

        private void set_init_form_load()
        {
            LayMaHoaDon(this, data =>
            {
                m_hoa_don = new HoaDon();
                m_hoa_don.cua_hang = new CuaHang();
                //
                m_hoa_don.cua_hang.ten_cua_hang = m_lbl_ten_cua_hang.Text = SystemInfo.ten_cua_hang;
                m_hoa_don.cua_hang.dia_chi = m_lbl_dia_chi.Text = SystemInfo.dia_chi_cua_hang;
                m_hoa_don.cua_hang.so_dien_thoai = m_lbl_dien_thoai.Text = SystemInfo.so_dien_thoai;
                m_lbl_nhan_vien.Text = SystemInfo.nhan_vien;
                m_dat_thoi_gian_tao.EditValue = DateTime.Now;
                m_hoa_don.ma_hoa_don = m_lbl_ma_hoa_don.Text = data.Data;
            });
            LayDanhSachKhachHang(DateTime.Now, this, data =>
            {
                m_list_khach_hang = data.Data;
                data_to_sle_khach_hang();
            });
            LayDanhSachHangHoa(SystemInfo.id_cua_hang, DateTime.Now, this, data =>
            {
                m_list_hang_hoa = data.Data;
                data_to_sle_hang_hoa();
            });
        }

        private void data_to_sle_khach_hang()
        {
            m_sle_khach_hang.Properties.DataSource = CommonFunction.list_to_data_table<KhachHang>(m_list_khach_hang);
            m_sle_khach_hang.Properties.DisplayMember = "ten_khach_hang";
            m_sle_khach_hang.Properties.ValueMember = "tai_khoan";
        }

        private void data_to_sle_size(HangHoa ip_hang)
        {
            m_sle_size.DataSource = CommonFunction.list_to_data_table(ip_hang.san_co.Where(s => s.so_luong != 0).ToList());
            m_sle_size.ValueMember = "ten_size";
            m_sle_size.DisplayMember = "ten_size";
        }

        private void data_to_sle_hang_hoa()
        {
            List<string> prop_name = new List<string> { "ma_hang_hoa", "ten_hang_hoa", "gia_hien_tai", };
            var ds = CommonFunction.convert_list_to_data_table<HangHoa>(prop_name, m_list_hang_hoa.Where(s => s.san_co.Count > 0).ToList());

            m_sle_hang_hoa.DataSource = ds;
            m_sle_hang_hoa.DisplayMember = "ten_hang_hoa";
            m_sle_hang_hoa.ValueMember = "ma_hang_hoa";
        }

        private void data_to_sle_so_luong(SizeSoLuongHienTai ip_ssl)
        {
            DataTable tb = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "so_luong";
            col.DataType = typeof(string);
            tb.Columns.Add(col);
            for (int i = 0; i < ip_ssl.so_luong; i++)
            {
                var value = (i + 1).ToString();
                tb.Rows.Add(value);
            }
            m_sle_so_luong.DataSource = tb;
            m_sle_so_luong.ValueMember = "so_luong";
            m_sle_so_luong.DisplayMember = "so_luong";
        }

        private void data_to_sle_size_khuyen_mai(HangHoa hang)
        {
            m_sle_khuyen_mai.DataSource = CommonFunction.list_to_data_table(hang.km_dang_ap_ung);
            m_sle_khuyen_mai.ValueMember = "ma_dot_khuyen_mai";
            m_sle_khuyen_mai.DisplayMember = "muc_khuyen_mai";
        }

        #endregion

        #region Event Handlers
        private void set_define_event()
        {
            this.Load += F02_them_hoa_don_v2_Load;
            m_btn_hoa_don_moi.Click += M_btn_hoa_don_moi_Click;
            m_btn_save.Click += M_btn_save_Click;
            m_btn_xuat_hoa_don.Click += M_btn_xuat_hoa_don_Click;
            m_btn_thoat.Click += M_btn_thoat_Click;
            m_sle_khach_hang.EditValueChanged += M_sle_khach_hang_EditValueChanged;
            m_sle_hang_hoa.EditValueChanged += M_sle_hang_hoa_EditValueChanged;
            m_sle_size.EditValueChanged += M_sle_size_EditValueChanged;
            m_sle_so_luong.EditValueChanged += M_sle_so_luong_EditValueChanged;
            m_btn_them_chi_tiet.Click += M_btn_them_chi_tiet_Click;
            m_grv_hoa_don_chi_tiet.InitNewRow += M_grv_hoa_don_chi_tiet_InitNewRow;
        }

        private void M_grv_hoa_don_chi_tiet_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                m_grv_hoa_don_chi_tiet.FocusedRowHandle = e.RowHandle;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_btn_them_chi_tiet_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void M_sle_so_luong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var p = sender as SearchLookUpEdit;
                var sl = p.EditValue.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_sle_size_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var p = sender as SearchLookUpEdit;
                p.Properties.DisplayMember = "ten_size";
                p.Properties.ValueMember = "ten_size";
                string size = p.EditValue.ToString();
                var ssl = m_current_hang_hoa.san_co.Where(s => s.ten_size == size).First();
                data_to_sle_so_luong(ssl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void M_sle_hang_hoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var p = sender as SearchLookUpEdit;
                p.Properties.DisplayMember = "ten_hang_hoa";
                p.Properties.ValueMember = "ma_hang_hoa";
                string ma = p.EditValue.ToString();
                m_current_hang_hoa = m_list_hang_hoa.Where(s => s.ma_hang_hoa == ma).First();
                data_to_sle_size(m_current_hang_hoa);
                data_to_sle_size_khuyen_mai(m_current_hang_hoa);
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        private void M_sle_khach_hang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_khach_hang.EditValue == null | String.IsNullOrEmpty(m_sle_khach_hang.Text) | String.IsNullOrWhiteSpace(m_sle_khach_hang.Text))
                {
                    m_sle_khach_hang.Text = "Khách vãng lai";
                }
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        private void M_btn_thoat_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        private void M_btn_xuat_hoa_don_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        private void M_btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (!data_ready_to_import())
                {
                    XtraMessageBox.Show("Vui lòng kiểm tra lại dữ liệu");
                }
                form_to_hoa_don();
                save_data();
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        private void M_btn_hoa_don_moi_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        private void F02_them_hoa_don_v2_Load(object sender, EventArgs e)
        {
            try
            {
                set_init_form_load();
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        #endregion

    }
}
