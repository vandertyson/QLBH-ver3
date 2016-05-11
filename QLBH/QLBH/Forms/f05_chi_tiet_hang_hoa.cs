using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBH.Controls;
using LibraryApi;
using QLBH.Common;

namespace QLBH.Forms
{
    public partial class f05_chi_tiet_hang_hoa : Form
    {
        public HangHoa m_curent_hang_hoa;
        public f05_chi_tiet_hang_hoa()
        {
            InitializeComponent();
            set_define_event();
        }

        private void set_define_event()
        {
            m_btn_thoat.Click += M_btn_thoat_Click;
        }

        private void M_btn_thoat_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        public void display(HangHoa v_hh)
        {
            m_curent_hang_hoa = v_hh;
            this.CenterToScreen();
            m_c01_hang_hoa_chi_tiet = new c01_hang_hoa_chi_tiet(m_curent_hang_hoa);
            this.ShowDialog();
        }
    }
}
