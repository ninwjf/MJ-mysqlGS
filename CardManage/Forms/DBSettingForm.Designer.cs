namespace CardManage.Forms
{
    partial class DBSettingForm : CardManage.Forms.SetFormBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBSettingForm));
            this.btnCheckDBSetting = new System.Windows.Forms.Button();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.txtDBIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUserLocalDB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(275, 309);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(120, 309);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "House");
            this.imageList1.Images.SetKeyName(1, "shuaka");
            // 
            // btnCheckDBSetting
            // 
            this.btnCheckDBSetting.Location = new System.Drawing.Point(176, 222);
            this.btnCheckDBSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckDBSetting.Name = "btnCheckDBSetting";
            this.btnCheckDBSetting.Size = new System.Drawing.Size(177, 29);
            this.btnCheckDBSetting.TabIndex = 42;
            this.btnCheckDBSetting.Text = "测试连接";
            this.btnCheckDBSetting.UseVisualStyleBackColor = true;
            this.btnCheckDBSetting.Click += new System.EventHandler(this.BtnCheckDBSetting_Click);
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Location = new System.Drawing.Point(176, 160);
            this.txtUserPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.PasswordChar = '*';
            this.txtUserPassword.Size = new System.Drawing.Size(193, 25);
            this.txtUserPassword.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(120, 165);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 40;
            this.label4.Text = "密码：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(176, 119);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(193, 25);
            this.txtUserName.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 124);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "用户名：";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(176, 78);
            this.txtDBName.Margin = new System.Windows.Forms.Padding(4);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(193, 25);
            this.txtDBName.TabIndex = 37;
            // 
            // txtDBIP
            // 
            this.txtDBIP.Location = new System.Drawing.Point(176, 36);
            this.txtDBIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtDBIP.Name = "txtDBIP";
            this.txtDBIP.Size = new System.Drawing.Size(193, 25);
            this.txtDBIP.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 35;
            this.label3.Text = "数据库名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 34;
            this.label2.Text = "数据库地址：";
            // 
            // btnUserLocalDB
            // 
            this.btnUserLocalDB.Location = new System.Drawing.Point(367, 35);
            this.btnUserLocalDB.Margin = new System.Windows.Forms.Padding(4);
            this.btnUserLocalDB.Name = "btnUserLocalDB";
            this.btnUserLocalDB.Size = new System.Drawing.Size(105, 29);
            this.btnUserLocalDB.TabIndex = 43;
            this.btnUserLocalDB.Text = "本机数据库";
            this.btnUserLocalDB.UseVisualStyleBackColor = true;
            this.btnUserLocalDB.Click += new System.EventHandler(this.BtnUserLocalDB_Click);
            // 
            // DBSettingForm
            // 
            this.AcceptButton = this.btnUserLocalDB;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(488, 376);
            this.Controls.Add(this.btnUserLocalDB);
            this.Controls.Add(this.btnCheckDBSetting);
            this.Controls.Add(this.txtUserPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.txtDBIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MinimumSize = new System.Drawing.Size(506, 421);
            this.Name = "DBSettingForm";
            this.Load += new System.EventHandler(this.DBSettingForm_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtDBIP, 0);
            this.Controls.SetChildIndex(this.txtDBName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtUserPassword, 0);
            this.Controls.SetChildIndex(this.btnCheckDBSetting, 0);
            this.Controls.SetChildIndex(this.btnUserLocalDB, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckDBSetting;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.TextBox txtDBIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUserLocalDB;
    }
}
