using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CardManage.Logic;
using CardManage.Model;
using CardManage.Tools;

namespace CardManage.Forms
{
    public partial class ComManageForm : SetFormBase
    {
         protected ComManageForm()
        {
            InitializeComponent();
        }

         public ComManageForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

         private void ComManageForm_Load(object sender, EventArgs e)
         {
             //配置器
             CardConfiger.GetInstance().OnStartFault += new ConfigerBase.StartFaultHandler(CardConfiger_OnStartFault);
             CardConfiger.GetInstance().OnStartSucc += new ConfigerBase.StartSuccHandler(CardConfiger_OnStartSucc);
             CardConfiger.GetInstance().OnStopFault += new ConfigerBase.StopFaultHandler(CardConfiger_OnStopFault);
             CardConfiger.GetInstance().OnStopSucc += new ConfigerBase.StopSuccHandler(CardConfiger_OnStopSucc);

             //监控器
             CardMonitor.GetInstance().OnStartFault += new ConfigerBase.StartFaultHandler(CardMonitor_OnStartFault);
             CardMonitor.GetInstance().OnStartSucc += new ConfigerBase.StartSuccHandler(CardMonitor_OnStartSucc);
             CardMonitor.GetInstance().OnStopFault += new ConfigerBase.StopFaultHandler(CardMonitor_OnStopFault);
             CardMonitor.GetInstance().OnStopSucc += new ConfigerBase.StopSuccHandler(CardMonitor_OnStopSucc);

             RenderLayout();
         }

         private void ComManageForm_FormClosing(object sender, FormClosingEventArgs e)
         {
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
        /// 渲染界面
        /// </summary>
         private void RenderLayout()
         {
             try
             {
                 lbWriteComStauts.Text = lbMonitorComStauts.Text = "关闭"; ;
                 btnWriteComOpenOrClose.Text = btnMonitorComOpenOrClose.Text = "开启(&O)";
                 if (CardConfiger.GetInstance().ComInfo != null)
                 {
                     ComInfo objCInfo = CardConfiger.GetInstance().ComInfo;
                     if (objCInfo.ComProperty != null)
                     {
                         Classes.ComProperty objCom = objCInfo.ComProperty;
                         btnWriteComOpenOrClose.Text = (objCInfo.IfOpen ? "关闭(&C)" : "开启(&O)");
                         lbWriteComStauts.Text = string.Format("{0},{1},{2},{3},{4},{5}", objCom.PortName.ToUpper(), objCom.BaudRate, objCom.DataBits, GetParityDesc(objCom.Parity), objCom.StopBits, (objCInfo.IfOpen ? "开启" : "关闭"));
                     }
                 }
                 if (CardMonitor.GetInstance().ComInfo != null)
                 {
                     ComInfo objCInfo = CardMonitor.GetInstance().ComInfo;
                     if (objCInfo.ComProperty != null)
                     {
                         Classes.ComProperty objCom = objCInfo.ComProperty;
                         btnMonitorComOpenOrClose.Text = (objCInfo.IfOpen ? "关闭(&C)" : "开启(&O)");
                         lbMonitorComStauts.Text = string.Format("{0},{1},{2},{3},{4},{5}", objCom.PortName.ToUpper(), objCom.BaudRate, objCom.DataBits, GetParityDesc(objCom.Parity), objCom.StopBits, (objCInfo.IfOpen ? "开启" : "关闭"));
                     }
                 }
             }
             catch
             {

             }
         }

         /// <summary>
         /// 回调方法-配置器当打开失败时
         /// </summary>
         /// <param name="strErrorMessage"></param>
         private void CardConfiger_OnStartFault(string strErrorMessage)
         {
             RenderLayout();
             CMessageBox.ShowError(string.Format("写卡串口开启失败，请查看您的配置，错误如下：\r\n{0}", strErrorMessage), Config.DialogTitle);  
         }

         /// <summary>
         /// 回调方法-配置器当成功打开时
         /// </summary>
         /// <param name="strNotice"></param>
         private void CardConfiger_OnStartSucc()
         {
             RenderLayout();
         }

         /// <summary>
         /// 回调方法-配置器当停止失败时
         /// </summary>
         /// <param name="strErrorMessage"></param>
         private void CardConfiger_OnStopFault(string strErrorMessage)
         {
             RenderLayout();
             CMessageBox.ShowError(string.Format("写卡串口停止失败，请查看您的配置，错误如下：\r\n{0}", strErrorMessage), Config.DialogTitle);   
         }

         /// <summary>
         /// 回调方法-配置器当成功停止时
         /// </summary>
         /// <param name="strNotice"></param>
         private void CardConfiger_OnStopSucc()
         {
             RenderLayout();           
         }

         /// <summary>
         /// 回调方法-监控器当打开失败时
         /// </summary>
         /// <param name="strErrorMessage"></param>
         private void CardMonitor_OnStartFault(string strErrorMessage)
         {
             RenderLayout();
             CMessageBox.ShowError(string.Format("监控串口开启失败，错误如下：\r\n{0}", strErrorMessage), Config.DialogTitle);
         }

         /// <summary>
         /// 回调方法-监控器当成功打开时
         /// </summary>
         /// <param name="strNotice"></param>
         private void CardMonitor_OnStartSucc()
         {
             RenderLayout();
         }

         /// <summary>
         /// 回调方法-监控器当停止失败时
         /// </summary>
         /// <param name="strErrorMessage"></param>
         private void CardMonitor_OnStopFault(string strErrorMessage)
         {
             RenderLayout();
             CMessageBox.ShowError(string.Format("监控串口关闭失败，错误如下：\r\n{0}", strErrorMessage), Config.DialogTitle);
         }

         /// <summary>
         /// 回调方法-监控器当成功停止时
         /// </summary>
         /// <param name="strNotice"></param>
         private void CardMonitor_OnStopSucc()
         {
             RenderLayout();             
         }

         private void BtnWriteComOpenOrClose_Click(object sender, EventArgs e)
         {
             if (btnWriteComOpenOrClose.Text.Equals("开启(&O)"))
             {
                 CardConfiger.GetInstance().Start(RunVariable.CurrentSetting.WriteComProperty);
             }
             else
             {
                 if (MessageBox.Show("关闭制卡串口会导致无法读写卡，确定要关闭制卡串口吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                 CardConfiger.GetInstance().Stop();
             }
         }

         private void BtnMonitorComOpenOrClose_Click(object sender, EventArgs e)
         {
             if (btnMonitorComOpenOrClose.Text.Equals("开启(&O)"))
             {
                 CardMonitor.GetInstance().Start(RunVariable.CurrentSetting.MonitorComProperty);
             }
             else
             {
                 if (MessageBox.Show("关闭监控串口会导致系统无法接收数据，确定要关闭监控串口吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                 CardMonitor.GetInstance().Stop();
             }
        }
    }
}
