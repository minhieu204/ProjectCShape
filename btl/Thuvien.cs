using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using iText.IO.Font;
using System.Diagnostics;

namespace btl
{
    internal class Thuvien
    {
        private static readonly string connectionString = "Data Source=.;Initial Catalog=QLSIEUTHI;User ID=sa;Password=1306;";

        public static void GenerateInvoice(string filePath, string tenkh)
        {
            // Kết nối cơ sở dữ liệu và truy vấn thông tin đơn hàng
            string query = "select tensp, soluongnhap, donvitinh, giaban, thanhtien from giohang";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                // Khởi tạo PdfWriter và PdfDocument
                using (PdfWriter writer = new PdfWriter(filePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    // Tạo đối tượng Document để thêm nội dung vào PDF
                    Document document = new Document(pdf);

                    // Chỉ định font có hỗ trợ tiếng Việt (vd: Arial hoặc Times New Roman)
                    string fontPath = @"C:\Windows\Fonts\arial.ttf";  // Đường dẫn tới font Arial
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);  // Sử dụng font Arial có hỗ trợ Unicode

                    // Font in đậm (Arial-Bold)
                    string boldFontPath = @"C:\Windows\Fonts\arialbd.ttf";  // Đường dẫn tới font Arial-Bold
                    PdfFont boldFont = PdfFontFactory.CreateFont(boldFontPath, PdfEncodings.IDENTITY_H);  // Sử dụng font Arial-Bold có hỗ trợ Unicode

                    // Thêm tiêu đề cho hóa đơn
                    document.Add(new Paragraph("HÓA ĐƠN")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20)
                        .SetFont(boldFont));  // Sử dụng font in đậm cho tiêu đề

                    document.Add(new Paragraph("Ngày: " + DateTime.Now.ToString("dd/MM/yyyy"))
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetFontSize(12)
                        .SetFont(font));  // Sử dụng font hỗ trợ tiếng Việt

                    document.Add(new Paragraph("Khách hàng: " + tenkh)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(12)
                        .SetFont(font));  // Sử dụng font hỗ trợ tiếng Việt

                    // Tạo bảng cho chi tiết đơn hàng
                    float[] columnWidths = { 3, 2, 3, 2, 2 };  // Định nghĩa chiều rộng cột
                    Table table = new Table(columnWidths);

                    // Thêm tiêu đề cột cho bảng
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Tên Sản Phẩm").SetTextAlignment(TextAlignment.CENTER).SetFont(boldFont)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Số Lượng").SetTextAlignment(TextAlignment.CENTER).SetFont(boldFont)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Đơn Vị Tính").SetTextAlignment(TextAlignment.CENTER).SetFont(boldFont)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Đơn Giá").SetTextAlignment(TextAlignment.CENTER).SetFont(boldFont)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Thành Tiền").SetTextAlignment(TextAlignment.CENTER).SetFont(boldFont)));

                    // Duyệt qua từng dòng dữ liệu và thêm vào bảng
                    decimal totalAmount = 0;
                    while (reader.Read())
                    {
                        string tensp = reader["tensp"].ToString();
                        string soluong = reader["soluongnhap"].ToString();
                        string dvt = reader["donvitinh"].ToString();
                        decimal giaban = Convert.ToDecimal(reader["giaban"]);
                        decimal thanhtien = Convert.ToDecimal(reader["thanhtien"]);

                        // Thêm dữ liệu vào bảng
                        table.AddCell(new Cell().Add(new Paragraph(tensp).SetTextAlignment(TextAlignment.LEFT).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(soluong).SetTextAlignment(TextAlignment.CENTER).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(dvt).SetTextAlignment(TextAlignment.CENTER).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(String.Format("{0:N0}", giaban) + " VNĐ").SetTextAlignment(TextAlignment.RIGHT).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(String.Format("{0:N0}", thanhtien) + " VNĐ").SetTextAlignment(TextAlignment.RIGHT).SetFont(font)));

                        totalAmount += thanhtien; // Cộng dồn tổng tiền
                    }

                    // Thêm bảng vào tài liệu PDF
                    document.Add(table);

                    // Thêm tổng tiền vào tài liệu
                    document.Add(new Paragraph("Tổng cộng: " + String.Format("{0:N0}", totalAmount) + " VNĐ")
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetFontSize(14)
                        .SetFont(boldFont));  // Sử dụng font in đậm cho tổng tiền
                }
            }

            Console.WriteLine("Hóa đơn đã được tạo thành công!");

            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }
        public static void CustomDisabledButton (Button button)
        {
            button.BackColor = Color.FromArgb(180, 210, 235); 
            button.ForeColor = Color.DarkGray; 
            button.Enabled = false;
        }

        public static void CustomEnabledButton(Button button)
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
        public static void LoadExceltk(string sql, string sql2, DataTable dt)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd1 = new SqlCommand(sql, con))
            using (SqlCommand cmd2 = new SqlCommand(sql2, con))
            using (SqlDataAdapter da1 = new SqlDataAdapter(cmd1))
            using (SqlDataAdapter da2 = new SqlDataAdapter(cmd2))
            {
                DataTable dt2 = new DataTable();
                da1.Fill(dt);
                da2.Fill(dt2);
                dt.Merge(dt2);
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
