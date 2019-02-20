using SelfService.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SelfService.Components
{
    class Waiting : Form
    {
        readonly PictureBox picture;

        public Waiting() {
            Size = Screen.PrimaryScreen.Bounds.Size;
            Location = Screen.PrimaryScreen.Bounds.Location;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.White;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;

            //_timeout = timeout;

            picture = new PictureBox {
                Dock = DockStyle.Fill,
                Name = "picture",
                SizeMode = PictureBoxSizeMode.CenterImage,
                TabStop = false,
                Image = (Image)Resources.Waiting_2,
            };
            Controls.Add(picture);

            //worker = new BackgroundWorker();
            //worker.DoWork += OnDoWork;
            //worker.RunWorkerCompleted += OnRunWorkerCompleted;
            //worker.RunWorkerAsync();
        }
    }
}
