using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace btl
{
    internal class Thuvien
    {
        private static readonly string connectionString = "Data Source=.;Initial Catalog=QLSIEUTHI;User ID=sa;Password=1306;";
        
        public static void CustomDisabledButton (Button button)
        {
            button.BackColor = Color.FromArgb(180, 210, 235); 
            button.ForeColor = Color.DarkGray; 
            button.Enabled = false;
        }

        public static void CustomEnabledTButton(Button button)
        {
            button.BackColor = Color.FromArgb(52, 152, 219);
            button.ForeColor = Color.White;
            button.Enabled = true;
        }
        public static void CustomDataGridView(DataGridView dataGridView)
        {
            // Thiết lập cơ bản
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;

            // Thiết lập font
            dataGridView.Font = new Font("Segoe UI", 10);

            // Màu sắc
            dataGridView.BackgroundColor = Color.White;
            dataGridView.GridColor = Color.FromArgb(240, 240, 240);

            // Style cho header cột (KHÔNG thay đổi khi chọn)
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(44, 62, 80); // Giữ nguyên màu khi chọn
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White; // Giữ nguyên màu chữ khi chọn

            // Style cho hàng
            dataGridView.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(209, 238, 255);
            //dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.RowTemplate.Height = 40;


            // Style cho cell
            dataGridView.DefaultCellStyle.Padding = new Padding(5);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

        }
        public static bool Test()
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    return con.State == ConnectionState.Open;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        public static void ExecuteQuery(string sql)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static void LoadData(string sql, DataGridView dgv)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        public static void LoadComboBox(string sql, ComboBox cb, string valueField, string displayField)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row[valueField] = DBNull.Value;
                row[displayField] = "---Chọn---";
                dt.Rows.InsertAt(row, 0);

                cb.DataSource = dt;
                cb.DisplayMember = displayField;
                cb.ValueMember = valueField;
            }
        }

        public static bool CheckExist(string sql)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result > 0;
            }
        }
        public static void LoadExcel(string sql, DataTable dt)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }
        public static void LoadDatatk(string sql1, string sql2, DataGridView dgv)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd1 = new SqlCommand(sql1, con))
            using (SqlCommand cmd2 = new SqlCommand(sql2, con))
            using (SqlDataAdapter da1 = new SqlDataAdapter(cmd1))
            using (SqlDataAdapter da2 = new SqlDataAdapter(cmd2))
            {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                da1.Fill(dt1);
                da2.Fill(dt2);

                // Gộp dữ liệu 2 bảng vào 1 DataTable
                dt1.Merge(dt2);

                dgv.DataSource = dt1;
            }
        }

        // Hàm tra cứu từ mã thành tên hoặc ngược lại
        public static string GetValueFromCode(string sql, string inputValue, string valueField, string displayField)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Tìm trong DataTable
                DataRow[] rows = dt.Select($"{displayField} = '{inputValue}'");

                if (rows.Length > 0)
                {
                    return rows[0][valueField].ToString();
                }

                return ""; // Không tìm thấy
            }
        }

        public static object GetSingleValue(string sql)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                return cmd.ExecuteScalar();
            }
        }
        public static void Login(String username, String password)
        {
            using (SqlConnection con = GetConnection())
            {
                // Check bảng quản lý
                string sqlQuanLy = "SELECT maquanly, hoten FROM quanly WHERE username = @username AND pass = @password";
                using (SqlCommand cmd = new SqlCommand(sqlQuanLy, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Datauser.IsSuccess = true;
                            Datauser.ID = reader["maquanly"].ToString();
                            Datauser.HoTen = reader["hoten"].ToString();
                            Datauser.Role = "quanly";
                            return;
                        }
                    }
                }

                // Check bảng nhân viên
                string sqlNhanVien = "SELECT manhanvien, hoten FROM nhanvien WHERE username = @username AND pass = @password";
                using (SqlCommand cmd = new SqlCommand(sqlNhanVien, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Datauser.IsSuccess = true;
                            Datauser.ID = reader["manhanvien"].ToString();
                            Datauser.HoTen = reader["hoten"].ToString();
                            Datauser.Role = "nhanvien";
                            return;
                        }
                    }
                }

                // Nếu không tìm thấy
                Datauser.IsSuccess = false;
                return;
            }
        }
        public static void CheckLogin()
        {
            using (SqlConnection con = GetConnection()) {
                String sql = "SELECT * FROM logins";
                using (SqlCommand cmd = new SqlCommand(sql, con)) {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Datauser.IsSuccess = true;
                            Datauser.ID = reader["id"].ToString();
                            Datauser.HoTen = reader["uname"].ToString();
                            Datauser.Role = reader["pq"].ToString();
                            return;
                        }
                    }
                }
            }
        }
        public static void LogLogin(string employeeId)
        {
            string sql = @"
                        INSERT INTO lslamviec (manhanvien, logintime, ngay, workstatus)
                        VALUES (@manhanvien, @logintime, @ngay, @workstatus);
                        SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@manhanvien", employeeId);
                cmd.Parameters.AddWithValue("@logintime", DateTime.Now);
                cmd.Parameters.AddWithValue("@ngay", DateTime.Today);
                cmd.Parameters.AddWithValue("@workstatus", "IN_PROGRESS");

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int idMoi = Convert.ToInt32(result);
                    Datauser.IDLogin = idMoi;
                    Console.WriteLine("💡 Đăng nhập thành công. ID lịch sử làm việc: " + idMoi);
                }
                else
                {
                    Console.WriteLine("⚠ Không lấy được ID lịch sử làm việc!");
                }
            }
        }
        public static void LogLogout(string employeeId)
        {
            string sql = @"
                        UPDATE lslamviec 
                        SET logouttime = @logouttime, 
                        workstatus = 'COMPLETED', 
                        giolamviec = DATEDIFF(MINUTE, logintime, @logouttime) / 60.0
                        WHERE manhanvien = @manhanvien AND workstatus = 'IN_PROGRESS' and mals = @mals";

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                DateTime logoutTime = DateTime.Now;

                cmd.Parameters.AddWithValue("@logouttime", logoutTime);
                cmd.Parameters.AddWithValue("@manhanvien", employeeId);
                cmd.Parameters.AddWithValue("@mals", Datauser.IDLogin);

                int rowsUpdated = cmd.ExecuteNonQuery();

                if (rowsUpdated > 0)
                {
                    Console.WriteLine("Đăng xuất thành công cho nhân viên ID: " + employeeId);
                }
                else
                {
                    Console.WriteLine("Không tìm thấy phiên làm việc đang hoạt động cho nhân viên ID: " + employeeId);
                }
            }
        }


    }
    internal class Datauser {
        public static bool IsSuccess=false;
        public static string ID;
        public static string HoTen;
        public static string Role;
        public static int IDLogin;
    }
}
