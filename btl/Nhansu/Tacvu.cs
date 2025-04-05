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
    public partial class Tacvu : Form
    {
        public Nhansu nhanSu;
        public Tacvu(Nhansu parent)
        {
            this.nhanSu = parent;
            InitializeComponent();
            luongtt.Text = "0";
            nhanduoc.Text = "0";
            lbluong.Text = "0";
            dateTimePicker1.Value = DateTime.Now;
           
        } 
        String ma = "";
        public void SetData(String ht, String ma)
        {
            label3.Text = ht;
            this.ma = ma;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
