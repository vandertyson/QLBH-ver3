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

        #region Data Structure

        #endregion

        #region Members
        private List<HangHoa> m_list_hang_hoa;
        private List<KhachHang> m_list_khach_hang;
        private HoaDon m_hoa_don;
        private HangHoa m_current_hang_hoa;
        private DataTable m_dt_chi_tiet;
        decimal tong_tien = 0;
        decimal tong_giam_tru = 0;
        decimal thanh_tien = 0;
        private decimal giam_tru_khac_hang = 0;
        private KhachHang m_current_khach;
        #endregion

        #region Private Methods

        private void save_data()
        {
            var list = CommonFunction.DataTableToList<HoaDonChiTiet>(m_dt_chi_tiet);
            m_hoa_don.chi_tiet = list;
            m_hoa_don.loai_thanh_toan = "TT";
            m_hoa_don.thoi_gian_tao = m_dat_thoi_gian_tao.DateTime;
            m_hoa_don.khach = m_current_khach;
            m_hoa_don.tong_gia_tri_hoa_don = tong_tien;
            m_hoa_don.giam_tru = tong_giam_tru;
            ThemHoaDon(m_hoa_don, this, data =>
            {
                if (data.Success)
                {
                    XtraMessageBox.Show("Đã thêm hóa đơn thành công");
                }
            });
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
            tong_tien = 0;
            tong_giam_tru = 0;
            thanh_tien = 0;
            giam_tru_khac_hang = 0;
            m_dat_thoi_gian_tao.EditValue = DateTime.Now;
            m_dt_chi_tiet = CommonFunction.create_table_form_struct(typeof(HoaDonChiTiet));
            LayMaHoaDon(this, data =>
            {
                m_hoa_don = new HoaDon();
                m_hoa_don.id_cua_hang = SystemInfo.id_cua_hang;
                m_hoa_don.khach = new KhachHang();
                m_hoa_don.chi_tiet = new List<HoaDonChiTiet>();
                //
                m_lbl_ten_cua_hang.Text = SystemInfo.ten_cua_hang;
                m_lbl_dia_chi.Text = SystemInfo.dia_chi_cua_hang;
                m_lbl_dien_thoai.Text = SystemInfo.so_dien_thoai;
                m_lbl_nhan_vien.Text = SystemInfo.nhan_vien;
                m_dat_thoi_gian_tao.EditValue = DateTime.Now;
                m_hoa_don.ma_hoa_don = m_lbl_ma_hoa_don.Text = data.Data;
            });
            LayDanhSachKhachHang(DateTime.Now, this, data =>
            {
                m_list_khach_hang = data.Data;
                data_to_sle_khach_hang();
            });
            //LayDanhSachHangHoa(SystemInfo.id_cua_hang, DateTime.Now, this, data =>
            //{
            //    m_list_hang_hoa = data.Data;
            //    data_to_sle_hang_hoa();
            //});
        }

        private void data_to_sle_khach_hang()
        {
            m_sle_khach_hang.Properties.DataSource = CommonFunction.list_to_data_table<KhachHang>(m_list_khach_hang);
            m_sle_khach_hang.Properties.DisplayMember = "ten_khach_hang";
            m_sle_khach_hang.Properties.ValueMember = "tai_khoan";
        }

        private void data_to_sle_size(HangHoa ip_hang)
        {
            m_sle_size.Properties.DataSource = CommonFunction.list_to_data_table(ip_hang.san_co.Where(s => s.so_luong != 0).ToList());
            m_sle_size.Properties.ValueMember = "ten_size";
            m_sle_size.Properties.DisplayMember = "ten_size";
        }

        private void data_to_sle_hang_hoa()
        {
            List<string> prop_name = new List<string> { "ma_hang_hoa", "ten_hang_hoa", "gia_hien_tai", };
            var ds = CommonFunction.convert_list_to_data_table<HangHoa>(prop_name, m_list_hang_hoa.Where(s => s.san_co.Count > 0).ToList());

            m_sle_hang_hoa.Properties.DataSource = ds;
            m_sle_hang_hoa.Properties.DisplayMember = "ten_hang_hoa";
            m_sle_hang_hoa.Properties.ValueMember = "ma_hang_hoa";
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
            m_sle_so_luong.Properties.DataSource = tb;
            m_sle_so_luong.Properties.ValueMember = "so_luong";
            m_sle_so_luong.Properties.DisplayMember = "so_luong";
        }

        private void data_to_sle_size_khuyen_mai(HangHoa hang)
        {
            m_sle_khuyen_mai.Properties.DataSource = CommonFunction.list_to_data_table(hang.km_dang_ap_ung);
            m_sle_khuyen_mai.Properties.ValueMember = "ma_dot_khuyen_mai";
            m_sle_khuyen_mai.Properties.DisplayMember = "muc_khuyen_mai";
        }

        private void tinh_gia_ban()
        {
            if (m_sle_hang_hoa.EditValue == null)
            {
                return;
            }
            if (m_sle_khuyen_mai.Text == "0")
            {
                m_txt_gia_ban.EditValue = m_current_hang_hoa.gia_hien_tai;
                return;
            }
            var gia_ban = m_current_hang_hoa.gia_hien_tai * (1 - Convert.ToDecimal(m_sle_khuyen_mai.Text.ToString()));
            m_txt_gia_ban.Text = gia_ban.ToString();
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
            m_dat_thoi_gian_tao.EditValueChanged += M_dat_thoi_gian_tao_EditValueChanged;
            m_sle_so_luong.EditValueChanged += M_sle_so_luong_EditValueChanged;
            m_btn_them_chi_tiet.Click += M_btn_them_chi_tiet_Click;
            m_sle_khuyen_mai.EditValueChanged += M_sle_khuyen_mai_EditValueChanged;
        }

        private void M_dat_thoi_gian_tao_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_dat_thoi_gian_tao.EditValue == null)
                {
                    m_dat_thoi_gian_tao.EditValue = DateTime.Now;
                }
                LayDanhSachHangHoa(SystemInfo.id_cua_hang, m_dat_thoi_gian_tao.DateTime, this, data =>
                {
                    m_list_hang_hoa = data.Data;
                    data_to_sle_hang_hoa();
                });
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Đã có lỗi xảy ra. Thông cảm vì chương trình vẫn đang được hoàn thiện");
            }
        }

        private void M_sle_khuyen_mai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_khuyen_mai.EditValue != null)
                {
                    tinh_gia_ban();
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Đã có lỗi xảy ra. Thông cảm vì chương trình vẫn đang được hoàn thiện");
            }
        }

        private void M_btn_them_chi_tiet_Click(object sender, EventArgs e)
        {
            try
            {
                var list_thuoc_tinh = new List<object>();
                if (m_sle_hang_hoa.EditValue == null
                    | m_sle_size.EditValue == null
                    | m_sle_so_luong.EditValue == null
                    | String.IsNullOrEmpty(m_txt_gia_ban.Text))
                {
                    XtraMessageBox.Show("Vui lòng nhập đủ thông tin hóa đơn chi tiết");
                    return;
                }
                foreach (DataRow item in m_dt_chi_tiet.Rows)
                {
                    if (item["ma_hang"].ToString() == m_sle_hang_hoa.EditValue.ToString())
                    {
                        XtraMessageBox.Show("Hàng hóa đã được thêm");
                        return;
                    }
                }
                list_thuoc_tinh.Add(m_sle_hang_hoa.EditValue);
                list_thuoc_tinh.Add(m_sle_size.EditValue);
                list_thuoc_tinh.Add(m_sle_so_luong.Text);
                list_thuoc_tinh.Add(m_sle_khuyen_mai.Text);
                list_thuoc_tinh.Add(m_txt_gia_ban.Text);
                //
                tong_tien += m_current_hang_hoa.gia_hien_tai * Convert.ToDecimal(m_sle_so_luong.EditValue);
                tong_giam_tru += m_current_hang_hoa.gia_hien_tai * Convert.ToDecimal(m_sle_khuyen_mai.Text);
                thanh_tien = tong_tien - tong_giam_tru - giam_tru_khac_hang;
                //
                m_lbl_giam_tru_khuyen_mai.Text += " " + tong_giam_tru.ToString();
                m_lbl_thanh_tien.Text += " " + thanh_tien.ToString();
                //
                m_dt_chi_tiet.Rows.Add(list_thuoc_tinh.ToArray());
                m_grc_chi_tiet.DataSource = m_dt_chi_tiet;
                m_sle_hang_hoa.EditValue = null;
                m_sle_size.EditValue = null;
                m_sle_so_luong.EditValue = null;
                m_sle_khuyen_mai.EditValue = null;
                m_txt_gia_ban.Text = "";
                //

            }
            catch (Exception)
            {
                XtraMessageBox.Show("Đã xảy ra 1 số lỗi");
            }
        }

        private void M_sle_so_luong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_so_luong.EditValue == null)
                {
                    return;
                }
                var sl = m_sle_so_luong.EditValue.ToString();
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
                if (m_sle_size.EditValue == null)
                {
                    return;
                }
                string size = m_sle_size.EditValue.ToString();
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
                if (m_sle_hang_hoa.EditValue == null)
                {
                    return;
                }
                string ma = (string)m_sle_hang_hoa.EditValue;
                m_current_hang_hoa = m_list_hang_hoa.Where(s => s.ma_hang_hoa == ma).First();
                data_to_sle_size(m_current_hang_hoa);
                data_to_sle_size_khuyen_mai(m_current_hang_hoa);
                tinh_gia_ban();
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
                if (m_sle_khach_hang.Text == "Khách vãng lai")
                {
                    m_current_khach = m_list_khach_hang.Where(s => s.tai_khoan == "customer").First();
                    m_lbl_giam_tru_khach_hang.Text += " " + m_current_khach.diem_giam_tru;
                    return;
                }
                m_current_khach = m_list_khach_hang.Where(s => s.tai_khoan == m_sle_khach_hang.EditValue.ToString()).First();
                m_lbl_giam_tru_khach_hang.Text += " " + m_current_khach.diem_giam_tru;
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
                //if (!data_ready_to_import())
                //{
                //    XtraMessageBox.Show("Vui lòng kiểm tra lại dữ liệu");
                //}
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
                set_init_form_load();
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
