using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CardManage.Model;
using CardManage.Tools;
using System.Diagnostics;
using System.Net;

namespace CardManage.Forms
{
    public partial class DBServerSettingForm : CardManage.Forms.SetFormBase
    {
        private DBSetting _DBSetting;
        /// <summary>
        ///获得当前设置
        /// </summary>
        /// <returns></returns>
        public DBSetting GetDBSetting()
        {
            return this._DBSetting;
        }

        protected DBServerSettingForm()
        {
            InitializeComponent();
        }

        public DBServerSettingForm(string strTitle, bool bIsModal, UserInfo objUserInfo, WindowSize objWindowSize = null, Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            string strDBIP = Functions.FormatString(txtDBIP.Text);
            string strUserName = Functions.FormatString(txtUserName.Text);
            string strUserPassword = Functions.FormatString(txtUserPassword.Text);

            //地址
            if (string.IsNullOrEmpty(strDBIP) || strDBIP.Equals(""))
            {
                txtDBIP.Focus();
                CMessageBox.ShowError(string.Format("请输入数据库地址！"), Config.DialogTitle);
                return;
            }
            //名称
            if (string.IsNullOrEmpty(strUserName) || strUserName.Equals(""))
            {
                txtUserName.Focus();
                CMessageBox.ShowError(string.Format("请输入数据库用户名！"), Config.DialogTitle);
                return;
            }

            DBLinkTestForm objModal = new DBLinkTestForm(string.Format("SERVER={0};Port={1};User ID={2};Password={3};Charset={4};Database=mysql;allow user variables=true", strDBIP, "3308", strUserName, strUserPassword, System.Text.Encoding.Default.HeaderName.ToString()));
            if (objModal.ShowDialog() == DialogResult.OK)
            {
                _DBSetting = new DBSetting()
                {
                    DB_IP = strDBIP,
                    DB_User = strUserName,
                    DB_Password = strUserPassword
                };
                this.DialogResult = DialogResult.OK;
            }
            else if (objModal.ShowDialog() == DialogResult.No)
            {
                CMessageBox.ShowError(string.Format("目的服务器配置错误，原因可能是数据库未安装或则用户名密码错误，请重新设置和排查！"), Config.DialogTitle);
            }
            else
            {
                Application.Exit();
            }
        }

        private void BtnUserLocalDB_Click(object sender, EventArgs e)
        {
            txtDBIP.Text = "localhost";
        }
    }
}
