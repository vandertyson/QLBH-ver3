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
using QLBH.Common;

namespace QLBH.Controls.Hóa_đơn
{
    public partial class c02_khuyen_mai_simple : UserControl
    {
        #region Public Data Member 
        public List<KhuyenMaiDangApDung> m_list_km_dang_ap_dung;
        public KhuyenMaiDangApDung m_current_km;
        #endregion

        #region Public Event Handler
        public event EventHandler ChonKhuyenMai;
        #endregion

        #region Public Methods
        public c02_khuyen_mai_simple()
        {
            InitializeComponent();
        }
        public c02_khuyen_mai_simple(HangHoa hh)
        {
            InitializeComponent();
            set_define_event();
            m_list_km_dang_ap_dung = hh.km_dang_ap_ung;
            data_to_sle();
        }

        private void data_to_sle()
        {
            m_sle_dot_khuyen_mai.Properties.DataSource = CommonFunction.list_to_data_table<KhuyenMaiDangApDung>(m_list_km_dang_ap_dung);
            m_sle_dot_khuyen_mai.Properties.DisplayMember = "mo_ta";
            m_sle_dot_khuyen_mai.Properties.ValueMember = "ma_dot_khuyen_mai";
        }

        private void set_define_event()
        {
            m_sle_dot_khuyen_mai.EditValueChanged += M_sle_dot_khuyen_mai_EditValueChanged;
        }

        public void refresh()
        {

        }

        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers

        private void M_sle_dot_khuyen_mai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_dot_khuyen_mai.EditValue == null | String.IsNullOrEmpty(m_sle_dot_khuyen_mai.Text) | String.IsNullOrWhiteSpace(m_sle_dot_khuyen_mai.Text))
                {
                    m_sle_dot_khuyen_mai.Text = "Không áp dụng khuyến mãi";
                }
                m_current_km = m_list_km_dang_ap_dung.Where(s => s.mo_ta == m_sle_dot_khuyen_mai.Text).First();
                if (ChonKhuyenMai == null)
                {
                    ChonKhuyenMai(m_current_km, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
