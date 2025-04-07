using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
            string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(txtEmail.Text, patternEmail))
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            string patternSdt = @"^(0[1-9][0-9]{8})$";
            if (!Regex.IsMatch(txtSdt.Text, patternSdt))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSdt.Focus();
                return;
            }
            if (Thuvien.CheckExist("select count(*) from nhacungcap where mancc = '" + mancc + "'"))
            {
                MessageBox.Show("Mã nhà cung cấp đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMancc.Focus();
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
            string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(txtEmail.Text, patternEmail))
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            string patternSdt = @"^(0[1-9][0-9]{8})$";
            if (!Regex.IsMatch(txtSdt.Text, patternSdt))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSdt.Focus();
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
            string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, patternEmail))
            {
                return; // Nếu email không hợp lệ, dừng lại không thực hiện thêm mới
            }
            string patternSdt = @"^(0[1-9][0-9]{8})$";
            if (!Regex.IsMatch(sdt, patternSdt))
            {
                return; // Nếu số điện thoại không hợp lệ, dừng lại không thực hiện thêm mới
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
            List<string> invalidEmails = new List<string>();
            List<string> invalidPhones = new List<string>();

            try
            {
                foreach (Microsoft.Office.Interop.Excel.Worksheet wsheet in workbook.Worksheets)
                {
                    int i = 2;  // Bắt đầu đọc từ dòng 2 (bỏ dòng tiêu đề)
                    while (!IsRowEmpty(wsheet, i))
                    {
                        string mancc = GetCellString(wsheet.Cells[i, 1].Value);
                        string tenncc = GetCellString(wsheet.Cells[i, 2].Value);
                        string diachi = GetCellString(wsheet.Cells[i, 3].Value);
                        string email = GetCellString(wsheet.Cells[i, 4].Value);
                        string sdt = wsheet.Cells[i, 5].Text; // Giữ định dạng số điện thoại với số 0 đầu

                        bool isValid = true;

                        // Kiểm tra mã trùng
                        String sqlCheck = "select count(*) from nhacungcap where mancc = '" + mancc + "'";
                        if (Thuvien.CheckExist(sqlCheck))
                        {
                            duplicateCodes.Add(mancc);
                            isValid = false;
                        }

                        // Kiểm tra email
                        string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                        if (!Regex.IsMatch(email, patternEmail))
                        {
                            invalidEmails.Add($"{mancc} - {email}");
                            isValid = false;
                        }

                        // Kiểm tra sdt
                        string patternSdt = @"^(0[1-9][0-9]{8})$";
                        if (!Regex.IsMatch(sdt, patternSdt))
                        {
                            invalidPhones.Add($"{mancc} - {sdt}");
                            isValid = false;
                        }

                        // Nếu dữ liệu hợp lệ mới thêm vào
                        if (isValid)
                        {
                            ThemmoiNCC(mancc, tenncc, diachi, email, sdt);
                        }

                        i++; // Sang dòng tiếp theo
                    }
                }

                // Hiển thị thông báo tổng hợp
                string result = "";
                if (duplicateCodes.Count > 0)
                    result += "Mã nhà cung cấp đã tồn tại:\n" + string.Join("\n", duplicateCodes) + "\n\n";

                if (invalidEmails.Count > 0)
                    result += "Email không hợp lệ:\n" + string.Join("\n", invalidEmails) + "\n\n";

                if (invalidPhones.Count > 0)
                    result += "Số điện thoại không hợp lệ:\n" + string.Join("\n", invalidPhones) + "\n\n";

                if (!string.IsNullOrEmpty(result))
                {
                    MessageBox.Show(result, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Nhập liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
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

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        // Kiểm tra hàng có trống không
        private bool IsRowEmpty(Microsoft.Office.Interop.Excel.Worksheet sheet, int row)
        {
            for (int col = 1; col <= 5; col++) // Kiểm tra 5 cột
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

        private void txtSdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
