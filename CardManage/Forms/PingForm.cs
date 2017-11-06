using CardManage.Logic;
using CardManage.Model;
using CardManage.Tools;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardManage.Forms
{
    public partial class PingForm : SetFormBase
    {
        private readonly int _MaxLines = 1000;
        private int _CurrentLines = 0;
                /// <summary>
        /// Ping序号
        /// </summary>
        private int _CurrentPingNum = 0;

        /// <summary>
        /// 最后一次发送的Ping数据
        /// </summary>
        private volatile DeviceAddress _LastDeviceAddress; //volatile 变量级别同步

        private int _TimerWaitTimes = 0; 
        protected PingForm()
        {
            InitializeComponent();
        }

        public PingForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

        private void PingForm_Load(object sender, EventArgs e)
        {
            CardMonitor.GetInstance().OnPingNotice += new CardMonitor.PingNoticeHandler(OnPingNotice);
            CardMonitor.GetInstance().OnPingResponse += new CardMonitor.PingResponseHandler(OnPingResponse);
            CardMonitor.GetInstance().OnMainDeviceACK4PingResponse += new CardMonitor.MainDeviceACK4PingResponse(OnMainDeviceACK4PingResponse);

            cbDeviceType.Items.Add(new ComboBoxItem("管理机", 1));
            cbDeviceType.Items.Add(new ComboBoxItem("交换机", 2));
            cbDeviceType.Items.Add(new ComboBoxItem("切换器", 3));
            cbDeviceType.Items.Add(new ComboBoxItem("围墙刷卡头", 4));
            cbDeviceType.Items.Add(new ComboBoxItem("围墙机", 5));
            cbDeviceType.Items.Add(new ComboBoxItem("门口机", 6));
            cbDeviceType.Items.Add(new ComboBoxItem("二次门口机", 7));
            cbDeviceType.SelectedIndex = 0;

            timer1.Interval = 1000;//毫秒为单位
            timer1.Enabled = false;
        }

        private void PingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardMonitor.GetInstance().OnPingNotice -= new CardMonitor.PingNoticeHandler(OnPingNotice);
            CardMonitor.GetInstance().OnPingResponse -= new CardMonitor.PingResponseHandler(OnPingResponse);
            CardMonitor.GetInstance().OnMainDeviceACK4PingResponse -= new CardMonitor.MainDeviceACK4PingResponse(OnMainDeviceACK4PingResponse);

            timer1.Enabled = false;
        }

        /// <summary>
        /// 回调方法-当有Ping设备时
        /// </summary>
        /// <param name="strNotice"></param>
        /// <param name="bArrBuffer"></param>
        private void OnPingNotice(string strNotice, byte[] bArrBuffer)
        {
            if (!(string.IsNullOrEmpty(strNotice) || strNotice.Equals("")))
            {
                string strContent = (RunVariable.IfDebug && bArrBuffer != null) ? string.Format("{0}:{1}", strNotice, ScaleConverter.ByteArr2HexStr(bArrBuffer)) : strNotice;
                ShowNotice(string.Format("{0}   {1}", DateTime.Now.ToString(Config.LongTimeFormat), strContent));
            }
        }
        
        private void OnMainDeviceACK4PingResponse(DeviceAddress objDeviceAddress, string strErrInfo)
        {
            if (!(objDeviceAddress == null || !string.IsNullOrEmpty(strErrInfo)))
            {
                if (this._LastDeviceAddress != null)
                {
                    if (this._LastDeviceAddress.DeviceNo.Equals(objDeviceAddress.DeviceNo) && this._LastDeviceAddress.DeviceType.Equals(objDeviceAddress.DeviceType))
                    {
                        //2017-07-13 11:14:17   接收从机链路层ACK指令:55 AA 00 0B 92 06 01 01 00 00 01 00 00 00 00 57 
                        string strContent = string.Format("接收从机链路层ACK指令");
                        strContent = (RunVariable.IfDebug && objDeviceAddress.Buffer != null) ? string.Format("{0}:{1}", strContent, ScaleConverter.ByteArr2HexStr(objDeviceAddress.Buffer)) : strContent;
                        ShowNotice(string.Format("{0}   {1}", DateTime.Now.ToString(Config.LongTimeFormat), strContent));
                        
                        //this._LastDeviceAddress = null;
                        timer1.Enabled = false;
                    }
                }
            }
        }

        private void OnPingResponse(DeviceAddress objDeviceAddress, string strErrInfo)
        {
            if (!(objDeviceAddress == null || !string.IsNullOrEmpty(strErrInfo)))
            {
                if (this._LastDeviceAddress != null)
                {
                    if (this._LastDeviceAddress.DeviceNo.Equals(objDeviceAddress.DeviceNo) && this._LastDeviceAddress.DeviceType.Equals(objDeviceAddress.DeviceType) && this._LastDeviceAddress.PingNum.Equals(objDeviceAddress.PingNum))
                    {
                        string strContent = string.Format("ping {0}{1}成功 !", GetDeviceTypeDesc(objDeviceAddress.DeviceType), objDeviceAddress.DeviceNo);
                        strContent = (RunVariable.IfDebug && objDeviceAddress.Buffer != null) ? string.Format("{0}:{1}", strContent, ScaleConverter.ByteArr2HexStr(objDeviceAddress.Buffer)) : strContent;
                        ShowNotice(string.Format("{0}   {1}", DateTime.Now.ToString(Config.LongTimeFormat), strContent));
                        
                        this._LastDeviceAddress = null;
                        //timer1.Enabled = false;
                    }
                }
            }
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
                    tbxComunicateData.Text = string.Format("{0}{1}{2}", strMsg, strNR, tbxComunicateData.Text);
                }));
            }
            catch { }
        }

        private void MaskAddressText(object sender, KeyPressEventArgs e)
        {
            //a-f:97-102
            //A-F:65-70

            //判断输入的值是否为数字、16进制字符、冒号或删除键或粘贴键22
            if (char.IsDigit(e.KeyChar) || (e.KeyChar >= 97 && e.KeyChar <= 102) || (e.KeyChar >= 65 && e.KeyChar <= 70) || e.KeyChar == 8 || e.KeyChar == 58 || e.KeyChar == 22)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            string strDeviceAddress = txtDeviceAddress.Text.Trim();
            string strOringinalDeviceAddress = strDeviceAddress;
            if (strDeviceAddress.Equals(""))
            {
                txtDeviceAddress.Focus();
                CMessageBox.ShowError(string.Format("设备地址必须填写，且必须是16进制字符！"), Config.DialogTitle);
                return;
            }
            if (strDeviceAddress.Length > 8) strDeviceAddress = strDeviceAddress.Substring(0, 8);
            strDeviceAddress = strDeviceAddress.PadRight(8, '0');

            if (!CheckDeviceAddress(strDeviceAddress))
            {
                txtDeviceAddress.Focus();
                CMessageBox.ShowError(string.Format("设备地址必须填写，且必须是16进制字符！"), Config.DialogTitle);
                return;
            }
#if DEBUG
            _CurrentPingNum = 1;
#else
            this._CurrentPingNum++;
#endif
            if (this._CurrentPingNum < 0 || this._CurrentPingNum > 9999) this._CurrentPingNum = 0;
            DeviceAddress objDeviceAddress = new DeviceAddress()
            {
                DeviceNo = strDeviceAddress,
                OriginalDeviceNo = strOringinalDeviceAddress,
                PingNum = this._CurrentPingNum,
                DeviceType = Convert.ToInt32(((ComboBoxItem)cbDeviceType.SelectedItem).Value)
            };
            
            if (!CardMonitor.GetInstance().Ping(objDeviceAddress, out string strErrInfo))
            {
                Tools.CMessageBox.ShowError(string.Format("对不起，ping设备失败，错误原因:\r\n{0}！", strErrInfo), Config.DialogTitle);
            }
            else
            {
                //浅表副本 复制给定时任务 用于失败重试
                _LastDeviceAddress = (DeviceAddress)objDeviceAddress.Clone();
                timer1.Enabled = true;
            }
        }

        private bool CheckDeviceAddress(string strDeviceAddress)
        {
            bool bIfOk = false;
            if (!string.IsNullOrEmpty(strDeviceAddress))
            {
                strDeviceAddress = strDeviceAddress.Trim();
                if (!strDeviceAddress.Equals(""))
                {
                    try
                    {
                        byte[] bArrDeviceAddress = ScaleConverter.HexStr2ByteArr(strDeviceAddress);
                        if (bArrDeviceAddress.Length >= 1 && bArrDeviceAddress.Length <= 4) bIfOk = true;
                    }
                    catch
                    {

                    }
                }
            }
            return bIfOk;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (this._LastDeviceAddress != null)
            {
                if (this._TimerWaitTimes == 3)
                {
                    string strContent = string.Format("ping {0}{1}失败 !", GetDeviceTypeDesc(this._LastDeviceAddress.DeviceType), this._LastDeviceAddress.OriginalDeviceNo);
                    ShowNotice(string.Format("{0}   {1}", DateTime.Now.ToString(Config.LongTimeFormat), strContent));
                    this._LastDeviceAddress = null;
                    this._TimerWaitTimes++;
                    timer1.Enabled = false;
                }
                else if (this._TimerWaitTimes < 3)
                {
                    this._TimerWaitTimes++;
                    CardMonitor.GetInstance().Ping(_LastDeviceAddress, out string strErrInfo);
                }
                else
                {
                    this._TimerWaitTimes = 0;
                }
            }
        }

        private void TsbClearDebugContent_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清空消息窗口中的内容吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            tbxComunicateData.Clear();
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

        private void TsbCopyDebugContent_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(Regex.Replace(this.tbxComunicateData.Text, "\n", "\r\n", RegexOptions.IgnoreCase));
        }
    }
}
