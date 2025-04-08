using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.StyledXmlParser.Node;

namespace btl
{
    public partial class FormBH : Form
    {
        public FormBH()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Thuvien.CustomDataGridView(dgvSP);
            Thuvien.CustomDataGridView(dgvHD);
            Thuvien.CustomDisabledButton(button1);
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            checkBox1.Enabled = false;
        }
        private void deleteGiohang()
        {
            String sql = "delete from giohang";
            Thuvien.ExecuteQuery(sql);
        }
        String sdt = "";
        String name = "";
        int diem = 0;
        int diemtichluy = 0;
        private void checkSL()
        {   
            if (txtSLban.Text.Trim() == "")
            {
                return;
            }
            int slban = int.Parse(txtSLban.Text.Trim());
            int slco = int.Parse(txtSLco.Text.Trim());
            if (slban <= 0)
            {
                MessageBox.Show("Số lượng bán phải > 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSLban.Text = "";
                return;
            }
            if (Thuvien.CheckExist("select count(*) from giohang where masp='" + txtMasp.Text.Trim() + "'"))
            {
                String sql = "select soluongnhap from giohang where masp='" + txtMasp.Text.Trim() + "'";
                int sldachon = int.Parse(Thuvien.GetSingleValue(sql).ToString());
                if (slban > slco - sldachon)
                {
                    MessageBox.Show("Không đủ hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSLban.Text = "";
                    return;
                }
            }
            if (slban > slco)
            {
                MessageBox.Show("Không đủ hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSLban.Text = "";
                return;
            }
        }

        private void loadTongtien()
        {
            if (string.IsNullOrWhiteSpace(txtTongtien.Text))
            {
                txtTongtien.Text = "....................................................";
            }
            string sql = "SELECT SUM(thanhtien) FROM giohang";
            object result = Thuvien.GetSingleValue(sql);
            int tongtien = 0;
            if (result != null && int.TryParse(result.ToString(), out int temp))
            {
                tongtien = temp;
            }
            if (checkBox1.Checked)
            {
                if (!string.IsNullOrWhiteSpace(sdt) && tongtien > 0)
                {
                    if (radioButton2.Checked)
                    {
                        tongtien -= diem * 10;
                        if (tongtien < 0) tongtien = 0;
                        txtTongtien.Text = tongtien.ToString();
                    }
                    else if (radioButton1.Checked)
                    {
                        diemtichluy = tongtien / 1000;
                        txtTongtien.Text = tongtien.ToString();
                    }
                    else
                    {
                        txtTongtien.Text = tongtien.ToString();
                    }
                }
                else
                {
                    txtTongtien.Text = tongtien > 0 ? tongtien.ToString() : "....................................................";
                }
            }
            else
            {
                txtTongtien.Text = tongtien > 0 ? tongtien.ToString() : "....................................................";
            }
            if (txtTongtien.Text == "....................................................")
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
            }
        }

        private void resetText()
        {
            txtMasp.Text = "";
            txtTensp.Text = "";
            txtDVT.Text = "";
            txtGiaban.Text = "";
            txtSLco.Text = "";
            txtSLban.Text = "";
            txtThanhtien.Text = "....................................................";
        }

        public void loadSP()
        {
            String sql = "select masp, tensp, donvitinh, giaban, soluong " +
                         "from sanpham " +
                         "where soluong > 0";
            Thuvien.LoadData(sql, dgvSP);
        }

        private void loadGiohang()
        {
            String sql = "select * from giohang";
            Thuvien.LoadData(sql, dgvHD);
        }


        private void loadMadon()
        {
            String sql = "select top 1 madon from donhang order by madon desc";
            int mahd = int.Parse(Thuvien.GetSingleValue(sql).ToString()) + 1;
            txtMahd.Text = mahd.ToString();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            deleteGiohang();
            loadSP();
            loadMadon();
            loadGiohang();
            txtMasp.Enabled = false;
            txtTensp.Enabled = false;
            txtDVT.Enabled = false;
            txtGiaban.Enabled = false;
            txtSLco.Enabled = false;
            txtSLban.Enabled = false;
            txtMahd.Enabled = false;
            txtNgaynhap.Enabled = false;
            txtNguoiban.Enabled = false;
            txtNguoiban.Text = Datauser.HoTen;
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnThemmoi);
            Thuvien.CustomDisabledButton(btnNhaplai);
        }

        String maTemp = "";
        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMasp.Text = dgvSP.Rows[i].Cells[0].Value.ToString();
                txtTensp.Text = dgvSP.Rows[i].Cells[1].Value.ToString();
                txtDVT.Text = dgvSP.Rows[i].Cells[2].Value.ToString();
                txtGiaban.Text = dgvSP.Rows[i].Cells[3].Value.ToString();
                txtSLco.Text = dgvSP.Rows[i].Cells[4].Value.ToString();
                txtSLban.Enabled = true;
                Thuvien.CustomEnabledButton(btnThemmoi);
                Thuvien.CustomEnabledButton(btnNhaplai);
            }
        }

        private void txtSLban_TextChanged(object sender, EventArgs e)
        {
            checkSL();
            if (txtSLban.Text.Trim() == "")
            {
                txtThanhtien.Text = "....................................................";
                return;
            }
            int slban = int.Parse(txtSLban.Text.Trim());
            int thanhtien = int.Parse(txtGiaban.Text.Trim()) * slban;
            txtThanhtien.Text = thanhtien.ToString();
        }

        private void txtSLban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            String masp = txtMasp.Text.Trim();
            String tensp = txtTensp.Text.Trim();
            String dvt = txtDVT.Text.Trim();
            String giaban = txtGiaban.Text.Trim();
            String slban = txtSLban.Text.Trim();
            String thanhtien = txtThanhtien.Text.Trim();
            if (slban == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSLban.Focus();
                return;
            }
            String check = "select count(*) from giohang where masp = '" + masp + "'";
            if (!Thuvien.CheckExist(check))
            {
                String sql = "insert into giohang values('" + masp + "', N'" + tensp + "', '" + giaban + "', N'" + dvt + "', '" + slban + "', '" + thanhtien + "')";
                Thuvien.ExecuteQuery(sql);
            }
            else
            {
                String sql = "update giohang set thanhtien=thanhtien+'" + thanhtien + "', soluongnhap=soluongnhap+'" + slban + "' where masp='" + masp + "'";
                Thuvien.ExecuteQuery(sql);
            }
            loadGiohang();  
            resetText();
            Thuvien.CustomDisabledButton(btnThemmoi);
            Thuvien.CustomDisabledButton(btnNhaplai);
            txtSLban.Enabled = false;
            dgvSP.ClearSelection();
            dgvHD.ClearSelection();
            loadTongtien();
            checkBox1.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String sql = "delete from giohang where masp='" + maTemp + "'";
            Thuvien.ExecuteQuery(sql);
            loadGiohang();
            Thuvien.CustomDisabledButton(btnXoa);
            dgvSP.ClearSelection();
            dgvHD.ClearSelection();
            loadTongtien();
        }

        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Thuvien.CustomEnabledButton(btnXoa);
            int i = e.RowIndex;
            if (i >= 0)
            {
                maTemp = dgvHD.Rows[i].Cells[0].Value.ToString();
            }
        }

        private void btnNhaplai_Click(object sender, EventArgs e)
        {
            txtSLban.Text = "";
        }

        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            if (!Thuvien.CheckExist("select count(*) from giohang"))
            {
                MessageBox.Show("Giỏ hàng trống, không thể thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkBox1.Checked)
            {
                if (radioButton1.Checked)
                {
                    String sqld = "update khachhang set diem=diem+" + diemtichluy + " where sdt='" + sdt + "'";
                    Thuvien.ExecuteQuery(sqld);
                }
                else if (radioButton2.Checked)
                {
                    String sqlf = "update khachhang set diem=diem-" + diem + " where sdt='" + sdt + "'";
                    Thuvien.ExecuteQuery(sqlf);
                }
            }
            string tenpdf = "donhang" + txtMahd.Text.Trim() + ".pdf";
            Thuvien.GenerateInvoice(tenpdf, name, txtMahd.Text, txtTongtien.Text, diem.ToString());
            String sql = "insert into donhang(ngayban, tongtien, manhanvien) " +
                         "values('" + DateTime.Now.ToString() + "', '" + txtTongtien.Text.Trim() + "', '" + Datauser.ID + "')";
            Thuvien.ExecuteQuery(sql);
            String madon = txtMahd.Text.Trim();
            String sql1 = "insert into chitietdonhang(madon, masp, soluong, giaban, thanhtien) " +
                          "select '" + madon + "', masp, soluongnhap, giaban, thanhtien from giohang";
            Thuvien.ExecuteQuery(sql1);
            String sql2 = "delete from giohang";
            Thuvien.ExecuteQuery(sql2);
            MessageBox.Show("Thanh toán thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadGiohang();
            loadSP();
            loadMadon();
            checkBox1.Checked = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String search = txtSearch.Text;
            String sql = "select masp, tensp, donvitinh, giaban, soluong " +
                         "from sanpham " +
                         "where soluong > 0 and tensp like N'%"+ search +"%'";
            Thuvien.LoadData(sql, dgvSP);
        }
        public void checkkh() {
            if (sdt != "")
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
            else
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            if (checkBox1.Checked == false)
            {
                sdt = "";
                name = "";
                diem = 0;
                diemtichluy = 0;
                txtdiem.Text = "..............................................................................";
                txtkh.Text = "..............................................................................";
                Thuvien.CustomDisabledButton(button1);
            }
            else
            {
                txtdiem.Text = "..............................................................................";
                txtkh.Text = "..............................................................................";
                Thuvien.CustomEnabledButton(button1);
            }
            checkkh();
        }
        public void SetData(String sdt, String name, int diem)
        {
            this.sdt = sdt;
            this.name = name;
            this.diem = diem;
            txtdiem.Text = diem.ToString();
            txtkh.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BHKhachhang f = new BHKhachhang(this);
            f.ShowDialog();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            loadTongtien();
        }
    }
}
