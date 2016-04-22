using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryApi;
using LibraryApi.QuanLyBanHang;
using LinqToExcel;
using DevExpress.XtraEditors;


namespace QLBH.Forms
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string file = @"C:\Users\Son Pham\Desktop\QLBH ver3\DataExcel.xlsx";
                string path = @"http://quanlybanhang.somee.com/docx/";
                var excel = new ExcelQueryFactory(file);
                var dt = from a in excel.Worksheet<ThemHangHoa>("HANG_HOA") select a;
                var listHH = new List<ThemHangHoaPost>();

                foreach (var item in dt)
                {
                    if (string.IsNullOrEmpty(item.Ten))
                    {
                        continue;
                    }
                    var hh = new ThemHangHoaPost();
                    hh.ten_hang_hoa = item.Ten;
                    hh.ma_nha_cung_cap = item.MaNhaCungCap;
                    hh.ma_tra_cuu = item.MaTraCuu;
                    var listLink = Common.TachID(item.Link);
                    hh.link_anh = new List<string>();
                    foreach (var link in listLink)
                    {
                        hh.link_anh.Add(link);
                    }
                    var listTag = Common.TachID(item.Tag);
                    hh.tag = new List<string>();
                    foreach (var t in listTag)
                    {
                        hh.tag.Add(t);
                    }
                    listHH.Add(hh);

                }
                MyNetwork.ThemHangHoa(listHH, this, data =>
                {
                    MessageBox.Show(data.Message);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void m_btn_phieu_nhap_Click(object sender, EventArgs e)
        {
            try
            {
                #region Cửa số chọn file

                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "Office Files|*.xlsx;*.xls;";
                opf.Multiselect = false;

                #endregion

                #region Lọc và kiểm tra dữ liệu

                if (opf.ShowDialog() == DialogResult.OK)
                {
                    if (!String.IsNullOrEmpty(opf.FileName))
                    {
                        var excel = new ExcelQueryFactory(opf.FileName);
                        var data_from_excel = (from a in excel.Worksheet<PhieuNhapExcel>("PHIEU_NHAP")
                                               select a).Where(s => !String.IsNullOrEmpty(s.ngay_nhap)).ToList();
                        //bat buoc phai co ma tra cuu va gia nhap. thieu phat end luon
                        foreach (var item in data_from_excel)
                        {
                            if (String.IsNullOrEmpty(item.ma_tra_cuu))
                            {
                                XtraMessageBox.Show("Vui lòng kiểm tra lại thông tin mã tra cứu hàng hóa ngày " 
                                                    + Convert.ToDateTime(item.ngay_nhap).ToShortDateString() 
                                                    + " trong file excel");
                                return;
                            }
                            if (String.IsNullOrEmpty(item.gia_nhap) | Convert.ToInt16(item.gia_nhap) < 0 )
                            {
                                XtraMessageBox.Show("Vui lòng kiểm tra lại thông tin giá nhập mặt hàng "
                                                     + item.ma_tra_cuu.ToString() 
                                                     + " ngày " + Convert.ToDateTime(item.ngay_nhap).ToShortDateString()
                                                     + " trong file excel");
                                return;
                            }
                        }

                        #endregion

                        #region Tạo danh sách phiếu nhập

                        var list_phieu_nhap = new List<PhieuNhap>();  
                                             
                        #region 1 ngày tương ứng 1 phiếu

                        var list_phieu_theo_ngay = data_from_excel.Select(s => Convert.ToDateTime(s.ngay_nhap)).Distinct();

                        #endregion                       

                        foreach (var item in list_phieu_theo_ngay)
                        {
                            PhieuNhap phieu = new PhieuNhap();
                            phieu.ngay_nhap = Convert.ToDateTime(item);
                            phieu.ten_tai_khoan = SystemInfo.ten_tai_khoan;
                            phieu.id_cua_hang = SystemInfo.id_cua_hang;
                            phieu.list_hang_hoa = new List<LibraryApi.QuanLyBanHang.HangHoa>();

                            #region nhập thông tin cho từng phiếu
                            // lay het hang nhap trong ngay
                            var hang_nhap_trong_ngay = data_from_excel.Where(s => Convert.ToDateTime(s.ngay_nhap) == item).ToList();
                            foreach (var hang in hang_nhap_trong_ngay)
                            {
                                #region mỗi size là 1 phiếu chi tiết
                                if (!String.IsNullOrEmpty(hang.so_luong_size_l) & Convert.ToInt16(hang.so_luong_size_l) != 0)
                                {
                                    LibraryApi.QuanLyBanHang.HangHoa hang_theo_size = new LibraryApi.QuanLyBanHang.HangHoa();
                                    hang_theo_size.ma_tra_cuu_hang_hoa = hang.ma_tra_cuu;
                                    hang_theo_size.gia_nhap = Convert.ToDecimal(hang.gia_nhap);
                                    hang_theo_size.so_luong = Convert.ToInt16(hang.so_luong_size_l);
                                    hang_theo_size.ten_size = "L";
                                    phieu.list_hang_hoa.Add(hang_theo_size);
                                }
                                if (!String.IsNullOrEmpty(hang.so_luong_size_s) & Convert.ToInt16(hang.so_luong_size_s) != 0)
                                {
                                    LibraryApi.QuanLyBanHang.HangHoa hang_theo_size = new LibraryApi.QuanLyBanHang.HangHoa();
                                    hang_theo_size.ma_tra_cuu_hang_hoa = hang.ma_tra_cuu;
                                    hang_theo_size.gia_nhap = Convert.ToDecimal(hang.gia_nhap);
                                    hang_theo_size.so_luong = Convert.ToInt16(hang.so_luong_size_s);
                                    hang_theo_size.ten_size = "S";
                                    phieu.list_hang_hoa.Add(hang_theo_size);
                                }
                                if (!String.IsNullOrEmpty(hang.so_luong_size_m) & Convert.ToInt16(hang.so_luong_size_m) != 0)
                                {
                                    LibraryApi.QuanLyBanHang.HangHoa hang_theo_size = new LibraryApi.QuanLyBanHang.HangHoa();
                                    hang_theo_size.ma_tra_cuu_hang_hoa = hang.ma_tra_cuu;
                                    hang_theo_size.gia_nhap = Convert.ToDecimal(hang.gia_nhap);
                                    hang_theo_size.so_luong = Convert.ToInt16(hang.so_luong_size_m);
                                    hang_theo_size.ten_size = "M";
                                    phieu.list_hang_hoa.Add(hang_theo_size);
                                }
                                if (!String.IsNullOrEmpty(hang.so_luong_size_xl) & Convert.ToInt16(hang.so_luong_size_xl) != 0)
                                {
                                    LibraryApi.QuanLyBanHang.HangHoa hang_theo_size = new LibraryApi.QuanLyBanHang.HangHoa();
                                    hang_theo_size.ma_tra_cuu_hang_hoa = hang.ma_tra_cuu;
                                    hang_theo_size.gia_nhap = Convert.ToDecimal(hang.gia_nhap);
                                    hang_theo_size.so_luong = Convert.ToInt16(hang.so_luong_size_xl);
                                    hang_theo_size.ten_size = "XL";
                                    phieu.list_hang_hoa.Add(hang_theo_size);
                                }
                                if (!String.IsNullOrEmpty(hang.so_luong_size_xxl) & Convert.ToInt16(hang.so_luong_size_xxl) != 0)
                                {
                                    LibraryApi.QuanLyBanHang.HangHoa hang_theo_size = new LibraryApi.QuanLyBanHang.HangHoa();
                                    hang_theo_size.ma_tra_cuu_hang_hoa = hang.ma_tra_cuu;
                                    hang_theo_size.gia_nhap = Convert.ToDecimal(hang.gia_nhap);
                                    hang_theo_size.so_luong = Convert.ToInt16(hang.so_luong_size_xxl);
                                    hang_theo_size.ten_size = "XXL";
                                    phieu.list_hang_hoa.Add(hang_theo_size);
                                }
                                #endregion
                            }
                            #endregion 

                            list_phieu_nhap.Add(phieu);
                        }

                        #endregion

                        #region Chạy request để nhập    

                        MyNetwork.ThemPhieuNhapTuExcel(list_phieu_nhap, this, data =>
                        {
                            MessageBox.Show(data.Message);
                        });                      
                    }                   
                }

                #endregion
            }
            catch (Exception ex)
            {
                Common.exception_handle(ex);
            }
        }
    }
}
