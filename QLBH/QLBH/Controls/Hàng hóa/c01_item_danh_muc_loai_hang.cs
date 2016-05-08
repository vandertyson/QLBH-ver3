using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH.Controls.Hàng_hóa
{
    public partial class c01_item_danh_muc_loai_hang : UserControl
    {
        public c01_item_danh_muc_loai_hang()
        {
            InitializeComponent();
        }
        public void SetName(string name)
        {
            lblName.Text = name;
        }
        public void SetHighlight()
        {
            BackColor = Color.BlueViolet;
            lblName.ForeColor = Color.White;
        }
        public void SetNormal()
        {
            BackColor = Color.White;
            lblName.ForeColor = Color.Black;
        }
    }
}
