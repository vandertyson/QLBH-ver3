using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService3
{
    public class QuanLyKhuyenMai
    {
        #region Struct
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
        #region function
        public static List<KhuyenMai> LayThongTinKhuyenMai()
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ds_km = new List<KhuyenMai>();
                var gd_khuyen_mai = context.GD_KHUYEN_MAI.ToList();
                var km = new KhuyenMai();
                foreach (var item in gd_khuyen_mai)
                { 
                    km.id = item.ID;
                    km.ma_dot = item.MA_DOT;
                    km.mo_ta = item.MO_TA;
                    km.tg_bat_dau = item.THOI_GIAN_BAT_DAU;
                    km.tg_ket_thuc = item.THOI_GIAN_KET_THUC;
                    var list_hang = new List<HangKhuyenMai>();
                    var temp = new HangKhuyenMai();
                    var gd_khuyen_mai_chi_tiet = context.GD_KHUYEN_MAI_CHI_TIET.ToList();
                    foreach (var item1 in gd_khuyen_mai_chi_tiet)
                    {
                        temp.id = item1.ID;
                        temp.muc_khuyen_mai = item1.MUC_KHUYEN_MAI;
                        temp.ten_hang_hoa = context.DM_HANG_HOA.Where(s => s.ID == item1.ID_HANG_HOA).First().TEN_HANG_HOA;
                        list_hang.Add(temp);
                        temp = new HangKhuyenMai();
                    }
                    km.ds_hang = list_hang;
                    ds_km.Add(km);
                }
                return ds_km;
            }
        }

        public static void ThemDotKhuyenMai(string ma_dot,string mo_ta,DateTime tg_bat_dau,DateTime tg_ket_thuc)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var dotKhuyenMai = new GD_KHUYEN_MAI();
                dotKhuyenMai.MA_DOT = ma_dot;
                dotKhuyenMai.MO_TA = mo_ta;
                dotKhuyenMai.THOI_GIAN_BAT_DAU = tg_bat_dau;
                dotKhuyenMai.THOI_GIAN_KET_THUC = tg_ket_thuc;
                context.GD_KHUYEN_MAI.Add(dotKhuyenMai);
                context.SaveChanges();
            }
        }

        public static void ThemHangHoaKhuyenMai(string ten_hang_hoa, string ma_dot,decimal muc_km)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var hangHoaKhuyenMai = new GD_KHUYEN_MAI_CHI_TIET();
                hangHoaKhuyenMai.ID_HANG_HOA = context.DM_HANG_HOA.Where(s => s.TEN_HANG_HOA == ten_hang_hoa).First().ID;
                hangHoaKhuyenMai.ID_KHUYEN_MAI = context.GD_KHUYEN_MAI.Where(s => s.MA_DOT == ma_dot).First().ID;
                hangHoaKhuyenMai.MUC_KHUYEN_MAI = muc_km;
                context.GD_KHUYEN_MAI_CHI_TIET.Add(hangHoaKhuyenMai);
                context.SaveChanges();
            }
        }

        public static void SuaMucKhuyenMai(string ten_hang_hoa,string ma_dot,decimal muc_km)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var idhanghoa = context.DM_HANG_HOA.Where(s => s.TEN_HANG_HOA == ten_hang_hoa).First().ID;
                var idkm = context.GD_KHUYEN_MAI.Where(s => s.MA_DOT == ma_dot).First().ID;
                var hangHoaKhuyenMai = context.GD_KHUYEN_MAI_CHI_TIET.Where(s => s.ID_HANG_HOA == idhanghoa).Where(s => s.ID_KHUYEN_MAI == idkm).FirstOrDefault();
                if (hangHoaKhuyenMai!=null)
                {
                    hangHoaKhuyenMai.MUC_KHUYEN_MAI = muc_km;
                    context.SaveChanges();
                }
            }
            
        }
 
        #endregion
    }
}