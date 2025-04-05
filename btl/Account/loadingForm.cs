using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Account
{
    public partial class loadingForm : Form
    {
        public loadingForm()
        {
            InitializeComponent();
            progressBar.Minimum = 0;
            progressBar.Value = 0;
            progressBar.Step = 1;
            progressBar.Maximum = 100;
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void loadingForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        public void SetMessage(string message)
        {
            labelStatus.Text = message;
        }

        public void SetProgress(int progress)
        {
            progressBar.Value = progress;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 30; // Điều chỉnh tốc độ animation
            timer.Tick += (s, ev) =>
            {
                if (this.Opacity > 0.1)
                {
                    this.Opacity -= 0.1;
                }
                else
                {
                    timer.Stop();
                    this.WindowState = FormWindowState.Minimized;
                    this.Opacity = 1; // Reset opacity khi mở lại
                }
            };
            timer.Start();
        }
    }
}
