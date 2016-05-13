using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApi
{
    public class QuanLyTagHangHoa
    {
        #region Struct
        public class LoaiHangHoa
        {
            public decimal id { get; set; }
            public string ma_tag { get; set; }
            public string ten_tag { get; set; }
            public string link_anh { get; set; }
        }
        public class HangHoa
        {
            public string ma_hang_hoa { get; set; }
            public string ten_hang_hoa { get; set; }
        }
        public class Tag
        {
            public decimal id { get; set; }
            public string ten_tag { get; set; }
        }
        public class LoaiTag
        {
            public decimal id { get; set; }
            public string ma_loai_tag { get; set; }
            public string ten_loai_tag { get; set; }
            public List<Tag> ds_tag { get; set; }
        }
        #endregion
        #region Containt
        public const string TEN_TAG = @"ten_tag";
        public const string LINK_ANH = @"link_anh";
        public const string TEN_HANG_HOA = @"ten_hang_hoa";
        public const string URL_GAN_TAG_HANG_HOA = MyNetwork.URL_SERVICE + @"GanTagHangHoa";
        public const string URL_THEM_TAG = MyNetwork.URL_SERVICE + @"ThemTag";
        public const string URL_LAY_DANH_SACH_TAG = MyNetwork.URL_SERVICE + @"LayDanhSachTag";
        public const string URL_LAY_DANH_SACH_HANG_HOA = MyNetwork.URL_SERVICE + @"LayDanhSachHangHoa";
        #endregion
        public static void GanTagHangHoa(
           string ten_tag,
           string ten_hang_hoa,
           ContainerControl f,
           MyNetwork.CompleteHandle<MyNetwork.TraVe<Tag>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ten_tag"] = ten_tag;
            param["ten_hang_hoa"] = ten_hang_hoa;
            MyNetwork.requestDataWithParam(param, URL_GAN_TAG_HANG_HOA, f, MyDelegate);
        }

        public static void ThemTag(
            string ten_tag,
            string ten_loai_tag,
            string link_anh,
           ContainerControl f,
           MyNetwork.CompleteHandle<MyNetwork.TraVe<Tag>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["ten_tag"] = ten_tag;
            param["ten_loai_tag"] = ten_loai_tag;
            param["link_anh"] = link_anh;
            MyNetwork.requestDataWithParam(param, URL_THEM_TAG, f, MyDelegate);
        }

        public static void LayDanhSachTag(
           ContainerControl f,
           MyNetwork.CompleteHandle<MyNetwork.TraVe<List<String>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_LAY_DANH_SACH_TAG, f, MyDelegate);
        }

        public static void LayDanhSachHangHoa(
          ContainerControl f,
          MyNetwork.CompleteHandle<MyNetwork.TraVe<List<HangHoa>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_LAY_DANH_SACH_HANG_HOA, f, MyDelegate);
        }
    }
}
