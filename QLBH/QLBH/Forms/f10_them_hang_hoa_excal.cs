using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqToExcel;
using QLBH.Common;
using static LibraryApi.QuanLyDanhMucHangHoa;
using DevExpress.XtraEditors;

namespace QLBH.Forms
{
    public partial class f10_them_hang_hoa_excal : Form
    {
        #region Public Interfaces
        public f10_them_hang_hoa_excal()
        {
            InitializeComponent();
            set_define_event();
        }
        #endregion

        #region Data Stucture
        #endregion

        #region Member
        private string m_file_name;
        public List<ThemHangHoaExcel> data_from_excel { get; private set; }
        private List<HangHoaVaMa> m_list_hang_hoa { get; set; }
        #endregion

        #region Private Methods

        private List<ThemHangHoaExcel> to_list()
        {
            List<ThemHangHoaExcel> result = new List<ThemHangHoaExcel>();
            var data = m_grc_phieu_nhap.DataSource as DataTable;
            foreach (var row in data.Rows)
            {
                DataRow dt_row = row as DataRow;
                ThemHangHoaExcel phieu = new ThemHangHoaExcel();
                phieu.Ten = dt_row["Ten"].ToString();
                phieu.MaTraCuu = dt_row["MaTraCuu"].ToString();
                phieu.MaNhaCungCap = dt_row["MaNhaCungCap"].ToString();
                phieu.MoTa = dt_row["MoTa"].ToString();
                phieu.Link = dt_row["Link"].ToString();
                phieu.Tag = dt_row["Tag"].ToString();
                result.Add(phieu);
            }
            return result;
        }

        private void load_data_to_sle_ma()
        {
            try
            {
                LayDanhSachNhaCungCap(this, data =>
                {
                    m_sle_nha_cung_cap.DataSource = CommonFunction.list_to_data_table<NhaCungCapV2>(data.Data);
                    m_sle_nha_cung_cap.ValueMember = "ma_nha_cung_cap";
                    m_sle_nha_cung_cap.DisplayMember = "ma_nha_cung_cap";
                });
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Kiểm tra kết nối Internet");
            }
        }

        private void load_data_to_grid()
        {
            data_from_excel.RemoveAll(s => s.Ten == null);
            m_grc_phieu_nhap.DataSource = CommonFunction.list_to_data_table<ThemHangHoaExcel>(data_from_excel);
            m_grv_phieu_nhap.BestFitColumns();
        }

        private void open_file()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Office Files|*.xlsx;*.xls;";
            opf.Multiselect = false;

            if (opf.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (String.IsNullOrEmpty(opf.FileName))
            {
                return;
            }
            m_file_name = opf.FileName;
            var excel = new ExcelQueryFactory(m_file_name);
            data_from_excel = (from a in excel.Worksheet<ThemHangHoaExcel>("HANG_HOA")
                               select a).ToList();
            //bat buoc phai co ten. thieu phat end luon
            load_data_to_grid();
        }

        private bool check_data()
        {
            foreach (var item in data_from_excel)
            {
                if (check_trung_ma_tra_cuu(item.MaTraCuu))
                {
                    XtraMessageBox.Show("Vui lòng kiểm tra lại thông tin mã tra cứu hàng hóa "
                                        + item.Ten
                                        + " trong file excel");
                    return false;
                }
                if (String.IsNullOrEmpty(item.MaNhaCungCap) | String.IsNullOrWhiteSpace(item.MaNhaCungCap))
                {
                    XtraMessageBox.Show("Vui lòng kiểm tra lại thông tin mã nhà cung cấp hàng hóa "
                                         + item.Ten
                                         + " trong file excel");
                    return false;
                }
            }
            return true;
        }

        private bool check_trung_ma_tra_cuu(string ma)
        {
            foreach (var item in m_list_hang_hoa)
            {
                if (item.ma_tra_cuu == ma)
                {
                    return true;
                }
            }
            return false;
        }

        private void get_data_ma_hang_hoa()
        {
            LibraryApi.QuanLyDanhMucHangHoa.LayDanhSachHangVaMaTraCuu(this, data =>
            {
                m_list_hang_hoa = data.Data;
            });
        }

        #endregion

        #region Event Handlers
        private void set_define_event()
        {
            this.Load += F03_them_phieu_nhap_excel_Load;
            m_btn_chon_file.Click += M_btn_chon_file_Click;
            m_btn_save.Click += M_btn_save_Click;
            m_btn_thoat.Click += M_btn_thoat_Click;
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

        private void F03_them_phieu_nhap_excel_Load(object sender, EventArgs e)
        {
            try
            {
                this.CenterToScreen();
                this.WindowState = FormWindowState.Maximized;
                load_data_to_sle_ma();
                get_data_ma_hang_hoa();
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
                data_from_excel = to_list();
                if (check_data())
                {
                    if (CommonFunction.MsgBox_Yes_No_Cancel("Bạn có chắc chắn muốn lưu?", "Xác nhận lưu") == DialogResult.Yes)
                    {
                        var listHH = new List<HangHoaExcel>();

                        foreach (var item in data_from_excel)
                        {
                            if (string.IsNullOrEmpty(item.Ten))
                            {
                                continue;
                            }
                            var hh = new HangHoaExcel();
                            hh.ten_hang_hoa = item.Ten;
                            hh.ma_nha_cung_cap = item.MaNhaCungCap;
                            hh.ma_tra_cuu = item.MaTraCuu;
                            var listLink = CommonFunction.TachID(item.Link);
                            hh.link_anh = new List<string>();
                            foreach (var link in listLink)
                            {
                                hh.link_anh.Add(link);
                            }
                            var listTag = CommonFunction.TachID(item.Tag);
                            hh.tag = new List<string>();
                            foreach (var t in listTag)
                            {
                                hh.tag.Add(t);
                            }
                            listHH.Add(hh);

                        }
                        ThemHangHoaExcel(listHH, this, data =>
                        {
                            XtraMessageBox.Show(data.Message);
                        });
                    }
                }
                else
                {
                    XtraMessageBox.Show("Vui lòng kiểm tra lại dữ liệu");
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Vui lòng kiểm tra lại dữ liệu");
            }
        }

        private void M_btn_chon_file_Click(object sender, EventArgs e)
        {
            try
            {
                open_file();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
