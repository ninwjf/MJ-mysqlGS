using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CardManage.Tools;
using System.Threading;

namespace CardManage.Forms
{
    public partial class BuidingBatchForm : SetFormBase
    {
        private bool _FormIfClose = false;
        protected BuidingBatchForm()
        {
            InitializeComponent();
        }

        public BuidingBatchForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

        private void BuidingBatchForm_Load(object sender, EventArgs e)
        {
            this.IfFormLoadOk = false;
            InitData();
            this.IfFormLoadOk = true;
        }

        private void BuidingBatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._FormIfClose = true;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
            //小区
            cbArea.Items.Add(new Model.ComboBoxItem("请选择小区", 0));
            cbBuild.Items.Add(new Model.ComboBoxItem("请选择楼栋", 0));
            cbUnit.Items.Add(new Model.ComboBoxItem("请选择单元", 0));
            cbArea.SelectedIndex = 0;
            cbBuild.SelectedIndex = 0;
            cbUnit.SelectedIndex = 0;

            IList<Model.Building> lsAll = objDAL.GetListByWhere(-1, 100000, "1=1");
            //小区
            IList<Model.Building> listArea = lsAll.Where(s => s.FID == 0).ToList();
            if (!(listArea == null || listArea.Count <= 0))
            {
                foreach (Model.Building model in listArea)
                {
                    cbArea.Items.Add(new Model.ComboBoxItem(string.Format("{0}", model.BName), model.ID));
                }
            }
        }

        private void CbCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IfFormLoadOk)
            {
                ComboBox cbCurrent = (ComboBox)sender;
                string strCbName = cbCurrent.Name;
                int iCurrentID = Convert.ToInt32(((Model.ComboBoxItem)cbCurrent.SelectedItem).Value);

                if (iCurrentID > 0)
                {
                    IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
                    IList<Model.Building> lsAll = objDAL.GetListByWhere(-1, 100000, "1=1");
                    IList<Model.Building> listBuilding = lsAll.Where(s => s.FID == iCurrentID).ToList();
                    if (string.Compare(strCbName, "cbArea", true) == 0)
                    {
                        //楼栋
                        cbBuild.Items.Clear();
                        cbBuild.Items.Add(new Model.ComboBoxItem("请选择楼栋", 0));

                        //初始化楼栋
                        if (!(listBuilding == null || listBuilding.Count <= 0))
                        {
                            foreach (Model.Building model in listBuilding)
                            {
                                cbBuild.Items.Add(new Model.ComboBoxItem(string.Format("{0}", model.BName), model.ID));
                            }
                        }
                        cbBuild.SelectedIndex = 0;
                    }
                    else if (string.Compare(strCbName, "cbBuild", true) == 0)
                    {
                        //单元
                        cbUnit.Items.Clear();
                        cbUnit.Items.Add(new Model.ComboBoxItem("请选择单元", 0));

                        //初始化单元
                        if (!(listBuilding == null || listBuilding.Count <= 0))
                        {
                            foreach (Model.Building model in listBuilding)
                            {
                                cbUnit.Items.Add(new Model.ComboBoxItem(string.Format("{0}", model.BName), model.ID));
                            }
                        }
                        cbUnit.SelectedIndex = 0;
                    }
                }
            }
        }

        public new void MaskCodeText(object sender, KeyPressEventArgs e)
        {
            //判断输入的值是否为数字或删除键或粘贴键22或Copy键3或Cut键24
            if (char.IsDigit(e.KeyChar) || (e.KeyChar >= 40 && e.KeyChar <= 49) || e.KeyChar == 8 || e.KeyChar == 58 || e.KeyChar == 22 || e.KeyChar == 3 || e.KeyChar == 24)
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
            int iAreaID = Convert.ToInt32(((Model.ComboBoxItem)cbArea.SelectedItem).Value);
            int iBuildID = Convert.ToInt32(((Model.ComboBoxItem)cbBuild.SelectedItem).Value);
            int iUnitID = Convert.ToInt32(((Model.ComboBoxItem)cbUnit.SelectedItem).Value);
            string strBeginCode = txtRoomCodeBegin.Text.Trim();
            string strEndCode = txtRoomCodeEnd.Text.Trim();

            if (iAreaID <= 0)
            {
                CMessageBox.ShowError("请选择一个小区！", Config.DialogTitle);
                return;
            }
            if (iBuildID <= 0)
            {
                CMessageBox.ShowError("请选择一个楼栋！", Config.DialogTitle);
                return;
            }
            if (iUnitID <= 0)
            {
                CMessageBox.ShowError("请选择一个单元！", Config.DialogTitle);
                return;
            }

            if (strBeginCode.Equals("") || !Functions.IsInt(strBeginCode))
            {
                txtRoomCodeBegin.Focus();
                CMessageBox.ShowError(string.Format("请输入起始楼层房间编码,且范围必须是0~9999之间的整数！"), Config.DialogTitle);
                return;
            }

            int iBeginCode = Functions.FormatInt(strBeginCode);
            if (!(iBeginCode >= 0 && iBeginCode <= 9999))
            {
                txtRoomCodeBegin.Focus();
                CMessageBox.ShowError(string.Format("输入的起始楼层房间编码超出范围，取值范围是0~9999之间的整数！"), Config.DialogTitle);
                return;
            }

            if (strEndCode.Equals("") || !Functions.IsInt(strEndCode))
            {
                txtRoomCodeEnd.Focus();
                CMessageBox.ShowError(string.Format("请输入结束楼层房间编码,且范围必须是0~9999之间的整数！"), Config.DialogTitle);
                return;
            }

            int iEndCode = Functions.FormatInt(strEndCode);
            if (!(iEndCode >= 0 && iEndCode <= 9999))
            {
                txtRoomCodeEnd.Focus();
                CMessageBox.ShowError(string.Format("输入的结束楼层房间编码超出范围，取值范围是0~9999之间的整数！"), Config.DialogTitle);
                return;
            }

            if (iBeginCode >= iEndCode)
            {
                txtRoomCodeBegin.Focus();
                CMessageBox.ShowError(string.Format("输入的起始楼层房间编码大于或等于结束楼层房间编码，请保证前者比后则小！"), Config.DialogTitle);
                return;
            }

            string strBegin = FormatRoomCode(iBeginCode);
            string strEnd = FormatRoomCode(iEndCode);
            int iBeginRoom = Convert.ToInt32(strBegin.Substring(2, 2));
            int iEndRoom = Convert.ToInt32(strEnd.Substring(2, 2));

            if (iBeginRoom >= iEndRoom)
            {
                txtRoomCodeBegin.Focus();
                CMessageBox.ShowError(string.Format("输入的起始房间编码大于或等于结束房间编码，请保证前者比后则小！"), Config.DialogTitle);
                return;
            }

            if (MessageBox.Show("确定要现在批量生成吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //0010 0011
            //0018 1111

            btnOK.Enabled = false;
            btnCancel.Enabled = false;
            tbxComunicateData.Clear();
            Model.BuidingBatchCondition objCondition = new Model.BuidingBatchCondition(iUnitID, iBeginCode, iEndCode);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TaskProcess), objCondition);
        }

        /// <summary>
        /// 创建楼层房间
        /// </summary>
        private void TaskProcess(object state)
        {
            int iSuccNum = 0;
            int iFaultNum = 0;
            SetButtonEnabled(false);
            try
            {
                Model.BuidingBatchCondition objCondition = (Model.BuidingBatchCondition)state;
                if (objCondition != null)
                {
                    if (!(objCondition.UnitID <= 0 || objCondition.BeginCode >= objCondition.EndCode))
                    {
                        string strBegin = FormatRoomCode(objCondition.BeginCode);
                        string strEnd = FormatRoomCode(objCondition.EndCode);
                        int iBeginFLoor = Convert.ToInt32(strBegin.Substring(0, 2));
                        int iEndFLoor = Convert.ToInt32(strEnd.Substring(0, 2));

                        int iBeginRoom = Convert.ToInt32(strBegin.Substring(2, 2));
                        int iEndRoom = Convert.ToInt32(strEnd.Substring(2, 2));

                        IList<Model.Building> listRoom = new List<Model.Building>();
                        if (iBeginFLoor < iEndFLoor)
                        {
                            for (int f = iBeginFLoor; f <= iEndFLoor; f++)
                            {
                                for (int r = iBeginRoom; r <= iEndRoom; r++)
                                {
                                    Model.Building objModel = new Model.Building()
                                    {
                                        Flag = 3,
                                        FID = objCondition.UnitID,
                                        ID = 0,
                                        BName = BQ2(f) + BQ2(r),
                                        Code = f * 100 + r,
                                        Contact = "",
                                        Tel = "",
                                        BuildingSerialNo = ""
                                    };
                                    listRoom.Add(objModel);
                                }
                            }
                        }
                        else
                        {
                            for (int r = iBeginRoom; r <= iEndRoom; r++)
                            {
                                Model.Building objModel = new Model.Building()
                                {
                                    Flag = 3,
                                    FID = objCondition.UnitID,
                                    ID = 0,
                                    Code = iBeginFLoor * 100 + r
                                };
                                objModel.BName = FormatRoomCode(objModel.Code);
                                objModel.Contact = "";
                                objModel.Tel = "";
                                objModel.BuildingSerialNo = "";
                                listRoom.Add(objModel);
                            }
                        }
                        
                        string strErrorInfo = "";
                        if (!(listRoom == null || listRoom.Count == 0))
                        {
                            IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
                            foreach (Model.Building objModel in listRoom)
                            {
                                if (this._FormIfClose) return;
                                int iNewID = objDAL.Add(objModel, out strErrorInfo);
                                if (iNewID <= 0)
                                {
                                    iFaultNum++;
                                    ShowNotice(string.Format("创建楼层房间{0}数据失败，(原因：{1})！", objModel.BName, strErrorInfo));
                                }
                                else
                                {
                                    iSuccNum++;
                                    ShowNotice(string.Format("创建楼层房间{0}数据成功！", objModel.BName));
                                }
                            }
                        }
                        ShowNotice(string.Format("\r\n生成报告：成功数{0}，失败数{1}，总计：{2}！\r\n\r\n", iSuccNum, iFaultNum, (iSuccNum + iFaultNum)));
                    }
                }
            }
            catch (Exception err)
            {
                ShowNotice(string.Format("创建房间数据失败，(原因：{0})！", err.Message));
            }
            SetButtonEnabled(true);
            if (iSuccNum > 0) Manager.GetInstance().BuildingDataChangeNotice();
            CMessageBox.ShowSucc("生成结束！", Config.DialogTitle);
        }
        
        /// <summary>
        /// 输出数据
        /// </summary>
        /// <param name="strMsg"></param>
        private void ShowNotice(string strMsg, bool bIfBR = true)
        {
            try
            {
                this.Invoke(new EventHandler(delegate
                {
                    string strNR = (bIfBR) ? "\r\n" : "";
                    tbxComunicateData.Text = string.Format("{0}{1}{2}", tbxComunicateData.Text, strNR, strMsg);
                }));
            }
            catch { }
        }

        private void SetButtonEnabled(bool bEnabled)
        {
            this.Invoke(new EventHandler(delegate
            {
                btnOK.Enabled = bEnabled;
                btnCancel.Enabled = bEnabled;
            }));
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

        private bool CheckCondition(Model.BuidingBatchCondition objCondition, out string strErrMessage)
        {
            bool bIfOk = false;
            strErrMessage = "";
            if (objCondition != null)
            {
                if (objCondition.UnitID <= 0)
                {
                    strErrMessage = "单元编号小于0";
                }
                if (!(objCondition.UnitID <= 0 || objCondition.BeginCode >= objCondition.EndCode))
                {
                    //0010 0011
                    //0010 1111
                }

            }
            else
            {
                strErrMessage = "无调教";
            }
            return bIfOk;
        }
    }
}
