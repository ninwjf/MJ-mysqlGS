namespace CardManage.Forms
{
    partial class DataBatchDeleteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataBatchDeleteForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("数据加载中");
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tv = new System.Windows.Forms.TreeView();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(524, 221);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(148, 40);
            this.btnCancel.Text = "关闭&Q)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(524, 36);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Size = new System.Drawing.Size(148, 40);
            this.btnOK.Text = "删除选择(&D)";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "House");
            this.imageList1.Images.SetKeyName(1, "shuaka");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tv);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(481, 501);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据";
            // 
            // tv
            // 
            this.tv.CheckBoxes = true;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.HideSelection = false;
            this.tv.Location = new System.Drawing.Point(4, 22);
            this.tv.Margin = new System.Windows.Forms.Padding(4);
            this.tv.Name = "tv";
            treeNode1.Name = "init";
            treeNode1.Text = "数据加载中";
            treeNode1.ToolTipText = "数据加载中";
            this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tv.Size = new System.Drawing.Size(473, 475);
            this.tv.TabIndex = 3;
            this.tv.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.Tv_AfterCheck);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(524, 95);
            this.btnDeleteAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(148, 40);
            this.btnDeleteAll.TabIndex = 10;
            this.btnDeleteAll.Text = "删除全部数据(&A)";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // DataBatchDeleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(688, 531);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MinimumSize = new System.Drawing.Size(706, 576);
            this.Name = "DataBatchDeleteForm";
            this.Activated += new System.EventHandler(this.DataBatchDeleteForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataBatchDeleteForm_FormClosing);
            this.Load += new System.EventHandler(this.DataBatchDeleteForm_Load);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnDeleteAll, 0);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.TreeView tv;
    }
}
