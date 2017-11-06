namespace CardManage.Forms
{
    partial class BuildingViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildingViewForm));
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbCode = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.lbArea = new System.Windows.Forms.Label();
            this.lbBuild = new System.Windows.Forms.Label();
            this.lbUnit = new System.Windows.Forms.Label();
            this.cbBuild = new System.Windows.Forms.ComboBox();
            this.cbUnit = new System.Windows.Forms.ComboBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.lbContact = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.lbTel = new System.Windows.Forms.Label();
            this.lbRangeCode = new System.Windows.Forms.Label();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.lbSerialNo = new System.Windows.Forms.Label();
            this.lbSerialNoTip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(244, 276);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(96, 276);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "House");
            this.imageList1.Images.SetKeyName(1, "shuaka");
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(131, 144);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtCode.MaxLength = 2;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(265, 25);
            this.txtCode.TabIndex = 61;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(131, 111);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(265, 25);
            this.txtName.TabIndex = 60;
            // 
            // lbCode
            // 
            this.lbCode.AutoSize = true;
            this.lbCode.Location = new System.Drawing.Point(50, 147);
            this.lbCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(68, 15);
            this.lbCode.TabIndex = 58;
            this.lbCode.Text = "编  码：";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(37, 116);
            this.lbName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(82, 15);
            this.lbName.TabIndex = 57;
            this.lbName.Text = "建筑名称：";
            // 
            // cbArea
            // 
            this.cbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Location = new System.Drawing.Point(131, 14);
            this.cbArea.Margin = new System.Windows.Forms.Padding(4);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(265, 23);
            this.cbArea.TabIndex = 64;
            this.cbArea.Visible = false;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.CbCombo_SelectedIndexChanged);
            // 
            // lbArea
            // 
            this.lbArea.AutoSize = true;
            this.lbArea.Location = new System.Drawing.Point(37, 19);
            this.lbArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbArea.Name = "lbArea";
            this.lbArea.Size = new System.Drawing.Size(82, 15);
            this.lbArea.TabIndex = 63;
            this.lbArea.Text = "所属小区：";
            this.lbArea.Visible = false;
            // 
            // lbBuild
            // 
            this.lbBuild.AutoSize = true;
            this.lbBuild.Location = new System.Drawing.Point(37, 51);
            this.lbBuild.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbBuild.Name = "lbBuild";
            this.lbBuild.Size = new System.Drawing.Size(82, 15);
            this.lbBuild.TabIndex = 65;
            this.lbBuild.Text = "所属楼栋：";
            this.lbBuild.Visible = false;
            // 
            // lbUnit
            // 
            this.lbUnit.AutoSize = true;
            this.lbUnit.Location = new System.Drawing.Point(37, 84);
            this.lbUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(82, 15);
            this.lbUnit.TabIndex = 66;
            this.lbUnit.Text = "所属单元：";
            this.lbUnit.Visible = false;
            // 
            // cbBuild
            // 
            this.cbBuild.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBuild.FormattingEnabled = true;
            this.cbBuild.Location = new System.Drawing.Point(131, 46);
            this.cbBuild.Margin = new System.Windows.Forms.Padding(4);
            this.cbBuild.Name = "cbBuild";
            this.cbBuild.Size = new System.Drawing.Size(265, 23);
            this.cbBuild.TabIndex = 68;
            this.cbBuild.Visible = false;
            this.cbBuild.SelectedIndexChanged += new System.EventHandler(this.CbCombo_SelectedIndexChanged);
            // 
            // cbUnit
            // 
            this.cbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnit.FormattingEnabled = true;
            this.cbUnit.Location = new System.Drawing.Point(131, 79);
            this.cbUnit.Margin = new System.Windows.Forms.Padding(4);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(265, 23);
            this.cbUnit.TabIndex = 69;
            this.cbUnit.Visible = false;
            this.cbUnit.SelectedIndexChanged += new System.EventHandler(this.CbCombo_SelectedIndexChanged);
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(131, 176);
            this.txtContact.Margin = new System.Windows.Forms.Padding(4);
            this.txtContact.MaxLength = 20;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(265, 25);
            this.txtContact.TabIndex = 72;
            // 
            // lbContact
            // 
            this.lbContact.AutoSize = true;
            this.lbContact.Location = new System.Drawing.Point(21, 181);
            this.lbContact.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbContact.Name = "lbContact";
            this.lbContact.Size = new System.Drawing.Size(97, 15);
            this.lbContact.TabIndex = 71;
            this.lbContact.Text = "管理员姓名：";
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(131, 209);
            this.txtTel.Margin = new System.Windows.Forms.Padding(4);
            this.txtTel.MaxLength = 20;
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(265, 25);
            this.txtTel.TabIndex = 74;
            // 
            // lbTel
            // 
            this.lbTel.AutoSize = true;
            this.lbTel.Location = new System.Drawing.Point(37, 214);
            this.lbTel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTel.Name = "lbTel";
            this.lbTel.Size = new System.Drawing.Size(82, 15);
            this.lbTel.TabIndex = 73;
            this.lbTel.Text = "联系电话：";
            // 
            // lbRangeCode
            // 
            this.lbRangeCode.AutoSize = true;
            this.lbRangeCode.Location = new System.Drawing.Point(397, 149);
            this.lbRangeCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbRangeCode.Name = "lbRangeCode";
            this.lbRangeCode.Size = new System.Drawing.Size(100, 15);
            this.lbRangeCode.TabIndex = 75;
            this.lbRangeCode.Text = "(范围：0~99)";
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(131, 242);
            this.txtSerialNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtSerialNo.MaxLength = 32;
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(265, 25);
            this.txtSerialNo.TabIndex = 77;
            this.txtSerialNo.Visible = false;
            this.txtSerialNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaskSerialNoText);
            // 
            // lbSerialNo
            // 
            this.lbSerialNo.AutoSize = true;
            this.lbSerialNo.Location = new System.Drawing.Point(35, 245);
            this.lbSerialNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSerialNo.Name = "lbSerialNo";
            this.lbSerialNo.Size = new System.Drawing.Size(83, 15);
            this.lbSerialNo.TabIndex = 76;
            this.lbSerialNo.Text = "序 列 号：";
            this.lbSerialNo.Visible = false;
            // 
            // lbSerialNoTip
            // 
            this.lbSerialNoTip.AutoSize = true;
            this.lbSerialNoTip.Location = new System.Drawing.Point(397, 248);
            this.lbSerialNoTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSerialNoTip.Name = "lbSerialNoTip";
            this.lbSerialNoTip.Size = new System.Drawing.Size(99, 15);
            this.lbSerialNoTip.TabIndex = 123;
            this.lbSerialNoTip.Text = "(长度：32位)";
            this.lbSerialNoTip.Visible = false;
            // 
            // BuildingViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(512, 326);
            this.Controls.Add(this.lbSerialNoTip);
            this.Controls.Add(this.txtSerialNo);
            this.Controls.Add(this.lbSerialNo);
            this.Controls.Add(this.lbRangeCode);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.lbTel);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.lbContact);
            this.Controls.Add(this.cbUnit);
            this.Controls.Add(this.cbBuild);
            this.Controls.Add(this.lbUnit);
            this.Controls.Add(this.lbBuild);
            this.Controls.Add(this.cbArea);
            this.Controls.Add(this.lbArea);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lbCode);
            this.Controls.Add(this.lbName);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MaximumSize = new System.Drawing.Size(530, 371);
            this.MinimumSize = new System.Drawing.Size(530, 371);
            this.Name = "BuildingViewForm";
            this.Load += new System.EventHandler(this.BuildingViewForm_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.lbName, 0);
            this.Controls.SetChildIndex(this.lbCode, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtCode, 0);
            this.Controls.SetChildIndex(this.lbArea, 0);
            this.Controls.SetChildIndex(this.cbArea, 0);
            this.Controls.SetChildIndex(this.lbBuild, 0);
            this.Controls.SetChildIndex(this.lbUnit, 0);
            this.Controls.SetChildIndex(this.cbBuild, 0);
            this.Controls.SetChildIndex(this.cbUnit, 0);
            this.Controls.SetChildIndex(this.lbContact, 0);
            this.Controls.SetChildIndex(this.txtContact, 0);
            this.Controls.SetChildIndex(this.lbTel, 0);
            this.Controls.SetChildIndex(this.txtTel, 0);
            this.Controls.SetChildIndex(this.lbRangeCode, 0);
            this.Controls.SetChildIndex(this.lbSerialNo, 0);
            this.Controls.SetChildIndex(this.txtSerialNo, 0);
            this.Controls.SetChildIndex(this.lbSerialNoTip, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lbCode;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.Label lbArea;
        private System.Windows.Forms.Label lbBuild;
        private System.Windows.Forms.Label lbUnit;
        private System.Windows.Forms.ComboBox cbBuild;
        private System.Windows.Forms.ComboBox cbUnit;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label lbContact;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label lbTel;
        private System.Windows.Forms.Label lbRangeCode;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.Label lbSerialNo;
        private System.Windows.Forms.Label lbSerialNoTip;
    }
}
