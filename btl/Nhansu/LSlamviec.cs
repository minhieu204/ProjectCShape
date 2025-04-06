using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Nhansu
{
    public partial class LSlamviec : Form
    {
        public LSlamviec()
        {
            InitializeComponent();
            Thuvien.CustomDataGridView(dataGridView1);
            dateTimePicker1.Value = DateTime.Now;
            radiothg.Checked = true;
            Loadtb();
            dateTimePicker1.Enabled = false;
            dataGridView1.Columns["manhanvien"].Visible = false;
            dataGridView1.Columns["mals"].Visible=false;
        }
        int i = 0;
        String ma;
        String ht;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public void Loadtb()
        {
            if (i == 0)
            {
                String sql = "select * from lslamviec where manhanvien='"+ma+"'";
                Thuvien.LoadData(sql, dataGridView1);
            }
            else
            {
                int month = dateTimePicker1.Value.Month;
                int year = dateTimePicker1.Value.Year;
                String sql = "select * from lslamviec where MONTH(ngay) = " + month + " and YEAR(ngay) = " + year + " and manhanvien='"+ma+"'";
                Thuvien.LoadData(sql, dataGridView1);

            }
        }
        public void ExportExcel_LSLamViec(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu file Excel";
            if (i == 0)
            {
                sfd.FileName = "LichSuLamViec"+ma+"All.xlsx";
            }
            else
            {
                sfd.FileName = "LichSuLamViec"+ma+"Thang" + dateTimePicker1.Value.Month + "_" + dateTimePicker1.Value.Year + ".xlsx";
            }

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            var oBooks = oExcel.Workbooks;
            var oBook = oBooks.Add(Type.Missing);
            var oSheets = oBook.Worksheets;
            var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oExcel.Visible = false;
            oExcel.DisplayAlerts = false;
            oSheet.Name = "LSLamViec";

            var head = oSheet.get_Range("A1", "G1");
            head.MergeCells = true;
            if (i == 0)
            {
                head.Value2 = "LỊCH SỬ LÀM VIỆC CỦA "+ht.ToUpper();
            }
            else
            {
                head.Value2 = "LỊCH SỬ LÀM VIỆC CỦA "+ht.ToUpper()+" THÁNG " + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Year;
            }
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = 18;
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            string[] columnNames = { "MÃ LS", "MÃ NHÂN VIÊN", "GIỜ VÀO", "GIỜ RA", "GIỜ LÀM", "NGÀY", "TRẠNG THÁI" };
            string[] columnLetters = { "A", "B", "C", "D", "E", "F", "G" };
            double[] columnWidths = { 10, 15, 22, 22, 12, 15, 15 };

            for (int i = 0; i < columnNames.Length; i++)
            {
                var col = oSheet.get_Range(columnLetters[i] + "3");
                col.Value2 = columnNames[i];
                col.ColumnWidth = columnWidths[i];
                col.Font.Bold = true;
                col.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                col.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            }

            object[,] arr = new object[tb.Rows.Count, columnNames.Length];
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                arr[r, 0] = tb.Rows[r]["mals"];
                arr[r, 1] = tb.Rows[r]["manhanvien"];
                arr[r, 2] = ((DateTime)tb.Rows[r]["logintime"]).ToString("dd/MM/yyyy HH:mm");
                arr[r, 3] = tb.Rows[r]["logouttime"] == DBNull.Value ? "" : ((DateTime)tb.Rows[r]["logouttime"]).ToString("dd/MM/yyyy HH:mm");
                arr[r, 4] = tb.Rows[r]["giolamviec"];
                arr[r, 5] = ((DateTime)tb.Rows[r]["ngay"]).ToString("dd/MM/yyyy");
                arr[r, 6] = tb.Rows[r]["workstatus"];
            }

            int rowStart = 4;
            int rowEnd = rowStart + tb.Rows.Count - 1;

            var c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, 1];
            var c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnNames.Length];
            var range = oSheet.get_Range(c1, c2);
            range.Value2 = arr;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Format giolamviec column (E)
            oSheet.get_Range("E4", $"E{rowEnd}").NumberFormat = "#,##0.00";

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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
            }
        }

        public void SetData(String ht, String ma)
        {
            this.ht = ht;
            label2.Text = "Lịch sử phiên làm việc của: "+ht;
            this.ma = ma;
            Loadtb();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (i == 0)
            {
                String sql = "select * from lslamviec where manhanvien='"+ma+"'";

                Thuvien.LoadExcel(sql, dt);
                ExportExcel_LSLamViec(dt);
            }
            else
            {
                int month = dateTimePicker1.Value.Month;
                int year = dateTimePicker1.Value.Year;
                String sql = "select * from lslamviec where MONTH(ngay) = " + month + " and YEAR(ngay) = " + year + " and manhanvien='"+ma+"'";
                Thuvien.LoadExcel(sql, dt);
                ExportExcel_LSLamViec(dt);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radiothg.Checked)
            {
                i = 0;
                dateTimePicker1.Enabled = false;
                Loadtb();
            }
            else
            {
                i = 1;
                dateTimePicker1.Enabled = true;
                Loadtb();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Loadtb();
        }
    }
}
