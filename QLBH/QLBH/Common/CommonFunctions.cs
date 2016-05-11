using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using LibraryApi;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data;

namespace QLBH.Common
{
    class CommonFunction
    {
        public static Image get_image(string link)
        {
            if (String.IsNullOrEmpty(link))
            {
                return Image.FromFile(@"C: \Users\Son Pham\Desktop\quan.jpeg");
            }
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(link);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    return Image.FromStream(mem);
                }

            }
        }

        public static DataTable list_to_data_table<T>(List<T> ip_list)
        {
            DataTable result = new DataTable();
            var type = typeof(T);
            var coltype = type.GetProperties();
            foreach (var item in coltype)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = item.Name;
                col.DataType = item.PropertyType;
                result.Columns.Add(col);
            }
            foreach (var data in ip_list)
            {
                var values = new object[coltype.Length];
                for (int i = 0; i < coltype.Length; i++)
                {
                    values[i] = coltype[i].GetValue(data, null);
                }
                result.Rows.Add(values);
            }
            return result;
        }

        public static DataTable convert_list_to_data_table<T>(List<string> PropertyNames, List<T> input_list)
        {
            DataTable result = new DataTable();
            foreach (var item in typeof(T).GetProperties())
            {
                if (PropertyNames.Contains(item.Name))
                {
                    DataColumn col = new DataColumn();
                    col.ColumnName = item.Name;
                    col.DataType = item.PropertyType;
                    result.Columns.Add(col);
                }
            }
            foreach (var obj in input_list)
            {
                List<object> data_row_value = new List<object>();
                var prop = obj.GetType().GetProperties();
                foreach (var prop_name in PropertyNames)
                {
                    var value = obj.GetType().GetProperty(prop_name).GetValue(obj, null);
                    data_row_value.Add(value);
                }
                result.Rows.Add(data_row_value.ToArray());
            }
            return result;
        }
    
        internal static string download_docx_file_from_link(string mo_ta, string file_name)
        {
                if (String.IsNullOrEmpty(mo_ta))
                {
                   
                }
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                           
                }
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.DownloadFileAsync(new Uri(mo_ta),file_name);
                }
                return file_name;
        }

        public static void exception_handle(Exception ex)
        {
            XtraMessageBox.Show(ex.Data.ToString() + "INNER     :" + ex.InnerException);         
        }

        public static Color lay_mau_theo_ma_mau(string ma_mau_html)
        {
            return ColorTranslator.FromHtml(ma_mau_html);
        }
        public static List<string> TachID(string input)
        {
            var dsID = input.Split(';').ToList();
            var listID = new List<string>();
            foreach (var item in dsID)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                listID.Add(item);
            }
            return listID;
        }

    }
}
