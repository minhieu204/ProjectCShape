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
            DateTime batDau = dateTimePicker1.Value;
            DateTime ketThuc = dateTimePicker2.Value;
            float giamGia = float.Parse(textBox2.Text);

            using (SqlConnection conn = Thuvien.GetConnection())
            {
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    SqlCommand cmdKM = new SqlCommand("INSERT INTO KhuyenMai VALUES (@Ma, @Ten, @BD, @KT, @GG)", conn, tran);
                    cmdKM.Parameters.AddWithValue("@Ma", maKM);
                    cmdKM.Parameters.AddWithValue("@Ten", tenKM);
                    cmdKM.Parameters.AddWithValue("@BD", batDau);
                    cmdKM.Parameters.AddWithValue("@KT", ketThuc);
                    cmdKM.Parameters.AddWithValue("@GG", giamGia);
                    cmdKM.ExecuteNonQuery();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Chon"].Value))
                        {
                            string maSP = row.Cells["MaSP"].Value.ToString();
                            decimal giaGoc = Convert.ToDecimal(row.Cells["GiaGoc"].Value);
                            decimal giaSau = Convert.ToDecimal(row.Cells["GiaSauGiam"].Value);

                            SqlCommand cmdCT = new SqlCommand("INSERT INTO ChiTietKhuyenMai VALUES (@MaKM, @MaSP, @GiaSau)", conn, tran);
                            cmdCT.Parameters.AddWithValue("@MaKM", maKM);
                            cmdCT.Parameters.AddWithValue("@MaSP", maSP);
                            cmdCT.Parameters.AddWithValue("@GiaSau", giaSau);
                            cmdCT.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    MessageBox.Show("Thêm khuyến mãi thành công");
                }
                catch
                {
                    tran.Rollback();
                    MessageBox.Show("Lỗi khi thêm khuyến mãi");
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
                    row.Cells["GiaSauGiam"].Value = giaSau.ToString("N0");
                }
                else
                {
                    row.Cells["GiaSauGiam"].Value = ""; // Nếu không được chọn thì để trống
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
