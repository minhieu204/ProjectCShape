using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xls = Microsoft.Office.Interop.Excel;

namespace btl.Account
{
    public partial class Acctb : Form
    {
        Acc Acc;
        public Acctb(Acc parent)
        {
            InitializeComponent();
            this.Acc = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            dataGridView1.Columns["pass"].Visible=false;
            paneltk.Visible = false;
            cbgioitinhtk.SelectedItem = "---Chọn---";
            loadtb();
        }

        private void Acctb_Load(object sender, EventArgs e)
        {
            
        }
        public void loadtb()
        {
            Thuvien.LoadDatatk("select * from quanly", "select manhanvien as maquanly, hoten, gioitinh, maphanquyen, username, pass, sdt, email from nhanvien", dataGridView1);
        }
        public void ExportExcel_QuanLy(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu file Excel";
            sfd.FileName = "DanhSachTaiKhoan.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            var oBooks = oExcel.Workbooks;
            var oBook = oBooks.Add(Type.Missing);
            var oSheets = oBook.Worksheets;
            var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oExcel.Visible = false;
            oExcel.DisplayAlerts = false;
            oSheet.Name = "QL";

            var head = oSheet.get_Range("A1", "G1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH TÀI KHOẢN";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = 18;
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            string[] columnNames = { "MÃ QUẢN LÝ", "TÊN ĐĂNG NHẬP", "MÃ PHÂN QUYỀN", "HỌ TÊN", "GIỚI TÍNH", "SỐ ĐIỆN THOẠI", "EMAIL" };
            string[] columnLetters = { "A", "B", "C", "D", "E", "F", "G" };
            double[] columnWidths = { 15, 25, 20, 25, 15, 20, 30 };

            for (int i = 0; i < columnNames.Length; i++)
            {
                var col = oSheet.get_Range(columnLetters[i] + "3");
                col.Value2 = columnNames[i];
                col.ColumnWidth = columnWidths[i];
                col.Font.Bold = true;
                col.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                col.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            }

            oSheet.get_Range("F4", "F1000").NumberFormat = "@"; // SDT định dạng Text

            object[,] arr = new object[tb.Rows.Count, columnNames.Length];
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                arr[r, 0] = tb.Rows[r]["maquanly"];
                arr[r, 1] = tb.Rows[r]["username"];
                arr[r, 2] = tb.Rows[r]["maphanquyen"];
                arr[r, 3] = tb.Rows[r]["hoten"];
                arr[r, 4] = tb.Rows[r]["gioitinh"];
                arr[r, 5] = tb.Rows[r]["sdt"];
                arr[r, 6] = tb.Rows[r]["email"];
            }

            int rowStart = 4;
            int rowEnd = rowStart + tb.Rows.Count - 1;

            var c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, 1];
            var c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnNames.Length];
            var range = oSheet.get_Range(c1, c2);
            range.Value2 = arr;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            try
            {
                oBook.SaveAs(sfd.FileName);
                MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(sfd.FileName); // Mở file sau khi lưu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oBook.Close(false);
                oExcel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
            }
        }
        private void ReadExcel(string filename)
        {
            List<int> duplicateRows = new List<int>();
            StringBuilder errorMessages = new StringBuilder();
            bool hasError = false; 
            var loadingForm = new loadingForm();

            if (filename == null)
            {
                MessageBox.Show("Chưa chọn file");
                return;
            }

            loadingForm.Show();
            loadingForm.SetMessage("Đang đọc file...");

            xls.Application Excel = new xls.Application();
            xls.Workbook workbook = Excel.Workbooks.Open(filename);

            // Giai đoạn 1: Kiểm tra các lỗi và ghi vào log
            int totalRows = 0;
            foreach (xls.Worksheet wsheet in Excel.Worksheets)
            {
                int i = 2;
                totalRows = (int)wsheet.UsedRange.Rows.Count; // Lấy tổng số dòng dữ liệu

                do
                {
                    // Kiểm tra điều kiện dừng khi không còn dòng dữ liệu hợp lệ
                    if (wsheet.Cells[i, 1].Value == null && wsheet.Cells[i, 2].Value == null && wsheet.Cells[i, 3].Value == null &&
                        wsheet.Cells[i, 4].Value == null && wsheet.Cells[i, 5].Value == null && wsheet.Cells[i, 6].Value == null 
                        && wsheet.Cells[i, 7].Value == null && wsheet.Cells[i, 8].Value == null)
                    {
                        break;
                    }

                    string ma = wsheet.Cells[i, 1].Value.ToString();
                    string username = wsheet.Cells[i, 2].Value.ToString();
                    string phanquyen = wsheet.Cells[i, 4].Value.ToString();

                    // Kiểm tra trùng mã tài khoản và username trong cả hai bảng (quanly và nhanvien)
                    string sqlCheckQuanLyMa = $"SELECT COUNT(*) FROM quanly WHERE maquanly = '{ma}'";
                    string sqlCheckNhanVienMa = $"SELECT COUNT(*) FROM nhanvien WHERE manhanvien = '{ma}'";
                    string sqlCheckQuanLyUsername = $"SELECT COUNT(*) FROM quanly WHERE username = '{username}'";
                    string sqlCheckNhanVienUsername = $"SELECT COUNT(*) FROM nhanvien WHERE username = '{username}'";

                    bool isMaTrung = Thuvien.CheckExist(sqlCheckQuanLyMa) || Thuvien.CheckExist(sqlCheckNhanVienMa);
                    bool isUsernameTrung = Thuvien.CheckExist(sqlCheckQuanLyUsername) || Thuvien.CheckExist(sqlCheckNhanVienUsername);
                    bool isPhanQuyenInvalid = phanquyen != "ql" && phanquyen != "nv";

                    if (isMaTrung || isUsernameTrung || isPhanQuyenInvalid)
                    {
                        // Ghi lại dòng trùng và thêm thông báo lỗi
                        duplicateRows.Add(i);
                        if (isMaTrung)
                            errorMessages.AppendLine($"Dòng {i} bị trùng: Mã tài khoản đã tồn tại.");
                        if (isUsernameTrung)
                            errorMessages.AppendLine($"Dòng {i} bị trùng: Username đã tồn tại.");
                        if (isPhanQuyenInvalid)
                            errorMessages.AppendLine($"Dòng {i} bị lỗi: Phân quyền không hợp lệ.");

                        hasError = true; // Đánh dấu là có lỗi
                    }

                    // Cập nhật tiến trình
                    loadingForm.SetProgress((int)((float)i / totalRows * 100));

                    i++;
                    loadingForm.SetMessage($"Đang kiểm tra dòng {i}..."); // Cập nhật trạng thái
                }
                while (true);
            }

            // Giai đoạn 2: Nếu có lỗi, ghi log và hiển thị thông báo, không thêm vào DB
            if (hasError)
            {
                // Ghi thông tin vào file log
                string logFilePath = Path.Combine(Application.StartupPath, "Logs", "duplicate_log.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); // Tạo thư mục Logs nếu chưa có
                File.AppendAllText(logFilePath, $"Lỗi trùng vào lúc {DateTime.Now}\n");
                File.AppendAllText(logFilePath, errorMessages.ToString());
                File.AppendAllText(logFilePath, "=================================\n");

                // Hiển thị thông báo tóm tắt
                MessageBox.Show($"Phát hiện dữ liệu không hợp lệ, Chi tiết: {logFilePath}");
            }
            else
            {
                // Giai đoạn 2: Nếu không có lỗi, nhập dữ liệu vào cơ sở dữ liệu
                foreach (xls.Worksheet wsheet in Excel.Worksheets)
                {
                    int i = 2;
                    do
                    {
                        if (wsheet.Cells[i, 1].Value == null && wsheet.Cells[i, 2].Value == null && wsheet.Cells[i, 3].Value == null &&
                            wsheet.Cells[i, 4].Value == null && wsheet.Cells[i, 5].Value == null && wsheet.Cells[i, 6].Value == null
                            && wsheet.Cells[i, 7].Value == null && wsheet.Cells[i, 8].Value == null)
                        {
                            break;
                        }

                        string ma = wsheet.Cells[i, 1].Value.ToString();
                        string username = wsheet.Cells[i, 2].Value.ToString();
                        string phanquyen = wsheet.Cells[i, 4].Value.ToString();
                        Themmoitaikhoan(ma, username, phanquyen,
                            wsheet.Cells[i, 3].Value.ToString(), wsheet.Cells[i, 5].Value.ToString(), wsheet.Cells[i, 6].Value.ToString(), wsheet.Cells[i, 7].Value.ToString(), wsheet.Cells[i, 8].Value.ToString());
                        loadingForm.SetProgress((int)((float)i / totalRows * 100));
                        i++;
                        loadingForm.SetMessage($"Đang thêm dòng {i} vào cơ sở dữ liệu..."); // Cập nhật trạng thái
                    }
                    while (true);
                }

                MessageBox.Show("Dữ liệu đã được thêm vào cơ sở dữ liệu thành công.");
            }
            loadingForm.Close();
            workbook.Close(false); 
            Excel.Quit();
        }
        private void Themmoitaikhoan(String ma, String un, String pq, String pass, String ht, String gt, String sdt, String email)
        {
            if (pq == "ql")
            {
                String sql = String.Format("insert into quanly values('{0}', '{1}', '{2}', N'{3}', N'{4}', N'{5}', '{6}', '{7}')", ma, un, pass, pq, ht, gt, sdt, email);
                Thuvien.ExecuteQuery(sql);
            }
            else
            {
                String sql = String.Format("insert into nhanvien values('{0}', '{1}', '{2}', N'{3}', N'{4}', N'{5}', '{6}', '{7}')", ma, un, pass, pq, ht, gt, sdt, email);
                Thuvien.ExecuteQuery(sql);
            }
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
                string ma = row.Cells["maquanly"].Value.ToString();
                string ht = row.Cells["hoten"].Value.ToString();
                string gt = row.Cells["gioitinh"].Value.ToString();
                string pq = row.Cells["maphanquyen"].Value.ToString();
                string un = row.Cells["username"].Value.ToString();
                string pw = row.Cells["pass"].Value.ToString();
                string sdt = row.Cells["sdt"].Value.ToString();
                string email = row.Cells["email"].Value.ToString();
                int i = 0;
                Acc.acctv.setData(ma, ht, gt, pq, un, pw, sdt, email, i);
                Acc.SwitchToTab(1);
            }
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            String sql = "";
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["maquanly"].Value.ToString();
            string pq = row.Cells["maphanquyen"].Value.ToString();
            if (ma==Datauser.ID) {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập");
                return;
            }
            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không ?", "Xác nhận: ", MessageBoxButtons.YesNo);
            if (mess_delete == DialogResult.Yes)
            {
                if (pq == "ql")
                {
                    sql = "delete from quanly where maquanly = '" + ma + "'";
                }
                else
                {
                    sql = "delete from nhanvien where manhanvien = '" + ma + "'";
                }
            }
            Thuvien.ExecuteQuery(sql);
            loadtb();
            MessageBox.Show("Xóa tài khoản thành công!","Thông báo!");

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String tk = textBox1.Text;
            String sql1 = "select * from quanly where hoten like N'%" + tk + "%' or maquanly like N'%" + tk + "%' or username like N'%" + tk + "%' or sdt like N'%" + tk + "%' or email like N'%" + tk + "%'";
            String sql2 = "select manhanvien as maquanly, hoten, gioitinh, maphanquyen, username, pass, sdt, email from nhanvien where hoten like N'%" + tk + "%' or manhanvien like N'%" + tk + "%' or username like N'%" + tk + "%' or sdt like N'%" + tk + "%' or email like N'%" + tk + "%'";
            Thuvien.LoadDatatk(sql1, sql2, dataGridView1);
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(234, 132, 50);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(74, 125, 175);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
            if (paneltk.Visible)
            {
                paneltk.Visible = false;
                textBox1.Enabled = true;
                loadtb();
            }
            else
            {
                paneltk.Visible = true;
                textBox1.Enabled = false;
                textBox1.Text = "";
            }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            String ma = matk.Text;
            String ht = httk.Text;
            String gt = cbgioitinhtk.SelectedItem.ToString();
            if (gt == "---Chọn---")
            {
                gt = "";
            }
            String un = unametk.Text;
            String sdt = sdttk.Text;
            String email = emailtk.Text;
            String sql1 = "select * from quanly where hoten like N'%" + ht + "%' and maquanly like N'%" + ma + "%' and username like N'%" + un + "%' and sdt like N'%" + sdt + "%' and email like N'%" + email + "%' and gioitinh like N'%" + gt + "%'";
            String sql2 = "select manhanvien as maquanly, hoten, gioitinh, maphanquyen, username, pass, sdt, email from nhanvien where hoten like N'%" + ht + "%' and manhanvien like N'%" + ma + "%' and username like N'%" + un + "%' and sdt like N'%" + sdt + "%' and email like N'%" + email + "%' and gioitinh like N'%" + gt + "%'";
            Thuvien.LoadDatatk(sql1, sql2, dataGridView1);
        }

        private void sdttk_Leave(object sender, EventArgs e)
        {

        }

        private void sdttk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx|All Files|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Chọn file Excel";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                ReadExcel(filePath);
                loadtb();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
           String sql1 = "select * from quanly";
            string sql2 = "select manhanvien as maquanly, hoten, gioitinh, maphanquyen, username, pass, sdt, email from nhanvien";
            Thuvien.LoadExceltk(sql1, sql2, dt);
            ExportExcel_QuanLy(dt);
        }
    }
}
