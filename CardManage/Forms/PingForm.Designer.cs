namespace CardManage.Forms
{
    partial class PingForm : SetFormBase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PingForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeviceAddress = new System.Windows.Forms.TextBox();
            this.cbDeviceType = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxComunicateData = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbClearDebugContent = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveDebugContent = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyDebugContent = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(637, 22);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(149, 42);
            this.btnCancel.Text = "关闭(&Q)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(419, 22);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Size = new System.Drawing.Size(149, 42);
            this.btnOK.Text = "Ping(检测设备)";
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
            this.label1.Location = new System.Drawing.Point(16, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "请输入设备地址：";
            // 
            // txtDeviceAddress
            // 
            this.txtDeviceAddress.Location = new System.Drawing.Point(149, 31);
            this.txtDeviceAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtDeviceAddress.MaxLength = 8;
            this.txtDeviceAddress.Name = "txtDeviceAddress";
            this.txtDeviceAddress.Size = new System.Drawing.Size(121, 25);
            this.txtDeviceAddress.TabIndex = 10;
            this.txtDeviceAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskAddressText);
            // 
            // cbDeviceType
            // 
            this.cbDeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeviceType.FormattingEnabled = true;
            this.cbDeviceType.Location = new System.Drawing.Point(279, 31);
            this.cbDeviceType.Margin = new System.Windows.Forms.Padding(4);
            this.cbDeviceType.Name = "cbDeviceType";
            this.cbDeviceType.Size = new System.Drawing.Size(131, 23);
            this.cbDeviceType.TabIndex = 43;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxComunicateData);
            this.groupBox1.Controls.Add(this.toolStrip2);
            this.groupBox1.Location = new System.Drawing.Point(16, 80);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(951, 385);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ping消息";
            // 
            // tbxComunicateData
            // 
            this.tbxComunicateData.BackColor = System.Drawing.SystemColors.ControlText;
            this.tbxComunicateData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxComunicateData.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tbxComunicateData.Location = new System.Drawing.Point(4, 49);
            this.tbxComunicateData.Margin = new System.Windows.Forms.Padding(4);
            this.tbxComunicateData.Name = "tbxComunicateData";
            this.tbxComunicateData.ReadOnly = true;
            this.tbxComunicateData.Size = new System.Drawing.Size(943, 332);
            this.tbxComunicateData.TabIndex = 47;
            this.tbxComunicateData.Text = "";
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClearDebugContent,
            this.tsbSaveDebugContent,
            this.tsbCopyDebugContent});
            this.toolStrip2.Location = new System.Drawing.Point(4, 22);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip2.Size = new System.Drawing.Size(943, 27);
            this.toolStrip2.TabIndex = 46;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbClearDebugContent
            // 
            this.tsbClearDebugContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearDebugContent.Image")));
            this.tsbClearDebugContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearDebugContent.Name = "tsbClearDebugContent";
            this.tsbClearDebugContent.Size = new System.Drawing.Size(121, 24);
            this.tsbClearDebugContent.Text = "清空消息内容";
            this.tsbClearDebugContent.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbClearDebugContent.Click += new System.EventHandler(this.TsbClearDebugContent_Click);
            // 
            // tsbSaveDebugContent
            // 
            this.tsbSaveDebugContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveDebugContent.Image")));
            this.tsbSaveDebugContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveDebugContent.Name = "tsbSaveDebugContent";
            this.tsbSaveDebugContent.Size = new System.Drawing.Size(121, 24);
            this.tsbSaveDebugContent.Text = "保存消息内容";
            this.tsbSaveDebugContent.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbSaveDebugContent.Click += new System.EventHandler(this.TsbSaveDebugContent_Click);
            // 
            // tsbCopyDebugContent
            // 
            this.tsbCopyDebugContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyDebugContent.Image")));
            this.tsbCopyDebugContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyDebugContent.Name = "tsbCopyDebugContent";
            this.tsbCopyDebugContent.Size = new System.Drawing.Size(121, 24);
            this.tsbCopyDebugContent.Text = "复制消息内容";
            this.tsbCopyDebugContent.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbCopyDebugContent.Click += new System.EventHandler(this.TsbCopyDebugContent_Click);
            // 
            // PingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(983, 480);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbDeviceType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDeviceAddress);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MinimumSize = new System.Drawing.Size(1001, 525);
            this.Name = "PingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PingForm_FormClosing);
            this.Load += new System.EventHandler(this.PingForm_Load);
            this.Controls.SetChildIndex(this.txtDeviceAddress, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.cbDeviceType, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeviceAddress;
        private System.Windows.Forms.ComboBox cbDeviceType;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbClearDebugContent;
        private System.Windows.Forms.ToolStripButton tsbSaveDebugContent;
        private System.Windows.Forms.ToolStripButton tsbCopyDebugContent;
        private System.Windows.Forms.RichTextBox tbxComunicateData;
    }
}
