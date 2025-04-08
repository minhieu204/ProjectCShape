using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.ChiPhi
{
    public partial class ChiPhiQL : Form
    {
        ChiPhi chiPhi;
        public ChiPhiQL(ChiPhi parent)
        {
            InitializeComponent();
            this.chiPhi = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
            TinhTongChiPhi();
            txtTongSuaChua.Enabled = false;
            txtTongTienDien.Enabled = false;
            txtTongTienNuoc.Enabled = false;
        }
        public void loadtb()
        {
            Thuvien.LoadData("select * from ChiPhi", dataGridView1);
        }

        private void TinhTongChiPhi()
        {
            decimal tongSuaChua = 0;
            decimal tongTienDien = 0;
            decimal tongTienNuoc = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; // bỏ qua dòng trống cuối cùng

                if (decimal.TryParse(row.Cells["PhiSuaChua"].Value?.ToString(), out decimal suaChua))
                    tongSuaChua += suaChua;

                if (decimal.TryParse(row.Cells["TienDien"].Value?.ToString(), out decimal dien))
                    tongTienDien += dien;

                if (decimal.TryParse(row.Cells["TienNuoc"].Value?.ToString(), out decimal nuoc))
                    tongTienNuoc += nuoc;
            }

            // Hiển thị kết quả vào các TextBox
            txtTongSuaChua.Text = tongSuaChua.ToString("N2");
            txtTongTienDien.Text = tongTienDien.ToString("N2");
            txtTongTienNuoc.Text = tongTienNuoc.ToString("N2");
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void suacm_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                string id = row.Cells["ID"].Value.ToString();
                string tienDien = row.Cells["TienDien"].Value.ToString();
                string tienNuoc = row.Cells["TienNuoc"].Value.ToString();
                DateTime thangNam = DateTime.Parse(row.Cells["ThangNam"].Value.ToString());
                string phiSuaChua = row.Cells["PhiSuaChua"].Value.ToString();

                chiPhi.chiPhiTV.SetData(id, tienDien, tienNuoc, thangNam, phiSuaChua);

                chiPhi.SwitchToTab(1);
            }

        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string id = row.Cells["ID"].Value.ToString(); // Lấy ID của dòng cần xóa

            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (mess_delete == DialogResult.Yes)
            {
                string sql = "DELETE FROM ChiPhi WHERE ID = '" + id + "'";
                Thuvien.ExecuteQuery(sql);
                loadtb(); // Tải lại bảng sau khi xóa
                MessageBox.Show("Xóa chi phí thành công!", "Thông báo!");
            }
        }

        private void dataGridView1_MouseDown_1(object sender, MouseEventArgs e)
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
    }
}
