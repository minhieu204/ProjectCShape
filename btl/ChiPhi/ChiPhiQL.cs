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

namespace btl.ChiPhi
{
    public partial class ChiPhiQL : Form
    {
        ChiPhi chiPhi;
        public ChiPhiQL(ChiPhi parent)
        {
            InitializeComponent();
            this.chiPhi = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
            
        }
        public void loadtb()
        {
            Thuvien.LoadData("select * from ChiPhi", dataGridView1);
            TinhTongChiPhi();
        }

        private void TinhTongChiPhi()
        {
            decimal tongSuaChua = 0;
            decimal tongTienDien = 0;
            decimal tongTienNuoc = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; // bỏ qua dòng trống cuối cùng

                if (decimal.TryParse(row.Cells["PhiSuaChua"].Value?.ToString(), out decimal suaChua))
                    tongSuaChua += suaChua;

                if (decimal.TryParse(row.Cells["TienDien"].Value?.ToString(), out decimal dien))
                    tongTienDien += dien;

                if (decimal.TryParse(row.Cells["TienNuoc"].Value?.ToString(), out decimal nuoc))
                    tongTienNuoc += nuoc;
            }

            // Hiển thị kết quả vào các TextBox
            lblTongSuaChua.Text = tongSuaChua.ToString("N2");
            lblTongTienDien.Text = tongTienDien.ToString("N2");
            lblTongTienNuoc.Text = tongTienNuoc.ToString("N2");
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void suacm_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                string id = row.Cells["ID"].Value.ToString();
                string tienDien = row.Cells["TienDien"].Value.ToString();
                string tienNuoc = row.Cells["TienNuoc"].Value.ToString();
                DateTime thangNam = DateTime.Parse(row.Cells["ThangNam"].Value.ToString());
                string phiSuaChua = row.Cells["PhiSuaChua"].Value.ToString();

                chiPhi.chiPhiTV.SetData(id, tienDien, tienNuoc, thangNam, phiSuaChua);

                chiPhi.SwitchToTab(1);
                TinhTongChiPhi();
            }

        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string id = row.Cells["ID"].Value.ToString(); // Lấy ID của dòng cần xóa

            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (mess_delete == DialogResult.Yes)
            {
                string sql = "DELETE FROM ChiPhi WHERE ID = '" + id + "'";
                Thuvien.ExecuteQuery(sql);
                loadtb(); // Tải lại bảng sau khi xóa
                MessageBox.Show("Xóa chi phí thành công!", "Thông báo!");
            }
        }

        private void dataGridView1_MouseDown_1(object sender, MouseEventArgs e)
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sql = "select * from chiphi";
            Thuvien.LoadExcel(sql, dt);
            ExportExcel_ChiPhi(dt);
            TinhTongChiPhi();
        }

        public void ExportExcel_ChiPhi(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook|*.xlsx";
                sfd.Title = "Lưu file Excel";
                sfd.FileName = "DanhSachChiPhi.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                var oExcel = new Microsoft.Office.Interop.Excel.Application();
                var oBook = oExcel.Workbooks.Add(Type.Missing);
                var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Sheets[1];

                oExcel.Visible = false;
                oExcel.DisplayAlerts = false;
                oSheet.Name = "ChiPhi";

                var head = oSheet.get_Range("A1", "E1");
                head.MergeCells = true;
                head.Value2 = "DANH SÁCH CHI PHÍ";
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = 18;
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                string[] columnNames = { "ID", "THÁNG NĂM", "PHÍ SỬA CHỮA", "TIỀN ĐIỆN", "TIỀN NƯỚC" };
                string[] columnLetters = { "A", "B", "C", "D", "E" };
                double[] columnWidths = { 10, 20, 20, 20, 20 };

                for (int i = 0; i < columnNames.Length; i++)
                {
                    var col = oSheet.get_Range(columnLetters[i] + "3");
                    col.Value2 = columnNames[i];
                    col.ColumnWidth = columnWidths[i];
                    col.Font.Bold = true;
                    col.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                    col.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                }

                // Format ngày tháng
                oSheet.get_Range("B4", "B1000").NumberFormat = "yyyy/MM/dd";

                // Format số tiền
                oSheet.get_Range("C4", "E1000").NumberFormat = "#,##0.00";

                object[,] arr = new object[tb.Rows.Count, columnNames.Length];
                for (int r = 0; r < tb.Rows.Count; r++)
                {
                    arr[r, 0] = tb.Rows[r]["ID"];
                    arr[r, 1] = ((DateTime)tb.Rows[r]["ThangNam"]).ToString("yyyy/MM/dd");
                    arr[r, 2] = tb.Rows[r]["PhiSuaChua"];
                    arr[r, 3] = tb.Rows[r]["TienDien"];
                    arr[r, 4] = tb.Rows[r]["TienNuoc"];
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
        }


        private void ReadExcel_ChiPhi(string filename)
        {
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

            try
            {
                foreach (xls.Worksheet wsheet in Excel.Worksheets)
                {
                    int totalRows = (int)wsheet.UsedRange.Rows.Count;
                    int i = 2;

                    while (true)
                    {
                        if (wsheet.Cells[i, 1].Value == null && wsheet.Cells[i, 2].Value == null &&
                            wsheet.Cells[i, 3].Value == null && wsheet.Cells[i, 4].Value == null)
                            break;

                        string thangNamStr = wsheet.Cells[i, 1]?.Value?.ToString().Trim();
                        string phiSuaChuaStr = wsheet.Cells[i, 2]?.Value?.ToString().Trim();
                        string tienDienStr = wsheet.Cells[i, 3]?.Value?.ToString().Trim();
                        string tienNuocStr = wsheet.Cells[i, 4]?.Value?.ToString().Trim();

                        DateTime thangNam;
                        decimal phiSuaChua=0, tienDien=0, tienNuoc = 0;

                        bool isRowValid = DateTime.TryParse(thangNamStr, out thangNam) &&
                                          decimal.TryParse(phiSuaChuaStr, out phiSuaChua) &&
                                          decimal.TryParse(tienDienStr, out tienDien) &&
                                          decimal.TryParse(tienNuocStr, out tienNuoc);

                        if (!isRowValid)
                        {
                            hasError = true;
                            errorMessages.AppendLine($"Dòng {i} lỗi dữ liệu không hợp lệ.");
                        }
                        else
                        {
                            ThemChiPhi(thangNam, phiSuaChua, tienDien, tienNuoc);
                        }

                        loadingForm.SetProgress((int)((float)i / totalRows * 100));
                        loadingForm.SetMessage($"Đang xử lý dòng {i}...");
                        i++;
                    }
                }

                if (hasError)
                {
                    string logFilePath = Path.Combine(Application.StartupPath, "Logs", "chiphi_log.txt");
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                    File.AppendAllText(logFilePath, $"Lỗi vào lúc {DateTime.Now}\n");
                    File.AppendAllText(logFilePath, errorMessages.ToString());
                    File.AppendAllText(logFilePath, "=================================\n");

                    MessageBox.Show($"Phát hiện lỗi dữ liệu! Chi tiết: {logFilePath}");
                }
                else
                {
                    MessageBox.Show("Nhập dữ liệu thành công!");
                }
            }
            finally
            {
                loadingForm.Close();
                workbook.Close(false);
                Excel.Quit();
            }
        }



        private void ThemChiPhi(DateTime thangNam, decimal phiSuaChua, decimal tienDien, decimal tienNuoc)
        {
            // Format ngày theo chuẩn SQL
            string sqlInsert = $"INSERT INTO ChiPhi (ThangNam, PhiSuaChua, TienDien, TienNuoc) " +
                   $"VALUES ('{thangNam:dd-MM-yyyy}', {phiSuaChua}, {tienDien}, {tienNuoc})";

            Thuvien.ExecuteQuery(sqlInsert);
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
                ReadExcel_ChiPhi(filePath);
                loadtb();
                TinhTongChiPhi();

            }
        }
    }
}
