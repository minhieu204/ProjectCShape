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

namespace btl
{
    public partial class frmThongKe : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=QLSIEUTHI;User ID=sa;Password=1306;";
        public frmThongKe()
        {
            InitializeComponent();
            LoadData();
        }


        // Phương thức thực hiện truy vấn SQL
        private DataTable ExecuteQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                conn.Open();
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            //DataTable dt = ExecuteQuery("Select maquanly from sanpham");
            //DataTable dt1 = ExecuteQuery("Select tenncc from nhacungcap");

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    cbLoaiHang.Items.Add(dt.Rows[i][0].ToString());
            //}

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int check = comboBox1.SelectedIndex;

            if (check == 0)
            {
                string query = "SELECT sp.masp AS MaHang, sp.tensp AS TenHang, ncc.mancc AS MaNCC, sp.giaban AS GiaBan, " +
                               "SUM(ctdh.soluong) AS TongSoLuongBan, sp.soluong AS SoLuongTon " +
                               "FROM chitietdonhang ctdh " +
                               "JOIN sanpham sp ON ctdh.masp = sp.masp " +
                               "JOIN nhacungcap ncc ON sp.mancc = ncc.mancc " +
                               "GROUP BY sp.masp, sp.tensp, ncc.mancc, sp.giaban, sp.soluong;";

                DataTable dt = ExecuteQuery(query);
                listView1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem(row[0].ToString());
                    item.SubItems.Add(row[1].ToString());
                    item.SubItems.Add(row[2].ToString()); 
                    item.SubItems.Add(row[3].ToString()); 
                    item.SubItems.Add(row[4].ToString()); // TongSoLuongBan
                    item.SubItems.Add(row[5].ToString()); // SoLuongTon (hiện tại)
                    listView1.Items.Add(item);
                }
            }
            else if (check == 1)
            {
                string query = "SELECT s.masp, s.tensp, s.mancc, s.giaban, s.soluong FROM sanpham AS s WHERE s.soluong > 100;";
                DataTable dt = ExecuteQuery(query);

                listView1.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem(dt.Rows[i][0].ToString()); // masp
                    item.SubItems.Add(dt.Rows[i][1].ToString()); // tensp
                    item.SubItems.Add(dt.Rows[i][2].ToString()); // mancc
                    item.SubItems.Add(dt.Rows[i][3].ToString()); // gianhap
                    item.SubItems.Add(""); // giaban
                    item.SubItems.Add(dt.Rows[i][4].ToString()); // soluong
                    listView1.Items.Add(item);
                }
            }
            else if (check == 2)
            {
                string query = "SELECT s.masp, s.tensp, s.mancc, s.giaban, s.soluong FROM sanpham AS s WHERE s.soluong < 50;";
                DataTable dt = ExecuteQuery(query);

                listView1.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem(dt.Rows[i][0].ToString()); // masp
                    item.SubItems.Add(dt.Rows[i][1].ToString()); // tensp
                    item.SubItems.Add(dt.Rows[i][2].ToString()); // mancc
                    item.SubItems.Add(dt.Rows[i][3].ToString()); // gianhap
                    item.SubItems.Add(""); // giaban
                    item.SubItems.Add(dt.Rows[i][4].ToString()); // soluong
                    listView1.Items.Add(item);
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            
        }



        //----------------------------------
        //chi phí


        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ID, FORMAT(ThangNam, 'MM/yyyy') AS ThangNam, PhiSuaChua, TienDien, TienNuoc FROM ChiPhi";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            TinhTongTien(); // ⚡ Cập nhật tổng tiền sau khi load dữ liệu
        }


        private void TinhTongTien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT SUM(PhiSuaChua) AS TongPhiSuaChua, SUM(TienDien) AS TongTienDien, SUM(TienNuoc) AS TongTienNuoc FROM ChiPhi";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtTongSuaChua.Text = reader["TongPhiSuaChua"] != DBNull.Value ? Convert.ToDecimal(reader["TongPhiSuaChua"]).ToString("N2") : "0.00";
                    txtTongTienDien.Text = reader["TongTienDien"] != DBNull.Value ? Convert.ToDecimal(reader["TongTienDien"]).ToString("N2") : "0.00";
                    txtTongTienNuoc.Text = reader["TongTienNuoc"] != DBNull.Value ? Convert.ToDecimal(reader["TongTienNuoc"]).ToString("N2") : "0.00";
                }

                reader.Close();
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO ChiPhi (ThangNam, PhiSuaChua, TienDien, TienNuoc) VALUES (@ThangNam, @PhiSuaChua, @TienDien, @TienNuoc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ThangNam", dtpThangNam.Value);
                cmd.Parameters.AddWithValue("@PhiSuaChua", txtPhiSuaChua.Text);
                cmd.Parameters.AddWithValue("@TienDien", txtTienDien.Text);
                cmd.Parameters.AddWithValue("@TienNuoc", txtTienNuoc.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                LoadData();
                TinhTongTien();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa!");
                return;
            }

            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            string id = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();

            DialogResult result = MessageBox.Show("Bạn có chắc muốn cập nhật không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE ChiPhi SET ThangNam = @ThangNam, PhiSuaChua = @PhiSuaChua, TienDien = @TienDien, TienNuoc = @TienNuoc WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@ThangNam", dtpThangNam.Value);
                    cmd.Parameters.AddWithValue("@PhiSuaChua", txtPhiSuaChua.Text);
                    cmd.Parameters.AddWithValue("@TienDien", txtTienDien.Text);
                    cmd.Parameters.AddWithValue("@TienNuoc", txtTienNuoc.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Cập nhật thành công!");
                LoadData();
                TinhTongTien();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
                return;
            }

            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            string id = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM ChiPhi WHERE ID = @ID", conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Xóa thành công!");
                LoadData();
                TinhTongTien();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng đang chọn
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Chuyển đổi lại định dạng tháng/năm
                if (DateTime.TryParse(row.Cells[1].Value?.ToString(), out DateTime thangNam))
                {
                    dtpThangNam.Text = thangNam.ToString("MM/yyyy");
                }
                else
                {
                    dtpThangNam.Text = row.Cells[1].Value?.ToString();
                }

                txtPhiSuaChua.Text = row.Cells[2].Value?.ToString();
                txtTienDien.Text = row.Cells[3].Value?.ToString();
                txtTienNuoc.Text = row.Cells[4].Value?.ToString();
            }
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
