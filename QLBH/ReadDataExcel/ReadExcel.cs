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
        public static object FromFile<T>(string path,string name)
        {
            var excel = new ExcelQueryFactory(path);
            var list = from a in excel.Worksheet<T>(name) select a;

            return list;
        }

    }
}
