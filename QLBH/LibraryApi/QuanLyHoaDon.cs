using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApi
{
    public class QuanLyHoaDon
    {
        #region CONST SERVICE URL - Chứa địa chỉ service

        //public const string URL_SERVICE = @"http://quanlybanhang.somee.com//QLBanHang.asmx/";
        public const string URL_SERVICE = @"http://localhost:32608/QLBanHang.asmx/";
        public const string URL_DO_SOMETHING = URL_SERVICE + @"tên request";
        private static string URL_LAY_DANH_SACH_KHACH_HANG = URL_SERVICE + @"LayDanhSachKhachHang";
        private static string URL_LAY_DANH_SACH_HANG_HOA = URL_SERVICE + @"LayDanhSachHangHoaByCuaHangAndNgay";
        private static string URL_THEM_HOA_DON = URL_SERVICE + @"ThemHoaDon";
        private static string URL_LAY_DANH_SACH_HOA_DON = URL_SERVICE + @"LayDanhSachHoaDon";
        private static string URL_LAY_MA_HOA_DON = URL_SERVICE + @"LayMaHoaDon";
        private static string URL_XOA_HOA_DON = URL_SERVICE + @"XoaHoaDon";
        private static string URL_SUA_HOA_DON = URL_SERVICE + @"SuaHoaDon";

        #endregion

        #region STRUCT- Chứa các cấu trúc dữ liệu ( public class )

        public class CuaHang
        {
            public string ten_cua_hang { get; set; }
            public string dia_chi { get; set; }
            public string so_dien_thoai { get; set; }
        }
        public class HangHoa
        {
            public string ma_hang_hoa { get; set; }
            public string ten_hang_hoa { get; set; }
            public decimal gia_hien_tai { get; set; }
            public List<SizeSoLuongHienTai> san_co { get; set; }
            public KhuyenMaiDangApDung km_dang_ap_ung { get; set; }
        }
        public class SizeSoLuongHienTai
        {
            public string ten_size { get; set; }
            public int so_luong { get; set; }
        }
        public class KhuyenMaiDangApDung
        {
            public string ma_dot_khuyen_mai { get; set; }
            public string mo_ta { get; set; }
            public decimal muc_khuyen_mai { get; set; }
        }
        public class KhachHang
        {
            public string tai_khoan { get; set; }
            public string ten_khach_hang { get; set; }
            public decimal diem_giam_tru { get; set; }
            public DateTime ngay_gia_nhap { get; set; }
        }
        public class HoaDonChiTiet
        {
            public decimal gia_goc { get; set; }
            public string dot_khuyen_mai { get; set; }
            public decimal muc_khuyen_mai { get; set; }
            public string ma_hang { get; set; }
            public string ten_size { get; set; }
            public int so_luong { get; set; }
            public decimal gia_ban { get; set; }
        }
        public class HoaDon
        {
            public string ma_hoa_don { get; set; }
            public DateTime thoi_gian_tao { get; set; }
            public string ten_cua_hang { get; set; }
            public string tai_khoan_tao { get; set; }
            public KhachHang khach { get; set; }
            public string loai_thanh_toan { get; set; }
            public decimal giam_tru { get; set; }
            public List<HoaDonChiTiet> chi_tiet { get; set; }
            public decimal tong_gia_tri_hoa_don { get; set; }
            public decimal tong_tien_giam_tru_km { get; set; }
            public decimal thanh_tien { get; set; }
        }
        #endregion

        #region FUNCTION - Chứa các hàm lấy dữ liệu bằng request ( public static )

        public static void LayDanhSachKhachHang(DateTime ngay_hien_tai, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<List<KhachHang>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ip_ngay_hien_tai"] = ngay_hien_tai;
            MyNetwork.requestDataWithParam(param, URL_LAY_DANH_SACH_KHACH_HANG, f, MyDelegate);
        }
        public static void LayDanhSachHangHoa(decimal id_cua_hang, DateTime ngay_hien_tai, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<List<HangHoa>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ip_id_cua_hang"] = id_cua_hang;
            param["ip_ngay_hien_tai"] = ngay_hien_tai;
            MyNetwork.requestDataWithParam(param, URL_LAY_DANH_SACH_HANG_HOA, f, MyDelegate);
        }
        public static void ThemHoaDon(HoaDon hoa_don, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ip_hoa_don"] = JsonConvert.SerializeObject(hoa_don);
            MyNetwork.requestDataWithParam(param, URL_THEM_HOA_DON, f, MyDelegate);
        }
        public static void LayDanhSachHoaDon(DateTime ngay_hien_tai,Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<List<HoaDon>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ngay_hien_tai"] = ngay_hien_tai;
            MyNetwork.requestDataWithParam(param, URL_LAY_DANH_SACH_HOA_DON, f, MyDelegate);
        }
        public static void LayDanhSachHoaDon(Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<List<HoaDon>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_LAY_DANH_SACH_HOA_DON, f, MyDelegate);
        }
        public static void LayMaHoaDon(Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_LAY_MA_HOA_DON, f, MyDelegate);
        }
        public static void XoaHoaDon(string ma_hoa_don, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ma_hoa_don"] = ma_hoa_don;
            MyNetwork.requestDataWithParam(param, URL_XOA_HOA_DON, f, MyDelegate);
        }
        public static void SuaHoaDon(HoaDon hoa_don, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ip_hoa_don"] = JsonConvert.SerializeObject(hoa_don);
            MyNetwork.requestDataWithParam(param, URL_SUA_HOA_DON, f, MyDelegate);
        }
        #endregion
    }
}
