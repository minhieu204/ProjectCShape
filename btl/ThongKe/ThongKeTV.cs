using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        private void TinhTongVaHienThi()
        {
            decimal tongLuong = 0;
            decimal tongQuangCao = 0;
            decimal tongChiPhi = 0;
            decimal tongNhapHang = 0;
            decimal tongBanHang = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["LuongNhanVien"].Value != null)
                    tongLuong += Convert.ToDecimal(row.Cells["LuongNhanVien"].Value);

                if (row.Cells["PhiQuangCao"].Value != null)
                    tongQuangCao += Convert.ToDecimal(row.Cells["PhiQuangCao"].Value);

                if (row.Cells["ChiPhi"].Value != null)
                    tongChiPhi += Convert.ToDecimal(row.Cells["ChiPhi"].Value);

                if (row.Cells["TienNhapHang"].Value != null)
                    tongNhapHang += Convert.ToDecimal(row.Cells["TienNhapHang"].Value);

                if (row.Cells["TienBanHang"].Value != null)
                    tongBanHang += Convert.ToDecimal(row.Cells["TienBanHang"].Value);
            }

            // Gán vào label (format tiền tệ)
            lbtongLuong.Text = tongLuong.ToString("N0") + " VNĐ";
            lbtongQuangCao.Text = tongQuangCao.ToString("N0") + " VNĐ";
            lbtongChiPhi.Text = tongChiPhi.ToString("N0") + " VNĐ";
            lbtongNhapHang.Text = tongNhapHang.ToString("N0") + " VNĐ";
            lbtongBanHang.Text = tongBanHang.ToString("N0") + " VNĐ";
        }



        private void guna2Button4_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = dateTimePicker1.Value.Date;
            DateTime ngayKetThuc = dateTimePicker2.Value.Date;

            SqlConnection conn = Thuvien.GetConnection();

            // Xóa dữ liệu cũ trong khoảng ngày đã chọn (nếu cần)
            SqlCommand deleteCmd = new SqlCommand("DELETE FROM thongke WHERE Ngay BETWEEN @start AND @end", conn);
            deleteCmd.Parameters.AddWithValue("@start", ngayBatDau);
            deleteCmd.Parameters.AddWithValue("@end", ngayKetThuc);
            deleteCmd.ExecuteNonQuery();

            // Tạo DataTable để hiển thị
            DataTable dtNgay = new DataTable();
            dtNgay.Columns.Add("Ngay", typeof(DateTime));
            dtNgay.Columns.Add("LuongNhanVien", typeof(double));
            dtNgay.Columns.Add("PhiQuangCao", typeof(int));
            dtNgay.Columns.Add("ChiPhi", typeof(int));
            dtNgay.Columns.Add("TienNhapHang", typeof(int));
            dtNgay.Columns.Add("TienBanHang", typeof(int));

            for (DateTime day = ngayBatDau; day <= ngayKetThuc; day = day.AddDays(1))
            {
                double luong = 0;
                int quangcao = 0;
                int chiphi = 0;
                int nhap = 0;
                int ban = 0;

                // Tính lương
                SqlCommand cmdLuong = new SqlCommand("SELECT ISNULL(SUM(tienduocnhan), 0) FROM luong WHERE ngaynhan = @ngay", conn);
                cmdLuong.Parameters.AddWithValue("@ngay", day);
                luong = Convert.ToDouble(cmdLuong.ExecuteScalar());

                // Tính phí quảng cáo còn hiệu lực trong ngày
                SqlCommand cmdQC = new SqlCommand("SELECT ISNULL(SUM(chiphi), 0) FROM DoiTac WHERE @ngay BETWEEN ngaybatdau AND ngayketthuc", conn);
                cmdQC.Parameters.AddWithValue("@ngay", day);
                quangcao = Convert.ToInt32(cmdQC.ExecuteScalar());

                // Tính chi phí trong tháng
                SqlCommand cmdChiPhi = new SqlCommand("SELECT ISNULL(SUM(PhiSuaChua + TienDien + TienNuoc), 0) FROM ChiPhi WHERE MONTH(ThangNam) = @thang AND YEAR(ThangNam) = @nam", conn);
                cmdChiPhi.Parameters.AddWithValue("@thang", day.Month);
                cmdChiPhi.Parameters.AddWithValue("@nam", day.Year);
                chiphi = Convert.ToInt32(cmdChiPhi.ExecuteScalar());

                // Tính tiền bán hàng
                SqlCommand cmdBan = new SqlCommand("SELECT ISNULL(SUM(tongtien), 0) FROM donhang WHERE ngayban = @ngay", conn);
                cmdBan.Parameters.AddWithValue("@ngay", day);
                ban = Convert.ToInt32(cmdBan.ExecuteScalar());

                // Tiền nhập hàng (tạm thời là 0 nếu chưa có bảng riêng)
                nhap = 0;

                // Chèn vào bảng thongke theo từng ngày
                SqlCommand insertCmd = new SqlCommand("INSERT INTO thongke (Ngay, LuongNhanVien, PhiQuangCao, ChiPhi, TienNhapHang, TienBanHang) " +
                                                       "VALUES (@Ngay, @Luong, @QC, @Chi, @Nhap, @Ban)", conn);
                insertCmd.Parameters.AddWithValue("@Ngay", day);
                insertCmd.Parameters.AddWithValue("@Luong", luong);
                insertCmd.Parameters.AddWithValue("@QC", quangcao);
                insertCmd.Parameters.AddWithValue("@Chi", chiphi);
                insertCmd.Parameters.AddWithValue("@Nhap", nhap);
                insertCmd.Parameters.AddWithValue("@Ban", ban);
                insertCmd.ExecuteNonQuery();

                // Thêm vào bảng hiển thị
                dtNgay.Rows.Add(day, luong, quangcao, chiphi, nhap, ban);
            }

            conn.Close();

            // Hiển thị trên datagridview
            dataGridView1.DataSource = dtNgay;

            // Gọi hàm tính tổng nếu có
            TinhTongVaHienThi();
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
                TinhTongVaHienThi();
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
        
        private void lbtongChiPhi_Click(object sender, EventArgs e)
        {

        }
    }
}
