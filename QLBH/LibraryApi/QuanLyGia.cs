using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApi
{
    public class QuanLyGia
    {
        #region URL SERVICE
        //public const string URL_SERVICE = @"http://quanlybanhang.somee.com//QLBanHang.asmx/";
        public const string URL_SERVICE = @"http://localhost:32608/QLBanHang.asmx/";
        public const string URL_THEM_GIA_EXCEL = URL_SERVICE + @"ThemGiaExcel";

        #endregion

        #region STRUCT- Chứa các cấu trúc dữ liệu ( public class )
        public class GiaExcel
        {
            public string ma_tra_cuu { get; set; }
            public decimal gia_ban { get; set; }
            public DateTime ngay_ap_dung { get; set; }
        }

        #endregion

        #region FUNCTION - Chứa các hàm lấy dữ liệu bằng request ( public static )

        public static void ThemGiaTuExcel(List<GiaExcel> list_gia, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ip_gia"] = JsonConvert.SerializeObject(list_gia);
            MyNetwork.requestDataWithParam(param, URL_THEM_GIA_EXCEL, f, MyDelegate);
        }

        #endregion
    }
}
