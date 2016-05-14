using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryApi;
using QLBH.Common;
using QLBH.Controls.Hàng_hóa;
using System.Threading;
using QLBH.Forms;

namespace QLBH.Controls
{
    public partial class c01_quan_ly_hang_hoa : UserControl
    {
        #region public member
        public c01_danh_muc_hang_hoa v_c01_danh_muc_hang_hoa = new c01_danh_muc_hang_hoa();
        public c01_hang_hoa_chi_tiet v_c01_chi_tiet_hang_hoa = new c01_hang_hoa_chi_tiet();
        string key;
        int page=0;
        #endregion

        #region data binding type
        public List<HangHoa> list_hang_hoa { get; set; }
        public List<HangHoaMaster> list_hang_hoa_master { get; set; }
        //public List<LoaiHang> list_loai_hang { get; set; }
        #endregion

        #region public event handler

        #endregion

        
            #region public method
        public c01_quan_ly_hang_hoa()
        {
            InitializeComponent();
        }

        public c01_quan_ly_hang_hoa(List<QuanLyDanhMucHangHoa.LoaiHang> hang)
        {
            InitializeComponent();
            //v_c01_danh_muc_hang_hoa = new c01_danh_muc_hang_hoa(loai_hang);
            //m_pnl_danh_muc.Controls.Add(v_c01_danh_muc_hang_hoa);
            //v_c01_danh_muc_hang_hoa.Dock = DockStyle.Left;
            //v_c01_danh_muc_hang_hoa.ButtonHangHoaClick += V_danh_muc_hang_hoa_ButtonHangHoaClick;
            //test();
            
            khoiTaoLoaiHang(hang);
        }

        void khoiTaoLoaiHang(List<QuanLyDanhMucHangHoa.LoaiHang> loai_hang)
        {
            table_danh_muc.Length = () => { return loai_hang.Count; };
            table_danh_muc.CellAtIndex = i =>
            {
                var cell = new c01_item_danh_muc_loai_hang();
                cell.SetName(loai_hang[i].ten_tag);
                return cell;
            };
            table_danh_muc.DidSelectCellAtIndex = (i, sender) =>
            {
                moLoaiHang(loai_hang[i].ten_tag,0);                
            };
            table_danh_muc.ItemHighlight = sender =>
            {
                var control = sender as c01_item_danh_muc_loai_hang;
                control.SetHighlight();
            };
            table_danh_muc.ItemNormal = sender =>
            {
                var control = sender as c01_item_danh_muc_loai_hang;
                control.SetNormal();
            };
            table_danh_muc.Init();
            
            //if (loai_hang.Count > 0)
            //{
            //    moLoaiHang(loai_hang[0]);
            //}
        }

        void moLoaiHang(string keyword,int page)
        {
            QuanLyDanhMucHangHoa.TimKiemHangHoa(keyword,100, page, this, data =>
            {
                var l = data.Data;
                if (l.Count==0)
                {
                    MessageBox.Show("Không có luôn");
                    return;
                }
                table_danh_sach.NumberOfCellPerLine = () => { return 5; };
                table_danh_sach.Length = () => { return l.Count; };
                table_danh_sach.CellAtIndex = i =>
                {
                    var cell = new c01_item_hang_hoa();
                        cell.setDiem(l[i].diem);
                        cell.setGia(l[i].gia);
                        cell.setGiamGia(l[i].do_giam_gia);
                        cell.setIcon(l[i].ds_link[0]);
                        cell.setLuotClick(l[i].luot_click);
                        cell.setName(l[i].ten_hang_hoa);
                        cell.setSoComment(l[i].so_comment);
                    return cell;
                };
                table_danh_sach.DidSelectCellAtIndex = (i, sender) =>
                {
                    //MessageBox.Show("click " + i.ToString());
                    f05_chi_tiet_hang_hoa v_f = new f05_chi_tiet_hang_hoa();
                    QuanLyDanhMucHangHoa.HangHoaMaster hh = new QuanLyDanhMucHangHoa.HangHoaMaster();
                    var l1 = data.Data;
                    v_f.display(l1[i]);
                };
                table_danh_sach.Init();
            });
        }

        #endregion

        #region event handler

        #endregion

        private void V_danh_muc_hang_hoa_ButtonHangHoaClick(object sender, EventArgs e)
        {
            try
            {
                //xtraScrollableControl1.Controls.Clear();
                //int id_hang_hoa = Convert.ToInt16(sender);
                //HangHoa p = v_c01_danh_muc_hang_hoa.v_list_hang_hoa.Where(s => s.id == id_hang_hoa).First();
                //v_c01_chi_tiet_hang_hoa = new c01_hang_hoa_chi_tiet(p);
                //// m_tab_page_danh_muc_hang_hoa.Controls.Add(v_c01_chi_tiet_hang_hoa);
                //xtraScrollableControl1.Controls.Add(v_c01_chi_tiet_hang_hoa);
                //v_c01_chi_tiet_hang_hoa.Dock = DockStyle.Fill;
                //v_c01_chi_tiet_hang_hoa.ButtonDeleteClick += V_c_ButtonDeleteClick;
            }
            catch (Exception v_e)
            {
                CommonFunction.exception_handle(v_e);
            }
        }

        private void V_c_ButtonDeleteClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception v_e)
            {
                CommonFunction.exception_handle(v_e);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //khoiTaoLoaiHang(loai_hang);
        }

        private void c01_quan_ly_hang_hoa_Load(object sender, EventArgs e)
        {
            txtPage.Text = 0.ToString();
            searchBar.Search = k =>
            {
                key = k;
                moLoaiHang(k, page);
            };
            QuanLyDanhMucHangHoa.GetDanhSachLoaiHang(this, data =>
            {
                var loai_hang = data.Data;
                khoiTaoLoaiHang(loai_hang);
            });
        }

        private void txtPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!int.TryParse(txtPage.Text, out page))
                {
                    MessageBox.Show("Trang sai =.=");
                }
                else
                {
                    moLoaiHang(key, page);
                }
            }
        }
    }
}
