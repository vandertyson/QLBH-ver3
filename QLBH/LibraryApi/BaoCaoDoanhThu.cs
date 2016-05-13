using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApi
{
    public class BaoCaoDoanhThu
    {

        #region constaint
        public const string URL_SERVICE = @"http://localhost:32608/QLBanHang.asmx/";
        private static string URL_LAY_DOANH_THU_DOANH_SO = URL_SERVICE + @"LayDoanhThuDoanhSoTheoThang";
        #endregion

        #region struct
        public class BaoCaoDoanhThuDoanhSo
        {
            public decimal nam { get; set; }
            public decimal thang { get; set; }
            public decimal tong_doanh_so { get; set; }
            public decimal tong_doanh_thu { get; set; }
        }
        #endregion

        #region Function
        public static void lay_doanh_thu_doanh_so(DateTime tg_bat_dau, DateTime tg_ket_thuc, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<List<BaoCaoDoanhThuDoanhSo>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["thang_bd"] = tg_bat_dau;
            param["thang_kt"] = tg_ket_thuc;
            MyNetwork.requestDataWithParam(param, URL_LAY_DOANH_THU_DOANH_SO, f, MyDelegate);
        }
        #endregion
    }
}
