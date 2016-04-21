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
}
