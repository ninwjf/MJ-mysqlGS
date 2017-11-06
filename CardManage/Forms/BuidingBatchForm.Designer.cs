namespace CardManage.Forms
{
    partial class BuidingBatchForm : SetFormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuidingBatchForm));
            this.cbBuild = new System.Windows.Forms.ComboBox();
            this.lbBuild = new System.Windows.Forms.Label();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.lbArea = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRoomCodeBegin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRoomCodeEnd = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxComunicateData = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbUnit = new System.Windows.Forms.ComboBox();
            this.lbUnit = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(591, 85);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(177, 48);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "关闭(&Q)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(591, 20);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Size = new System.Drawing.Size(177, 48);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "开始创建(&B)";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "House");
            this.imageList1.Images.SetKeyName(1, "shuaka");
            // 
            // cbBuild
            // 
            this.cbBuild.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBuild.FormattingEnabled = true;
            this.cbBuild.Location = new System.Drawing.Point(208, 48);
            this.cbBuild.Margin = new System.Windows.Forms.Padding(4);
            this.cbBuild.Name = "cbBuild";
            this.cbBuild.Size = new System.Drawing.Size(232, 23);
            this.cbBuild.TabIndex = 2;
            this.cbBuild.SelectedIndexChanged += new System.EventHandler(this.CbCombo_SelectedIndexChanged);
            // 
            // lbBuild
            // 
            this.lbBuild.AutoSize = true;
            this.lbBuild.Location = new System.Drawing.Point(148, 52);
            this.lbBuild.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbBuild.Name = "lbBuild";
            this.lbBuild.Size = new System.Drawing.Size(52, 15);
            this.lbBuild.TabIndex = 79;
            this.lbBuild.Text = "楼栋：";
            // 
            // cbArea
            // 
            this.cbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Location = new System.Drawing.Point(208, 15);
            this.cbArea.Margin = new System.Windows.Forms.Padding(4);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(232, 23);
            this.cbArea.TabIndex = 1;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.CbCombo_SelectedIndexChanged);
            // 
            // lbArea
            // 
            this.lbArea.AutoSize = true;
            this.lbArea.Location = new System.Drawing.Point(148, 20);
            this.lbArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbArea.Name = "lbArea";
            this.lbArea.Size = new System.Drawing.Size(52, 15);
            this.lbArea.TabIndex = 77;
            this.lbArea.Text = "小区：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 118);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 15);
            this.label1.TabIndex = 81;
            this.label1.Text = "起始楼层房间编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 15);
            this.label2.TabIndex = 82;
            this.label2.Text = "结束楼层房间编码：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(325, 118);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(116, 15);
            this.label12.TabIndex = 119;
            this.label12.Text = "(范围：0~9999)";
            // 
            // txtRoomCodeBegin
            // 
            this.txtRoomCodeBegin.Location = new System.Drawing.Point(208, 112);
            this.txtRoomCodeBegin.Margin = new System.Windows.Forms.Padding(4);
            this.txtRoomCodeBegin.MaxLength = 4;
            this.txtRoomCodeBegin.Name = "txtRoomCodeBegin";
            this.txtRoomCodeBegin.Size = new System.Drawing.Size(105, 25);
            this.txtRoomCodeBegin.TabIndex = 3;
            this.txtRoomCodeBegin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskCodeText);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 15);
            this.label3.TabIndex = 121;
            this.label3.Text = "(范围：0~9999)";
            // 
            // txtRoomCodeEnd
            // 
            this.txtRoomCodeEnd.Location = new System.Drawing.Point(208, 145);
            this.txtRoomCodeEnd.Margin = new System.Windows.Forms.Padding(4);
            this.txtRoomCodeEnd.MaxLength = 4;
            this.txtRoomCodeEnd.Name = "txtRoomCodeEnd";
            this.txtRoomCodeEnd.Size = new System.Drawing.Size(105, 25);
            this.txtRoomCodeEnd.TabIndex = 4;
            this.txtRoomCodeEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskCodeText);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxComunicateData);
            this.groupBox1.Location = new System.Drawing.Point(16, 245);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(808, 241);
            this.groupBox1.TabIndex = 122;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "生成报告";
            // 
            // tbxComunicateData
            // 
            this.tbxComunicateData.BackColor = System.Drawing.SystemColors.ControlText;
            this.tbxComunicateData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxComunicateData.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tbxComunicateData.Location = new System.Drawing.Point(4, 22);
            this.tbxComunicateData.Margin = new System.Windows.Forms.Padding(4);
            this.tbxComunicateData.Name = "tbxComunicateData";
            this.tbxComunicateData.ReadOnly = true;
            this.tbxComunicateData.Size = new System.Drawing.Size(800, 215);
            this.tbxComunicateData.TabIndex = 46;
            this.tbxComunicateData.Text = "";
            this.tbxComunicateData.TextChanged += new System.EventHandler(this.TbxComunicateData_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Location = new System.Drawing.Point(17, 189);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(717, 15);
            this.label4.TabIndex = 123;
            this.label4.Text = "比如：起始楼层房间：0101,结束楼层房间：11080101 意味0102 ~0108 0201~0208 ~1108这些都会被创建";
            // 
            // cbUnit
            // 
            this.cbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnit.FormattingEnabled = true;
            this.cbUnit.Location = new System.Drawing.Point(208, 80);
            this.cbUnit.Margin = new System.Windows.Forms.Padding(4);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(232, 23);
            this.cbUnit.TabIndex = 125;
            // 
            // lbUnit
            // 
            this.lbUnit.AutoSize = true;
            this.lbUnit.Location = new System.Drawing.Point(148, 85);
            this.lbUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(52, 15);
            this.lbUnit.TabIndex = 124;
            this.lbUnit.Text = "单元：";
            // 
            // BuidingBatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(840, 499);
            this.Controls.Add(this.cbUnit);
            this.Controls.Add(this.lbUnit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRoomCodeEnd);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtRoomCodeBegin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbBuild);
            this.Controls.Add(this.lbBuild);
            this.Controls.Add(this.cbArea);
            this.Controls.Add(this.lbArea);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MinimumSize = new System.Drawing.Size(858, 544);
            this.Name = "BuidingBatchForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BuidingBatchForm_FormClosing);
            this.Load += new System.EventHandler(this.BuidingBatchForm_Load);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lbArea, 0);
            this.Controls.SetChildIndex(this.cbArea, 0);
            this.Controls.SetChildIndex(this.lbBuild, 0);
            this.Controls.SetChildIndex(this.cbBuild, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtRoomCodeBegin, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.txtRoomCodeEnd, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.lbUnit, 0);
            this.Controls.SetChildIndex(this.cbUnit, 0);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbBuild;
        private System.Windows.Forms.Label lbBuild;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.Label lbArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRoomCodeBegin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRoomCodeEnd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbxComunicateData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbUnit;
        private System.Windows.Forms.Label lbUnit;
    }
}
