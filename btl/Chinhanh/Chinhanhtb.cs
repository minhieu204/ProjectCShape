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

namespace btl.Chinhanh
{
    public partial class Chinhanhtb : Form
    {
        Chinhanh Chinhanh;
        public Chinhanhtb(Chinhanh parent)
        {
            InitializeComponent();
            this.Chinhanh = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
        }

        public void loadtb()
        {
            Thuvien.LoadData("select * from ChiNhanh", dataGridView1);
        }

        private void ReadExcel_ChiNhanh(string filename)
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
                    if (wsheet.Cells[i, 1].Value == null &&
                        wsheet.Cells[i, 2].Value == null &&
                        wsheet.Cells[i, 3].Value == null &&
                        wsheet.Cells[i, 4].Value == null &&
                        wsheet.Cells[i, 5].Value == null)
                        break;

                    string ma = wsheet.Cells[i, 1].Value.ToString();
                    string sqlCheckMa = $"SELECT COUNT(*) FROM ChiNhanh WHERE Machinhanh = '{ma}'";

                    if (Thuvien.CheckExist(sqlCheckMa))
                    {
                        duplicateRows.Add(i);
                        errorMessages.AppendLine($"Dòng {i} bị trùng: Mã chi nhánh đã tồn tại.");
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
                string logFilePath = Path.Combine(Application.StartupPath, "Logs", "log_chinhanh.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                File.AppendAllText(logFilePath, $"Lỗi trùng vào lúc {DateTime.Now}\n");
                File.AppendAllText(logFilePath, errorMessages.ToString());
                File.AppendAllText(logFilePath, "=================================\n");
                MessageBox.Show($"Phát hiện dữ liệu không hợp lệ, Chi tiết: {logFilePath}");
            }
            else
            {
                foreach (xls.Worksheet wsheet in Excel.Worksheets)
                {
                    int i = 2;
                    do
                    {
                        if (wsheet.Cells[i, 1].Value == null &&
                            wsheet.Cells[i, 2].Value == null &&
                            wsheet.Cells[i, 3].Value == null &&
                            wsheet.Cells[i, 4].Value == null &&
                            wsheet.Cells[i, 5].Value == null)
                            break;

                        ThemChiNhanh(
                            wsheet.Cells[i, 1].Value.ToString(),
                            wsheet.Cells[i, 2].Value.ToString(),
                            wsheet.Cells[i, 3].Value.ToString(),
                            wsheet.Cells[i, 4].Value.ToString(),
                            wsheet.Cells[i, 5].Value.ToString()
                        );

                        loadingForm.SetProgress((int)((float)i / totalRows * 100));
                        loadingForm.SetMessage($"Đang thêm dòng {i} vào cơ sở dữ liệu...");
                        i++;
                    }
                    while (true);
                }

                MessageBox.Show("Dữ liệu đã được thêm vào cơ sở dữ liệu thành công.");
            }

            loadingForm.Close();
            workbook.Close(false);
            Excel.Quit();
        }

        private void ThemChiNhanh(string ma, string ten, string diachi, string email, string sdt)
        {
            string sql = $"INSERT INTO ChiNhanh VALUES('{ma}', N'{ten}', N'{diachi}', '{email}', '{sdt}')";
            Thuvien.ExecuteQuery(sql);
        }

        public void ExportExcel_ChiNhanh(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo thư mục HoaDon nếu chưa có
            string folderPath = Path.Combine(Application.StartupPath, "HoaDon");
            Directory.CreateDirectory(folderPath);

            // Tạo tên file
            string filePath = Path.Combine(folderPath, $"DanhSachChiNhanh_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");

            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            var oBooks = oExcel.Workbooks;
            var oBook = oBooks.Add(Type.Missing);
            var oSheets = oBook.Worksheets;
            var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oExcel.Visible = false;
            oExcel.DisplayAlerts = false;
            oSheet.Name = "ChiNhanh";

            var head = oSheet.get_Range("A1", "E1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH CHI NHÁNH";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = 18;
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            string[] columnNames = { "MÃ CHI NHÁNH", "TÊN CHI NHÁNH", "ĐỊA CHỈ", "EMAIL", "SỐ ĐIỆN THOẠI" };
            string[] columnLetters = { "A", "B", "C", "D", "E" };
            double[] columnWidths = { 15, 30, 35, 30, 20 };

            for (int i = 0; i < columnNames.Length; i++)
            {
                var col = oSheet.get_Range(columnLetters[i] + "3");
                col.Value2 = columnNames[i];
                col.ColumnWidth = columnWidths[i];
                col.Font.Bold = true;
                col.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                col.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            }

            oSheet.get_Range("E4", "E1000").NumberFormat = "@"; // SDT dạng text

            object[,] arr = new object[tb.Rows.Count, columnNames.Length];
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                arr[r, 0] = tb.Rows[r]["Machinhanh"];
                arr[r, 1] = tb.Rows[r]["Tenchinhanh"];
                arr[r, 2] = tb.Rows[r]["Diachi"];
                arr[r, 3] = tb.Rows[r]["Email"];
                arr[r, 4] = tb.Rows[r]["SDT"];
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
                oBook.SaveAs(filePath);
                MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(filePath); // Mở file sau khi lưu
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
                string ma = row.Cells["Machinhanh"].Value.ToString();
                string ten = row.Cells["Tenchinhanh"].Value.ToString();
                string dt = row.Cells["SDT"].Value.ToString();
                string em = row.Cells["Email"].Value.ToString();
                string dc = row.Cells["Diachi"].Value.ToString();
                Chinhanh.chinhanhtv.SetData(ma, ten, dt, em, dc);
                Chinhanh.SwitchToTab(1);
            }
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            String sql = "";
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["Machinhanh"].Value.ToString();
            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không ?", "Xác nhận: ", MessageBoxButtons.YesNo);
            sql = "delete from ChiNhanh where Machinhanh = '" + ma + "'";
            Thuvien.ExecuteQuery(sql);
            loadtb();
            MessageBox.Show("Xóa tài khoản thành công!", "Thông báo!");
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
                ReadExcel_ChiNhanh(filePath);
                loadtb();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sql = "select * from ChiNhanh";
            Thuvien.LoadExcel(sql, dt);
            ExportExcel_ChiNhanh(dt);
        }
    }
}
