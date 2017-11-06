using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CardManage.Tools;
using System.Text.RegularExpressions;
using CardManage.Logic;
using CardManage.Model;
using System.Threading;
using CardManage.Observers;



namespace CardManage.Forms
{
    /// <summary>
    /// 卡片视图
    /// </summary>
    public partial class CardViewForm : SetFormBase
    {
        private EAction _CurrentAction; //修改还是创建标志
        private int _CurrentID = 0; //选择的卡号
        private int _CurrentBuildingID = 0;//选择的建筑编号
        private bool _WriteFlag = false;
        private bool _UpdateFlag = false;
        private List<ComboBox> cbCodeList;
        private List<string> strSerialNo = new List<string> { };

        private Area _area = new Area();

        private Card _LastCard;
        protected CardViewForm()
        {
            InitializeComponent();
        }

        public CardViewForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;

            //初始化时扇区默认为1,不可编辑不可选择,扇区重置按钮默认不可见
            cbCHS.Items.Clear();
            cbCHS.Items.Add(RunVariable.CurrentSetting.OtherSetting.Chs);
            cbCHS.SelectedIndex = 0;

#if DEBUG
            cbCHS.Visible = true;
#else
            cbCHS.Visible = false;
#endif
            //_CurrentAction 判断是类型 是创建还是修改
            this._CurrentAction = (EAction)objFlag.Keyword1;
            if (objFlag.Keyword2 != null)
            {
                switch (this._CurrentAction)
                {
                    case EAction.Create:
                        this._CurrentBuildingID = Convert.ToInt32(objFlag.Keyword2);
                        break;
                    case EAction.Edit:
                        this._CurrentID = Convert.ToInt32(objFlag.Keyword2);
                        break;
                }
            }
            //渲染界面
            gbDebg.Visible = RunVariable.IfDebug;
        }

        private void CardViewForm_Load(object sender, EventArgs e)
        {
            this.IfFormLoadOk = false;
            CardConfiger.GetInstance().OnNotice +=new ConfigerBase.NoticeHandler(CardViewForm_OnNotice);
            CardConfiger.GetInstance().OnStartFault +=new ConfigerBase.StartFaultHandler(CardViewForm_OnStartFault);
            CardConfiger.GetInstance().OnStartSucc +=new ConfigerBase.StartSuccHandler(CardViewForm_OnStartSucc);
            CardConfiger.GetInstance().OnWriteCardReponse += new CardConfiger.WriteCardReponseHandler(CardViewForm_OnWriteCardReponse);
            CardConfiger.GetInstance().OnReadCardReponse += new CardConfiger.ReadCardReponseHandler(CardViewForm_OnReadCardReponse);

            cbCodeList = new List<ComboBox>
                {
                    cbAreaCode,
                    cbBuildCode,
                    cbUnitCode,
                    cbFloorCode
                };
            if (this._CurrentAction.Equals(EAction.Edit))
                EditInitData();
            else
                CreateInitData();
            this.IfFormLoadOk = true;
            
        }

        private void CardViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardConfiger.GetInstance().OnNotice -= new ConfigerBase.NoticeHandler(CardViewForm_OnNotice);
            CardConfiger.GetInstance().OnStartFault -= new ConfigerBase.StartFaultHandler(CardViewForm_OnStartFault);
            CardConfiger.GetInstance().OnStartSucc -= new ConfigerBase.StartSuccHandler(CardViewForm_OnStartSucc);
            CardConfiger.GetInstance().OnWriteCardReponse -= new CardConfiger.WriteCardReponseHandler(CardViewForm_OnWriteCardReponse);
            CardConfiger.GetInstance().OnReadCardReponse -= new CardConfiger.ReadCardReponseHandler(CardViewForm_OnReadCardReponse);
            _area.NotifyEvent -= new NotifyEventHandler(TextBoxNotify);
            _area.NotifyEvent -= new NotifyEventHandler(ComBoBoxNotify);
        }


        private void CreateInitData()
        {
            cbAreaCode.Items.Add("快速选择");
            cbBuildCode.Items.Add("快速选择");
            cbUnitCode.Items.Add("快速选择");
            cbFloorCode.Items.Add("快速选择");
            cbRoomCode.Items.Add("快速选择");
            //下拉选择
            cbAreaCode.SelectedIndex = 0;
            cbBuildCode.SelectedIndex = 0;
            cbUnitCode.SelectedIndex = 0;
            cbFloorCode.SelectedIndex = 0;
            cbRoomCode.SelectedIndex = 0;

            cbEndDate.Items.Add("快速选择");
            for (int i = 1; i < 6; i++)
            {
                cbEndDate.Items.Add(string.Format("{0}年", i));
            }
            cbEndDate.Items.Add("长期有效");
            cbEndDate.SelectedIndex = 0;

            //串口状态
            ShowComStatus();
            //卡片类型
            cbCardType.Items.Add(new Model.ComboBoxItem("请选择卡类型", -1));
            cbCardType.Items.Add(new Model.ComboBoxItem("用户卡", 0));
            cbCardType.Items.Add(new Model.ComboBoxItem("巡更卡", 1));
            cbCardType.Items.Add(new Model.ComboBoxItem("管理卡", 2));
            cbCardType.SelectedIndex = 0;

            
            //初始化卡片有效期 
            dtExpiryDate.Value = DateTime.Now.AddMonths(1);
            _area.NotifyEvent += new NotifyEventHandler(TextBoxNotify);
            _area.NotifyEvent += new NotifyEventHandler(ComBoBoxNotify);
            //初始化编码
            SetNextComBoBox(0);
        }

        private void EditInitData()
        {
            IDAL.ICard objDAL = DALFactory.DALFactory.Card();
            Model.Card objModel = objDAL.GetModel(this._CurrentID);
            if (objModel != null)
            {
                txtCardNo.Text = objModel.CardNo.ToString();
                txtRAreaCode.Text = FormatBuildingCode(objModel.RAreaCode);
                txtRBuildCode.Text = FormatBuildingCode(objModel.RBuildCode);
                txtRUnitCode.Text = FormatBuildingCode(objModel.RUnitCode);
                cbAreaCode.Items.Add(string.Format("{0}|{1:D2}", objModel.AreaName, objModel.RAreaCode));
                cbBuildCode.Items.Add(string.Format("{0}|{1:D2}", objModel.BuildName, objModel.RBuildCode));
                cbUnitCode.Items.Add(string.Format("{0}|{1:D2}", objModel.UnitName, objModel.RUnitCode));
                cbFloorCode.Items.Add(string.Format("{0}|{1:D4}", objModel.RoomName, objModel.RRoomCode));
                cbAreaCode.SelectedIndex = 0;
                cbBuildCode.SelectedIndex = 0;
                cbUnitCode.SelectedIndex = 0;
                cbFloorCode.SelectedIndex = 0;

                string strRoomCode = FormatRoomCode(objModel.RRoomCode);
                txtRFloorCode.Text = strRoomCode.Substring(0, 2);
                txtRRoomCode.Text = strRoomCode.Substring(2, 2);

                txtSerialNo.Text = objModel.SerialNo;
                
                cbCardType.Items.Add(new Model.ComboBoxItem("请选择卡类型", -1));
                cbCardType.Items.Add(new Model.ComboBoxItem("用户卡", 0));
                cbCardType.Items.Add(new Model.ComboBoxItem("巡更卡", 1));
                cbCardType.Items.Add(new Model.ComboBoxItem("管理卡", 2));
                cbCardType.SelectedIndex = objModel.CardType+1;

                txtContact.Text = objModel.Contact;
                txtTel.Text = objModel.Tel;
                txtMemo.Text = objModel.Memo;

                dtExpiryDate.Value = Functions.ConvertToNormalTime(objModel.ExpiryDate);
                cbEndDate.Visible = false;

                string strBelongDesc = "未归属";
                if (!objModel.AreaName.Equals(""))
                {
                    strBelongDesc = "";
                    strBelongDesc += string.Format("{0}", objModel.AreaName);
                    if (!objModel.BuildName.Equals(""))
                    {
                        strBelongDesc += string.Format("=>{0}", objModel.BuildName);
                        if (!objModel.UnitName.Equals(""))
                        {
                            strBelongDesc += string.Format("=>{0}", objModel.UnitName);
                            if (!objModel.RoomName.Equals(""))
                            {
                                strBelongDesc += string.Format("=>{0}", objModel.RoomName);
                            }
                        }
                    }
                }
                txtBelongDesc.Text = strBelongDesc;
            }
            //一些控件设置为不可变,隐藏部分控件
            SetReadOnly();
        }

        private void SetComBoBox(int iInt, IList<Building> listBuilding)
        {
            cbCodeList[iInt].Items.Clear();
            cbCodeList[iInt].Items.Add("快速选择");
            //cbCodeList[iInt].SelectedIndex = 0;
            foreach (Building objmodel in listBuilding)
            {
                if (iInt == 3)
                {
                    cbCodeList[iInt].Items.Add(string.Format("{0}|{1:D4}", objmodel.BName, objmodel.Code));
                }
                else
                {
                    cbCodeList[iInt].Items.Add(string.Format("{0}|{1:D2}", objmodel.BName, objmodel.Code));
                }
                if (iInt == 0)
                {
                    strSerialNo.Add(string.Format(objmodel.BuildingSerialNo));
                }
            }
        }

        private void BtnReadCard_Click(object sender, EventArgs e)
        {
            if(_CurrentAction.Equals(EAction.Edit))
            {
                //修改资料
                this._LastCard = GetCardModel(false, out string strErrMessage);
                if (this._LastCard == null)
                {
                    CMessageBox.ShowError(strErrMessage, Config.DialogTitle);
                    return;
                }

                if (MessageBox.Show("确定要保存修改吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                IDAL.ICard objDAL = DALFactory.DALFactory.Card();
                bool bIfSucc = objDAL.Update(this._LastCard, out string strErrorInfo);
                if (bIfSucc)
                {
                    CMessageBox.ShowError(string.Format("恭喜您，保存成功!"), Config.DialogTitle);
                }
                else
                {
                    CMessageBox.ShowError(string.Format("保存失败，错误如下：\r\n{0}", strErrorInfo), Config.DialogTitle);
                }
            }
            else
            {
                //读卡
                if (CardConfiger.GetInstance().Switch.Equals(ConfigerBase.ESwitch.CLOSE))
                {
                    if (MessageBox.Show("制卡串口当前已关闭，请先开启后再重试，确定现在开启吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    CardConfiger.GetInstance().Start(RunVariable.CurrentSetting.WriteComProperty);
                }
                else
                {
                    if (MessageBox.Show("确定现在读卡吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    if (!CardConfiger.GetInstance().ReadCard(out string strErrMessage))
                    {
                        CMessageBox.ShowError(string.Format("读卡失败，错误如下：\r\n{0}", strErrMessage), Config.DialogTitle);
                    }
                }
            }
        }

        private void BtnWriteCard_Click(object sender, EventArgs e) 
        {
            this._LastCard = GetCardModel(true, out string strErrMessage);
            if (this._LastCard == null)
            {
                CMessageBox.ShowError(strErrMessage, Config.DialogTitle);
                return;
            }
            
            if (CardConfiger.GetInstance().Switch.Equals(ConfigerBase.ESwitch.CLOSE))
            {
                if (MessageBox.Show("制卡串口当前已关闭，请先开启后再重试，确定现在开启吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                CardConfiger.GetInstance().Start(RunVariable.CurrentSetting.WriteComProperty);
            }
            else
            {
                if (MessageBox.Show("确定要把当前数据写入卡吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }

            //先查询卡片,看是否已经存在数据库中,置写卡标志为1
            _WriteFlag = true;
            if (!CardConfiger.GetInstance().ReadCard(out strErrMessage))
            {
                CMessageBox.ShowError(string.Format("读卡失败，错误如下：\r\n{0}", strErrMessage), Config.DialogTitle);
                _WriteFlag = false;
            }
        }

        private void SetReadOnly()
        {
            txtRAreaCode.ReadOnly = true;
            txtRBuildCode.ReadOnly = true;
            txtRUnitCode.ReadOnly = true;
            txtRFloorCode.ReadOnly = true;
            txtRRoomCode.ReadOnly = true;
            txtSerialNo.ReadOnly = true;
            cbAreaCode.Enabled = false;
            cbBuildCode.Enabled = false;
            cbUnitCode.Enabled = false;
            cbFloorCode.Enabled = false;
            cbRoomCode.Enabled = false;
            cbCardType.Enabled = false;
            dtExpiryDate.Enabled = false;
            cbEndDate.Enabled = false;
            btnQuikChooseSerialNO.Enabled = false;
            //写卡按钮不可见,读卡按钮变为保存
            btnWriteCard.Visible = false;
            btnReadCard.Text = "保存(&S)";
        }

        private void BtnGetCodeFromBelong_Click(object sender, EventArgs e)
        {
            SelectBuildingForm objModalForm = new SelectBuildingForm("选择归属", true, this.CurrentUserInfo, new WindowSize(303, 222));
            if (objModalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (objModalForm.BuildingCode != null)
                {
                    txtRAreaCode.Text = objModalForm.BuildingCode.AreaCode;
                    txtRBuildCode.Text = objModalForm.BuildingCode.BuildCode;
                    txtRUnitCode.Text = objModalForm.BuildingCode.UnitCode;

                    if (string.IsNullOrEmpty(objModalForm.BuildingCode.RoomCode))
                    {
                        txtRFloorCode.Text = "";
                        txtRRoomCode.Text = "";
                    }
                    else
                    {
                        string strRoomCode = FormatRoomCode(objModalForm.BuildingCode.RoomCode);
                        txtRFloorCode.Text = strRoomCode.Substring(0, 2);
                        txtRRoomCode.Text = strRoomCode.Substring(2, 2);
                    }
                }
            }
        }

        private new void MaskCodeText(object sender, KeyPressEventArgs e)
        {
            //判断输入的值是否为数字或删除键或粘贴键22
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 46 || (e.KeyChar >= 40 && e.KeyChar<=49)))
            {
                e.Handled = true;
            }
        }

        private void MaskAddressText(object sender, KeyPressEventArgs e)
        {
            //a-f:97-102
            //A-F:65-70  于17.6.16 改为只能输入数字,不接受字母

            //判断输入的值是否为数字、16进制字符、冒号或删除键或粘贴键22或Copy键3
            //if (char.IsDigit(e.KeyChar) || (e.KeyChar >= 97 && e.KeyChar <= 102) || (e.KeyChar >= 65 && e.KeyChar <= 70) || e.KeyChar == 8 || e.KeyChar == 58 || e.KeyChar == 22 || e.KeyChar == 3)
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 58 || e.KeyChar == 22 || e.KeyChar == 3)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TsbClearDebugContent_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清空调试窗口中的内容吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
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

        /// <summary>
        /// 回调方法-配置器接收时
        /// </summary>
        /// <param name="strNotice"></param>
        /// <param name="bArrBuffer"></param>
        private void CardViewForm_OnNotice(string strNotice, byte[] bArrBuffer)
        {
            if (!(string.IsNullOrEmpty(strNotice) || strNotice.Equals("")))
            {
                string strContent = (RunVariable.IfDebug && bArrBuffer != null) ? string.Format("{0}:{1}", strNotice, ScaleConverter.ByteArr2HexStr(bArrBuffer)) : strNotice;
                ShowSendOrReceiveMessage(strContent);
            }
        }

        /// <summary>
        /// 回调方法-配置器当打开失败时
        /// </summary>
        /// <param name="strErrorMessage"></param>
        private void CardViewForm_OnStartFault(string strErrorMessage)
        {
            CMessageBox.ShowError(string.Format("制卡串口开启失败，请查看您的配置，错误原因如下：\r\n{0}", strErrorMessage), Config.DialogTitle);
            ShowComStatus();
        }

        /// <summary>
        /// 回调方法-配置器当成功打开时
        /// </summary>
        /// <param name="strNotice"></param>
        private void CardViewForm_OnStartSucc()
        {
            ShowComStatus();
            CMessageBox.ShowSucc(string.Format("恭喜您，制卡串口开启成功！"), Config.DialogTitle);
        }

        /// <summary>
        /// 回调方法-配置器当写卡返回时
        /// </summary>
        private void CardViewForm_OnWriteCardReponse(uint iCardNo, string strErrInfo)
        {
            if (this._LastCard == null) return;
            if (iCardNo != uint.MaxValue && string.IsNullOrEmpty(strErrInfo))
            {
                try
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        txtCardNo.Text = iCardNo.ToString();
                        this._LastCard.CardNo = iCardNo;
                        //保存数据库
                        IDAL.ICard objDAL = DALFactory.DALFactory.Card();
                        if (!_UpdateFlag)
                        {
                            int iNewID = objDAL.Add(this._LastCard, out string strErrorInfo);
                            if (iNewID <= 0)
                            {
                                CMessageBox.ShowError(string.Format("恭喜您，写卡成功,但是保存卡片数据到数据库失败，错误如下：\r\n{0}！", strErrorInfo), Config.DialogTitle);
                                return;
                            }
                        }
                        else
                        {
                            bool bIfSucc = objDAL.Update(this._LastCard, out string strErrorInfo);
                            if (!bIfSucc)
                            {
                                CMessageBox.ShowError(string.Format("恭喜您，写卡成功,但是保存卡片数据到数据库失败，错误如下：\r\n{0}！", strErrorInfo), Config.DialogTitle);
                                return;
                            }
                        }
                        Manager.GetInstance().CardDataChangeNotice();
                        CMessageBox.ShowSucc(string.Format("恭喜您，写卡和保存卡片数据到数据库都成功！"), Config.DialogTitle);
                    }));
                }
                catch { }
            }
            else
            {
                CMessageBox.ShowError(string.Format("写卡失败，错误原因如下：\r\n{0}", strErrInfo), Config.DialogTitle);
            }
        }

        /// <summary>
        /// 回调方法-配置器当读卡返回时
        /// </summary>
        private void CardViewForm_OnReadCardReponse(CardData objCardData, string strErrInfo)
        {
            if (objCardData != null && string.IsNullOrEmpty(strErrInfo))
            {
                try
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        string strErrMessage;
                        txtCardNo.Text = Functions.FormatString(objCardData.CardNo);

                        //如果是写卡前的读卡
                        if(_WriteFlag)
                        {
                            _WriteFlag = false;
                            //通过卡号查询是否有数据
                            IDAL.ICard objDAL = DALFactory.DALFactory.Card();
                            if (objDAL.ExistsByWhere(string.Format("CardNo = {0}", objCardData.CardNo)))
                            {
                                //卡片存在,是否覆盖
                                if (MessageBox.Show(string.Format("系统中已经存在卡号为[{0}]的卡了,是否覆盖当前卡片?", objCardData.CardNo), objCardData.CardNo.ToString(), MessageBoxButtons.YesNoCancel) != DialogResult.Yes) return;
                                _UpdateFlag = true;
                            }

                            //如果使用扇区与默认扇区不一致 提示是否更改扇区
                            //旧读卡器没返回时,已设置使用扇区为1,不需要另外考虑
                            this._LastCard.listChsInfo[1][0] = RunVariable.CurrentSetting.OtherSetting.Chs;
                            if (objCardData.listChsInfo[1].Count > 0)
                            {
                                int oldChs = Convert.ToInt32(objCardData.listChsInfo[1][0]);
                                if (oldChs != RunVariable.CurrentSetting.OtherSetting.Chs)
                                {
                                    switch (MessageBox.Show(string.Format("当前卡片使用扇区与系统默认设置不一至,是否使用系统默认扇区?"), objCardData.CardNo.ToString(), MessageBoxButtons.YesNoCancel))
                                    {
                                        case DialogResult.Yes:
                                            //删除当前使用扇区
                                            if (!CardConfiger.GetInstance().ChsClean(oldChs.ToString(), out strErrMessage))
                                            {
                                                CMessageBox.ShowError(string.Format("删除旧扇区失败，错误如下：\r\n{0}", strErrMessage), Config.DialogTitle);
                                            }
                                            Thread.Sleep(1000);
                                            break;
                                        case DialogResult.No:
                                            this._LastCard.listChsInfo[1][0] = oldChs;
                                            break;
                                        case DialogResult.Cancel:
                                            return;
                                    }
                                }
                            }

                            if (!CardConfiger.GetInstance().WriteCard(this._LastCard, out strErrMessage))
                            {
                                CMessageBox.ShowError(string.Format("写卡失败，错误如下：\r\n{0}", strErrMessage), Config.DialogTitle);
                            }
                        }
                        else
                        {
                            //只是读卡(非写卡前的读卡)
                            if (objCardData.CardType == 6)
                            {
                                CMessageBox.ShowSucc(string.Format("恭喜您，读卡成功！卡片是空卡!"), Config.DialogTitle);
                                SetFormEmpty();
                                SetNextComBoBox(0);
                                return;
                            }

                            txtRAreaCode.Text = FormatBuildingCode(objCardData.AreaCode);
                            txtRBuildCode.Text = FormatBuildingCode(objCardData.BuildCode);
                            txtRUnitCode.Text = FormatBuildingCode(objCardData.UnitCode);

                            string strRoomCode = FormatRoomCode(objCardData.RoomCode);
                            txtRFloorCode.Text = strRoomCode.Substring(0, 2);
                            txtRRoomCode.Text = strRoomCode.Substring(2, 2);


                            txtSerialNo.Text = Functions.FormatString(objCardData.SerialNo);
                            dtExpiryDate.Value = Functions.ConvertToNormalTime(objCardData.ExpiryDate);
                            if (objCardData.CardType >= 0 && objCardData.CardType <= 2)
                            {
                                cbCardType.SelectedIndex = objCardData.CardType + 1;
                            }
                            else
                            {
                                cbCardType.SelectedIndex = 0;
                            }

                            cbCHS.Items.Clear();
                            cbCHS.Items.Add(objCardData.listChsInfo[1][0]);
                            cbCHS.SelectedIndex = 0;
                            cbCHS.Enabled = false;

                            CMessageBox.ShowSucc(string.Format("恭喜您，读卡成功！"), Config.DialogTitle);
                        }
                    }));
                }
                catch { }
            }
            else
            {
                CMessageBox.ShowError(string.Format("读卡失败，错误原因如下：\r\n{0}", strErrInfo), Config.DialogTitle);
            }
        }

        /// <summary>
        /// 空卡时,清理旧卡片数据
        /// </summary>
        /// <param name="strMsg"></param>
        private void SetFormEmpty()
        {
            txtRAreaCode.Text = "";
            txtRBuildCode.Text = "";
            txtRUnitCode.Text = "";
            txtRFloorCode.Text = "";
            txtRRoomCode.Text = "";
            txtSerialNo.Text = "";
            txtContact.Text = "";
            txtTel.Text = "";
            txtMemo.Text = "";
            cbCardType.SelectedIndex = 0;
            cbEndDate.SelectedIndex = 0;
            dtExpiryDate.Value = DateTime.Now.AddMonths(1);
            cbAreaCode.Items.Clear();
            cbBuildCode.Items.Clear();
            cbUnitCode.Items.Clear();
            cbFloorCode.Items.Clear();
            cbAreaCode.Items.Add("快速选择");
            cbBuildCode.Items.Add("快速选择");
            cbUnitCode.Items.Add("快速选择");
            cbFloorCode.Items.Add("快速选择");
            cbAreaCode.SelectedIndex = 0;
            cbBuildCode.SelectedIndex = 0;
            cbUnitCode.SelectedIndex = 0;
            cbFloorCode.SelectedIndex = 0;
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
                    string strNR = (bIfBR && !string.IsNullOrEmpty(tbxComunicateData.Text)) ? "\r\n" : "";
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

        /// <summary>
        /// 显示串口状态信息
        /// </summary>
        private void ShowComStatus()
        {
            this.Invoke(new EventHandler(delegate
            {
                lbComStatus.Text = string.Format("[{0}]", (CardConfiger.GetInstance().Switch.Equals(CardConfiger.ESwitch.OPEN) ? "开启" : "关闭"));
            }));
        }

        private Model.Card GetCardModel(bool bIfWriteCardOP, out string strErrMessage)
        {
            strErrMessage = "";

            string strCardNo = txtCardNo.Text.Trim();
            string strRAreaCode = txtRAreaCode.Text.Trim();
            string strRBuildCode = txtRBuildCode.Text.Trim();
            string strRUnitCode = txtRUnitCode.Text.Trim();
            string strRFloorCode = txtRFloorCode.Text.Trim();
            string strRRoomCode = txtRRoomCode.Text.Trim();
            string strContact = txtContact.Text.Trim();
            string strTel = txtTel.Text.Trim();
            string strMemo = txtMemo.Text.Trim();

            int iCardType = Convert.ToInt32(((Model.ComboBoxItem)cbCardType.SelectedItem).Value);
            string strSerialNo = txtSerialNo.Text.Trim();

            //写卡时就无需验证卡号了
            if (!bIfWriteCardOP)
            {
                if (strCardNo.Equals(""))
                {
                    strErrMessage = string.Format("卡号必须先获取到！");
                    return null;
                }
                else
                {
                    if (!Functions.IsUInt(strCardNo))
                    {
                        strErrMessage = string.Format("卡号的值异常！");
                        return null;
                    }
                }
            }
            else
            {
                strCardNo = "0";
            }

            string strTip = "编码不能为空，且范围必须是0~99之间的整数！";
            if (!CheckCode(strRAreaCode))
            {
                txtRAreaCode.Focus();
                strErrMessage = string.Format("小区{0}！", strTip);
                return null;
            }
            if (!CheckCode(strRBuildCode))
            {
                txtRBuildCode.Focus();
                strErrMessage = string.Format("楼栋{0}！", strTip);
                return null;
            }
            if (!CheckCode(strRUnitCode))
            {
                txtRUnitCode.Focus();
                strErrMessage = string.Format("单元{0}！", strTip);
                return null;
            }

            //后台原因 小区和楼栋编码不能同时为0
            if (Convert.ToInt32(strRUnitCode) + Convert.ToInt32(strRBuildCode) == 0)
            {
                txtRBuildCode.Focus();
                strErrMessage = string.Format("楼栋和单元编码不能同时为0或00！");
                return null;
            }

            if (!CheckCode(strRFloorCode))
            {
                txtRFloorCode.Focus();
                strErrMessage = string.Format("楼层{0}！", strTip);
                return null;
            }
            if (!CheckCode(strRRoomCode))
            {
                txtRRoomCode.Focus();
                strErrMessage = string.Format("房间{0}！", strTip);
                return null;
            }

            if (iCardType <= -1)
            {
                cbCardType.Focus();
                strErrMessage = string.Format("卡片类型必须选择！");
                return null;
            }

            if (!CheckSerialNo(strSerialNo))
            {
                txtSerialNo.Focus();
                strErrMessage = string.Format("卡片序列号必须填写，且必须是16字节的16进制字符！");
                return null;
            }

            Model.Card objModel = new Model.Card()
            {
                ID = this._CurrentID,
                RAreaCode = Convert.ToInt32(strRAreaCode),
                RBuildCode = Convert.ToInt32(strRBuildCode),
                RUnitCode = Convert.ToInt32(strRUnitCode),
                RRoomCode = (Convert.ToInt32(strRFloorCode) * 100) + Convert.ToInt32(strRRoomCode),
                CardType = iCardType,
                CardNo = Convert.ToUInt32(strCardNo),
                ExpiryDate = Functions.ConvertToUnixTime(Convert.ToDateTime(string.Format("{0} 23:59:59", dtExpiryDate.Value.ToString("yyyy-MM-dd")))),
                CreateDate = Functions.ConvertToUnixTime(DateTime.Now),
                SerialNo = strSerialNo,
                Contact = strContact,
                Tel = strTel,
                Memo = strMemo,
                IfFrezen = false
            };
            int i = Convert.ToInt32(this.cbCHS.SelectedItem);
            objModel.listChsInfo[1].Add(i);

            return objModel;
        }

        private bool CheckCode(string strCode)
        {
            bool bIfOk = false;
            if (!string.IsNullOrEmpty(strCode))
            {
                strCode = strCode.Trim();
                if (!strCode.Equals(""))
                {
                    if (Functions.IsInt(strCode))
                    {
                        int iCode = Functions.FormatInt(strCode);
                        if (iCode >= 0 && iCode <= 99) bIfOk = true;
                    }
                }
            }
            return bIfOk;
        }

        private bool CheckSerialNo(string strSerialNo)
        {
            bool bIfOk = false;
            if (!string.IsNullOrEmpty(strSerialNo))
            {
                strSerialNo = strSerialNo.Trim();
                if (!strSerialNo.Equals("") && strSerialNo.Length ==32)
                {
                    try
                    {
                        byte[] bArrSerialNo = ScaleConverter.HexStr2ByteArr(strSerialNo);
                        if (bArrSerialNo.Length == 16) bIfOk = true;
                    }
                    catch{

                    }
                }
            }
            return bIfOk;
        }

        private void TbCode_TextChanged(object sender, EventArgs e)
        {
            if (this.IfFormLoadOk)
            {
                TextBox objTB = (TextBox)sender;
                switch(objTB.Name)
                {
                    case "txtRAreaCode":
                        _area.SetAreaCode(objTB.Text);
                        break;
                    case "txtRBuildCode":
                        _area.SetBuildCode(objTB.Text);
                        break;
                    case "txtRUnitCode":
                        _area.SetUnitCode(objTB.Text);
                        break;
                    case "txtRFloorCode":
                        _area.SetFloorCode(objTB.Text);
                        break;
                    case "txtRRoomCode":
                        _area.SetRoomCode(objTB.Text);
                        break;
                }
            }
        }


        private void CbCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IfFormLoadOk)
            {
                ComboBox objCB = (ComboBox)sender;
                if (objCB.SelectedIndex > 0)
                {
                    string strSelValue = "";
                    strSelValue = objCB.Text;

                    switch (objCB.Name)
                    {
                        case "cbAreaCode":
                            _area.SetAreaCode(strSelValue.Substring(strSelValue.LastIndexOf("|") + 1, 2));
                            break;
                        case "cbBuildCode":
                            _area.SetBuildCode(strSelValue.Substring(strSelValue.LastIndexOf("|") + 1, 2));
                            break;
                        case "cbUnitCode":
                            _area.SetUnitCode(strSelValue.Substring(strSelValue.LastIndexOf("|") + 1, 2));
                            break;
                        case "cbFloorCode":
                            _area.SetFloorRoomCode(strSelValue.Substring(strSelValue.LastIndexOf("|") + 1, 4));
                            break;
                        case "cbEndDate":
                            if (objCB.SelectedIndex == 6)
                            {
                                dtExpiryDate.Value = dtExpiryDate.MaxDate;
                            }
                            else
                            {
                                dtExpiryDate.Value = DateTime.Now.AddYears(Convert.ToInt16(objCB.SelectedIndex));
                            }
                            break;
                    }
                }
            }
        }

        private void TextBoxNotify()
        {
            txtRAreaCode.Text = _area.GetAreaCode();
            txtRBuildCode.Text = _area.GetBuildCode();
            txtRUnitCode.Text = _area.GetUnitCode();
            txtRFloorCode.Text = _area.GetFloorCode();
            txtRRoomCode.Text = _area.GetRoomCode();
        }

        private void ComBoBoxNotify()
        {
            //cbAreaCode,cbBuildCode,cbUnitCode,cbFloorCode
            SetAllComBoBox();
            SetComBoBoxSelectedIndex(cbAreaCode, _area.GetAreaCode(), 1);
            SetComBoBoxSelectedIndex(cbBuildCode, _area.GetBuildCode(), 1);
            SetComBoBoxSelectedIndex(cbUnitCode, _area.GetUnitCode(), 1);
            SetComBoBoxSelectedIndex(cbFloorCode, _area.GetFloorRoomCode(), 2);
            //获取序列号
            txtSerialNo.Text = strSerialNo[cbAreaCode.SelectedIndex - 1];
        }

        private void SetComBoBoxSelectedIndex(ComboBox iComboBox, string iStr, int iNo)
        {
            if (iStr == "" || iStr == null)
                return;
            string strTmp = "";
            int iInt = 0;
            if(iComboBox.SelectedIndex > 0)
            {
                strTmp = iComboBox.SelectedItem.ToString();
                iInt = int.Parse(strTmp.Substring(strTmp.Length - 2 * iNo, 2));
            }
            if (iComboBox.SelectedIndex <= 0 || int.Parse(strTmp.Substring(strTmp.Length-2 * iNo, 2 * iNo)) != int.Parse(iStr))
            {
                for (int i = iComboBox.Items.Count - 1; i > 0; i--)
                {
                    strTmp = iComboBox.GetItemText(iComboBox.Items[i]).ToString();
                    if (int.Parse(strTmp.Substring(strTmp.Length - 2 * iNo, 2 * iNo)) == int.Parse(iStr))
                    {
                        iComboBox.SelectedIndex = i;
                        break;
                    }
                    else
                    {
                        if (i == 1) iComboBox.SelectedIndex = 0;
                    }
                }
            }
             return;
        }

        private void SetAllComBoBox()
        {
            //cbAreaCode cbBuildCode cbUnitCode cbFloorCode
            if (cbAreaCode.Items.Count <= 1)
                SetNextComBoBox(0);
            if (_area.GetAreaCode() != null && _area.GetChgNo() <= 1)
            {
                SetNextComBoBox(1);
            }
            if (_area.GetBuildCode() != null && _area.GetChgNo() <= 2)
            {
                SetNextComBoBox(2);
            }
            if (_area.GetUnitCode() != null && _area.GetChgNo() <= 3)
            {
                SetNextComBoBox(3);
            }
        }

        private void SetNextComBoBox(int iInt)
        { 
            //查询下个CB
            int SelNum = 100;
            IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
            IList<CardManage.Model.Building> listBuilding;
            switch (iInt)
            {
                case 0:
                    listBuilding = objDAL.GetListByWhere(iInt, SelNum, "1=1");
                    SetComBoBox(iInt, listBuilding);
                    _area.SetAreaCode("01");
                    break;
                case 1:
                    listBuilding = objDAL.GetListByWhere(iInt, SelNum, string.Format("AreaCode={0}", _area.GetAreaCode()));
                    SetComBoBox(iInt, listBuilding);
                    cbBuildCode.SelectedIndex = 0;
                    break;
                case 2:
                    listBuilding = objDAL.GetListByWhere(iInt, SelNum, string.Format("AreaCode={0} and BuildCode={1}", txtRAreaCode.Text, txtRBuildCode.Text));
                    SetComBoBox(iInt, listBuilding);
                    cbUnitCode.SelectedIndex = 0;
                    break;
                case 3:
                    listBuilding = objDAL.GetListByWhere(iInt, SelNum * SelNum, string.Format("AreaCode={0} and BuildCode={1} and UnitCode={2}", txtRAreaCode.Text, txtRBuildCode.Text, txtRUnitCode.Text));
                    SetComBoBox(iInt, listBuilding);
                    cbFloorCode.SelectedIndex = 0;
                    break;
            }

        }

        private void BtnQuikChooseSerialNO_Click(object sender, EventArgs e)
        {
            AreaChooseForm objModalForm = new AreaChooseForm("选择序列号", true, this.CurrentUserInfo, new WindowSize(459, 310));
            if (objModalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Building objArea = objModalForm.GetAreaData();
                if (objArea != null)
                {
                    txtSerialNo.Text = objArea.BuildingSerialNo;
                }
            }
        }
    }

    public class AllCode
    {
        public string AreaCode;
        public string BuildCode;
        public string UnitCode;
        public string FloorCode;
        public string RoomCode;
    }

    //具体被观察者
    public class Area : Subject
    {
        private AllCode _code;
        private int _chgNo;

        public Area()
        {
            _code = new AllCode();
        }
        Area(AllCode code)
        {
            _code = code;
        }
        Area(string AreaCode, string BuildCode, string UnitCode, string FloorCode, string RoomCode)
        {
            AllCode code = new AllCode()
            {
                AreaCode = AreaCode,
                BuildCode = BuildCode,
                UnitCode = UnitCode,
                FloorCode = FloorCode,
                RoomCode = RoomCode
            };
            _code = code;
        }

        public void SetAreaCode(string iStr)
        {
            if (iStr != _code.AreaCode)
            {
                _chgNo = 1;
                _code.AreaCode = iStr;
                Run();
            }
        }
        public void SetBuildCode(string iStr)
        {
            if (iStr != _code.BuildCode)
            {
                _chgNo = 2;
                _code.BuildCode = iStr;
                Run();
            }
        }
        public void SetUnitCode(string iStr)
        {
            if (iStr != _code.UnitCode)
            {
                _chgNo = 3;
                _code.UnitCode = iStr;
                Run();
            }
        }
        public void SetFloorCode(string iStr)
        {
            if (iStr != _code.FloorCode)
            {
                _chgNo = 4;
                _code.FloorCode = iStr;
                Run();
            }
        }
        public void SetRoomCode(string iStr)
        {
            if (iStr != _code.RoomCode)
            {
                _chgNo = 4;
                _code.RoomCode = iStr;
                Run();
            }
        }

        public void SetFloorRoomCode(string iStr)
        {
            string iStr1 = iStr.Substring(0, 2);
            string iStr2 = iStr.Substring(2, 2);
            if (iStr1 != _code.FloorCode || iStr2 != _code.RoomCode)
            {
                _chgNo = 4;
                _code.FloorCode = iStr1;
                _code.RoomCode = iStr2;
                Run();
            }
        }
        public int GetChgNo()
        {
            return _chgNo;
        }
        public string GetAreaCode()
        {
            return _code.AreaCode;
        }
        public string GetBuildCode()
        {
            return _code.BuildCode;
        }
        public string GetUnitCode()
        {
            return _code.UnitCode;
        }
        public string GetFloorCode()
        {
            return _code.FloorCode;
        }
        public string GetRoomCode()
        {
            return _code.RoomCode;
        }
        public string GetFloorRoomCode()
        {
            return _code.FloorCode + _code.RoomCode;
        }

        public override void Run()
        {
            this.Notify();
        }
    }

    public class ConcreteSubject : Subject
    {
        public void Response()
        {
            //this.
        }
    }
}

