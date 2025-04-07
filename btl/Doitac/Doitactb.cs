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
using btl.Account;
using xls = Microsoft.Office.Interop.Excel;

namespace btl.Doitac
{
    public partial class Doitactb : Form
    {
        Doitac Doitac;

        public Doitactb(Doitac parent)
        {
            InitializeComponent();
            this.Doitac = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            loadtb();
        }

        public void loadtb()
        {
            Thuvien.LoadData("select * from DoiTac", dataGridView1);
        }

        public void ExportExcel_DoiTac(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu file Excel";
            sfd.FileName = "DanhSachDoiTac.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            var oBook = oExcel.Workbooks.Add(Type.Missing);
            var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Worksheets[1];
            oExcel.Visible = false;
            oExcel.DisplayAlerts = false;
            oSheet.Name = "DoiTac";

            oSheet.get_Range("A1", "E1").MergeCells = true;
            oSheet.Cells[1, 1] = "DANH SÁCH ĐỐI TÁC";
            oSheet.Cells[1, 1].Font.Size = 18;
            oSheet.Cells[1, 1].Font.Bold = true;
            oSheet.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            string[] headers = { "MÃ ĐỐI TÁC", "TÊN QUẢNG CÁO", "NGÀY BẮT ĐẦU", "NGÀY KẾT THÚC", "CHI PHÍ" };
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
                data[i, 0] = tb.Rows[i]["Madoitac"];
                data[i, 1] = tb.Rows[i]["Tenquangcao"];
                data[i, 2] = Convert.ToDateTime(tb.Rows[i]["Ngaybatdau"]).ToString("dd/MM/yyyy");
                data[i, 3] = Convert.ToDateTime(tb.Rows[i]["Ngayketthuc"]).ToString("dd/MM/yyyy");
                data[i, 4] = tb.Rows[i]["Chiphi"];
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

        private void ReadExcel_DoiTac(string filename)
        {
            if (filename == null)
            {
                MessageBox.Show("Chưa chọn file");
                return;
            }

            xls.Application Excel = new xls.Application();
            xls.Workbook workbook = Excel.Workbooks.Open(filename);
            xls.Worksheet sheet = workbook.Sheets[1];
            int i = 2;

            List<int> errorRows = new List<int>();
            StringBuilder log = new StringBuilder();
            bool hasError = false;

            while (sheet.Cells[i, 1].Value != null)
            {
                string ma = sheet.Cells[i, 1].Value.ToString();
                string checkSql = $"SELECT COUNT(*) FROM DoiTac WHERE Madoitac = '{ma}'";
                if (Thuvien.CheckExist(checkSql))
                {
                    log.AppendLine($"Dòng {i} bị trùng: Mã đối tác đã tồn tại.");
                    hasError = true;
                }
                i++;
            }

            if (hasError)
            {
                string logPath = Path.Combine(Application.StartupPath, "Logs", "doitac_log.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                File.AppendAllText(logPath, $"Log lúc {DateTime.Now}\n" + log.ToString() + "====================\n");
                MessageBox.Show("Có dữ liệu trùng, xem chi tiết tại: " + logPath);
            }
            else
            {
                i = 2;
                while (sheet.Cells[i, 1].Value != null)
                {
                    string ma = sheet.Cells[i, 1].Value.ToString();
                    string tenqc = sheet.Cells[i, 2].Value.ToString();
                    string nbd = DateTime.Parse(sheet.Cells[i, 3].Value.ToString()).ToString("yyyy-MM-dd");
                    string nkt = DateTime.Parse(sheet.Cells[i, 4].Value.ToString()).ToString("yyyy-MM-dd");
                    string chiphi = sheet.Cells[i, 5].Value.ToString();

                    string sql = $"INSERT INTO DoiTac VALUES('{ma}', N'{tenqc}', '{nbd}', '{nkt}', {chiphi})";
                    Thuvien.ExecuteQuery(sql);

                    i++;
                }
                MessageBox.Show("Đã thêm dữ liệu vào bảng Đối Tác thành công!");
            }

            workbook.Close(false);
            Excel.Quit();
        }

        private void ThemMoiDoiTac(string ma, string ten, string nbd, string nkt, int chiphi)
        {
            string sql = $"INSERT INTO DoiTac VALUES('{ma}', N'{ten}', '{nbd}', '{nkt}', {chiphi})";
            Thuvien.ExecuteQuery(sql);
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
                string ma = row.Cells["Madoitac"].Value.ToString();
                string ten = row.Cells["Tenquangcao"].Value.ToString();
                DateTime bd = DateTime.Parse(row.Cells["Ngaybatdau"].Value.ToString());
                DateTime kt = DateTime.Parse(row.Cells["Ngayketthuc"].Value.ToString());
                string cp = row.Cells["Chiphi"].Value.ToString();
                Doitac.doitactv.SetData(ma, ten, bd, kt, cp);
                Doitac.SwitchToTab(1);
            }
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            String sql = "";
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["Madoitac"].Value.ToString();
            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không ?", "Xác nhận: ", MessageBoxButtons.YesNo);
            sql = "delete from DoiTac where Madoitac = '"+ ma +"'";
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
                ReadExcel_DoiTac(filePath);
                loadtb();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sql = "select * from ChiNhanh";
            Thuvien.LoadExcel(sql, dt);
            ExportExcel_DoiTac(dt);
        }
    }
}
