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
        int temp = 0;
        public Login()
        {
            InitializeComponent();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void cbshow_CheckedChanged(object sender, EventArgs e)
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
            if (txtname.Text == "" || txtpass.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!");
                return;
            }
            String name = txtname.Text;
            String pass = txtpass.Text;
            Thuvien.Login(name, pass);
            if (Datauser.IsSuccess) {
                MessageBox.Show("Đăng nhập thành công với ID:"+Datauser.ID);
                if (cbshow.Checked) {
                    String sql =String.Format("insert into logins values('{0}',N'{1}','{2}')",Datauser.ID,Datauser.HoTen,Datauser.Role);
                    Thuvien.ExecuteQuery(sql);
                }
                if (Datauser.Role == "nhanvien")
                {
                    Thuvien.LogLogin(Datauser.ID);
                }
                Form1 f = new Form1();
            f.Show();
            this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            if (temp == 0)
            {
                guna2PictureBox1.Image = Properties.Resources.icons8_eye_30;
                txtpass.UseSystemPasswordChar = false;
                temp = 1;
            }
            else
            {
                guna2PictureBox1.Image = Properties.Resources.icons8_hide_password_30;
                txtpass.UseSystemPasswordChar = true;
                temp = 0;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
