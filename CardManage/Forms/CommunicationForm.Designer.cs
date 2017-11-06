namespace CardManage.Forms
{
    partial class CommunicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommunicationForm));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tsbClearDebugContent = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveDebugContent = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyDebugContent = new System.Windows.Forms.ToolStripButton();
            this.tbxComunicateData = new System.Windows.Forms.RichTextBox();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "House");
            this.imageList1.Images.SetKeyName(1, "shuaka");
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tsbClearDebugContent,
            this.tsbSaveDebugContent,
            this.tsbCopyDebugContent});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip2.Size = new System.Drawing.Size(1093, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(51, 22);
            this.tsbClose.Text = "关闭";
            this.tsbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbClose.Click += new System.EventHandler(this.TsbClose_Click);
            // 
            // tsbClearDebugContent
            // 
            this.tsbClearDebugContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearDebugContent.Image")));
            this.tsbClearDebugContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearDebugContent.Name = "tsbClearDebugContent";
            this.tsbClearDebugContent.Size = new System.Drawing.Size(99, 22);
            this.tsbClearDebugContent.Text = "清空调试内容";
            this.tsbClearDebugContent.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbClearDebugContent.Click += new System.EventHandler(this.TsbClearDebugContent_Click);
            // 
            // tsbSaveDebugContent
            // 
            this.tsbSaveDebugContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveDebugContent.Image")));
            this.tsbSaveDebugContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveDebugContent.Name = "tsbSaveDebugContent";
            this.tsbSaveDebugContent.Size = new System.Drawing.Size(99, 22);
            this.tsbSaveDebugContent.Text = "保存调试内容";
            this.tsbSaveDebugContent.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbSaveDebugContent.Click += new System.EventHandler(this.TsbSaveDebugContent_Click);
            // 
            // tsbCopyDebugContent
            // 
            this.tsbCopyDebugContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyDebugContent.Image")));
            this.tsbCopyDebugContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyDebugContent.Name = "tsbCopyDebugContent";
            this.tsbCopyDebugContent.Size = new System.Drawing.Size(99, 22);
            this.tsbCopyDebugContent.Text = "复制调试内容";
            this.tsbCopyDebugContent.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbCopyDebugContent.Click += new System.EventHandler(this.TsbCopyDebugContent_Click);
            // 
            // tbxComunicateData
            // 
            this.tbxComunicateData.BackColor = System.Drawing.SystemColors.ControlText;
            this.tbxComunicateData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxComunicateData.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tbxComunicateData.Location = new System.Drawing.Point(0, 25);
            this.tbxComunicateData.Name = "tbxComunicateData";
            this.tbxComunicateData.ReadOnly = true;
            this.tbxComunicateData.Size = new System.Drawing.Size(1093, 448);
            this.tbxComunicateData.TabIndex = 10;
            this.tbxComunicateData.Text = "";
            this.tbxComunicateData.TextChanged += new System.EventHandler(this.TbxComunicateData_TextChanged);
            // 
            // CommunicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1093, 473);
            this.Controls.Add(this.tbxComunicateData);
            this.Controls.Add(this.toolStrip2);
            this.Name = "CommunicationForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommunicationForm_FormClosing);
            this.Load += new System.EventHandler(this.CommunicationForm_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbClearDebugContent;
        private System.Windows.Forms.ToolStripButton tsbSaveDebugContent;
        private System.Windows.Forms.ToolStripButton tsbCopyDebugContent;
        private System.Windows.Forms.RichTextBox tbxComunicateData;
        private System.Windows.Forms.ToolStripButton tsbClose;
    }
}
