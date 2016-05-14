using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WebService3
{
    public class BaoCaoKinhDoanh
    {
        #region struct
        public class BaoCaoDoanhThuDoanhSo
        {
            public decimal nam { get; set; }
            public decimal thang { get; set; }
            public decimal tong_doanh_so { get; set; }
            public decimal tong_doanh_thu { get; set; }
        }
        #endregion
        #region function
        public static List<BaoCaoDoanhThuDoanhSo> get_doanh_thu_doanh_so_theo_thang(string thang_bd,string thang_kt)
        {
            var danh_sach_bao_cao = new List<BaoCaoDoanhThuDoanhSo>();
            using (var context = new TKHTQuanLyBanHangEntities())
            {
 
                int thang_tinh = Convert.ToDateTime(thang_bd).Month;
                int nam_tinh = Convert.ToDateTime(thang_bd).Year;
                do
                {
                    decimal tien = 0;
                    decimal slsp = 0;
                    var bao_cao = new BaoCaoDoanhThuDoanhSo();
                    var table = context.GD_HOA_DON.Where(s => s.THOI_GIAN_TAO.Month == thang_tinh).Where(s => s.THOI_GIAN_TAO.Year == nam_tinh).ToList();
                    foreach (var item in table)
                    {
                        decimal temp_tien = 0;
                        decimal temp_slsp = 0;
                        var table_chi_tiet = context.GD_HOA_DON_CHI_TIET.Where(s => s.ID_HOA_DON == item.ID).ToList();
                        foreach (var item1 in table_chi_tiet)
                        {
                            temp_tien += item1.GIA_BAN * item1.SO_LUONG * 1000;
                            temp_slsp += item1.SO_LUONG;
                        }
                        if (item.GIAM_TRU != null)
                        {
                            decimal giam_tru = Convert.ToDecimal(item.GIAM_TRU);
                            tien += temp_tien - giam_tru;
                        }
                        else tien += temp_tien;
                        slsp += temp_slsp; 

                    }
                    bao_cao.tong_doanh_thu = tien;
                    bao_cao.tong_doanh_so = slsp;
                    bao_cao.nam = nam_tinh;
                    bao_cao.thang = thang_tinh;
                    danh_sach_bao_cao.Add(bao_cao);
                    if (thang_tinh == 12)
                    {
                        thang_tinh = 1;
                        nam_tinh++;
                    }
                    else thang_tinh++;

                } while (((thang_tinh<= Convert.ToDateTime(thang_kt).Month)&(nam_tinh== Convert.ToDateTime(thang_kt).Year))|(nam_tinh< Convert.ToDateTime(thang_kt).Year));
            }
            return danh_sach_bao_cao;
        }
        #endregion
    }
}