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
    public partial class c01_item_hang_hoa : UserControl
    {
        public c01_item_hang_hoa()
        {
            InitializeComponent();
        }
        public bool IsHighLight { get; set; } = false;
        public void setHighLight()
        {
            IsHighLight = true;
            this.BackColor = Color.Violet;
        }
        public void setNormal()
        {
            IsHighLight = false;
            this.BackColor = Color.White;
        }
        public void setName(string name)
        {
            txtName.Text = name;
        }
        public void setGia(decimal gia)
        {
            txtGia.Text = string.Format("{0:0},000 đ", gia);
        }
        public void setGiamGia(float giamGia)
        {
            txtGiamGia.Text = string.Format("{0:0}%", -giamGia * 100);
        }
        public void setDiem(decimal diem)
        {
            lblDiem.Text = string.Format("{0:0.0}", diem);
        }
        public void setSoComment(int so)
        {
            lblComment.Text = so.ToString();
        }
        public void setLuotClick(int so)
        {
            lblClick.Text = so.ToString();
        }
        public void setIcon(string link)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                picIcon.LoadAsync(link);
            }
            else
            {
                picIcon.LoadAsync(@"C:\Users\Son Pham\Desktop\Quan ly ban hang\QLBH-ver3\QLBH\QLBH\Template\ao-so-mi.jpg");
            }
        }
        public void setMaTraCuu(string ma)
        {
            lblMaTraCuu.Text = ma;
        }
    }
}
