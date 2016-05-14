using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Docking2010;
using QLBH.Forms;
using QLBH.Controls;
using LibraryApi;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraEditors;
using QLBH.Common;
using QLBH.Controls.Cửa_hàng;
using QLBH.Controls.Kinh_doanh;


namespace QLBH.Forms
{
    public partial class f01_main_form : Form
    {
        #region member
        bool v_menu_detail_is_opened = false;
        public List<XtraTabPage> m_opening_control { get; set; } = new List<XtraTabPage>();

        #endregion

        #region public methods
        public f01_main_form()
        {
            InitializeComponent();
            set_init_form_load();
            format_control();
        }

        #endregion

        #region private methods
        private void format_control()
        {
            m_pnl_status.BackColor = Color.FromArgb(50, Color.White);
            m_tabcontrol_main_view.TabPages.Clear();
        }

        private void set_init_form_load()
        {
            v_menu_detail_is_opened = false;
            set_menu_detail_status();
            set_define_event();
        }

        private void set_menu_detail_status()
        {
            m_pnl_menu_detail.Visible = v_menu_detail_is_opened;
        }

        private XtraTabPage check_exist(Type type)
        {
            foreach (var item in m_opening_control)
            {
                if (item.Name == type.ToString())
                {
                    return item;
                }

            }
            return null;
        }
        #endregion

        #region event handlers
        private void set_define_event()
        {
            //menu
            m_btn_open_menu.Click += m_btn_open_menu_Click;
            //tong quan
            m_btn_tong_quan.Click += M_btn_tong_quan_Click;
            m_btn_menu_tong_quan.Click += M_btn_tong_quan_Click;
            //hang hoa
            m_btn_hang_hoa.Click += M_btn_hang_hoa_Click;
            m_btn_menu_hang_hoa.Click += M_btn_hang_hoa_Click;
            //cua hang
            m_btn_cua_hang.Click += M_btn_cua_hang_Click;
            m_btn_menu_cua_hang.Click += M_btn_cua_hang_Click;
            //khach hang
            m_btn_khach_hang.Click += M_btn_khach_hang_Click;
            m_btn_menu_khach_hang.Click += M_btn_khach_hang_Click;
            //kinh doanh
            m_btn_kinh_doanh.Click += M_btn_kinh_doanh_Click;
            m_btn_menu_kinh_doanh.Click += M_btn_kinh_doanh_Click;
            //he thong
            m_btn_he_thong.Click += M_btn_he_thong_Click;
            m_btn_menu_he_thong.Click += M_btn_he_thong_Click;
            //thoat
            m_btn_exit.Click += M_btn_exit_Click;
            m_btn_menu_exit.Click += M_btn_exit_Click;
            //tab control
            m_tabcontrol_main_view.CloseButtonClick += m_tabcontrol_main_view_CloseButtonClick;
        }

        private void M_btn_exit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_btn_he_thong_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_btn_kinh_doanh_Click(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage vtp = new XtraTabPage();
                vtp.Name = typeof(c05_bao_cao_kinh_doanh).ToString();
                m_tabcontrol_main_view.TabPages.Add(vtp);
                vtp.Text = "Báo cáo kinh doanh";
                m_opening_control.Add(vtp);
                c05_bao_cao_kinh_doanh v_ql = new c05_bao_cao_kinh_doanh();
                vtp.Controls.Add(v_ql);
                v_ql.Dock = DockStyle.Fill;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void M_btn_khach_hang_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_btn_hang_hoa_Click(object sender, EventArgs e)
        {
            try
            {
                var p = check_exist(typeof(c01_quan_ly_hang_hoa));
                if (p != null)
                {
                    m_tabcontrol_main_view.SelectedTabPage = p;
                }
                else
                {
                    try
                    {
                        XtraTabPage vtp = new XtraTabPage();
                        vtp.Name = typeof(c01_quan_ly_hang_hoa).ToString();
                        m_tabcontrol_main_view.TabPages.Add(vtp);
                        vtp.Text = "Quản lý hàng hóa";
                        m_opening_control.Add(vtp);
                        c01_quan_ly_hang_hoa v_ql = new c01_quan_ly_hang_hoa();
                        vtp.Controls.Add(v_ql);
                        v_ql.Dock = DockStyle.Fill;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        private void M_btn_tong_quan_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void M_btn_cua_hang_Click(object sender, EventArgs e)
        {
            var p = check_exist(typeof(c03_nghiep_vu_cua_hang));
            if (p != null)
            {
                m_tabcontrol_main_view.SelectedTabPage = p;
            }
            else
            {
                try
                {
                    XtraTabPage vtp = new XtraTabPage();
                    vtp.Name = typeof(c03_nghiep_vu_cua_hang).ToString();
                    m_tabcontrol_main_view.TabPages.Add(vtp);
                    vtp.Text = "Nghiệp vụ cửa hàng";
                    m_opening_control.Add(vtp);
                    c03_nghiep_vu_cua_hang v_ql = new c03_nghiep_vu_cua_hang();
                    vtp.Controls.Add(v_ql);
                    v_ql.Dock = DockStyle.Fill;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void m_btn_open_menu_Click(object sender, EventArgs e)
        {
            v_menu_detail_is_opened = !v_menu_detail_is_opened;
            set_menu_detail_status();
        }

        private void m_tabcontrol_main_view_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            m_opening_control.Remove(arg.Page as XtraTabPage);
            m_tabcontrol_main_view.TabPages.Remove(arg.Page as XtraTabPage);
        }

        #endregion

    }
}
