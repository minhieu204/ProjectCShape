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

namespace btl.Doitac
{
    public partial class Doitactb : Form
    {
        Doitac Doitac;

        public Doitactb(Doitac parent)
        {
            InitializeComponent();
            this.Doitac = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
        }

        public void loadtb()
        {
            Thuvien.LoadData("select * from DoiTac", dataGridView1);
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
                string ma = row.Cells["Madoitac"].Value.ToString();
                string ten = row.Cells["Tenquangcao"].Value.ToString();
                DateTime bd = DateTime.Parse(row.Cells["Ngaybatdau"].Value.ToString());
                DateTime kt = DateTime.Parse(row.Cells["Ngayketthuc"].Value.ToString());
                string cp = row.Cells["Chiphi"].Value.ToString();
                Doitac.doitactv.SetData(ma, ten, bd, kt, cp);
                Doitac.SwitchToTab(1);
            }
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            String sql = "";
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["Madoitac"].Value.ToString();
            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không ?", "Xác nhận: ", MessageBoxButtons.YesNo);
            sql = "delete from DoiTac where Madoitac = '"+ ma +"'";
            Thuvien.ExecuteQuery(sql);
            loadtb();
            MessageBox.Show("Xóa tài khoản thành công!", "Thông báo!");
        }
    }
}
