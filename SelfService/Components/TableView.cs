using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService.Components
{
    class TableView : DataGridView
    {
        public TableView(DataGridViewRow rowTemplate) {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Dock = DockStyle.Fill;
            //Location = new Point(0, 0);
            Margin = new Padding(5);
            Name = "grid";
            ReadOnly = true;
            RightToLeft = RightToLeft.Yes;
            TabStop = false;
            AutoGenerateColumns = false;
            MultiSelect = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowTemplate = rowTemplate;
        }

        public TableView() : this(new DataGridViewRow { Height = 40 }) {}
    }
}
