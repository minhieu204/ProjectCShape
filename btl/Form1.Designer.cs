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
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.panelDB = new System.Windows.Forms.Panel();
            this.panelSP = new System.Windows.Forms.Panel();
            this.panelLO = new System.Windows.Forms.Panel();
            this.sidebarTransition = new System.Windows.Forms.Timer(this.components);
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.btnDB = new System.Windows.Forms.Button();
            this.btnSP = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnHam = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.sidebar.SuspendLayout();
            this.panelDB.SuspendLayout();
            this.panelSP.SuspendLayout();
            this.panelLO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(33)))), ((int)(((byte)(89)))));
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
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(167)))));
            this.sidebar.Controls.Add(this.panelDB);
            this.sidebar.Controls.Add(this.panelSP);
            this.sidebar.Controls.Add(this.panelLO);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebar.Location = new System.Drawing.Point(0, 63);
            this.sidebar.Name = "sidebar";
            this.sidebar.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.sidebar.Size = new System.Drawing.Size(260, 658);
            this.sidebar.TabIndex = 3;
            // 
            // panelDB
            // 
            this.panelDB.Controls.Add(this.btnDB);
            this.panelDB.Location = new System.Drawing.Point(3, 33);
            this.panelDB.Name = "panelDB";
            this.panelDB.Size = new System.Drawing.Size(257, 40);
            this.panelDB.TabIndex = 5;
            // 
            // panelSP
            // 
            this.panelSP.Controls.Add(this.btnSP);
            this.panelSP.Location = new System.Drawing.Point(3, 79);
            this.panelSP.Name = "panelSP";
            this.panelSP.Size = new System.Drawing.Size(257, 40);
            this.panelSP.TabIndex = 7;
            // 
            // panelLO
            // 
            this.panelLO.Controls.Add(this.button2);
            this.panelLO.Location = new System.Drawing.Point(3, 125);
            this.panelLO.Name = "panelLO";
            this.panelLO.Size = new System.Drawing.Size(257, 40);
            this.panelLO.TabIndex = 6;
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
            // btnDB
            // 
            this.btnDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(167)))));
            this.btnDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDB.FlatAppearance.BorderSize = 0;
            this.btnDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDB.ForeColor = System.Drawing.Color.White;
            this.btnDB.Image = ((System.Drawing.Image)(resources.GetObject("btnDB.Image")));
            this.btnDB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDB.Location = new System.Drawing.Point(0, 0);
            this.btnDB.Margin = new System.Windows.Forms.Padding(0);
            this.btnDB.Name = "btnDB";
            this.btnDB.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnDB.Size = new System.Drawing.Size(257, 40);
            this.btnDB.TabIndex = 4;
            this.btnDB.TabStop = false;
            this.btnDB.Text = "         Dashboard";
            this.btnDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDB.UseVisualStyleBackColor = false;
            this.btnDB.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSP
            // 
            this.btnSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(167)))));
            this.btnSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSP.FlatAppearance.BorderSize = 0;
            this.btnSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSP.ForeColor = System.Drawing.Color.White;
            this.btnSP.Image = ((System.Drawing.Image)(resources.GetObject("btnSP.Image")));
            this.btnSP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSP.Location = new System.Drawing.Point(0, 0);
            this.btnSP.Margin = new System.Windows.Forms.Padding(0);
            this.btnSP.Name = "btnSP";
            this.btnSP.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSP.Size = new System.Drawing.Size(257, 40);
            this.btnSP.TabIndex = 4;
            this.btnSP.TabStop = false;
            this.btnSP.Text = "         Sản phẩm";
            this.btnSP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSP.UseVisualStyleBackColor = false;
            this.btnSP.Click += new System.EventHandler(this.btnSP_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(167)))));
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(257, 40);
            this.button2.TabIndex = 4;
            this.button2.TabStop = false;
            this.button2.Text = "         Logout";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
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
            this.sidebar.ResumeLayout(false);
            this.panelDB.ResumeLayout(false);
            this.panelSP.ResumeLayout(false);
            this.panelLO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.Panel panelLO;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer sidebarTransition;
        private System.Windows.Forms.Panel panelSP;
        private System.Windows.Forms.Button btnSP;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

