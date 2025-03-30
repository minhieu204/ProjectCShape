using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Account
{
    public partial class Acctb : Form
    {
        public Acctb()
        {
            InitializeComponent();
            Thuvien.CustomDataGridView(dataGridView1);
        }

        private void Acctb_Load(object sender, EventArgs e)
        {

        }
    }
}
