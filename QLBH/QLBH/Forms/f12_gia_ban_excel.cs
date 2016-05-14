using DevExpress.XtraEditors;
using LinqToExcel;
using QLBH.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LibraryApi.QuanLyGia;
namespace QLBH.Forms
{
    public partial class f12_gia_ban_excel : Form
    {
        #region Public Interfaces
        public f12_gia_ban_excel()
        {
            InitializeComponent();
            set_define_event();
        }
        #endregion

        #region Data Stucture
        #endregion

        #region Member
        private string m_file_name;
        public List<ThemGiaExcel> data_from_excel { get; private set; } = new List<ThemGiaExcel>();
        #endregion

        #region Private Methods

        private List<ThemGiaExcel> to_list()
        {
            List<ThemGiaExcel> result = new List<ThemGiaExcel>();
            var data = m_grc_gia_ban.DataSource as DataTable;
            foreach (var row in data.Rows)
            {
                DataRow dt_row = row as DataRow;
                ThemGiaExcel phieu = new ThemGiaExcel();
                phieu.ma_tra_cuu = dt_row["ma_tra_cuu"].ToString();
                phieu.gia_ban = dt_row["gia_ban"].ToString();
                phieu.ngay_ap_dung = dt_row["ngay_ap_dung"].ToString();

                result.Add(phieu);
            }
            return result;
        }

        private void load_data_to_sle_ma()
        {
            try
            {
                LibraryApi.QuanLyDanhMucHangHoa.LayDanhSachHangVaMaTraCuu(this, data =>
                {
                    m_sle_hang_hoa.DataSource = CommonFunction.list_to_data_table<LibraryApi.QuanLyDanhMucHangHoa.HangHoaVaMa>(data.Data);
                    m_sle_hang_hoa.ValueMember = "ma_tra_cuu";
                    m_sle_hang_hoa.DisplayMember = "ma_tra_cuu";
                });
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Kiểm tra kết nối Internet");
            }
        }

        private void load_data_to_grid()
        {
            data_from_excel.RemoveAll(s => s.ma_tra_cuu == null);
            m_grc_gia_ban.DataSource = CommonFunction.list_to_data_table<ThemGiaExcel>(data_from_excel);
            m_grv_gia_ban.BestFitColumns();
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
            data_from_excel = (from a in excel.Worksheet<ThemGiaExcel>("GIA_BAN")
                               select a).ToList();
            //bat buoc phai co ma tra cuu va gia nhap. thieu phat end luon
            load_data_to_grid();
        }

        private bool check_data()
        {
            foreach (var item in data_from_excel)
            {

            }
            return true;
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

                        var list_gia = new List<GiaExcel>();
                        foreach (var item in data_from_excel)
                        {
                            GiaExcel g = new GiaExcel();
                            g.gia_ban = Convert.ToDecimal(item.gia_ban);
                            g.ma_tra_cuu = item.ma_tra_cuu;
                            g.ngay_ap_dung = Convert.ToDateTime(item.ngay_ap_dung);
                            list_gia.Add(g);
                        }
                        ThemGiaTuExcel(list_gia, this, data =>
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(CommonMessage.MESSAGE_EXCEPTION);
                XtraMessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion
    }

}

