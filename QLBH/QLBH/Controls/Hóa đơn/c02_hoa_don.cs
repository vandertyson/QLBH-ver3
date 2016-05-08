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

namespace QLBH.Controls.Hóa_đơn
{
    public partial class c02_hoa_don : UserControl
    {
        #region Public Data Member 
        public string ma_hoa_don { get; set; }
        public DateTime thoi_gian_tao { get; set; }
        public CuaHang cua_hang { get; set; }
        public KhachHang khach { get; set; }
        public string loai_thanh_toan { get; set; }
        public decimal giam_tru { get; set; }
        public decimal tong_gia_tri_hoa_don { get; set; }
        public decimal tong_tien_giam_tru_km { get; set; }
        #endregion

        #region Public Event Handler
        public event EventHandler ButtonAClick;
        #endregion

        #region Public Methods
     
        public void refresh()
        {

        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion
        public c02_hoa_don()
        {
            InitializeComponent();
        }

        private void c02_hoa_don_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
