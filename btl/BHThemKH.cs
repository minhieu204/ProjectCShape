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

namespace btl
{
    public partial class BHThemKH : Form
    {
        BHKhachhang BHKhachhang;
        public BHThemKH(BHKhachhang parent)
        {
            InitializeComponent();
            this.BHKhachhang = parent;
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void btntv_Click(object sender, EventArgs e)
        {
            String sdt = textBox1.Text;
            String ht = textBox2.Text;
            int diem = 0;
            if (sdt=="") {
                MessageBox.Show("Vui lòng nhập số điện thoại!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ht == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Thuvien.CheckExist("select count(*) from khachhang where sdt='"+sdt+"'")) {
                MessageBox.Show("Số điện thoại đã tồn tại!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql= "insert into khachhang(sdt,hoten,diem) values ('" + sdt + "',N'" + ht + "',"+diem+")";
            Thuvien.ExecuteQuery(sql);
            BHKhachhang.BHKhachhang_Load();
            this.Close();
            MessageBox.Show("Thêm thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {

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
