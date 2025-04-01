using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Account
{
    public partial class Acctb : Form
    {
        Acc Acc;
        public Acctb(Acc parent)
        {
            InitializeComponent();
            this.Acc = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
        }

        private void Acctb_Load(object sender, EventArgs e)
        {
            
        }
        public void loadtb()
        {
            Thuvien.LoadDatatk("select * from quanly", "select manhanvien as maquanly, hoten, gioitinh, maphanquyen, username, pass, sdt, email from nhanvien", dataGridView1);
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Chỉ xử lý chuột phải
            {
                var hitTest = dataGridView1.HitTest(e.X, e.Y); // Kiểm tra vị trí chuột
                if (hitTest.RowIndex >= 0) // Kiểm tra có bấm vào hàng hợp lệ
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hitTest.RowIndex].Selected = true; // Chọn hàng được click
                    context.Show(dataGridView1, new Point(e.X, e.Y)); // Hiển thị menu tại vị trí chuột
                }
            }
        }

        private void suacm_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string ma = row.Cells["maquanly"].Value.ToString();
                string ht = row.Cells["hoten"].Value.ToString();
                string gt = row.Cells["gioitinh"].Value.ToString();
                string pq = row.Cells["maphanquyen"].Value.ToString();
                string un = row.Cells["username"].Value.ToString();
                string pw = row.Cells["pass"].Value.ToString();
                string sdt = row.Cells["sdt"].Value.ToString();
                string email = row.Cells["email"].Value.ToString();
                Acc.acctv.setData(ma, ht, gt, pq, un, pw, sdt, email);
                Acc.SwitchToTab(1); // Chuyển sang tab 2
            }
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            String sql = "";
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["maquanly"].Value.ToString();
            string pq = row.Cells["maphanquyen"].Value.ToString();
            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không ?", "Xác nhận: ", MessageBoxButtons.YesNo);
            if (mess_delete == DialogResult.Yes)
            {
                if (pq == "ql")
                {
                    sql = "delete from quanly where maquanly = '" + ma + "'";
                }
                else
                {
                    sql = "delete from nhanvien where manhanvien = '" + ma + "'";
                }
            }
            Thuvien.ExecuteQuery(sql);
            loadtb();
            MessageBox.Show("Xóa tài khoản thành công!","Thông báo!");

        }
    }
}
