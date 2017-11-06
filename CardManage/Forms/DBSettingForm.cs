using CardManage.Logic;
using CardManage.Model;
using CardManage.Tools;
using System;
using System.IO;
using System.Windows.Forms;

namespace CardManage.Forms
{
    /// <summary>
    /// 单独数据库连接设置
    /// </summary>
    public partial class DBSettingForm : CardManage.Forms.SetFormBase
    {
        /// <summary>
        /// 当前配置
        /// </summary>
        private Setting _OldSetting;

        protected DBSettingForm()
        {
            InitializeComponent();
        }

        public DBSettingForm(string strTitle, bool bIsModal, UserInfo objUserInfo, WindowSize objWindowSize = null, Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;

        }

        private void DBSettingForm_Load(object sender, EventArgs e)
        {
            //加载数据
            this._OldSetting = SysConfiger.LoadSetting(RunVariable.IniPathAndFileName, out string strErrInfo);
            if (!(this._OldSetting == null || !strErrInfo.Equals("")))
            {
                //数据库配置
                if (this._OldSetting.DBSetting != null)
                {
                    txtDBIP.Text = Functions.FormatString(this._OldSetting.DBSetting.DB_IP);
                    txtDBName.Text = Functions.FormatString(this._OldSetting.DBSetting.DB_Name);
                    txtUserName.Text = Functions.FormatString(this._OldSetting.DBSetting.DB_User);
                    txtUserPassword.Text = Functions.FormatString(this._OldSetting.DBSetting.DB_Password);
                }
            }
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            DBSetting objDBSetting = GetSettingFromUI();
            if (!CheckDBSettings(objDBSetting, out string strErrInfo))
            {
                CMessageBox.ShowError(string.Format("数据库配置错误，错误如下：\r\n{0}", strErrInfo), Config.DialogTitle);
                return;
            }
            Setting objAllSetting = new Setting()
            {
                //数据库
                DBSetting = objDBSetting,
                //制卡串口
                WriteComProperty = new Classes.ComProperty()
            };
            objAllSetting.WriteComProperty.PortName = "";
            objAllSetting.WriteComProperty.BaudRate = 9600;
            objAllSetting.WriteComProperty.DataBits = 8;
            objAllSetting.WriteComProperty.StopBits = System.IO.Ports.StopBits.One;
            objAllSetting.WriteComProperty.Parity = System.IO.Ports.Parity.None;
            //监控串口
            objAllSetting.MonitorComProperty = new Classes.ComProperty()
            {
                PortName = "",
                BaudRate = 19200,
                DataBits = 8,
                StopBits = System.IO.Ports.StopBits.One,
                Parity = System.IO.Ports.Parity.None
            };
            //其他配置
            objAllSetting.OtherSetting = new OtherSetting()
            {
                SyncTimePL = 10,
                SyncAuto = 0,
                Chs = 1
            };
            if (this._OldSetting != null)
            {
                //制卡串口
                if (this._OldSetting.WriteComProperty != null)
                {
                    objAllSetting.WriteComProperty.BaudRate = this._OldSetting.WriteComProperty.BaudRate;
                    objAllSetting.WriteComProperty.DataBits = this._OldSetting.WriteComProperty.DataBits;
                    objAllSetting.WriteComProperty.StopBits = this._OldSetting.WriteComProperty.StopBits;
                    objAllSetting.WriteComProperty.Parity = this._OldSetting.WriteComProperty.Parity;
                }
                //监控串口
                if (this._OldSetting.MonitorComProperty != null)
                {
                    objAllSetting.MonitorComProperty.BaudRate = this._OldSetting.MonitorComProperty.BaudRate;
                    objAllSetting.MonitorComProperty.DataBits = this._OldSetting.MonitorComProperty.DataBits;
                    objAllSetting.MonitorComProperty.StopBits = this._OldSetting.MonitorComProperty.StopBits;
                    objAllSetting.MonitorComProperty.Parity = this._OldSetting.MonitorComProperty.Parity;
                }
                //其他配置
                if (this._OldSetting.OtherSetting != null)
                {
                    objAllSetting.OtherSetting.SyncTimePL = this._OldSetting.OtherSetting.SyncTimePL;
                    objAllSetting.OtherSetting.SyncAuto = this._OldSetting.OtherSetting.SyncAuto;
                    objAllSetting.OtherSetting.Chs = this._OldSetting.OtherSetting.Chs;
                }
            }

            if (!SysConfiger.SaveSetting(objAllSetting, RunVariable.IniPathAndFileName, out strErrInfo))
            {
                CMessageBox.ShowError(string.Format("对不起，保存数据库配置失败，错误如下：\r\n{0}", strErrInfo), Config.DialogTitle);
            }
            else
            {
                CMessageBox.ShowSucc(string.Format("恭喜您，保存数据库配置成功！"), Config.DialogTitle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnCheckDBSetting_Click(object sender, EventArgs e)
        {
            DBSetting objDBSetting = GetSettingFromUI();
            if (!CheckDBSettings(objDBSetting, out string strErrInfo))
            {
                CMessageBox.ShowError(string.Format("数据库配置错误，错误如下：\r\n{0}", strErrInfo), Config.DialogTitle);
                return;
            }
            DBLinkTestForm objModal = new DBLinkTestForm(string.Format("SERVER={0};Port={1};User ID={2};Password={3};Charset={4};Database={5};allow user variables=true", objDBSetting.DB_IP, "3308", objDBSetting.DB_User, objDBSetting.DB_Password, System.Text.Encoding.Default.HeaderName.ToString(), objDBSetting.DB_Name));
            if (objModal.ShowDialog() == DialogResult.OK)
            {
                CMessageBox.ShowSucc(string.Format("恭喜您，数据库连接成功！"), Config.DialogTitle);
            }
            else if (objModal.ShowDialog() == DialogResult.No)
            {
                CMessageBox.ShowError(string.Format("对不起，数据库连接失败！请重新配置软件参数！"), Config.DialogTitle);
                File.Delete(RunVariable.IniPathAndFileName);
            }
        }

        private DBSetting GetSettingFromUI()
        {
            DBSetting objDBSetting = new DBSetting()
            {
                DB_IP = Functions.FormatString(txtDBIP.Text),
                DB_Name = Functions.FormatString(txtDBName.Text),
                DB_User = Functions.FormatString(txtUserName.Text),
                DB_Password = Functions.FormatString(txtUserPassword.Text)
            };
            return objDBSetting;
        }

        private bool CheckDBSettings(Model.DBSetting objDBSetting, out string strErrInfo)
        {
            bool bIfOk = false;
            strErrInfo = "";
            if (objDBSetting != null)
            {
                //地址
                if (string.IsNullOrEmpty(objDBSetting.DB_IP) || objDBSetting.DB_IP.Equals(""))
                {
                    strErrInfo += "数据库地址为空！\r\n";
                }
                //名称
                if (string.IsNullOrEmpty(objDBSetting.DB_Name) || objDBSetting.DB_Name.Equals(""))
                {
                    strErrInfo += "数据库名为空！\r\n";
                }
                //用户名
                if (string.IsNullOrEmpty(objDBSetting.DB_User) || objDBSetting.DB_User.Equals(""))
                {
                    strErrInfo += "数据库用户名为空！\r\n";
                }
                bIfOk = string.IsNullOrEmpty(strErrInfo);
            }
            else
            {
                strErrInfo = "数据库配置为空！";
            }
            return bIfOk;
        }

        private void BtnUserLocalDB_Click(object sender, EventArgs e)
        {
            txtDBIP.Text = "localhost";
        }
    }
}
