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
            public List<string> link_anh { get; set; }
            public List<SizeSoLuongHienTai> san_co { get; set; }
            public List<KhuyenMaiDangApDung> km_dang_ap_ung { get; set; }
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
            public string so_dien_thoai { get; set; }
            public string email { get; set; }
            public decimal diem_giam_tru { get; set; }
        }

        public class HoaDonChiTiet
        {
            public HangHoa hang { get; set; }
            public string ten_size { get; set; }
            public int so_luong { get; set; }
            public decimal gia_ban { get; set; }
        }

        public class HoaDon
        {
            public string ma_hoa_don { get; set; }
            public DateTime thoi_gian_tao { get; set; }
            public CuaHang cua_hang { get; set; }
            public KhachHang khach { get; set; }
            public string loai_thanh_toan { get; set; }
            public decimal giam_tru { get; set; }
            public List<HoaDonChiTiet> chi_tiet { get; set; }
            public decimal tong_gia_tri_hoa_don { get; set; }
            public decimal tong_tien_giam_tru_km { get; set; }
        }

        #endregion

        #region FUNCTION - Chứa các hàm lấy dữ liệu bằng request ( public static )

        public static void LayDanhSachKhachHang(object input, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["tên tham số truyền vào bên service"] = input;
            MyNetwork.requestDataWithParam(param, URL_DO_SOMETHING, f, MyDelegate);
        }

        #endregion
    }
}
