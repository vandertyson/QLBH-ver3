using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApi
{
    public class QuanLyPhieuNhapXuat
    {
        #region CONST SERVICE URL - Chứa địa chỉ service

        //public const string URL_SERVICE = @"http://quanlybanhang.somee.com//QLBanHang.asmx/";
        public const string URL_SERVICE = @"http://localhost:32608/QLBanHang.asmx/";
        public const string URL_THEM_PHIEU_NHAP_EXCEL = URL_SERVICE + @"ThemPhieuNhapExcel";
        public static string URL_XOA_PHIEU_NHAP = URL_SERVICE + @"XoaPhieuNhap";
        public static string URL_SUA_PHIEU_NHAP = URL_SERVICE + @"SuaPhieuNhap";
        public static string URL_THEM_MOT_PHIEU_NHAP = URL_SERVICE + @"ThemMotPhieuNhap";
        private static string URL_LAY_MA_PHIEU_NHAP_MOI = URL_SERVICE + @"LayMaPhieuMoi";
        private static string URL_LAY_DANH_SACH_PHIEU_NHAP = URL_SERVICE + @"LayDanhSachPhieuNhap";

        #endregion

        #region STRUCT- Chứa các cấu trúc dữ liệu ( public class )

        public class SizeSL
        {
            public string ten_size { get; set; }
            public int so_luong { get; set; }
        }

     

        public class HangHoa
        {
            public string ma_tra_cuu_hang_hoa { get; set; }
            public List<SizeSL> size_sl { get; set; }
            public decimal gia_nhap { get; set; }
        }

        public class PhieuNhap
        {
            public string ma_phieu { get; set; }
            public DateTime ngay_nhap { get; set; }
            public string ten_tai_khoan { get; set; }
            public decimal id_cua_hang { get; set; }
            public List<HangHoa> list_hang_hoa { get; set; }
        }
        #endregion

        #region FUNCTION - Chứa các hàm lấy dữ liệu bằng request ( public static )

        public static void ThemPhieuNhapTuExcel(List<PhieuNhap> list_phieu_nhap, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["list_phieu_nhap"] = JsonConvert.SerializeObject(list_phieu_nhap);
            MyNetwork.requestDataWithParam(param, URL_THEM_PHIEU_NHAP_EXCEL, f, MyDelegate);
        }

        public static void XoaPhieuNhap(string ma_phieu,Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ma_phieu"] = ma_phieu;
            MyNetwork.requestDataWithParam(param, URL_XOA_PHIEU_NHAP, f, MyDelegate);
        }

        public static void SuaPhieuNhap(PhieuNhap phieu, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["phieu"] = JsonConvert.SerializeObject(phieu);
            MyNetwork.requestDataWithParam(param, URL_SUA_PHIEU_NHAP, f, MyDelegate);
        }

        public static void ThemMotPhieuNhap(PhieuNhap phieu, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["phieu"] = JsonConvert.SerializeObject(phieu);
            MyNetwork.requestDataWithParam(param, URL_THEM_MOT_PHIEU_NHAP, f, MyDelegate);
        }

        public static void LayMaPhieuNhap(Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_LAY_MA_PHIEU_NHAP_MOI, f, MyDelegate);
        }

        public static void LayDanhSachPhieuNhap(DateTime nbd, DateTime nkt, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<List<PhieuNhap>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["nbd"] = nbd;
            param["nkt"] = nkt;
            MyNetwork.requestDataWithParam(param, URL_LAY_DANH_SACH_PHIEU_NHAP, f, MyDelegate);
        }
        #endregion


    }

}
