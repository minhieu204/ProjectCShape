using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Khachhang
{
    public partial class Khachhang : Form
    {
        public Khachhang()
        {
            InitializeComponent();
            paneltk.Visible = false;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
        }
        public void loadtb()
        {
            String sql = "select * from khachhang";
            Thuvien.LoadData(sql, dataGridView1);
        }
        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(234, 132, 50);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(74, 125, 175);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            txttk.Enabled = !txttk.Enabled;
            paneltk.Visible = !paneltk.Visible;
            txttk.Text = "";
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dataGridView1.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hitTest.RowIndex].Selected = true;
                    context.Show(dataGridView1, new Point(e.X, e.Y));
                }
            }
        }

        private void suacm_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["sdt"].Value.ToString();
            string ht = row.Cells["hoten"].Value.ToString();
            string gt = row.Cells["diem"].Value.ToString();
            UpdateKH updateKH = new UpdateKH(this);
            updateKH.setdata(ma, ht, gt);
            updateKH.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            UpdateKH updateKH = new UpdateKH(this);
            updateKH.reset();
            updateKH.ShowDialog();
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["sdt"].Value.ToString();
            String sql = "delete from khachhang where sdt='" + ma + "'";
            Thuvien.ExecuteQuery(sql);
            loadtb();
            MessageBox.Show("Xóa thành công!");
        }
    }
}
