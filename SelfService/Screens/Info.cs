using System;
using System.ComponentModel;
using System.Windows.Forms;
using AxWMPLib;

namespace SelfService.Screens
{
    class Info : BaseForm
    {
        string url;
        AxWindowsMediaPlayer player;

        public Info() {
            url = Application.StartupPath.Replace('\\', '/') + "/Videos/Vid1.mp4";
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Info));
            player = new AxWindowsMediaPlayer() {
                Enabled = true,
                Location = new System.Drawing.Point(491, 63),
                Name = "player",
                OcxState = ((AxHost.State)(resources.GetObject("player.OcxState"))),
                Size = new System.Drawing.Size(237, 232),
                TabIndex = 1,
                fullScreen = true
            };
            this.Controls.Add(player);
        }

        protected override void OnLoad(EventArgs e) {
            player.URL = url;
            player.Ctlcontrols.play();
        }
    }
}
