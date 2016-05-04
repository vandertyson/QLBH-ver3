using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApi
{
    public class TemplateClass
    {
        #region CONST SERVICE URL - Chứa địa chỉ service
        //public const string URL_SERVICE = @"http://quanlybanhang.somee.com//QLBanHang.asmx/";
        public const string URL_SERVICE = @"http://localhost:32608/QLBanHang.asmx/";
        public const string URL_DO_SOMETHING = URL_SERVICE + @"tên request";
        #endregion

        #region STRUCT- Chứa các cấu trúc dữ liệu ( public class )

        public class A
        {
            public string MyProperty { get; set; }
        }

        #endregion

        #region FUNCTION - Chứa các hàm lấy dữ liệu bằng request ( public static )

        public static void GetSomething(object input, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<A>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["tên tham số truyền vào bên service"] = input;      
            MyNetwork.requestDataWithParam(param, URL_DO_SOMETHING, f, MyDelegate);
        }

        #endregion
    }
}
