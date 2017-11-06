using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CardManage.Logic;

namespace CardManage.Forms
{
    public partial class SyncTimeForm : CardManage.Forms.SetFormBase
    {
        protected SyncTimeForm()
        {
            InitializeComponent();
        }

        public SyncTimeForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

        private void SyncTimeForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lbCurrentTime.Text = DateTime.Now.ToString(Config.LongTimeFormat);
        }

        private void SyncTimeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lbCurrentTime.Text = DateTime.Now.ToString(Config.LongTimeFormat);
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要马上发送远程校时指令吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            if (CardMonitor.GetInstance().SyncTime(DateTime.Now, out string strErrInfo))
            {
                Tools.CMessageBox.ShowSucc("恭喜您，远程校时指令发送成功！", Config.DialogTitle);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Tools.CMessageBox.ShowError(string.Format("对不起，远程校时指令发送失败，错误原因:\r\n{0}！", strErrInfo), Config.DialogTitle);
            }
        }
    }
}
