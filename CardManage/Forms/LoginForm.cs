using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CardManage.IDAL;
using CardManage.Tools;
namespace CardManage.Forms
{
    public partial class LoginForm : SetFormBase
    {
        protected LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //从临时文件中取得用户名
            txtUserName.Text = Manager.GetInstance().GetUserName();
#if DEBUG
                txtPassword.Text = "123456";
#else
                txtPassword.Text = "";
#endif
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            //验证逻辑
            string strUserName = txtUserName.Text.Trim();
            string strPassword = txtPassword.Text.Trim();
            if (strUserName.Equals(""))
            {
                CMessageBox.ShowError("请输入用户名！", Config.DialogTitle);
                return;
            }

            IUserInfo objUser = DALFactory.DALFactory.UserInfo();
            int iUserID = objUser.Login(strUserName, Functions.Md5_32(strPassword), out string strErrorInfo);
            if (iUserID <= 0)
            {
                CMessageBox.ShowError("输入的用户名或密码错误，请重新输入！", Config.DialogTitle);
                return;
            }
            RunVariable.CurrentUserInfo = objUser.GetModel(iUserID);
            if (RunVariable.CurrentUserInfo == null)
            {
                CMessageBox.ShowError(string.Format("无法找到编号为{0}的用户！", iUserID), Config.DialogTitle);
                return;
            }
            //保存用户名到临时文件
            Manager.GetInstance().SaveUserName(strUserName);
            this.DialogResult = DialogResult.OK;
        }
    }
}
