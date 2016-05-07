using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH.Controls
{
    public partial class c01_search_box : UserControl
    {
        public delegate void DelSearch(string key);
        public DelSearch Search;
        public c01_search_box()
        {
            InitializeComponent();
        }

        private void m_btn_search_Click(object sender, EventArgs e)
        {
            Search?.Invoke(txtKeyWord.Text);
        }

        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search?.Invoke(txtKeyWord.Text);
            }
        }
    }
}
