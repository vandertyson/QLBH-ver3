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
                string path = @"http://quanlybanhang.somee.com/docx/";
                var excel = new ExcelQueryFactory(file);
                var dt = from a in excel.Worksheet<ThemHangHoa>("HANG_HOA") select a;
                var listHH = new List<ThemHangHoaPost>();

                foreach (var item in dt)
                {
                    if (string.IsNullOrEmpty(item.Ten))
                    {
                        continue;
                    }
                    var hh = new ThemHangHoaPost();
                    hh.ten_hang_hoa = item.Ten;
                    hh.ma_nha_cung_cap = item.MaNhaCungCap;
                    hh.ma_tra_cuu = item.MaTraCuu;
                    var listLink = Common.TachID(item.Link);
                    hh.link_anh = new List<string>();
                    foreach (var link in listLink)
                    {
                        hh.link_anh.Add(link);
                    }
                    var listTag = Common.TachID(item.Tag);
                    hh.tag = new List<string>();
                    foreach (var t in listTag)
                    {
                        hh.tag.Add(t);
                    }
                    listHH.Add(hh);

                }
                MyNetwork.ThemHangHoa(listHH, this, data =>
                {
                    MessageBox.Show(data.Message);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
