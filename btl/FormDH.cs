using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl
{
    public partial class FormDH : Form
    {
        public FormDH()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Thuvien.CustomDataGridView(dgvCTDH);
            Thuvien.CustomDataGridView(dgvDH);
        }

        public void ExportExcel(DataTable tb, string sheetname)
        {
            // Tạo các đối tượng Excel
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            // Tạo mới một Excel WorkBook 
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetname;

            // Tạo phần đầu nếu muốn
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "D1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH ĐƠN HÀNG";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "MÃ ĐƠN HÀNG";
            cl1.ColumnWidth = 15.0;
            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "NGÀY BÁN";
            cl2.ColumnWidth = 20.0;
            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "TỔNG TIỀN";
            cl3.ColumnWidth = 20.0;
            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "NGƯỜI BÁN";
            cl4.ColumnWidth = 30.0;

            // Kẻ viền cho header
            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "D3");
            rowHead.Font.Bold = true;
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo mảng đối tượng để lưu dữ liệu từ DataTable vào
            object[,] arr = new object[tb.Rows.Count, tb.Columns.Count];

            // Chuyển dữ liệu từ DataTable vào mảng arr
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                for (int j = 0; j < tb.Columns.Count; j++)
                {
                    arr[i, j] = tb.Rows[i][j];
                }
            }

            // Xác định vùng để điền dữ liệu vào Excel
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + tb.Rows.Count - 1;
            int columnEnd = tb.Columns.Count;

            // Ô bắt đầu điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            // Ô kết thúc điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            // Lấy về vùng điền dữ liệu
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);
            // Điền dữ liệu vào vùng đã thiết lập
            range.Value2 = arr;

            // Kẻ viền cho dữ liệu
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Định dạng ngày tháng cho cột Ngày nhập
            Microsoft.Office.Interop.Excel.Range cl_ngs = oSheet.get_Range("B" + rowStart, "B" + rowEnd);
            cl_ngs.Columns.NumberFormat = "dd/mm/yyyy";
        }

        private void resetText()
        {
            txtMadh.Text = "";
            txtNgaynhap.Text = "";
            txtTongtien.Text = "";
            txtNguoiban.Text = "";
        }

        private void loadCTDH()
        {
            String sql = "select madon, tensp, chitietdonhang.soluong, chitietdonhang.giaban, thanhtien " +
                             "from chitietdonhang, sanpham " +
                             "where chitietdonhang.masp = sanpham.masp and madon='" + txtMadh.Text.Trim() + "'";
            Thuvien.LoadData(sql, dgvCTDH);
        }

        private void loadDH()
        {
            String sql = "select madon, ngayban, tongtien, quanly.hoten " +
                         "from donhang, quanly " +
                         "where donhang.manhanvien = quanly.maquanly " +
                         "union all " +
                         "select madon, ngayban, tongtien, nhanvien.hoten " +
                         "from donhang, nhanvien " +
                         "where donhang.manhanvien = nhanvien.manhanvien ";
            Thuvien.LoadData(sql, dgvDH);
        }

        private void FormDH_Load(object sender, EventArgs e)
        {
            txtMadh.Enabled = false;
            txtNgaynhap.Enabled = false;
            txtTongtien.Enabled = false;
            txtNguoiban.Enabled = false;
            loadDH();
            Thuvien.CustomDisabledButton(btnXoa);
        }

        private void FormDH_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }

        private void dgvDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMadh.Text = dgvDH.Rows[i].Cells[0].Value.ToString();
                txtNgaynhap.Text = dgvDH.Rows[i].Cells[1].Value.ToString();
                txtTongtien.Text = dgvDH.Rows[i].Cells[2].Value.ToString();
                txtNguoiban.Text = dgvDH.Rows[i].Cells[3].Value.ToString();
                loadCTDH();
                Thuvien.CustomEnabledButton(btnXoa);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String madon = txtMadh.Text.Trim();
            String sql = "delete from chitietdonhang where madon='" + madon + "'";
            Thuvien.ExecuteQuery(sql);
            sql = "delete from donhang where madon='" + madon + "'";
            Thuvien.ExecuteQuery(sql);
            loadDH();
            resetText();
            Thuvien.CustomDisabledButton(btnXoa);
            loadCTDH();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String search = txtSearch.Text;
            String sql = "select madon, ngayban, tongtien, quanly.hoten " +
                         "from donhang, quanly " +
                         "where donhang.manhanvien = quanly.maquanly and madon like '%" + search + "%' " +
                         "union all " +
                         "select madon, ngayban, tongtien, nhanvien.hoten " +
                         "from donhang, nhanvien " +
                         "where donhang.manhanvien = nhanvien.manhanvien and madon like '%" + search + "%'";
            Thuvien.LoadData(sql, dgvDH);

        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            String search = txtSearch.Text;
            String sql = "select madon, ngayban, tongtien, quanly.hoten " +
                         "from donhang, quanly " +
                         "where donhang.manhanvien = quanly.maquanly and madon like '%" + search + "%' " +
                         "union all " +
                         "select madon, ngayban, tongtien, nhanvien.hoten " +
                         "from donhang, nhanvien " +
                         "where donhang.manhanvien = nhanvien.manhanvien and madon like '%" + search + "%'";
            DataTable dt = new DataTable();
            Thuvien.LoadExcel(sql, dt);
            if (dt.Rows.Count > 0)
            {
                ExportExcel(dt, "Danh sách đơn hàng");
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
