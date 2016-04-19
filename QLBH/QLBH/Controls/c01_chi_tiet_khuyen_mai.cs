﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryApi;
using LibraryApi.ChiTietHangHoa.BaoCaoKhuyenMai;

namespace QLBH.Controls
{
    public partial class c01_chi_tiet_khuyen_mai : UserControl
    {
        private BaoCaoKhuyenMai v_bao_cao_km;
        private DotKhuyenMai m_current_lich_su;

        public c01_chi_tiet_khuyen_mai()
        {
            InitializeComponent();
            set_define_event();
        }

        public c01_chi_tiet_khuyen_mai(BaoCaoKhuyenMai ip)
        {
            InitializeComponent();
            this.v_bao_cao_km = ip;
            m_current_lich_su = v_bao_cao_km.dot_khuyen_mai_hien_tai;
            data_to_hien_tai();
            data_to_lich_su();
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
                    m_current_lich_su = v_bao_cao_km.dot_khuyen_mai_hien_tai;
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
                Common.exception_handle(ex);
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

        private DataTable get_danh_sach_dot_km()
        {
            DataTable result = new DataTable();
            //
            DataColumn col1 = new DataColumn();
            col1.ColumnName = "MaDot";
            col1.DataType = typeof(string);
            col1.Caption = "Mã đợt";

            DataColumn col2 = new DataColumn();
            col2.ColumnName = "TenDot";
            col2.DataType = typeof(string);
            col2.Caption = "Mô tả";

            result.Columns.Add(col1);
            result.Columns.Add(col2);

            foreach (var item in v_bao_cao_km.lich_su)
            {
                string ma = item.ma_dot;
                string ten_dot = item.mo_ta;
                result.Rows.Add(new object[] { ma, ten_dot });
            }
            result.Rows.Add(new object[] { m_current_lich_su.ma_dot, m_current_lich_su.mo_ta });
            return result;
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
            m_lbl_so_tien_ban_duoc.Text = m_current_lich_su.tong_doanh_thu.ToString();
            //
           
        }

        private void data_2_sle_dot_km()
        {
            m_sle_chon_dot_km.Properties.DataSource = get_danh_sach_dot_km();
            m_sle_chon_dot_km.Properties.DisplayMember = "TenDot";
            m_sle_chon_dot_km.Properties.ValueMember = "MaDot";

            m_sle_chon_dot_km.Properties.View.BestFitColumns();
        }

        private void data_to_hien_tai()
        {
            m_lbl_muc_khuyen_mai.Text = v_bao_cao_km.dot_khuyen_mai_hien_tai.muc_khuyen_mai.ToString();
            m_lbl_ten_dot_km.Text = v_bao_cao_km.dot_khuyen_mai_hien_tai.mo_ta;
            m_lbl_thoi_gian_ap_dung.Text = v_bao_cao_km.dot_khuyen_mai_hien_tai.thoi_gian_bat_dau.Date + " - " + v_bao_cao_km.dot_khuyen_mai_hien_tai.thoi_gian_ket_thuc;
        }


    }
}
