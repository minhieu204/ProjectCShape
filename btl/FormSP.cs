using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl
{
    public partial class FormSP : Form
    {

        public FormSP()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Thuvien.CustomDataGridView(dataGridView1);
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
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "I1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH SẢN PHẨM";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "MÃ SẢN PHẨM";
            cl1.ColumnWidth = 15.0;
            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "TÊN SẢN PHẨM";
            cl2.ColumnWidth = 30.0;
            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "NHÀ CUNG CẤP";
            cl3.ColumnWidth = 20.0;
            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "GIÁ NHẬP";
            cl4.ColumnWidth = 15.0;
            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "GIÁ BÁN";
            cl5.ColumnWidth = 15.0;
            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
            cl6.Value2 = "SỐ LƯỢNG";
            cl6.ColumnWidth = 10.0;
            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "NGÀY NHẬP";
            cl7.ColumnWidth = 20.0;
            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
            cl8.Value2 = "ĐƠN VỊ TÍNH";
            cl8.ColumnWidth = 15.0;
            Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
            cl9.Value2 = "NGƯỜI NHẬP";
            cl9.ColumnWidth = 30.0;

            // Kẻ viền cho header
            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "I3");
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
            Microsoft.Office.Interop.Excel.Range cl_ngs = oSheet.get_Range("G" + rowStart, "G" + rowEnd);
            cl_ngs.Columns.NumberFormat = "dd/mm/yyyy";
        }


        private void resetText()
        {
            txtMasp.Text = "";
            txtTensp.Text = "";
            cbbNCC.SelectedIndex = 0;
            txtGianhap.Text = "";
            txtGiaban.Text = "";
            txtSoluong.Text = "";
            txtNgaynhap.Value = DateTime.Now;
            txtDVT.Text = "";
            txtNguoinhap.Text = Datauser.HoTen;
        }

        private void setDisable()
        {
            txtMasp.Enabled = false;
            txtTensp.Enabled = false;
            cbbNCC.Enabled = false;
            txtGianhap.Enabled = false;
            txtGiaban.Enabled = false;
            txtSoluong.Enabled = false;
            txtNgaynhap.Enabled = false;
            txtDVT.Enabled = false;
            txtNguoinhap.Enabled = false;
        }

        private void setEnable()
        {
            txtMasp.Enabled = true;
            txtTensp.Enabled = true;
            cbbNCC.Enabled = true;
            txtGianhap.Enabled = true;
            txtGiaban.Enabled = true;
            txtSoluong.Enabled = true;
            txtNgaynhap.Enabled = true;
            txtDVT.Enabled = true;
        }

        private void loadSP()
        {
            String sql = "select masp, tensp, tenncc, gianhap, giaban, soluong, ngaynhap, donvitinh, hoten " +
                         "from sanpham, nhacungcap, quanly " +
                         "where sanpham.mancc = nhacungcap.mancc and sanpham.maquanly = quanly.maquanly";
            Thuvien.LoadData(sql, dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void loadCbbNCC()
        {
            String sql = "select * from nhacungcap";
            Thuvien.LoadComboBox(sql, cbbNCC, "mancc", "tenncc");
        }

        private void FormSP_Load(object sender, EventArgs e)
        {
            loadSP();
            loadCbbNCC();
            txtNguoinhap.Text = Datauser.HoTen;
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
            Thuvien.CustomDisabledButton(btnLuu);
        }

        private void FormSP_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMasp.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txtTensp.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                cbbNCC.SelectedValue = Thuvien.GetValueFromCode("select * from nhacungcap", dataGridView1.Rows[i].Cells[2].Value.ToString(), "mancc", "tenncc");
                txtGianhap.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                txtGiaban.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                txtSoluong.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                txtNgaynhap.Value = DateTime.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                txtDVT.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                txtNguoinhap.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
                Thuvien.CustomEnabledButton(btnXoa);
                Thuvien.CustomEnabledButton(btnSua);
                Thuvien.CustomDisabledButton(btnLuu);
                setEnable();
                txtMasp.Enabled = false;
            }
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            resetText();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
            Thuvien.CustomEnabledButton(btnLuu);
            setEnable();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            String masp = txtMasp.Text.Trim();
            String tensp = txtTensp.Text.Trim();
            String mancc = cbbNCC.SelectedValue.ToString();
            String gianhap = txtGianhap.Text.Trim();
            String giaban = txtGiaban.Text.Trim();
            String soluong = txtSoluong.Text.Trim();
            String ngaynhap = txtNgaynhap.Value.ToString();
            String donvitinh = txtDVT.Text.Trim();
            String maquanly = Datauser.ID;
            if (String.IsNullOrEmpty(masp) || String.IsNullOrEmpty(tensp) || String.IsNullOrEmpty(mancc) || String.IsNullOrEmpty(gianhap) || String.IsNullOrEmpty(giaban) || String.IsNullOrEmpty(soluong)
                || String.IsNullOrEmpty(donvitinh) || cbbNCC.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Thuvien.CheckExist("select count(*) from sanpham where masp = '" + masp + "'"))
            {
                MessageBox.Show("Mã sản phẩm không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql = "insert into sanpham values('" + masp + "', N'" + tensp + "', '" + mancc + "', '" + gianhap + "', '" + giaban + "', '" + soluong + "', '" + ngaynhap + "', N'" + donvitinh + "', '" + maquanly + "')";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Lưu sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadSP();
            resetText();
            setDisable();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String masp = txtMasp.Text.Trim();
            String tensp = txtTensp.Text.Trim();
            String mancc = cbbNCC.SelectedValue.ToString();
            String gianhap = txtGianhap.Text.Trim();
            String giaban = txtGiaban.Text.Trim();
            String soluong = txtSoluong.Text.Trim();
            String ngaynhap = txtNgaynhap.Value.ToString();
            String donvitinh = txtDVT.Text.Trim();
            if (String.IsNullOrEmpty(masp) || String.IsNullOrEmpty(tensp) || String.IsNullOrEmpty(mancc) || String.IsNullOrEmpty(gianhap) || String.IsNullOrEmpty(giaban) || String.IsNullOrEmpty(soluong)
                || String.IsNullOrEmpty(donvitinh) || cbbNCC.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql = "update sanpham set tensp = N'" + tensp + "', mancc = '" + mancc + "', gianhap = '" + gianhap + "', giaban = '" + giaban + "', soluong = '" + soluong + "', ngaynhap = '" + ngaynhap + "', donvitinh = N'" + donvitinh + "' where masp = '" + masp + "'";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Sửa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadSP();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String masp = txtMasp.Text.Trim();
            String sql = "delete from sanpham where masp = '" + masp + "'";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Xóa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadSP();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String search = txtSearch.Text;
            String sql = "select masp, tensp, tenncc, gianhap, giaban, soluong, ngaynhap, donvitinh, hoten " +
                         "from sanpham, nhacungcap, quanly " +
                         "where sanpham.mancc = nhacungcap.mancc and sanpham.maquanly = quanly.maquanly "+
                         "and tensp like N'%"+ search +"%'";
            Thuvien.LoadData(sql, dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            String search = txtSearch.Text;
            String sql = "select masp, tensp, tenncc, gianhap, giaban, soluong, ngaynhap, donvitinh, hoten " +
                         "from sanpham, nhacungcap, quanly " +
                         "where sanpham.mancc = nhacungcap.mancc and sanpham.maquanly = quanly.maquanly " +
                         "and tensp like N'%" + search + "%'";
            DataTable dt = new DataTable();
            Thuvien.LoadExcel(sql, dt);
            if (dt.Rows.Count > 0)
            {
                ExportExcel(dt, "Danh sách sản phẩm");
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtGianhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGiaban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
