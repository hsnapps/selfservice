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
            var dailySchedules = DB.Execute.GetDailySchedule();
            var examSchedules = DB.Execute.GetExamSchedule();
            var style = new DataGridViewCellStyle {
                NullValue = "",
                Font = new Font(Fonts.TimesNewRoman, 16),
            };
            var rowTemplate = new DataGridViewRow { Height = 40 };
            TableView grid1 = new TableView();
            TableView grid2 = new TableView();
            TabPage page1 = new TabPage {
                Padding = new Padding(3),
                RightToLeft = RightToLeft.Yes,
                TabIndex = 0,
                Text = "الجدول الدراسي",
            };
            TabPage page2 = new TabPage {
                Padding = new Padding(3),
                RightToLeft = RightToLeft.Yes,
                TabIndex = 0,
                Text = "جدول الاختبارات",
            };
            TabControl tabControl = new TabControl {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                RightToLeftLayout = true,
                SelectedIndex = 0,
                TabStop = false,
                SizeMode = TabSizeMode.FillToRight,
                Appearance = TabAppearance.FlatButtons,
            };
            Footer footer = new Footer(CommandButton.DefaultHeight + 120, new Padding(0, 5, 0, 5), Resources.Back, Resources.Print);

            grid1.Columns.AddRange(GetDailySchedulesColumns(style));
            grid1.DataSource = dailySchedules;

            grid2.Columns.AddRange(GetExamSchedulesColumns(style));
            grid2.DataSource = examSchedules;

            page1.Controls.Add(grid1);
            page2.Controls.Add(grid2);

            tabControl.Controls.Add(page1);
            tabControl.Controls.Add(page2);

            footer.SetCallback(0, (s, e) => { Close(); });
            footer.SetCallback(1, (s, e) => {
                if (tabControl.SelectedIndex == 0) {
                    Tools.PrintDataGrid(grid1, "الجدول الدراسي", 60, 41);
                } else {
                    Tools.PrintDataGrid(grid2, "جدول الاختبارات", 60, 41);
                }
            });

            Padding = new Padding(0, 150, 0, 0);
            Controls.AddRange(new Control[] { tabControl, footer });
        }

        DataGridViewColumn[] GetDailySchedulesColumns(DataGridViewCellStyle style) {
            DataGridViewTextBoxColumn course_code = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "course_code",
                HeaderText = "رمز المادة",
                Name = "course_code",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn course_title = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "course_title",
                HeaderText = "اسم المادة",
                Name = "course_title",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn group_type = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "group_type",
                HeaderText = "نوع الشعبة",
                Name = "group_type",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn days = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "days",
                HeaderText = "الأيام",
                Name = "days",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn times = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "times",
                HeaderText = "الأوقات",
                Name = "times",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn hall = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "hall",
                HeaderText = "المبنى",
                Name = "hall",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn trainer = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "trainer",
                HeaderText = "المدرب",
                Name = "trainer",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };

            var cols = new DataGridViewColumn[] {
                course_code,
                course_title,
                group_type,
                days,
                times,
                hall,
                trainer,
            };

            return cols;
        }

        DataGridViewColumn[] GetExamSchedulesColumns(DataGridViewCellStyle style) {
            DataGridViewTextBoxColumn code = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "code",
                HeaderText = "رمز المادة",
                Name = "code",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn title = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "title",
                HeaderText = "اسم المادة",
                Name = "title",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn group = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "group",
                HeaderText = "نوع الشعبة",
                Name = "group",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn exa_date = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "exa_date",
                HeaderText = "التاريخ",
                Name = "exa_date",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn exam_time = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "exam_time",
                HeaderText = "الفترة",
                Name = "exam_time",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn time = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "time",
                HeaderText = "الوقت",
                Name = "time",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };
            DataGridViewTextBoxColumn place1 = new DataGridViewTextBoxColumn {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "place1",
                HeaderText = "القاعة",
                Name = "place1",
                ReadOnly = true,
                ValueType = typeof(String),
                DefaultCellStyle = style,
            };

            var cols = new DataGridViewColumn[] {
                code,
                title,
                group,
                exa_date,
                exam_time,
                time,
                place1,
            };

            return cols;
        }
    }
}
