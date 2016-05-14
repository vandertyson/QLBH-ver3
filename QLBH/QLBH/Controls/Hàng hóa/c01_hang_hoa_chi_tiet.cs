using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using QLBH.Common;
using LibraryApi;

namespace QLBH.Controls
{
    public partial class c01_hang_hoa_chi_tiet : UserControl
    {
        #region public member
        public c01_hinh_anh_minh_hoa v_c01_hinh_anh = new c01_hinh_anh_minh_hoa();
        public c01_mo_ta_hang_hoa v_c01_mo_ta = new c01_mo_ta_hang_hoa();
        public c01_nhan_xet_khach_hang v_c01_nhan_xet_khach_hang = new c01_nhan_xet_khach_hang();
        public c01_chi_tiet_khuyen_mai v_c01_chi_tiet_khuyen_mai;
        public c01_tinh_trang_kinh_doanh v_c01_tinh_tinh_trang_kinh_doanh = new c01_tinh_trang_kinh_doanh();
        private LibraryApi.HangHoa m_curent_hang_hoa;
        private decimal id;

        #endregion

        #region data binding type
        public HangHoa v_hang_hoa { get; set; }
        public HangHoaMaster v_hang_hoa_master { get; set; }
        #endregion

        #region public event handler
        public event EventHandler ButtonDeleteClick;
        #endregion

        #region public and private method

        public c01_hang_hoa_chi_tiet()
        {
            InitializeComponent();
        }



        public c01_hang_hoa_chi_tiet(HangHoaMaster v_hhmh)
        {
            InitializeComponent();
            v_hang_hoa_master = v_hhmh;
            //
            get_data_for_member();
            data_to_control();
            data_2_mo_ta_v2();
        }
        public c01_hang_hoa_chi_tiet(HangHoa v_hh)
        {
            InitializeComponent();
            v_hang_hoa = v_hh;
            //
            get_data_for_member();
            data_2_mo_ta_v2();
        }

        public c01_hang_hoa_chi_tiet(decimal id)
        {
            InitializeComponent();
            QuanLyDanhMucHangHoa.ChiTietHangHoa(id, this.TopLevelControl as Form, data =>
             {
                 v_hang_hoa = data.Data;
                 get_data_for_member();
                 //data_2_mo_ta_v2();
             });
        }

        public void get_data_for_member()
        {
            //get_hinh_anh_data();
            get_khuyen_mai_data();
            //get_mo_ta_data();
            //get_nhan_xet_data();
            //get_kinh_doanh_data();
        }

        private void get_khuyen_mai_data()
        {
            //v_c01_chi_tiet_khuyen_mai = new c01_chi_tiet_khuyen_mai(v_hang_hoa);
            LibraryApi.BaoCaoChiTietHangHoa.LayBaoCaoChiTietKhuyenMai(v_hang_hoa.id, DateTime.Now, this.TopLevelControl as Form, data =>
              {
                  LibraryApi.BaoCaoChiTietHangHoa.LayBaoCaoPhanHoiKhachHang(v_hang_hoa.id, DateTime.Now.AddMonths(-6), 6, this.TopLevelControl as Form, data1 =>
                  {
                      if (data1.Data == null | data1.Data.thong_ke_theo_thang.Count == 0)
                      {
                          return;
                      }
                      v_c01_nhan_xet_khach_hang = new c01_nhan_xet_khach_hang(data1.Data);
                      m_xtra_scroll.Controls.Add(v_c01_nhan_xet_khach_hang);
                      v_c01_nhan_xet_khach_hang.Dock = DockStyle.Top;
                      m_lbl_ten_san_pham.Text = v_hang_hoa.ten;
                      string tag = "";
                      foreach (var item in v_hang_hoa.ds_tag)
                      {
                          tag += "-" + item.ten_tag;
                      }
                      m_lbl_hang_hoa_master.Text = "Mã sản phẩm: " + v_hang_hoa.ma_hang_hoa + " - Xuất xứ : " + v_hang_hoa.nha_cung_cap.ten;
                      m_lbl_tag.Text = tag;
                      //

                      //

                      v_c01_chi_tiet_khuyen_mai = new c01_chi_tiet_khuyen_mai(data.Data, v_hang_hoa);
                      m_xtra_scroll.Controls.Add(v_c01_chi_tiet_khuyen_mai);
                      v_c01_chi_tiet_khuyen_mai.Dock = DockStyle.Top;


                      v_c01_mo_ta = new c01_mo_ta_hang_hoa(v_hang_hoa);
                      m_xtra_scroll.Controls.Add(v_c01_mo_ta);
                      v_c01_mo_ta.Dock = DockStyle.Top;
                      //
                      if (data.Data == null | data.Data.lich_su.Count == 0)
                      {
                          return;
                      }
                      v_c01_hinh_anh = new c01_hinh_anh_minh_hoa(v_hang_hoa);
                      m_xtra_scroll.Controls.Add(v_c01_hinh_anh);
                      v_c01_hinh_anh.Dock = DockStyle.Top;
                      //
                  });
                 
              });
        }
        private void get_hinh_anh_data()
        {
            v_c01_hinh_anh = new c01_hinh_anh_minh_hoa(v_hang_hoa);
            m_xtra_scroll.Controls.Add(v_c01_hinh_anh);
            v_c01_hinh_anh.Dock = DockStyle.Top;
        }
        private void get_mo_ta_data()
        {
            v_c01_mo_ta = new c01_mo_ta_hang_hoa(v_hang_hoa);
        }
        private void get_kinh_doanh_data()
        {
            v_c01_tinh_tinh_trang_kinh_doanh = new c01_tinh_trang_kinh_doanh();
            m_xtra_scroll.Controls.Add(v_c01_tinh_tinh_trang_kinh_doanh);
            v_c01_tinh_tinh_trang_kinh_doanh.Dock = DockStyle.Top;
        }
        private void get_nhan_xet_data()
        {
            LibraryApi.BaoCaoChiTietHangHoa.LayBaoCaoPhanHoiKhachHang(v_hang_hoa.id, DateTime.Now.AddMonths(-6), 6, this.TopLevelControl as Form, data =>
             {
                 v_c01_nhan_xet_khach_hang = new c01_nhan_xet_khach_hang(data.Data);
                 m_xtra_scroll.Controls.Add(v_c01_nhan_xet_khach_hang);
                 v_c01_nhan_xet_khach_hang.Dock = DockStyle.Top;
             });

        }

        public void data_to_control()
        {


            m_xtra_scroll.Controls.Add(v_c01_mo_ta);
            v_c01_mo_ta.Dock = DockStyle.Top;

            //
            //v_c01_hinh_anh.Dock = DockStyle.Left;
            //v_c01_mo_ta.Dock = DockStyle.Left;
            //v_c01_nhan_xet_khach_hang.Dock = DockStyle.Left;
            //v_c01_quan_ly_gia.Dock = DockStyle.Left;
            //v_c01_chi_tiet_khuyen_mai.Dock = DockStyle.Left;
            //v_c01_tinh_tinh_trang_kinh_doanh.Dock = DockStyle.Left;
            //

        }

        public void data_2_mo_ta()
        {
            m_lbl_ten_san_pham.Text = v_hang_hoa_master.ten_hang_hoa;
            //m_lbl_hang_hoa_master.Text = "Mã sản phẩm: " + v_hang_hoa_master.ma_hang_hoa + " - Xuất xứ : " + v_hang_hoa_master.nha_san_xuat + " - Chất liệu: " + v_hang_hoa_master.chat_lieu;
        }
        public void data_2_mo_ta_v2()
        {
            m_lbl_ten_san_pham.Text = v_hang_hoa.ten;
            string tag = "";
            foreach (var item in v_hang_hoa.ds_tag)
            {
                tag += " " + item.ten_tag;
            }
            m_lbl_hang_hoa_master.Text = "Mã sản phẩm: " + v_hang_hoa.ma_hang_hoa + " - Xuất xứ : " + v_hang_hoa.nha_cung_cap.ten + " - " + tag;
        }

        #endregion

        #region event handler
        private void m_btn_xoa_san_pham_Click(object sender, EventArgs e)
        {
            try
            {
                decimal p = v_hang_hoa.id;
                if (this.ButtonDeleteClick != null)
                {
                    this.ButtonDeleteClick(p, e);
                }
            }
            catch (Exception v_E)
            {
                CommonFunction.exception_handle(v_E);
            }
        }
        #endregion
    }
}
