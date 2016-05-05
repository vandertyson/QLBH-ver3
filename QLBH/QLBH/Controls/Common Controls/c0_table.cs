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
    public partial class c0_table : UserControl
    {
        public c0_table()
        {
            InitializeComponent();
        }
        // Member
        int columnCount = 0;
        int rowCount = 0;
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

        //
        public void InitTable()
        {
            table.Controls.Clear();
            length = Length();
            var a = NumberOfCellPerLine();
            var b = (length + 1) / a;
            if (Style == ScrollStyle.Vertical)
            {
                columnCount = a;
                rowCount = b;
            }
            else
            {
                table.Dock = DockStyle.Left;
                columnCount = b;
                rowCount = a;
            }

            table.ColumnCount = columnCount;
            table.ColumnStyles.Clear();
            for (int i = 0; i < columnCount; i++)
            {
                if (ScrollStyle.Vertical == Style)
                {
                    table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / columnCount));
                }
                else
                {
                    float len = LengthForCellAtIndex(i);
                    table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, len));
                }
            }
            table.RowCount = rowCount;
            table.RowStyles.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                if (Style == ScrollStyle.Vertical)
                {
                    float len = LengthForCellAtIndex(i);
                    table.RowStyles.Add(new RowStyle(SizeType.Absolute, len));
                }
                else
                {
                    table.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / rowCount));
                }
            }
            for (int i = 0; i < length; i++)
            {
                int indexRow = Style == ScrollStyle.Vertical ? i / a : i % a;
                int indexColumn = Style == ScrollStyle.Vertical ? i % a : i / a;
                var control = CellAtIndex(i);
                if (control.Dock != DockStyle.Fill)
                {
                    control.Anchor = AnchorStyles.None;
                }

                table.Controls.Add(control, indexColumn, indexRow);
            }
        }
        public void Add(Control control, int index,int length)
        {
            int indexRow=0,indexColumn=0;
            if (control.Dock != DockStyle.Fill)
            {
                control.Anchor = AnchorStyles.None;
            }
            if (Style == ScrollStyle.Vertical)
            {
                if (length >= rowCount*columnCount)
                {
                    rowCount++;
                    table.RowStyles.Add(new RowStyle(SizeType.Absolute, length));
                }
                var a = columnCount;
                indexColumn = index % a;
                indexRow = index / a;
            }
            else
            {
                if (length >= rowCount * columnCount)
                {
                    columnCount++;
                    table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, length));
                }
                var a = rowCount;
                indexColumn = index / a;
                indexRow = index % a;
            }

            table.Controls.Add(control, indexColumn, indexRow);
        }
        public void Add(Control control)
        {
            Add(control, length, LengthForCellAtIndex(0));
        }
        // Private
    }
}
