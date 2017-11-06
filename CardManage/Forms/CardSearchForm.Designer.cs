namespace CardManage.Forms
{
    partial class CardSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardSearchForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbDeviceNo = new System.Windows.Forms.Label();
            this.lbDeviceType = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.cbCardType = new System.Windows.Forms.ComboBox();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.cbCardValid = new System.Windows.Forms.ComboBox();
            this.cbDeviceType = new System.Windows.Forms.ComboBox();
            this.txtDeviceNo = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(201, 304);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(65, 304);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Text = "开始搜索(&S)";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "House");
            this.imageList1.Images.SetKeyName(1, "shuaka");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "卡号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "卡片类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 141);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "卡片系列号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 64);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "卡片有效性：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 180);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "持卡者姓名：";
            // 
            // lbDeviceNo
            // 
            this.lbDeviceNo.AutoSize = true;
            this.lbDeviceNo.Location = new System.Drawing.Point(80, 258);
            this.lbDeviceNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDeviceNo.Name = "lbDeviceNo";
            this.lbDeviceNo.Size = new System.Drawing.Size(67, 15);
            this.lbDeviceNo.TabIndex = 16;
            this.lbDeviceNo.Text = "设备号：";
            // 
            // lbDeviceType
            // 
            this.lbDeviceType.AutoSize = true;
            this.lbDeviceType.Location = new System.Drawing.Point(64, 219);
            this.lbDeviceType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDeviceType.Name = "lbDeviceType";
            this.lbDeviceType.Size = new System.Drawing.Size(82, 15);
            this.lbDeviceType.TabIndex = 17;
            this.lbDeviceType.Text = "设备类型：";
            // 
            // txtCardNo
            // 
            this.txtCardNo.Location = new System.Drawing.Point(159, 98);
            this.txtCardNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(251, 25);
            this.txtCardNo.TabIndex = 18;
            // 
            // cbCardType
            // 
            this.cbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCardType.FormattingEnabled = true;
            this.cbCardType.Location = new System.Drawing.Point(159, 20);
            this.cbCardType.Margin = new System.Windows.Forms.Padding(4);
            this.cbCardType.Name = "cbCardType";
            this.cbCardType.Size = new System.Drawing.Size(251, 23);
            this.cbCardType.TabIndex = 19;
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(159, 136);
            this.txtSerialNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(251, 25);
            this.txtSerialNo.TabIndex = 20;
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(159, 175);
            this.txtContact.Margin = new System.Windows.Forms.Padding(4);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(251, 25);
            this.txtContact.TabIndex = 22;
            // 
            // cbCardValid
            // 
            this.cbCardValid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCardValid.FormattingEnabled = true;
            this.cbCardValid.Location = new System.Drawing.Point(159, 59);
            this.cbCardValid.Margin = new System.Windows.Forms.Padding(4);
            this.cbCardValid.Name = "cbCardValid";
            this.cbCardValid.Size = new System.Drawing.Size(251, 23);
            this.cbCardValid.TabIndex = 23;
            // 
            // cbDeviceType
            // 
            this.cbDeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeviceType.FormattingEnabled = true;
            this.cbDeviceType.Location = new System.Drawing.Point(159, 214);
            this.cbDeviceType.Margin = new System.Windows.Forms.Padding(4);
            this.cbDeviceType.Name = "cbDeviceType";
            this.cbDeviceType.Size = new System.Drawing.Size(251, 23);
            this.cbDeviceType.TabIndex = 24;
            // 
            // txtDeviceNo
            // 
            this.txtDeviceNo.Location = new System.Drawing.Point(159, 252);
            this.txtDeviceNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtDeviceNo.Name = "txtDeviceNo";
            this.txtDeviceNo.Size = new System.Drawing.Size(251, 25);
            this.txtDeviceNo.TabIndex = 25;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(337, 304);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(123, 29);
            this.btnReset.TabIndex = 26;
            this.btnReset.Text = "重置(&R)";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // CardSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(477, 348);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtDeviceNo);
            this.Controls.Add(this.cbDeviceType);
            this.Controls.Add(this.cbCardValid);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.txtSerialNo);
            this.Controls.Add(this.cbCardType);
            this.Controls.Add(this.txtCardNo);
            this.Controls.Add(this.lbDeviceType);
            this.Controls.Add(this.lbDeviceNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MinimumSize = new System.Drawing.Size(495, 393);
            this.Name = "CardSearchForm";
            this.Load += new System.EventHandler(this.CardSearchForm_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.lbDeviceNo, 0);
            this.Controls.SetChildIndex(this.lbDeviceType, 0);
            this.Controls.SetChildIndex(this.txtCardNo, 0);
            this.Controls.SetChildIndex(this.cbCardType, 0);
            this.Controls.SetChildIndex(this.txtSerialNo, 0);
            this.Controls.SetChildIndex(this.txtContact, 0);
            this.Controls.SetChildIndex(this.cbCardValid, 0);
            this.Controls.SetChildIndex(this.cbDeviceType, 0);
            this.Controls.SetChildIndex(this.txtDeviceNo, 0);
            this.Controls.SetChildIndex(this.btnReset, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbDeviceNo;
        private System.Windows.Forms.Label lbDeviceType;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.ComboBox cbCardType;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.ComboBox cbCardValid;
        private System.Windows.Forms.ComboBox cbDeviceType;
        private System.Windows.Forms.TextBox txtDeviceNo;
        private System.Windows.Forms.Button btnReset;
    }
}
