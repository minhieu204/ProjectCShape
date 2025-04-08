using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace btl.ThongKe
{
    public partial class ThongKeQL : Form
    {
        public ThongKe thongKe;
        public ThongKeQL(ThongKe parent)
        {
            InitializeComponent();
            Thuvien.CustomDataGridView(dataGridView1);
            this.thongKe = parent;
            comboBox1.SelectedItem = "Hàng Bán Chạy";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int check = comboBox1.SelectedIndex;

            if (check == 0)
            {
                string query = @"SELECT sp.masp AS MaHang, sp.tensp AS TenHang, ncc.mancc AS MaNCC, sp.giaban AS GiaBan, 
                    SUM(ctdh.soluong) AS TongSoluongBan, sp.soluong AS SoluongTon 
                    FROM chitietdonhang ctdh 
                    JOIN sanpham sp ON ctdh.masp = sp.masp 
                    JOIN nhacungcap ncc ON sp.mancc = ncc.mancc 
                    GROUP BY sp.masp, sp.tensp, ncc.mancc, sp.giaban, sp.soluong ORDER BY sp.soluong DESC";

                // Sử dụng phương thức LoadData từ Thuvien
                Thuvien.LoadData(query, dataGridView1);
            }
            else if (check == 1)
            {
                string query = "SELECT s.masp, s.tensp, s.mancc, s.giaban, s.soluong FROM sanpham AS s WHERE s.soluong > 100 ORDER BY s.soluong DESC, s.tensp ASC;";

                Thuvien.LoadData(query, dataGridView1);
            }
            else if (check == 2)
            {
                string query = "SELECT s.masp, s.tensp, s.mancc, s.giaban, s.soluong FROM sanpham AS s WHERE s.soluong < 50 ORDER BY s.soluong ASC, s.tensp ASC;";

                Thuvien.LoadData(query, dataGridView1);
            }
        
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ExportExcel_DataGridView();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ImportExcelToDataGridView();
        }


        private void ExportExcel_DataGridView()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu file Excel";
            sfd.FileName = "ThongKe.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var excel = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excel.Workbooks.Add(Type.Missing);
            var sheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
            excel.Visible = false;
            excel.DisplayAlerts = false;
            sheet.Name = "ThongKe";

            int columnsCount = dataGridView1.Columns.Count;

            // Tiêu đề lớn
            sheet.get_Range("A1", GetExcelColumn(columnsCount) + "1").MergeCells = true;
            sheet.Cells[1, 1] = "BÁO CÁO THỐNG KÊ";
            sheet.Cells[1, 1].Font.Size = 18;
            sheet.Cells[1, 1].Font.Bold = true;
            sheet.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Ghi header
            for (int i = 0; i < columnsCount; i++)
            {
                sheet.Cells[3, i + 1] = dataGridView1.Columns[i].HeaderText;
                var cell = sheet.Cells[3, i + 1];
                cell.Font.Bold = true;
                cell.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                cell.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ((Microsoft.Office.Interop.Excel.Range)cell).ColumnWidth = 20;
            }

            // Ghi dữ liệu
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    sheet.Cells[i + 4, j + 1] = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                }
            }

            try
            {
                workbook.SaveAs(sfd.FileName);
                MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu file: " + ex.Message);
            }
            finally
            {
                workbook.Close(false);
                excel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            }
        }

        // Hàm phụ để chuyển số cột thành ký tự A, B, ..., Z, AA, AB,...
        private string GetExcelColumn(int columnNumber)
        {
            string column = "";
            while (columnNumber > 0)
            {
                int mod = (columnNumber - 1) % 26;
                column = (char)(65 + mod) + column;
                columnNumber = (columnNumber - mod) / 26;
            }
            return column;
        }



        private void ImportExcelToDataGridView()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Workbook|*.xlsx;*.xls";
            ofd.Title = "Chọn file Excel";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var excel = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excel.Workbooks.Open(ofd.FileName);
            var sheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            int row = 4; // Dòng bắt đầu chứa dữ liệu (sau tiêu đề lớn + header)
            int colCount = sheet.UsedRange.Columns.Count;

            DataTable dt = new DataTable();

            // Tạo cột từ dòng 3 (header)
            for (int i = 1; i <= colCount; i++)
            {
                string columnName = sheet.Cells[3, i].Value?.ToString() ?? $"Cột {i}";
                dt.Columns.Add(columnName);
            }

            // Đọc dữ liệu
            while (sheet.Cells[row, 1].Value != null)
            {
                DataRow dr = dt.NewRow();
                for (int i = 1; i <= colCount; i++)
                {
                    dr[i - 1] = sheet.Cells[row, i].Value?.ToString();
                }
                dt.Rows.Add(dr);
                row++;
            }

            dataGridView1.DataSource = dt;

            workbook.Close(false);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

            MessageBox.Show("Nhập dữ liệu từ Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
