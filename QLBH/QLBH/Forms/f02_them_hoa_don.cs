using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTab;
using QLBH.Controls.Hóa_đơn;
using static LibraryApi.QuanLyHoaDon;

namespace QLBH.Forms
{
    public partial class f02_them_hoa_don : Form
    {
        private decimal id_cua_hang;
        private DateTime now;
        #region Public Interfaces
        public f02_them_hoa_don()
        {
            InitializeComponent();
        }

        public f02_them_hoa_don(DateTime now, decimal id_cua_hang)
        {
            InitializeComponent();
            xtraTabPage1.Controls.Clear();
            this.now = now;
            this.id_cua_hang = id_cua_hang;
            set_define_event();
            //them_hoa_don_moi();
        }

        private void set_define_event()
        {

            m_btn_them_hoa_don.Click += M_btn_them_hoa_don_Click;
        }

        private void M_btn_them_hoa_don_Click(object sender, EventArgs e)
        {
            try
            {
                them_hoa_don_moi();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void them_hoa_don_moi()
        {
            LayMaHoaDon(this, data =>
            {
                c02_hoa_don_full hd = new c02_hoa_don_full(id_cua_hang, now, data.Data);
                hd.Dock = DockStyle.Fill;
                XtraTabPage page = new XtraTabPage();
                page.Controls.Add(hd);
                page.Text = "Hóa đơn" + data.Data;
                m_tab_control_hoa_don.SelectedTabPage = m_tab_control_hoa_don.SelectedTabPage = page;
            });
        }

        public void display()
        {
            this.ShowDialog();
            this.WindowState = FormWindowState.Maximized;
        }
        #endregion
        #region Data Stucture
        #endregion
        #region Member

        #endregion
        #region Private Methods
        #endregion
        #region Event Handlers
        #endregion

    }
}
