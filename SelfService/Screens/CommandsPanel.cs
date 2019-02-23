using SelfService.Code;
using SelfService.Components;
using SelfService.Models;
using SelfService.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class CommandsPanel : BaseForm
    {
        const int DEFAULT_X = 0;

        CommandButton[] commands;
        Dictionary<string, string> dictionary;

        public CommandsPanel() {
            this.Command = Commands.Exit;

            commands = new CommandButton[10];
            dictionary = new Dictionary<string, string>();
            dictionary.Add("Calendar", Resources.Calendar + "*");
            dictionary.Add("Letters", Resources.Letters);
            dictionary.Add("Plan", Resources.Plan + "*");
            dictionary.Add("Schedual", Resources.Schedual + "*");
            dictionary.Add("StudentGuide", Resources.StudentGuide);
            dictionary.Add("Requests", Resources.Requirements);
            dictionary.Add("Map", Resources.Map + "*");
            dictionary.Add("Suggestions", Resources.Suggestions);
            dictionary.Add("Exit", Resources.Exit);
            dictionary.Add("CommingSubjects", Resources.CommingSubjects);

            var x = DEFAULT_X;
            var y = 0;
            int i = 0;

            foreach (var item in dictionary) {
                commands[i] = new CommandButton(item.Value) {
                    Location = new Point(x, y),
                    Tag = item.Key,
                };

                if (i % 2 == 1) {
                    x = DEFAULT_X;
                    y += CommandButton.DefaultHeight + 20;
                } else {
                    x = DEFAULT_X + CommandButton.DefaultWidth + 150;
                }

                commands[i].Click += (s, e) => {
                    string tag = (s as CommandButton).Tag.ToString();
                    this.Command = (Commands)Enum.Parse(typeof(Commands), tag);
                    this.Close();
                };

                i++;
            }

            var size = new Size(CommandButton.DefaultWidth * 2 + 152,
                CommandButton.DefaultHeight * 6 + 12);
            var left = (Screen.PrimaryScreen.Bounds.Width - size.Width) / 2;
            var top = (Screen.PrimaryScreen.Bounds.Height - size.Height) / 2;
            Panel panel = new Panel {
                Location = new Point(left, top),
                Size = size,
                BackColor = Color.Transparent,
            };
            panel.Controls.AddRange(commands);

            this.Controls.Add(panel);
        }

        public Commands Command { get; private set; } 
    }
}
