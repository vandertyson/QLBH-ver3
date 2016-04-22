using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqToExcel.Attributes;

namespace QLBH
{
    class ThemHangHoa
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
    #region Quản lý bán hàng
    public class PhieuNhapExcel
    {
        [ExcelColumn("NGAY_NHAP")]
        public string ngay_nhap { get; set; }
        [ExcelColumn("MA_TRA_CUU_HANG_HOA")]
        public string ma_tra_cuu { get; set; }
        [ExcelColumn("SIZE_S")]
        public string so_luong_size_s { get; set; }
        [ExcelColumn("SIZE_M")]
        public string so_luong_size_m { get; set; }
        [ExcelColumn("SIZE_L")]
        public string so_luong_size_l { get; set; }
        [ExcelColumn("SIZE_XL")]
        public string so_luong_size_xl { get; set; }
        [ExcelColumn("SIZE_XXL")]
        public string so_luong_size_xxl { get; set; }
        [ExcelColumn("GIA_NHAP")]
        public string gia_nhap { get; set; }
    }
    #endregion
}
