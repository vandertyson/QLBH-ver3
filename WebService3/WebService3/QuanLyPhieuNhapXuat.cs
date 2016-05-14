using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WebService3
{
    public class QuanLyPhieuNhapXuat
    {
        #region Struct

        public class SizeSL
        {
            public string ten_size { get; set; }
            public int so_luong { get; set; }
        }

        public class HangHoa
        {
            public string ma_tra_cuu_hang_hoa { get; set; }
            public List<SizeSL> size_sl { get; set; }
            public decimal gia_nhap { get; set; }
        }

        public class PhieuNhap
        {
            public string ma_phieu { get; set; }
            public string ngay_nhap { get; set; }
            public string ten_tai_khoan { get; set; }
            public decimal id_cua_hang { get; set; }
            public List<HangHoa> list_hang_hoa { get; set; }
        }


        #endregion

        #region Function
        public static void ThemMotPhieuNhap(PhieuNhap p)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                  new System.TimeSpan(0, 15, 0)))
            {
                try
                {
                    using (var context = new TKHTQuanLyBanHangEntities())
                    {
                        //Nhap phieu
                        var gd_phieu_nhap_xuat = new GD_PHIEU_NHAP_XUAT();
                        gd_phieu_nhap_xuat.LOAI_PHIEU = "N";
                        gd_phieu_nhap_xuat.MA_PHIEU = p.ma_phieu;
                        gd_phieu_nhap_xuat.ID_TAI_KHOAN = context.DM_TAI_KHOAN.Where(s => s.TEN_TAI_KHOAN == p.ten_tai_khoan).First().ID;
                        gd_phieu_nhap_xuat.NGAY_NHAP = Convert.ToDateTime(p.ngay_nhap);
                        gd_phieu_nhap_xuat.ID_CUA_HANG = p.id_cua_hang;
                        context.GD_PHIEU_NHAP_XUAT.Add(gd_phieu_nhap_xuat);
                        context.SaveChanges();
                        var phieuNhapXuat = context.GD_PHIEU_NHAP_XUAT.Where(s => s.MA_PHIEU == p.ma_phieu).First();
                        var id = phieuNhapXuat.ID;
                        //Nhap chi tiet phieu
                        int so_luong = 0;
                        foreach (var item2 in p.list_hang_hoa)
                        {
                            var gd_phieu_nhap_chi_tiet = new GD_PHIEU_NHAP_CHI_TIET();
                            gd_phieu_nhap_chi_tiet.ID_PHIEU_NHAP_XUAT = id;
                            gd_phieu_nhap_chi_tiet.ID_HANG_HOA = context.DM_HANG_HOA.Where(s => s.MA_TRA_CUU == item2.ma_tra_cuu_hang_hoa).First().ID;
                            var giaNhap = item2.gia_nhap;
                            gd_phieu_nhap_chi_tiet.GIA_NHAP = giaNhap;
                            so_luong = item2.size_sl.Sum(s => s.so_luong);
                            gd_phieu_nhap_chi_tiet.GIA_NHAP_BINH_QUAN = tinh_gia_nhap_binh_quan(p.id_cua_hang, gd_phieu_nhap_chi_tiet.ID_HANG_HOA, giaNhap, so_luong);
                            context.GD_PHIEU_NHAP_CHI_TIET.Add(gd_phieu_nhap_chi_tiet);
                            foreach (var item3 in item2.size_sl)
                            {
                                var gd_phieu_nhap_xuat_chi_tiet = new GD_PHIEU_NHAP_XUAT_CHI_TIET();
                                gd_phieu_nhap_xuat_chi_tiet.ID_PHIEU_NHAP_XUAT = id;
                                var id_hang_hoa = context.DM_HANG_HOA.Where(s => s.MA_TRA_CUU == item2.ma_tra_cuu_hang_hoa).First().ID;
                                gd_phieu_nhap_xuat_chi_tiet.ID_HANG_HOA = id_hang_hoa;
                                var id_size = context.GD_TAG.Where(s => s.TEN_TAG == item3.ten_size).First().ID;
                                gd_phieu_nhap_xuat_chi_tiet.ID_SIZE = id_size;
                                gd_phieu_nhap_xuat_chi_tiet.SO_LUONG = item3.so_luong;
                                context.GD_PHIEU_NHAP_XUAT_CHI_TIET.Add(gd_phieu_nhap_xuat_chi_tiet);

                                // Nhập tồn kho
                                var tonKho = context.GD_TON_KHO
                                    .Where(s => s.ID_CUA_HANG == p.id_cua_hang && s.ID_HANG_HOA == id_hang_hoa && s.ID_SIZE == id_size)
                                    .FirstOrDefault();
                                if (tonKho == null)
                                {
                                    var gdTonKho = new GD_TON_KHO();
                                    gdTonKho.ID_CUA_HANG = p.id_cua_hang;
                                    gdTonKho.ID_HANG_HOA = id_hang_hoa;
                                    gdTonKho.ID_SIZE = id_size;
                                    gdTonKho.SO_LUONG_TON_KHO = item3.so_luong;
                                    context.GD_TON_KHO.Add(gdTonKho);
                                }
                                else
                                {
                                    tonKho.SO_LUONG_TON_KHO += item3.so_luong;
                                }

                                context.SaveChanges();
                            }
                        }

                    }
                    scope.Complete();
                }
                catch (Exception v_e)
                {
                    scope.Dispose();
                    throw v_e;
                }
                finally
                {

                }
            }
        }
        public static object ThemPhieuNhapXuat(
          List<PhieuNhap> list_phieu_nhap)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                   new System.TimeSpan(0, 15, 0)))
            {
                try
                {
                    using (var context = new TKHTQuanLyBanHangEntities())
                    {
                        foreach (var item in list_phieu_nhap)
                        {
                            //Nhap phieu
                            var gd_phieu_nhap_xuat = new GD_PHIEU_NHAP_XUAT();
                            gd_phieu_nhap_xuat.LOAI_PHIEU = "N";
                            var lastPhieu = context.GD_PHIEU_NHAP_XUAT.OrderByDescending(s => s.MA_PHIEU).FirstOrDefault();
                            var maCu = lastPhieu == null ? null : lastPhieu.MA_PHIEU;
                            var maMoi = Common.GenMa("P", 7, maCu);
                            gd_phieu_nhap_xuat.MA_PHIEU = maMoi;
                            gd_phieu_nhap_xuat.ID_TAI_KHOAN = context.DM_TAI_KHOAN.Where(s => s.TEN_TAI_KHOAN == item.ten_tai_khoan).First().ID;
                            gd_phieu_nhap_xuat.NGAY_NHAP = Convert.ToDateTime(item.ngay_nhap);
                            var id_cua_hang = item.id_cua_hang;
                            gd_phieu_nhap_xuat.ID_CUA_HANG = id_cua_hang;
                            context.GD_PHIEU_NHAP_XUAT.Add(gd_phieu_nhap_xuat);
                            context.SaveChanges();
                            var phieuNhapXuat = context.GD_PHIEU_NHAP_XUAT.Where(s => s.MA_PHIEU == maMoi).First();
                            var id = phieuNhapXuat.ID;
                            //Nhap chi tiet phieu
                            int so_luong = 0;
                            foreach (var item2 in item.list_hang_hoa)
                            {
                                var gd_phieu_nhap_chi_tiet = new GD_PHIEU_NHAP_CHI_TIET();
                                gd_phieu_nhap_chi_tiet.ID_PHIEU_NHAP_XUAT = id;
                                gd_phieu_nhap_chi_tiet.ID_HANG_HOA = context.DM_HANG_HOA.Where(s => s.MA_TRA_CUU == item2.ma_tra_cuu_hang_hoa).First().ID;
                                var giaNhap = item2.gia_nhap;
                                gd_phieu_nhap_chi_tiet.GIA_NHAP = giaNhap;
                                so_luong = item2.size_sl.Sum(s => s.so_luong);
                                gd_phieu_nhap_chi_tiet.GIA_NHAP_BINH_QUAN = tinh_gia_nhap_binh_quan(item.id_cua_hang, gd_phieu_nhap_chi_tiet.ID_HANG_HOA, giaNhap, so_luong);
                                context.GD_PHIEU_NHAP_CHI_TIET.Add(gd_phieu_nhap_chi_tiet);
                                foreach (var item3 in item2.size_sl)
                                {
                                    var gd_phieu_nhap_xuat_chi_tiet = new GD_PHIEU_NHAP_XUAT_CHI_TIET();
                                    gd_phieu_nhap_xuat_chi_tiet.ID_PHIEU_NHAP_XUAT = id;
                                    var id_hang_hoa = context.DM_HANG_HOA.Where(s => s.MA_TRA_CUU == item2.ma_tra_cuu_hang_hoa).First().ID;
                                    gd_phieu_nhap_xuat_chi_tiet.ID_HANG_HOA = id_hang_hoa;
                                    var id_size = context.GD_TAG.Where(s => s.TEN_TAG == item3.ten_size).First().ID;
                                    gd_phieu_nhap_xuat_chi_tiet.ID_SIZE = id_size;
                                    gd_phieu_nhap_xuat_chi_tiet.SO_LUONG = item3.so_luong;
                                    context.GD_PHIEU_NHAP_XUAT_CHI_TIET.Add(gd_phieu_nhap_xuat_chi_tiet);

                                    // Nhập tồn kho
                                    var tonKho = context.GD_TON_KHO
                                        .Where(s => s.ID_CUA_HANG == id_cua_hang && s.ID_HANG_HOA == id_hang_hoa && s.ID_SIZE == id_size)
                                        .FirstOrDefault();
                                    if (tonKho == null)
                                    {
                                        var gdTonKho = new GD_TON_KHO();
                                        gdTonKho.ID_CUA_HANG = id_cua_hang;
                                        gdTonKho.ID_HANG_HOA = id_hang_hoa;
                                        gdTonKho.ID_SIZE = id_size;
                                        gdTonKho.SO_LUONG_TON_KHO = item3.so_luong;
                                        context.GD_TON_KHO.Add(gdTonKho);
                                    }
                                    else
                                    {
                                        tonKho.SO_LUONG_TON_KHO += item3.so_luong;
                                    }

                                    context.SaveChanges();
                                }
                            }
                        }
                    }
                    scope.Complete();
                    return null;
                }
                catch (Exception v_e)
                {
                    scope.Dispose();
                    throw v_e;
                }
                finally
                {

                }
            }
        }

        private static decimal tinh_gia_nhap_binh_quan(decimal id_cua_hang, decimal iD_HANG_HOA, decimal gia_nhap, int slNhap)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var temp = context.GD_PHIEU_NHAP_CHI_TIET
                    .Where(s => s.ID_HANG_HOA == iD_HANG_HOA).FirstOrDefault();
                if (temp == null)
                {
                    return gia_nhap;
                }
                var slDu = context.GD_TON_KHO
                    .Where(s => s.ID_CUA_HANG == id_cua_hang && s.ID_HANG_HOA == iD_HANG_HOA)
                    .Sum(s => s.SO_LUONG_TON_KHO);
                var giaBinhQuanCu = context.GD_PHIEU_NHAP_CHI_TIET
                    .Where(s => s.GD_PHIEU_NHAP_XUAT.ID_CUA_HANG == id_cua_hang && s.ID_HANG_HOA == iD_HANG_HOA)
                    .OrderByDescending(s => s.GD_PHIEU_NHAP_XUAT.NGAY_NHAP)
                    .FirstOrDefault()
                    .GIA_NHAP_BINH_QUAN;
                return (giaBinhQuanCu * slDu + gia_nhap * slNhap) / (slNhap + slDu);
            }
        }

        public static List<PhieuNhap> lay_danh_sach_phieu_nhap(DateTime nbd, DateTime nkt)
        {
            List<PhieuNhap> result = new List<PhieuNhap>();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var list_phieu = context.GD_PHIEU_NHAP_XUAT.Where(s => s.NGAY_NHAP >= nbd & s.NGAY_NHAP <= nkt).ToList();
                foreach (var phieu in list_phieu)
                {
                    PhieuNhap p = new PhieuNhap();
                    p.ma_phieu = phieu.MA_PHIEU;
                    p.ngay_nhap = phieu.NGAY_NHAP.ToString();
                    p.id_cua_hang = phieu.ID_CUA_HANG;
                    p.ten_tai_khoan = context.DM_TAI_KHOAN.Where(s => s.ID == phieu.ID_TAI_KHOAN).First().TEN_TAI_KHOAN;
                    p.list_hang_hoa = new List<HangHoa>();

                    var ds_hang = context.GD_PHIEU_NHAP_CHI_TIET.Where(s => s.ID_PHIEU_NHAP_XUAT == phieu.ID).Select(s => s.ID_HANG_HOA).Distinct().ToList();
                    //
                    foreach (var item in ds_hang)
                    {
                        HangHoa hang = new HangHoa();
                        hang.ma_tra_cuu_hang_hoa = context.DM_HANG_HOA.Where(s => s.ID == item).First().MA_TRA_CUU;
                        hang.gia_nhap = context.GD_PHIEU_NHAP_CHI_TIET.Where(s => s.ID_HANG_HOA == item & s.ID_PHIEU_NHAP_XUAT == phieu.ID).First().GIA_NHAP;
                        hang.size_sl = new List<SizeSL>();

                        var cx = context.GD_PHIEU_NHAP_XUAT_CHI_TIET.Where(s => s.ID_HANG_HOA == item & s.ID_PHIEU_NHAP_XUAT == phieu.ID).ToList();
                        foreach (var ssl in cx)
                        {
                            SizeSL size = new SizeSL();
                            size.so_luong = Convert.ToInt32(ssl.SO_LUONG);
                            size.ten_size = context.GD_TAG.Where(s => s.ID == ssl.ID_SIZE).First().TEN_TAG;
                            hang.size_sl.Add(size);
                        }
                        p.list_hang_hoa.Add(hang);
                    }
                    result.Add(p);
                }
            }
            return result.OrderByDescending(s => s.ngay_nhap).ToList();
        }

        public static void sua_phieu_nhap_xuat(PhieuNhap phieu_nhap)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                   new System.TimeSpan(0, 15, 0)))
            {
                try
                {
                    using (var context = new TKHTQuanLyBanHangEntities())
                    {
                        var id_phieu_nhap_xuat = context.GD_PHIEU_NHAP_XUAT.Where(s => s.MA_PHIEU == phieu_nhap.ma_phieu).FirstOrDefault();
                        xoa_phieu_nhap_xuat(phieu_nhap.ma_phieu);
                        ThemMotPhieuNhap(phieu_nhap);
                        context.SaveChanges();
                        scope.Complete();
                    }
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }

        public static void xoa_phieu_nhap_xuat(string ma_phieu)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                   new System.TimeSpan(0, 15, 0)))
            {
                try
                {
                    using (var context = new TKHTQuanLyBanHangEntities())
                    {
                        var phieu = context.GD_PHIEU_NHAP_XUAT.Where(s => s.MA_PHIEU == ma_phieu).FirstOrDefault();

                        //xoa chi tiet
                        var ct = context.GD_PHIEU_NHAP_XUAT_CHI_TIET.Where(s => s.ID_PHIEU_NHAP_XUAT == phieu.ID).ToList();
                        foreach (var item in ct)
                        {
                            context.GD_PHIEU_NHAP_XUAT_CHI_TIET.Remove(item);
                        }
                        //xoa chi tiet nhap
                        var ctn = context.GD_PHIEU_NHAP_CHI_TIET.Where(s => s.ID_PHIEU_NHAP_XUAT == phieu.ID).ToList();
                        foreach (var item in ctn)
                        {
                            context.GD_PHIEU_NHAP_CHI_TIET.Remove(item);
                        }
                        context.SaveChanges();
                        context.GD_PHIEU_NHAP_XUAT.Remove(phieu);
                        context.SaveChanges();
                        scope.Complete();
                    }
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }

        public static string lay_ma_phieu_nhap_moi()
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var last_hd = context.GD_PHIEU_NHAP_XUAT.OrderByDescending(s => s.ID).First();
                if (last_hd == null)
                {
                    return "P0000001";
                }
                return Common.GenMa("P", 7, last_hd.MA_PHIEU);
            }
        }
        #endregion
    }
}