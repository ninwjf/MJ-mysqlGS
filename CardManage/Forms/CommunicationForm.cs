using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using CardManage.Tools;
using CardManage.Logic;

namespace CardManage.Forms
{
    /// <summary>
    /// 实时通讯
    /// </summary>
    public partial class CommunicationForm : CardManage.Forms.FormBase
    {
        private readonly int _MaxLines = 1000;
        private int _CurrentLines = 0;
        protected CommunicationForm()
        {
            InitializeComponent();
        }

        public CommunicationForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

        private void CommunicationForm_Load(object sender, EventArgs e)
        {
            CardMonitor.GetInstance().OnNotice += new ConfigerBase.NoticeHandler(CommunicationForm_OnNotice);
            CardMonitor.GetInstance().OnStartFault += new ConfigerBase.StartFaultHandler(CommunicationForm_OnStartFault);
            CardMonitor.GetInstance().OnStartSucc += new ConfigerBase.StartSuccHandler(CommunicationForm_OnStartSucc);
        }

        private void CommunicationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardMonitor.GetInstance().OnNotice -= new ConfigerBase.NoticeHandler(CommunicationForm_OnNotice);
            CardMonitor.GetInstance().OnStartFault -= new ConfigerBase.StartFaultHandler(CommunicationForm_OnStartFault);
            CardMonitor.GetInstance().OnStartSucc -= new ConfigerBase.StartSuccHandler(CommunicationForm_OnStartSucc);
        }

        /// <summary>
        /// 回调方法-监控器接收时
        /// </summary>
        /// <param name="strNotice"></param>
        /// <param name="bArrBuffer"></param>
        private void CommunicationForm_OnNotice(string strNotice, byte[] bArrBuffer)
        {
            if (!(string.IsNullOrEmpty(strNotice) || strNotice.Equals("")))
            {
                string strContent = (RunVariable.IfDebug && bArrBuffer != null) ? string.Format("{0}:{1}", strNotice, ScaleConverter.ByteArr2HexStr(bArrBuffer)) : strNotice;
                ShowNotice(string.Format("{0}   {1}", DateTime.Now.ToString(Config.LongTimeFormat), strContent));
            }
        }

        /// <summary>
        /// 回调方法-监控器当打开失败时
        /// </summary>
        /// <param name="strErrorMessage"></param>
        private void CommunicationForm_OnStartFault(string strErrorMessage)
        {
            ShowNotice(string.Format("串口开启失败，错误原因如下：\r\n{0}", strErrorMessage));
        }

        /// <summary>
        /// 回调方法-监控器当成功打开时
        /// </summary>
        /// <param name="strNotice"></param>
        private void CommunicationForm_OnStartSucc()
        {
            //CMessageBox.ShowSucc(string.Format("恭喜您，串口开启成功！"), Config.DialogTitle);
        }

        /// <summary>
        /// 输出接受到的数据
        /// </summary>
        /// <param name="strMsg"></param>
        private void ShowNotice(string strMsg, bool bIfBR = true)
        {
            try
            {
                this.Invoke(new EventHandler(delegate
                {
                    if (this._CurrentLines >= this._MaxLines)
                    {
                        tbxComunicateData.Clear();
                        this._CurrentLines = 0;
                    }
                    this._CurrentLines++;
                    string strNR = (bIfBR) ? "\r\n" : "";
                    tbxComunicateData.Text = string.Format("{0}{1}{2}", tbxComunicateData.Text, strNR, strMsg);
                }));
            }
            catch { }
        }

        private void TbxComunicateData_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //自动滚动到最底部
                tbxComunicateData.SelectionStart = tbxComunicateData.Text.Length;
                tbxComunicateData.ScrollToCaret();
            }
            catch { }
        }

        private void TsbCopyDebugContent_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(Regex.Replace(this.tbxComunicateData.Text, "\n", "\r\n", RegexOptions.IgnoreCase));
        }

        private void TsbSaveDebugContent_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                Filter = " txt files(*.txt)|*.txt",//设置文件类型
                RestoreDirectory = true//保存对话框是否记忆上次打开的目录
            };
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter aWriter = new StreamWriter(saveFileDialog1.OpenFile());
                    aWriter.Write(Regex.Replace(this.tbxComunicateData.Text, "\n", "\r\n", RegexOptions.IgnoreCase));
                    aWriter.Close();
                    aWriter = null;
                    CMessageBox.ShowSucc("保存文件成功！", Config.DialogTitle);
                }
                catch (Exception err)
                {
                    CMessageBox.ShowError(string.Format("保存文件失败，错误原因：{0}", err), Config.DialogTitle);
                }
            }
        }

        private void TsbClearDebugContent_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清空调试窗口中的内容吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            tbxComunicateData.Clear();
        }

        private void TsbClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要关闭窗口吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            this.Close();
        }
    }
}
