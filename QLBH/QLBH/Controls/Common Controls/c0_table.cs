using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace QLBH.Controls.Common_Controls
{
    public partial class c0_table : UserControl
    {
        public c0_table()
        {
            InitializeComponent();
        }
        // Member
        int length = 0;
        public enum ScrollStyle
        {
            Vertical,
            Horizontal
        }
        public ScrollStyle Style { get; set; } = ScrollStyle.Vertical;
        // Data Source
        public delegate int DelLength();
        public delegate int DelNumberOfCellPerLine();
        public delegate Control DelCellAtIndex(int index);
        public delegate int DelHeightForCellAtIndex(int index);

        public DelLength Length;
        public DelNumberOfCellPerLine NumberOfCellPerLine;
        public DelCellAtIndex CellAtIndex;
        public DelHeightForCellAtIndex LengthForCellAtIndex;
        // Delegate
        public delegate void DelDidSelectCellAtIndex(int index, object sender);
        public DelDidSelectCellAtIndex DidSelectCellAtIndex;

        public delegate void DelItemHighlight(object item);
        public DelItemHighlight ItemHighlight;

        public delegate void DelItemNormal(object item);
        public DelItemHighlight ItemNormal;
        //
        //public void InitTable()
        //{
        //    try
        //    {
        //        table.Controls.Clear();
        //        length = Length();
        //        var a = NumberOfCellPerLine == null ? 1 : NumberOfCellPerLine();
        //        var b = (length + 1) / a;
        //        var temp = CellAtIndex(0);
        //        if (Style == ScrollStyle.Vertical)
        //        {
        //            columnCount = a;
        //            rowCount = b;
        //            defSize = (int)((temp.Height) / 0.8);
        //        }
        //        else
        //        {
        //            table.Dock = DockStyle.Left;
        //            columnCount = b;
        //            rowCount = a;
        //            defSize = (int)(temp.Width / 0.8);
        //        }

        //        table.ColumnCount = columnCount;
        //        table.ColumnStyles.Clear();
        //        for (int i = 0; i < columnCount; i++)
        //        {
        //            if (ScrollStyle.Vertical == Style)
        //            {
        //                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / columnCount));
        //            }
        //            else
        //            {
        //                float len = LengthForCellAtIndex == null ? defSize : LengthForCellAtIndex(i);
        //                table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, len));
        //            }
        //        }
        //        table.RowCount = rowCount;
        //        table.RowStyles.Clear();
        //        for (int i = 0; i < rowCount; i++)
        //        {
        //            if (Style == ScrollStyle.Vertical)
        //            {
        //                float len = LengthForCellAtIndex == null ? defSize : LengthForCellAtIndex(i);
        //                table.RowStyles.Add(new RowStyle(SizeType.Absolute, len));
        //            }
        //            else
        //            {
        //                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / rowCount));
        //            }
        //        }
        //        FillControl();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}
        //public void FillControl()
        //{
        //    for (int i = 0; i < length; i++)
        //    {
        //        var a = NumberOfCellPerLine == null ? 1 : NumberOfCellPerLine();
        //        int indexRow = Style == ScrollStyle.Vertical ? i / a : i % a;
        //        int indexColumn = Style == ScrollStyle.Vertical ? i % a : i / a;
        //        var control = CellAtIndex(i);
        //        if (control.Dock != DockStyle.Fill)
        //        {
        //            control.Anchor = AnchorStyles.None;
        //        }

        //        table.Controls.Add(control, indexColumn, indexRow);
        //        FillEvent(control, i);

        //    }
        //}

        public void Init()
        {
            init();
        }
        void init()
        {
            table.Controls.Clear();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            var count = NumberOfCellPerLine == null ? 1 : NumberOfCellPerLine();
            if (Style == ScrollStyle.Vertical)
            {
                for (int i = 0; i < count; i++)
                {
                    table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / count));
                }
                table.ColumnCount = count;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    table.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / count));
                }
                table.RowCount = count;
            }
            var len = Length();
            for (int i = 0; i < len; i++)
            {
                var control = CellAtIndex(i);
                Add(control, i);
            }
        }

        private void C0_table_Click(object sender, EventArgs e)
        {
            if (DidSelectCellAtIndex != null)
            {
                int index = (int)(sender as Control).Tag;
                DidSelectCellAtIndex(index, table.Controls[index]);
                setHighlight(index);
            }
        }

        public void Add(Control control, int index)
        {
            int indexRow = 0, indexColumn = 0;
            if (control.Dock != DockStyle.Fill)
            {
                control.Anchor = AnchorStyles.None;
            }
            if (Style == ScrollStyle.Vertical)
            {
                if (length >= table.RowCount * table.ColumnCount)
                {
                    table.RowCount++;
                    table.RowStyles.Add(new RowStyle(SizeType.Absolute, control.Height / 0.8F));
                }
                var a = table.ColumnCount;
                indexColumn = index % a;
                indexRow = index / a;
            }
            else
            {
                if (length >= table.RowCount * table.ColumnCount)
                {
                    table.ColumnCount++;
                    table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, control.Width / 0.8F));
                }
                var a = table.RowCount;
                indexColumn = index / a;
                indexRow = index % a;
            }
            FillEvent(control, index);
            length++;
            table.Controls.Add(control, indexColumn, indexRow);
        }
        public void Add(Control control)
        {
            Add(control, length);
        }
        // Private
        void setHighlight(int index)
        {
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
        private void FillEvent(Control control, int index)
        {
            control.Click += C0_table_Click;
            control.Tag = index;
            foreach (Control item in control.Controls)
            {
                FillEvent(item, index);
            }
        }
    }
}
