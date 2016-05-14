using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using LibraryApi;
using DevExpress.XtraCharts;
using static LibraryApi.BaoCaoChiTietHangHoa;

namespace QLBH.Controls
{
    public partial class c01_nhan_xet_khach_hang : UserControl
    {
        public decimal id_hang_hoa { get; set; }
        public DateTime bat_dau { get; set; }
        public int so_thang { get; set; }
        public BaoCaoPhanHoi v_bao_cao_phan_hoi { get; set; }
        public c01_nhan_xet_khach_hang()
        {
            InitializeComponent();
        }
        public c01_nhan_xet_khach_hang(BaoCaoPhanHoi v_bc)
        {
            InitializeComponent();
            v_bao_cao_phan_hoi = v_bc;
            data_to_chart();
            data_to_nhan_xet_khach_hang();
            data_to_thong_ke();
        }
        public c01_nhan_xet_khach_hang(decimal id_hang_hoa, DateTime bat_dau, int so_thang)
        {
            InitializeComponent();
            LayBaoCaoPhanHoiKhachHang(id_hang_hoa, bat_dau, so_thang, this.TopLevelControl as Form, data =>
               {
                   v_bao_cao_phan_hoi = data.Data;
                   data_to_chart();
                   data_to_nhan_xet_khach_hang();
                   data_to_thong_ke();
               });
        }

        public void refresh_data()
        {
            
        }

        public void data_to_nhan_xet_khach_hang()
        {
            m_xtra_scroll_comment.Controls.Clear();
            var list_comments = v_bao_cao_phan_hoi.thong_ke_theo_thang.OrderByDescending(s => s.nam).OrderByDescending(s => s.thang).Select(s => s.comments).ToList();
            foreach (var item in list_comments)
            {
                if (item.Count == 0)
                {
                    continue;
                }
                item.Reverse();
                int i = 0;
                foreach (var comment in item)
                {
                    c01_comment_cua_khach_hang v_c = new c01_comment_cua_khach_hang(comment.nguoi_commnet.ten_khach_hang, comment.noi_dung, comment.thoi_gian);
                    //
                    Panel pn = new Panel();
                    pn.BorderStyle = BorderStyle.None;        
                    //           
                    pn.Controls.Add(v_c);
                    if (i % 2 == 0)
                    {
                        v_c.Dock = DockStyle.Left;
                    }
                    else
                    {
                        v_c.Dock = DockStyle.Right;
                    }
                    //
                    this.m_xtra_scroll_comment.Controls.Add(pn);
                    pn.Dock = DockStyle.Top;
                    i++;
                }
            }
        }
        
        public void data_to_chart()
        {
            foreach (var item in v_bao_cao_phan_hoi.thong_ke_theo_thang)
            {
                string thang_nam = "Tháng " + item.thang + "/" + item.nam;
                m_chart_comment.Series[0].Points.Add(new SeriesPoint(thang_nam,item.comments.Count));
                m_chart_view.Series[0].Points.Add(new SeriesPoint(thang_nam,item.luot_xem.Count));
            }
        }

        public void data_to_thong_ke()
        {
            m_lbl_rating.Text = String.Format("{0:0.00}",v_bao_cao_phan_hoi.rating);
            m_lbl_tong_so_view.Text = v_bao_cao_phan_hoi.views.ToString();
            m_lbl_yeu_thich.Text = v_bao_cao_phan_hoi.duoc_yeu_thich.ToString();
        }
    }
}
