namespace btl
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbname = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnHam = new System.Windows.Forms.PictureBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.panelDB = new System.Windows.Forms.Panel();
            this.btnDB = new System.Windows.Forms.Button();
            this.panelSP = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSP = new System.Windows.Forms.Button();
            this.panelNCC = new System.Windows.Forms.Panel();
            this.btnncc = new System.Windows.Forms.Button();
            this.panelDH = new System.Windows.Forms.Panel();
            this.btndh = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnacc = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnns = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnkh = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btncn = new System.Windows.Forms.Button();
            this.panelLO = new System.Windows.Forms.Panel();
            this.btndt = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnkm = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btntk = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnlu = new System.Windows.Forms.Button();
            this.sidebarTransition = new System.Windows.Forms.Timer(this.components);
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).BeginInit();
            this.sidebar.SuspendLayout();
            this.panelDB.SuspendLayout();
            this.panelSP.SuspendLayout();
            this.panelNCC.SuspendLayout();
            this.panelDH.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panelLO.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(33)))), ((int)(((byte)(89)))));
            this.panel1.Controls.Add(this.lbname);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnHam);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1348, 63);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // lbname
            // 
            this.lbname.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbname.ForeColor = System.Drawing.SystemColors.Control;
            this.lbname.Location = new System.Drawing.Point(61, 12);
            this.lbname.Name = "lbname";
            this.lbname.Size = new System.Drawing.Size(372, 43);
            this.lbname.TabIndex = 4;
            this.lbname.Text = "Xin chào,!";
            this.lbname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbname.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::btl.Properties.Resources.minimize_square_minimalistic_svgrepo_com__1_;
            this.pictureBox2.Location = new System.Drawing.Point(1242, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(44, 43);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::btl.Properties.Resources.close_square_svgrepo_com__1_;
            this.pictureBox1.Location = new System.Drawing.Point(1292, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnHam
            // 
            this.btnHam.Image = global::btl.Properties.Resources.burger_menu_svgrepo_com;
            this.btnHam.Location = new System.Drawing.Point(3, 12);
            this.btnHam.Name = "btnHam";
            this.btnHam.Size = new System.Drawing.Size(52, 43);
            this.btnHam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHam.TabIndex = 1;
            this.btnHam.TabStop = false;
            this.btnHam.Click += new System.EventHandler(this.btnHam_Click);
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.sidebar.Controls.Add(this.panelDB);
            this.sidebar.Controls.Add(this.panelSP);
            this.sidebar.Controls.Add(this.panelNCC);
            this.sidebar.Controls.Add(this.panelDH);
            this.sidebar.Controls.Add(this.panel2);
            this.sidebar.Controls.Add(this.panel3);
            this.sidebar.Controls.Add(this.panel4);
            this.sidebar.Controls.Add(this.panel6);
            this.sidebar.Controls.Add(this.panelLO);
            this.sidebar.Controls.Add(this.panel5);
            this.sidebar.Controls.Add(this.panel7);
            this.sidebar.Controls.Add(this.panel8);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebar.Location = new System.Drawing.Point(0, 63);
            this.sidebar.Name = "sidebar";
            this.sidebar.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.sidebar.Size = new System.Drawing.Size(200, 658);
            this.sidebar.TabIndex = 3;
            // 
            // panelDB
            // 
            this.panelDB.Controls.Add(this.btnDB);
            this.panelDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDB.Location = new System.Drawing.Point(3, 33);
            this.panelDB.Name = "panelDB";
            this.panelDB.Size = new System.Drawing.Size(197, 40);
            this.panelDB.TabIndex = 5;
            // 
            // btnDB
            // 
            this.btnDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btnDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDB.FlatAppearance.BorderSize = 0;
            this.btnDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDB.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDB.ForeColor = System.Drawing.Color.White;
            this.btnDB.Image = ((System.Drawing.Image)(resources.GetObject("btnDB.Image")));
            this.btnDB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDB.Location = new System.Drawing.Point(0, 0);
            this.btnDB.Margin = new System.Windows.Forms.Padding(0);
            this.btnDB.Name = "btnDB";
            this.btnDB.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnDB.Size = new System.Drawing.Size(197, 40);
            this.btnDB.TabIndex = 4;
            this.btnDB.TabStop = false;
            this.btnDB.Text = "         Bán hàng";
            this.btnDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDB.UseVisualStyleBackColor = false;
            this.btnDB.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelSP
            // 
            this.panelSP.Controls.Add(this.button1);
            this.panelSP.Controls.Add(this.btnSP);
            this.panelSP.Location = new System.Drawing.Point(3, 79);
            this.panelSP.Name = "panelSP";
            this.panelSP.Size = new System.Drawing.Size(197, 40);
            this.panelSP.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(197, 40);
            this.button1.TabIndex = 8;
            this.button1.TabStop = false;
            this.button1.Text = "         Đăng xuất";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnlu_Click);
            // 
            // btnSP
            // 
            this.btnSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btnSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSP.FlatAppearance.BorderSize = 0;
            this.btnSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSP.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSP.ForeColor = System.Drawing.Color.White;
            this.btnSP.Image = ((System.Drawing.Image)(resources.GetObject("btnSP.Image")));
            this.btnSP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSP.Location = new System.Drawing.Point(0, 0);
            this.btnSP.Margin = new System.Windows.Forms.Padding(0);
            this.btnSP.Name = "btnSP";
            this.btnSP.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSP.Size = new System.Drawing.Size(197, 40);
            this.btnSP.TabIndex = 4;
            this.btnSP.TabStop = false;
            this.btnSP.Text = "         Sản phẩm";
            this.btnSP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSP.UseVisualStyleBackColor = false;
            this.btnSP.Click += new System.EventHandler(this.btnSP_Click);
            // 
            // panelNCC
            // 
            this.panelNCC.Controls.Add(this.btnncc);
            this.panelNCC.Location = new System.Drawing.Point(3, 125);
            this.panelNCC.Name = "panelNCC";
            this.panelNCC.Size = new System.Drawing.Size(197, 40);
            this.panelNCC.TabIndex = 8;
            // 
            // btnncc
            // 
            this.btnncc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btnncc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnncc.FlatAppearance.BorderSize = 0;
            this.btnncc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnncc.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnncc.ForeColor = System.Drawing.Color.White;
            this.btnncc.Image = ((System.Drawing.Image)(resources.GetObject("btnncc.Image")));
            this.btnncc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnncc.Location = new System.Drawing.Point(0, 0);
            this.btnncc.Margin = new System.Windows.Forms.Padding(0);
            this.btnncc.Name = "btnncc";
            this.btnncc.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnncc.Size = new System.Drawing.Size(197, 40);
            this.btnncc.TabIndex = 7;
            this.btnncc.TabStop = false;
            this.btnncc.Text = "         Nhà cung cấp";
            this.btnncc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnncc.UseVisualStyleBackColor = false;
            this.btnncc.Click += new System.EventHandler(this.btnncc_Click);
            // 
            // panelDH
            // 
            this.panelDH.Controls.Add(this.btndh);
            this.panelDH.Location = new System.Drawing.Point(3, 171);
            this.panelDH.Name = "panelDH";
            this.panelDH.Size = new System.Drawing.Size(197, 40);
            this.panelDH.TabIndex = 8;
            // 
            // btndh
            // 
            this.btndh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btndh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndh.FlatAppearance.BorderSize = 0;
            this.btndh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndh.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndh.ForeColor = System.Drawing.Color.White;
            this.btndh.Image = ((System.Drawing.Image)(resources.GetObject("btndh.Image")));
            this.btndh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndh.Location = new System.Drawing.Point(0, 0);
            this.btndh.Margin = new System.Windows.Forms.Padding(0);
            this.btndh.Name = "btndh";
            this.btndh.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btndh.Size = new System.Drawing.Size(197, 40);
            this.btndh.TabIndex = 7;
            this.btndh.TabStop = false;
            this.btndh.Text = "         Đơn hàng";
            this.btndh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndh.UseVisualStyleBackColor = false;
            this.btndh.Click += new System.EventHandler(this.btndh_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnacc);
            this.panel2.Location = new System.Drawing.Point(3, 217);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(197, 40);
            this.panel2.TabIndex = 7;
            // 
            // btnacc
            // 
            this.btnacc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btnacc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnacc.FlatAppearance.BorderSize = 0;
            this.btnacc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnacc.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnacc.ForeColor = System.Drawing.Color.White;
            this.btnacc.Image = ((System.Drawing.Image)(resources.GetObject("btnacc.Image")));
            this.btnacc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnacc.Location = new System.Drawing.Point(0, 0);
            this.btnacc.Margin = new System.Windows.Forms.Padding(0);
            this.btnacc.Name = "btnacc";
            this.btnacc.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnacc.Size = new System.Drawing.Size(197, 40);
            this.btnacc.TabIndex = 5;
            this.btnacc.TabStop = false;
            this.btnacc.Text = "         Tài khoản";
            this.btnacc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnacc.UseVisualStyleBackColor = false;
            this.btnacc.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnns);
            this.panel3.Location = new System.Drawing.Point(3, 263);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(197, 40);
            this.panel3.TabIndex = 7;
            // 
            // btnns
            // 
            this.btnns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btnns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnns.FlatAppearance.BorderSize = 0;
            this.btnns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnns.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnns.ForeColor = System.Drawing.Color.White;
            this.btnns.Image = ((System.Drawing.Image)(resources.GetObject("btnns.Image")));
            this.btnns.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnns.Location = new System.Drawing.Point(0, 0);
            this.btnns.Margin = new System.Windows.Forms.Padding(0);
            this.btnns.Name = "btnns";
            this.btnns.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnns.Size = new System.Drawing.Size(197, 40);
            this.btnns.TabIndex = 5;
            this.btnns.TabStop = false;
            this.btnns.Text = "         Nhân sự";
            this.btnns.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnns.UseVisualStyleBackColor = false;
            this.btnns.Click += new System.EventHandler(this.btnns_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnkh);
            this.panel4.Location = new System.Drawing.Point(3, 309);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(197, 40);
            this.panel4.TabIndex = 7;
            // 
            // btnkh
            // 
            this.btnkh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btnkh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnkh.FlatAppearance.BorderSize = 0;
            this.btnkh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkh.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnkh.ForeColor = System.Drawing.Color.White;
            this.btnkh.Image = ((System.Drawing.Image)(resources.GetObject("btnkh.Image")));
            this.btnkh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnkh.Location = new System.Drawing.Point(0, 0);
            this.btnkh.Margin = new System.Windows.Forms.Padding(0);
            this.btnkh.Name = "btnkh";
            this.btnkh.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnkh.Size = new System.Drawing.Size(197, 40);
            this.btnkh.TabIndex = 6;
            this.btnkh.TabStop = false;
            this.btnkh.Text = "         Khách hàng";
            this.btnkh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnkh.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btncn);
            this.panel6.Location = new System.Drawing.Point(3, 355);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(197, 40);
            this.panel6.TabIndex = 8;
            // 
            // btncn
            // 
            this.btncn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btncn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncn.FlatAppearance.BorderSize = 0;
            this.btncn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncn.ForeColor = System.Drawing.Color.White;
            this.btncn.Image = ((System.Drawing.Image)(resources.GetObject("btncn.Image")));
            this.btncn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncn.Location = new System.Drawing.Point(0, 0);
            this.btncn.Margin = new System.Windows.Forms.Padding(0);
            this.btncn.Name = "btncn";
            this.btncn.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btncn.Size = new System.Drawing.Size(197, 40);
            this.btncn.TabIndex = 8;
            this.btncn.TabStop = false;
            this.btncn.Text = "         Chi Nhánh";
            this.btncn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncn.UseVisualStyleBackColor = false;
            // 
            // panelLO
            // 
            this.panelLO.Controls.Add(this.btndt);
            this.panelLO.Location = new System.Drawing.Point(3, 401);
            this.panelLO.Name = "panelLO";
            this.panelLO.Size = new System.Drawing.Size(197, 40);
            this.panelLO.TabIndex = 6;
            // 
            // btndt
            // 
            this.btndt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btndt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndt.FlatAppearance.BorderSize = 0;
            this.btndt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndt.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndt.ForeColor = System.Drawing.Color.White;
            this.btndt.Image = ((System.Drawing.Image)(resources.GetObject("btndt.Image")));
            this.btndt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndt.Location = new System.Drawing.Point(0, 0);
            this.btndt.Margin = new System.Windows.Forms.Padding(0);
            this.btndt.Name = "btndt";
            this.btndt.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btndt.Size = new System.Drawing.Size(197, 40);
            this.btndt.TabIndex = 8;
            this.btndt.TabStop = false;
            this.btndt.Text = "         Đối tác";
            this.btndt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndt.UseVisualStyleBackColor = false;
            this.btndt.Click += new System.EventHandler(this.btndt_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnkm);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 447);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(197, 40);
            this.panel5.TabIndex = 9;
            // 
            // btnkm
            // 
            this.btnkm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btnkm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnkm.FlatAppearance.BorderSize = 0;
            this.btnkm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkm.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnkm.ForeColor = System.Drawing.Color.White;
            this.btnkm.Image = ((System.Drawing.Image)(resources.GetObject("btnkm.Image")));
            this.btnkm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnkm.Location = new System.Drawing.Point(0, 0);
            this.btnkm.Margin = new System.Windows.Forms.Padding(0);
            this.btnkm.Name = "btnkm";
            this.btnkm.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnkm.Size = new System.Drawing.Size(197, 40);
            this.btnkm.TabIndex = 8;
            this.btnkm.TabStop = false;
            this.btnkm.Text = "         Khuyến mại";
            this.btnkm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnkm.UseVisualStyleBackColor = false;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btntk);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 493);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(197, 40);
            this.panel7.TabIndex = 10;
            // 
            // btntk
            // 
            this.btntk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btntk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btntk.FlatAppearance.BorderSize = 0;
            this.btntk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btntk.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntk.ForeColor = System.Drawing.Color.White;
            this.btntk.Image = ((System.Drawing.Image)(resources.GetObject("btntk.Image")));
            this.btntk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btntk.Location = new System.Drawing.Point(0, 0);
            this.btntk.Margin = new System.Windows.Forms.Padding(0);
            this.btntk.Name = "btntk";
            this.btntk.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btntk.Size = new System.Drawing.Size(197, 40);
            this.btntk.TabIndex = 7;
            this.btntk.TabStop = false;
            this.btntk.Text = "         Thống kê";
            this.btntk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btntk.UseVisualStyleBackColor = false;
            this.btntk.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnlu);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 539);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(197, 40);
            this.panel8.TabIndex = 11;
            // 
            // btnlu
            // 
            this.btnlu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.btnlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnlu.FlatAppearance.BorderSize = 0;
            this.btnlu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlu.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlu.ForeColor = System.Drawing.Color.White;
            this.btnlu.Image = ((System.Drawing.Image)(resources.GetObject("btnlu.Image")));
            this.btnlu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlu.Location = new System.Drawing.Point(0, 0);
            this.btnlu.Margin = new System.Windows.Forms.Padding(0);
            this.btnlu.Name = "btnlu";
            this.btnlu.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnlu.Size = new System.Drawing.Size(197, 40);
            this.btnlu.TabIndex = 7;
            this.btnlu.TabStop = false;
            this.btnlu.Text = "         Đăng xuất";
            this.btnlu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlu.UseVisualStyleBackColor = false;
            this.btnlu.Click += new System.EventHandler(this.btnlu_Click);
            // 
            // sidebarTransition
            // 
            this.sidebarTransition.Interval = 10;
            this.sidebarTransition.Tick += new System.EventHandler(this.sidebarTransition_Tick);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).EndInit();
            this.sidebar.ResumeLayout(false);
            this.panelDB.ResumeLayout(false);
            this.panelSP.ResumeLayout(false);
            this.panelNCC.ResumeLayout(false);
            this.panelDH.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panelLO.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnHam;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.Button btnDB;
        private System.Windows.Forms.Panel panelDB;
        private System.Windows.Forms.Timer sidebarTransition;
        private System.Windows.Forms.Panel panelSP;
        private System.Windows.Forms.Button btnSP;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnacc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnns;
        private System.Windows.Forms.Button btnkh;
        private System.Windows.Forms.Button btnlu;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btntk;
        private System.Windows.Forms.Panel panelLO;
        private System.Windows.Forms.Panel panelNCC;
        private System.Windows.Forms.Button btnncc;
        private System.Windows.Forms.Panel panelDH;
        private System.Windows.Forms.Button btndh;
        private System.Windows.Forms.Label lbname;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btncn;
        private System.Windows.Forms.Button btndt;
        private System.Windows.Forms.Button btnkm;
        private System.Windows.Forms.Button button1;
    }
}

