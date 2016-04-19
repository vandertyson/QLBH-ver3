using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;

namespace ReadDataExcel
{
    public class ReadExcel
    {
        public static void FromFile<T>(string path)
        {
            var excel = new ExcelQueryFactory(path);

        }
    }
}
