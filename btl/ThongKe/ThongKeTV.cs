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
                SqlCommand cmdChiPhi = new SqlCommand("SELECT ISNULL(SUM(PhiSuaChua + TienDien + TienNuoc), 0) FROM ChiPhi WHERE CONVERT(date, ThangNam) = @ngay", conn);
                cmdChiPhi.Parameters.AddWithValue("@ngay", day.Date);
                chiphi = Convert.ToInt32(cmdChiPhi.ExecuteScalar());

                // Tính tiền bán hàng
                SqlCommand cmdBan = new SqlCommand("SELECT ISNULL(SUM(tongtien), 0) FROM donhang WHERE ngayban = @ngay", conn);
                cmdBan.Parameters.AddWithValue("@ngay", day);
                ban = Convert.ToInt32(cmdBan.ExecuteScalar());

                // Tiền nhập hàng (tạm thời là 0 nếu chưa có bảng riêng)
                nhap = 0;

                // Kiểm tra nếu có ít nhất một loại chi phí khác 0 mới thêm
                if (luong != 0 || quangcao != 0 || chiphi != 0 || nhap != 0 || ban != 0)
                {
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

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook|*.xlsx";
                sfd.Title = "Lưu file Excel";
                sfd.FileName = "ThongKe.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                var oExcel = new Microsoft.Office.Interop.Excel.Application();
                var oBook = oExcel.Workbooks.Add(Type.Missing);
                var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Sheets[1];

                oExcel.Visible = false;
                oExcel.DisplayAlerts = false;
                oSheet.Name = "ThongKe";

                var head = oSheet.get_Range("A1", "I1");
                head.MergeCells = true;
                head.Value2 = "BÁO CÁO THỐNG KÊ";
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = 18;
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                string[] columnNames = { "ID", "NGÀY BẮT ĐẦU", "NGÀY KẾT THÚC", "NGÀY", "LƯƠNG NHÂN VIÊN", "PHÍ QUẢNG CÁO", "CHI PHÍ", "TIỀN NHẬP HÀNG", "TIỀN BÁN HÀNG" };
                string[] columnLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
                double[] columnWidths = { 10, 20, 20, 20, 20, 20, 20, 20, 20 };

                for (int i = 0; i < columnNames.Length; i++)
                {
                    var col = oSheet.get_Range(columnLetters[i] + "3");
                    col.Value2 = columnNames[i];
                    col.ColumnWidth = columnWidths[i];
                    col.Font.Bold = true;
                    col.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                    col.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                }

                // Format ngày
                oSheet.get_Range("B4", "D1000").NumberFormat = "dd/MM/yyyy";

                // Format số tiền
                oSheet.get_Range("E4", "I1000").NumberFormat = "#,##0";

                object[,] arr = new object[tb.Rows.Count, columnNames.Length];
                for (int r = 0; r < tb.Rows.Count; r++)
                {
                    arr[r, 0] = tb.Rows[r]["id"];
                    arr[r, 1] = tb.Rows[r]["NgayBatDau"] == DBNull.Value ? "" : ((DateTime)tb.Rows[r]["NgayBatDau"]).ToString("dd/MM/yyyy");
                    arr[r, 2] = tb.Rows[r]["NgayKetThuc"] == DBNull.Value ? "" : ((DateTime)tb.Rows[r]["NgayKetThuc"]).ToString("dd/MM/yyyy");
                    arr[r, 3] = tb.Rows[r]["Ngay"] == DBNull.Value ? "" : ((DateTime)tb.Rows[r]["Ngay"]).ToString("dd/MM/yyyy");
                    arr[r, 4] = tb.Rows[r]["LuongNhanVien"];
                    arr[r, 5] = tb.Rows[r]["PhiQuangCao"];
                    arr[r, 6] = tb.Rows[r]["ChiPhi"];
                    arr[r, 7] = tb.Rows[r]["TienNhapHang"];
                    arr[r, 8] = tb.Rows[r]["TienBanHang"];
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
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }



        
        
        private void lbtongChiPhi_Click(object sender, EventArgs e)
        {

        }

        private void btnload_Click(object sender, EventArgs e)
        {
            // Xác nhận xóa dữ liệu
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả dữ liệu thống kê?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Xóa toàn bộ dữ liệu trong bảng ThongKe
                    using (SqlConnection conn = Thuvien.GetConnection())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM ThongKe", conn);
                        cmd.ExecuteNonQuery();
                    }

                    // Làm mới DataGridView
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();

                    // Xóa các label tổng
                    lbtongLuong.Text = "0 VNĐ";
                    lbtongQuangCao.Text = "0 VNĐ";
                    lbtongChiPhi.Text = "0 VNĐ";
                    lbtongNhapHang.Text = "0 VNĐ";
                    lbtongBanHang.Text = "0 VNĐ";

                    MessageBox.Show("Đã xóa toàn bộ dữ liệu và làm mới bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
