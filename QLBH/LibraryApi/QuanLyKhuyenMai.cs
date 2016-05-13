using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApi
{
    public class QuanLyKhuyenMai
    {
        #region constaint
        public const string URL_SERVICE = @"http://localhost:32608/QLBanHang.asmx/";
        private static string URL_THEM_DOT_KHUYEN_MAI = URL_SERVICE+ @"ThemDotKhuyenMai";
        private static string URL_THEM_HANG_HOA_KHUYEN_MAI = URL_SERVICE + @"ThemHangHoaKhuyenMai";
        private static string URL_SUA_MUC_KHUYEN_MAI = URL_SERVICE + @"SuaMucKhuyenMai";
        private static string URL_GEN_MA_KHUYEN_MAI = URL_SERVICE + @"GenMaKhuyenMai";
        private static string URL_LAY_THONG_TIN_KHUYEN_MAI = URL_SERVICE + @"LayThongTinKhuyenMai";
        #endregion
        #region struct
        public class HangKhuyenMai
        {
            public decimal id { get; set; }
            public string ten_hang_hoa { get; set; }
            public string ma_hang_hoa { get; set; }
            public decimal muc_khuyen_mai { get; set; }
        }
        public class KhuyenMai_HangHoa
        {
            public string ma_dot { get; set; }
            public string ma_hang_hoa { get; set; }
            public decimal muc_km { get; set; }
            
        }
        public class KhuyenMai
        {
            public decimal id { get; set; }
            public string ma_dot { get; set; }
            public string mo_ta { get; set; }
            public DateTime tg_bat_dau { get; set; }
            public DateTime tg_ket_thuc { get; set; }
       }
        #endregion
        #region Function
        public static void them_dot_khuyen_mai(string ma_dot, string mo_ta, DateTime tg_bat_dau, DateTime tg_ket_thuc, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ma_dot"] = ma_dot;
            param["mo_ta"] = mo_ta;
            param["tg_bd"] = tg_bat_dau;
            param["tg_kt"] = tg_ket_thuc;
            MyNetwork.requestDataWithParam(param, URL_THEM_DOT_KHUYEN_MAI, f, MyDelegate);
        }
        #endregion

        public static void them_hang_hoa_khuyen_mai(List<KhuyenMai_HangHoa> list_hang_hoa,Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["list_hang_hoa"] = JsonConvert.SerializeObject(list_hang_hoa);
            MyNetwork.requestDataWithParam(param, URL_THEM_HANG_HOA_KHUYEN_MAI, f, MyDelegate);
        }

        public static void sua_muc_khuyen_mai(string ma_hang_hoa, string ma_dot, decimal muc_khuyen_mai, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ma_dot"] = ma_dot;
            param["ma_hang_hoa"] = ma_hang_hoa;
            param["muc_km"] = muc_khuyen_mai;
            MyNetwork.requestDataWithParam(param, URL_SUA_MUC_KHUYEN_MAI, f, MyDelegate);
        }

        public static void gen_ma_khuyen_mai(Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_GEN_MA_KHUYEN_MAI, f, MyDelegate);
        }

        public static void lay_thong_tin_khuyen_mai(Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<List<KhuyenMai>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_LAY_THONG_TIN_KHUYEN_MAI, f, MyDelegate);
        }

    }
}