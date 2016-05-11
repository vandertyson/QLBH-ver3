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

namespace QLBH.Controls
{
    public partial class c01_hang_hoa_simple : UserControl
    {
        #region Public Data Member 
        private decimal m_id_cua_hang;
        private DateTime m_ngay_hien_tai;
        public List<HangHoa> m_list_hang_hoa;
        public HangHoa m_hang_hoa;
        #endregion

        #region Public Event Handler
        public event EventHandler ChonHangHoa;
        #endregion

        #region Public Methods
        public c01_hang_hoa_simple()
        {
            InitializeComponent();
        }
        public c01_hang_hoa_simple(decimal id_cua_hang, DateTime ngay_hien_tai)
        {
            InitializeComponent();
            set_define_event();
            m_id_cua_hang = id_cua_hang;
            m_ngay_hien_tai = ngay_hien_tai;
            LayDanhSachHangHoa(id_cua_hang, ngay_hien_tai, this.TopLevelControl as Form, data =>
              {
                  m_list_hang_hoa = data.Data;
                  load_data_to_sle_hang_hoa();
              });
        }
        public c01_hang_hoa_simple(decimal id_cua_hang, DateTime ngay_hien_tai, List<HangHoa> ip_list)
        {
            InitializeComponent();
            set_define_event();
            m_id_cua_hang = id_cua_hang;
            m_ngay_hien_tai = ngay_hien_tai;
            m_list_hang_hoa = ip_list;
            load_data_to_sle_hang_hoa();
        }
        private void set_define_event()
        {
            m_sle_hang_hoa.EditValueChanged += M_sle_hang_hoa_EditValueChanged;
        }

        private void M_sle_hang_hoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_hang_hoa.EditValue == null | String.IsNullOrEmpty(m_sle_hang_hoa.Text) | String.IsNullOrWhiteSpace(m_sle_hang_hoa.Text))
                {
                    m_sle_hang_hoa.EditValue = m_list_hang_hoa.First().ma_hang_hoa;
                }
                string ma = (string)m_sle_hang_hoa.EditValue;
                //
                m_hang_hoa = m_list_hang_hoa.Where(s => s.ma_hang_hoa == ma).First();
                m_lbl_gia.Text = m_hang_hoa.gia_hien_tai.ToString() + "VND";
                m_lbl_ma_hang_hoa.Text = m_hang_hoa.ma_hang_hoa;

                //
                m_isl_hinh_anh.Images.Clear();
                foreach (var item in m_hang_hoa.link_anh)
                {
                    var pic = CommonFunction.get_image(item);
                    m_isl_hinh_anh.Images.Add(pic);
                }
                //

                if (ChonHangHoa == null)
                {
                    this.ChonHangHoa(m_hang_hoa, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void load_data_to_sle_hang_hoa()
        {
            List<string> prop_name = new List<string> { "ten_hang_hoa", "ma_hang_hoa", "gia_hien_tai" };
            var ds = CommonFunction.convert_list_to_data_table<HangHoa>(prop_name, m_list_hang_hoa);

            m_sle_hang_hoa.Properties.DataSource = ds;
            m_sle_hang_hoa.Properties.DisplayMember = "ten_hang_hoa";
            m_sle_hang_hoa.Properties.ValueMember = "ma_hang_hoa";

        }

        public void refresh()
        {
            m_id_cua_hang = SystemInfo.id_cua_hang;
            m_ngay_hien_tai = DateTime.Now;
            LayDanhSachHangHoa(m_id_cua_hang, m_ngay_hien_tai, this.TopLevelControl as Form, data =>
            {
                m_list_hang_hoa = data.Data;
                load_data_to_sle_hang_hoa();
            });
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion



    }
}
