using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Nhansu
{
    public partial class Sualuong : Form
    {
        public Sualuong(Luong luong)
        {
            InitializeComponent();
            this.luong = luong;
        }
        Luong luong;
        String ma;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        public void setData(string ma, string manv, string lcb, string thuong)
        {
            this.ma = ma;
            label3.Text = "Sửa bảng lương của:"+manv;
            textBox1.Text = lcb;
            textBox2.Text = thuong;

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text)) { 
                  float lcb = float.Parse(textBox1.Text);
                  float thuong = 0;
                if (textBox2.Text == "")
                {
                    thuong = 0;
                }
                else
                {
                    thuong = float.Parse(textBox2.Text);
                }
                if (lcb <= 0)
                {
                    MessageBox.Show("Lương cơ bản phải lớn hơn 0");
                    return;
                }
                String sql = "update luong set luongtheogio = " + lcb + ", thuong = " + thuong + " where maluong = '" + ma + "'";
                Thuvien.ExecuteQuery(sql);
                MessageBox.Show("Sửa bảng lương thành công!", "Thông báo!");
                luong.Loadtb();
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập lương cơ bản!");
                return;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
