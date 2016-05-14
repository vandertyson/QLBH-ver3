using DevExpress.XtraEditors;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LibraryApi.QuanLyPhieuNhapXuat;
using QLBH.Common;


namespace QLBH.Forms
{
    public partial class f03_them_phieu_nhap_excel : Form
    {
        #region Public Interfaces
        public f03_them_phieu_nhap_excel()
        {
            InitializeComponent();
            set_define_event();
        }
        #endregion

        #region Data Stucture
        #endregion

        #region Member
        private string m_file_name;
        public List<NhieuPhieuNhapExcel> data_from_excel { get; private set; }
        #endregion

        #region Private Methods
       
        private List<NhieuPhieuNhapExcel> to_list()
        {
            List<NhieuPhieuNhapExcel> result = new List<NhieuPhieuNhapExcel>();
            var data = m_grc_phieu_nhap.DataSource as DataTable;
            foreach (var row in data.Rows)
            {
                DataRow dt_row = row as DataRow;
                NhieuPhieuNhapExcel phieu = new NhieuPhieuNhapExcel();
                phieu.ngay_nhap = dt_row["ngay_nhap"].ToString();
                phieu.ma_tra_cuu = dt_row["ma_tra_cuu"].ToString();
                phieu.S = dt_row["S"].ToString();
                phieu.M = dt_row["M"].ToString();
                phieu.L = dt_row["L"].ToString();
                phieu.XL = dt_row["XL"].ToString();
                phieu.XXL = dt_row["XXL"].ToString();
                phieu.gia_nhap = dt_row["gia_nhap"].ToString();
                result.Add(phieu);
            }
            return result;
        }

        private void load_data_to_sle_ma()
        {
            try
            {
                LibraryApi.QuanLyHoaDon.LayDanhSachHangHoa(SystemInfo.id_cua_hang, DateTime.Now, this, data =>
                {
                    List<string> prop_name = new List<string> { "ma_hang_hoa", "ten_hang_hoa" };
                    m_sle_ma_tra_cuu.DataSource = CommonFunction.convert_list_to_data_table<LibraryApi.QuanLyHoaDon.HangHoa>(prop_name, data.Data);
                    m_sle_ma_tra_cuu.ValueMember = "ma_hang_hoa";
                    m_sle_ma_tra_cuu.DisplayMember = "ma_hang_hoa";
                });
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Kiểm tra kết nối Internet");
            }
        }


        private void load_data_to_grid()
        {
            List<string> prop_name = new List<string> { "ngay_nhap", "ma_tra_cuu", "S", "M", "L", "XL", "XXL","gia_nhap" };
            m_grc_phieu_nhap.DataSource = CommonFunction.convert_list_to_data_table<NhieuPhieuNhapExcel>(prop_name, data_from_excel);
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
            data_from_excel = (from a in excel.Worksheet<NhieuPhieuNhapExcel>("PHIEU_NHAP")
                               select a).Where(s => !String.IsNullOrEmpty(s.ngay_nhap)).ToList();
            //bat buoc phai co ma tra cuu va gia nhap. thieu phat end luon
            load_data_to_grid();
        }

        private bool check_data()
        {
            foreach (var item in data_from_excel)
            {
                if (String.IsNullOrEmpty(item.ma_tra_cuu))
                {
                    XtraMessageBox.Show("Vui lòng kiểm tra lại thông tin mã tra cứu hàng hóa ngày "
                                        + Convert.ToDateTime(item.ngay_nhap).ToShortDateString()
                                        + " trong file excel");
                    return false;
                }
                if (String.IsNullOrEmpty(item.gia_nhap) | Convert.ToInt16(item.gia_nhap) < 0)
                {
                    XtraMessageBox.Show("Vui lòng kiểm tra lại thông tin giá nhập mặt hàng "
                                         + item.ma_tra_cuu.ToString()
                                         + " ngày " + Convert.ToDateTime(item.ngay_nhap).ToShortDateString()
                                         + " trong file excel");
                    return false;
                }
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
                    if(CommonFunction.MsgBox_Yes_No_Cancel("Bạn có chắc chắn muốn lưu?","Xác nhận lưu") == DialogResult.Yes)
                    {
                        #region Tạo danh sách phiếu nhập

                        var list_phieu_nhap = new List<PhieuNhap>();

                        #region 1 ngày tương ứng 1 phiếu

                        var list_phieu_theo_ngay = data_from_excel.Select(s => Convert.ToDateTime(s.ngay_nhap)).Distinct();

                        #endregion

                        foreach (var item in list_phieu_theo_ngay)
                        {
                            PhieuNhap phieu = new PhieuNhap();
                            phieu.ngay_nhap = Convert.ToDateTime(item).ToString();
                            phieu.ten_tai_khoan = SystemInfo.ten_tai_khoan;
                            phieu.id_cua_hang = SystemInfo.id_cua_hang;
                            phieu.list_hang_hoa = new List<HangHoa>();

                            #region nhập thông tin cho từng phiếu
                            // lay het hang nhap trong ngay
                            var hang_nhap_trong_ngay = data_from_excel.Where(s => Convert.ToDateTime(s.ngay_nhap) == item).ToList();
                            foreach (var hang in hang_nhap_trong_ngay)
                            {
                                var hang_hoa = new HangHoa();
                                hang_hoa.gia_nhap = decimal.Parse(hang.gia_nhap);
                                hang_hoa.ma_tra_cuu_hang_hoa = hang.ma_tra_cuu;
                                hang_hoa.size_sl = new List<SizeSL>();
                                foreach (var item2 in new List<string> { "S", "M", "L", "XL", "XXL" })
                                {
                                    var sl = int.Parse(hang.GetType().GetProperty(item2).GetValue(hang).ToString());
                                    if (sl == 0)
                                    {
                                        continue;
                                    }
                                    SizeSL s = new SizeSL();
                                    s.ten_size = item2;
                                    s.so_luong = sl;
                                    hang_hoa.size_sl.Add(s);
                                }
                                phieu.list_hang_hoa.Add(hang_hoa);
                            }
                            #endregion

                            list_phieu_nhap.Add(phieu);
                        }

                        #endregion

                        #region Chạy request để nhập    

                        ThemPhieuNhapTuExcel(list_phieu_nhap, this, data =>
                        {
                            XtraMessageBox.Show(data.Message);
                        });
                    }
                    #endregion
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

