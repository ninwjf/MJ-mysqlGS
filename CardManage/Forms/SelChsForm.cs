using CardManage.Logic;
using CardManage.Model;
using CardManage.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CardManage.Forms
{
    public partial class SelChsForm : FormBase
    {
        List<RadioButton> rbList;
        //公司扇区
        private List<int> _ChsCleanList;
        //用户选定控件编号
        private int _rbInt;
        private int _TimerWaitTimes = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (this._TimerWaitTimes == 3)
            {
                this._TimerWaitTimes++;
                timer1.Enabled = false;
                MessageBox.Show(string.Format("获取空白扇区失败!\r\n设备无响应，可能是旧设备请升级！"), Config.DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (this._TimerWaitTimes < 3)
            {
                this._TimerWaitTimes++;
                CardConfiger.GetInstance().ChsSel(out string strErrMessage);
            }
            else
            {
                this._TimerWaitTimes = 0;
            }
        }

        public SelChsForm()
        {
            InitializeComponent();
        }

        public SelChsForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            rbList = new List<RadioButton>();
            _ChsCleanList = new List<int>();

            rbList.Add(radioButtonChs0);
            rbList.Add(radioButtonChs1);
            rbList.Add(radioButtonChs2);
            rbList.Add(radioButtonChs3);
            rbList.Add(radioButtonChs4);
            rbList.Add(radioButtonChs5);
            rbList.Add(radioButtonChs6);
            rbList.Add(radioButtonChs7);
            rbList.Add(radioButtonChs8);
            rbList.Add(radioButtonChs9);
            rbList.Add(radioButtonChs10);
            rbList.Add(radioButtonChs11);
            rbList.Add(radioButtonChs12);
            rbList.Add(radioButtonChs13);
            rbList.Add(radioButtonChs14);
            rbList.Add(radioButtonChs15);

            groupBox2.Visible = RunVariable.IfDebug;
        }

        private void RbList_init()
        {
            for (int i=0;i<16;i++)
            {
                rbList[i].ForeColor = Color.Black;
                rbList[i].Enabled = false;
            }
            _ChsCleanList.Clear();
            _rbInt = 1;
        }

        private void SelChsForm_Load(object sender, EventArgs e)
        {
            CardConfiger.GetInstance().OnChsSelReponse += new CardConfiger.ChsSelReponseHandler(SelChsForm_OnChsSelReponse);
            CardConfiger.GetInstance().OnChsCleanReponse += new CardConfiger.ChsCleanReponseHandler(SelChsForm_OnChsCleanReponse);
            CardConfiger.GetInstance().OnNotice += new ConfigerBase.NoticeHandler(SelChsForm_OnNotice);
            RbList_init();
            
            timer1.Enabled = false;
        }

        private void SelChsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardConfiger.GetInstance().OnChsSelReponse -= new CardConfiger.ChsSelReponseHandler(SelChsForm_OnChsSelReponse);
            CardConfiger.GetInstance().OnChsCleanReponse -= new CardConfiger.ChsCleanReponseHandler(SelChsForm_OnChsCleanReponse);
            CardConfiger.GetInstance().OnNotice -= new ConfigerBase.NoticeHandler(SelChsForm_OnNotice);
            timer1.Enabled = false;
        }


        private void RadioButton_Click(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            //如果选中的是可重置扇区
            _rbInt = Convert.ToInt16(rb.Text.Substring(2));
            if (_ChsCleanList.Contains(_rbInt))
            {
                btnChsClean.Text = "重置扇区";
            }
            else
            {
                btnChsClean.Text = "扇区选择";
            }
        }


        /// <summary>
        /// 回调方法-配置器接收时
        /// </summary>
        /// <param name="strNotice"></param>
        /// <param name="bArrBuffer"></param>
        private void SelChsForm_OnNotice(string strNotice, byte[] bArrBuffer)
        {
            if (!(string.IsNullOrEmpty(strNotice) || strNotice.Equals("")))
            {
                string strContent = (RunVariable.IfDebug && bArrBuffer != null) ? string.Format("{0}:{1}", strNotice, ScaleConverter.ByteArr2HexStr(bArrBuffer)) : strNotice;
                ShowSendOrReceiveMessage(strContent);
            }
        }

        /// <summary>
        /// 输出接受到的数据
        /// </summary>
        /// <param name="strMsg"></param>
        private void ShowSendOrReceiveMessage(string strMsg, bool bIfBR = true)
        {
            try
            {
                this.Invoke(new EventHandler(delegate
                {
                    string strNR = (bIfBR && !string.IsNullOrEmpty(richTextBox1.Text)) ? "\r\n" : "";
                    richTextBox1.Text = string.Format("{0}{1}{2}", richTextBox1.Text, strNR, strMsg);
                }));
            }
            catch { }
        }

        /// <summary>
        /// 回调方法-配置器当获取空白扇区返回时
        /// </summary>
        private void SelChsForm_OnChsSelReponse(CardData objCardData, string strErrInfo)
        {
            if (objCardData != null && string.IsNullOrEmpty(strErrInfo))
            {
                try
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        //先选定默认扇区
                        _rbInt = RunVariable.CurrentSetting.OtherSetting.Chs;
                        rbList[_rbInt].Checked = true;
                        btnChsClean.Text = "扇区选择";

                        //所有公司扇区都变色
                        foreach (int i in objCardData.listChsInfo[1])
                        {
                            _ChsCleanList.Add(i);
                            rbList[i].ForeColor = Color.Cyan;
                            rbList[i].Enabled = true;
                            if(_rbInt == i)
                            {
                                btnChsClean.Text = "重置扇区";
                            }
                        }
                        //置可使用扇区为可选择
                        foreach (int chsno in objCardData.listChsInfo[0])
                        {
                            rbList[chsno].ForeColor = Color.Black;
                            rbList[chsno].Enabled = true;
                        }
                        //置已使用扇区为不可操作
                        foreach (int chsno in objCardData.listChsInfo[2])
                        {
                            rbList[chsno].ForeColor = Color.Red;
                            rbList[chsno].Enabled = false;
                        }
                        timer1.Enabled = false;
                    }));
                }
                catch { }
            }
            else
            {
                MessageBox.Show(string.Format("获取空白扇区失败，错误原因如下：\r\n{0}", strErrInfo), Config.DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 回调方法-配置器当扇区重置返回时
        /// </summary>
        private void SelChsForm_OnChsCleanReponse(uint iCardNo, string strErrInfo)
        {
            if (string.IsNullOrEmpty(strErrInfo))
            {
                try
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        //CMessageBox.ShowSucc(string.Format("恭喜您，扇区重置成功！"), Config.DialogTitle);
                        //字体颜色恢复默认
                        rbList[_rbInt].ForeColor = Color.Black;
                        _ChsCleanList.Remove(_rbInt);
                        btnChsClean.Text = "扇区选择";

                        //删除数据库卡片数据
                        IDAL.ICard objDAL = DALFactory.DALFactory.Card();
                        IList<CardManage.Model.Card> CleanCard = objDAL.GetListByWhere(1, string.Format("cardno = {0}", iCardNo));
                        if (CleanCard.Count > 0 && !objDAL.Delete(CleanCard[0].ID))
                        {
                            CMessageBox.ShowError(string.Format("恭喜您，扇区重置成功,但是删除数据库卡片数据失败！"), Config.DialogTitle);
                            return;
                        }

                        CMessageBox.ShowSucc(string.Format("恭喜您，扇区重置和保存卡片数据到数据库都成功！"), Config.DialogTitle);
                    }));
                }
                catch { }
            }
            else
            {
                CMessageBox.ShowError(string.Format("操作失败，错误原因如下：\r\n{0}", strErrInfo), Config.DialogTitle);
            }
        }

        /// <summary>
        /// 扇区重置
        /// </summary>
        private void BtnChsClean_Click(object sender, EventArgs e)
        {
            string strErrMessage = "";
            
            if (btnChsClean.Text == "扇区选择")
            {
                if (MessageBox.Show("更改写入扇区可能导致扇区混乱，确定更改写入扇区吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                //更改写入扇区,并更改配置文件
                RunVariable.CurrentSetting.OtherSetting.Chs = _rbInt;
                
                if (!SysConfiger.SaveSetting(RunVariable.CurrentSetting, RunVariable.IniPathAndFileName, out strErrMessage))
                {
                    CMessageBox.ShowError(string.Format("对不起，保存配置失败，错误如下：\r\n{0}", strErrMessage), Config.DialogTitle);
                }
                else
                {
                    Manager.GetInstance().SettingChangeNotice();
                    CMessageBox.ShowSucc(string.Format("恭喜您，保存配置成功！"), Config.DialogTitle);
                }
                this.btnCancel.PerformClick();
            }
            else
            {
                if (CardConfiger.GetInstance().Switch.Equals(ConfigerBase.ESwitch.CLOSE))
                {
                    if (MessageBox.Show("制卡串口当前已关闭，请先开启后再重试，确定现在开启吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    CardConfiger.GetInstance().Start(RunVariable.CurrentSetting.WriteComProperty);
                }
                else
                {
                    if (MessageBox.Show("当前操作会清除扇区所有数据\n确定现在重置扇区吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                    
                    if (!CardConfiger.GetInstance().ChsClean(_rbInt.ToString(), out strErrMessage))
                    {
                        CMessageBox.ShowError(string.Format("重置扇区失败，错误如下：\r\n{0}", strErrMessage), Config.DialogTitle);
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            RbList_init();
            
            if (CardConfiger.GetInstance().Switch.Equals(ConfigerBase.ESwitch.CLOSE))
            {
                if (MessageBox.Show("制卡串口当前已关闭，请先开启后再重试，确定现在开启吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                CardConfiger.GetInstance().Start(RunVariable.CurrentSetting.WriteComProperty);
            }
            else
            {
                if (!CardConfiger.GetInstance().ChsSel(out string strErrMessage))
                {
                    MessageBox.Show(string.Format("获取空白扇区失败，错误如下：\r\n{0}", strErrMessage), Config.DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    timer1.Enabled = true;
                }
            }
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(Regex.Replace(this.richTextBox1.Text, "\n", "\r\n", RegexOptions.IgnoreCase));
        }

        private void ToolStripButtonSave_Click(object sender, EventArgs e)
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
                    aWriter.Write(Regex.Replace(this.richTextBox1.Text, "\n", "\r\n", RegexOptions.IgnoreCase));
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

        private void ToolStripButtonClean_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清空调试窗口中的内容吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            richTextBox1.Clear();
        }
    }
}
