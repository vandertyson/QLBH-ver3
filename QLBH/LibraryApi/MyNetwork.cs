using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
namespace LibraryApi
{
    public class MyNetwork
    {
        #region CONST SERVICE URL
        //public const string URL_SERVICE = @"http://quanlybanhang.somee.com//QLBanHang.asmx/";
        public const string URL_SERVICE = @"http://localhost:32608/QLBanHang.asmx/";
        
        #endregion
        #region Danh mục hàng hóa
        public const string URL_GET_LOAI_HANG = URL_SERVICE + @"DanhSachLoaiHang";
        public const string URL_LAY_DANH_SACH_HANG_THEO_LOAI_HANG = URL_SERVICE + @"LayDanhSachHangHoaTheoLoaiHangHoa";
        public const string URL_GET_TAG = URL_SERVICE;
        public const string URL_THEM_HANG_HOA = @"ThemHangHoa";
        #endregion
        #region Quản lý bán hàng
        #endregion

        public delegate void CompleteHandle<T>(T data);
        public class TraVe<T>
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }
        }
        public static void requestDataWithParam<T>(Dictionary<string, object> param
            , string url
            , ContainerControl f
            , CompleteHandle<TraVe<T>> MyDelegate
            )
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                foreach (KeyValuePair<string, object> pair in param)
                {
                    request.AddParameter(pair.Key, pair.Value);
                }
                
                client.ExecuteAsync(request, response =>
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //var jsonObject = JsonConvert.DeserializeObject<TraVe<T>>(response.Content);
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var jsonObject = js.Deserialize<TraVe<T>>(response.Content);
                        if (f!=null)
                        {
                            f.Invoke(new Action(() =>
                            {
                                MyDelegate(jsonObject);
                            }));
                        }
                        else
                        {
                            MyDelegate(jsonObject);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra do một số chức năng đang hoàn thiện");
                        MessageBox.Show(response.ErrorException.InnerException.Message);
                        //throw new Exception(response.ErrorMessage);
                    }
                });
            }
            catch (Exception e)
            {
                MessageBox.Show("Đã có lỗi xảy ra do một số chức năng đang hoàn thiện");
                MessageBox.Show(e.InnerException.Message);
            }
        }

        #region Quản lý danh mục hàng hóa

        

        public static void LayDanhSachHangHoaTheoLoaiHangHoa(decimal id_loai_hang, Form f, CompleteHandle<TraVe<List<HangHoa>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["id_loai_hang_hoa"] = id_loai_hang;
            requestDataWithParam(param, URL_LAY_DANH_SACH_HANG_THEO_LOAI_HANG, f, MyDelegate);
        }
        #region Chi tiết hàng hóa
       
        #endregion
        #endregion



    }
}
