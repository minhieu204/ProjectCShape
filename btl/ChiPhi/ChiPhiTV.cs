using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.ChiPhi
{
    public partial class ChiPhiTV : Form
    {
        private string hiddenID;
        ChiPhi chiPhi;
        public ChiPhiTV(ChiPhi parent)
        {
            InitializeComponent();
            this.chiPhi = parent;
        }

        public void SetData(string id,string dn, string nc, DateTime thang, string sc)
        {
            hiddenID = id;
            txtdien.Text = dn;
            txtnuoc.Text = nc;
            dateTimePicker1.Value = thang;
            txtsuachua.Text = sc;
            
            btntv.Text = "Cập nhật";
            header.Text = "Cập nhật Chi Phí";
        }

        private void Reload()
        {
            txtdien.Text = "";
            txtnuoc.Text = "";
            txtsuachua.Text = "";
            btntv.Text = "Thêm";
            header.Text = "Thêm Chi Phí";
        }
        private void btntv_Click(object sender, EventArgs e)
        {
            if (txtsuachua.Text == "" || txtdien.Text == "" || txtnuoc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            decimal phiSuaChua = decimal.Parse(txtsuachua.Text);
            decimal tienDien = decimal.Parse(txtdien.Text);
            decimal tienNuoc = decimal.Parse(txtnuoc.Text);
            DateTime thangNam = dateTimePicker1.Value;

            string sql = "";

            // Chuyển về ngày đầu tháng để quản lý theo tháng
            DateTime ngayDauThang = new DateTime(thangNam.Year, thangNam.Month, 1);
            string ngaySQL = ngayDauThang.ToString("dd-MM-yyyy");

            if (btntv.Text == "Thêm")
            {
                // Kiểm tra đã tồn tại tháng này chưa
                if (!Thuvien.CheckExist($"SELECT COUNT(*) FROM ChiPhi WHERE ThangNam = '{ngaySQL}'"))
                {
                    sql = $"INSERT INTO ChiPhi (ThangNam, PhiSuaChua, TienDien, TienNuoc) " +
                          $"VALUES ('{ngaySQL}', {phiSuaChua}, {tienDien}, {tienNuoc})";
                }
                else
                {
                    MessageBox.Show("Chi phí cho tháng này đã tồn tại");
                    return;
                }
            }
            else // Cập nhật
            {
                sql = $"UPDATE ChiPhi SET PhiSuaChua = {phiSuaChua}, TienDien = {tienDien}, TienNuoc = {tienNuoc}, ThangNam = '{ngaySQL}' " +
             $"WHERE ID = '{hiddenID}'";

            }

            Thuvien.ExecuteQuery(sql);
            chiPhi.chiPhiQL.loadtb();
            chiPhi.SwitchToTab(0);
            MessageBox.Show("Thao tác Thành công!!", "Thông báo!");
            Reload();
        }

        private void btnload_Click(object sender, EventArgs e)
        {
            Reload();
        }
    }
}
