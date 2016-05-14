using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService3
{
    public class BaoCaoChiTietHangHoa
    {
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
            public string thoi_gian { get; set; }
        }

        public class Comment
        {
            public decimal id { get; set; }
            public string noi_dung { get; set; }
            public string thoi_gian { get; set; }
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
            public string thoi_gian_bat_dau { get; set; }
            public string thoi_gian_ket_thuc { get; set; }
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

        #region Chi tiết hàng hóa - Phản hồi khách hàng

        public static BaoCaoPhanHoi bao_cao_phan_hoi_khach_hang(string bat_dau, int so_thang, decimal id_hang_hoa)
        {
            BaoCaoPhanHoi result = new BaoCaoPhanHoi();
            result.rating = tinh_rating(id_hang_hoa);
            result.duoc_yeu_thich = so_khach_hang_yeu_thich(id_hang_hoa);
            result.thong_ke_theo_thang = new List<ThongKeTheoThang>();
            var p = Common.lay_cac_thang_tiep_theo(bat_dau, so_thang);
            foreach (var item in p)
            {
                result.thong_ke_theo_thang.Add(lay_thong_ke_theo_thang(item.thang, item.nam, id_hang_hoa));
            }
            result.comments = so_luot_comment_den_hien_tai(id_hang_hoa);
            result.views = so_luot_xem_den_thoi_diem_hien_tai(id_hang_hoa);
            return result;
        }

        public static ThongKeTheoThang lay_thong_ke_theo_thang(int thang, int nam, decimal id_hang_hoa)
        {
            ThongKeTheoThang result = new ThongKeTheoThang();
            result.nam = nam;
            result.thang = thang;
            result.luot_xem = lay_luot_xem_trong_thang(thang, nam, id_hang_hoa);
            result.comments = lay_comment_trong_thang(thang, nam, id_hang_hoa);
            return result;
        }

        public static List<Comment> lay_comment_trong_thang(int thang, int nam, decimal id_hang_hoa)
        {
            List<Comment> result = new List<Comment>();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var p1 = context.GD_NHAN_XET.Where(s => s.ID_HANG_HOA == id_hang_hoa & s.THOI_GIAN.Year == nam & s.THOI_GIAN.Month == thang).ToList();
                var p2 = context.DM_LOAI_TAI_KHOAN.Where(s => s.MA_LOAI == "CUSTOMER").Select(s => s.ID).ToList();
                foreach (var item in p1)
                {
                    var p3 = item.DM_TAI_KHOAN.ID_LOAI_TAI_KHOAN;
                    //Kiem tra nguoi comment la khach hang
                    if (!p2.Contains(p3)) continue;
                    //Lay comment
                    Comment cm = new Comment();
                    //Lay thong tin comment
                    var p4 = new KhachHang();
                    p4.id = item.DM_TAI_KHOAN.ID;
                    p4.ten_khach_hang = item.DM_TAI_KHOAN.TEN_TAI_KHOAN;
                    //
                    cm.id = item.ID;
                    cm.nguoi_commnet = p4;
                    cm.noi_dung = item.NHAN_XET;
                    cm.thoi_gian = item.THOI_GIAN.ToString();
                    result.Add(cm);
                }
            }
            return result;
        }

        public static List<LuotXem> lay_luot_xem_trong_thang(int thang, int nam, decimal id_hang_hoa)
        {
            List<LuotXem> result = new List<LuotXem>();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var p1 = context.GD_CLICK_HANG_HOA.Where(s => s.ID_HANG_HOA == id_hang_hoa & s.THOI_GIAN.Year == nam & s.THOI_GIAN.Month == thang).ToList();
                var p2 = context.DM_LOAI_TAI_KHOAN.Where(s => s.MA_LOAI == "CUSTOMER").First();
                foreach (var item in p1)
                {
                    var p3 = item.DM_TAI_KHOAN.ID_LOAI_TAI_KHOAN;
                    //Kiem tra nguoi comment la khach hang
                    if (p2.ID == p3) continue;
                    //Lay comment
                    LuotXem lx = new LuotXem();
                    //Lay thong tin comment
                    var p4 = new KhachHang();
                    p4.id = item.DM_TAI_KHOAN.ID;
                    p4.ten_khach_hang = item.DM_TAI_KHOAN.TEN_TAI_KHOAN;
                    //
                    lx.id = item.ID;
                    lx.thoi_gian = item.THOI_GIAN.ToString();
                    //
                    result.Add(lx);
                }
            }
            return result;
        }

        public static double tinh_rating(decimal id_hang_hoa)
        {
            double result = 0;
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var p1 = context.GD_DANH_GIA.Where(s => s.ID_HANG_HOA == id_hang_hoa).ToList();
                double tong_diem = 0;
                if (p1.Count == 0)
                {
                    return 5.0;
                }
                foreach (var item in p1)
                {
                    tong_diem += Convert.ToDouble(item.DIEM);
                }
                result = tong_diem / p1.Count;
            }
            return result;
        }

        public static int so_khach_hang_yeu_thich(decimal id_hang_hoa)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                return context.GD_SAN_PHAM_UA_THICH.Where(s => s.ID_HANG_HOA == id_hang_hoa).ToList().Count;
            }
        }

        public static int so_luot_comment_den_hien_tai(decimal id_hang_hoa)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var p1 = context.GD_NHAN_XET.Where(s => s.ID_HANG_HOA == id_hang_hoa).ToList();
                var p2 = context.DM_TAI_KHOAN.Where(s => s.ID_LOAI_TAI_KHOAN != 3).Select(s => s.ID).ToList();
                return p1.Where(s => !p2.Contains(s.ID_TAI_KHOAN)).ToList().Count;
            }
        }

        public static int so_luot_xem_den_thoi_diem_hien_tai(decimal id_hang_hoa)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var p1 = context.GD_CLICK_HANG_HOA.Where(s => s.ID_HANG_HOA == id_hang_hoa).ToList();
                var p2 = context.DM_TAI_KHOAN.Where(s => s.ID_LOAI_TAI_KHOAN != 3).Select(s => s.ID).ToList();
                return p1.Where(s => !p2.Contains(s.ID)).ToList().Count();
            }
        }

        #endregion

        #region Chi tiết hàng hóa - Thông tin khuyến mãi

        public static BaoCaoKhuyenMai bao_cao_khuyen_mai_san_pham(decimal id_san_pham, string ngay_hien_tai)
        {
            BaoCaoKhuyenMai result = new BaoCaoKhuyenMai();
            result.lich_su = new List<DotKhuyenMai>();
            var list_dot_khuyen_mai = danh_sach_id_cac_dot_khuyen_mai_cua_san_pham(id_san_pham);
            foreach (var item in list_dot_khuyen_mai)
            {
                var dot_km = thong_tin_dot_khuyen_mai(id_san_pham, item);
                if (Convert.ToDateTime(dot_km.thoi_gian_bat_dau) <= Convert.ToDateTime(ngay_hien_tai) & Convert.ToDateTime(dot_km.thoi_gian_ket_thuc) >= Convert.ToDateTime(ngay_hien_tai))
                {
                    result.dot_khuyen_mai_hien_tai = dot_km;
                }
                else
                {
                    result.lich_su.Add(dot_km);
                }
            }
            return result;
        }

        public static DotKhuyenMai thong_tin_dot_khuyen_mai(decimal id_san_pham, decimal id_dot_km)
        {
            DotKhuyenMai result = new DotKhuyenMai();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var p2 = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).First();
                //
                result.id = p2.ID;
                result.ma_dot = p2.MA_DOT;
                result.mo_ta = p2.MO_TA;
                result.thoi_gian_bat_dau = p2.THOI_GIAN_BAT_DAU.ToString();
                result.thoi_gian_ket_thuc = p2.THOI_GIAN_KET_THUC.ToString();
                result.muc_khuyen_mai = context.GD_KHUYEN_MAI_CHI_TIET.Where(s => s.ID_KHUYEN_MAI == id_dot_km & s.ID_HANG_HOA == id_san_pham).Select(s => s.MUC_KHUYEN_MAI).First();
                //
                result.luot_mua = get_luot_mua_san_pham_trong_dot_km(id_san_pham, id_dot_km);
                result.luot_xem = get_luot_xem_san_pham_trong_dot_km(id_san_pham, id_dot_km);
                result.so_luong_ban_duoc = get_doanh_so_san_pham_trong_dot_km(id_san_pham, id_dot_km);
                result.so_tien_ban_duoc = get_doanh_thu_san_pham_trong_dot_km(id_san_pham, id_dot_km);
                result.tong_doanh_so = get_tong_doanh_so_dot_km(id_dot_km);
                result.tong_doanh_thu = get_tong_doanh_thu_dot_km(id_dot_km);
            }
            return result;
        }

        public static decimal get_tong_doanh_thu_dot_km(decimal id_dot_km)
        {
            decimal result = 0;

            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ngay_bat_dau = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_BAT_DAU).First();
                var ngay_ket_thuc = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_KET_THUC).First();
                var list_hoa_don = context.GD_HOA_DON.Where(s => s.THOI_GIAN_TAO <= ngay_ket_thuc & s.THOI_GIAN_TAO >= ngay_bat_dau).ToList();
                foreach (var item in list_hoa_don)
                {
                    decimal tong_tien = 0;
                    var chi_tiet = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == item.ID).ToList();
                    foreach (var item2 in chi_tiet)
                    {
                        tong_tien += item2.SO_LUONG * item2.GIA_BAN;
                    }
                    tong_tien -= Convert.ToDecimal(item.GIAM_TRU);
                    result += tong_tien;
                }
            }
            return result;
        }

        public static int get_tong_doanh_so_dot_km(decimal id_dot_km)
        {
            int result = 0;
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ngay_bat_dau = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_BAT_DAU).First();
                var ngay_ket_thuc = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_KET_THUC).First();
                var list_hoa_don = context.GD_HOA_DON.Where(s => s.THOI_GIAN_TAO <= ngay_ket_thuc & s.THOI_GIAN_TAO >= ngay_bat_dau).Select(s => s.ID).ToList();
                foreach (var item in list_hoa_don)
                {
                    var chi_tiet = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == item).ToList();
                    foreach (var item2 in chi_tiet)
                    {
                        result += (int)item2.SO_LUONG;
                    }
                }
            }
            return result;
        }

        public static decimal get_doanh_thu_san_pham_trong_dot_km(decimal id_san_pham, decimal id_dot_km)
        {
            decimal result = 0;
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ngay_bat_dau = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_BAT_DAU).First();
                var ngay_ket_thuc = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_KET_THUC).First();
                var list_hoa_don = context.GD_HOA_DON.Where(s => s.THOI_GIAN_TAO <= ngay_ket_thuc & s.THOI_GIAN_TAO >= ngay_bat_dau).ToList();
                foreach (var item in list_hoa_don)
                {
                    decimal tong_tien = 0;
                    var chi_tiet = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == item.ID & s.ID_HANG_HOA == id_san_pham).ToList();
                    foreach (var tt in chi_tiet)
                    {
                        tong_tien += tt.SO_LUONG * tt.GIA_BAN;
                    }
                    tong_tien -= Convert.ToDecimal(item.GIAM_TRU);
                    result += tong_tien;
                }
            }
            return result;
        }

        public static int get_doanh_so_san_pham_trong_dot_km(decimal id_san_pham, decimal id_dot_km)
        {
            int result = 0;
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ngay_bat_dau = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_BAT_DAU).First();
                var ngay_ket_thuc = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_KET_THUC).First();
                var list_hoa_don = context.GD_HOA_DON.Where(s => s.THOI_GIAN_TAO <= ngay_ket_thuc & s.THOI_GIAN_TAO >= ngay_bat_dau).Select(s => s.ID).ToList();
                foreach (var item in list_hoa_don)
                {
                    var chi_tiet = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == item & s.ID_HANG_HOA == id_san_pham).ToList();
                    foreach (var tt in chi_tiet)
                    {
                        result += int.Parse(tt.SO_LUONG.ToString());
                    }
                }
            }
            return result;
        }

        public static int get_luot_xem_san_pham_trong_dot_km(decimal id_san_pham, decimal id_dot_km)
        {
            int result = 0;
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ngay_bat_dau = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_BAT_DAU).First();
                var ngay_ket_thuc = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_KET_THUC).First();
                result = context.GD_CLICK_HANG_HOA.Where(s => s.THOI_GIAN <= ngay_ket_thuc & s.THOI_GIAN >= ngay_bat_dau & s.ID_HANG_HOA == id_san_pham).ToList().Count;
            }
            return result;
        }

        public static int get_luot_mua_san_pham_trong_dot_km(decimal id_san_pham, decimal id_dot_km)
        {
            int result = 0;
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ngay_bat_dau = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_BAT_DAU).First();
                var ngay_ket_thuc = context.GD_KHUYEN_MAI.Where(s => s.ID == id_dot_km).Select(s => s.THOI_GIAN_KET_THUC).First();
                var hd = context.GD_HOA_DON.Where(s => s.THOI_GIAN_TAO <= ngay_ket_thuc & s.THOI_GIAN_TAO >= ngay_bat_dau).Select(s => s.ID).ToList();
                foreach (var item in hd)
                {
                    var ct = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == item).ToList();
                    foreach (var tt in ct)
                    {
                        if (tt.ID_HANG_HOA == id_san_pham)
                        {
                            result += 1;
                        }
                    }
                }
            }
            return result;
        }

        public static List<decimal> danh_sach_id_cac_dot_khuyen_mai_cua_san_pham(decimal id_san_pham)
        {
            List<decimal> result = new List<decimal>();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                result = context.GD_KHUYEN_MAI_CHI_TIET.Where(s => s.ID_HANG_HOA == id_san_pham).Select(s => s.ID_KHUYEN_MAI).ToList();
            }
            return result;
        }

        #endregion

        #endregion
    }

}
