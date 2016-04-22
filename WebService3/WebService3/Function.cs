﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using WebService3.ChiTietHangHoa.BaoCaoPhanHoi;
using WebService3.ChiTietHangHoa.BaoCaoKhuyenMai;
using System.Transactions;
using System.Data;

namespace WebService3
{
    public class Function
    {
        #region Struct dùng chung
        public class ThangNam
        {
            public int thang { get; set; }
            public int nam { get; set; }
        }
        #endregion
        #region Hằng số
        public const string DANH_MUC_SP = @"DANH_MUC_SAN_PHAM";
        public const string SIZE = @"SIZE_QUAN_AO";
        public const string WEB_PATH = @"d:\DZHosts\LocalUser\pagontashika31\www.quanlybanhang.somee.com\";
        public const string WEB_ADDRESS = @"http://quanlybanhang.somee.com/";
        #endregion
        #region Hàm dùng chung
        public static List<ThangNam> lay_cac_thang_tiep_theo(DateTime bat_dau, int so_thang)
        {
            List<ThangNam> result = new List<ThangNam>();
            int thang = bat_dau.Month;
            int nam = bat_dau.Year;
            for (int i = 0; i < so_thang; i++)
            {
                thang += i;
                if (thang % 13 == 0)
                {
                    thang = 1;
                    nam += 1;
                }
                ThangNam p = new ThangNam();
                p.thang = thang;
                p.nam = nam;
                result.Add(p);
            }
            return result;
        }
        public static string upLoadFile(string binary, string file_name)
        {
            var list = new List<byte>();
            foreach (var item in binary.ToArray())
            {
                list.Add(byte.Parse(item.ToString()));
            }
            byte[] bin = list.ToArray();
            var type = file_name.Split('.').Last();
            string path = @"";
            if (type == "jpg" || type == "png")
            {
                path = @"image";
            }
            else if (type == "docx")
            {
                path = @"docx";
            }
            var adr = WEB_PATH + path + @"\" + file_name;
            File.WriteAllBytes(path, bin);
            return WEB_ADDRESS + path + @"/" + file_name;
        }
        #endregion
        #region Quản lý danh mục hàng hóa
        #region Danh mục hàng hóa
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
        //public static List<HangHoaMaster> TimKiemHangHoa(string ma_hang_hoa, string ten_hang_hoa, string list_id_loai_tag)
        //{
        //    using (var context = new TKHTQuanLyBanHangEntities())
        //    {
        //        if (ma_hang_hoa != null && ma_hang_hoa != "")
        //        {
        //            var listHangHoa = context.DM_HANG_HOA.Where(s => s.MA_HANG_HOA == ma_hang_hoa).ToList();
        //            return toListHangHoaMaster(listHangHoa);
        //        }
        //        if (ten_hang_hoa != null && ten_hang_hoa != "")
        //        {
        //            var listHangHoa = context.DM_HANG_HOA.Where(s => s.TEN_HANG_HOA.Contains(ten_hang_hoa)).ToList();
        //            return toListHangHoaMaster(listHangHoa);
        //        }
        //        if (list_id_loai_tag != null && list_id_loai_tag != "")
        //        {
        //            var listHangHoa = new List<DM_HANG_HOA>();
        //            var listTag = Common.TachID(list_id_loai_tag);
        //            listTag = layHetIDTag(listTag);
        //            var dsHangHoa = context.DM_HANG_HOA.ToList();
        //            foreach (var hangHoa in dsHangHoa)
        //            {
        //                var idHangHoa = hangHoa.ID;
        //                var hangHoaTag = context.GD_HANG_HOA_TAG
        //                    .Where(s => s.ID_HANG_HOA == idHangHoa).ToList();
        //                var listHhTag = new List<decimal>();
        //                foreach (var tag in hangHoaTag)
        //                {
        //                    listHhTag.Add(tag.ID_TAG);
        //                }
        //                int flag = 1;
        //                foreach (var tag in listTag)
        //                {
        //                    if (!listHhTag.Contains(tag))
        //                    {
        //                        flag = 0;
        //                        break;
        //                    }
        //                }
        //                if (flag == 0)
        //                {
        //                    continue;
        //                }
        //                listHangHoa.Add(hangHoa);
        //            }
        //            return toListHangHoaMaster(listHangHoa);
        //        }
        //        return null;
        //    }
        //}
        static List<HangHoaMaster> toListHangHoaMaster(List<DM_HANG_HOA> dsHangHoa)
        {
            var listHh = new List<HangHoaMaster>();
            foreach (var item in dsHangHoa)
            {
                listHh.Add(toHangHoaMaster(item));
            }
            return listHh;
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
                var dsTag = context.GD_HANG_HOA_TAG.Where(s => s.ID_HANG_HOA == id).ToList();
                var listTag = new List<Tag>();
                foreach (var tag in dsTag)
                {
                    var t = new Tag();
                    t.id = tag.ID_TAG;
                    t.ten_tag = tag.GD_TAG.TEN_TAG;
                    listTag.Add(t);
                }
                hhMaster.ds_tag = listTag;
                hhMaster.gia = layGia(context, id);
                return hhMaster;
            }
        }

        public static decimal layGia(TKHTQuanLyBanHangEntities context, decimal idHh)
        {
            var gia = context.GD_GIA.Where(s => s.ID_HANG_HOA == idHh)
                .OrderByDescending(s => s.NGAY_LUU_HANH)
                .ToList();
            if (gia.Count == 0)
            {
                return 0;
            }
            return gia[0].GIA;
        }
        //public static List<decimal> layHetIDTag(List<decimal> listTag)
        //{
        //    using (var context = new TKHTQuanLyBanHangEntities())
        //    {
        //        var xet = new List<decimal>();
        //        foreach (var item in listTag)
        //        {
        //            xet.Add(item);
        //        }
        //        while (xet.Count > 0)
        //        {
        //            var id = xet[0];
        //            var tagCon = context.GD_TAG_CHI_TIET.Where(s => s.ID_TAG_CHA == id).ToList();
        //            foreach (var item in tagCon)
        //            {
        //                if (xet.Contains(item.ID_TAG_CON))
        //                {
        //                    continue;
        //                }
        //                xet.Add(item.ID_TAG_CON);
        //                listTag.Add(item.ID_TAG_CON);
        //            }
        //            xet.RemoveAt(0);
        //        }
        //        return listTag;
        //    }
        //}
        public static List<LoaiTag> LayDanhSachTag()
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var dsloaiTag = context.DM_LOAI_TAG.ToList();
                var listLoaiTag = new List<LoaiTag>();
                foreach (var item in dsloaiTag)
                {
                    var loaiTag = new LoaiTag();
                    loaiTag.id = item.ID;
                    loaiTag.ma_loai_tag = item.MA_LOAI_TAG;
                    loaiTag.ten_loai_tag = item.TEN_LOAI_TAG;
                    var listTag = new List<Tag>();
                    foreach (var item2 in item.GD_LOAI_TAG_CHI_TIET)
                    {
                        var tag = new Tag();
                        tag.id = item2.ID;
                        tag.ten_tag = item2.GD_TAG.TEN_TAG;
                        listTag.Add(tag);
                    }
                    loaiTag.ds_tag = listTag;
                    listLoaiTag.Add(loaiTag);

                }
                return listLoaiTag;
            }
        }
        public static HangHoa ChiTietHangHoa(decimal id)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var dsHangHoa = context.DM_HANG_HOA.Where(s => s.ID == id).ToList();
                if (dsHangHoa.Count == 0)
                {
                    return null;
                }
                var hh = dsHangHoa[0];
                var hangHoa = new HangHoa();
                hangHoa.id = hh.ID;
                hangHoa.ma_hang_hoa = hh.MA_HANG_HOA;
                hangHoa.ma_vach = hh.MA_VACH;
                hangHoa.ten = hh.TEN_HANG_HOA;
                hangHoa.gia = layGia(context, id);
                hangHoa.mo_ta = hh.MO_TA;
                var dsDanhGia = context.GD_DANH_GIA
                    .Where(s => s.ID_HANG_HOA == id)
                    .ToList();
                if (dsDanhGia.Count == 0)
                {
                    hangHoa.diem_danh_gia = 0;
                }
                else
                {
                    hangHoa.diem_danh_gia = dsDanhGia.Average(s => s.DIEM);
                }
                hangHoa.luot_xem = context.GD_CLICK_HANG_HOA.Where(s => s.ID_HANG_HOA == id).Count();
                hangHoa.khuyen_mai = 0;
                var toDay = DateTime.Now;
                var khuyenMai = context.GD_KHUYEN_MAI
                    .Where(s => s.THOI_GIAN_BAT_DAU < toDay && toDay < s.THOI_GIAN_KET_THUC).ToList();
                foreach (var km in khuyenMai)
                {
                    var kmai = km.GD_KHUYEN_MAI_CHI_TIET.Where(s => s.ID_HANG_HOA == id).ToList();
                    if (kmai.Count == 0)
                    {
                        continue;
                    }
                    hangHoa.khuyen_mai = kmai[0].MUC_KHUYEN_MAI;
                }
                var nhaCungCap = new NhaCungCap();
                nhaCungCap.id = hh.DM_NHA_CUNG_CAP.ID;
                nhaCungCap.ten = hh.DM_NHA_CUNG_CAP.TEN_NHA_CUNG_CAP;
                nhaCungCap.ten_nguoi_dai_dien = hh.DM_NHA_CUNG_CAP.TEN_NGUOI_DAI_DIEN;
                nhaCungCap.so_dien_thoai = hh.DM_NHA_CUNG_CAP.SO_DIEN_THOAI;
                nhaCungCap.email = hh.DM_NHA_CUNG_CAP.EMAIL;
                hangHoa.nha_cung_cap = nhaCungCap;
                var link = new List<string>();
                foreach (var item in hh.DM_LINK_ANH)
                {
                    link.Add(item.LINK_ANH);
                }
                hangHoa.link_anh = link;
                var dsTag = context.GD_HANG_HOA_TAG.Where(s => s.ID_HANG_HOA == id).ToList();
                var listTag = new List<Tag>();
                foreach (var tag in dsTag)
                {
                    var t = new Tag();
                    t.id = tag.ID_TAG;
                    t.ten_tag = tag.GD_TAG.TEN_TAG;
                    listTag.Add(t);
                }
                hangHoa.ds_tag = listTag;
                var listNhanXet = new List<NhanXet>();
                var dsNhanXet = context.GD_NHAN_XET.Where(s => s.ID_HANG_HOA == id).ToList();
                foreach (var item in dsNhanXet)
                {
                    var nhanXet = new NhanXet();
                    nhanXet.id = item.ID;
                    nhanXet.id_tai_khoan = item.ID_TAI_KHOAN;
                    nhanXet.ten_tai_khoan = item.DM_TAI_KHOAN.TEN_TAI_KHOAN;
                    nhanXet.nhan_xet = item.NHAN_XET;
                    nhanXet.thoi_gian = item.THOI_GIAN;
                    listNhanXet.Add(nhanXet);
                }
                hangHoa.nhan_xet = listNhanXet;
                var listCuaHang = new List<CuaHang>();
                var dsCuaHang = context.DM_CUA_HANG.ToList();
                foreach (var cuaHang in dsCuaHang)
                {
                    var ch = new CuaHang();
                    ch.id = cuaHang.ID;
                    ch.ten_cua_hang = cuaHang.TEN_CUA_HANG;
                    var listSoLuong = new List<SoLuong>();
                    var dsSize = context.DM_LOAI_TAG.Where(s => s.MA_LOAI_TAG == SIZE).First().GD_LOAI_TAG_CHI_TIET;
                    foreach (var item in dsSize)
                    {
                        var soLuong = new SoLuong();
                        var idSize = item.ID;
                        soLuong.id_size = idSize;
                        soLuong.ten_size = item.GD_TAG.TEN_TAG;
                        var sl = context.GD_TON_KHO
                            .Where(s => s.ID_HANG_HOA == id && s.ID_SIZE == idSize)
                            .ToList();
                        if (sl.Count == 0)
                        {
                            soLuong.so_luong = 0;
                        }
                        else
                        {
                            soLuong.so_luong = sl[0].SO_LUONG_TON_KHO;
                        }
                        listSoLuong.Add(soLuong);
                    }
                    ch.ton_kho = listSoLuong;
                    listCuaHang.Add(ch);
                }
                hangHoa.cua_hang = listCuaHang;
                return hangHoa;
            }
        }
        //public static List<HangHoa> DanhSachHangHoa(decimal id_loai_hang)
        //{
        //    using (var context = new TKHTQuanLyBanHangEntities())
        //    {
        //        var ket_qua = new List<HangHoa>();
        //        var ket_qua1 = TimKiemHangHoa("", "", id_loai_hang.ToString());
        //        foreach (var item in ket_qua1)
        //        {
        //            ket_qua.Add(ChiTietHangHoa(item.id));
        //        }
        //        return ket_qua;
        //    }
        //}
        #endregion
        #region Chi tiết hàng hóa - Tình trạng kinh doanh
        public static object tinh_trang_kinh_doanh(decimal id_hang_hoa, DateTime bd, DateTime kt)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ket_qua = new Dictionary<string, object>();
                var list = new List<Dictionary<string, object>>();
                int thang_bd = bd.Month;
                int nam_bd = bd.Year;
                int thang_kt = kt.Month;
                int nam_kt = kt.Year;
                int so_thang = thang_kt - thang_bd + (nam_kt - nam_bd) * 12;
                int nam = nam_bd;
                int thang = thang_bd;
                for (int i = 0; i <= so_thang; i++)
                {
                    list.Add(tinh_trang_kinh_doanh_mat_hang_trong_thang(id_hang_hoa, thang, nam));
                    thang++;
                    if (thang % 13 == 0)
                    {
                        thang = 1;
                        nam += 1;
                    }
                }
                return ket_qua;
            }
        }
        public static Dictionary<string, object> tinh_trang_kinh_doanh_mat_hang_trong_thang(decimal id_hang_hoa, int thang, int nam)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ket_qua = new Dictionary<string, object>();
                ket_qua["thang"] = thang;
                ket_qua["nam"] = nam;
                ket_qua["thong_tin_nhap"] = thong_tin_nhap_xuat_hang_trong_thang(id_hang_hoa, thang, nam, "N");
                ket_qua["thong_tin_xuat"] = thong_tin_nhap_xuat_hang_trong_thang(id_hang_hoa, thang, nam, "X");
                return ket_qua;
            }
        }
        // ham nay can xem lai
        public static object thong_tin_nhap_xuat_hang_trong_thang(decimal id_hang_hoa, int thang, int nam, string nhap_or_xuat)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ket_qua = new Dictionary<string, object>();
                //lay het phieu NHAP trong thang va nam truyen vao
                var p1 = context.GD_PHIEU_NHAP_XUAT.Where(s => s.LOAI_PHIEU == nhap_or_xuat & s.NGAY_NHAP.Month == thang & s.NGAY_NHAP.Year == nam).Select(s => s.ID).ToList();
                //lay het thong tin phieu NHAP trong thang va nam truyen vao va co hang hoa can tim
                var p = context.GD_PHIEU_NHAP_XUAT_CHI_TIET.Where(s => s.ID_HANG_HOA == id_hang_hoa & p1.Contains(s.ID_PHIEU_NHAP_XUAT));
                ket_qua["so_luong"] = p.Sum(s => s.SO_LUONG);
                var gia_nhap_xuat = context.GD_PHIEU_NHAP_CHI_TIET.Where(s => s.ID_HANG_HOA == id_hang_hoa && p1.Contains(s.ID_PHIEU_NHAP_XUAT)).Sum(s => s.GIA_NHAP);
                ket_qua["so_tien"] = p.Sum(s => s.SO_LUONG * gia_nhap_xuat);
                return ket_qua;
            }
        }
        public static PhieuNhapXuat thong_tin_phieu_nhap_xuat(decimal id_phieu)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                PhieuNhapXuat kq = new PhieuNhapXuat();
                var p = context.GD_PHIEU_NHAP_XUAT.Where(s => s.ID == id_phieu).FirstOrDefault();
                kq.id = p.ID;
                kq.loai_phieu = p.LOAI_PHIEU;
                kq.ngay_nhap_xuat = p.NGAY_NHAP;
                kq.ma_phieu = p.MA_PHIEU;
                var p1 = p.GD_PHIEU_NHAP_XUAT_CHI_TIET;
                kq.thong_tin_chi_tiet = new List<HoaDonSimple>();
                foreach (var item in p1)
                {
                    HoaDonSimple hds = new HoaDonSimple();
                    var hangHoa = toHangHoaMaster(item.DM_HANG_HOA);
                    hds.hang_hoa = hangHoa;
                    var idHangHoa = hangHoa.id;
                    hds.gia_ban = context.GD_PHIEU_NHAP_CHI_TIET.Where(s => s.ID_HANG_HOA == idHangHoa && s.ID_PHIEU_NHAP_XUAT == id_phieu).FirstOrDefault().GIA_NHAP;
                    hds.id_size = item.ID_SIZE;
                    hds.so_luong = item.SO_LUONG;
                    kq.thong_tin_chi_tiet.Add(hds);
                }
                return kq;
            }
        }
        public static object ThongTinKinhDoanhHangHoa(decimal id_hang_hoa, DateTime ngayBD, DateTime ngayKT)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var ketQua = new Dictionary<string, object>();
                // gia ban
                var lGia = new List<Dictionary<string, object>>();
                var lSoLuong = new List<Dictionary<string, object>>();
                var gia = context.GD_GIA
                    .Where(s => s.ID_HANG_HOA == id_hang_hoa
                             && s.NGAY_LUU_HANH >= ngayBD
                             && s.NGAY_LUU_HANH < ngayKT)
                    .OrderBy(s => s.NGAY_LUU_HANH)
                    .ToList();
                for (int i = 0; i < gia.Count; i++)
                {
                    var dic = new Dictionary<string, object>();
                    var dic2 = new Dictionary<string, object>();
                    var time1 = gia[i].NGAY_LUU_HANH;
                    var time2 = i + 1 < gia.Count ? gia[i + 1].NGAY_LUU_HANH : DateTime.MaxValue;
                    dic["thoi_gian"] = time1;
                    dic["gia_ban"] = gia[i].GIA;
                    var dsSoLuong = context.GD_HOA_DON_CHI_TIET
                        .Where(s => s.GD_HOA_DON.THOI_GIAN_TAO >= time1
                                && s.GD_HOA_DON.THOI_GIAN_TAO < time2
                                && s.ID_HANG_HOA == id_hang_hoa);
                    decimal soLuong = 0;
                    if (dsSoLuong.Count() > 0)
                    {
                        soLuong = dsSoLuong.Sum(s => s.SO_LUONG);
                    }
                    dic2["thoi_gian"] = time1;
                    dic2["so_luong"] = soLuong;

                    lGia.Add(dic);
                    lSoLuong.Add(dic2);
                }
                ketQua["gia_ban"] = lGia;
                ketQua["so_luong"] = lSoLuong;
                // gia nhap
                var lGiaNhap = new List<Dictionary<string, object>>();
                var dsGiaNhap = context.GD_PHIEU_NHAP_CHI_TIET
                    .Where(s => s.ID_HANG_HOA == id_hang_hoa
                            && s.GD_PHIEU_NHAP_XUAT.NGAY_NHAP >= ngayBD
                            && s.GD_PHIEU_NHAP_XUAT.NGAY_NHAP <= ngayKT)
                    .OrderBy(s => s.GD_PHIEU_NHAP_XUAT.NGAY_NHAP);
                foreach (var item in dsGiaNhap)
                {
                    var dic = new Dictionary<string, object>();
                    dic["thoi_gian"] = item.GD_PHIEU_NHAP_XUAT.NGAY_NHAP;
                    dic["gia_nhap"] = item.GIA_NHAP;
                    dic["gia_nhap_binh_quan"] = item.GIA_NHAP_BINH_QUAN;
                    lGiaNhap.Add(dic);
                }
                ketQua["gia_nhap"] = lGiaNhap;
                return ketQua;
            }
        }
        #endregion
        #region Chi tiết hàng hóa - Phản hồi khách hàng
        public static BaoCaoPhanHoi bao_cao_phan_hoi_khach_hang(DateTime bat_dau, int so_thang, decimal id_hang_hoa)
        {
            BaoCaoPhanHoi result = new BaoCaoPhanHoi();
            result.rating = tinh_rating(id_hang_hoa);
            result.duoc_yeu_thich = so_khach_hang_yeu_thich(id_hang_hoa);
            var p = lay_cac_thang_tiep_theo(bat_dau, so_thang);
            foreach (var item in p)
            {
                result.thong_ke_theo_thang.Add(lay_thong_ke_theo_thang(item.nam, item.thang, id_hang_hoa));
            }
            result.comments = so_luot_comment_den_hien_tai(id_hang_hoa);
            result.views = so_luot_xem_den_thoi_diem_hien_tai(id_hang_hoa);
            return result;
        }
        public static ChiTietHangHoa.BaoCaoPhanHoi.ThongKeTheoThang lay_thong_ke_theo_thang(int thang, int nam, decimal id_hang_hoa)
        {
            ChiTietHangHoa.BaoCaoPhanHoi.ThongKeTheoThang result = new ChiTietHangHoa.BaoCaoPhanHoi.ThongKeTheoThang();
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
                    cm.thoi_gian = item.THOI_GIAN;
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
                var p2 = context.DM_LOAI_TAI_KHOAN.Where(s => s.MA_LOAI == "CUSTOMER").Select(s => s.ID).ToList();
                foreach (var item in p1)
                {
                    var p3 = item.DM_TAI_KHOAN.ID_LOAI_TAI_KHOAN;
                    //Kiem tra nguoi comment la khach hang
                    if (!p2.Contains(p3)) continue;
                    //Lay comment
                    LuotXem lx = new LuotXem();
                    //Lay thong tin comment
                    var p4 = new KhachHang();
                    p4.id = item.DM_TAI_KHOAN.ID;
                    p4.ten_khach_hang = item.DM_TAI_KHOAN.TEN_TAI_KHOAN;
                    //
                    lx.id = item.ID;
                    lx.thoi_gian = item.THOI_GIAN;
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
                var p2 = context.DM_LOAI_TAI_KHOAN.Where(s => s.MA_LOAI == "CUSTOMER").Select(s => s.ID).ToList();
                return p1.Where(s => !p2.Contains(s.DM_TAI_KHOAN.ID_LOAI_TAI_KHOAN)).ToList().Count;
            }
        }
        public static int so_luot_xem_den_thoi_diem_hien_tai(decimal id_hang_hoa)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var p1 = context.GD_CLICK_HANG_HOA.Where(s => s.ID_HANG_HOA == id_hang_hoa).ToList();
                var p2 = context.DM_LOAI_TAI_KHOAN.Where(s => s.MA_LOAI == "CUSTOMER").Select(s => s.ID).ToList();
                return p1.Where(s => !p2.Contains(s.DM_TAI_KHOAN.ID_LOAI_TAI_KHOAN)).ToList().Count;
            }
        }
        #endregion
        #region Chi tiết hàng hóa - Thông tin khuyến mãi
        public static BaoCaoKhuyenMai bao_cao_khuyen_mai_san_pham(decimal id_san_pham, DateTime ngay_hien_tai)
        {
            BaoCaoKhuyenMai result = new BaoCaoKhuyenMai();
            var list_dot_khuyen_mai = danh_sach_id_cac_dot_khuyen_mai_cua_san_pham(id_san_pham);
            foreach (var item in list_dot_khuyen_mai)
            {
                var dot_km = thong_tin_dot_khuyen_mai(id_san_pham, item);
                if (dot_km.thoi_gian_bat_dau <= ngay_hien_tai & dot_km.thoi_gian_ket_thuc >= ngay_hien_tai)
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
                result.thoi_gian_bat_dau = p2.THOI_GIAN_BAT_DAU;
                result.thoi_gian_ket_thuc = p2.THOI_GIAN_KET_THUC;
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
                var list_hoa_don = context.GD_HOA_DON.Where(s => s.THOI_GIAN_TAO <= ngay_ket_thuc & s.THOI_GIAN_TAO >= ngay_bat_dau).Select(s => s.ID).ToList();
                foreach (var item in list_hoa_don)
                {
                    var chi_tiet = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == item).ToList();
                    foreach (var item2 in chi_tiet)
                    {
                        result += item2.SO_LUONG * item2.GIA_BAN;
                    }
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
                var list_hoa_don = context.GD_HOA_DON.Where(s => s.THOI_GIAN_TAO <= ngay_ket_thuc & s.THOI_GIAN_TAO >= ngay_bat_dau).Select(s => s.ID).ToList();
                foreach (var item in list_hoa_don)
                {
                    var chi_tiet = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == item & s.ID_HANG_HOA == id_san_pham).First();
                    result += chi_tiet.SO_LUONG * chi_tiet.GIA_BAN;
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
                    var chi_tiet = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == item & s.ID_HANG_HOA == id_san_pham).First();
                    result += (int)chi_tiet.SO_LUONG;
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
                result = context.GD_HOA_DON.Where(s => s.THOI_GIAN_TAO <= ngay_ket_thuc & s.THOI_GIAN_TAO >= ngay_bat_dau).Select(s => s.ID).ToList().ToList().Count;
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
        #region Cập nhật danh mục hàng hóa
        public static string GenMa(string key,int num,string last)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                string lastMaHangHoa = key;
                for (int i = 0; i < num-1; i++)
                {
                    lastMaHangHoa += "0";
                }
                lastMaHangHoa += "1";
                if (last!=null)
                {
                    lastMaHangHoa = last;
                }
                while (char.IsLetter(lastMaHangHoa[0]))
                {
                    lastMaHangHoa = lastMaHangHoa.Substring(key.Length);
                }
                var iden = (long.Parse(lastMaHangHoa) + 1).ToString();
                string space = "";
                for (int i = 0; i < num - iden.Length; i++)
                {
                    space += "0";
                }
                string hangHoaMoi = key + space + iden;
                return hangHoaMoi;
            }
        }
        public static object ThemHangHoa(
            List<ThemHangHoa> list_hang_hoa)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var context = new TKHTQuanLyBanHangEntities())
                    {
                        var listMaTraCuuSai = new List<string>();
                        var listMaNhaCungCapSai = new List<string>();
                        foreach (var item in list_hang_hoa)
                        {
                            var ma = item.ma_tra_cuu;
                            var temp = context.DM_HANG_HOA.Where(s => s.MA_TRA_CUU == ma).FirstOrDefault();
                            if (temp != null)
                            {
                                listMaTraCuuSai.Add(ma);
                            }
                            var ma2 = item.ma_nha_cung_cap;
                            var temp2 = context.DM_NHA_CUNG_CAP.Where(s => s.MA_NHA_CUNG_CAP == ma2).FirstOrDefault();
                            if (temp2 == null)
                            {
                                listMaNhaCungCapSai.Add(ma2);
                            }
                        }
                        if (listMaNhaCungCapSai.Count > 0 || listMaTraCuuSai.Count > 0)
                        {
                            var dic = new Dictionary<string, object>();
                            dic["ma_tra_cuu_sai"] = listMaTraCuuSai;
                            dic["ma_nha_cung_cap_sai"] = listMaNhaCungCapSai;
                            return dic;
                        }
                        foreach (var item in list_hang_hoa)
                        {
                            var hang = new DM_HANG_HOA();
                            var lastHangHoa = context.DM_HANG_HOA.OrderByDescending(s => s.MA_HANG_HOA).FirstOrDefault();
                            var maCu = lastHangHoa == null ? null : lastHangHoa.MA_HANG_HOA;
                            string ma = GenMa("H",6,maCu);
                            hang.MA_HANG_HOA = ma;
                            hang.TEN_HANG_HOA = item.ten_hang_hoa;
                            var macc = item.ma_nha_cung_cap;
                            var nhaCC = context.DM_NHA_CUNG_CAP.Where(s => s.MA_NHA_CUNG_CAP == macc).First();
                            hang.ID_NHA_CUNG_CAP = nhaCC.ID;
                            hang.MA_TRA_CUU = item.ma_tra_cuu;
                            hang.MO_TA = item.mo_ta;
                            hang.DA_XOA = "N";
                            context.DM_HANG_HOA.Add(hang);
                            context.SaveChanges();
                            var hangHoa = context.DM_HANG_HOA.Where(s => s.MA_HANG_HOA == ma).First();
                            if (item.link_anh != null)
                            {
                                foreach (var link in item.link_anh)
                                {
                                    var linkAnh = new DM_LINK_ANH();
                                    linkAnh.ID_HANG_HOA = hangHoa.ID;
                                    linkAnh.LINK_ANH = link;
                                    context.DM_LINK_ANH.Add(linkAnh);
                                }
                            }
                            if (item.tag != null)
                            {
                                foreach (var tag in item.tag)
                                {
                                    var old = context.GD_TAG.Where(s => s.TEN_TAG == tag).FirstOrDefault();
                                    var hhTag = new GD_HANG_HOA_TAG();
                                    hhTag.ID_HANG_HOA = hangHoa.ID;
                                    if (old != null)
                                    {
                                        hhTag.ID_TAG = old.ID;
                                    }
                                    else
                                    {
                                        var ne = new GD_TAG();
                                        ne.TEN_TAG = tag;
                                        context.GD_TAG.Add(ne);
                                        context.SaveChanges();
                                        var neww = context.GD_TAG.Where(s => s.TEN_TAG == tag).First();
                                        hhTag.ID_TAG = neww.ID;
                                    }
                                    context.GD_HANG_HOA_TAG.Add(hhTag);
                                }
                            }
                            context.SaveChanges();
                        }
                        scope.Complete();
                    }
                    return null;
                }

                catch (Exception v_e)
                {
                    scope.Dispose();
                    throw;
                }



                // }
                //dbContextTransaction.Commit();

                //}
                //catch (Exception e)
                //{
                //    dbContextTransaction.Rollback();
                //    throw e;
                //}
            }


        }
        #endregion
        #endregion
        #region Quản lý bán hàng
        #region Quản lý nhập xuất kho
        public static object ThemPhieuNhapXuat(
           List<PhieuNhap.PhieuNhap> list_phieu_nhap)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var context = new TKHTQuanLyBanHangEntities())
                    {
                        //
                        var gd_phieu_nhap_xuat = new GD_PHIEU_NHAP_XUAT();
                        //

                        foreach (var item in list_phieu_nhap)
                        {
                            //Nhap phieu
                            gd_phieu_nhap_xuat.LOAI_PHIEU = "N";
                            var lastPhieu = context.GD_PHIEU_NHAP_XUAT.OrderByDescending(s => s.MA_PHIEU).FirstOrDefault();
                            var maCu = lastPhieu == null ? null : lastPhieu.MA_PHIEU;
                            gd_phieu_nhap_xuat.MA_PHIEU = GenMa("P",7,maCu);
                            gd_phieu_nhap_xuat.ID_TAI_KHOAN = context.DM_TAI_KHOAN.Where(s => s.TEN_TAI_KHOAN == item.ten_tai_khoan).First().ID;
                            gd_phieu_nhap_xuat.NGAY_NHAP = item.ngay_nhap;
                            context.GD_PHIEU_NHAP_XUAT.Add(gd_phieu_nhap_xuat);
                            context.SaveChanges();

                            //Nhap chi tiet phieu
                            foreach (var item2 in item.list_hang_hoa)
                            {
                                var gd_phieu_nhap_xuat_chi_tiet = new GD_PHIEU_NHAP_XUAT_CHI_TIET();
                                gd_phieu_nhap_xuat_chi_tiet.ID_PHIEU_NHAP_XUAT = gd_phieu_nhap_xuat.ID;
                                gd_phieu_nhap_xuat_chi_tiet.ID_HANG_HOA = context.DM_HANG_HOA.Where(s => s.MA_TRA_CUU == item2.ma_tra_cuu_hang_hoa).First().ID;
                                gd_phieu_nhap_xuat_chi_tiet.ID_SIZE = context.GD_TAG.Where(s => s.TEN_TAG == item2.ten_size).First().ID;
                                gd_phieu_nhap_xuat_chi_tiet.SO_LUONG = item2.so_luong;
                                context.GD_PHIEU_NHAP_XUAT_CHI_TIET.Add(gd_phieu_nhap_xuat_chi_tiet);

                                //Nhap binh quan gia nhap
                                var gd_phieu_nhap_chi_tiet = new GD_PHIEU_NHAP_CHI_TIET();
                                gd_phieu_nhap_chi_tiet.ID_PHIEU_NHAP_XUAT = gd_phieu_nhap_xuat.ID;
                                gd_phieu_nhap_chi_tiet.ID_HANG_HOA = gd_phieu_nhap_xuat_chi_tiet.ID_HANG_HOA;
                                gd_phieu_nhap_chi_tiet.GIA_NHAP = item2.gia_nhap;
                                gd_phieu_nhap_chi_tiet.GIA_NHAP_BINH_QUAN = tinh_gia_nhap_binh_quan(gd_phieu_nhap_chi_tiet.ID_HANG_HOA, item2.gia_nhap);
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
            }
        }

        private static decimal tinh_gia_nhap_binh_quan(decimal id_cua_hang, decimal iD_HANG_HOA, decimal gia_nhap)
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
                var giaBinhQuanCu = context.GD_PHIEU_NHAP_CHI_TIET.Where(s => s.)
            }
            return 0;
        }
        #endregion
        #endregion
        #region Quản lý thành viên
        public static ThanhVien ChiTietThanhVien(decimal id_thanh_vien)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var thanhVien = new ThanhVien();
                var tv = context.DM_KHACH_HANG.Where(s => s.ID == id_thanh_vien).FirstOrDefault();
                if (tv == null)
                {
                    throw new Exception("Không có thành viên này");
                }
                thanhVien.id = tv.ID;
                thanhVien.ho_dem = tv.DM_TAI_KHOAN.HO_DEM;
                thanhVien.ten = tv.DM_TAI_KHOAN.TEN;
                thanhVien.so_dien_thoai = tv.SO_DIEN_THOAI;
                thanhVien.email = tv.DM_TAI_KHOAN.EMAIL;
                thanhVien.lien_lac = tv.LIEN_LAC;
                thanhVien.ngay_gia_nhap = tv.NGAY_THAM_GIA;
                thanhVien.ten_tai_khoan = tv.DM_TAI_KHOAN.TEN_TAI_KHOAN;
                thanhVien.diem = tv.DIEM;
                thanhVien.tong_tien_da_mua = tv.TONG_TIEN_DA_MUA;
                var listHoaDon = new List<HoaDonMaster>();
                var id_tai_khoan = tv.DM_TAI_KHOAN.ID;
                var dsHoaDon = context.GD_HOA_DON
                    .Where(s => s.ID_TAI_KHOAN == id_tai_khoan)
                    .OrderByDescending(s => s.THOI_GIAN_TAO);
                foreach (var item in dsHoaDon)
                {
                    var hoaDon = new HoaDonMaster();
                    hoaDon.id = item.ID;
                    hoaDon.ngay_mua = item.THOI_GIAN_TAO;
                    var dsHangHoa = item.GD_HOA_DON_CHI_TIET;
                    var hd = new List<HoaDonSimple>();
                    foreach (var hh in dsHangHoa)
                    {
                        var hang = new HoaDonSimple();
                        hang.hang_hoa = toHangHoaMaster(hh.DM_HANG_HOA);
                        hang.id_size = hh.ID_SIZE;
                        hang.so_luong = hh.SO_LUONG;
                        hang.gia_ban = hh.GIA_BAN;
                        hd.Add(hang);
                    }
                    hoaDon.hang_hoa = hd;
                    listHoaDon.Add(hoaDon);
                }
                thanhVien.hoa_don = listHoaDon;
                var dsSanPhamUaThich = context.GD_SAN_PHAM_UA_THICH
                    .Where(s => s.ID_TAI_KHOAN == id_tai_khoan);
                var listSput = new List<HangHoaMaster>();
                foreach (var item in dsSanPhamUaThich)
                {
                    var hh = toHangHoaMaster(item.DM_HANG_HOA);
                    listSput.Add(hh);
                }
                thanhVien.san_pham_ua_thich = listSput;
                var dshhDaXem = context.GD_CLICK_HANG_HOA
                    .Where(s => s.ID_TAI_KHOAN == id_tai_khoan)
                    .GroupBy(s => s.ID_HANG_HOA,
                    (key, g) => new
                    {
                        thoi_gian = g.Max(k => k.THOI_GIAN),
                        hang_hoa = g.FirstOrDefault().DM_HANG_HOA,
                        so_click = g.Count()
                    });
                var listDaXem = new List<HangHoaDaXem>();
                foreach (var item in dshhDaXem)
                {
                    var hhDaXem = new HangHoaDaXem();
                    hhDaXem.thoi_gian = item.thoi_gian;
                    hhDaXem.hang_hoa = toHangHoaMaster(item.hang_hoa);
                    hhDaXem.so_click = item.so_click;
                    listDaXem.Add(hhDaXem);
                }
                thanhVien.hang_hoa_da_xem = listDaXem;
                var dsComment = context.GD_NHAN_XET.Where(s => s.ID_TAI_KHOAN == id_tai_khoan);
                var listComment = new List<CommentMaster>();
                foreach (var item in dsComment)
                {
                    var comment = new CommentMaster();
                    comment.hang_hoa = toHangHoaMaster(item.DM_HANG_HOA);
                    comment.thoi_gian = item.THOI_GIAN;
                    comment.comment = item.NHAN_XET;
                    listComment.Add(comment);
                }
                thanhVien.comment = listComment;
                return thanhVien;
            }
        }
        #endregion

    }
}