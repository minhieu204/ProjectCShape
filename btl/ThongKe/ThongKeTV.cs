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

namespace btl.ThongKe
{
    public partial class ThongKeTV : Form
    {
        public ThongKe thongKe;


        public ThongKeTV(ThongKe parent)
        {
            InitializeComponent();
            this.thongKe = parent;
            Thuvien.CustomDataGridView(dataGridView1);
        }


        private void guna2Button4_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string ngayBD = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string ngayKT = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            string sql = $@"
        SELECT 
            '{ngayBD}' AS NgayBatDau,
            '{ngayKT}' AS NgayKetThuc,

            -- Tổng lương nhân viên
            ISNULL((SELECT SUM(tongluong) 
                    FROM luong 
                    WHERE ngaynhan BETWEEN '{ngayBD}' AND '{ngayKT}'), 0) AS LuongNhanVien,

            -- Tổng phí quảng cáo
            ISNULL((SELECT SUM(Chiphi) 
                    FROM DoiTac 
                    WHERE Ngaybatdau >= '{ngayBD}' AND Ngayketthuc <= '{ngayKT}'), 0) AS PhiQuangCao,

            -- Chi phí điện, nước, sửa chữa
            ISNULL((SELECT SUM(TienDien + TienNuoc + PhiSuaChua)
                    FROM ChiPhi
                    WHERE FORMAT(ThangNam, 'yyyy-MM') 
                          BETWEEN FORMAT(CONVERT(date, '{ngayBD}'), 'yyyy-MM') 
                          AND FORMAT(CONVERT(date, '{ngayKT}'), 'yyyy-MM')), 0) AS ChiPhi,

            -- Tổng tiền nhập hàng
            ISNULL((SELECT SUM(gianhap * soluong) 
                    FROM sanpham 
                    WHERE ngaynhap BETWEEN '{ngayBD}' AND '{ngayKT}'), 0) AS TienNhapHang,

            -- Tổng tiền bán hàng
            ISNULL((SELECT SUM(tongtien) 
                    FROM donhang 
                    WHERE ngayban BETWEEN '{ngayBD}' AND '{ngayKT}'), 0) AS TienBanHang
    ";

            // Load dữ liệu vào DataGridView
            Thuvien.LoadData(sql, dataGridView1);

            // Đọc từ dòng đầu tiên (chỉ có 1 dòng)
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.Rows[0];

                double tongLuong = Convert.ToDouble(row.Cells["LuongNhanVien"].Value);
                double tongQC = Convert.ToDouble(row.Cells["PhiQuangCao"].Value);
                double tongChiPhi = Convert.ToDouble(row.Cells["ChiPhi"].Value);
                double tongNhap = Convert.ToDouble(row.Cells["TienNhapHang"].Value);
                double tongBan = Convert.ToDouble(row.Cells["TienBanHang"].Value);

                MessageBox.Show($"📊 **Tổng kết từ {ngayBD} đến {ngayKT}**:\n\n" +
                                $"💼 Lương NV      : {tongLuong:n0} ₫\n" +
                                $"📢 Quảng cáo     : {tongQC:n0} ₫\n" +
                                $"🧾 Chi phí khác  : {tongChiPhi:n0} ₫\n" +
                                $"📦 Tiền nhập     : {tongNhap:n0} ₫\n" +
                                $"💰 Tiền bán      : {tongBan:n0} ₫",
                                "📈 Kết quả thống kê",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sql = "select * from thongke";
            Thuvien.LoadExcel(sql, dt);
            ExportExcel_ThongKe(dt);
        }

        public void ExportExcel_ThongKe(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu file Excel";
            sfd.FileName = "DanhSachThongKe.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            var oBook = oExcel.Workbooks.Add(Type.Missing);
            var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Worksheets[1];
            oExcel.Visible = false;
            oExcel.DisplayAlerts = false;
            oSheet.Name = "THONGKE";

            oSheet.get_Range("A1", "H1").MergeCells = true;
            oSheet.Cells[1, 1] = "DANH SÁCH THỐNG KÊ";
            oSheet.Cells[1, 1].Font.Size = 18;
            oSheet.Cells[1, 1].Font.Bold = true;
            oSheet.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            string[] headers = { "ID", "NGÀY BẮT ĐẦU", "NGÀY KẾT THÚC", "LƯƠNG NHÂN VIÊN", "PHÍ QUẢNG CÁO", "CHI PHÍ", "TIỀN NHẬP HÀNG", "TIỀN BÁN HÀNG" };
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
                data[i, 0] = tb.Rows[i]["id"];
                data[i, 1] = Convert.ToDateTime(tb.Rows[i]["NgayBatDau"]).ToString("dd/MM/yyyy");
                data[i, 2] = Convert.ToDateTime(tb.Rows[i]["NgayKetThuc"]).ToString("dd/MM/yyyy");
                data[i, 3] = tb.Rows[i]["LuongNhanVien"];
                data[i, 4] = tb.Rows[i]["PhiQuangCao"];
                data[i, 5] = tb.Rows[i]["ChiPhi"];
                data[i, 6] = tb.Rows[i]["TienNhapHang"];
                data[i, 7] = tb.Rows[i]["TienBanHang"];
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


        private void ReadExcel_ThongKe(string filename)
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
                string id = sheet.Cells[i, 1].Value.ToString();
                string checkSql = $"SELECT COUNT(*) FROM ThongKe WHERE id = '{id}'";
                if (Thuvien.CheckExist(checkSql))
                {
                    log.AppendLine($"Dòng {i} bị trùng: ID đã tồn tại.");
                    hasError = true;
                }
                i++;
            }

            if (hasError)
            {
                string logPath = Path.Combine(Application.StartupPath, "Logs", "thongke_log.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                File.AppendAllText(logPath, $"Log lúc {DateTime.Now}\n" + log.ToString() + "====================\n");
                MessageBox.Show("Có dữ liệu trùng, xem chi tiết tại: " + logPath);
            }
            else
            {
                i = 2;
                while (sheet.Cells[i, 1].Value != null)
                {
                    string id = sheet.Cells[i, 1].Value.ToString();
                    string ngaybd = DateTime.Parse(sheet.Cells[i, 2].Value.ToString()).ToString("yyyy-MM-dd");
                    string ngaykt = DateTime.Parse(sheet.Cells[i, 3].Value.ToString()).ToString("yyyy-MM-dd");
                    string luongnv = sheet.Cells[i, 4].Value.ToString();
                    string phiqc = sheet.Cells[i, 5].Value.ToString();
                    string chiphi = sheet.Cells[i, 6].Value.ToString();
                    string tiennhap = sheet.Cells[i, 7].Value.ToString();
                    string tienban = sheet.Cells[i, 8].Value.ToString();

                    string sql = $"INSERT INTO ThongKe VALUES({id}, '{ngaybd}', '{ngaykt}', {luongnv}, {phiqc}, {chiphi}, {tiennhap}, {tienban})";
                    Thuvien.ExecuteQuery(sql);
                    i++;
                }
                MessageBox.Show("Đã thêm dữ liệu vào bảng Thống Kê thành công!");
            }

            workbook.Close(false);
            Excel.Quit();
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
                ReadExcel_ThongKe(filePath);
                Thuvien.LoadData("SELECT * FROM ThongKe", dataGridView1);
            }
        }
    }
}
