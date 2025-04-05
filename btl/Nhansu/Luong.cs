using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }
        public void Loadtb()
        {
            int month = dateTimePicker1.Value.Month;
            int year = dateTimePicker1.Value.Year;
            String sql = "select * from luong where MONTH(ngaynhan) = " + month + " and YEAR(ngaynhan) = " + year + "";
            Thuvien.LoadData(sql, dataGridView1);
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Loadtb();
        }
    }
}
