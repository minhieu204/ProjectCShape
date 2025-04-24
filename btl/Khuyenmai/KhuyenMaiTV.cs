using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Khuyenmai
{
    public partial class KhuyenMaiTV : Form
    {
        KhuyenMai khuyenMai;
        public KhuyenMaiTV(KhuyenMai parent)
        {
            InitializeComponent();
            this.khuyenMai = parent;
        }


        public void SetData(string ma, string ten, DateTime bd, DateTime kt, string cbo , string ggoc , string giamgia , string gsaugiam)
        {
            txtma.Text = ma;
            txtten.Text = ten;
            dateTimePicker1.Value = bd;
            dateTimePicker2.Value = kt;
            txtma.Enabled = false;
            btntv.Text = "Cập nhật";
            header.Text = "Cập nhật khuyến mãi";
        }

        private void Reload()
        {
            txtma.Text = "";
            txtten.Text = "";
            txtma.Enabled = true;
            btntv.Text = "Thêm";
            header.Text = "Thêm khuyến mãi";
        }

        

        private void btntv_Click_1(object sender, EventArgs e)
        {
            string maKM = textBox1.Text;
            string tenKM = txtten.Text;
            DateTime ngayBD = dateTimePicker1.Value;
            DateTime ngayKT = dateTimePicker2.Value;
            float giamGia;

            if (string.IsNullOrWhiteSpace(maKM) || string.IsNullOrWhiteSpace(tenKM) ||
                !float.TryParse(textBox2.Text, out giamGia))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                return;
            }

            using (SqlConnection conn = Thuvien.GetConnection())
            {

                string query = @"INSERT INTO KhuyenMai (MaKhuyenMai, TenKhuyenMai, NgayBatDau, NgayKetThuc, GiamGia)
                         VALUES (@MaKhuyenMai, @TenKhuyenMai, @NgayBD, @NgayKT, @GiamGia)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKhuyenMai", maKM);
                    cmd.Parameters.AddWithValue("@TenKhuyenMai", tenKM);
                    cmd.Parameters.AddWithValue("@NgayBD", ngayBD);
                    cmd.Parameters.AddWithValue("@NgayKT", ngayKT);
                    cmd.Parameters.AddWithValue("@GiamGia", giamGia);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm khuyến mãi thành công!");
                       // LoadData(); // Gọi lại hàm load danh sách nếu có
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void btnload_Click(object sender, EventArgs e)
        {
            Reload();
        }


        private void KhuyenMaiTV_Load(object sender, EventArgs e)
        {
            string sql = "SELECT MaSP, TenSP, GiaBan FROM SanPham";
            using (SqlConnection conn = Thuvien.GetConnection())
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                // Tắt chế độ tự sinh cột
                dataGridView1.AutoGenerateColumns = false;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textBox2.Text.Replace("%", "").Trim(), out decimal phanTramGiam))
            {
                return; // Nếu không phải số, thoát
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // Chỉ xử lý nếu dòng được chọn (ô "Chon" được tick)
                bool isSelected = row.Cells["Chon"].Value != null && (bool)row.Cells["Chon"].Value;

                if (isSelected && decimal.TryParse(row.Cells["GiaGoc"].Value?.ToString().Replace(",", "").Trim(), out decimal giaGoc))
                {
                    decimal giaSau = giaGoc * (1 - phanTramGiam / 100);
                    row.Cells["GiaSau"].Value = giaSau.ToString("N0");
                }
                else
                {
                    row.Cells["GiaSau"].Value = ""; // Nếu không được chọn thì để trống
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Chon")
            {
                textBox2_TextChanged(null, null); // Gọi lại cập nhật giá
            }
        }
    }
}
