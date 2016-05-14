using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH.Controls.Common_Controls
{
    public partial class c0_flowLayout : UserControl
    {
        public c0_flowLayout()
        {
            InitializeComponent();
        }
        #region Data Source
        public delegate int DelLength();
        public DelLength Length;

        public delegate Control DelCellAtIndex(int index);
        public DelCellAtIndex CellAtIndex;
        #endregion
        #region Delegate
        public delegate void DelDidSelectCellAtIndex(int index, object sender, MouseEventArgs e);
        public DelDidSelectCellAtIndex DidSelectCellAtIndex;

        public delegate void DelItemHighlight(object item);
        public DelItemHighlight ItemHighlight;

        public delegate void DelItemNormal(object item);
        public DelItemHighlight ItemNormal;
        #endregion
        #region Member
        FlowDirection Style= FlowDirection.LeftToRight;
        #endregion
        public void Init()
        {
            var length = Length();
            table.Controls.Clear();
            table.FlowDirection = Style;
            for (int i = 0; i < length; i++)
            {
                var control = CellAtIndex(i);
                FillEvent(control, i);
                table.Controls.Add(control);
            }
        }
        public void SetOneItemHighlight(int index)
        {
            var length = Length();
            if (ItemNormal != null)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i == index)
                    {
                        continue;
                    }
                    ItemNormal(table.Controls[i]);
                }
            }
            ItemHighlight?.Invoke(table.Controls[index]);
        }
        public void SetItemHighLight(int index)
        {
            ItemHighlight?.Invoke(table.Controls[index]);
        }
        public void SetItemNormal(int index)
        {
            ItemNormal?.Invoke(table.Controls[index]);
        }
        private void FillEvent(Control control, int index)
        {
            control.MouseDown += mouseDownClick;
            control.Tag = index;
            foreach (Control item in control.Controls)
            {
                FillEvent(item, index);
            }
        }

        private void mouseDownClick(object sender, MouseEventArgs e)
        {
            int index = (int)(sender as Control).Tag;
            DidSelectCellAtIndex?.Invoke(index, table.Controls[index],e);
        }
    }
}
