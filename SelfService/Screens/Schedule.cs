using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class Schedule : BaseForm
    {
        public Schedule() {
            Padding = new Padding(0, 110, 0, 0);

            var schedules = DB.Execute.GetSchedule();
            var style = new DataGridViewCellStyle {
                NullValue = "",
                Font = new Font(Fonts.TimesNewRoman, 16),
            };
            var rowTemplate = new DataGridViewRow { Height = 40 };

            DataGridViewTextBoxColumn course_symbol = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "course_symbol",
                HeaderText = "رمز المادة",
                Name = "course_symbol",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn supervisor_name = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "supervisor_name",
                HeaderText = "اسم المدرب",
                Name = "supervisor_name",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn course_name = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "course_name",
                HeaderText = "اسم المادة",
                Name = "course_name",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridView grid = new DataGridView {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                Name = "grid",
                ReadOnly = true,
                RightToLeft = RightToLeft.Yes,
                TabStop = false,
                //AutoGenerateColumns = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowTemplate = rowTemplate,
                BackgroundColor = Color.White,
            };
            grid.Columns.AddRange(new DataGridViewColumn[] {
                course_symbol,
                course_name,
                supervisor_name,                
            });
            grid.DataSource = schedules;

            Footer footer = new Footer(CommandButton.DefaultHeight + 40, new Padding(0, 5, 0, 5), Resources.Back, Resources.Print);
            footer.SetCallback(0, (s, e) => { Close(); });
            footer.SetCallback(1, (s, e) => { Tools.PrintDataGrid(grid, false); });

            Padding = new Padding(0, 125, 0, 0);
            Controls.AddRange(new Control[] { grid, footer });
        }
    }
}
