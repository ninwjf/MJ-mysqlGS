namespace CardManage.Forms
{
    partial class SelectBuildingForm : SetFormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectBuildingForm));
            this.cbUnit = new System.Windows.Forms.ComboBox();
            this.cbBuild = new System.Windows.Forms.ComboBox();
            this.lbUnit = new System.Windows.Forms.Label();
            this.lbBuild = new System.Windows.Forms.Label();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.lbArea = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRoom = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(217, 180);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(63, 180);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Text = "确定(&S)";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "House");
            this.imageList1.Images.SetKeyName(1, "shuaka");
            // 
            // cbUnit
            // 
            this.cbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnit.FormattingEnabled = true;
            this.cbUnit.Location = new System.Drawing.Point(97, 91);
            this.cbUnit.Margin = new System.Windows.Forms.Padding(4);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(236, 23);
            this.cbUnit.TabIndex = 77;
            this.cbUnit.SelectedIndexChanged += new System.EventHandler(this.CbCombo_SelectedIndexChanged);
            // 
            // cbBuild
            // 
            this.cbBuild.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBuild.FormattingEnabled = true;
            this.cbBuild.Location = new System.Drawing.Point(97, 59);
            this.cbBuild.Margin = new System.Windows.Forms.Padding(4);
            this.cbBuild.Name = "cbBuild";
            this.cbBuild.Size = new System.Drawing.Size(236, 23);
            this.cbBuild.TabIndex = 76;
            this.cbBuild.SelectedIndexChanged += new System.EventHandler(this.CbCombo_SelectedIndexChanged);
            // 
            // lbUnit
            // 
            this.lbUnit.AutoSize = true;
            this.lbUnit.Location = new System.Drawing.Point(37, 96);
            this.lbUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(52, 15);
            this.lbUnit.TabIndex = 74;
            this.lbUnit.Text = "单元：";
            // 
            // lbBuild
            // 
            this.lbBuild.AutoSize = true;
            this.lbBuild.Location = new System.Drawing.Point(37, 64);
            this.lbBuild.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbBuild.Name = "lbBuild";
            this.lbBuild.Size = new System.Drawing.Size(52, 15);
            this.lbBuild.TabIndex = 73;
            this.lbBuild.Text = "楼栋：";
            // 
            // cbArea
            // 
            this.cbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Location = new System.Drawing.Point(97, 26);
            this.cbArea.Margin = new System.Windows.Forms.Padding(4);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(236, 23);
            this.cbArea.TabIndex = 72;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.CbCombo_SelectedIndexChanged);
            // 
            // lbArea
            // 
            this.lbArea.AutoSize = true;
            this.lbArea.Location = new System.Drawing.Point(37, 31);
            this.lbArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbArea.Name = "lbArea";
            this.lbArea.Size = new System.Drawing.Size(52, 15);
            this.lbArea.TabIndex = 71;
            this.lbArea.Text = "小区：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 129);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 79;
            this.label1.Text = "房间：";
            // 
            // cbRoom
            // 
            this.cbRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRoom.FormattingEnabled = true;
            this.cbRoom.Location = new System.Drawing.Point(97, 124);
            this.cbRoom.Margin = new System.Windows.Forms.Padding(4);
            this.cbRoom.Name = "cbRoom";
            this.cbRoom.Size = new System.Drawing.Size(236, 23);
            this.cbRoom.TabIndex = 80;
            this.cbRoom.SelectedIndexChanged += new System.EventHandler(this.CbCombo_SelectedIndexChanged);
            // 
            // SelectBuildingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(383, 230);
            this.Controls.Add(this.cbRoom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbUnit);
            this.Controls.Add(this.cbBuild);
            this.Controls.Add(this.lbUnit);
            this.Controls.Add(this.lbBuild);
            this.Controls.Add(this.cbArea);
            this.Controls.Add(this.lbArea);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MinimumSize = new System.Drawing.Size(401, 275);
            this.Name = "SelectBuildingForm";
            this.Load += new System.EventHandler(this.SelectBuildingForm_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.lbArea, 0);
            this.Controls.SetChildIndex(this.cbArea, 0);
            this.Controls.SetChildIndex(this.lbBuild, 0);
            this.Controls.SetChildIndex(this.lbUnit, 0);
            this.Controls.SetChildIndex(this.cbBuild, 0);
            this.Controls.SetChildIndex(this.cbUnit, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbRoom, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbUnit;
        private System.Windows.Forms.ComboBox cbBuild;
        private System.Windows.Forms.Label lbUnit;
        private System.Windows.Forms.Label lbBuild;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.Label lbArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRoom;
    }
}
