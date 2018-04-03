namespace CardManage.Forms
{
    partial class CardViewForm : SetFormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardViewForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbEndDate = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.lbComStatus = new System.Windows.Forms.Label();
            this.cbCHS = new System.Windows.Forms.ComboBox();
            this.btnQuikChooseSerialNO = new System.Windows.Forms.Button();
            this.cbRoomCode = new System.Windows.Forms.ComboBox();
            this.cbFloorCode = new System.Windows.Forms.ComboBox();
            this.cbUnitCode = new System.Windows.Forms.ComboBox();
            this.cbBuildCode = new System.Windows.Forms.ComboBox();
            this.cbAreaCode = new System.Windows.Forms.ComboBox();
            this.lbComStatusDesc = new System.Windows.Forms.Label();
            this.txtBelongDesc = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnGetCodeFromBelong = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.cbCardType = new System.Windows.Forms.ComboBox();
            this.dtExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.txtRRoomCode = new System.Windows.Forms.TextBox();
            this.txtRFloorCode = new System.Windows.Forms.TextBox();
            this.txtRUnitCode = new System.Windows.Forms.TextBox();
            this.txtRBuildCode = new System.Windows.Forms.TextBox();
            this.txtRAreaCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbRangeCode = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnWriteCard = new System.Windows.Forms.Button();
            this.btnReadCard = new System.Windows.Forms.Button();
            this.gbDebg = new System.Windows.Forms.GroupBox();
            this.tbxComunicateData = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbClearDebugContent = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveDebugContent = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyDebugContent = new System.Windows.Forms.ToolStripButton();
            this.groupBox2.SuspendLayout();
            this.gbDebg.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(481, 327);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Size = new System.Drawing.Size(112, 33);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "关闭(&Q)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(685, 327);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Size = new System.Drawing.Size(49, 21);
            this.btnOK.TabIndex = 12;
            this.btnOK.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "House");
            this.imageList1.Images.SetKeyName(1, "shuaka");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbEndDate);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.lbComStatus);
            this.groupBox2.Controls.Add(this.cbCHS);
            this.groupBox2.Controls.Add(this.btnQuikChooseSerialNO);
            this.groupBox2.Controls.Add(this.cbRoomCode);
            this.groupBox2.Controls.Add(this.cbFloorCode);
            this.groupBox2.Controls.Add(this.cbUnitCode);
            this.groupBox2.Controls.Add(this.cbBuildCode);
            this.groupBox2.Controls.Add(this.cbAreaCode);
            this.groupBox2.Controls.Add(this.lbComStatusDesc);
            this.groupBox2.Controls.Add(this.txtBelongDesc);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtMemo);
            this.groupBox2.Controls.Add(this.txtTel);
            this.groupBox2.Controls.Add(this.txtContact);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.btnGetCodeFromBelong);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtCardNo);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtSerialNo);
            this.groupBox2.Controls.Add(this.cbCardType);
            this.groupBox2.Controls.Add(this.dtExpiryDate);
            this.groupBox2.Controls.Add(this.txtRRoomCode);
            this.groupBox2.Controls.Add(this.txtRFloorCode);
            this.groupBox2.Controls.Add(this.txtRUnitCode);
            this.groupBox2.Controls.Add(this.txtRBuildCode);
            this.groupBox2.Controls.Add(this.txtRAreaCode);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbRangeCode);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(11, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(761, 308);
            this.groupBox2.TabIndex = 98;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "卡片信息";
            // 
            // cbEndDate
            // 
            this.cbEndDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEndDate.FormattingEnabled = true;
            this.cbEndDate.Location = new System.Drawing.Point(518, 45);
            this.cbEndDate.Name = "cbEndDate";
            this.cbEndDate.Size = new System.Drawing.Size(76, 20);
            this.cbEndDate.TabIndex = 147;
            this.cbEndDate.SelectedIndexChanged += new System.EventHandler(this.CbCode_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(16, 48);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 12);
            this.label20.TabIndex = 145;
            this.label20.Text = "小区编码：";
            // 
            // lbComStatus
            // 
            this.lbComStatus.AutoSize = true;
            this.lbComStatus.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbComStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbComStatus.Location = new System.Drawing.Point(646, 69);
            this.lbComStatus.Name = "lbComStatus";
            this.lbComStatus.Size = new System.Drawing.Size(66, 18);
            this.lbComStatus.TabIndex = 140;
            this.lbComStatus.Text = "[关闭]";
            // 
            // cbCHS
            // 
            this.cbCHS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCHS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCHS.Items.AddRange(new object[] {
            "1"});
            this.cbCHS.Location = new System.Drawing.Point(364, 186);
            this.cbCHS.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbCHS.Name = "cbCHS";
            this.cbCHS.Size = new System.Drawing.Size(61, 20);
            this.cbCHS.TabIndex = 100;
            this.cbCHS.Visible = false;
            // 
            // btnQuikChooseSerialNO
            // 
            this.btnQuikChooseSerialNO.Location = new System.Drawing.Point(296, 182);
            this.btnQuikChooseSerialNO.Name = "btnQuikChooseSerialNO";
            this.btnQuikChooseSerialNO.Size = new System.Drawing.Size(58, 23);
            this.btnQuikChooseSerialNO.TabIndex = 139;
            this.btnQuikChooseSerialNO.Text = "快速选择";
            this.btnQuikChooseSerialNO.UseVisualStyleBackColor = true;
            this.btnQuikChooseSerialNO.Visible = false;
            this.btnQuikChooseSerialNO.Click += new System.EventHandler(this.BtnQuikChooseSerialNO_Click);
            // 
            // cbRoomCode
            // 
            this.cbRoomCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRoomCode.FormattingEnabled = true;
            this.cbRoomCode.Location = new System.Drawing.Point(176, 153);
            this.cbRoomCode.Name = "cbRoomCode";
            this.cbRoomCode.Size = new System.Drawing.Size(88, 20);
            this.cbRoomCode.TabIndex = 138;
            this.cbRoomCode.Visible = false;
            this.cbRoomCode.SelectedIndexChanged += new System.EventHandler(this.CbCode_SelectedIndexChanged);
            // 
            // cbFloorCode
            // 
            this.cbFloorCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFloorCode.FormattingEnabled = true;
            this.cbFloorCode.Location = new System.Drawing.Point(176, 126);
            this.cbFloorCode.Name = "cbFloorCode";
            this.cbFloorCode.Size = new System.Drawing.Size(88, 20);
            this.cbFloorCode.TabIndex = 137;
            this.cbFloorCode.SelectedIndexChanged += new System.EventHandler(this.CbCode_SelectedIndexChanged);
            // 
            // cbUnitCode
            // 
            this.cbUnitCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnitCode.FormattingEnabled = true;
            this.cbUnitCode.Location = new System.Drawing.Point(176, 99);
            this.cbUnitCode.Name = "cbUnitCode";
            this.cbUnitCode.Size = new System.Drawing.Size(88, 20);
            this.cbUnitCode.TabIndex = 136;
            this.cbUnitCode.SelectedIndexChanged += new System.EventHandler(this.CbCode_SelectedIndexChanged);
            // 
            // cbBuildCode
            // 
            this.cbBuildCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBuildCode.FormattingEnabled = true;
            this.cbBuildCode.Location = new System.Drawing.Point(176, 72);
            this.cbBuildCode.Name = "cbBuildCode";
            this.cbBuildCode.Size = new System.Drawing.Size(88, 20);
            this.cbBuildCode.TabIndex = 135;
            this.cbBuildCode.SelectedIndexChanged += new System.EventHandler(this.CbCode_SelectedIndexChanged);
            // 
            // cbAreaCode
            // 
            this.cbAreaCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAreaCode.FormattingEnabled = true;
            this.cbAreaCode.Location = new System.Drawing.Point(176, 45);
            this.cbAreaCode.Name = "cbAreaCode";
            this.cbAreaCode.Size = new System.Drawing.Size(88, 20);
            this.cbAreaCode.TabIndex = 134;
            this.cbAreaCode.SelectedIndexChanged += new System.EventHandler(this.CbCode_SelectedIndexChanged);
            // 
            // lbComStatusDesc
            // 
            this.lbComStatusDesc.AutoSize = true;
            this.lbComStatusDesc.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbComStatusDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbComStatusDesc.Location = new System.Drawing.Point(604, 43);
            this.lbComStatusDesc.Name = "lbComStatusDesc";
            this.lbComStatusDesc.Size = new System.Drawing.Size(160, 18);
            this.lbComStatusDesc.TabIndex = 133;
            this.lbComStatusDesc.Text = "制卡串口当前状态";
            // 
            // txtBelongDesc
            // 
            this.txtBelongDesc.Location = new System.Drawing.Point(82, 247);
            this.txtBelongDesc.Name = "txtBelongDesc";
            this.txtBelongDesc.ReadOnly = true;
            this.txtBelongDesc.Size = new System.Drawing.Size(666, 21);
            this.txtBelongDesc.TabIndex = 132;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(17, 250);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 131;
            this.label19.Text = "卡片归属：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(29, 278);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(689, 12);
            this.label13.TabIndex = 130;
            this.label13.Text = "(备注：卡片会根据建筑编码和卡片类型自动归纳到相应小区或房间，如果无小区或房间数据，则该卡片会被归纳到未归属分类！)";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(433, 130);
            this.txtMemo.MaxLength = 200;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(313, 105);
            this.txtMemo.TabIndex = 11;
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(433, 98);
            this.txtTel.MaxLength = 100;
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(162, 21);
            this.txtTel.TabIndex = 10;
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(433, 71);
            this.txtContact.MaxLength = 50;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(162, 21);
            this.txtContact.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(387, 130);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 126;
            this.label18.Text = "备注：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(363, 102);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 125;
            this.label17.Text = "联系电话：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(351, 75);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 124;
            this.label16.Text = "持卡者姓名：";
            // 
            // btnGetCodeFromBelong
            // 
            this.btnGetCodeFromBelong.Location = new System.Drawing.Point(176, 17);
            this.btnGetCodeFromBelong.Name = "btnGetCodeFromBelong";
            this.btnGetCodeFromBelong.Size = new System.Drawing.Size(148, 23);
            this.btnGetCodeFromBelong.TabIndex = 123;
            this.btnGetCodeFromBelong.Text = "从归属取得编码";
            this.btnGetCodeFromBelong.UseVisualStyleBackColor = true;
            this.btnGetCodeFromBelong.Visible = false;
            this.btnGetCodeFromBelong.Click += new System.EventHandler(this.BtnGetCodeFromBelong_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(86, 207);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(131, 12);
            this.label15.TabIndex = 122;
            this.label15.Text = "(长度：32位 数字:0-9)";
            // 
            // txtCardNo
            // 
            this.txtCardNo.Location = new System.Drawing.Point(88, 18);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.ReadOnly = true;
            this.txtCardNo.Size = new System.Drawing.Size(80, 21);
            this.txtCardNo.TabIndex = 120;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 119;
            this.label14.Text = "卡片号码：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(261, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 117;
            this.label12.Text = "(范围：0~99)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(261, 130);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 116;
            this.label11.Text = "(范围：0~99)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(261, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 115;
            this.label10.Text = "(范围：0~99)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(261, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 114;
            this.label9.Text = "(范围：0~99)";
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Enabled = false;
            this.txtSerialNo.Location = new System.Drawing.Point(88, 183);
            this.txtSerialNo.MaxLength = 32;
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(208, 21);
            this.txtSerialNo.TabIndex = 6;
            this.txtSerialNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskAddressText);
            // 
            // cbCardType
            // 
            this.cbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCardType.FormattingEnabled = true;
            this.cbCardType.Location = new System.Drawing.Point(433, 18);
            this.cbCardType.Name = "cbCardType";
            this.cbCardType.Size = new System.Drawing.Size(162, 20);
            this.cbCardType.TabIndex = 7;
            // 
            // dtExpiryDate
            // 
            this.dtExpiryDate.CustomFormat = "yyyy-MM-dd";
            this.dtExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtExpiryDate.Location = new System.Drawing.Point(433, 45);
            this.dtExpiryDate.MaxDate = new System.DateTime(2099, 12, 31, 23, 59, 59, 999);
            this.dtExpiryDate.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dtExpiryDate.Name = "dtExpiryDate";
            this.dtExpiryDate.Size = new System.Drawing.Size(80, 21);
            this.dtExpiryDate.TabIndex = 8;
            this.dtExpiryDate.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            // 
            // txtRRoomCode
            // 
            this.txtRRoomCode.Location = new System.Drawing.Point(88, 153);
            this.txtRRoomCode.MaxLength = 2;
            this.txtRRoomCode.Name = "txtRRoomCode";
            this.txtRRoomCode.Size = new System.Drawing.Size(80, 21);
            this.txtRRoomCode.TabIndex = 5;
            this.txtRRoomCode.TextChanged += new System.EventHandler(this.TbCode_TextChanged);
            this.txtRRoomCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskCodeText);
            // 
            // txtRFloorCode
            // 
            this.txtRFloorCode.Location = new System.Drawing.Point(88, 126);
            this.txtRFloorCode.MaxLength = 2;
            this.txtRFloorCode.Name = "txtRFloorCode";
            this.txtRFloorCode.Size = new System.Drawing.Size(80, 21);
            this.txtRFloorCode.TabIndex = 4;
            this.txtRFloorCode.TextChanged += new System.EventHandler(this.TbCode_TextChanged);
            this.txtRFloorCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskCodeText);
            // 
            // txtRUnitCode
            // 
            this.txtRUnitCode.Location = new System.Drawing.Point(88, 99);
            this.txtRUnitCode.MaxLength = 2;
            this.txtRUnitCode.Name = "txtRUnitCode";
            this.txtRUnitCode.Size = new System.Drawing.Size(80, 21);
            this.txtRUnitCode.TabIndex = 3;
            this.txtRUnitCode.TextChanged += new System.EventHandler(this.TbCode_TextChanged);
            this.txtRUnitCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskCodeText);
            // 
            // txtRBuildCode
            // 
            this.txtRBuildCode.Location = new System.Drawing.Point(88, 72);
            this.txtRBuildCode.MaxLength = 2;
            this.txtRBuildCode.Name = "txtRBuildCode";
            this.txtRBuildCode.Size = new System.Drawing.Size(80, 21);
            this.txtRBuildCode.TabIndex = 2;
            this.txtRBuildCode.TextChanged += new System.EventHandler(this.TbCode_TextChanged);
            this.txtRBuildCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskCodeText);
            // 
            // txtRAreaCode
            // 
            this.txtRAreaCode.Location = new System.Drawing.Point(88, 45);
            this.txtRAreaCode.MaxLength = 2;
            this.txtRAreaCode.Name = "txtRAreaCode";
            this.txtRAreaCode.Size = new System.Drawing.Size(80, 21);
            this.txtRAreaCode.TabIndex = 1;
            this.txtRAreaCode.TextChanged += new System.EventHandler(this.TbCode_TextChanged);
            this.txtRAreaCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskCodeText);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 105;
            this.label8.Text = "卡片系列号：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(363, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 104;
            this.label7.Text = "卡片类型：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(351, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 103;
            this.label6.Text = "卡片有效期：";
            // 
            // lbRangeCode
            // 
            this.lbRangeCode.AutoSize = true;
            this.lbRangeCode.Location = new System.Drawing.Point(261, 49);
            this.lbRangeCode.Name = "lbRangeCode";
            this.lbRangeCode.Size = new System.Drawing.Size(77, 12);
            this.lbRangeCode.TabIndex = 102;
            this.lbRangeCode.Text = "(范围：0~99)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 101;
            this.label5.Text = "房间编码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 100;
            this.label4.Text = "楼层编码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 99;
            this.label3.Text = "楼栋编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 98;
            this.label2.Text = "单元编码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 97;
            this.label1.Text = "小区编码：";
            // 
            // btnWriteCard
            // 
            this.btnWriteCard.Location = new System.Drawing.Point(307, 328);
            this.btnWriteCard.Name = "btnWriteCard";
            this.btnWriteCard.Size = new System.Drawing.Size(112, 33);
            this.btnWriteCard.TabIndex = 15;
            this.btnWriteCard.Text = "写卡(&W)";
            this.btnWriteCard.UseVisualStyleBackColor = true;
            this.btnWriteCard.Click += new System.EventHandler(this.BtnWriteCard_Click);
            // 
            // btnReadCard
            // 
            this.btnReadCard.Location = new System.Drawing.Point(134, 328);
            this.btnReadCard.Name = "btnReadCard";
            this.btnReadCard.Size = new System.Drawing.Size(112, 33);
            this.btnReadCard.TabIndex = 14;
            this.btnReadCard.Text = "读卡(&R)";
            this.btnReadCard.UseVisualStyleBackColor = true;
            this.btnReadCard.Click += new System.EventHandler(this.BtnReadCard_Click);
            // 
            // gbDebg
            // 
            this.gbDebg.Controls.Add(this.tbxComunicateData);
            this.gbDebg.Controls.Add(this.toolStrip2);
            this.gbDebg.Location = new System.Drawing.Point(10, 367);
            this.gbDebg.Name = "gbDebg";
            this.gbDebg.Size = new System.Drawing.Size(761, 172);
            this.gbDebg.TabIndex = 99;
            this.gbDebg.TabStop = false;
            this.gbDebg.Text = "调试窗口";
            // 
            // tbxComunicateData
            // 
            this.tbxComunicateData.BackColor = System.Drawing.SystemColors.ControlText;
            this.tbxComunicateData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxComunicateData.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tbxComunicateData.Location = new System.Drawing.Point(3, 42);
            this.tbxComunicateData.Name = "tbxComunicateData";
            this.tbxComunicateData.ReadOnly = true;
            this.tbxComunicateData.Size = new System.Drawing.Size(755, 127);
            this.tbxComunicateData.TabIndex = 9;
            this.tbxComunicateData.Text = "";
            this.tbxComunicateData.TextChanged += new System.EventHandler(this.TbxComunicateData_TextChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClearDebugContent,
            this.tsbSaveDebugContent,
            this.tsbCopyDebugContent});
            this.toolStrip2.Location = new System.Drawing.Point(3, 17);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip2.Size = new System.Drawing.Size(755, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbClearDebugContent
            // 
            this.tsbClearDebugContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearDebugContent.Image")));
            this.tsbClearDebugContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearDebugContent.Name = "tsbClearDebugContent";
            this.tsbClearDebugContent.Size = new System.Drawing.Size(102, 22);
            this.tsbClearDebugContent.Text = "清空调试内容";
            this.tsbClearDebugContent.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbClearDebugContent.Click += new System.EventHandler(this.TsbClearDebugContent_Click);
            // 
            // tsbSaveDebugContent
            // 
            this.tsbSaveDebugContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveDebugContent.Image")));
            this.tsbSaveDebugContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveDebugContent.Name = "tsbSaveDebugContent";
            this.tsbSaveDebugContent.Size = new System.Drawing.Size(102, 22);
            this.tsbSaveDebugContent.Text = "保存调试内容";
            this.tsbSaveDebugContent.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbSaveDebugContent.Click += new System.EventHandler(this.TsbSaveDebugContent_Click);
            // 
            // tsbCopyDebugContent
            // 
            this.tsbCopyDebugContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyDebugContent.Image")));
            this.tsbCopyDebugContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyDebugContent.Name = "tsbCopyDebugContent";
            this.tsbCopyDebugContent.Size = new System.Drawing.Size(102, 22);
            this.tsbCopyDebugContent.Text = "复制调试内容";
            this.tsbCopyDebugContent.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbCopyDebugContent.Click += new System.EventHandler(this.TsbCopyDebugContent_Click);
            // 
            // CardViewForm
            // 
            this.AcceptButton = this.btnReadCard;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(790, 514);
            this.Controls.Add(this.gbDebg);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnWriteCard);
            this.Controls.Add(this.btnReadCard);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximumSize = new System.Drawing.Size(806, 1608);
            this.MinimumSize = new System.Drawing.Size(806, 417);
            this.Name = "CardViewForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardViewForm_FormClosing);
            this.Load += new System.EventHandler(this.CardViewForm_Load);
            this.Controls.SetChildIndex(this.btnReadCard, 0);
            this.Controls.SetChildIndex(this.btnWriteCard, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.gbDebg, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbDebg.ResumeLayout(false);
            this.gbDebg.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnWriteCard;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnReadCard;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.ComboBox cbCardType;
        private System.Windows.Forms.DateTimePicker dtExpiryDate;
        private System.Windows.Forms.TextBox txtRRoomCode;
        private System.Windows.Forms.TextBox txtRFloorCode;
        private System.Windows.Forms.TextBox txtRUnitCode;
        private System.Windows.Forms.TextBox txtRBuildCode;
        private System.Windows.Forms.TextBox txtRAreaCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbRangeCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetCodeFromBelong;
        private System.Windows.Forms.GroupBox gbDebg;
        private System.Windows.Forms.RichTextBox tbxComunicateData;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbClearDebugContent;
        private System.Windows.Forms.ToolStripButton tsbSaveDebugContent;
        private System.Windows.Forms.ToolStripButton tsbCopyDebugContent;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtBelongDesc;
        private System.Windows.Forms.Label lbComStatusDesc;
        private System.Windows.Forms.ComboBox cbAreaCode;
        private System.Windows.Forms.ComboBox cbRoomCode;
        private System.Windows.Forms.ComboBox cbFloorCode;
        private System.Windows.Forms.ComboBox cbUnitCode;
        private System.Windows.Forms.ComboBox cbBuildCode;
        private System.Windows.Forms.Button btnQuikChooseSerialNO;
        private System.Windows.Forms.Label lbComStatus;
        private System.Windows.Forms.ComboBox cbCHS;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbEndDate;
    }
}
