using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApi
{
    public class QuanLyDanhMucHangHoa
    {
        #region Constant
        public const string DANH_MUC_SP = @"DANH_MUC_SAN_PHAM";
        public const string SIZE = @"SIZE_QUAN_AO";
        public const string URL_GET_LOAI_HANG = MyNetwork.URL_SERVICE + @"DanhSachLoaiHang";
        public const string URL_TIM_KIEM_HANG_HOA = MyNetwork.URL_SERVICE + @"TimKiemHangHoa";
        public const string URL_LAY_DANH_SACH_NHA_CUNG_CAP = MyNetwork.URL_SERVICE + @"LayDanhSachNhaCungCap";
        private static string URL_THEM_HANG_HOA_EXCEL = MyNetwork.URL_SERVICE + @"ThemHangHoa";
        private static string URL_CHI_TIET_HANG_HOA = MyNetwork.URL_SERVICE + @"ChiTietHangHoa";
        public const string URL_LAY_DANH_SACH_HANG_HOA_VA_MA_TRA_CUU = MyNetwork.URL_SERVICE + @"LayHangHoaVaMaTraCuu";

        #endregion
        #region Struct
        public class HangHoaVaMa
        {
            public string ten_hang_hoa { get; set; }
            public string ma_tra_cuu { get; set; }
        }
        public class LoaiHang
        {
            public decimal id { get; set; }
            public string ma_tag { get; set; }
            public string ten_tag { get; set; }
            public string link_anh { get; set; }
        }
        public class HangHoaMaster
        {
            public decimal id { get; set; }
            public string ma_hang_hoa { get; set; }
            public string ma_tra_cuu { get; set; }
            public string ten_hang_hoa { get; set; }
            public string nha_san_xuat { get; set; }
            public decimal gia { get; set; }
            public float do_giam_gia { get; set; }
            public int so_comment { get; set; }
            public decimal diem { get; set; }
            public int luot_click { get; set; }
            public List<string> ds_link { get; set; }
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
        public class HangHoa
        {
            public decimal id { get; set; }
            public string ma_hang_hoa { get; set; }
            public string ma_vach { get; set; }
            public string ten { get; set; }
            public string mo_ta { get; set; }
            public NhaCungCap nha_cung_cap { get; set; }
            public List<string> link_anh { get; set; }
            public List<Tag> ds_tag { get; set; }
            public decimal gia { get; set; }
            public decimal khuyen_mai { get; set; }
            public decimal luot_xem { get; set; }
            public decimal diem_danh_gia { get; set; }
            public List<NhanXet> nhan_xet { get; set; }
            public List<CuaHang> cua_hang { get; set; }
        }
        public class NhaCungCap
        {
            public decimal id { get; set; }
            public string ten { get; set; }
            public string ten_nguoi_dai_dien { get; set; }
            public string so_dien_thoai { get; set; }
            public string email { get; set; }
        }
        public class NhaCungCapV2
        {
            public string ma_nha_cung_cap { get; set; }
            public string ten_nha_cung_cap { get; set; }
            public string dia_chi { get; set; }
        }
        public class CuaHang
        {
            public decimal id { get; set; }
            public string ten_cua_hang { get; set; }
            public List<SoLuong> ton_kho { get; set; }
        }
        public class SoLuong
        {
            public decimal id_size { get; set; }
            public string ten_size { get; set; }
            public decimal so_luong { get; set; }
        }
        public class NhanXet
        {
            public decimal id { get; set; }
            public decimal id_tai_khoan { get; set; }
            public string ten_tai_khoan { get; set; }
            public string nhan_xet { get; set; }
            public DateTime thoi_gian { get; set; }
        }
        public class HoaDonMaster
        {
            public decimal id { get; set; }
            public DateTime ngay_mua { get; set; }
            public List<HoaDonSimple> hang_hoa { get; set; }
        }
        public class HoaDonSimple
        {
            public HangHoaMaster hang_hoa { get; set; }
            public decimal id_size { get; set; }
            public decimal so_luong { get; set; }
            public decimal gia_ban { get; set; }
        }
        public class HangHoaDaXem
        {
            public DateTime thoi_gian { get; set; }
            public HangHoaMaster hang_hoa { get; set; }
            public decimal so_click { get; set; }
        }
        public class CommentMaster
        {
            public HangHoaMaster hang_hoa { get; set; }
            public string comment { get; set; }
            public DateTime thoi_gian { get; set; }
        }
        public class PhieuNhapXuat
        {
            public decimal id { get; set; }
            public string ma_phieu { get; set; }
            public string loai_phieu { get; set; }
            public DateTime ngay_nhap_xuat { get; set; }
            public List<HoaDonSimple> thong_tin_chi_tiet { get; set; }
        }
        public class ThemHangHoa
        {
            public string ten_hang_hoa { get; set; }
            public string ma_tra_cuu { get; set; }
            public string ma_nha_cung_cap { get; set; }
            public string mo_ta { get; set; }
            public List<string> link_anh { get; set; }
            public List<string> tag { get; set; }
        }
        public class HangHoaExcel
        {
            public string ten_hang_hoa { get; set; }
            public string ma_tra_cuu { get; set; }
            public string ma_nha_cung_cap { get; set; }
            public string mo_ta { get; set; }
            public List<string> link_anh { get; set; }
            public List<string> tag { get; set; }
        }
        #endregion

        public static void GetDanhSachLoaiHang(
            ContainerControl f,
            MyNetwork.CompleteHandle<MyNetwork.TraVe<List<LoaiHang>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_GET_LOAI_HANG, f, MyDelegate);
        }
        public static void TimKiemHangHoa(
            string keyword,
            int length,
            int page,
            ContainerControl f,
            MyNetwork.CompleteHandle<MyNetwork.TraVe<List<HangHoaMaster>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["keyword"] = keyword;
            param["length"] = length;
            param["page"] = page;
            MyNetwork.requestDataWithParam(param, URL_TIM_KIEM_HANG_HOA, f, MyDelegate);
        }
        public static void LayDanhSachNhaCungCap(Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<List<NhaCungCapV2>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_LAY_DANH_SACH_NHA_CUNG_CAP, f, MyDelegate);
        }
        public static void ThemHangHoaExcel(
          List<HangHoaExcel> list_hang_hoa,
          Form f,
          MyNetwork.CompleteHandle<MyNetwork.TraVe<string>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["list_hang_hoa"] = JsonConvert.SerializeObject(list_hang_hoa);
            MyNetwork.requestDataWithParam(param, URL_THEM_HANG_HOA_EXCEL, f, MyDelegate);
        }
        public static void ChiTietHangHoa(decimal id_hang_hoa, Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<LibraryApi.HangHoa>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param["id_hang_hoa"] = id_hang_hoa;
            MyNetwork.requestDataWithParam(param, URL_CHI_TIET_HANG_HOA, f, MyDelegate);
        }

        public static void LayDanhSachHangVaMaTraCuu(Form f, MyNetwork.CompleteHandle<MyNetwork.TraVe<List<HangHoaVaMa>>> MyDelegate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            MyNetwork.requestDataWithParam(param, URL_LAY_DANH_SACH_HANG_HOA_VA_MA_TRA_CUU, f, MyDelegate);
        }

    }
}
