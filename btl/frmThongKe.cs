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
        private string connectionString = "Data Source=NCTOAN;Initial Catalog=QLSIEUTHI;Persist Security Info=True;User ID=sa;Password=1306;";
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
                string query = "SELECT sp.masp AS MaHang, sp.tensp AS TenHang, ncc.mancc AS MaNCC, sp.giaban AS GiaBan," +
                               "SUM(ctdh.soluong) AS TongSoLuongBan, (sp.soluong - SUM(ctdh.soluong)) AS SoLuongTon " +
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
                    item.SubItems.Add(row[4].ToString());
                    item.SubItems.Add(row[5].ToString());
                    listView1.Items.Add(item);
                }
            }
            else if (check == 1)
            {
                string query = "SELECT  s.masp,  s.tensp, s.mancc, s.gianhap, s.giaban, s.soluong, s.ngaynhap, s.donvitinh, s.maquanly FROM sanpham AS s WHERE s.soluong > 100;";
                DataTable dt = ExecuteQuery(query);

                listView1.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem(dt.Rows[i][0].ToString());
                    item.SubItems.Add(dt.Rows[i][1].ToString());
                    item.SubItems.Add(dt.Rows[i][2].ToString());
                    item.SubItems.Add(dt.Rows[i][3].ToString());
                    item.SubItems.Add(dt.Rows[i][4].ToString());
                    item.SubItems.Add(dt.Rows[i][5].ToString());
                    listView1.Items.Add(item);
                }
            }
            else if (check == 2)
            {
                listView1.Items.Clear();
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
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

                    MessageBox.Show("Cập nhật thành công!");

                    LoadData();
                    TinhTongTien();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng để sửa!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Kiểm tra xem có dữ liệu hợp lệ ở cột ID không
                var idCell = dataGridView1.SelectedRows[0].Cells["ID"];
                if (idCell != null && idCell.Value != null)
                {
                    int id = Convert.ToInt32(idCell.Value);
                    if (id > 0) // Đảm bảo ID hợp lệ
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "DELETE FROM ChiPhi WHERE ID = @ID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Xóa thành công!");
                            LoadData();
                            TinhTongTien();
                        }
                    }
                    else
                    {
                        MessageBox.Show("ID không hợp lệ.");
                    }
                }
                else
                {
                    MessageBox.Show("Không có giá trị ID trong dòng đã chọn.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng để xóa!");
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
    }
}
