using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class RestCourses : BaseForm
    {
        public RestCourses() {
            var courses = DB.Execute.GetRestCourses();
            var integerStyle = new DataGridViewCellStyle {
                Format = "N0",
                NullValue = 0,
                Font = new Font(Fonts.TimesNewRoman, 16),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            var style = new DataGridViewCellStyle {
                NullValue = "",
                Font = new Font(Fonts.ALMohanad, 16),
            };

            DataGridViewTextBoxColumn course_code = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "course_code",
                HeaderText = "رمز المقرر",
                Name = "course_code",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn title = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "title",
                HeaderText = "اسم المقرر",
                Name = "title",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn approved_units_completed = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "approved_units_completed",
                HeaderText = "الوحدات المعتمدة المنجزة",
                Name = "approved_units_completed",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };
            DataGridViewTextBoxColumn approved_units_required = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "approved_units_required",
                HeaderText = "الوحدات المعتمدة المطلوبة",
                Name = "approved_units_required",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };
            DataGridViewTextBoxColumn courses_completed = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "courses_completed",
                HeaderText = "عدد المقررات المنجزة",
                Name = "courses_completed",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };
            DataGridViewTextBoxColumn courses_required = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "courses_required",
                HeaderText = "عدد المقررات المطلوبة",
                Name = "courses_required",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };
            DataGridViewTextBoxColumn approved_units_required_for_program = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "approved_units_required_for_program",
                HeaderText = "الوحدات المعتمدة المطلوبة للبرنامج",
                Name = "passed_units",
                ReadOnly = true,
                DefaultCellStyle = integerStyle,
            };

            TableView grid = new TableView();
            grid.Columns.AddRange(new DataGridViewColumn[] {
                course_code,
                title,
                approved_units_completed,
                approved_units_required,
                courses_completed,
                courses_required,
                approved_units_required_for_program,
            });
            grid.DataSource = courses;

            Font = new Font(Fonts.ALMohanad, 13);

            Footer footer = new Footer(CommandButton.DefaultHeight + 120, new Padding(0, 5, 0, 5), Resources.Back, Resources.Print);
            footer.SetCallback(0, (s, e) => { Close(); });
            footer.SetCallback(1, (s, e) => {
                if (DB.Execute.CanPrint("RestCourses")) {
                    Tools.PrintDataGrid(grid, Resources.CommingSubjects, 60, 41);
                }
            });

            Padding = new Padding(0, 150, 0, 0);
            Controls.AddRange(new Control[] { grid, footer });
        }
    }
}
