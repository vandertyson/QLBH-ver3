using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH.Forms
{
    public partial class f06_khuyen_mai : Form
    {
        #region Public Interfaces
        #endregion
        #region Members
        #endregion
        #region Data Structures
        #endregion
        #region Private Methods
        #endregion
        #region Event Handlers
        #endregion
        public f06_khuyen_mai()
        {
            InitializeComponent();
            this.CenterToParent();
            this.WindowState = FormWindowState.Maximized;
            set_defined_event();
        }

        private void set_defined_event()
        {
            m_btn_them_hang_hoa.Click += M_btn_them_hang_hoa_Click;
        }

        private void M_btn_them_hang_hoa_Click(object sender, EventArgs e)
        {
            try
            {
                m_grv_khuyen_mai_chi_tiet.AddNewRow();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
