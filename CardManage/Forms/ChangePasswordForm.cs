using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CardManage.Tools;

namespace CardManage.Forms
{
    public partial class ChangePasswordForm : SetFormBase
    {
        protected ChangePasswordForm()
        {
            InitializeComponent();
        }


        public ChangePasswordForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;

            txtUserName.Text = objUserInfo.UserName;
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            //验证逻辑
            string strOldPassword = txtOldPassword.Text;
            string strNewPassword = txtNewPassword.Text;
            string strNewConfirmPassword = txtNewConfirmPassword.Text;

            if (strOldPassword.Equals("") || strNewPassword.Equals("") || strNewConfirmPassword.Equals(""))
            {
                CMessageBox.ShowError("旧密码、新密码和确认密码都必须填写！", Config.DialogTitle);
                txtOldPassword.Focus();
                return;
            }
            if (!strNewPassword.Equals(strNewConfirmPassword))
            {
                CMessageBox.ShowError("新密码和确认密码不一致，请重新输入！", Config.DialogTitle);
                txtNewPassword.Focus();
                return;
            }

            CardManage.IDAL.IUserInfo objUser = CardManage.DALFactory.DALFactory.UserInfo();
            bool bIfSucc = objUser.ChangePassword(RunVariable.CurrentUserInfo.ID, Functions.Md5_32(strOldPassword), Functions.Md5_32(strNewPassword), out string strErrorInfo);
            if (!bIfSucc)
            {
                CMessageBox.ShowError(string.Format("对不起，修改密码错误，错误如下：\r\n{0}！", strErrorInfo), Config.DialogTitle);
                return;
            }
            CMessageBox.ShowSucc("恭喜您，密码修改成功！", Config.DialogTitle);
            this.DialogResult = DialogResult.OK;
        }
    }
}
