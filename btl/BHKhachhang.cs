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
using btl.Account;

namespace btl
{
    public partial class BHKhachhang : Form
    {
        FormBH parent;
        public BHKhachhang(FormBH parent)
        {
            this.parent = parent;
            InitializeComponent();
            Thuvien.CustomDataGridView(dataGridView1);
            BHKhachhang_Load();
        }
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
        public void BHKhachhang_Load()
        {
            String sql = "select * from khachhang";
            Thuvien.LoadData(sql, dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String sql = "select * from khachhang where sdt like '%" + textBox1.Text + "%' or hoten like N'%" + textBox1.Text + "%'";
            Thuvien.LoadData(sql, dataGridView1);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            BHThemKH themkh = new BHThemKH(this);
            themkh.ShowDialog();
        }
        string sdt2;
        string ht2;
        int diem2 = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                sdt2 = row.Cells["sdt"].Value.ToString();
                ht2 = row.Cells["hoten"].Value.ToString();
                diem2 =int.Parse(row.Cells["diem"].Value.ToString());
                lbkh.Text = ht2;

            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            parent.SetData(sdt2, ht2, diem2);
            this.Close();
            parent.checkkh();
        }
    }
}
