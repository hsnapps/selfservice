using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class Start : BaseForm
    {
        Login login;
        CommandsPanel commands;
        Timer _timer;
        int _timeout;
        int _timerTick;
        CommandButton start;
        CommandButton close;
        AxWMPLib.AxWindowsMediaPlayer player;
        readonly string url;

        public Start() : base(true) {
            this.Name = "Start";

            string selection = DB.Execute.GetVideoSelection();
            switch (selection) {
                case "web":
                    url = DB.Execute.GetVideoUrl();
                    break;
                case "youtube":
                    url = DB.Execute.GetYoutubeUrl();
                    break;
                default:
                    url = DB.Execute.GetVideoPath();
                    break;
            }

            InitializeVideo();

            var x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            var y = (Screen.PrimaryScreen.Bounds.Height - CommandButton.DefaultHeight) / 2;

            start = new CommandButton(Resources.StartHere) {
                Left = x,
                Top = y,
                Height = 90,
            };

            start.Click += OnStart;
            Controls.Add(start);

            //video = new Video();
            //video.FormClosed += (s, e) => { _timer.Start(); };

            _timeout = DB.Execute.GetTimeout();
            _timer = new Timer {
                Interval = 1000,
                Enabled = true,
            };
            _timer.Tick += OnTimer;

            string[] args = System.Environment.GetCommandLineArgs();
            bool exit = false;
            foreach (var arg in args) {
                if (arg.StartsWith("--exit=")) {
                    exit = arg.EndsWith("true") || arg.EndsWith("1");
                }
            }
            if (exit) {
                close = new CommandButton(Resources.Exit) {
                    Left = x,
                    Top = y + CommandButton.DefaultHeight + 20,
                    Height = 90,
                };

                close.Click += (s, e) => { Close(); };
                Controls.Add(close);
            }
        }

        void OnTimer(object s, EventArgs e) {
            _timerTick++;
            if (_timerTick < _timeout) {
                return;
            }

            // FormCollection forms = Application.OpenForms;
            IEnumerator formsCollection = Application.OpenForms.GetEnumerator();
            List<Form> forms = new List<Form>();

            while (formsCollection.MoveNext()) {
                forms.Add((Form)formsCollection.Current);                
            };

            for (int i = 0; i < forms.Count; i++) {
                Form form = forms[i];
                if (form.Name != "Start") {
                    form.Close();
                }
            }

            start.Visible = false;
#if DEBUG
            close.Visible = false;
#endif
            player.Visible = true;
            player.URL = url;
            _timerTick = 0;
            (s as Timer).Stop();
        }

        void InitializeVideo() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Video));
            player = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(player)).BeginInit();

            player.Enabled = true;
            player.Location = new System.Drawing.Point(183, 65);
            player.Name = "player";
            player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("player.OcxState")));
            player.Size = new System.Drawing.Size(417, 275);
            player.TabIndex = 0;
            player.ClickEvent += new AxWMPLib._WMPOCXEvents_ClickEventHandler(OnPlayerClickEvent);
            player.Visible = false;
            player.Bounds = this.Bounds;
            player.EndOfStream += OnEndOfStream;

            Controls.Add(player);
            ((System.ComponentModel.ISupportInitialize)(player)).EndInit();
        }

        void OnEndOfStream(object s, AxWMPLib._WMPOCXEvents_EndOfStreamEvent e) {
            (s as AxWMPLib.AxWindowsMediaPlayer).URL = url;
        }

        void OnPlayerClickEvent(object s, AxWMPLib._WMPOCXEvents_ClickEvent e) {
            (s as AxWMPLib.AxWindowsMediaPlayer).Visible = false;
            (s as AxWMPLib.AxWindowsMediaPlayer).URL = "";
            start.Visible = true;
#if DEBUG
            close.Visible = true;
#endif
            _timer.Start();
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
                    // Log added to each selection
                    DisplayForm(new SelectLetters());
                    break;

                case Commands.Calendar:
                    DB.Execute.Log("calendar", "");
                    DisplayForm(new Calendar());
                    break;

                case Commands.Schedual:
                    DB.Execute.Log("schedual", "");
                    DisplayForm(new Schedule());
                    break;

                case Commands.Plan:
                    DB.Execute.Log("plan", "");
                    DisplayForm(new Plan());
                    break;

                case Commands.Requests:
                    // Log added to each selection
                    DisplayForm(new RequestsScreen());
                    break;

                case Commands.StudentGuide:
                    DB.Execute.Log("studentguide", "");
                    DisplayForm(new StudentGuide());
                    break;

                case Commands.Suggestions:
                    DisplayForm(new Claiming());
                    break;

                case Commands.Map:
                    DB.Execute.Log("map", "");
                    DisplayForm(new Map());
                    break;

                case Commands.RestCourses:
                    DB.Execute.Log("commingsubjects", "");
                    DisplayForm(new RestCourses());
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
