using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
namespace WebService3
{
    public class QuanLyDanhMucHangHoa
    {
        #region Constant
        public const string DANH_MUC_SP = @"DANH_MUC_SAN_PHAM";
        public const string SIZE = @"SIZE_QUAN_AO";
    
        #endregion

        #region Struct
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
        public class NhaCungCapV2
        {
            public string ma_nha_cung_cap { get; set; }
            public string ten_nha_cung_cap { get; set; }
            public string dia_chi { get; set; }
        }
        public class HangHoaVaMa
        {
            public string ten_hang_hoa { get; set; }
            public string ma_tra_cuu { get; set; }
        }

        #endregion

        #region Functions
        public static List<LoaiHang> DanhMucLoaiHang()
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var idLoaiTag = context.DM_LOAI_TAG.Where(s => s.MA_LOAI_TAG == DANH_MUC_SP).First().ID;
                var listDs = new List<LoaiHang>();
                var dsLoaiHang = context.GD_LOAI_TAG_CHI_TIET.Where(s => s.ID_LOAI_TAG == idLoaiTag).ToList();
                foreach (var item in dsLoaiHang)
                {
                    var loaiHang = new LoaiHang();
                    loaiHang.id = item.ID;
                    loaiHang.ten_tag = item.GD_TAG.TEN_TAG;
                    loaiHang.link_anh = item.GD_TAG.LINK_ANH;
                    listDs.Add(loaiHang);
                }
                return listDs;
            }
        }

        public static List<HangHoaMaster> TimKiemHangHoa(string keyword, int length, int page)
        {
            keyword = keyword.ToLower();

            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var listTag = context.GD_TAG.Where(s => keyword.Contains(s.TEN_TAG)).ToArray();

                var listHH = context.DM_HANG_HOA.ToArray();
                var listKey = keyword.Split(' ').ToList();
                var listID = new List<DM_HANG_HOA>();
                foreach (var hang in listHH)
                {
                    if (hang.MA_TRA_CUU.ToLower() == keyword)
                    {
                        listID.Add(hang);
                    }
                    else if (hang.MA_HANG_HOA.ToLower() == keyword)
                    {
                        listID.Add(hang);
                    }
                    else if (kiemTraNamTrong(listKey, hang.TEN_HANG_HOA.ToLower().Split(' ').ToList(), (a, b) =>
                               {
                                   return a == b;
                               }))
                    {
                        listID.Add(hang);
                    }
                    else
                    {
                        var lTag = hang.GD_HANG_HOA_TAG.Select(s => s.GD_TAG.TEN_TAG.ToLower()).ToList();
                        if (
                            kiemTraNamTrong(new List<string> { keyword },lTag, (a, b) =>
                             {
                                 return a.Contains(b);
                             })
                            )
                        {
                            listID.Add(hang);
                        }
                    }
                }
                var ketQua = new List<HangHoaMaster>();
                for (int i = length*page; i < length*page + length&&i<listID.Count; i++)
                {
                    ketQua.Add(toHangHoaMaster(listID[i]));
                }
                return ketQua;
            }
        }

        delegate bool bangNhau<T>(T t1, T t2);

        static bool kiemTraNamTrong<T>(List<T> list1, List<T> list2, bangNhau<T> bangNhau)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                bool boo = false;
                for (int j = 0; j < list2.Count; j++)
                {
                    if (bangNhau(list1[i], list2[j]))
                    {
                        boo = true;
                        break;
                    }
                }
                if (!boo)
                {
                    return false;
                }
            }
            return true;
        }

        static HangHoaMaster toHangHoaMaster(DM_HANG_HOA hh)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var hhMaster = new HangHoaMaster();
                var id = hh.ID;
                hhMaster.id = id;
                hhMaster.ma_hang_hoa = hh.MA_HANG_HOA;
                hhMaster.ten_hang_hoa = hh.TEN_HANG_HOA;
                hhMaster.nha_san_xuat = hh.DM_NHA_CUNG_CAP.TEN_NHA_CUNG_CAP;
                var listLink = new List<string>();
                var dsLink = context.DM_LINK_ANH.Where(s => s.ID_HANG_HOA == id).ToList();
                foreach (var link in dsLink)
                {
                    listLink.Add(link.LINK_ANH);
                }
                hhMaster.ds_link = listLink;
                var gia = layGia(context, id);
                hhMaster.gia = gia.Count==0?0:gia[0];
                hhMaster.do_giam_gia = (float)(gia.Count < 2 ? 0 : 1 - gia[0] / gia[1]);
                var diem = hh.GD_DANH_GIA.Where(s => s.ID_HANG_HOA == id).ToArray();
                hhMaster.diem = diem.Count() > 0 ? diem.Average(s => s.DIEM) : 0;
                hhMaster.ma_tra_cuu = hh.MA_TRA_CUU;
                hhMaster.so_comment = hh.GD_NHAN_XET.Where(s => s.ID_HANG_HOA == id).Count();
                hhMaster.luot_click = hh.GD_CLICK_HANG_HOA.Where(s => s.ID_HANG_HOA == id).Count();
                return hhMaster;
            }
        }

        internal static List<NhaCungCapV2> LayDanhSachNhaCungCap()
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                List<NhaCungCapV2> rsul = new List<NhaCungCapV2>();
                var ncc = context.DM_NHA_CUNG_CAP.ToList();
                foreach (var item in ncc)
                {
                    NhaCungCapV2 cc = new NhaCungCapV2();
                    cc.dia_chi = item.DIA_CHI;
                    cc.ma_nha_cung_cap = item.MA_NHA_CUNG_CAP;
                    cc.ten_nha_cung_cap = item.TEN_NHA_CUNG_CAP;
                    rsul.Add(cc);
                }
                return rsul;
            }
        }

        public static List<decimal> layGia(TKHTQuanLyBanHangEntities context, decimal idHh)
        {
            var gia = context.GD_GIA.Where(s => s.ID_HANG_HOA == idHh)
                .OrderByDescending(s => s.NGAY_LUU_HANH)
                .Take(2)
                .Select(s=>s.GIA)
                .ToList();
            return gia;
        }

        public static void Test3(string keyword,decimal id_tag)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var context = new TKHTQuanLyBanHangEntities())
                    {
                        if (keyword=="namnu")
                        {
                            var hh = context.DM_HANG_HOA.ToArray();
                            foreach (var item in hh)
                            {
                                if (item.ID<45)
                                {
                                    var hhtag = context.GD_HANG_HOA_TAG
                                .Where(s => s.ID_HANG_HOA == item.ID && s.ID_TAG==2)
                                .FirstOrDefault();
                                    if (hhtag == null)
                                    {
                                        var hangtag = new GD_HANG_HOA_TAG();
                                        hangtag.ID_HANG_HOA = item.ID;
                                        hangtag.ID_TAG = 2;
                                        context.GD_HANG_HOA_TAG.Add(hangtag);
                                    }
                                }
                                else
                                {
                                    var hhtag = context.GD_HANG_HOA_TAG
                                .Where(s => s.ID_HANG_HOA == item.ID && s.ID_TAG == 1)
                                .FirstOrDefault();
                                    if (hhtag == null)
                                    {
                                        var hangtag = new GD_HANG_HOA_TAG();
                                        hangtag.ID_HANG_HOA = item.ID;
                                        hangtag.ID_TAG = 1;
                                        context.GD_HANG_HOA_TAG.Add(hangtag);
                                    }
                                }
                            }
                            context.SaveChanges();
                            scope.Complete();
                            return;
                        }
                        var list = TimKiemHangHoa(keyword, 100, 0);
                        foreach (var item in list)
                        {
                            var hh = context.DM_HANG_HOA.Where(s => s.ID == item.id).First();
                            var hhtag = context.GD_HANG_HOA_TAG
                                .Where(s => s.ID_HANG_HOA == item.id && s.ID_TAG == id_tag)
                                .FirstOrDefault();
                            if (hhtag==null)
                            {
                                var hangtag = new GD_HANG_HOA_TAG();
                                hangtag.ID_HANG_HOA = item.id;
                                hangtag.ID_TAG = id_tag;
                                context.GD_HANG_HOA_TAG.Add(hangtag);
                            }
                        }
                        context.SaveChanges();
                        scope.Complete();
                    }

                }
                catch (Exception e)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }

        public static List<HangHoaVaMa> lay_danh_sach_hang_va_ma_tra_cuu()
        {
            List<HangHoaVaMa> result = new List<HangHoaVaMa>();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var hanghoa = context.DM_HANG_HOA.Where(s => s.DA_XOA == "N").ToList();
                foreach (var item in hanghoa)
                {
                    var temp = new HangHoaVaMa();
                    temp.ma_tra_cuu = item.MA_TRA_CUU;
                    temp.ten_hang_hoa = item.TEN_HANG_HOA;
                    result.Add(temp);
                }
                return result;
            }
        }

        #endregion
    }
}