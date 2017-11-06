using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

using CardManage.Model;
using CardManage.Logic;
using CardManage.Tools;
namespace CardManage.Forms
{
    public partial class SystemSetForm : SetFormBase
    {
        private bool _IfLoginBefore = false;

        protected SystemSetForm()
        {
            InitializeComponent();
        }

        public SystemSetForm(string strTitle, bool bIsModal, UserInfo objUserInfo, WindowSize objWindowSize = null, Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
            if (!(objFlag == null || string.Compare(objFlag.Keyword1.ToString(), "LoginBefore") != 0)) this._IfLoginBefore = true;


            //列出串口列表
            string[] ports = SerialPort.GetPortNames();
            for (int i = 1; i <= 2; i++)
            {
                ComboBox cbPortName = (ComboBox)this.GetType().GetField("cbPortName" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                ComboBox cbBaudRate = (ComboBox)this.GetType().GetField("cbBaudRate" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                ComboBox cbDataBits = (ComboBox)this.GetType().GetField("cbDataBits" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                ComboBox cbStopBits = (ComboBox)this.GetType().GetField("cbStopBits" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                ComboBox cbParity = (ComboBox)this.GetType().GetField("cbParity" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                //串口列表
                cbPortName.Items.Add("请选择串口");
                if (ports.Length > 0)
                {
                    Array.Sort(ports);
                    cbPortName.Items.AddRange(ports);
                }
                cbPortName.SelectedIndex = 0;

                //波特率
                cbBaudRate.Items.Add("300");
                cbBaudRate.Items.Add("600");
                cbBaudRate.Items.Add("1200");
                cbBaudRate.Items.Add("2400");
                cbBaudRate.Items.Add("4800");
                cbBaudRate.Items.Add("9600");
                cbBaudRate.Items.Add("19200");
                cbBaudRate.Items.Add("38400");
                cbBaudRate.Items.Add("43000");
                cbBaudRate.Items.Add("56000");
                cbBaudRate.Items.Add("57600");
                cbBaudRate.Items.Add("115200");
                cbBaudRate.SelectedIndex = (string.Compare(cbBaudRate.Name, "cbBaudRate1", true) == 0) ? 5 : 6;

                //列出停止位
                cbStopBits.Items.Add("1");
                cbStopBits.Items.Add("2");
                cbStopBits.SelectedIndex = 0;

                //列出数据位
                cbDataBits.Items.Add("8");
                cbDataBits.Items.Add("7");
                cbDataBits.Items.Add("6");
                cbDataBits.Items.Add("5");
                cbDataBits.SelectedIndex = 0;

                //列出奇偶校验位
                cbParity.Items.Add(PParity.None);
                cbParity.Items.Add(PParity.Odd);
                cbParity.Items.Add(PParity.Even);
                cbParity.SelectedIndex = 0;
            }
        }

        private void SystemSetForm_Load(object sender, EventArgs e)
        {
            this.IfFormLoadOk = false;
            //加载数据
            Setting objSetting = SysConfiger.LoadSetting(RunVariable.IniPathAndFileName, out string strErrInfo);
            if (!(objSetting == null || !strErrInfo.Equals("")))
            {
                //数据库配置
                if (objSetting.DBSetting != null)
                {
                    txtDBIP.Text = Functions.FormatString(objSetting.DBSetting.DB_IP);
                    txtDBName.Text = Functions.FormatString(objSetting.DBSetting.DB_Name);
                    txtUserName.Text = Functions.FormatString(objSetting.DBSetting.DB_User);
                    txtUserPassword.Text = Functions.FormatString(objSetting.DBSetting.DB_Password);
                }
                //Com1配置
                if (objSetting.WriteComProperty != null)
                {
                    txtPortName1.Text = Functions.FormatString(objSetting.WriteComProperty.PortName);
                    cbBaudRate1.Text = Functions.FormatString(objSetting.WriteComProperty.BaudRate);
                    cbDataBits1.Text = Functions.FormatString(objSetting.WriteComProperty.DataBits);
                    cbStopBits1.Text = objSetting.WriteComProperty.StopBits.Equals(StopBits.One) ? "1" : "2";
                    cbParity1.SelectedIndex = objSetting.WriteComProperty.Parity.Equals(Parity.None) ? 0 : (objSetting.WriteComProperty.Parity.Equals(Parity.Odd) ? 1 : 2);
                }
                //Com2配置
                if (objSetting.MonitorComProperty != null)
                {
                    txtPortName2.Text = Functions.FormatString(objSetting.MonitorComProperty.PortName);
                    cbBaudRate2.Text = Functions.FormatString(objSetting.MonitorComProperty.BaudRate);
                    cbDataBits2.Text = Functions.FormatString(objSetting.MonitorComProperty.DataBits);
                    cbStopBits2.Text = objSetting.MonitorComProperty.StopBits.Equals(StopBits.One) ? "1" : "2";
                    cbParity2.SelectedIndex = objSetting.MonitorComProperty.Parity.Equals(Parity.None) ? 0 : (objSetting.MonitorComProperty.Parity.Equals(Parity.Odd) ? 1 : 2);
                }
                //其它配置
                tBChs.Text = "1";
                txtSyncPL.Text = "10";
                cBSyncAuto.Checked = false;
                if (objSetting.OtherSetting != null)
                {
                    txtSyncPL.Text = objSetting.OtherSetting.SyncTimePL.ToString();
                    cBSyncAuto.Checked = (objSetting.OtherSetting.SyncAuto == 1);
                    tBChs.Text = objSetting.OtherSetting.Chs.ToString();
                }
            } 
            this.IfFormLoadOk = true;
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            string strSyncTimePL = txtSyncPL.Text.Trim();
            if (string.IsNullOrEmpty(strSyncTimePL) || !Functions.IsInt(strSyncTimePL))
            {
                tabControl1.SelectedTab = tabPage4;
                txtSyncPL.Focus();
                CMessageBox.ShowError(string.Format("时间同步频率不能为空，且必须为大于等于1的整数"), Config.DialogTitle);
                return;
            }
            else
            {
                int iSyncTimePL = Convert.ToInt32(strSyncTimePL);
                if (iSyncTimePL < 1)
                {
                    tabControl1.SelectedTab = tabPage4;
                    txtSyncPL.Focus();
                    CMessageBox.ShowError(string.Format("时间同步频率必须为大于等于1的整数"), Config.DialogTitle);
                    return;
                }
            }

            string strChs = tBChs.Text.Trim();
            if (string.IsNullOrEmpty(strChs) || !Functions.IsInt(strChs))
            {
                tabControl1.SelectedTab = tabPage4;
                tBChs.Focus();
                CMessageBox.ShowError(string.Format("默认扇区不能为空，且必须为大于0小于16的整数"), Config.DialogTitle);
                return;
            }
            else
            {
                int iChs = Convert.ToInt32(strChs);
                if (iChs < 1)
                {
                    tabControl1.SelectedTab = tabPage4;
                    txtSyncPL.Focus();
                    CMessageBox.ShowError(string.Format("默认扇区必须为大于0小于16的整数"), Config.DialogTitle);
                    return;
                }
            }

            //验证逻辑
            Setting objSetting = GetSettingFromUI();      
            if (!SysConfiger.CheckSettings(objSetting, out string strErrInfo))
            {
                if (strErrInfo.IndexOf("数据库") >= 0 && strErrInfo.IndexOf("制卡") < 0 && strErrInfo.IndexOf("监控") < 0)
                {
                    //只有数据库配置错误
                    tabControl1.SelectedTab = tabPage1;
                }
                else if (strErrInfo.IndexOf("制卡") >= 0 && strErrInfo.IndexOf("数据库") < 0 && strErrInfo.IndexOf("监控") < 0)
                {
                    //只有写卡配置错误
                    tabControl1.SelectedTab = tabPage2;
                }
                else if (strErrInfo.IndexOf("监控") >= 0 && strErrInfo.IndexOf("数据库") < 0 && strErrInfo.IndexOf("制卡") < 0)
                {
                    //只有写卡配置错误
                    tabControl1.SelectedTab = tabPage3;
                }
                CMessageBox.ShowError(string.Format("配置错误，错误如下：\r\n{0}", strErrInfo), Config.DialogTitle);
            }
            else
            {
                if (!SysConfiger.SaveSetting(objSetting, RunVariable.IniPathAndFileName, out strErrInfo))
                {
                    CMessageBox.ShowError(string.Format("对不起，保存配置失败，错误如下：\r\n{0}", strErrInfo), Config.DialogTitle);
                }
                else
                {
                    if (this._IfLoginBefore)
                    {
                        CMessageBox.ShowSucc(string.Format("恭喜您，保存配置成功！"), Config.DialogTitle);
                    }
                    else
                    {
                        Manager.GetInstance().SettingChangeNotice();
                        CMessageBox.ShowSucc(string.Format("恭喜您，保存配置成功，要使数据库的配置生效，必须重新启动系统，数据库以外的配置可即刻生效！"), Config.DialogTitle);
                    }
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private Setting GetSettingFromUI()
        {
            Setting objSetting = new Setting()
            {
                //数据库
                DBSetting = new DBSetting()
                {
                    DB_IP = Functions.FormatString(txtDBIP.Text),
                    DB_Name = Functions.FormatString(txtDBName.Text),
                    DB_User = Functions.FormatString(txtUserName.Text),
                    DB_Password = Functions.FormatString(txtUserPassword.Text)
                },
                //Com1
                WriteComProperty = new Classes.ComProperty()
            };
            objSetting.WriteComProperty.PortName = Functions.FormatString(txtPortName1.Text);
            objSetting.WriteComProperty.BaudRate = Functions.FormatInt(cbBaudRate1.Text);
            objSetting.WriteComProperty.DataBits = Functions.FormatInt(cbDataBits1.Text);
            objSetting.WriteComProperty.StopBits = cbStopBits1.Text.Equals("1") ? StopBits.One : StopBits.Two;
            objSetting.WriteComProperty.Parity = cbParity1.Text.Equals(PParity.None) ? Parity.None : (cbParity1.Text.Equals(PParity.Odd) ? Parity.Odd : Parity.Even);

            //Com2
            objSetting.MonitorComProperty = new Classes.ComProperty()
            {
                PortName = Functions.FormatString(txtPortName2.Text),
                BaudRate = Functions.FormatInt(cbBaudRate2.Text),
                DataBits = Functions.FormatInt(cbDataBits2.Text),
                StopBits = cbStopBits2.Text.Equals("1") ? StopBits.One : StopBits.Two,
                Parity = cbParity2.Text.Equals(PParity.None) ? Parity.None : (cbParity2.Text.Equals(PParity.Odd) ? Parity.Odd : Parity.Even)
            };

            //其它
            objSetting.OtherSetting = new OtherSetting()
            {
                SyncTimePL = Functions.FormatInt(txtSyncPL.Text),
                SyncAuto = Functions.FormatInt(cBSyncAuto.Checked),
                Chs = Functions.FormatInt(tBChs.Text)
            };
            return objSetting;
        }

        private void CbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IfFormLoadOk)
            {
                ComboBox cbPort = (ComboBox)sender;
                if (!cbPort.Text.Equals("请选择串口"))
                {
                    if (cbPort.Name.Equals("cbPortName1"))
                    {
                        txtPortName1.Text = cbPort.Text;
                    }
                    else
                    {
                        txtPortName2.Text = cbPort.Text;
                    }
                }
            }
        }

        private void BtnCheckDBSetting_Click(object sender, EventArgs e)
        {
            Setting objSetting = GetSettingFromUI();
            if (!SysConfiger.CheckSettings(objSetting, out string strErrInfo))
            {
                if (strErrInfo.IndexOf("数据库") >= 0)
                {
                    CMessageBox.ShowError(string.Format("数据库配置错误，错误如下：\r\n{0}", strErrInfo), Config.DialogTitle);
                    return;
                }
            }
            DBSetting objDb = objSetting.DBSetting;
            DBLinkTestForm objModal = new DBLinkTestForm(string.Format("SERVER={0};Port={1};User ID={2};Password={3};Charset={4};Database={5};allow user variables=true", objDb.DB_IP, "3308", objDb.DB_User, objDb.DB_Password, System.Text.Encoding.Default.HeaderName.ToString(), objDb.DB_Name));
            if (objModal.ShowDialog() == DialogResult.OK)
            {
                CMessageBox.ShowSucc(string.Format("恭喜您，数据库连接成功！"), Config.DialogTitle);
            }
            else if (objModal.ShowDialog() == DialogResult.No)
            {
                CMessageBox.ShowError(string.Format("对不起，数据库连接失败！"), Config.DialogTitle);
            }
        }

        private void TxtSyncPL_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断输入的值是否为数字或删除键或粘贴键22
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 46 || (e.KeyChar >= 40 && e.KeyChar <= 49)))
            {
                e.Handled = true;
            }
        }

        private void BtnUserLocalDB_Click(object sender, EventArgs e)
        {
            txtDBIP.Text = "localhost";
        }
    }
}
