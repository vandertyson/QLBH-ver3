using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryApi;
using DevExpress.XtraEditors;
using QLBH.Common;
using DevExpress.XtraCharts;

namespace QLBH.Forms
{
    public partial class f11_bao_cao_doanh_thu : Form
    {
        public f11_bao_cao_doanh_thu()
        {
            InitializeComponent();
            this.CenterToScreen();
            set_defined_event();
            
        }
        private void set_defined_event()
        {
            m_btn_in_bao_cao.Click += M_btn_in_bao_cao_Click;
            m_btn_thoat.Click += M_btn_thoat_Click;
            m_btn_xem_bao_cao.Click += M_btn_xem_bao_cao_Click;
        }

        private void M_btn_xem_bao_cao_Click(object sender, EventArgs e)
        {
            try
            {
                BaoCaoDoanhThu.lay_doanh_thu_doanh_so(m_dat_thang_bat_dau.DateTime, m_dat_thang_ket_thuc.DateTime, this, data =>
                {
                    if (!data.Success)
                    {
                        XtraMessageBox.Show("Xảy ra lỗi vui lòng kiểm tra lại đầu vào");
                    }
                    else
                    {
                        data_to_chart(data.Data);
                    }
                });
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void data_to_chart(List<BaoCaoDoanhThu.BaoCaoDoanhThuDoanhSo> data)
        {
            var table = CommonFunction.list_to_data_table<BaoCaoDoanhThu.BaoCaoDoanhThuDoanhSo>(data);
            m_chart_bao_cao.DataSource = table;
            //doanh so
            foreach (var item in data)
            {
                m_chart_bao_cao.Series[0].Points.Add(new SeriesPoint(item.thang, item.tong_doanh_so));
                          //doanh thu
                m_chart_bao_cao_1.Series[0].Points.Add(new SeriesPoint(item.thang, item.tong_doanh_thu));
               
            }
           
        }


        private void M_btn_thoat_Click(object sender, EventArgs e)
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

        private void M_btn_in_bao_cao_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog t = new SaveFileDialog();
                if (t == null)
                {
                    return;
                }
                m_chart_bao_cao.ExportToPdf(t.FileName);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void m_chart_bao_cao_Click(object sender, EventArgs e)
        {

        }
    }
}
