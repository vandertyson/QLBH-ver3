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
            try {
                var listHH = new List<ThemHangHoaPost>();
                for (int i = 0; i < 50; i++)
                {
                    var hh = new ThemHangHoaPost();
                    hh.id_nha_cung_cap = 1;
                    //hh.link_anh = new List<string> { "gì đó hoặc null" };
                    //hh.tag = new List<decimal> { };
                    hh.tenHangHoa = "Hàng hóa " + i.ToString();
                    //hh.mo_ta = "Gì đó hoặc null";
                    listHH.Add(hh);
                }
                MyNetwork.ThemHangHoa(listHH, this, data =>
                {
                    if (data.Success)
                    {
                    // lam gi do neu thanh cong
                    //data la cai tra ve a
                    //hay vl
                    // ok xo
                    // thu chay xem sao
                }

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
