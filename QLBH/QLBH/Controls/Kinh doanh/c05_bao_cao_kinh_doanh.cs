using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBH.Forms;

namespace QLBH.Controls.Kinh_doanh
{
    public partial class c05_bao_cao_kinh_doanh : UserControl
    {
        public c05_bao_cao_kinh_doanh()
        {
            InitializeComponent();
            set_define_event();

        }
        private void set_define_event()
        {
            m_tile_ket_qua_kinh_doanh.ItemClick += M_tile_ket_qua_kinh_doanh_ItemClick;
        }

        private void M_tile_ket_qua_kinh_doanh_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                f11_bao_cao_doanh_thu v_f = new f11_bao_cao_doanh_thu();
                v_f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
