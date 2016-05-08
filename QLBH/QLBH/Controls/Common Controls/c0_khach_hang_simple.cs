using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LibraryApi.QuanLyHoaDon;

namespace QLBH.Controls.Common_Controls
{
    public partial class c0_khach_hang_simple : UserControl
    {
        #region Public Data Member 
        public List<KhachHang> m_list_khach_hang;
        public KhachHang m_current_khach_hang;
        #endregion

        #region Public Event Handler
        public event EventHandler ButtonAClick;
        #endregion

        #region Public Methods
        public c0_khach_hang_simple()
        {
            InitializeComponent();
            
        }
        public void refresh()
        {

        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion
       
    }
}
