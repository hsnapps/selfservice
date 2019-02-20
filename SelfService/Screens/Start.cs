using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class Start : BaseForm
    {
        Login login;
        CommandsPanel commands;

        public Start() : base(true) {
            var x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            var y = (Screen.PrimaryScreen.Bounds.Height - CommandButton.DefaultHeight) / 2;

            var btn = new CommandButton(Resources.StartHere) {
                Left = x,
                Top = y,
                Height = 90,
            };

            btn.Click += OnStart;
            this.Controls.Add(btn);

#if DEBUG
            var close = new CommandButton(Resources.Exit) {
                Left = x,
                Top = y + CommandButton.DefaultHeight + 20,
                Height = 90,
            };

            close.Click += (s, e)=> { Close(); };
            this.Controls.Add(close);
#endif
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                Close();
            }
        }

        void OnStart(object sender, EventArgs e) {
            login = new Login();
            login.FormClosed += OnLoginClosed;
            login.Show(this);
        }

        void OnCommandsPanelCLosed(object sender, FormClosedEventArgs e) {
            switch (commands.Command) {
                case Commands.Letters:
                    SelectLetters letters = new SelectLetters();
                    letters.FormClosed += (s, v) => { DisplayCommands(); };
                    letters.Show(this);
                    break;

                case Commands.Calendar:
                    break;

                case Commands.Schedual:
                    Schedule schedule = new Schedule();
                    schedule.FormClosed += (s, v) => { DisplayCommands(); };
                    schedule.Show(this);
                    break;

                case Commands.Plan:
                    break;

                case Commands.Requests:
                    RequestsScreen requests = new RequestsScreen();
                    requests.FormClosed += (s, v) => { DisplayCommands(); };
                    requests.Show(this);
                    break;

                case Commands.StudentGuide:
                    StudentGuide guide = new StudentGuide();
                    guide.FormClosed += (s, v) => { DisplayCommands(); };
                    guide.Show();
                    break;

                case Commands.Suggestions:
                    break;

                case Commands.Map:
                    break;

                case Commands.CommingSubjects:
                    break;

                default:
                    BaseForm.Student = null;
                    break;
            }
        }

        void OnLoginClosed(object sender, FormClosedEventArgs e) {
            if (login.IsTimeout) return;

            if (login.IsLogged) {
                DisplayCommands();
            }
        }

        void DisplayCommands() {
            commands = new CommandsPanel();
            commands.FormClosed += OnCommandsPanelCLosed;
            commands.Show(this);
        }
    }
}
