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

        #endregion

    }

}
