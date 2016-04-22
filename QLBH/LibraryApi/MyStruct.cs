using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi
{
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
        public string ten_hang_hoa { get; set; }
        public string nha_san_xuat { get; set; }
        public string chat_lieu { get; set; }
        public decimal gia { get; set; }
        public List<Tag> ds_tag { get; set; }
        public List<string> ds_link { get; set; }
    }
    public class Tag
    {
        public decimal id { get; set; }
        public string ma_tag { get; set; }
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
    public class ThemHangHoaPost
    {
        public string ten_hang_hoa { get; set; }
        public string ma_tra_cuu { get; set; }
        public string ma_nha_cung_cap { get; set; }
        public string mo_ta { get; set; }
        public List<string> link_anh { get; set; }
        public List<string> tag { get; set; }
    }
    namespace ChiTietHangHoa
    {
        namespace BaoCaoPhanHoi
        {
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
        }
        namespace BaoCaoKhuyenMai
        {
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
        }
        #region enum

        #endregion
    }
    namespace QuanLyBanHang
    {
        public class HangHoa
        {
            public string ma_tra_cuu_hang_hoa { get; set; }
            public string ten_size { get; set; }
            public int so_luong { get; set; }
            public decimal gia_nhap { get; set; }
        }
        public class PhieuNhap
        {
            public DateTime ngay_nhap { get; set; }
            public string ten_tai_khoan { get; set; }
            public decimal id_cua_hang { get; set; }
            public List<HangHoa> list_hang_hoa { get; set; }
        }
    }
}



