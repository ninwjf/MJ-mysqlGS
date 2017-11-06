namespace CardManage.Forms
{
    partial class SystemSetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemSetForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnUserLocalDB = new System.Windows.Forms.Button();
            this.btnCheckDBSetting = new System.Windows.Forms.Button();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.txtDBIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cbParity1 = new System.Windows.Forms.ComboBox();
            this.cbStopBits1 = new System.Windows.Forms.ComboBox();
            this.cbDataBits1 = new System.Windows.Forms.ComboBox();
            this.cbBaudRate1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbPortName1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPortName1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbParity2 = new System.Windows.Forms.ComboBox();
            this.cbStopBits2 = new System.Windows.Forms.ComboBox();
            this.cbDataBits2 = new System.Windows.Forms.ComboBox();
            this.cbBaudRate2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPortName2 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPortName2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSyncPL = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cBSyncAuto = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tBChs = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(307, 422);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(152, 422);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "House");
            this.imageList1.Images.SetKeyName(1, "shuaka");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(16, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(557, 396);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.btnUserLocalDB);
            this.tabPage1.Controls.Add(this.btnCheckDBSetting);
            this.tabPage1.Controls.Add(this.txtUserPassword);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtUserName);
            this.tabPage1.Controls.Add(this.txtDBName);
            this.tabPage1.Controls.Add(this.txtDBIP);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(549, 367);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据库连接设置";
            this.tabPage1.Controls.SetChildIndex(this.label2, 0);
            this.tabPage1.Controls.SetChildIndex(this.label3, 0);
            this.tabPage1.Controls.SetChildIndex(this.txtDBIP, 0);
            this.tabPage1.Controls.SetChildIndex(this.txtDBName, 0);
            this.tabPage1.Controls.SetChildIndex(this.txtUserName, 0);
            this.tabPage1.Controls.SetChildIndex(this.label4, 0);
            this.tabPage1.Controls.SetChildIndex(this.txtUserPassword, 0);
            this.tabPage1.Controls.SetChildIndex(this.btnCheckDBSetting, 0);
            this.tabPage1.Controls.SetChildIndex(this.btnUserLocalDB, 0);
            // 
            // btnUserLocalDB
            // 
            this.btnUserLocalDB.Location = new System.Drawing.Point(407, 45);
            this.btnUserLocalDB.Margin = new System.Windows.Forms.Padding(4);
            this.btnUserLocalDB.Name = "btnUserLocalDB";
            this.btnUserLocalDB.Size = new System.Drawing.Size(105, 29);
            this.btnUserLocalDB.TabIndex = 34;
            this.btnUserLocalDB.Text = "本机数据库";
            this.btnUserLocalDB.UseVisualStyleBackColor = true;
            this.btnUserLocalDB.Click += new System.EventHandler(this.BtnUserLocalDB_Click);
            // 
            // btnCheckDBSetting
            // 
            this.btnCheckDBSetting.Location = new System.Drawing.Point(211, 232);
            this.btnCheckDBSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckDBSetting.Name = "btnCheckDBSetting";
            this.btnCheckDBSetting.Size = new System.Drawing.Size(177, 29);
            this.btnCheckDBSetting.TabIndex = 33;
            this.btnCheckDBSetting.Text = "测试连接";
            this.btnCheckDBSetting.UseVisualStyleBackColor = true;
            this.btnCheckDBSetting.Click += new System.EventHandler(this.BtnCheckDBSetting_Click);
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Location = new System.Drawing.Point(211, 170);
            this.txtUserPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.PasswordChar = '*';
            this.txtUserPassword.Size = new System.Drawing.Size(193, 25);
            this.txtUserPassword.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 175);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 31;
            this.label4.Text = "密码：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(211, 129);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(193, 25);
            this.txtUserName.TabIndex = 30;
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(211, 88);
            this.txtDBName.Margin = new System.Windows.Forms.Padding(4);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(193, 25);
            this.txtDBName.TabIndex = 28;
            // 
            // txtDBIP
            // 
            this.txtDBIP.Location = new System.Drawing.Point(211, 46);
            this.txtDBIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtDBIP.Name = "txtDBIP";
            this.txtDBIP.Size = new System.Drawing.Size(193, 25);
            this.txtDBIP.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "数据库名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "数据库地址：";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.cbParity1);
            this.tabPage2.Controls.Add(this.cbStopBits1);
            this.tabPage2.Controls.Add(this.cbDataBits1);
            this.tabPage2.Controls.Add(this.cbBaudRate1);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.cbPortName1);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.txtPortName1);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(549, 367);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "制卡COM设置";
            // 
            // cbParity1
            // 
            this.cbParity1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity1.FormattingEnabled = true;
            this.cbParity1.Location = new System.Drawing.Point(200, 228);
            this.cbParity1.Margin = new System.Windows.Forms.Padding(4);
            this.cbParity1.Name = "cbParity1";
            this.cbParity1.Size = new System.Drawing.Size(101, 23);
            this.cbParity1.TabIndex = 47;
            // 
            // cbStopBits1
            // 
            this.cbStopBits1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits1.FormattingEnabled = true;
            this.cbStopBits1.Location = new System.Drawing.Point(200, 182);
            this.cbStopBits1.Margin = new System.Windows.Forms.Padding(4);
            this.cbStopBits1.Name = "cbStopBits1";
            this.cbStopBits1.Size = new System.Drawing.Size(101, 23);
            this.cbStopBits1.TabIndex = 46;
            // 
            // cbDataBits1
            // 
            this.cbDataBits1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits1.FormattingEnabled = true;
            this.cbDataBits1.Location = new System.Drawing.Point(200, 138);
            this.cbDataBits1.Margin = new System.Windows.Forms.Padding(4);
            this.cbDataBits1.Name = "cbDataBits1";
            this.cbDataBits1.Size = new System.Drawing.Size(101, 23);
            this.cbDataBits1.TabIndex = 45;
            // 
            // cbBaudRate1
            // 
            this.cbBaudRate1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate1.FormattingEnabled = true;
            this.cbBaudRate1.Location = new System.Drawing.Point(200, 92);
            this.cbBaudRate1.Margin = new System.Windows.Forms.Padding(4);
            this.cbBaudRate1.Name = "cbBaudRate1";
            this.cbBaudRate1.Size = new System.Drawing.Size(101, 23);
            this.cbBaudRate1.TabIndex = 44;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(127, 232);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 43;
            this.label10.Text = "校验位：";
            // 
            // cbPortName1
            // 
            this.cbPortName1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPortName1.FormattingEnabled = true;
            this.cbPortName1.Location = new System.Drawing.Point(305, 48);
            this.cbPortName1.Margin = new System.Windows.Forms.Padding(4);
            this.cbPortName1.Name = "cbPortName1";
            this.cbPortName1.Size = new System.Drawing.Size(120, 23);
            this.cbPortName1.TabIndex = 42;
            this.cbPortName1.SelectedIndexChanged += new System.EventHandler(this.CbPortName_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 188);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 39;
            this.label5.Text = "停止位：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(127, 142);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 37;
            this.label6.Text = "数据位：";
            // 
            // txtPortName1
            // 
            this.txtPortName1.Location = new System.Drawing.Point(200, 48);
            this.txtPortName1.Margin = new System.Windows.Forms.Padding(4);
            this.txtPortName1.Name = "txtPortName1";
            this.txtPortName1.Size = new System.Drawing.Size(101, 25);
            this.txtPortName1.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(127, 98);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 34;
            this.label7.Text = "波特率：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(127, 52);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 33;
            this.label8.Text = "串口名：";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.cbParity2);
            this.tabPage3.Controls.Add(this.cbStopBits2);
            this.tabPage3.Controls.Add(this.cbDataBits2);
            this.tabPage3.Controls.Add(this.cbBaudRate2);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.cbPortName2);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.txtPortName2);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(549, 367);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "监控COM设置";
            // 
            // cbParity2
            // 
            this.cbParity2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity2.FormattingEnabled = true;
            this.cbParity2.Location = new System.Drawing.Point(200, 228);
            this.cbParity2.Margin = new System.Windows.Forms.Padding(4);
            this.cbParity2.Name = "cbParity2";
            this.cbParity2.Size = new System.Drawing.Size(101, 23);
            this.cbParity2.TabIndex = 58;
            // 
            // cbStopBits2
            // 
            this.cbStopBits2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits2.FormattingEnabled = true;
            this.cbStopBits2.Location = new System.Drawing.Point(200, 182);
            this.cbStopBits2.Margin = new System.Windows.Forms.Padding(4);
            this.cbStopBits2.Name = "cbStopBits2";
            this.cbStopBits2.Size = new System.Drawing.Size(101, 23);
            this.cbStopBits2.TabIndex = 57;
            // 
            // cbDataBits2
            // 
            this.cbDataBits2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits2.FormattingEnabled = true;
            this.cbDataBits2.Location = new System.Drawing.Point(200, 138);
            this.cbDataBits2.Margin = new System.Windows.Forms.Padding(4);
            this.cbDataBits2.Name = "cbDataBits2";
            this.cbDataBits2.Size = new System.Drawing.Size(101, 23);
            this.cbDataBits2.TabIndex = 56;
            // 
            // cbBaudRate2
            // 
            this.cbBaudRate2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate2.FormattingEnabled = true;
            this.cbBaudRate2.Location = new System.Drawing.Point(200, 92);
            this.cbBaudRate2.Margin = new System.Windows.Forms.Padding(4);
            this.cbBaudRate2.Name = "cbBaudRate2";
            this.cbBaudRate2.Size = new System.Drawing.Size(101, 23);
            this.cbBaudRate2.TabIndex = 55;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(127, 232);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 54;
            this.label9.Text = "校验位：";
            // 
            // cbPortName2
            // 
            this.cbPortName2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPortName2.FormattingEnabled = true;
            this.cbPortName2.Location = new System.Drawing.Point(305, 48);
            this.cbPortName2.Margin = new System.Windows.Forms.Padding(4);
            this.cbPortName2.Name = "cbPortName2";
            this.cbPortName2.Size = new System.Drawing.Size(120, 23);
            this.cbPortName2.TabIndex = 53;
            this.cbPortName2.SelectedIndexChanged += new System.EventHandler(this.CbPortName_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(127, 188);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 15);
            this.label11.TabIndex = 52;
            this.label11.Text = "停止位：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(127, 142);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 15);
            this.label12.TabIndex = 51;
            this.label12.Text = "数据位：";
            // 
            // txtPortName2
            // 
            this.txtPortName2.Location = new System.Drawing.Point(200, 48);
            this.txtPortName2.Margin = new System.Windows.Forms.Padding(4);
            this.txtPortName2.Name = "txtPortName2";
            this.txtPortName2.Size = new System.Drawing.Size(101, 25);
            this.txtPortName2.TabIndex = 50;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(127, 98);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 15);
            this.label13.TabIndex = 49;
            this.label13.Text = "波特率：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(127, 52);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 15);
            this.label14.TabIndex = 48;
            this.label14.Text = "串口名：";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Transparent;
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.tBChs);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Controls.Add(this.cBSyncAuto);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.txtSyncPL);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(549, 367);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "其它";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(314, 94);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(146, 15);
            this.label16.TabIndex = 30;
            this.label16.Text = "分(范围：1-9999分)";
            // 
            // txtSyncPL
            // 
            this.txtSyncPL.Location = new System.Drawing.Point(216, 89);
            this.txtSyncPL.Margin = new System.Windows.Forms.Padding(4);
            this.txtSyncPL.MaxLength = 4;
            this.txtSyncPL.Name = "txtSyncPL";
            this.txtSyncPL.Size = new System.Drawing.Size(88, 25);
            this.txtSyncPL.TabIndex = 29;
            this.txtSyncPL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSyncPL_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(70, 94);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 15);
            this.label15.TabIndex = 28;
            this.label15.Text = "时间同步频率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 134);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "用户名：";
            // 
            // cBSyncAuto
            // 
            this.cBSyncAuto.AutoSize = true;
            this.cBSyncAuto.Location = new System.Drawing.Point(191, 94);
            this.cBSyncAuto.Name = "cBSyncAuto";
            this.cBSyncAuto.Size = new System.Drawing.Size(18, 17);
            this.cBSyncAuto.TabIndex = 31;
            this.cBSyncAuto.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(100, 210);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 15);
            this.label17.TabIndex = 32;
            this.label17.Text = "扇区选择：";
            // 
            // tBChs
            // 
            this.tBChs.Location = new System.Drawing.Point(216, 200);
            this.tBChs.Margin = new System.Windows.Forms.Padding(4);
            this.tBChs.MaxLength = 4;
            this.tBChs.Name = "tBChs";
            this.tBChs.Size = new System.Drawing.Size(88, 25);
            this.tBChs.TabIndex = 33;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(314, 203);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 15);
            this.label18.TabIndex = 34;
            this.label18.Text = "(范围：1-15)";
            // 
            // SystemSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 466);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MinimumSize = new System.Drawing.Size(611, 511);
            this.Name = "SystemSetForm";
            this.Text = "SystemSetForm";
            this.Load += new System.EventHandler(this.SystemSetForm_Load);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.TextBox txtDBIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCheckDBSetting;
        private System.Windows.Forms.ComboBox cbPortName1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPortName1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbBaudRate1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cbParity1;
        private System.Windows.Forms.ComboBox cbStopBits1;
        private System.Windows.Forms.ComboBox cbDataBits1;
        private System.Windows.Forms.ComboBox cbParity2;
        private System.Windows.Forms.ComboBox cbStopBits2;
        private System.Windows.Forms.ComboBox cbDataBits2;
        private System.Windows.Forms.ComboBox cbBaudRate2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPortName2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPortName2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtSyncPL;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnUserLocalDB;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tBChs;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox cBSyncAuto;
    }
}