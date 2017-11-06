namespace CardManage.Forms
{
    partial class ComManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComManageForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lbWriteComStauts = new System.Windows.Forms.Label();
            this.btnWriteComOpenOrClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbMonitorComStauts = new System.Windows.Forms.Label();
            this.btnMonitorComOpenOrClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 170);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Text = "关闭(&Q)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(149, 170);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Text = "串口设置(&S)";
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
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(21, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "制卡串口配置与状态：";
            // 
            // lbWriteComStauts
            // 
            this.lbWriteComStauts.AutoSize = true;
            this.lbWriteComStauts.Location = new System.Drawing.Point(191, 44);
            this.lbWriteComStauts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbWriteComStauts.Name = "lbWriteComStauts";
            this.lbWriteComStauts.Size = new System.Drawing.Size(37, 15);
            this.lbWriteComStauts.TabIndex = 10;
            this.lbWriteComStauts.Text = "关闭";
            // 
            // btnWriteComOpenOrClose
            // 
            this.btnWriteComOpenOrClose.Location = new System.Drawing.Point(488, 38);
            this.btnWriteComOpenOrClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnWriteComOpenOrClose.Name = "btnWriteComOpenOrClose";
            this.btnWriteComOpenOrClose.Size = new System.Drawing.Size(100, 29);
            this.btnWriteComOpenOrClose.TabIndex = 11;
            this.btnWriteComOpenOrClose.Text = "开启(&O)";
            this.btnWriteComOpenOrClose.UseVisualStyleBackColor = true;
            this.btnWriteComOpenOrClose.Click += new System.EventHandler(this.BtnWriteComOpenOrClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(21, 95);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "监控串口配置与状态：";
            // 
            // lbMonitorComStauts
            // 
            this.lbMonitorComStauts.AutoSize = true;
            this.lbMonitorComStauts.Location = new System.Drawing.Point(191, 95);
            this.lbMonitorComStauts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbMonitorComStauts.Name = "lbMonitorComStauts";
            this.lbMonitorComStauts.Size = new System.Drawing.Size(37, 15);
            this.lbMonitorComStauts.TabIndex = 13;
            this.lbMonitorComStauts.Text = "关闭";
            // 
            // btnMonitorComOpenOrClose
            // 
            this.btnMonitorComOpenOrClose.Location = new System.Drawing.Point(488, 89);
            this.btnMonitorComOpenOrClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnMonitorComOpenOrClose.Name = "btnMonitorComOpenOrClose";
            this.btnMonitorComOpenOrClose.Size = new System.Drawing.Size(100, 29);
            this.btnMonitorComOpenOrClose.TabIndex = 14;
            this.btnMonitorComOpenOrClose.Text = "开启(&O)";
            this.btnMonitorComOpenOrClose.UseVisualStyleBackColor = true;
            this.btnMonitorComOpenOrClose.Click += new System.EventHandler(this.BtnMonitorComOpenOrClose_Click);
            // 
            // ComManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(604, 230);
            this.Controls.Add(this.btnMonitorComOpenOrClose);
            this.Controls.Add(this.lbMonitorComStauts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnWriteComOpenOrClose);
            this.Controls.Add(this.lbWriteComStauts);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MinimumSize = new System.Drawing.Size(622, 275);
            this.Name = "ComManageForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComManageForm_FormClosing);
            this.Load += new System.EventHandler(this.ComManageForm_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.lbWriteComStauts, 0);
            this.Controls.SetChildIndex(this.btnWriteComOpenOrClose, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lbMonitorComStauts, 0);
            this.Controls.SetChildIndex(this.btnMonitorComOpenOrClose, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbWriteComStauts;
        private System.Windows.Forms.Button btnWriteComOpenOrClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbMonitorComStauts;
        private System.Windows.Forms.Button btnMonitorComOpenOrClose;
    }
}
