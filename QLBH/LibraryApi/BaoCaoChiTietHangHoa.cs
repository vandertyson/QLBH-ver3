using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LibraryApi
{
    public class BaoCaoChiTietHangHoa
    {
        #region CONST SERVICE URL
        //public const string URL_SERVICE = @"http://quanlybanhang.somee.com//QLBanHang.asmx/";
        public const string URL_SERVICE = @"http://localhost:32608/QLBanHang.asmx/";
        public const string URL_LAY_BAO_CAO_PHAN_HOI_KHACH_HANG = URL_SERVICE + @"BaoCaoPhanHoiKhachHang";
        public const string URL_LAY_BAO_CAO_CHI_TIET_KHUYEN_MAI = URL_SERVICE + @"BaoCaoChiTietKhuyenMai";
        #endregion

        #region Struct
        public class KhachHang
        {
            public decimal id { get; set; }
            public string ten_khach_hang { get; set; }
            public string link_anh_dai_dien { get; set; }
        }
        public class LuotXem
        {
            public decimal id { get; set; }
            public DateTime thoi_gian { get; set; }
        }
        public class Comment
        {
            public decimal id { get; set; }
            public string noi_dung { get; set; }
            public DateTime thoi_gian { get; set; }
            public KhachHang nguoi_commnet { get; set; }
        }
        public class ThongKeTheoThang
        {
            public int thang { get; set; }
            public int nam { get; set; }
            public List<Comment> comments { get; set; }
            public List<LuotXem> luot_xem { get; set; }
        }
        public class BaoCaoPhanHoi
        {
            public List<ThongKeTheoThang> thong_ke_theo_thang { get; set; }
            public int views { get; set; }
            public int comments { get; set; }
            public int duoc_yeu_thich { get; set; }
            public double rating { get; set; }
        }
        public class BaoCaoKhuyenMai
        {
            public DotKhuyenMai dot_khuyen_mai_hien_tai { get; set; }
            public List<DotKhuyenMai> lich_su { get; set; }
        }
        public class DotKhuyenMai
        {
            public decimal id { get; set; }
            public string ma_dot { get; set; }
            public string mo_ta { get; set; }
            public DateTime thoi_gian_bat_dau { get; set; }
            public DateTime thoi_gian_ket_thuc { get; set; }
            public decimal muc_khuyen_mai { get; set; }
            public int luot_xem { get; set; }
            public int luot_mua { get; set; }
            public decimal so_tien_ban_duoc { get; set; }
            public int so_luong_ban_duoc { get; set; }
            public decimal tong_doanh_thu { get; set; }
            public int tong_doanh_so { get; set; }
        }
        #endregion

        #region Function

        public static void LayBaoCaoPhanHoiKhachHang(decimal id_hang_hoa, DateTime bat_dau, int so_thang, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<BaoCaoPhanHoi>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["id_hang_hoa"] = id_hang_hoa;
            param["bat_dau"] = bat_dau;
            param["so_thang"] = so_thang;
            MyNetwork.requestDataWithParam(param, URL_LAY_BAO_CAO_PHAN_HOI_KHACH_HANG, f, MyDelegate);
        }

        public static void LayBaoCaoChiTietKhuyenMai(decimal id_hang_hoa, DateTime ngay_hien_tai, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<BaoCaoKhuyenMai>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["id_san_pham"] = id_hang_hoa;
            param["ngay_hien_tai"] = ngay_hien_tai;
            MyNetwork.requestDataWithParam(param, URL_LAY_BAO_CAO_CHI_TIET_KHUYEN_MAI, f, MyDelegate);
        }

       #endregion
    }
}
