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
                var listHH = new List<ThemHangHoaPost>();
                for (int i = 0; i < 20; i++)
                {
                    var hh = new ThemHangHoaPost();
                    hh.id_nha_cung_cap = 1;
                    hh.link_anh = new List<string> { "link1","link2" };
                    hh.tag = new List<decimal> {1,4,12};
                    hh.ten_hang_hoa = "Hàng hóa " + i.ToString();
                    hh.mo_ta = "Dữ liệu test";
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
