using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xls = Microsoft.Office.Interop.Excel;

namespace btl.Account
{
    public partial class Acctb : Form
    {
        Acc Acc;
        public Acctb(Acc parent)
        {
            InitializeComponent();
            this.Acc = parent;
            Thuvien.CustomDataGridView(dataGridView1);
            paneltk.Visible = false;
            cbgioitinhtk.SelectedItem = "---Chọn---";
            loadtb();
        }

        private void Acctb_Load(object sender, EventArgs e)
        {
            
        }
        public void loadtb()
        {
            Thuvien.LoadDatatk("select * from quanly", "select manhanvien as maquanly, hoten, gioitinh, maphanquyen, username, pass, sdt, email from nhanvien", dataGridView1);
        }
        private void ReadExcel(string filename)
        {
            List<int> duplicateRows = new List<int>();
            StringBuilder errorMessages = new StringBuilder();
            bool hasError = false; 
            var loadingForm = new loadingForm();

            if (filename == null)
            {
                MessageBox.Show("Chưa chọn file");
                return;
            }

            loadingForm.Show();
            loadingForm.SetMessage("Đang đọc file...");

            xls.Application Excel = new xls.Application();
            xls.Workbook workbook = Excel.Workbooks.Open(filename);

            // Giai đoạn 1: Kiểm tra các lỗi và ghi vào log
            int totalRows = 0;
            foreach (xls.Worksheet wsheet in Excel.Worksheets)
            {
                int i = 2;
                totalRows = (int)wsheet.UsedRange.Rows.Count; // Lấy tổng số dòng dữ liệu

                do
                {
                    // Kiểm tra điều kiện dừng khi không còn dòng dữ liệu hợp lệ
                    if (wsheet.Cells[i, 1].Value == null && wsheet.Cells[i, 2].Value == null && wsheet.Cells[i, 3].Value == null &&
                        wsheet.Cells[i, 4].Value == null && wsheet.Cells[i, 5].Value == null && wsheet.Cells[i, 6].Value == null 
                        && wsheet.Cells[i, 7].Value == null && wsheet.Cells[i, 8].Value == null)
                    {
                        break;
                    }

                    string ma = wsheet.Cells[i, 1].Value.ToString();
                    string username = wsheet.Cells[i, 2].Value.ToString();
                    string phanquyen = wsheet.Cells[i, 4].Value.ToString();

                    // Kiểm tra trùng mã tài khoản và username trong cả hai bảng (quanly và nhanvien)
                    string sqlCheckQuanLyMa = $"SELECT COUNT(*) FROM quanly WHERE maquanly = '{ma}'";
                    string sqlCheckNhanVienMa = $"SELECT COUNT(*) FROM nhanvien WHERE manhanvien = '{ma}'";
                    string sqlCheckQuanLyUsername = $"SELECT COUNT(*) FROM quanly WHERE username = '{username}'";
                    string sqlCheckNhanVienUsername = $"SELECT COUNT(*) FROM nhanvien WHERE username = '{username}'";

                    bool isMaTrung = Thuvien.CheckExist(sqlCheckQuanLyMa) || Thuvien.CheckExist(sqlCheckNhanVienMa);
                    bool isUsernameTrung = Thuvien.CheckExist(sqlCheckQuanLyUsername) || Thuvien.CheckExist(sqlCheckNhanVienUsername);
                    bool isPhanQuyenInvalid = phanquyen != "ql" && phanquyen != "nv";

                    if (isMaTrung || isUsernameTrung || isPhanQuyenInvalid)
                    {
                        // Ghi lại dòng trùng và thêm thông báo lỗi
                        duplicateRows.Add(i);
                        if (isMaTrung)
                            errorMessages.AppendLine($"Dòng {i} bị trùng: Mã tài khoản đã tồn tại.");
                        if (isUsernameTrung)
                            errorMessages.AppendLine($"Dòng {i} bị trùng: Username đã tồn tại.");
                        if (isPhanQuyenInvalid)
                            errorMessages.AppendLine($"Dòng {i} bị lỗi: Phân quyền không hợp lệ.");

                        hasError = true; // Đánh dấu là có lỗi
                    }

                    // Cập nhật tiến trình
                    loadingForm.SetProgress((int)((float)i / totalRows * 100));

                    i++;
                    loadingForm.SetMessage($"Đang kiểm tra dòng {i}..."); // Cập nhật trạng thái
                }
                while (true);
            }

            // Giai đoạn 2: Nếu có lỗi, ghi log và hiển thị thông báo, không thêm vào DB
            if (hasError)
            {
                // Ghi thông tin vào file log
                string logFilePath = Path.Combine(Application.StartupPath, "Logs", "duplicate_log.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); // Tạo thư mục Logs nếu chưa có
                File.AppendAllText(logFilePath, $"Lỗi trùng vào lúc {DateTime.Now}\n");
                File.AppendAllText(logFilePath, errorMessages.ToString());
                File.AppendAllText(logFilePath, "=================================\n");

                // Hiển thị thông báo tóm tắt
                MessageBox.Show($"Phát hiện dữ liệu không hợp lệ, Chi tiết: {logFilePath}");
            }
            else
            {
                // Giai đoạn 2: Nếu không có lỗi, nhập dữ liệu vào cơ sở dữ liệu
                foreach (xls.Worksheet wsheet in Excel.Worksheets)
                {
                    int i = 2;
                    do
                    {
                        if (wsheet.Cells[i, 1].Value == null && wsheet.Cells[i, 2].Value == null && wsheet.Cells[i, 3].Value == null &&
                            wsheet.Cells[i, 4].Value == null && wsheet.Cells[i, 5].Value == null && wsheet.Cells[i, 6].Value == null
                            && wsheet.Cells[i, 7].Value == null && wsheet.Cells[i, 8].Value == null)
                        {
                            break;
                        }

                        string ma = wsheet.Cells[i, 1].Value.ToString();
                        string username = wsheet.Cells[i, 2].Value.ToString();
                        string phanquyen = wsheet.Cells[i, 4].Value.ToString();
                        Themmoitaikhoan(ma, username, phanquyen,
                            wsheet.Cells[i, 3].Value.ToString(), wsheet.Cells[i, 5].Value.ToString(), wsheet.Cells[i, 6].Value.ToString(), wsheet.Cells[i, 7].Value.ToString(), wsheet.Cells[i, 8].Value.ToString());
                        loadingForm.SetProgress((int)((float)i / totalRows * 100));
                        i++;
                        loadingForm.SetMessage($"Đang thêm dòng {i} vào cơ sở dữ liệu..."); // Cập nhật trạng thái
                    }
                    while (true);
                }

                MessageBox.Show("Dữ liệu đã được thêm vào cơ sở dữ liệu thành công.");
            }
            loadingForm.Close();
            workbook.Close(false); 
            Excel.Quit();
        }
        private void Themmoitaikhoan(String ma, String un, String pq, String pass, String ht, String gt, String sdt, String email)
        {
            if (pq == "ql")
            {
                String sql = String.Format("insert into quanly values('{0}', '{1}', '{2}', N'{3}', N'{4}', N'{5}', '{6}', '{7}')", ma, un, pass, pq, ht, gt, sdt, email);
                Thuvien.ExecuteQuery(sql);
            }
            else
            {
                String sql = String.Format("insert into nhanvien values('{0}', '{1}', '{2}', N'{3}', N'{4}', N'{5}', '{6}', '{7}')", ma, un, pass, pq, ht, gt, sdt, email);
                Thuvien.ExecuteQuery(sql);
            }
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
                string ma = row.Cells["maquanly"].Value.ToString();
                string ht = row.Cells["hoten"].Value.ToString();
                string gt = row.Cells["gioitinh"].Value.ToString();
                string pq = row.Cells["maphanquyen"].Value.ToString();
                string un = row.Cells["username"].Value.ToString();
                string pw = row.Cells["pass"].Value.ToString();
                string sdt = row.Cells["sdt"].Value.ToString();
                string email = row.Cells["email"].Value.ToString();
                Acc.acctv.setData(ma, ht, gt, pq, un, pw, sdt, email);
                Acc.SwitchToTab(1);
            }
        }

        private void xoacm_Click(object sender, EventArgs e)
        {
            String sql = "";
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string ma = row.Cells["maquanly"].Value.ToString();
            string pq = row.Cells["maphanquyen"].Value.ToString();
            if (ma==Datauser.ID) {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập");
                return;
            }
            DialogResult mess_delete = MessageBox.Show("Bạn có muốn xóa không ?", "Xác nhận: ", MessageBoxButtons.YesNo);
            if (mess_delete == DialogResult.Yes)
            {
                if (pq == "ql")
                {
                    sql = "delete from quanly where maquanly = '" + ma + "'";
                }
                else
                {
                    sql = "delete from nhanvien where manhanvien = '" + ma + "'";
                }
            }
            Thuvien.ExecuteQuery(sql);
            loadtb();
            MessageBox.Show("Xóa tài khoản thành công!","Thông báo!");

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String tk = textBox1.Text;
            String sql1 = "select * from quanly where hoten like N'%" + tk + "%' or maquanly like N'%" + tk + "%' or username like N'%" + tk + "%' or sdt like N'%" + tk + "%' or email like N'%" + tk + "%'";
            String sql2 = "select manhanvien as maquanly, hoten, gioitinh, maphanquyen, username, pass, sdt, email from nhanvien where hoten like N'%" + tk + "%' or manhanvien like N'%" + tk + "%' or username like N'%" + tk + "%' or sdt like N'%" + tk + "%' or email like N'%" + tk + "%'";
            Thuvien.LoadDatatk(sql1, sql2, dataGridView1);
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(234, 132, 50);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(74, 125, 175);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
            if (paneltk.Visible)
            {
                paneltk.Visible = false;
                textBox1.Enabled = true;
                loadtb();
            }
            else
            {
                paneltk.Visible = true;
                textBox1.Enabled = false;
                textBox1.Text = "";
            }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            String ma = matk.Text;
            String ht = httk.Text;
            String gt = cbgioitinhtk.SelectedItem.ToString();
            if (gt == "---Chọn---")
            {
                gt = "";
            }
            String un = unametk.Text;
            String sdt = sdttk.Text;
            String email = emailtk.Text;
            String sql1 = "select * from quanly where hoten like N'%" + ht + "%' and maquanly like N'%" + ma + "%' and username like N'%" + un + "%' and sdt like N'%" + sdt + "%' and email like N'%" + email + "%' and gioitinh like N'%" + gt + "%'";
            String sql2 = "select manhanvien as maquanly, hoten, gioitinh, maphanquyen, username, pass, sdt, email from nhanvien where hoten like N'%" + ht + "%' and manhanvien like N'%" + ma + "%' and username like N'%" + un + "%' and sdt like N'%" + sdt + "%' and email like N'%" + email + "%' and gioitinh like N'%" + gt + "%'";
            Thuvien.LoadDatatk(sql1, sql2, dataGridView1);
        }

        private void sdttk_Leave(object sender, EventArgs e)
        {

        }

        private void sdttk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
                ReadExcel(filePath);
                loadtb();
            }
        }
    }
}
