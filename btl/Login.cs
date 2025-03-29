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

namespace btl
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private const int WM_SYSCOMMAND = 0x0112;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void cbshow_CheckedChanged(object sender, EventArgs e)
        {
            txtpass.UseSystemPasswordChar = !cbshow.Checked;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
