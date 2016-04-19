using LinqToExcel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDataExcel
{
    public class ThemHangHoa
    {
        [ExcelColumn("TEN")]
        public string ten_hang_hoa { get; set; }
        [ExcelColumn("ID_NHA_CUNG_CAP")]
        public decimal id_nha_cung_cap { get; set; }
        [ExcelColumn("MO_TA")]
        public string mo_ta { get; set; }
        [ExcelColumn("TAP_LINK_ANH")]
        public string link_anh { get; set; }
        [ExcelColumn("TAP_TAG")]
        public string tag { get; set; }
    }
}
