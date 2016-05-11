using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH.Controls.Common_Controls
{
    public partial class c0_gia_tien : UserControl
    {
        #region Public Data Member 
        public string m_so_tien;
        public string m_don_vi_tien;
        #endregion

        #region Public Event Handler
        public event EventHandler ChonGiaBan;
        #endregion

        #region Public Methods
        public c0_gia_tien()
        {
            InitializeComponent();
        }
        public c0_gia_tien(string gia_ban)
        {
            InitializeComponent();
            set_define_event();
            data_to_sle();
            m_txt_gia_ban.Text = gia_ban;
        }

        private void data_to_sle()
        {
            DataTable tb = new DataTable();
            DataColumn col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "ten";
            tb.Rows.Add(new object[] { "VND" });

            m_sle_ngoai_te.Properties.DataSource = tb;
            m_sle_ngoai_te.Properties.DisplayMember = "ten";
        }

        public void refresh()
        {

        }
        #endregion

        #region Private Methods
        private void set_define_event()
        {
            m_txt_gia_ban.TextChanged += M_txt_gia_ban_TextChanged;
            m_sle_ngoai_te.EditValueChanged += M_sle_ngoai_te_EditValueChanged;
        }

        private void M_sle_ngoai_te_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_ngoai_te.EditValue == null | String.IsNullOrEmpty(m_sle_ngoai_te.Text) | String.IsNullOrWhiteSpace(m_sle_ngoai_te.Text))
                {
                    m_sle_ngoai_te.Text = "VND";
                }
                m_don_vi_tien = m_sle_ngoai_te.Text;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void M_txt_gia_ban_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(m_txt_gia_ban.Text) | String.IsNullOrWhiteSpace(m_txt_gia_ban.Text))
                {
                    m_txt_gia_ban.Text = "0";
                }
                m_so_tien = m_txt_gia_ban.Text;
                if (ChonGiaBan == null)
                {
                    ChonGiaBan(m_so_tien, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Event Handlers
        #endregion

    }
}
