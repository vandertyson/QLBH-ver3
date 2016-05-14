using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBH.Controls.Hàng_hóa;

namespace QLBH.Forms
{
    public partial class Test2 : Form
    {
        public Test2()
        {
            InitializeComponent();
        }

        private void Test2_Load(object sender, EventArgs e)
        {
            var list = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                list.Add(i.ToString());
            }
            myTable.Length = () => { return list.Count; };
            myTable.CellAtIndex = index => 
            {
                var control = new c01_item_hang_hoa();
                control.setName(list[index]);
                return control;
            };
            myTable.DidSelectCellAtIndex = (index, item,even) =>
            {
                var control = item as c01_item_hang_hoa;
                if (even.Button == MouseButtons.Left)
                {
                    myTable.SetOneItemHighlight(index);
                }
                if (even.Button == MouseButtons.Right)
                {
                    myTable.SetItemHighLight(index);
                }
            };
            myTable.ItemHighlight = item =>
            {
                var control = item as c01_item_hang_hoa;
                control.setHighLight();
            };
            myTable.ItemNormal = item =>
            {
                var control = item as c01_item_hang_hoa;
                control.setNormal();
            };
            myTable.Init();
        }
    }
}
