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
using LinqToExcel;
using DevExpress.XtraEditors;
using QLBH.Common;
using QLBH.Controls.Hóa_đơn;

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
                var dt = from a in excel.Worksheet<ThemHangHoaExcel>("HANG_HOA") select a;
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
                //QuanLyDanhMucHangHoa.ThemHangHoaExcel(listHH, this, data =>
                //{
                //    MessageBox.Show(data.Message);
                //});
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
                //#region Cửa số chọn file

                //OpenFileDialog opf = new OpenFileDialog();
                //opf.Filter = "Office Files|*.xlsx;*.xls;";
                //opf.Multiselect = false;

                //#endregion

                //#region Lọc và kiểm tra dữ liệu

                //if (opf.ShowDialog() == DialogResult.OK)
                //{
                //    if (!String.IsNullOrEmpty(opf.FileName))
                //    {
                //        var excel = new ExcelQueryFactory(opf.FileName);
                //        var data_from_excel = (from a in excel.Worksheet<PhieuNhapExcel>("PHIEU_NHAP")
                //                               select a).Where(s => !String.IsNullOrEmpty(s.ngay_nhap)).ToList();
                //        //bat buoc phai co ma tra cuu va gia nhap. thieu phat end luon
                //        foreach (var item in data_from_excel)
                //        {
                //            if (String.IsNullOrEmpty(item.ma_tra_cuu))
                //            {
                //                XtraMessageBox.Show("Vui lòng kiểm tra lại thông tin mã tra cứu hàng hóa ngày "
                //                                    + Convert.ToDateTime(item.ngay_nhap).ToShortDateString()
                //                                    + " trong file excel");
                //                return;
                //            }
                //            if (String.IsNullOrEmpty(item.gia_nhap) | Convert.ToInt16(item.gia_nhap) < 0)
                //            {
                //                XtraMessageBox.Show("Vui lòng kiểm tra lại thông tin giá nhập mặt hàng "
                //                                     + item.ma_tra_cuu.ToString()
                //                                     + " ngày " + Convert.ToDateTime(item.ngay_nhap).ToShortDateString()
                //                                     + " trong file excel");
                //                return;
                //            }
                //        }

                //        #endregion

                //        #region Tạo danh sách phiếu nhập

                //        var list_phieu_nhap = new List<QuanLyPhieuNhapXuat.PhieuNhap>();

                //        #region 1 ngày tương ứng 1 phiếu

                //        var list_phieu_theo_ngay = data_from_excel.Select(s => Convert.ToDateTime(s.ngay_nhap)).Distinct();

                //        #endregion                       

                //        foreach (var item in list_phieu_theo_ngay)
                //        {
                //            QuanLyPhieuNhapXuat.PhieuNhap phieu = new QuanLyPhieuNhapXuat.PhieuNhap();
                //            phieu.ngay_nhap = Convert.ToDateTime(item);
                //            phieu.ten_tai_khoan = SystemInfo.ten_tai_khoan;
                //            phieu.id_cua_hang = SystemInfo.id_cua_hang;
                //            phieu.list_hang_hoa = new List<QuanLyPhieuNhapXuat.HangHoa>();

                //            #region nhập thông tin cho từng phiếu
                //            // lay het hang nhap trong ngay
                //            var hang_nhap_trong_ngay = data_from_excel.Where(s => Convert.ToDateTime(s.ngay_nhap) == item).ToList();
                //            foreach (var hang in hang_nhap_trong_ngay)
                //            {
                //                var hang_hoa = new QuanLyPhieuNhapXuat.HangHoa();
                //                hang_hoa.gia_nhap = decimal.Parse(hang.gia_nhap);
                //                hang_hoa.ma_tra_cuu_hang_hoa = hang.ma_tra_cuu;
                //                hang_hoa.size_sl = new List<QuanLyPhieuNhapXuat.SizeSL>();
                //                foreach (var item2 in new List<string> { "S", "M", "L", "XL", "XXL" })
                //                {
                //                    var sl = int.Parse(hang.GetType().GetProperty(item2).GetValue(hang).ToString());
                //                    if (sl == 0)
                //                    {
                //                        continue;
                //                    }
                //                    QuanLyPhieuNhapXuat.SizeSL s = new QuanLyPhieuNhapXuat.SizeSL();
                //                    s.ten_size = item2;
                //                    s.so_luong = sl;
                //                    hang_hoa.size_sl.Add(s);
                //                }
                //                phieu.list_hang_hoa.Add(hang_hoa);
                //            }
                //            #endregion 

                //            list_phieu_nhap.Add(phieu);
                //        }

                //        #endregion

                //        #region Chạy request để nhập    

                //        QuanLyPhieuNhapXuat.ThemPhieuNhapTuExcel(list_phieu_nhap, this, data =>
                //        {
                //            MessageBox.Show(data.Message);
                //        });
                //    }
                //}

                //#endregion
                f03_them_phieu_nhap_excel v_f = new f03_them_phieu_nhap_excel();
                v_f.ShowDialog();
                
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////table.Style = c0_table.ScrollStyle.Horizontal;
            //table.BackColor = Color.White;
            //var list = new List<string>();
            //for (int i = 0; i < 6; i++)
            //{
            //    list.Add(i.ToString());
            //}
            //table.Length = () => { return list.Count; };
            //table.NumberOfCellPerLine = () => { return 2; };
            //table.LengthForCellAtIndex = index =>
            //{
            //    return 100;
            //};
            //table.CellAtIndex = index => 
            //{
            //    var cell = new Button();
            //    cell.Dock = DockStyle.Fill;
            //    cell.Text = list[index];
            //    return cell;
            //};
            //var btn = new Button();
            //btn.Text = "special";
            //btn.Dock = DockStyle.Fill;
        
        }
    }
}
