using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApi
{
    class QuanLyKhuyenMai
    {
        public const string URL_SERVICE = @"http://localhost:32608/QLBanHang.asmx/";
        private static string URL_THEM_DOT_KHUYEN_MAI = URL_SERVICE+@"ThemDotKhuyenMai";

        #region struct
        public class HangKhuyenMai
        {
            public decimal id { get; set; }
            public string ten_hang_hoa { get; set; }
            public decimal muc_khuyen_mai { get; set; }
        }
        public class KhuyenMai
        {
            public decimal id { get; set; }
            public string ma_dot { get; set; }
            public string mo_ta { get; set; }
            public DateTime tg_bat_dau { get; set; }
            public DateTime tg_ket_thuc { get; set; }
            public List<HangKhuyenMai> ds_hang { get; set; }
       }
        #endregion
        #region Function
        public static void them_dot_khuyen_mai(string ma_dot, string mo_ta, DateTime tg_bat_dau, DateTime tg_ket_thuc, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ma_dot"] = ma_dot;
            param["mo_ta"] = mo_ta;
            param["tg_bt_dau"] = tg_bat_dau;
            param["tg_ket_thuc"] = tg_ket_thuc;
            MyNetwork.requestDataWithParam(param, URL_THEM_DOT_KHUYEN_MAI, f, MyDelegate);
        }
        #endregion


    }
}