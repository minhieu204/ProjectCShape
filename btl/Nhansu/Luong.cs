using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using btl.Account;

namespace btl.Nhansu
{
    public partial class Luong : Form
    {
        public Nhansu nhanSu;
        public Luong(Nhansu parent)
        {
            this.nhanSu = parent;
            InitializeComponent();
            Loadtb();
            Thuvien.CustomDataGridView(dataGridView1);
            dateTimePicker1.Value = DateTime.Now;
            ratiothg.Checked = true;
            dateTimePicker1.Enabled = false;

        }
        int i = 0;
        public void Loadtb()
        {
            if (i == 0)
            {
                String sql = "select * from luong";
                Thuvien.LoadData(sql, dataGridView1);
            }
            else
            {
                int month = dateTimePicker1.Value.Month;
                int year = dateTimePicker1.Value.Year;
                String sql = "select * from luong where MONTH(ngaynhan) = " + month + " and YEAR(ngaynhan) = " + year + "";
                Thuvien.LoadData(sql, dataGridView1);

            }
        }
        public void ExportExcel_Luong(DataTable tb)
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
                sfd.FileName = "BangLuongAll.xlsx";
            }
            else
            {
                sfd.FileName = "BangLuongThang" + dateTimePicker1.Value.Month + "_" + dateTimePicker1.Value.Year + ".xlsx";
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
            oSheet.Name = "Luong";

            var head = oSheet.get_Range("A1", "H1");
            head.MergeCells = true;
            if (i==0) 
            {
                head.Value2 = "BẢNG LƯƠNG NHÂN VIÊN"; 
            }
            else
            {
                head.Value2 = "BẢNG LƯƠNG NHÂN VIÊN THÁNG " + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Year;
            }

            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = 18;
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            string[] columnNames = { "MÃ LƯƠNG", "MÃ NV", "TỔNG GIỜ", "LƯƠNG/GIỜ", "TỔNG LƯƠNG", "NGÀY NHẬN", "THƯỞNG", "TIỀN NHẬN" };
            string[] columnLetters = { "A", "B", "C", "D", "E", "F", "G", "H" };
            double[] columnWidths = { 12, 15, 12, 15, 18, 15, 15, 18 };

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
                arr[r, 0] = tb.Rows[r]["maluong"];
                arr[r, 1] = tb.Rows[r]["manhanvien"];
                arr[r, 2] = tb.Rows[r]["tonggio"];
                arr[r, 3] = tb.Rows[r]["luongtheogio"];
                arr[r, 4] = tb.Rows[r]["tongluong"];
                arr[r, 5] = ((DateTime)tb.Rows[r]["ngaynhan"]).ToString("dd/MM/yyyy");
                arr[r, 6] = tb.Rows[r]["thuong"];
                arr[r, 7] = tb.Rows[r]["tienduocnhan"];
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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (i == 0)
            {
                String sql = "select * from luong";
                Thuvien.LoadExcel(sql, dt);
                ExportExcel_Luong(dt);
            }
            else
            {
                int month = dateTimePicker1.Value.Month;
                int year = dateTimePicker1.Value.Year;
                String sql = "select * from luong where MONTH(ngaynhan) = " + month + " and YEAR(ngaynhan) = " + year + "";
                Thuvien.LoadExcel(sql, dt);
                ExportExcel_Luong(dt);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Loadtb();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (ratiothg.Checked)
            {
                i = 0;
                dateTimePicker1.Enabled = false;
                textBox1.Text = "";
                Loadtb();
            }
            else
            {
                i = 1;
                dateTimePicker1.Enabled = true;
                textBox1.Text = "";
                Loadtb();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (i == 0)
            {
                String sql = "select * from luong where manhanvien like '%" + textBox1.Text + "%'";
                Thuvien.LoadData(sql, dataGridView1);
            }
            else
            {
                int month = dateTimePicker1.Value.Month;
                int year = dateTimePicker1.Value.Year;
                String sql = "select * from luong where manhanvien like '%" + textBox1.Text + "%' and MONTH(ngaynhan) = " + month + " and YEAR(ngaynhan) = " + year + "";
                Thuvien.LoadData(sql, dataGridView1);
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string ma = row.Cells["id"].Value.ToString();
                string manv = row.Cells["manv"].Value.ToString();
                string lcb = row.Cells["luongh"].Value.ToString();
                string thuong = row.Cells["thuong"].Value.ToString();
                Sualuong sl = new Sualuong(this);
                sl.setData(ma, manv, lcb, thuong);
                sl.ShowDialog();
            }
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            String sql = "";
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["id"].Value.ToString();
           
            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không ?", "Xác nhận: ", MessageBoxButtons.YesNo);
            if (mess_delete == DialogResult.Yes)
            {                
                    sql = "delete from luong where maluong = '" + ma + "'";
            }
            Thuvien.ExecuteQuery(sql);
            Loadtb();
            MessageBox.Show("Xóa bảng lương thành công!", "Thông báo!");
        }
    }
}
