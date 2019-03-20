﻿using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace SelfService.Screens
{
    internal class Map : BaseForm
    {
        public Map() {
            Footer footer = new Footer(Resources.Back);
            footer.SetCallback(0, (s, e) => { Close(); });
            var url = DB.Execute.GetConfig("map");
            Image loading = Tools.LoadImage("Waiting-2.gif");

            PictureBox box = new PictureBox {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = loading,
            };

            bool fromInternet = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (fromInternet) {
                box.WaitOnLoad = false;
                box.LoadAsync(url);
            } else {
                box.Image = Tools.LoadImage(url);
            }

            Controls.AddRange(new Control[] { box, footer });
        }
    }
}