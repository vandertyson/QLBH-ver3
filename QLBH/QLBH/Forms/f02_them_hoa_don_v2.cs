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

        internal void display_chi_tiet(HoaDon p)
        {
            m_e_mode = Mode.XemChiTiet;
            this.m_hoa_don = p;
            data_to_form();
            this.ShowDialog();
        }

        internal void display_them_moi()
        {
            m_e_mode = Mode.ThemMoi;
            this.ShowDialog();
        }

        internal void display_update(HoaDon p)
        {
            m_e_mode = Mode.Sua;
            this.m_hoa_don = p;
            data_to_form();
            this.ShowDialog();
        }
        #endregion

        #region Data Structure
        enum Mode
        {
            XemChiTiet,
            Sua,
            ThemMoi
        }
        #endregion

        #region Members
        private Mode m_e_mode;
        private List<HangHoa> m_list_hang_hoa = new List<HangHoa>();
        private List<KhachHang> m_list_khach_hang = new List<KhachHang>();
        private HoaDon m_hoa_don = new HoaDon();
        private DataTable m_dt_chi_tiet = new DataTable();

        //
        decimal gia_ban = 0;
        private decimal giam_tru_khach_hang = 0;
        private decimal tong_gia_tri_hoa_don = 0;
        private decimal tong_giam_tru_khuyen_mai = 0;
        private decimal thanh_tien = 0;
        #endregion

        #region Private Methods
        private void data_to_form()
        {
            m_lbl_ten_cua_hang.Text = "Cửa hàng thời trang " + m_hoa_don.ten_cua_hang;
            m_lbl_nhan_vien.Text = "Người tạo: " + m_hoa_don.tai_khoan_tao;
            m_lbl_ma_hoa_don.Text = "Mã hóa đơn " + m_hoa_don.ma_hoa_don;
            m_dat_thoi_gian_tao.EditValue = m_hoa_don.thoi_gian_tao;
            m_dt_chi_tiet = CommonFunction.list_to_data_table(m_hoa_don.chi_tiet);
            m_grc_chi_tiet.DataSource = m_dt_chi_tiet;
        }

        private void save_data()
        {
            switch (m_e_mode)
            {
                case Mode.XemChiTiet:
                    break;
                case Mode.Sua:
                    form_to_data();
                    SuaHoaDon(m_hoa_don, this, data =>
                    {
                        XtraMessageBox.Show(data.Message);
                    });
                    break;
                case Mode.ThemMoi:
                    form_to_data();
                    ThemHoaDon(m_hoa_don, this, data =>
                    {
                        XtraMessageBox.Show(data.Message);
                    });
                    break;
                default:
                    break;
            }

        }

        private void form_to_data()
        {
            var list = CommonFunction.DataTableToList<HoaDonChiTiet>(m_dt_chi_tiet);
            m_hoa_don.chi_tiet = list;
            m_hoa_don.loai_thanh_toan = "TT";
            m_hoa_don.thoi_gian_tao = m_dat_thoi_gian_tao.DateTime;
            m_hoa_don.khach = m_list_khach_hang.Where(s => s.ten_khach_hang == m_sle_khach_hang.Text).First();
            m_hoa_don.tong_gia_tri_hoa_don = tong_gia_tri_hoa_don;
            m_hoa_don.giam_tru = (tong_giam_tru_khuyen_mai + giam_tru_khach_hang) / 1000;
            ThemHoaDon(m_hoa_don, this, data =>
            {
                if (data.Success)
                {
                    XtraMessageBox.Show("Đã thêm hóa đơn thành công");
                }
                else
                {
                    XtraMessageBox.Show(data.Message);
                }
            });
        }

        private bool data_ready_to_import()
        {
            return true;
        }

        private void set_init_form_load()
        {
            switch (m_e_mode)
            {
                case Mode.XemChiTiet:
                    m_lbl_tong_gia_tri.Text = "Tổng giá trị hàng hóa : " + m_hoa_don.tong_gia_tri_hoa_don;
                    m_lbl_giam_tru_khuyen_mai.Text = "Tổng giảm trừ khuyến mãi : " + m_hoa_don.tong_tien_giam_tru_km;
                    m_lbl_thanh_tien.Text = "Thành tiền : " + m_hoa_don.thanh_tien;
                    break;
                case Mode.Sua:
                    m_lbl_tong_gia_tri.Text = "Tổng giá trị hàng hóa : " + m_hoa_don.tong_gia_tri_hoa_don;
                    m_lbl_giam_tru_khuyen_mai.Text = "Tổng giảm trừ khuyến mãi : " + m_hoa_don.tong_tien_giam_tru_km;
                    m_lbl_thanh_tien.Text = "Thành tiền : " + m_hoa_don.thanh_tien;
                    break;
                case Mode.ThemMoi:
                    //
                    m_lbl_tong_gia_tri.Text = "Tổng giá trị hàng hóa : ";
                    m_lbl_giam_tru_khuyen_mai.Text = "Tổng giảm trừ khuyến mãi : ";
                    m_lbl_thanh_tien.Text = "Thành tiền : ";
                    m_lbl_giam_tru_khach_hang.Text = "Giảm trừ thành viên: ";
                    //
                    m_dat_thoi_gian_tao.EditValue = DateTime.Now;
                    m_dt_chi_tiet = new DataTable();
                    m_dt_chi_tiet = CommonFunction.create_table_form_struct(typeof(HoaDonChiTiet));
                    m_grc_chi_tiet.DataSource = m_dt_chi_tiet;

                    //
                    LayMaHoaDon(this, data =>
                    {
                        m_hoa_don.chi_tiet = new List<HoaDonChiTiet>();
                        //
                        m_lbl_ten_cua_hang.Text = "Cửa hàng thời trang " + SystemInfo.ten_cua_hang;
                        m_lbl_nhan_vien.Text = "Người tạo: " + SystemInfo.ten_tai_khoan;
                        m_hoa_don.ma_hoa_don = m_lbl_ma_hoa_don.Text = data.Data;
                        m_dat_thoi_gian_tao.EditValue = DateTime.Now;
                    });
                    break;
                default:
                    break;
            }
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
            if (m_list_hang_hoa == null | m_list_hang_hoa.Count == 0)
            {
                XtraMessageBox.Show("Ngày chưa bắt đầu kinh doanh!");
            }
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

        private void tinh_gia_ban()
        {
            if (m_sle_hang_hoa.EditValue == null)
            {
                return;
            }
            var ma = (string)m_sle_hang_hoa.EditValue;
            var hang = m_list_hang_hoa.Where(s => s.ma_hang_hoa == ma).First();
            gia_ban = hang.gia_hien_tai * (1 - Convert.ToDecimal(hang.km_dang_ap_ung.muc_khuyen_mai / 100));
            m_txt_gia_ban.Text = String.Format("{0:#,##0 VND}", gia_ban);
        }

        private void hien_thi_cac_thong_so()
        {
            if (tong_gia_tri_hoa_don != 0)
            {
                m_lbl_tong_gia_tri.Text = "Tổng giá trị hàng hóa : " + String.Format("{0:#,##0 VND}", tong_gia_tri_hoa_don);
            }
            else
            {
                m_lbl_tong_gia_tri.Text = "Tổng giá trị hàng hóa : ";
            }
            if (tong_giam_tru_khuyen_mai != 0)
            {
                m_lbl_giam_tru_khuyen_mai.Text = "Tổng giảm trừ khuyến mãi : " + String.Format("{0:#,##0 VND}", tong_giam_tru_khuyen_mai);
            }
            else
            {
                m_lbl_giam_tru_khuyen_mai.Text = "Tổng giảm trừ khuyến mãi : ";
            }
            if (thanh_tien != 0)
            {
                m_lbl_thanh_tien.Text = "Thành tiền : " + String.Format("{0:#,##0 VND}", thanh_tien);
            }
            else
            {
                m_lbl_thanh_tien.Text = "Thành tiền : ";
            }
        }

        private void tinh_lai_cac_thong_so()
        {
            tong_gia_tri_hoa_don = 0;
            tong_giam_tru_khuyen_mai = 0;
            thanh_tien = 0;
            foreach (DataRow item in m_dt_chi_tiet.Rows)
            {
                //tong_gia_tri
                var gia_hien_tai = m_list_hang_hoa.Where(s => s.ma_hang_hoa == item["ma_hang"].ToString()).First().gia_hien_tai;
                var sl = Convert.ToDecimal(item["so_luong"].ToString());
                tong_gia_tri_hoa_don += gia_hien_tai * sl;
                //tong khuyen mai
                var giam = m_list_hang_hoa.Where(s => s.ma_hang_hoa == item["ma_hang"].ToString()).First().gia_hien_tai * Convert.ToDecimal(item["muc_khuyen_mai"]);
                tong_giam_tru_khuyen_mai += giam * sl;
                //thanh tien
            }
            thanh_tien = (tong_gia_tri_hoa_don - tong_giam_tru_khuyen_mai - giam_tru_khach_hang);
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
            m_grc_chi_tiet.DataSourceChanged += M_grc_chi_tiet_DataSourceChanged;
            m_grv_chi_tiet.RowDeleted += M_grv_chi_tiet_RowDeleted;
            m_grv_chi_tiet.CellValueChanged += M_grv_chi_tiet_CellValueChanged;
            m_txt_gia_ban.Leave += M_txt_gia_ban_Leave;
        }

        private void M_txt_gia_ban_Leave(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(m_txt_gia_ban.Text)|String.IsNullOrWhiteSpace(m_txt_gia_ban.Text))
                {
                    return;
                }
                m_txt_gia_ban.Text = String.Format("{0:#,##0 VND}", m_txt_gia_ban.Text);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.InnerException.Message);
            }
        }

        private void M_grv_chi_tiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                tinh_lai_cac_thong_so();
                hien_thi_cac_thong_so();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.InnerException.Message);
                XtraMessageBox.Show(CommonMessage.MESSAGE_EXCEPTION);
            }
        }

        private void M_grv_chi_tiet_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            try
            {
                tinh_lai_cac_thong_so();
                hien_thi_cac_thong_so();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.InnerException.Message);
                XtraMessageBox.Show(CommonMessage.MESSAGE_EXCEPTION);
            }
        }

        private void M_grc_chi_tiet_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                tinh_lai_cac_thong_so();
                hien_thi_cac_thong_so();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Data.ToString());
                XtraMessageBox.Show("Đã xảy lỗi");
            }
        }

        private void M_dat_thoi_gian_tao_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_dat_thoi_gian_tao.EditValue == null)
                {
                    return;
                }
                LayDanhSachHangHoa(SystemInfo.id_cua_hang, m_dat_thoi_gian_tao.DateTime, this, data =>
                {
                    m_list_hang_hoa = data.Data;
                    data_to_sle_hang_hoa();
                });
                LayDanhSachKhachHang(DateTime.Now, this, data =>
                {
                    m_list_khach_hang = data.Data;
                    data_to_sle_khach_hang();
                    switch (m_e_mode)
                    {
                        case Mode.XemChiTiet:
                            m_sle_khach_hang.EditValue = m_hoa_don.khach.ten_khach_hang;
                            break;
                        case Mode.Sua:
                            m_sle_khach_hang.EditValue = m_hoa_don.khach.ten_khach_hang;
                            break;
                        case Mode.ThemMoi:
                            m_sle_khach_hang.EditValue = m_list_khach_hang.Where(s => s.tai_khoan == "customer").First().ten_khach_hang;
                            break;
                        default:
                            break;
                    }
                });
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
                string ma = (string)m_sle_hang_hoa.EditValue;
                var hang = m_list_hang_hoa.Where(s => s.ma_hang_hoa == ma).First();
                list_thuoc_tinh.Add(m_sle_hang_hoa.EditValue);
                list_thuoc_tinh.Add(m_sle_size.EditValue);
                list_thuoc_tinh.Add(m_sle_so_luong.Text);
                list_thuoc_tinh.Add(hang.km_dang_ap_ung.mo_ta);
                list_thuoc_tinh.Add(hang.km_dang_ap_ung.muc_khuyen_mai);
                list_thuoc_tinh.Add(m_txt_gia_ban.Text);

                m_dt_chi_tiet.Rows.Add(list_thuoc_tinh.ToArray());
                m_grc_chi_tiet.DataSource = m_dt_chi_tiet;

                //
                tinh_lai_cac_thong_so();
                hien_thi_cac_thong_so();
                //

                m_sle_hang_hoa.EditValue = null;
                m_sle_size.EditValue = null;
                m_sle_so_luong.EditValue = null;
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
                XtraMessageBox.Show("Đã xảy ra 1 số lỗi");
            }
        }

        private void M_sle_size_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_hang_hoa.EditValue == null)
                {
                    return;
                }
                if (m_sle_size.EditValue == null)
                {
                    return;
                }
                string ma = (string)m_sle_hang_hoa.EditValue;
                var hang = m_list_hang_hoa.Where(s => s.ma_hang_hoa == ma).First();
                //
                string size = m_sle_size.EditValue.ToString();
                var ssl = hang.san_co.Where(s => s.ten_size == size).First();
                data_to_sle_so_luong(ssl);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Đã xảy ra 1 số lỗi");
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
                var hang = m_list_hang_hoa.Where(s => s.ma_hang_hoa == ma).First();
                data_to_sle_size(hang);
                tinh_gia_ban();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.InnerException.Message);
            }
        }

        private void M_sle_khach_hang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_khach_hang.EditValue == null | String.IsNullOrEmpty(m_sle_khach_hang.Text) | String.IsNullOrWhiteSpace(m_sle_khach_hang.Text))
                {
                    m_sle_khach_hang.EditValue = m_list_khach_hang.Where(s => s.tai_khoan == "customer").First().ten_khach_hang;
                }
                var tai_khoan = (string)m_sle_khach_hang.EditValue;
                var khach = m_list_khach_hang.Where(s => s.tai_khoan == tai_khoan).First();
                m_lbl_giam_tru_khach_hang.Text = "Giảm trừ thành viên: " + String.Format("{0:#,##0 VND}", khach.diem_giam_tru);
                if (m_dt_chi_tiet.Rows.Count != 0)
                {
                    tinh_lai_cac_thong_so();
                    hien_thi_cac_thong_so();
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
                //if (!data_ready_to_import())
                //{
                //    XtraMessageBox.Show("Vui lòng kiểm tra lại dữ liệu");
                //}
                if (CommonFunction.MsgBox_Yes_No_Cancel("Bạn có chắc chắn muốn lưu?", "Xác nhận lưu?") == DialogResult.Yes)
                {
                    save_data();
                }
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
