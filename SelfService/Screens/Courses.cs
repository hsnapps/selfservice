using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class Courses : BaseForm
    {
        public Courses() {
            var courses = DB.Execute.GetCourses();
            var decimalStyle = new DataGridViewCellStyle {
                Format = "N2",
                NullValue = 0,
                Font = new Font(Fonts.TimesNewRoman, 16),
            };
            var integerStyle = new DataGridViewCellStyle {
                Format = "N0",
                NullValue = 0,
                Font = new Font(Fonts.TimesNewRoman, 16),
            };
            var style = new DataGridViewCellStyle {
                NullValue = "",
                Font = new Font(Fonts.ALMohanad, 16),
            };
            var rowTemplate = new DataGridViewRow { Height = 40 };

            DataGridViewTextBoxColumn registered = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "registered",
                HeaderText = "مسجل حاليا",
                Name = "registered",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn completed = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "completed",
                HeaderText = "مستوفى",
                Name = "completed",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn authorized_units = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "authorized_units",
                HeaderText = "الوحدات المعتمدة للمقرر",
                Name = "authorized_units",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };
            DataGridViewTextBoxColumn course_name = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader,
                DataPropertyName = "course_name",
                HeaderText = "إسم المقرر",
                Name = "course_name",
                ReadOnly = true,
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn course_symbol = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "course_symbol",
                HeaderText = "رمز المقرر",
                Name = "course_symbol",
                ReadOnly = true,
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn gpa = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "gpa",
                HeaderText = "المعدل التراكمي",
                Name = "gpa",
                ReadOnly = true,
                DefaultCellStyle = decimalStyle,
            };
            DataGridViewTextBoxColumn passed_units = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "passed_units",
                HeaderText = "الوحدات المعتمدة المنجزة",
                Name = "passed_units",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };
            DataGridViewTextBoxColumn required_units = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "required_units",
                HeaderText = "الوحدات المعتمدة المطلوبة",
                Name = "required_units",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };
            DataGridViewTextBoxColumn passed_subjects = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "passed_subjects",
                HeaderText = "عدد المقررات المنجزة",
                Name = "passed_subjects",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };
            DataGridViewTextBoxColumn required_subjects = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "required_subjects",
                HeaderText = "عدد المقررات المطلوبة",
                Name = "required_subjects",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };
            DataGridView grid = new DataGridView {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Margin = new Padding(5),
                Name = "grid",
                ReadOnly = true,
                RightToLeft = RightToLeft.Yes,
                TabStop = false,
                AutoGenerateColumns = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowTemplate = rowTemplate
            };
            grid.Columns.AddRange(new DataGridViewColumn[] {
                registered,
                completed,
                authorized_units,
                course_name,
                course_symbol,
                gpa,
                passed_units,
                required_units,
                passed_subjects,
                required_subjects,
            });
            grid.DataSource = courses;

            Font = new Font(Fonts.ALMohanad, 13);

            Footer footer = new Footer(CommandButton.DefaultHeight + 40, new Padding(0, 5, 0, 5), Resources.Back, Resources.Print);
            footer.SetCallback(0, (s, e) => { Close(); });
            footer.SetCallback(1, (s, e) => { Tools.PrintDataGrid(grid); });

            Padding = new Padding(0, 125, 0, 0);
            Controls.AddRange(new Control[] { grid, footer });
        }
    }
}
