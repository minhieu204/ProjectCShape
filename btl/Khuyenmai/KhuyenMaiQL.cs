using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Khuyenmai
{
    public partial class KhuyenMaiQL : Form
    {
        KhuyenMai khuyenMai;
        public KhuyenMaiQL(KhuyenMai parent)
        {
            InitializeComponent();
            this.khuyenMai = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
        }
        public void loadtb()
        {
            Thuvien.LoadData("select * from KhuyenMai", dataGridView1);
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
                string ma = row.Cells["MaKhuyenMai"].Value.ToString();
                string ten = row.Cells["TenKhuyenMai"].Value.ToString();
                DateTime bd = DateTime.Parse(row.Cells["NgayBatDau"].Value.ToString());
                DateTime kt = DateTime.Parse(row.Cells["NgayKetThuc"].Value.ToString());
                string cbo = row.Cells["MaSanPham"].Value.ToString();
                string ggoc = row.Cells["GiaGoc"].Value.ToString();
                string giamgia = row.Cells["GiamGia"].Value.ToString();
                string gsaugiam = row.Cells["GiaSauGiam"].Value.ToString();
                khuyenMai.khuyenMaiTV.SetData( ma,  ten,  bd,  kt,  cbo,  ggoc, gsaugiam,  giamgia);
                khuyenMai.SwitchToTab(1);
            }
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            String sql = "";
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["MaKhuyenMai"].Value.ToString();
            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không ?", "Xác nhận: ", MessageBoxButtons.YesNo);
            sql = "delete from KhuyenMai where MaKhuyenMai = '" + ma + "'";
            Thuvien.ExecuteQuery(sql);
            loadtb();
            MessageBox.Show("Xóa tài khoản thành công!", "Thông báo!");
        }
    }
}
