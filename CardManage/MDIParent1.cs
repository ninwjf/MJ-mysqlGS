using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CardManage.Logic;
using CardManage.Forms;
using CardManage.Model;
using CardManage.Tools;
using System.IO;
using System.Threading;
namespace CardManage
{
    public partial class MDIParent1 : Form
    {
        public MDIParent1()
        {
            InitializeComponent();
            this.Text = string.Format("{0} {1}", Config.SoftName, Config.SoftVersion);            
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            //总管理器
            Manager.GetInstance().OnSettingChange += new Manager.SettingChangeHandler(OnSettingChange);

            //配置器
            //CardConfiger.GetInstance().OnNotice += new ConfigerBase.NoticeHandler(CardConfiger_OnNotice);
            CardConfiger.GetInstance().OnStartFault += new ConfigerBase.StartFaultHandler(CardConfiger_OnStartFault);
            CardConfiger.GetInstance().OnStartSucc += new ConfigerBase.StartSuccHandler(CardConfiger_OnStartSucc);
            CardConfiger.GetInstance().OnStopFault += new ConfigerBase.StopFaultHandler(CardConfiger_OnStopFault);
            CardConfiger.GetInstance().OnStopSucc += new ConfigerBase.StopSuccHandler(CardConfiger_OnStopSucc);

            //监控器
            CardMonitor.GetInstance().OnStartFault += new ConfigerBase.StartFaultHandler(CardMonitor_OnStartFault);
            CardMonitor.GetInstance().OnStartSucc += new ConfigerBase.StartSuccHandler(CardMonitor_OnStartSucc);
            CardMonitor.GetInstance().OnStopFault += new ConfigerBase.StopFaultHandler(CardMonitor_OnStopFault);
            CardMonitor.GetInstance().OnStopSucc += new ConfigerBase.StopSuccHandler(CardMonitor_OnStopSucc);


            this.Hide();
            //全局变量初始化
            //运行配置
            RunVariable.IfDebug = GetDebugStatus();//是否是调试模式，如果是调试模式则在已发送的命令输出区输出指令码

            RunVariable.IniPathAndFileName = string.Format("{0}\\{1}", Application.StartupPath, Config.IniFileName);
            string strErrInfo = "";

            bool bIfLoop = true;
            DialogResult dResult;
            string strErrMessage = "";

            //1.创建数据库或使用现有数据库
            //1.判断文件是否在
            if (!IOHelper.Exists(RunVariable.IniPathAndFileName))
            {
                dResult = MessageBox.Show("未找到数据库配置文件，进入系统前必须配置好数据库，如需新建数据库请按[是]键，使用现有的数据库请按[否]键，退出系统请按[取消]键？", Config.DialogTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dResult == DialogResult.Yes)
                {
                    //创建数据库
                    DBServerSettingForm objModalDBServerForm = new DBServerSettingForm("数据库服务器配置", true, RunVariable.CurrentUserInfo, new WindowSize(365, 288));
                    dResult = objModalDBServerForm.ShowDialog();
                    if (dResult == DialogResult.OK)
                    {
                        //连接成功,创建数据库
                        DBSetting objDBSetting = objModalDBServerForm.GetDBSetting();
                        if (objDBSetting != null)
                        {
                            string strSqlContent = Properties.Resources.db;
                            DBManager objDM = new DBManager(objDBSetting.DB_IP, objDBSetting.DB_User, objDBSetting.DB_Password);
                            bool bIfCanLink = objDM.CheckCanLink();
                            if (bIfCanLink)
                            {
                                string strDBName = Config.DefaultDBName;
                                if (!objDM.IsExists(strDBName))
                                {
                                    bool bIfCreateSucc = objDM.CreateBySql(strDBName, strSqlContent, out strErrMessage);
                                    if (!bIfCreateSucc)
                                    {
                                        //退出系统
                                        CMessageBox.ShowError(string.Format("数据库创建失败，请重新开启软件重试，错误原因\r\n{0}", strErrMessage), Config.DialogTitle);
                                        bIfLoop = false;
                                        Application.Exit();
                                    }
                                    else
                                    {
                                        CMessageBox.ShowSucc("恭喜你，数据库创建成功！", Config.DialogTitle);
                                    }
                                }
                                Model.Setting objAllSetting = new Setting();
                                //数据库
                                objAllSetting.DBSetting = new DBSetting();
                                objAllSetting.DBSetting.DB_IP = objDBSetting.DB_IP;
                                objAllSetting.DBSetting.DB_Name = strDBName;
                                objAllSetting.DBSetting.DB_User = objDBSetting.DB_User;
                                objAllSetting.DBSetting.DB_Password = objDBSetting.DB_Password;
                                //制卡串口
                                objAllSetting.WriteComProperty = new Classes.ComProperty();
                                objAllSetting.WriteComProperty.BaudRate = 9600;
                                objAllSetting.WriteComProperty.DataBits = 8;
                                objAllSetting.WriteComProperty.StopBits = System.IO.Ports.StopBits.One;
                                objAllSetting.WriteComProperty.Parity = System.IO.Ports.Parity.None;
                                //监控串口
                                objAllSetting.MonitorComProperty = new Classes.ComProperty();
                                objAllSetting.MonitorComProperty.BaudRate = 19200;
                                objAllSetting.MonitorComProperty.DataBits = 8;
                                objAllSetting.MonitorComProperty.StopBits = System.IO.Ports.StopBits.One;
                                objAllSetting.MonitorComProperty.Parity = System.IO.Ports.Parity.None;
                                //其他配置
                                objAllSetting.OtherSetting = new OtherSetting();
                                objAllSetting.OtherSetting.SyncTimePL = 10;
                                objAllSetting.OtherSetting.Chs = 1;
                                objAllSetting.OtherSetting.SyncAuto = 0;

                                SysConfiger.SaveSetting(objAllSetting, RunVariable.IniPathAndFileName, out strErrMessage);
                            }
                            else
                            {
                                //退出系统
                                CMessageBox.ShowError("数据库服务器连接失败，请重新开启软件重试！", Config.DialogTitle);
                                bIfLoop = false;
                                Application.Exit();
                            }
                        }
                        else
                        {
                            //退出系统
                            bIfLoop = false;
                            Application.Exit();
                        }
                    }
                    else
                    {
                        //退出系统
                        bIfLoop = false;
                        Application.Exit();
                    }
                }
                else if (dResult == DialogResult.No)
                {
                    //使用现有的数据库
                    //弹出配置串口
                    DBSettingForm objModalSysForm = new DBSettingForm("数据库设置", true, RunVariable.CurrentUserInfo, new WindowSize(382, 339));
                    if (objModalSysForm.ShowDialog() != DialogResult.OK)
                    {
                        bIfLoop = false;
                        Application.Exit();
                    }
                    else
                    {
                        bIfLoop = true;
                    }
                }
                else
                {
                    //退出系统
                    bIfLoop = false;
                    Application.Exit();
                }
            }

            //2.加载配置文件
            while (bIfLoop)
            {
                //2.配置文件载入验证
                RunVariable.CurrentSetting = SysConfiger.LoadSetting(RunVariable.IniPathAndFileName, out strErrInfo);
                if (RunVariable.CurrentSetting == null || !strErrInfo.Equals(""))
                {
                    CMessageBox.ShowError(string.Format("无法载入系统配置文件,错误信息如下：\r\n{0}", strErrInfo), Config.DialogTitle);
                    bIfLoop = false;
                    Application.Exit();
                }

                //3.数据库连接验证
                DBSetting objDb = RunVariable.CurrentSetting.DBSetting;
                
                string strConnectStringTest = string.Format("SERVER={0};Port={1};User ID={2};Password={3};Charset={4};Database={5};allow user variables=true", objDb.DB_IP, "3308", objDb.DB_User, objDb.DB_Password, Encoding.Default.HeaderName.ToString(), objDb.DB_Name);
                string strConnectStringReal = string.Format("SERVER={0};Port={1};User ID={2};Password={3};Charset={4};Database={5};allow user variables=true", objDb.DB_IP, "3308", objDb.DB_User, objDb.DB_Password, System.Text.Encoding.Default.HeaderName.ToString(), objDb.DB_Name);
                DBLinkTestForm objDBLinkModal = new DBLinkTestForm(strConnectStringTest);
                if (objDBLinkModal.ShowDialog() == DialogResult.OK)
                {
                    //数据库连接成功
                    RunVariable.ConnectionString = strConnectStringReal;
                    CardManage.DBUtility.DbHelperSQL.connectionString = RunVariable.ConnectionString;
                    LoginForm objLoginForm = new LoginForm("用户登录", true, null, new Model.WindowSize(350, 260));
                    if (objLoginForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        LayoutManager.GetInstance().InitLayout(this);
                        CardConfiger.GetInstance().Start(RunVariable.CurrentSetting.WriteComProperty);
                        CardMonitor.GetInstance().Start(RunVariable.CurrentSetting.MonitorComProperty);
                        CardMonitor.GetInstance().SetSyncTimePL(RunVariable.CurrentSetting.OtherSetting.SyncTimePL * 60);
                        CardMonitor.GetInstance().StartTask(RunVariable.CurrentSetting.OtherSetting.SyncAuto);
                        UpdateStatusDesc();
                        this.Show();
                        bIfLoop = false;
                    }
                    else
                    {
                        bIfLoop = false;
                        Application.Exit();
                    }
                }
                else if (objDBLinkModal.ShowDialog() == DialogResult.No)
                {
                    if (MessageBox.Show("数据库连接失败，需要配置好数据库信息才能进入系统，如需马上配置请按[是]键，退出系统请按[否]键？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //弹出配置串口
                        DBSettingForm objModalSysForm = new DBSettingForm("数据库设置", true, RunVariable.CurrentUserInfo, new WindowSize(382, 339));
                        if (objModalSysForm.ShowDialog() != DialogResult.OK)
                        {
                            bIfLoop = false;
                            Application.Exit();
                        }
                    }
                    else
                    {
                        bIfLoop = false;
                        Application.Exit();
                    }
                }
                else
                {
                    bIfLoop = false;
                    Application.Exit();
                }
            }
        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardMonitor.GetInstance().StopTask();
            //总管理器
            Manager.GetInstance().OnSettingChange -= new Manager.SettingChangeHandler(OnSettingChange);
            //配置器
            CardConfiger.GetInstance().OnStartFault -= new ConfigerBase.StartFaultHandler(CardConfiger_OnStartFault);
            CardConfiger.GetInstance().OnStartSucc -= new ConfigerBase.StartSuccHandler(CardConfiger_OnStartSucc);
            CardConfiger.GetInstance().OnStopFault -= new ConfigerBase.StopFaultHandler(CardConfiger_OnStopFault);
            CardConfiger.GetInstance().OnStopSucc -= new ConfigerBase.StopSuccHandler(CardConfiger_OnStopSucc);

            //监控器
            CardMonitor.GetInstance().OnStartFault -= new ConfigerBase.StartFaultHandler(CardMonitor_OnStartFault);
            CardMonitor.GetInstance().OnStartSucc -= new ConfigerBase.StartSuccHandler(CardMonitor_OnStartSucc);
            CardMonitor.GetInstance().OnStopFault -= new ConfigerBase.StopFaultHandler(CardMonitor_OnStopFault);
            CardMonitor.GetInstance().OnStopSucc -= new ConfigerBase.StopSuccHandler(CardMonitor_OnStopSucc);
        }

        /// <summary>
        /// 回调方法-配置器当打开失败时
        /// </summary>
        /// <param name="strErrorMessage"></param>
        private void CardConfiger_OnStartFault(string strErrorMessage)
        {
            UpdateStatusDesc();
        }

        /// <summary>
        /// 回调方法-配置器当成功打开时
        /// </summary>
        /// <param name="strNotice"></param>
        private void CardConfiger_OnStartSucc()
        {
            UpdateStatusDesc();
        }

        /// <summary>
        /// 回调方法-配置器当停止失败时
        /// </summary>
        /// <param name="strErrorMessage"></param>
        private void CardConfiger_OnStopFault(string strErrorMessage)
        {
            UpdateStatusDesc();
        }

        /// <summary>
        /// 回调方法-配置器当成功停止时
        /// </summary>
        /// <param name="strNotice"></param>
        private void CardConfiger_OnStopSucc()
        {
            UpdateStatusDesc();
        }

        /// <summary>
        /// 回调方法-监控器当打开失败时
        /// </summary>
        /// <param name="strErrorMessage"></param>
        private void CardMonitor_OnStartFault(string strErrorMessage)
        {
            UpdateStatusDesc();
        }

        /// <summary>
        /// 回调方法-监控器当成功打开时
        /// </summary>
        /// <param name="strNotice"></param>
        private void CardMonitor_OnStartSucc()
        {
            UpdateStatusDesc();
        }

        /// <summary>
        /// 回调方法-监控器当停止失败时
        /// </summary>
        /// <param name="strErrorMessage"></param>
        private void CardMonitor_OnStopFault(string strErrorMessage)
        {
            UpdateStatusDesc();
        }

        /// <summary>
        /// 回调方法-监控器当成功停止时
        /// </summary>
        /// <param name="strNotice"></param>
        private void CardMonitor_OnStopSucc()
        {
            UpdateStatusDesc();
        }

        private void OnSettingChange()
        {
            string strErrInfo = "";
            Setting objSetting = SysConfiger.LoadSetting(RunVariable.IniPathAndFileName, out strErrInfo);
            if (objSetting != null && strErrInfo.Equals(""))
            {
                RunVariable.CurrentSetting = objSetting;
                //先关闭串口
                CardConfiger.GetInstance().Stop();
                CardMonitor.GetInstance().Stop();
                Thread.Sleep(500);
                CardConfiger.GetInstance().Start(RunVariable.CurrentSetting.WriteComProperty);
                CardMonitor.GetInstance().Start(RunVariable.CurrentSetting.MonitorComProperty);
                CardMonitor.GetInstance().SetSyncTimePL(RunVariable.CurrentSetting.OtherSetting.SyncTimePL * 60);
                CardMonitor.GetInstance().StartTask(RunVariable.CurrentSetting.OtherSetting.SyncAuto);
                UpdateStatusDesc();
            }
        }

        private void UpdateStatusDesc()
        {
            string strMessage = "";

            string strSynCTimeDesc = "时间同步频率：10分钟";
            string strDBDesc = "数据库：[地址：,数据库：]";
            string strWComDesc = "写串口：无信息";
            string strMComDesc = "监控串口：无信息";
            if (RunVariable.CurrentSetting != null)
            {
                Classes.ComProperty objCom;
                if (RunVariable.CurrentSetting.DBSetting != null)
                {
                    OtherSetting objOtherSetting = RunVariable.CurrentSetting.OtherSetting;
                    strSynCTimeDesc = string.Format("时间同步频率：{0}分钟", objOtherSetting.SyncTimePL);
                }
                if (RunVariable.CurrentSetting.DBSetting != null)
                {
                    DBSetting objSetting = RunVariable.CurrentSetting.DBSetting;
                    strDBDesc = string.Format("数据库：{0},数据库：{1}", objSetting.DB_IP, objSetting.DB_Name);
                }
                if (RunVariable.CurrentSetting.WriteComProperty != null)
                {
                    objCom = RunVariable.CurrentSetting.WriteComProperty;
                    strWComDesc = string.Format("制卡串口：{0},{1},{2},{3},{4},关闭", objCom.PortName, objCom.BaudRate, objCom.DataBits, GetParityDesc(objCom.Parity), objCom.StopBits);
                }
                if (RunVariable.CurrentSetting.MonitorComProperty != null)
                {
                    objCom = RunVariable.CurrentSetting.MonitorComProperty;
                    strMComDesc = string.Format("监控串口：{0},{1},{2},{3},{4},关闭", objCom.PortName, objCom.BaudRate, objCom.DataBits, GetParityDesc(objCom.Parity), objCom.StopBits);
                }

                if (CardConfiger.GetInstance().ComInfo != null)
                {
                    ComInfo objCInfo = CardConfiger.GetInstance().ComInfo;
                    if (objCInfo.ComProperty != null)
                    {
                        objCom = objCInfo.ComProperty;
                        strWComDesc = string.Format("制卡串口：{0},{1},{2},{3},{4},{5}", objCom.PortName, objCom.BaudRate, objCom.DataBits, GetParityDesc(objCom.Parity), objCom.StopBits, (objCInfo.IfOpen ? "开启" : "关闭"));
                    }
                }
                if (CardMonitor.GetInstance().ComInfo != null)
                {
                    ComInfo objCInfo = CardMonitor.GetInstance().ComInfo;
                    if (objCInfo.ComProperty != null)
                    {
                        objCom = objCInfo.ComProperty;
                        strMComDesc = string.Format("监控串口：{0},{1},{2},{3},{4},{5}", objCom.PortName, objCom.BaudRate, objCom.DataBits, GetParityDesc(objCom.Parity), objCom.StopBits, (objCInfo.IfOpen ? "开启" : "关闭"));
                    }
                }
            }
            strMessage = string.Format("配置信息=><{0}><{1}><{2}><{3}>", strSynCTimeDesc, strDBDesc, strWComDesc, strMComDesc);
            if (RunVariable.CurrentUserInfo != null)
            {
                strMessage = string.Format("当前用户：{0}({1})   {2}", RunVariable.CurrentUserInfo.UserName, (RunVariable.CurrentUserInfo.Flag.Equals(0) ? "超级管理员" : "普通管理员"), strMessage);
            }
            else
            {
                strMessage = string.Format("当前用户：未知(未知)    {0}", strMessage);
            }
            LayoutManager.GetInstance().SetStatusDesc(strMessage);
        }

        private bool GetDebugStatus()
        {
            bool bIfDebug = false;
            try
            {
                bIfDebug = File.Exists(Application.StartupPath + "\\" + Config.DebugSafeFileName);
            }
            catch
            {
                bIfDebug = false;
            }
            return bIfDebug;
        }

        private string GetParityDesc(System.IO.Ports.Parity parity)
        {
            string strDesc = "未知校验";
            switch (parity)
            {
                case System.IO.Ports.Parity.None:
                    strDesc = "校验无";
                    break;
                case System.IO.Ports.Parity.Even:
                    strDesc = "偶校验";
                    break;
                case System.IO.Ports.Parity.Odd:
                    strDesc = "奇校验";
                    break;
            }
            return strDesc;
        }
    }
}
