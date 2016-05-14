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
            this.Load += F11_bao_cao_doanh_thu_Load;
            m_btn_in_bao_cao.Click += M_btn_in_bao_cao_Click;
            m_btn_thoat.Click += M_btn_thoat_Click;
            m_btn_xem_bao_cao.Click += M_btn_xem_bao_cao_Click;
        }

        private void F11_bao_cao_doanh_thu_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception)
            {
                XtraMessageBox.Show(CommonMessage.MESSAGE_EXCEPTION);
            }
        }

        private void M_btn_xem_bao_cao_Click(object sender, EventArgs e)
        {
            try
            {
                BaoCaoDoanhThu.lay_doanh_thu_doanh_so(m_dat_thang_bat_dau.DateTime, m_dat_thang_ket_thuc.DateTime, this, data =>
                {
                    if (!data.Success)
                    {
                        XtraMessageBox.Show(CommonMessage.USER_INPUT_ERROR);
                    }
                    else
                    {
                        data_to_chart(data.Data);
                    }
                });
            }
            catch (Exception)
            {
                XtraMessageBox.Show(CommonMessage.MESSAGE_EXCEPTION);
            }

        }

        private void data_to_chart(List<BaoCaoDoanhThu.BaoCaoDoanhThuDoanhSo> data)
        {
            var table = CommonFunction.list_to_data_table<BaoCaoDoanhThu.BaoCaoDoanhThuDoanhSo>(data);
            //doanh so
            m_chart_bao_cao.Series[0].Points.Clear();
            m_chart_bao_cao_1.Series[0].Points.Clear();
            //
            XYDiagram diagram = m_chart_bao_cao.Diagram as XYDiagram;
            diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
            diagram.AxisX.Label.TextPattern = "{V:MM/yyyy}";

            XYDiagram diagram1 = m_chart_bao_cao_1.Diagram as XYDiagram;
            diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
            diagram.AxisX.Label.TextPattern = "{V:MM/yyyy}";
            //
            foreach (var item in data)
            {
                //doanh so
                m_chart_bao_cao.Series[0].Points.Add(new SeriesPoint((item.thang + "-" + item.nam).ToString(), item.tong_doanh_so));
                //doanh thu
                m_chart_bao_cao_1.Series[0].Points.Add(new SeriesPoint((item.thang + "-" + item.nam).ToString(), item.tong_doanh_thu));
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
                t.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                t.RestoreDirectory = true;
                if (t.ShowDialog() == DialogResult.OK)
                {
                    m_chart_bao_cao.ExportToPdf(t.FileName);
                    XtraMessageBox.Show(CommonMessage.EXPORT_SUCCESS);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show(CommonMessage.MESSAGE_EXCEPTION);
            }
        }

        private void m_chart_bao_cao_Click(object sender, EventArgs e)
        {

        }
    }
}
