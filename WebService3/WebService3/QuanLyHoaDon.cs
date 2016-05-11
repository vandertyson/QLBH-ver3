using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WebService3
{
    public class QuanLyHoaDon
    {
        #region Struct
        public class CuaHang
        {
            public string ten_cua_hang { get; set; }
            public string dia_chi { get; set; }
            public string so_dien_thoai { get; set; }
        }
        public class HangHoa
        {
            public string ma_hang_hoa { get; set; }
            public string ten_hang_hoa { get; set; }
            public decimal gia_hien_tai { get; set; }
            public List<string> link_anh { get; set; }
            public List<SizeSoLuongHienTai> san_co { get; set; }
            public List<KhuyenMaiDangApDung> km_dang_ap_ung { get; set; }
        }
        public class SizeSoLuongHienTai
        {
            public string ten_size { get; set; }
            public int so_luong { get; set; }
        }
        public class KhuyenMaiDangApDung
        {
            public string ma_dot_khuyen_mai { get; set; }
            public string mo_ta { get; set; }
            public decimal muc_khuyen_mai { get; set; }
        }
        public class KhachHang
        {
            public string tai_khoan { get; set; }
            public string ten_khach_hang { get; set; }
            public string so_dien_thoai { get; set; }
            public string email { get; set; }
            public decimal diem_giam_tru { get; set; }
        }
        public class HoaDonChiTiet
        {
            public HangHoa hang { get; set; }
            public string ten_size { get; set; }
            public int so_luong { get; set; }
            public decimal gia_ban { get; set; }
        }
        public class HoaDon
        {
            public string ma_hoa_don { get; set; }
            public DateTime thoi_gian_tao { get; set; }
            public CuaHang cua_hang { get; set; }
            public KhachHang khach { get; set; }
            public string loai_thanh_toan { get; set; }
            public decimal giam_tru { get; set; }
            public List<HoaDonChiTiet> chi_tiet { get; set; }
            public decimal tong_gia_tri_hoa_don { get; set; }
            public decimal tong_tien_giam_tru_km { get; set; }
        }
        #endregion

        #region Function
        public static List<KhachHang> get_danh_sach_khach_hang(DateTime ngay)
        {
            List<KhachHang> result = new List<KhachHang>();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var list_khach = context.DM_KHACH_HANG.Where(s => s.NGAY_THAM_GIA <= ngay).ToList();
                foreach (var khach in list_khach)
                {
                    KhachHang cus = new KhachHang();
                    var acc = context.DM_TAI_KHOAN.Where(s => s.ID == khach.ID_TAI_KHOAN).First();
                    cus.ten_khach_hang = acc.HO_DEM + " " + acc.TEN;
                    cus.so_dien_thoai = khach.SO_DIEN_THOAI;
                    cus.email = acc.EMAIL;
                    cus.tai_khoan = acc.TEN_TAI_KHOAN;
                    result.Add(cus);
                }
            }
            return result;
        }
        public static List<HangHoa> get_danh_sach_hang_hien_tai(decimal id_cua_hang, DateTime ngay_hien_tai)
        {
            List<HangHoa> result = new List<HangHoa>();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ds_hang = context.DM_HANG_HOA.Where(s => s.DA_XOA == "N").ToList();
                foreach (var hang in ds_hang)
                {
                    HangHoa sp = new HangHoa();
                    sp.ten_hang_hoa = hang.TEN_HANG_HOA;
                    sp.ma_hang_hoa = hang.MA_TRA_CUU;
                    sp.gia_hien_tai = context.GD_GIA.Where(s => s.ID_HANG_HOA == hang.ID).OrderByDescending(s => s.NGAY_LUU_HANH).First().GIA;

                    //
                    sp.link_anh = new List<string>();
                    foreach (var link in context.DM_LINK_ANH.Where(s => s.ID_HANG_HOA == hang.ID).ToList())
                    {
                        sp.link_anh.Add(link.LINK_ANH);
                    }

                    //
                    sp.san_co = new List<SizeSoLuongHienTai>();
                    foreach (var ton_kho in context.GD_TON_KHO.Where(s => s.ID_HANG_HOA == hang.ID & s.ID_CUA_HANG == id_cua_hang))
                    {
                        if (ton_kho.SO_LUONG_TON_KHO != 0)
                        {
                            SizeSoLuongHienTai ssl = new SizeSoLuongHienTai();
                            ssl.ten_size = context.GD_TAG.Where(s => s.ID == ton_kho.ID_SIZE).First().TEN_TAG;
                            ssl.so_luong = int.Parse(ton_kho.SO_LUONG_TON_KHO.ToString());
                        }
                    }
                    //
                    sp.km_dang_ap_ung = new List<KhuyenMaiDangApDung>();
                    foreach (var km in context.GD_KHUYEN_MAI.Where(s => s.THOI_GIAN_BAT_DAU <= ngay_hien_tai & ngay_hien_tai < s.THOI_GIAN_KET_THUC).ToList())
                    {
                        foreach (var kmct in context.GD_KHUYEN_MAI_CHI_TIET.Where(s => s.ID_KHUYEN_MAI == km.ID).ToList())
                        {
                            if (kmct.ID_HANG_HOA == hang.ID)
                            {
                                KhuyenMaiDangApDung km_ad = new KhuyenMaiDangApDung();
                                km_ad.ma_dot_khuyen_mai = km.MA_DOT;
                                km_ad.mo_ta = km.MO_TA;
                                km_ad.muc_khuyen_mai = kmct.MUC_KHUYEN_MAI;
                                sp.km_dang_ap_ung.Add(km_ad);
                            }
                        }
                    }
                    KhuyenMaiDangApDung ko_km = new KhuyenMaiDangApDung();
                    ko_km.ma_dot_khuyen_mai = "";
                    ko_km.mo_ta = "Không áp dụng khuyến mãi";
                    ko_km.muc_khuyen_mai = 0;
                    sp.km_dang_ap_ung.Add(ko_km);
                    //
                    result.Add(sp);
                }
            }
            return result;
        }
        public static void them_hoa_don(HoaDon hoa_don)
        {

            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var context = new TKHTQuanLyBanHangEntities())
                    {
                        //them hoa don
                        GD_HOA_DON hd = new GD_HOA_DON();
                        hd.ID_CUA_HANG = context.DM_CUA_HANG.Where(s => s.TEN_CUA_HANG == hoa_don.cua_hang.ten_cua_hang).First().ID;
                        hd.ID_TAI_KHOAN = context.DM_TAI_KHOAN.Where(s => s.TEN_TAI_KHOAN == hoa_don.khach.tai_khoan).First().ID;
                        hd.THOI_GIAN_TAO = hoa_don.thoi_gian_tao;
                        hd.MA_HOA_DON = hoa_don.ma_hoa_don;
                        hd.GIAM_TRU = hoa_don.giam_tru;
                        hd.LOAI_THANH_TOAN = hoa_don.loai_thanh_toan;
                        context.GD_HOA_DON.Add(hd);
                        context.SaveChanges();
                        decimal id_hd = context.GD_HOA_DON.Where(s => s.MA_HOA_DON == hoa_don.ma_hoa_don).First().ID;

                        //them hoa don chi tiet
                        foreach (var chi_tiet in hoa_don.chi_tiet)
                        {
                            GD_HOA_DON_CHI_TIET ct = new GD_HOA_DON_CHI_TIET();
                            ct.ID_HOA_DON = id_hd;

                            var id_hang_hoa = context.DM_HANG_HOA.Where(s => s.MA_TRA_CUU == chi_tiet.hang.ma_hang_hoa).First().ID;
                            var id_size = context.GD_TAG.Where(s => s.TEN_TAG == chi_tiet.ten_size).First().ID;

                            ct.ID_HANG_HOA = id_hang_hoa;
                            ct.ID_SIZE = id_size;
                            ct.SO_LUONG = chi_tiet.so_luong;
                            ct.GIA_BAN = chi_tiet.gia_ban;

                            context.GD_HOA_DON_CHI_TIET.Add(ct);

                            //update ton kho
                            var ton_kho = context.GD_TON_KHO.Where(s => s.ID_CUA_HANG == hd.ID_CUA_HANG
                                                                    & s.ID_HANG_HOA == id_hang_hoa
                                                                    & s.ID_SIZE == id_size).First();
                            ton_kho.SO_LUONG_TON_KHO -= chi_tiet.so_luong;

                            //tinh diem cho khach hang

                            context.SaveChanges();
                        }

                        scope.Complete();
                    }
                }
                catch (Exception v_e)
                {
                    scope.Dispose();
                    throw v_e;
                }
            }
        }
        public static List<HoaDon> danh_sach_hoa_don()
        {
            List<HoaDon> result = new List<HoaDon>();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                foreach (var gdhd in context.GD_HOA_DON.ToList())
                {
                    HoaDon hd = new HoaDon();
                    hd.giam_tru = decimal.Parse(gdhd.GIAM_TRU.ToString());
                    hd.thoi_gian_tao = gdhd.THOI_GIAN_TAO;
                    hd.loai_thanh_toan = gdhd.LOAI_THANH_TOAN;
                    hd.ma_hoa_don = gdhd.MA_HOA_DON;

                    hd.cua_hang = new CuaHang();
                    hd.cua_hang.ten_cua_hang = context.DM_CUA_HANG.Where(s => s.ID == gdhd.ID_CUA_HANG).First().TEN_CUA_HANG;
                    hd.cua_hang.dia_chi = context.DM_CUA_HANG.Where(s => s.ID == gdhd.ID_CUA_HANG).First().DIA_CHI;
                    hd.cua_hang.so_dien_thoai = context.DM_CUA_HANG.Where(s => s.ID == gdhd.ID_CUA_HANG).First().ToString();

                    hd.khach = new KhachHang();
                    hd.khach.so_dien_thoai = context.DM_KHACH_HANG.Where(s => s.ID_TAI_KHOAN == gdhd.ID_TAI_KHOAN).First().SO_DIEN_THOAI;
                    hd.khach.email = context.DM_KHACH_HANG.Where(s => s.ID_TAI_KHOAN == gdhd.ID_TAI_KHOAN).First().SO_DIEN_THOAI;
                    hd.khach.ten_khach_hang = context.DM_TAI_KHOAN.Where(s => s.ID == gdhd.ID_TAI_KHOAN).First().HO_DEM
                                            + context.DM_TAI_KHOAN.Where(s => s.ID == gdhd.ID_TAI_KHOAN).First().TEN;
                    hd.khach.diem_giam_tru = context.DM_KHACH_HANG.Where(s => s.ID_TAI_KHOAN == gdhd.ID_TAI_KHOAN).First().DIEM;
                    hd.chi_tiet = new List<HoaDonChiTiet>();
                    foreach (var gdct in context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == gdhd.ID))
                    {
                        HoaDonChiTiet hdct = new HoaDonChiTiet();
                        hdct.hang = new HangHoa();
                        hdct.hang.ten_hang_hoa = context.DM_HANG_HOA.Where(s => s.ID == gdct.ID_HANG_HOA).First().TEN_HANG_HOA;
                        hdct.hang.ma_hang_hoa = context.DM_HANG_HOA.Where(s => s.ID == gdct.ID_HANG_HOA).First().MA_TRA_CUU;
                        hdct.hang.link_anh = context.DM_LINK_ANH.Where(s => s.ID_HANG_HOA == gdct.ID_HANG_HOA).Select(s => s.LINK_ANH).ToList();
                        hdct.ten_size = context.GD_TAG.Where(s => s.ID == gdct.ID_SIZE).First().TEN_TAG;
                        hdct.gia_ban = gdct.GIA_BAN;
                        hdct.so_luong = int.Parse(gdct.SO_LUONG.ToString());
                        hd.chi_tiet.Add(hdct);
                        hd.tong_gia_tri_hoa_don += gdct.SO_LUONG * gdct.GIA_BAN;
                    }
                    hd.tong_tien_giam_tru_km = get_so_tien_giam_tru_km_cua_hoa_don(gdhd.ID);
                    result.Add(hd);
                }
            }
            return result;
        }

        private static decimal get_so_tien_giam_tru_km_cua_hoa_don(decimal iD_hoa_don)
        {
            decimal result = 0;
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var hoa_don = context.GD_HOA_DON.Where(s => s.ID == iD_hoa_don).First();
                var ngay_tao_hoa_don = hoa_don.THOI_GIAN_TAO.Date;
                var km = context.GD_KHUYEN_MAI.Where(s => s.THOI_GIAN_BAT_DAU <= ngay_tao_hoa_don & ngay_tao_hoa_don <= s.THOI_GIAN_KET_THUC).First();
                if (km == null)
                {
                    return result;
                }
                var hoa_don_ct = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == hoa_don.ID).ToList();
                foreach (var ct in hoa_don_ct)
                {
                    var kmct = km.GD_KHUYEN_MAI_CHI_TIET.Where(s => s.ID_HANG_HOA == ct.ID_HANG_HOA).FirstOrDefault();
                    if (kmct == null)
                    {
                        continue;
                    }
                    var gia = context.GD_GIA.Where(s => s.ID_HANG_HOA == ct.ID_HANG_HOA & s.NGAY_LUU_HANH <= ngay_tao_hoa_don)
                                     .OrderByDescending(s => s.NGAY_LUU_HANH).First()
                                     .GIA;
                    var muc_km = kmct.MUC_KHUYEN_MAI;
                    result += ct.SO_LUONG * (gia - ct.GIA_BAN);
                }
            }
            return result;
        }
        #endregion
    }
}