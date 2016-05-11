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

namespace QLBH.Controls.Common_Controls
{
    public partial class c0_khach_hang_simple : UserControl
    {
        #region Public Data Member 
        public List<KhachHang> m_list_khach_hang;
        public KhachHang m_current_khach_hang;
        #endregion

        #region Public Event Handler
        public event EventHandler ChonKhachHang;
        #endregion

        #region Public Methods
        public c0_khach_hang_simple()
        {
            InitializeComponent();
            set_define_event();
        }
        public c0_khach_hang_simple(DateTime ip_ngay_hien_tai)
        {
            InitializeComponent();
            set_define_event();
            LayDanhSachKhachHang(DateTime.Now.Date, this.TopLevelControl as Form, data =>
            {
                m_list_khach_hang = data.Data;
                data_to_sle();
            });
        }

        public void refresh()
        {
            LayDanhSachKhachHang(DateTime.Now.Date, this.TopLevelControl as Form, data =>
            {
                m_list_khach_hang = data.Data;
                data_to_sle();
            });
        }
        #endregion

        #region Private Methods
        private void set_define_event()
        {
            m_sle_khach_hang.EditValueChanged += M_sle_khach_hang_EditValueChanged;
        }


        private void data_to_sle()
        {
            m_sle_khach_hang.Properties.DataSource = CommonFunction.list_to_data_table<KhachHang>(m_list_khach_hang);
            m_sle_khach_hang.Properties.DisplayMember = "ten_khach_hang";
            m_sle_khach_hang.Properties.ValueMember = "tai_khoan";
        }

        #endregion

        #region Event Handlers
        private void M_sle_khach_hang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_khach_hang.EditValue == null | String.IsNullOrEmpty(m_sle_khach_hang.Text) | String.IsNullOrWhiteSpace(m_sle_khach_hang.Text))
                {
                    return;
                }
                if (m_current_khach_hang.ten_khach_hang == "Khách vãng lai")
                {
                    m_lbl_email.Text = "";
                    m_lbl_so_dien_thoai.Text = "";
                    if (this.ChonKhachHang == null)
                    {
                        this.ChonKhachHang(m_current_khach_hang, e);
                    }
                    return;
                }
                m_current_khach_hang = m_list_khach_hang.Where(s => s.tai_khoan == (string)m_sle_khach_hang.EditValue).First();
                m_lbl_email.Text = m_current_khach_hang.email;
                m_lbl_so_dien_thoai.Text = m_current_khach_hang.so_dien_thoai;
                if (this.ChonKhachHang != null)
                {
                    this.ChonKhachHang(m_current_khach_hang, e);
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
