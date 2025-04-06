using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using btl.Account;

namespace btl.Nhansu
{
    public partial class Nhanvien : Form
    {
        public Nhansu nhansu;
        public Nhanvien(Nhansu parent)
        {
            this.nhansu = parent;
            InitializeComponent();
            loadtb();
            Thuvien.CustomDataGridView(Datanv);
            Datanv.Columns["pass"].Visible = false;
            Datanv.Columns["maphanquyen"].Visible = false;
            Datanv.Columns["username"].Visible = false;
            paneltk.Visible = false;
        }
        public void loadtb()
        {
            String sql = "select * from nhanvien";
            Thuvien.LoadData(sql, Datanv);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            nhansu.toacc();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String ma = txttk.Text;
            String ht = txttk.Text;
            String sdt = txttk.Text;
            String email = txttk.Text;
            String sql = "select * from nhanvien where manhanvien like '%" + ma + "%' or hoten like N'%" + ht + "%' or sdt like '%" + sdt + "%' or email like '%" + email + "%'";
            Thuvien.LoadData(sql, Datanv);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Datanv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Chỉ xử lý chuột phải
            {
                var hitTest = Datanv.HitTest(e.X, e.Y); // Kiểm tra vị trí chuột
                if (hitTest.RowIndex >= 0) // Kiểm tra có bấm vào hàng hợp lệ
                {
                    Datanv.ClearSelection();
                    Datanv.Rows[hitTest.RowIndex].Selected = true; // Chọn hàng được click
                    context.Show(Datanv, new Point(e.X, e.Y)); // Hiển thị menu tại vị trí chuột
                }
            }
        }

        private void tinhluong_Click(object sender, EventArgs e)
        {
            if (Datanv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = Datanv.SelectedRows[0];
                string ma = row.Cells["ma"].Value.ToString();
                string ht = row.Cells["hoten"].Value.ToString();
                nhansu.tacvu.SetData(ht, ma);
                nhansu.SwitchToTab(2);
            }
        

        }

        private void sua_Click(object sender, EventArgs e)
        {
            if (Datanv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = Datanv.SelectedRows[0];
                string ma = row.Cells["ma"].Value.ToString();
                string ht = row.Cells["hoten"].Value.ToString();
                string gt = row.Cells["gt"].Value.ToString();
                string pq = row.Cells["maphanquyen"].Value.ToString();
                string un = row.Cells["username"].Value.ToString();
                string pw = row.Cells["pass"].Value.ToString();
                string sdt = row.Cells["sdt"].Value.ToString();
                string email = row.Cells["email"].Value.ToString();
                nhansu.suanv(ma, ht, gt, pq, un, pw, sdt, email);
            }
        }

        private void ls_Click(object sender, EventArgs e)
        {
            if (Datanv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = Datanv.SelectedRows[0];
                string ma = row.Cells["ma"].Value.ToString();
                string ht = row.Cells["hoten"].Value.ToString();
                LSlamviec lSlamviec = new LSlamviec();
                lSlamviec.SetData(ht, ma);
                lSlamviec.ShowDialog();
            }
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            label6.ForeColor = Color.FromArgb(234, 132, 50);
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.FromArgb(74, 125, 175);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            paneltk.Visible= !paneltk.Visible;
            txttk.Text = "";
            txttk.Visible = !txttk.Visible;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            String ma = matk.Text;
            String ht = httk.Text;
            String sdt = textBox3.Text;
            String email = textBox5.Text;
            String sql = "select * from nhanvien where manhanvien like '%" + ma + "%' and hoten like N'%" + ht + "%' and sdt like '%" + sdt + "%' and email like '%" + email + "%'";
            Thuvien.LoadData(sql, Datanv);
        }
    }
}
