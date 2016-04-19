using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryApi;
using ReadDataExcel;
using LinqToExcel;

namespace QLBH.Forms
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string file = @"C:\Users\Son Pham\Desktop\QLBH ver3\DataExcel.xlsx";
                var excel = new ExcelQueryFactory(file);
                var dt = from a in excel.Worksheet<ThemHangHoa>("HANG_HOA") select a;
                var listHH = new List<ThemHangHoaPost>();

                foreach (var item in dt)
                {
                    if (string.IsNullOrEmpty(item.ten_hang_hoa))
                    {
                        continue;
                    }
                    var hh = new ThemHangHoaPost();
                    hh.ten_hang_hoa = item.ten_hang_hoa;
                    hh.id_nha_cung_cap = item.id_nha_cung_cap;
                    var listLink = Common.TachID(item.link_anh);
                    hh.link_anh = new List<string>();
                    foreach (var link in listLink)
                    {
                        hh.link_anh.Add(link);
                    }
                    var listTag = Common.TachID(item.tag);
                    hh.tag = new List<decimal>();
                    foreach (var link in listTag)
                    {
                        hh.link_anh.Add(link);
                    }
                    listHH.Add(hh);
                }
                MessageBox.Show(listHH.Count.ToString());
                //MyNetwork.ThemHangHoa(listHH, this, data =>
                //{
                //    MessageBox.Show(data.Message);
                //});
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
