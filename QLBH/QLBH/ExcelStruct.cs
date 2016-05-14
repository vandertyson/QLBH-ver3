using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqToExcel.Attributes;

namespace QLBH
{
    public class ThemHangHoaExcel
    {
        [ExcelColumn("TEN")]
        public string Ten { get; set; }
        [ExcelColumn("MA_NHA_CUNG_CAP")]
        public string MaNhaCungCap { get; set; }
        [ExcelColumn("MA_TRA_CUU")]
        public string MaTraCuu { get; set; }
        [ExcelColumn("MO_TA")]
        public string MoTa { get; set; }
        [ExcelColumn("LINK")]
        public string Link { get; set; }
        [ExcelColumn("TAG")]
        public string Tag { get; set; }
    }

    public class NhieuPhieuNhapExcel
    {
        [ExcelColumn("NGAY_NHAP")]
        public string ngay_nhap { get; set; }
        [ExcelColumn("MA_TRA_CUU_HANG_HOA")]
        public string ma_tra_cuu { get; set; }
        [ExcelColumn("SIZE_S")]
        public string S { get; set; }
        [ExcelColumn("SIZE_M")]
        public string M { get; set; }
        [ExcelColumn("SIZE_L")]
        public string L { get; set; }
        [ExcelColumn("SIZE_XL")]
        public string XL { get; set; }
        [ExcelColumn("SIZE_XXL")]
        public string XXL { get; set; }
        [ExcelColumn("GIA_NHAP")]
        public string gia_nhap { get; set; }
    }

    public class MotPhieuNhapExcel
    {
        [ExcelColumn("MA_TRA_CUU_HANG_HOA")]
        public string ma_tra_cuu { get; set; }
        [ExcelColumn("SIZE_S")]
        public string S { get; set; }
        [ExcelColumn("SIZE_M")]
        public string M { get; set; }
        [ExcelColumn("SIZE_L")]
        public string L { get; set; }
        [ExcelColumn("SIZE_XL")]
        public string XL { get; set; }
        [ExcelColumn("SIZE_XXL")]
        public string XXL { get; set; }
        [ExcelColumn("GIA_NHAP")]
        public string gia_nhap { get; set; }
    }

    public class ThemGiaExcel
    {
        [ExcelColumn("MA_TRA_CUU_HANG_HOA")]
        public string ma_tra_cuu { get; set; }
        [ExcelColumn("GIA_BAN")]
        public string gia_ban { get; set; }
        [ExcelColumn("NGAY_AP_DUNG")]
        public string ngay_ap_dung { get; set; }
    }
}
