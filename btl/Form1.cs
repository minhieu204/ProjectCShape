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
    public partial class Form1 : Form
    {
        Dashboard dashboard;
        FormSP formSP;
        Account.Acc account;
        public Form1()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            mdiProp();
        }
        private const int WM_SYSCOMMAND = 0x0112;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        bool sidebarExpand = true;
        private void mdiProp()
        {
            this.SetBevel(false);
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(232, 234, 237);
        }
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            //if (sidebarExpand)
            //{
            //    sidebar.Width -= 10;
            //    if (sidebar.Width <= 60)
            //    {
            //        sidebarExpand = false;
            //        sidebarTransition.Stop();
            //        panelDB.Width = sidebar.Width;
            //        panelSP.Width = sidebar.Width;
            //        panelLO.Width = sidebar.Width;
            //    }
            //}
            //else
            //{
            //    sidebar.Width += 10;
            //    if (sidebar.Width >= 260)
            //    {
            //        sidebarExpand = true;
            //        sidebarTransition.Stop();
            //        panelDB.Width = sidebar.Width;
            //        panelSP.Width = sidebar.Width;
            //        panelLO.Width = sidebar.Width;
            //    }
            //}
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            //sidebarTransition.Start();
            if (sidebarExpand)
            {
                sidebar.Width = 60; // Thu nhỏ ngay lập tức
            }
            else
            {
                sidebar.Width = 200; // Mở rộng ngay lập tức
            }

            sidebarExpand = !sidebarExpand; // Đảo trạng thái mở rộng
            panelDB.Width = sidebar.Width;
            panelSP.Width = sidebar.Width;
            panelLO.Width = sidebar.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dashboard == null)
            {
                dashboard = new Dashboard();
                dashboard.FormClosed += Dashboard_FormClosed;
                dashboard.MdiParent = this;
                dashboard.Dock = DockStyle.Fill;
                dashboard.Show();
            }
            else
            {
                dashboard.Activate();
            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            dashboard = null;
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
            
            if (formSP == null)
            {
                formSP = new FormSP();
                formSP.FormClosed += Sanpham_FormClosed;
                formSP.MdiParent = this;
                formSP.Dock = DockStyle.Fill;
                formSP.Show();
            }
            else
            {
                formSP.Activate();
            }
        }

        private void Sanpham_FormClosed(object sender, FormClosedEventArgs e)
        {
            formSP = null;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (account == null)
            {
                account = new Account.Acc();
                account.MdiParent = this;
                account.Dock = DockStyle.Fill;
                account.Show();
            }
            else
            {
                account.Activate();
            }
        }
    }
}
