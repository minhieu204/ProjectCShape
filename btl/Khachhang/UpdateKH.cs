using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Khachhang
{
    public partial class UpdateKH : Form
    {
        Khachhang khachhang;
        public UpdateKH(Khachhang parent)
        {
            this.khachhang = parent;
            InitializeComponent();
            
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public void setdata(string makh, string hoten, string diem)
        {
            textBox1.Text = makh;
            textBox1.Enabled = false;
            textBox2.Text = hoten;
            textBox3.Text = diem;
            checkBox1.Checked = true;
            textBox3.Enabled = true;
            label1.Text = "Cập nhật khách hàng";
            btntv.Text = "Cập nhật";
        }
        public void reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            checkBox1.Checked = false;
            textBox3.Enabled = false;
            label1.Text = "Thêm khách hàng";
            btntv.Text = "Thêm";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = checkBox1.Checked;
            if (!checkBox1.Checked)
            {
                textBox3.Text = "";
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btntv_Click(object sender, EventArgs e)
        {
            String sdt = textBox1.Text;
            String hoten = textBox2.Text;
            int diem = 0;
            if (textBox3.Text == "")
            {
                diem = 0;
            }
            else
            {
                diem = int.Parse(textBox3.Text);
            }
            if (btntv.Text == "Thêm")
            {
                String sql1 = "select count(*) from khachhang where sdt='"+ sdt+"'";
                if (Thuvien.CheckExist(sql1))
                {
                    MessageBox.Show("Số điện thoại đã tồn tại");
                    return;
                }
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin họ tên và số điện thoại");
                    return;
                }
                else
                {
                    String sql = "insert into khachhang values('" + sdt + "',N'" + hoten + "'," + diem + ")";
                    Thuvien.ExecuteQuery(sql);
                    MessageBox.Show("Thêm thành công");
                    khachhang.loadtb();
                    this.Close();
                }
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin họ tên và số điện thoại");
                    return;
                }
                else
                {
                    String sql = "update khachhang set hoten=N'"+ hoten +"',diem=" + diem + " where sdt='" + sdt + "'";
                    Thuvien.ExecuteQuery(sql);
                    MessageBox.Show("Cập nhật thành công");
                    khachhang.loadtb();
                    this.Close();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string phoneNumber = textBox1.Text.Trim();
            string pattern = @"^0[3|5|7|8|9][0-9]{8}$";

            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập lại.");
                textBox1.Text = "";
            }
        }
    }
}
