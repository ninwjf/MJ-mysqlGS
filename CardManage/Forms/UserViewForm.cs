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
    /// <summary>
    /// 用户视图
    /// </summary>
    public partial class UserViewForm : SetFormBase
    {
        private EAction _CurrentAction;
        private int _CurrentID = 0;
        private int _CurrentFlag = -1;

        private string _OriginalPassword = "";
        protected UserViewForm()
        {
            InitializeComponent();
        }

        public UserViewForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;

            this._CurrentAction = (EAction)objFlag.Keyword1;
            if (objFlag.Keyword2 != null)
            {
                switch (this._CurrentAction)
                {
                    case EAction.Create:
                        this._CurrentFlag = Convert.ToInt32(objFlag.Keyword2);
                        break;
                    case EAction.Edit:
                        this._CurrentID = Convert.ToInt32(objFlag.Keyword2);
                        break;
                }
            }
        }

        private void UserViewForm_Load(object sender, EventArgs e)
        {
            cbFlag.Items.Add("请选择角色");
            cbFlag.Items.Add("超级管理员");
            cbFlag.Items.Add("普通管理员");
            cbFlag.SelectedIndex = this._CurrentFlag + 1;

            //获得数据
            if (this._CurrentAction.Equals(EAction.Edit))
            {
                IDAL.IUserInfo objUser = DALFactory.DALFactory.UserInfo();
                Model.UserInfo objModel = objUser.GetModel(this._CurrentID);
                if (objModel != null)
                {
                    cbFlag.SelectedIndex = objModel.Flag + 1;
                    txtUserName.Text = objModel.UserName;
                    this._OriginalPassword = objModel.UserPwd;
                    txtPassword.Text = this._OriginalPassword;
                    txtMemo.Text = objModel.Memo;
                }
            }
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            int iFlag = cbFlag.SelectedIndex - 1;
            string strUserName = txtUserName.Text.Trim();
            string strPassword = txtPassword.Text.Trim();
            string strMemo = txtMemo.Text.Trim();

            if (iFlag == -1 || strUserName.Equals("") || strPassword.Equals(""))
            {
                CMessageBox.ShowError("角色、用户名和密码都必须输入！", Config.DialogTitle);
                return;
            }

            if (MessageBox.Show("确定要保存吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            Model.UserInfo objModel = new Model.UserInfo()
            {
                Flag = iFlag,
                ID = this._CurrentID,
                UserName = strUserName,
                UserPwd = (this._CurrentAction.Equals(EAction.Create)) ? Functions.Md5_32(strPassword) : ((strPassword.Equals(this._OriginalPassword) ? this._OriginalPassword : Functions.Md5_32(strPassword))),
                Memo = strMemo
            };
            string strErrorInfo = "";
            IDAL.IUserInfo objDAL = DALFactory.DALFactory.UserInfo();
            if (this._CurrentAction.Equals(EAction.Create))
            {
                int iNewID = objDAL.Add(objModel, out strErrorInfo);
                if (iNewID <= 0)
                {
                    CMessageBox.ShowError(string.Format("创建用户失败，错误如下：\r\n{0}！", strErrorInfo), Config.DialogTitle);
                    return;
                }
            }
            else
            {
                bool bIfSucc = objDAL.Update(objModel, out strErrorInfo);
                if (!bIfSucc)
                {
                    CMessageBox.ShowError(string.Format("修改用户失败，错误如下：\r\n{0}！", strErrorInfo), Config.DialogTitle);
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
