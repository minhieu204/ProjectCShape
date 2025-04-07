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
    public partial class frmkhuyenmai : Form
    {
        string connectionString = "Data Source=NCTOAN;Initial Catalog=QLSIEUTHI;User ID=sa;Password=1306;";
        SqlDataAdapter adapter;
        DataTable table = new DataTable();
        public frmkhuyenmai()
        {
            InitializeComponent();
            LoadData();
            LoadMaSanPham();
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter("SELECT * FROM KhuyenMai", conn);
                table.Clear();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void LoadMaSanPham()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT masp, giaban FROM sanpham", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBoxMaSP.DataSource = dt;
                comboBoxMaSP.DisplayMember = "masp";
                comboBoxMaSP.ValueMember = "masp";

                comboBoxMaSP.SelectedIndexChanged += (s, e) =>
                {
                    if (comboBoxMaSP.SelectedIndex >= 0)
                    {
                        DataRowView selectedRow = comboBoxMaSP.SelectedItem as DataRowView;
                        if (selectedRow != null)
                        {
                            txtGiaGoc.Text = selectedRow["giaban"].ToString();
                            TinhGiaSauGiam();
                        }
                    }
                };
            }
        }

        private void TinhGiaSauGiam()
        {
            if (decimal.TryParse(txtGiaGoc.Text, out decimal giaGoc) &&
                decimal.TryParse(txtGiamGia.Text, out decimal phanTramGiam))
            {
                decimal giaSau = giaGoc - (giaGoc * phanTramGiam / 100);
                txtGiaSau.Text = giaSau.ToString("0.##");
            }
        }


        private void frmkhuyenmai_Load(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO KhuyenMai VALUES (@Ma, @Ten, @NgayBD, @NgayKT, @MaSP, @GiamGia, @GiaSau, @GiaGoc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", txtMaKM.Text);
                cmd.Parameters.AddWithValue("@Ten", txtTenKM.Text);
                cmd.Parameters.AddWithValue("@NgayBD", dtpBatDau.Value);
                cmd.Parameters.AddWithValue("@NgayKT", dtpKetThuc.Value);
                cmd.Parameters.AddWithValue("@MaSP", comboBoxMaSP.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@GiamGia", txtGiamGia.Text);
                cmd.Parameters.AddWithValue("@GiaSau", txtGiaSau.Text);
                cmd.Parameters.AddWithValue("@GiaGoc", txtGiaGoc.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadData();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE KhuyenMai SET TenKhuyenMai=@Ten, NgayBatDau=@NgayBD, NgayKetThuc=@NgayKT, MaSanPham=@MaSP, GiamGia=@GiamGia, GiaSauGiam=@GiaSau, GiaGoc=@GiaGoc WHERE MaKhuyenMai=@Ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", txtMaKM.Text);
                cmd.Parameters.AddWithValue("@Ten", txtTenKM.Text);
                cmd.Parameters.AddWithValue("@NgayBD", dtpBatDau.Value);
                cmd.Parameters.AddWithValue("@NgayKT", dtpKetThuc.Value);
                cmd.Parameters.AddWithValue("@MaSP", comboBoxMaSP.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@GiamGia", txtGiamGia.Text);
                cmd.Parameters.AddWithValue("@GiaSau", txtGiaSau.Text);
                cmd.Parameters.AddWithValue("@GiaGoc", txtGiaGoc.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                                                   "Bạn có chắc chắn muốn xóa khuyến mãi này không?",
                                                   "Xác nhận xóa",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM KhuyenMai WHERE MaKhuyenMai=@Ma";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtMaKM.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LoadData();
                }
            }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            TinhGiaSauGiam();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaKM.Text = row.Cells["MaKhuyenMai"].Value.ToString();
                txtTenKM.Text = row.Cells["TenKhuyenMai"].Value.ToString();
                dtpBatDau.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                dtpKetThuc.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);
                comboBoxMaSP.SelectedValue = row.Cells["MaSanPham"].Value.ToString();
                txtGiamGia.Text = row.Cells["GiamGia"].Value.ToString();
                txtGiaSau.Text = row.Cells["GiaSauGiam"].Value.ToString();
                txtGiaGoc.Text = row.Cells["GiaGoc"].Value.ToString();
            }
        }
    }
}
