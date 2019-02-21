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
        Timer _timer;
        Video video;

        public Start() : base(true) {
            var x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            var y = (Screen.PrimaryScreen.Bounds.Height - CommandButton.DefaultHeight) / 2;

            var btn = new CommandButton(Resources.StartHere) {
                Left = x,
                Top = y,
                Height = 90,
            };

            btn.Click += OnStart;
            Controls.Add(btn);

            video = new Video();
            video.FormClosed += (s, e) => { _timer.Start(); };

            _timer = new Timer {
                Interval = DB.Execute.GetTimeout(),
                Enabled = true,
            };
            _timer.Tick += (s, e) => {
                FormCollection fc = Application.OpenForms;
                if(fc.Count == 1) {
                    if (video == null) video = new Video();
                    video.Show();
                    (s as Timer).Stop();
                }
            };

#if DEBUG
            var close = new CommandButton(Resources.Exit) {
                Left = x,
                Top = y + CommandButton.DefaultHeight + 20,
                Height = 90,
            };

            close.Click += (s, e)=> { Close(); };
            Controls.Add(close);
#endif
        }

#if DEBUG
        protected override void OnKeyUp(KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                Close();
            }
        }
#endif

        void OnStart(object sender, EventArgs e) {
            login = new Login();
            login.FormClosed += OnLoginClosed;
            login.Show(this);
        }

        void OnCommandsPanelCLosed(object sender, FormClosedEventArgs e) {
            switch (commands.Command) {
                case Commands.Letters:
                    DisplayForm(new SelectLetters());
                    break;

                case Commands.Calendar:
                    break;

                case Commands.Schedual:
                    DisplayForm(new Schedule());
                    break;

                case Commands.Plan:
                    break;

                case Commands.Requests:
                    DisplayForm(new RequestsScreen());
                    break;

                case Commands.StudentGuide:
                    DisplayForm(new StudentGuide());
                    break;

                case Commands.Suggestions:
                    DisplayForm(new Claiming());
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

        void DisplayForm(BaseForm form) {
            form.FormClosed += (s, v) => {
                DisplayCommands();
            };
            form.Show();            
        }
    }
}
