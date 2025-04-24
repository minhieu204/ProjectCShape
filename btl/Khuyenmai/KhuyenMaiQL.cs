using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xls = Microsoft.Office.Interop.Excel;


namespace btl.Khuyenmai
{
    public partial class KhuyenMaiQL : Form
    {
        KhuyenMai khuyenMai;
        public KhuyenMaiQL(KhuyenMai parent)
        {
            InitializeComponent();
            this.khuyenMai = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
        }
        public void loadtb()
        {
            Thuvien.LoadData("select * from KhuyenMai", dataGridView1);
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
                string ma = row.Cells["MaKhuyenMai"].Value.ToString();
                string ten = row.Cells["TenKhuyenMai"].Value.ToString();
                DateTime bd = DateTime.Parse(row.Cells["NgayBatDau"].Value.ToString());
                DateTime kt = DateTime.Parse(row.Cells["NgayKetThuc"].Value.ToString());
                string cbo = row.Cells["MaSanPham"].Value.ToString();
                string ggoc = row.Cells["GiaGoc"].Value.ToString();
                string giamgia = row.Cells["GiamGia"].Value.ToString();
                string gsaugiam = row.Cells["GiaSauGiam"].Value.ToString();
                khuyenMai.khuyenMaiTV.SetData( ma,  ten,  bd,  kt,  cbo,  ggoc, gsaugiam,  giamgia);
                khuyenMai.SwitchToTab(1);
            }
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            String sql = "";
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["MaKhuyenMai"].Value.ToString();
            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không ?", "Xác nhận: ", MessageBoxButtons.YesNo);
            sql = "delete from KhuyenMai where MaKhuyenMai = '" + ma + "'";
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
                ReadExcel_KhuyenMai(filePath);
         
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sql = "select * from KhuyenMai";
            Thuvien.LoadExcel(sql, dt);
            ExportExcel_KhuyenMai(dt);
        }

        

        public void ExportExcel_KhuyenMai(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu file Excel";
            sfd.FileName = "DanhSachKhuyenMai.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            var oBook = oExcel.Workbooks.Add(Type.Missing);
            var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Worksheets[1];
            oExcel.Visible = false;
            oExcel.DisplayAlerts = false;
            oSheet.Name = "KHUYENMAI";

            oSheet.get_Range("A1", "H1").MergeCells = true;
            oSheet.Cells[1, 1] = "DANH SÁCH KHUYẾN MÃI";
            oSheet.Cells[1, 1].Font.Size = 18;
            oSheet.Cells[1, 1].Font.Bold = true;
            oSheet.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            string[] headers = { "Mã KM", "Tên KM", "Ngày Bắt Đầu", "Ngày Kết Thúc", "Mã Sản Phẩm", "Giá Gốc", "Giảm Giá (%)", "Giá Sau Giảm" };
            for (int i = 0; i < headers.Length; i++)
            {
                oSheet.Cells[3, i + 1] = headers[i];
                var cell = oSheet.Cells[3, i + 1];
                cell.Font.Bold = true;
                cell.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                cell.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ((Microsoft.Office.Interop.Excel.Range)cell).ColumnWidth = 20;
            }

            object[,] data = new object[tb.Rows.Count, headers.Length];
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                data[i, 0] = tb.Rows[i]["MaKhuyenMai"];
                data[i, 1] = tb.Rows[i]["TenKhuyenMai"];
                data[i, 2] = Convert.ToDateTime(tb.Rows[i]["NgayBatDau"]).ToString("dd/MM/yyyy");
                data[i, 3] = Convert.ToDateTime(tb.Rows[i]["NgayKetThuc"]).ToString("dd/MM/yyyy");
                data[i, 4] = tb.Rows[i]["MaSanPham"];
                data[i, 5] = tb.Rows[i]["GiaGoc"];
                data[i, 6] = tb.Rows[i]["GiamGia"];
                data[i, 7] = tb.Rows[i]["GiaSauGiam"];
            }

            var startCell = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[4, 1];
            var endCell = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[3 + tb.Rows.Count, headers.Length];
            var writeRange = oSheet.get_Range(startCell, endCell);
            writeRange.Value2 = data;
            writeRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            try
            {
                oBook.SaveAs(sfd.FileName);
                MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(sfd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu file: " + ex.Message);
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



        private void ReadExcel_KhuyenMai(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Vui lòng chọn file Excel.");
                    return;
                }

                xls.Application excelApp = new xls.Application();
                xls.Workbook workbook = excelApp.Workbooks.Open(filePath);
                xls.Worksheet worksheet = (xls.Worksheet)workbook.Sheets[1];

                xls.Range usedRange = worksheet.UsedRange;
                int rowCount = usedRange.Rows.Count;
                int colCount = usedRange.Columns.Count;

                // Tạo DataTable
                DataTable dt = new DataTable();

                // Thêm cột từ dòng đầu tiên (giả sử là tiêu đề)
                for (int col = 1; col <= colCount; col++)
                {
                    string colName = usedRange.Cells[1, col].Value?.ToString() ?? $"Cột {col}";
                    dt.Columns.Add(colName);
                }

                // Thêm dữ liệu từ dòng 2 trở đi
                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        dataRow[col - 1] = usedRange.Cells[row, col].Value?.ToString() ?? "";
                    }
                    dt.Rows.Add(dataRow);
                }

                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = dt;

                // Đóng Excel
                workbook.Close(false);
                excelApp.Quit();
                MessageBox.Show("Đọc dữ liệu từ Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc Excel: " + ex.Message);
            }
        }


    }
}
