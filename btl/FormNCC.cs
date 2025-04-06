using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace btl
{
    public partial class FormNCC : Form
    {
        public FormNCC()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Thuvien.CustomDataGridView(dataGridView1);
        }

        private void resetText()
        {
            txtMancc.Text = "";
            txtTenncc.Text = "";
            txtDiachi.Text = "";
            txtSdt.Text = "";
            txtEmail.Text = "";
            txtSdt.Text = "";
        }

        private void setDisable()
        {
            txtMancc.Enabled = false;
            txtTenncc.Enabled = false;
            txtDiachi.Enabled = false;
            txtSdt.Enabled = false;
            txtEmail.Enabled = false;
        }

        private void setEnable()
        {
            txtMancc.Enabled = true;
            txtTenncc.Enabled = true;
            txtDiachi.Enabled = true;
            txtSdt.Enabled = true;
            txtEmail.Enabled = true;
        }

        private void loadNCC()
        {
            String sql = "select * from NhaCungCap";
            Thuvien.LoadData(sql, dataGridView1);
        }

        private void FormNCC_Load(object sender, EventArgs e)
        {
            loadNCC();
            setDisable();
            Thuvien.CustomDisabledButton(btnLuu);
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void FormNCC_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            setEnable();
            resetText();
            Thuvien.CustomEnabledButton(btnLuu);
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            String mancc = txtMancc.Text.Trim();
            String tenncc = txtTenncc.Text.Trim();
            String diachi = txtDiachi.Text.Trim();
            String email = txtEmail.Text.Trim();
            String sdt = txtSdt.Text.Trim();
            if (String.IsNullOrEmpty(mancc) || String.IsNullOrEmpty(tenncc) || String.IsNullOrEmpty(diachi) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Thuvien.CheckExist("select count(*) from nhacungcap where mancc = '" + mancc + "'"))
            {
                MessageBox.Show("Mã nhà cung cấp đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql = "insert into nhacungcap values('" + mancc + "', N'" + tenncc + "', N'" + diachi + "', '" + email + "', '" + sdt + "')";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Lưu nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadNCC();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnLuu);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMancc.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txtTenncc.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txtDiachi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                txtSdt.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                Thuvien.CustomEnabledButton(btnXoa);
                Thuvien.CustomEnabledButton(btnSua);
                Thuvien.CustomDisabledButton(btnLuu);
                setEnable();
                txtMancc.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String mancc = txtMancc.Text.Trim();
            String tenncc = txtTenncc.Text.Trim();
            String diachi = txtDiachi.Text.Trim();
            String email = txtEmail.Text.Trim();
            String sdt = txtSdt.Text.Trim();
            if (String.IsNullOrEmpty(tenncc) || String.IsNullOrEmpty(diachi) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql = "update nhacungcap set tenncc = N'" + tenncc + "', diachi = N'" + diachi + "', email = '" + email + "', sdt = '" + sdt + "' where mancc = '" + mancc + "'";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Sửa nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadNCC();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String mancc = txtMancc.Text.Trim();
            String sql = "delete from nhacungcap where mancc = '" + mancc + "'";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Xóa nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadNCC();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String search = txtSearch.Text;
            String sql = "select * from nhacungcap where tenncc like N'%" + search + "%'";
            Thuvien.LoadData(sql, dataGridView1);
        }

        private void ThemmoiNCC(String mancc, String tenncc, String diachi, String email, String sdt)
        {
            // Kiểm tra mã nhà cung cấp đã tồn tại chưa
            String sqlcheck = "select count(*) from nhacungcap where mancc = '" + mancc + "'";
            if (Thuvien.CheckExist(sqlcheck))
            {
                return; // Nếu đã tồn tại, dừng lại không thực hiện thêm mới
            }
            String sql = "insert into nhacungcap values('" + mancc + "', N'" + tenncc + "', N'" + diachi + "', '" + email + "', '" + sdt + "')";
            Thuvien.ExecuteQuery(sql);
        }

        private void ReadExcel(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Chưa chọn file");
                return;
            }

            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = Excel.Workbooks.Open(filename);

            List<string> duplicateCodes = new List<string>();

            try
            {
                foreach (Microsoft.Office.Interop.Excel.Worksheet wsheet in workbook.Worksheets)
                {
                    int i = 2;  // Bắt đầu đọc từ dòng 2 (bỏ dòng tiêu đề)
                    while (!IsRowEmpty(wsheet, i))
                    {
                        
                        // Xử lý số điện thoại (cột 5 - có thể mất số 0 đầu tiên)
                        string sdt = GetCellString(wsheet.Cells[i, 5].Value);
                        if (!string.IsNullOrEmpty(sdt) && double.TryParse(sdt, out double phoneNumber))
                        {
                            sdt = phoneNumber.ToString("0"); // Giữ nguyên số 0 đầu nếu có
                        }

                        string mancc = GetCellString(wsheet.Cells[i, 1].Value);  // Mã nhà cung cấp
                        String sql = "select count(*) from nhacungcap where mancc = '" + mancc + "'";
                        if (Thuvien.CheckExist(sql))
                        {
                            // Nếu mã nhà cung cấp đã tồn tại, lưu vào danh sách trùng
                            duplicateCodes.Add(mancc);
                        }

                        // Đưa dữ liệu vào DB
                        ThemmoiNCC(
                            GetCellString(wsheet.Cells[i, 1].Value), // Mã nhà cung cấp
                            GetCellString(wsheet.Cells[i, 2].Value), // Tên nhà cung cấp
                            GetCellString(wsheet.Cells[i, 3].Value), // Địa chỉ                       
                            GetCellString(wsheet.Cells[i, 4].Value), // Email
                            sdt  // Số điện thoại
                        );

                        i++; // Chuyển sang dòng tiếp theo
                    }
                }
                // Hiển thị thông báo tổng kết
                if (duplicateCodes.Count > 0)
                {
                    string duplicateMessage = "Các mã nhà cung cấp sau đã tồn tại và không được thêm mới:\n" + string.Join("\n", duplicateCodes);
                    MessageBox.Show(duplicateMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Nhập liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                // Đóng Workbook và Application
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }
                if (Excel != null)
                {
                    Excel.Quit();
                    Marshal.ReleaseComObject(Excel);
                }

                GC.Collect();  // Dọn dẹp bộ nhớ
                GC.WaitForPendingFinalizers();
            }
        }

        // Kiểm tra hàng có trống không
        private bool IsRowEmpty(Microsoft.Office.Interop.Excel.Worksheet sheet, int row)
        {
            for (int col = 1; col <= 6; col++) // Kiểm tra 6 cột
            {
                if (sheet.Cells[row, col].Value != null)
                {
                    return false; // Có dữ liệu
                }
            }
            return true; // Cả hàng trống
        }

        // Chuyển dữ liệu về string, tránh lỗi null
        private string GetCellString(object value)
        {
            return value?.ToString() ?? string.Empty;
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "excel file |*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = false;
            DialogResult kq = openFileDialog1.ShowDialog();
            if (kq == DialogResult.OK)
            {
                string tenfile = openFileDialog1.FileName;
                ReadExcel(tenfile);
                loadNCC();
            }
        }
    }
}
