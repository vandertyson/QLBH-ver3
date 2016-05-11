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

namespace QLBH.Controls.Cửa_hàng
{
    public partial class c03_nghiep_vu_cua_hang : UserControl
    {
        public c03_nghiep_vu_cua_hang()
        {
            InitializeComponent();
            set_define_event();
        }

        private void set_define_event()
        {
            m_tile_them_ban_hang.ItemClick += M_tile_them_ban_hang_ItemClick;
            m_tile_nhap_kho.ItemClick += M_tile_nhap_kho_ItemClick;
            m_tile_tao_khuyen_mai.ItemClick += M_tile_tao_khuyen_mai_ItemClick;
            m_tile_them_hang_hoa.ItemClick += M_tile_them_hang_hoa_ItemClick;
        }

        private void M_tile_them_hang_hoa_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            f10_them_hang_hoa_excal v_f = new f10_them_hang_hoa_excal();
            v_f.Show();
        }

        private void M_tile_tao_khuyen_mai_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            f06_khuyen_mai v_f = new f06_khuyen_mai();
            v_f.ShowDialog();
        }

        private void M_tile_nhap_kho_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            f03_them_phieu_nhap_excel f = new f03_them_phieu_nhap_excel();
            f.ShowDialog();
        }

        private void M_tile_them_ban_hang_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            f02_them_hoa_don_v2 f = new f02_them_hoa_don_v2();
            f.ShowDialog();
        }
    }
}
