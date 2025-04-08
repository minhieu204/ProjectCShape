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
            //LoadComboBoxMaSP(); 
            txtgiasugiam.Enabled = false;
            txtgiagoc.Enabled = false;
        }

        private void TinhGiaSauGiam()
        {
            if (decimal.TryParse(txtgiagoc.Text, out decimal giaGoc) &&
                decimal.TryParse(txtgiamgia.Text, out decimal phanTram))
            {
                decimal giaSauGiam = giaGoc * (1 - phanTram / 100);
                txtgiasugiam.Text = giaSauGiam.ToString("N0");
            }
            else
            {
                txtgiasugiam.Clear();
            }
        }

        //private void LoadComboBoxMaSP()
        //{
        //    string sql = "SELECT MaSP, MaSP + ' - ' + TenSP AS TenHienThi FROM SanPham";
        //    Thuvien.LoadComboBox(sql, comboBox1, "MaSP", "TenHienThi");
        //    comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        //    comboBox1.SelectedIndex = 0;

            //}




        public void SetData(string ma, string ten, DateTime bd, DateTime kt, string cbo , string ggoc , string giamgia , string gsaugiam)
        {
            txtma.Text = ma;
            txtten.Text = ten;
            dateTimePicker1.Value = bd;
            dateTimePicker2.Value = kt;
            comboBox1.SelectedValue = cbo;
            txtma.Enabled = false;
            txtgiagoc.Text = ggoc;
            txtgiamgia.Text = giamgia;
            txtgiasugiam.Text = gsaugiam;
            btntv.Text = "Cập nhật";
            header.Text = "Cập nhật khuyến mãi";
        }

        private void Reload()
        {
            txtma.Text = "";
            txtten.Text = "";
            txtgiagoc.Text = "";
            txtgiamgia.Text = "";
            txtgiasugiam.Text = "";
            comboBox1.SelectedValue = 0;
            txtma.Enabled = true;
            btntv.Text = "Thêm";
            header.Text = "Thêm khuyến mãi";
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btntv_Click(object sender, EventArgs e)
        {

        }

        private void btntv_Click_1(object sender, EventArgs e)
        {
            if (txtma.Text == "" || txtten.Text == "" || comboBox1.SelectedValue == null ||
    txtgiagoc.Text == "" || txtgiamgia.Text == "" || txtgiasugiam.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            string ma = txtma.Text;
            string ten = txtten.Text;
            DateTime bd = dateTimePicker1.Value;
            DateTime kt = dateTimePicker2.Value;
            string masp = comboBox1.SelectedValue.ToString();
            int giagoc = int.Parse(txtgiagoc.Text.Replace(",", "").Trim());
            int giamgia = int.Parse(txtgiamgia.Text.Replace("%", "").Trim());
            int giasaugiam = int.Parse(txtgiasugiam.Text.Replace(",", "").Trim());

            if (btntv.Text == "Thêm")
            {
                if (!Thuvien.CheckExist("SELECT COUNT(*) FROM KhuyenMai WHERE MaKhuyenMai = '" + ma + "'"))
                {
                    string query = "INSERT INTO KhuyenMai VALUES (@Ma, @Ten, @NgayBD, @NgayKT, @MaSP, @GiaGoc, @GiamGia, @GiaSau)";
                    using (SqlConnection conn = Thuvien.GetConnection())
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ma", ma);
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@NgayBD", bd);
                        cmd.Parameters.AddWithValue("@NgayKT", kt);
                        cmd.Parameters.AddWithValue("@MaSP", masp);
                        cmd.Parameters.AddWithValue("@GiaGoc", giagoc);
                        cmd.Parameters.AddWithValue("@GiamGia", giamgia);
                        cmd.Parameters.AddWithValue("@GiaSau", giasaugiam);

                        
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    MessageBox.Show("Thêm thành công!");
                }
                else
                {
                    MessageBox.Show("Mã khuyến mãi đã tồn tại");
                    return;
                }
            }
            else // Sửa
            {
                string query = "UPDATE KhuyenMai SET TenKhuyenMai = @Ten, NgayBatDau = @NgayBD, NgayKetThuc = @NgayKT, MaSanPham = @MaSP, GiaGoc = @GiaGoc, GiamGia = @GiamGia, GiaSauGiam = @GiaSau WHERE MaKhuyenMai = @Ma";
                using (SqlConnection conn = Thuvien.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", ma);
                    cmd.Parameters.AddWithValue("@Ten", ten);
                    cmd.Parameters.AddWithValue("@NgayBD", bd);
                    cmd.Parameters.AddWithValue("@NgayKT", kt);
                    cmd.Parameters.AddWithValue("@MaSP", masp);
                    cmd.Parameters.AddWithValue("@GiaGoc", giagoc);
                    cmd.Parameters.AddWithValue("@GiamGia", giamgia);
                    cmd.Parameters.AddWithValue("@GiaSau", giasaugiam);

                   
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Cập nhật thành công!");
                }
            }

            khuyenMai.khuyenMaiQL.loadtb(); // làm mới bảng dữ liệu
            khuyenMai.SwitchToTab(0);        // chuyển tab hiển thị
            Reload();                        // làm mới form

        }

        private void btnload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void txtgiasugiam_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                DataRowView drv = comboBox1.SelectedItem as DataRowView;
                if (drv != null)
                {
                    decimal gia = Convert.ToDecimal(drv["giaban"]);
                    txtgiagoc.Text = gia.ToString("N0"); // Format có dấu phẩy
                    TinhGiaSauGiam(); ; // Gọi luôn hàm tính nếu đã nhập %
                }
            }
            else
            {
                txtgiagoc.Clear();
                txtgiasugiam.Clear();
            }
        }

        private void KhuyenMaiTV_Load(object sender, EventArgs e)
        {
           
            string sql = "SELECT MaSP, TenSP, giaban FROM SanPham";
            Thuvien.LoadComboBox(sql, comboBox1, "MaSP", "TenSP");
        

    }

        private void txtgiamgia_TextChanged(object sender, EventArgs e)
        {
            TinhGiaSauGiam();
        }
    }
}
