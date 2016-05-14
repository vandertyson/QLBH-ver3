using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBH.Common;
using static LibraryApi.BaoCaoChiTietHangHoa;
using DevExpress.XtraCharts;

namespace QLBH.Controls
{
    public partial class c01_chi_tiet_khuyen_mai : UserControl
    {
        private BaoCaoKhuyenMai v_bao_cao_km;
        private DotKhuyenMai m_current_lich_su;
        private LibraryApi.HangHoa m_hang_hoa;
        private decimal m_id_hang_hoa;
        public c01_chi_tiet_khuyen_mai()
        {
            InitializeComponent();
            set_define_event();
        }

        public c01_chi_tiet_khuyen_mai(BaoCaoKhuyenMai v_bc, LibraryApi.HangHoa hang_hoa)
        {
            InitializeComponent();
            set_define_event();
            this.v_bao_cao_km = v_bc;
            m_hang_hoa = hang_hoa;
            if (v_bao_cao_km.dot_khuyen_mai_hien_tai == null)
            {
                var p = v_bao_cao_km.lich_su.OrderByDescending(s => s.thoi_gian_ket_thuc).First();
                m_current_lich_su = p;
            }
            else
            {
                m_current_lich_su = v_bao_cao_km.dot_khuyen_mai_hien_tai;
            }
            data_to_hien_tai();
            data_to_lich_su();
        }
        public c01_chi_tiet_khuyen_mai(LibraryApi.HangHoa hang)
        {
            InitializeComponent();
            m_id_hang_hoa = hang.id;
            LayBaoCaoChiTietKhuyenMai(m_id_hang_hoa, DateTime.Now, this.TopLevelControl as Form, data =>
            {
                this.v_bao_cao_km = data.Data;
                m_current_lich_su = v_bao_cao_km.dot_khuyen_mai_hien_tai;
                data_to_hien_tai();
                data_to_lich_su();
            });
        }

        private void set_define_event()
        {
            m_sle_chon_dot_km.EditValueChanged += M_sle_chon_dot_km_EditValueChanged;
        }

        private void M_sle_chon_dot_km_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_sle_chon_dot_km.EditValue == null)
                {
                    var p = v_bao_cao_km.lich_su.OrderByDescending(s => s.thoi_gian_ket_thuc).First();
                    m_current_lich_su = p;
                    data_2_chart();
                    return;
                }
                string ma = m_sle_chon_dot_km.EditValue.ToString();
                decimal id_dot = find_id_dot_km_theo_ma(ma);
                m_current_lich_su = v_bao_cao_km.lich_su.Where(s => s.id == id_dot).First();
                data_2_chart();
            }
            catch (Exception ex)
            {
                CommonFunction.exception_handle(ex);
            }
        }

        private decimal find_id_dot_km_theo_ma(string ma)
        {
            foreach (var item in v_bao_cao_km.lich_su)
            {
                if (item.ma_dot == ma)
                {
                    return item.id;
                }
            }
            return 0;
        }

        private void data_to_lich_su()
        {
            data_2_sle_dot_km();
            data_2_chart();
        }

        private void data_2_chart()
        {
            
            //
            m_lbl_so_luot_mua.Text = m_current_lich_su.luot_mua.ToString();
            m_lbl_so_luot_xem.Text = m_current_lich_su.luot_xem.ToString();
            m_lbl_so_tien_ban_duoc.Text = String.Format("{0:#,##0 VND}", m_current_lich_su.so_tien_ban_duoc);
            //
            //doanh so
            m_chart_khuyen_mai.Series[0].Points.Clear();
            m_chart_khuyen_mai.Series[0].Points.Add(new SeriesPoint(m_hang_hoa.ten, m_current_lich_su.so_luong_ban_duoc));
            m_chart_khuyen_mai.Series[0].Points.Add(new SeriesPoint("Phần còn lại", m_current_lich_su.tong_doanh_so - m_current_lich_su.so_luong_ban_duoc));

            //doanh thu
            m_chart_khuyen_mai.Series[1].Points.Clear();
            m_chart_khuyen_mai.Series[1].Points.Add(new SeriesPoint(m_hang_hoa.ten, m_current_lich_su.so_tien_ban_duoc));
            m_chart_khuyen_mai.Series[1].Points.Add(new SeriesPoint("Phần còn lại", m_current_lich_su.tong_doanh_thu - m_current_lich_su.so_tien_ban_duoc));
        }

        private void data_2_sle_dot_km()
        {
            if (v_bao_cao_km.dot_khuyen_mai_hien_tai != null)
            {
                v_bao_cao_km.lich_su.Add(v_bao_cao_km.dot_khuyen_mai_hien_tai);
            }
            m_sle_chon_dot_km.Properties.DataSource = CommonFunction.list_to_data_table<DotKhuyenMai>(v_bao_cao_km.lich_su);
            m_sle_chon_dot_km.Properties.DisplayMember = "mo_ta";
            m_sle_chon_dot_km.Properties.ValueMember = "ma_dot";

            m_sle_chon_dot_km.Properties.View.BestFitColumns();
        }

        private void data_to_hien_tai()
        {
            if (v_bao_cao_km.dot_khuyen_mai_hien_tai == null)
            {
                m_lbl_muc_khuyen_mai.Text = "0%";
                m_lbl_ten_dot_km.Text = "Không có đợt khuyến mãi nào";
                m_lbl_thoi_gian_ap_dung.Text = "Không có đợt khuyến mãi nào";

                return;
            }
            m_lbl_muc_khuyen_mai.Text = v_bao_cao_km.dot_khuyen_mai_hien_tai.muc_khuyen_mai.ToString();
            m_lbl_ten_dot_km.Text = v_bao_cao_km.dot_khuyen_mai_hien_tai.mo_ta;
            m_lbl_thoi_gian_ap_dung.Text = v_bao_cao_km.dot_khuyen_mai_hien_tai.thoi_gian_bat_dau.Date + " - " + v_bao_cao_km.dot_khuyen_mai_hien_tai.thoi_gian_ket_thuc;
        }


    }
}
