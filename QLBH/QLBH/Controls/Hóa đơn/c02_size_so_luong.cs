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
using LibraryApi;
using DevExpress.XtraGrid.Columns;

namespace QLBH.Controls.Hóa_đơn
{
    public partial class c02_size_so_luong : UserControl
    {
        #region Data Member 
        private int m_so_luong_max;
        private List<SizeSoLuongHienTai> m_list_ssl;

        public string m_size_hien_tai;
        public int m_so_luong_hien_tai;

        #endregion

        #region Public Event Handler
        public event EventHandler ChonSize;
        public event EventHandler ChonSoLuong;
        #endregion

        #region Public Methods

        public c02_size_so_luong()
        {
            InitializeComponent();
        }

        public c02_size_so_luong(List<SizeSoLuongHienTai> input_list_ssl)
        {
            InitializeComponent();
            set_define_events();
            m_list_ssl = input_list_ssl.Where(s => s.so_luong > 0).ToList();
            data_to_sle(m_list_ssl);
        }
       
        public void refresh()
        {
            
        }

        #endregion

        #region Private Methods

        private void data_to_sle(List<SizeSoLuongHienTai> input_list_ssl)
        {
            GridColumn col1 = new GridColumn();
            col1.FieldName = typeof(SizeSoLuongHienTai).GetProperty("ten_size").Name;
            col1.Caption = "Size";
            m_sle_size.Properties.View.Columns.Add(col1);
            m_sle_size.Properties.DataSource = CommonFunction.list_to_data_table<SizeSoLuongHienTai>(input_list_ssl);
            m_sle_size.Properties.ValueMember = "ten_size";
            m_sle_size.Properties.DisplayMember = "ten_size";
            m_sle_size.Properties.NullText = "S";
        }

        private void set_define_events()
        {
            m_btn_up.Click += M_btn_up_Click;
            m_btn_down.Click += M_btn_down_Click;
            m_sle_size.EditValueChanged += M_sle_size_EditValueChanged;
            m_txt_so_luong.EditValueChanged += M_txt_so_luong_EditValueChanged;
        }

        #endregion

        #region Event Handlers

        private void M_txt_so_luong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_txt_so_luong.EditValue == null)
                {
                    m_txt_so_luong.EditValue = "1";
                    m_so_luong_hien_tai = (int)m_txt_so_luong.EditValue;
                    if (ChonSoLuong == null)
                    {
                        this.ChonSoLuong(m_so_luong_hien_tai, e);
                    }
                    return;
                }
                var sl = (int)m_txt_so_luong.EditValue;
                if (sl > m_so_luong_max)
                {
                    m_txt_so_luong.EditValue = m_so_luong_max;
                    if (ChonSoLuong == null)
                    {
                        this.ChonSoLuong(m_so_luong_hien_tai, e);
                    }
                    return;
                }
                m_so_luong_hien_tai = (int)m_txt_so_luong.EditValue;
                if (ChonSoLuong == null)
                {
                    this.ChonSoLuong(m_so_luong_hien_tai, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void M_sle_size_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var ssl = m_list_ssl.Where(s => s.ten_size == (string)m_sle_size.EditValue).First();
                m_size_hien_tai = ssl.ten_size;
                m_so_luong_max = ssl.so_luong;
                m_txt_so_luong.Text = ssl.so_luong.ToString();
                if (ChonSize == null)
                {
                    ChonSize(m_size_hien_tai, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void M_btn_down_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_so_luong_hien_tai == 1)
                {
                    return;
                }
                m_so_luong_hien_tai -= 1;
                m_txt_so_luong.Text = m_so_luong_hien_tai.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void M_btn_up_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_so_luong_hien_tai == m_so_luong_max)
                {
                    return;
                }
                m_so_luong_hien_tai += 1;
                m_txt_so_luong.Text = m_so_luong_hien_tai.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
