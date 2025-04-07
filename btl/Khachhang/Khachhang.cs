using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using xls = Microsoft.Office.Interop.Excel;

namespace btl.Khachhang
{
    public partial class Khachhang : Form
    {
        public Khachhang()
        {
            InitializeComponent();
            paneltk.Visible = false;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
        }
        public void loadtb()
        {
            String sql = "select * from khachhang";
            Thuvien.LoadData(sql, dataGridView1);
        }
        private void ReadExcel_KhachHang(string filename)
        {
            List<int> duplicateRows = new List<int>();
            StringBuilder errorMessages = new StringBuilder();
            bool hasError = false;
            var loadingForm = new Account.loadingForm();

            if (filename == null)
            {
                MessageBox.Show("Chưa chọn file");
                return;
            }

            loadingForm.Show();
            loadingForm.SetMessage("Đang đọc file...");

            xls.Application Excel = new xls.Application();
            xls.Workbook workbook = Excel.Workbooks.Open(filename);

            int totalRows = 0;
            foreach (xls.Worksheet wsheet in Excel.Worksheets)
            {
                int i = 2;
                totalRows = (int)wsheet.UsedRange.Rows.Count;

                do
                {
                    if (wsheet.Cells[i, 1].Value == null && wsheet.Cells[i, 2].Value == null && wsheet.Cells[i, 3].Value == null)
                        break;

                    string sdt = wsheet.Cells[i, 1]?.Value?.ToString().Trim();
                    string hoten = wsheet.Cells[i, 2]?.Value?.ToString().Trim();
                    string diemStr = wsheet.Cells[i, 3]?.Value?.ToString().Trim();
                    int diem;

                    string sqlCheck = $"SELECT COUNT(*) FROM khachhang WHERE sdt = '{sdt}'";
                    bool isDuplicate = Thuvien.CheckExist(sqlCheck);

                    // SĐT Việt Nam: bắt đầu 03|05|07|08|09 và 10 chữ số
                    bool isPhoneValid = Regex.IsMatch(sdt, @"^(03|05|07|08|09)\d{8}$");

                    if (isDuplicate || !isPhoneValid || !int.TryParse(diemStr, out diem))
                    {
                        duplicateRows.Add(i);
                        if (isDuplicate)
                            errorMessages.AppendLine($"Dòng {i} bị trùng: Số điện thoại đã tồn tại.");
                        if (!isPhoneValid)
                            errorMessages.AppendLine($"Dòng {i} lỗi: Số điện thoại không hợp lệ.");
                        if (!int.TryParse(diemStr, out diem))
                            errorMessages.AppendLine($"Dòng {i} lỗi: Điểm phải là số nguyên.");

                        hasError = true;
                    }

                    loadingForm.SetProgress((int)((float)i / totalRows * 100));
                    loadingForm.SetMessage($"Đang kiểm tra dòng {i}...");
                    i++;
                }
                while (true);
            }

            if (hasError)
            {
                string logFilePath = Path.Combine(Application.StartupPath, "Logs", "khachhang_log.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                File.AppendAllText(logFilePath, $"Lỗi vào lúc {DateTime.Now}\n");
                File.AppendAllText(logFilePath, errorMessages.ToString());
                File.AppendAllText(logFilePath, "=================================\n");

                MessageBox.Show($"Phát hiện lỗi dữ liệu! Chi tiết: {logFilePath}");
            }
            else
            {
                foreach (xls.Worksheet wsheet in Excel.Worksheets)
                {
                    int i = 2;
                    do
                    {
                        if (wsheet.Cells[i, 1].Value == null && wsheet.Cells[i, 2].Value == null && wsheet.Cells[i, 3].Value == null)
                            break;

                        string sdt = wsheet.Cells[i, 1]?.Value?.ToString().Trim();
                        string hoten = wsheet.Cells[i, 2]?.Value?.ToString().Trim();
                        int diem = int.Parse(wsheet.Cells[i, 3]?.Value?.ToString().Trim());

                        string sqlInsert = $"INSERT INTO khachhang VALUES ('{sdt}', N'{hoten}', {diem})";
                        Thuvien.ExecuteQuery(sqlInsert);

                        loadingForm.SetProgress((int)((float)i / totalRows * 100));
                        loadingForm.SetMessage($"Đang thêm dòng {i} vào cơ sở dữ liệu...");
                        i++;
                    }
                    while (true);
                }

                MessageBox.Show("Nhập dữ liệu thành công!");
            }

            loadingForm.Close();
            workbook.Close(false);
            Excel.Quit();
        }
        public void ExportExcel_KhachHang(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu file Excel";
            sfd.FileName = "DanhSachKhachHang.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            var oBook = oExcel.Workbooks.Add(Type.Missing);
            var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Sheets[1];

            oExcel.Visible = false;
            oExcel.DisplayAlerts = false;
            oSheet.Name = "KH";

            var head = oSheet.get_Range("A1", "C1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH KHÁCH HÀNG";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = 18;
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            string[] columnNames = { "SỐ ĐIỆN THOẠI", "HỌ TÊN", "ĐIỂM" };
            string[] columnLetters = { "A", "B", "C" };
            double[] columnWidths = { 20, 30, 10 };

            for (int i = 0; i < columnNames.Length; i++)
            {
                var col = oSheet.get_Range(columnLetters[i] + "3");
                col.Value2 = columnNames[i];
                col.ColumnWidth = columnWidths[i];
                col.Font.Bold = true;
                col.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                col.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            }

            oSheet.get_Range("A4", "A1000").NumberFormat = "@";

            object[,] arr = new object[tb.Rows.Count, columnNames.Length];
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                arr[r, 0] = tb.Rows[r]["sdt"].ToString(); 
                arr[r, 1] = tb.Rows[r]["hoten"].ToString(); 
                arr[r, 2] = tb.Rows[r]["diem"];
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
                Process.Start(sfd.FileName);
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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
            }
        }
        private void ThemKhachHang(string sdt, string hoten, int diem)
        {
            string sql = $"INSERT INTO khachhang VALUES('{sdt}', N'{hoten}', {diem})";
            Thuvien.ExecuteQuery(sql);
        }
        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(234, 132, 50);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(74, 125, 175);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            txttk.Enabled = !txttk.Enabled;
            paneltk.Visible = !paneltk.Visible;
            txttk.Text = "";
            if (!paneltk.Visible)
            {
                matk.Text = "";
                httk.Text = "";
                diembd.Text = "";
                diemkt.Text = "";
            }
            
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dataGridView1.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hitTest.RowIndex].Selected = true;
                    context.Show(dataGridView1, new Point(e.X, e.Y));
                }
            }
        }

        private void suacm_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["sdt"].Value.ToString();
            string ht = row.Cells["hoten"].Value.ToString();
            string gt = row.Cells["diem"].Value.ToString();
            UpdateKH updateKH = new UpdateKH(this);
            updateKH.setdata(ma, ht, gt);
            updateKH.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            UpdateKH updateKH = new UpdateKH(this);
            updateKH.reset();
            updateKH.ShowDialog();
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["sdt"].Value.ToString();
            String sql = "delete from khachhang where sdt='" + ma + "'";
            Thuvien.ExecuteQuery(sql);
            loadtb();
            MessageBox.Show("Xóa thành công!");
        }

        private void txttk_TextChanged(object sender, EventArgs e)
        {
            String sql = "select * from khachhang where sdt like '%" + txttk.Text + "%' or hoten like N'%" + txttk.Text + "%'";
            Thuvien.LoadData(sql, dataGridView1);
        }

        private void matk_TextChanged(object sender, EventArgs e)
        {
            String ma = matk.Text;
            String sdt = httk.Text;
            int diembd1 = 0;
            int diemkt1 = int.MaxValue;
            if (diembd.Text == "")
            {
                diembd1 = 0;
            }
            else
            {
                diembd1 = int.Parse(diembd.Text.ToString());
            }
            if (diemkt.Text=="") 
            {
                diemkt1 = int.MaxValue;
            }
            else
            {
                diemkt1 = int.Parse(diemkt.Text.ToString());
            }
            string sql = $"select * from khachhang where sdt like '%{ma}%' and hoten like N'%{sdt}%' and diem between {diembd1} and {diemkt1}";
            Thuvien.LoadData(sql, dataGridView1);
        }

        private void matk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sql = "select * from khachhang";
            Thuvien.LoadExcel(sql, dt);
            ExportExcel_KhachHang(dt);
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
                ReadExcel_KhachHang(filePath);
                loadtb();
            }
        }
    }
}
