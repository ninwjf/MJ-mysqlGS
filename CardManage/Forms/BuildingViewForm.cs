using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CardManage.Tools;

namespace CardManage.Forms
{
    /// <summary>
    /// 建筑视图
    /// </summary>
    public partial class BuildingViewForm : SetFormBase
    {
        //原始数据
        private string _OldName = "";
        private int _OldCode = 0;
        private int _OldAreaID = 0;
        private int _OldBuildID = 0;
        private int _OldUnitID = 0;
        

        private EAction _CurrentAction;
        private int _CurrentID = 0;
        private int _CurrentParentID = 0;
        /// <summary>
        /// 当前数据的类型0：区；1：栋；2：单元；3：房间；
        /// </summary>
        private int _DataType = 0;

        private string _DataTypeName;

        protected BuildingViewForm()
        {
            InitializeComponent();
        }

        public BuildingViewForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;

            this._CurrentAction = (EAction)objFlag.Keyword1;
            this._DataType = Convert.ToInt16(objFlag.Keyword2);
            //模块表头
            switch (this._DataType)
            {
                case 0:
                    this._DataTypeName = "小区";
                    break;
                case 1:
                    this._DataTypeName = "楼栋";
                    break;
                case 2:
                    this._DataTypeName = "单元";
                    break;
                case 3:
                    this._DataTypeName = "房间";
                    break;
            }
            if (objFlag.Keyword2 != null)
            {
                switch (this._CurrentAction)
                {
                    case EAction.Create:
                        this._CurrentParentID = Convert.ToInt32(objFlag.Keyword3);
                        break;
                    case EAction.Edit:
                        this._CurrentID = Convert.ToInt32(objFlag.Keyword3);
                        break;
                }
            }

            //渲染界面
            RenderLayout();
        }

        private void BuildingViewForm_Load(object sender, EventArgs e)
        {
            this.IfFormLoadOk = false;
            InitData();
            this.IfFormLoadOk = true;
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            int iNewAreaID = 0;
            int iNewBuildID = 0;
            int iNewUnitID = 0;

            int iCode = 0;
            int iFatherID = 0;
            string strBName = txtName.Text.Trim();
            string strCode = txtCode.Text.Trim();
            string strContact = txtContact.Text.Trim();
            string strTel = txtTel.Text.Trim();
            string strSerialNo = txtSerialNo.Text.Trim();

            int iSelectID = 0;
            if (cbArea.Visible)
            {
                iSelectID = Convert.ToInt32(((Model.ComboBoxItem)cbArea.SelectedItem).Value);
                if (iSelectID <= 0)
                {
                    CMessageBox.ShowError("请选择一个小区！", Config.DialogTitle);
                    return;
                }
                if (this._DataType.Equals(1)) iFatherID = iSelectID;
                iNewAreaID = iSelectID;
            }
            if (cbBuild.Visible)
            {
                iSelectID = Convert.ToInt32(((Model.ComboBoxItem)cbBuild.SelectedItem).Value);
                if (iSelectID <= 0)
                {
                    CMessageBox.ShowError("请选择一个楼栋！", Config.DialogTitle);
                    return;
                }
                if (this._DataType.Equals(2)) iFatherID = iSelectID;
                iNewBuildID = iSelectID;
            }
            if (cbUnit.Visible)
            {
                iSelectID = Convert.ToInt32(((Model.ComboBoxItem)cbUnit.SelectedItem).Value);
                if (iSelectID <= 0)
                {
                    CMessageBox.ShowError("请选择一个单元！", Config.DialogTitle);
                    return;
                }
                if (this._DataType.Equals(3)) iFatherID = iSelectID;
                iNewUnitID = iSelectID;
            }

            if (strBName.Equals(""))
            {
                txtName.Focus();
                CMessageBox.ShowError(string.Format("请输入{0}名称！", this._DataTypeName), Config.DialogTitle);
                return;
            }
            if (strCode.Equals("") || !Functions.IsInt(strCode))
            {
                txtCode.Focus();
                CMessageBox.ShowError(string.Format("{0}编码不能为空，且范围必须是{1}之间的整数！", this._DataTypeName, (this._DataType.Equals(3) ? "0~9999" : "0~99")), Config.DialogTitle);
                return;
            }
            iCode = Functions.FormatInt(strCode);
            if (cbUnit.Visible)
            {
                //是房间
                if (!(iCode >= 0 && iCode <= 9999))
                {
                    txtCode.Focus();
                    CMessageBox.ShowError(string.Format("输入的{0}编码超出范围，取值范围是0~9999之间的整数！", this._DataTypeName), Config.DialogTitle);
                    return;
                }
            }
            else
            {
                if (!(iCode >= 0 && iCode <= 99))
                {
                    txtCode.Focus();
                    CMessageBox.ShowError(string.Format("输入的{0}编码超出范围，取值范围是0~99之间的整数！", this._DataTypeName), Config.DialogTitle);
                    return;
                }
            }

            if (this._DataType.Equals(0) && !CheckSerialNo(strSerialNo))
            {
                txtSerialNo.Focus();
                CMessageBox.ShowError(string.Format("序列号必须填写，且必须是16字节的16进制字符！"), Config.DialogTitle);
                return;
            }

            if (MessageBox.Show("确定要保存吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            Model.Building objModel = new Model.Building()
            {
                Flag = _DataType,
                FID = iFatherID,
                ID = this._CurrentID,
                BName = strBName,
                Code = iCode,
                Contact = strContact,
                Tel = strTel,
                BuildingSerialNo = strSerialNo
            };
            string strErrorInfo = "";
            IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
            if (this._CurrentAction.Equals(EAction.Create))
            {
                int iNewID = objDAL.Add(objModel, out strErrorInfo);
                if (iNewID <= 0)
                {
                    CMessageBox.ShowError(string.Format("创建{0}数据失败，错误如下：\r\n{1}！", this._DataTypeName, strErrorInfo), Config.DialogTitle);
                    return;
                }
            }
            else
            {
                bool bIfSucc = objDAL.Update(objModel, out strErrorInfo);
                if (!bIfSucc)
                {
                    CMessageBox.ShowError(string.Format("修改{0}数据失败，错误如下：\r\n{1}！", this._DataTypeName, strErrorInfo), Config.DialogTitle);
                    return;
                }
            }
            //是否通知建筑数据已经改变
            bool bIfNotice = false;
            if (this._CurrentAction == EAction.Create)
            {
                //新增必通知
                bIfNotice = true;
            }
            else
            {
                if (string.Compare(this._OldName, strBName) != 0 || !this._OldCode.Equals(iCode) || !this._OldAreaID.Equals(iNewAreaID) || !this._OldBuildID.Equals(iNewBuildID) || !this._OldUnitID.Equals(iNewUnitID))
                {
                    //条件满足通知
                    bIfNotice = true;
                }
            }
            if(bIfNotice) Manager.GetInstance().BuildingDataChangeNotice();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 渲染界面
        /// </summary>
        private void RenderLayout()
        {
            int iTop = 10;
            int iSpan = 28;
            int iBlance = 6;
            int iCbHeight = cbArea.Height;
            if (this._DataType >= 1 && this._DataType <= 3)
            {
                cbArea.Top = iTop;
                lbArea.Top = cbArea.Top + iBlance;
                cbArea.Visible = lbArea.Visible = true;

                iTop += iSpan;
            }

            if (this._DataType >= 2 && this._DataType <= 3)
            {
                cbBuild.Top = iTop;
                lbBuild.Top = cbBuild.Top + iBlance;
                cbBuild.Visible = lbBuild.Visible = true;

                iTop += iSpan;
            }

            if (this._DataType == 3)
            {
                cbUnit.Top = iTop;
                lbUnit.Top = cbUnit.Top + iBlance;
                cbUnit.Visible = lbUnit.Visible = true;

                lbRangeCode.Text = "(范围：0~9999)";
                txtCode.MaxLength = 4;
                iTop += iSpan;
            }
            else
            {
                lbRangeCode.Text = "(范围：0~99)";
                txtCode.MaxLength = 2;
            }

            //只有小区时才显示序列号
            if (this._DataType.Equals(0))
            {
                txtSerialNo.Visible = true;
                lbSerialNo.Visible = true;
                lbSerialNoTip.Visible = true;
            }
            else
            {
                txtSerialNo.Visible = false;
                lbSerialNo.Visible = false;
                lbSerialNoTip.Visible = false;
            }

            //定位名称
            lbName.Text = string.Format("{0}名称：", this._DataTypeName);
            txtName.Top = iTop;
            lbName.Top = txtName.Top + iBlance;
            iTop += iSpan;

            //定位编码
            txtCode.Top = iTop;
            lbCode.Top = txtCode.Top + iBlance;
            lbRangeCode.Top = lbCode.Top;
            iTop += iSpan;

            //定位姓名
            txtContact.Top = iTop;
            lbContact.Top = txtContact.Top + iBlance;
            iTop += iSpan;

            //定位电话
            txtTel.Top = iTop;
            lbTel.Top = txtTel.Top + iBlance;
            iTop += iSpan;

            //定位序列号
            txtSerialNo.Top = iTop;
            lbSerialNo.Top = txtSerialNo.Top + iBlance;
            lbSerialNoTip.Top = lbSerialNo.Top;
            iTop += iSpan;

            //定位按钮
            //btnOK.Top = btnCancel.Top = this.Height - btnOK.Height - 80;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            int iAreaID, iBuildID, iUnitID;
            iAreaID = iBuildID = iUnitID = 0;

            IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
            //小区
            cbArea.Items.Add(new Model.ComboBoxItem("请选择小区", 0));
            cbBuild.Items.Add(new Model.ComboBoxItem("请选择楼栋", 0));
            cbUnit.Items.Add(new Model.ComboBoxItem("请选择单元", 0));
            cbArea.SelectedIndex = 0;
            cbBuild.SelectedIndex = 0;
            cbUnit.SelectedIndex = 0;
            
            if (this._CurrentAction.Equals(EAction.Edit))
            {
                Model.Building objModel = objDAL.GetModel(this._DataType, this._CurrentID);
                if (objModel != null)
                {
                    txtName.Text = objModel.BName;
                    txtCode.Text = objModel.Flag.Equals(3) ? FormatRoomCode(objModel.Code) : FormatBuildingCode(objModel.Code);
                    txtContact.Text = objModel.Contact;
                    txtTel.Text = objModel.Tel;
                    txtSerialNo.Text = objModel.BuildingSerialNo;

                    switch (this._DataType)
                    {
                        case 1:
                            iAreaID = objModel.FID;
                            break;
                        case 2:
                            iBuildID = objModel.FID;
                            iAreaID = objModel.FatherInfo.FID;
                            break;
                        case 3:
                            iUnitID = objModel.FID;
                            iBuildID = objModel.FatherInfo.FID;
                            iAreaID = objModel.FatherInfo.FatherInfo.FID;
                            break;
                    }
                    //记录旧数据
                    this._OldName = objModel.BName;
                    this._OldCode = objModel.Code;
                    this._OldAreaID = iAreaID;
                    this._OldBuildID = iBuildID;
                    this._OldUnitID = iUnitID;
                }                
            }
            else
            {
                Model.Building objModel = null;
                switch (this._DataType)
                {
                    case 1:
                        iAreaID = this._CurrentParentID;
                        break;
                    case 2:
                        iBuildID = this._CurrentParentID;
                        objModel = objDAL.GetModel(this._DataType - 1, iBuildID);
                        if (objModel != null)
                        {
                            iAreaID = objModel.FID;
                        }
                        break;
                    case 3:
                        iUnitID = this._CurrentParentID;
                        objModel = objDAL.GetModel(this._DataType - 1, iUnitID);
                        if (objModel != null)
                        {
                            iBuildID = objModel.FID;
                            iAreaID = objModel.FatherInfo.FID;
                        }
                        break;
                }
            }

            if (iAreaID > 0 || (this._DataType >= 1 && this._DataType <= 3))
            {
                IList<Model.Building> lsAll = objDAL.GetListByWhere(-1, 100000, "1=1");

                int iCount = 0;
                int iSelectIndex = 0;
                IList<Model.Building> listArea = lsAll.Where(s => s.FID == 0).ToList();
                if (!(listArea == null || listArea.Count <= 0))
                {
                    foreach (Model.Building model in listArea)
                    {
                        iCount++;
                        if (model.ID.Equals(iAreaID)) iSelectIndex = iCount;
                        cbArea.Items.Add(new Model.ComboBoxItem(model.BName, model.ID));
                    }
                    cbArea.SelectedIndex = iSelectIndex;
                    //新增时如果小区只有一个就自动选择那个小区
                    if (this._CurrentAction.Equals(EAction.Create) && iSelectIndex == 0 && listArea.Count == 1)
                    {
                        cbArea.SelectedIndex = 1;
                        iAreaID = listArea[0].ID;
                    }
                }

                //新增时如果只有一个楼栋
                if (this._CurrentAction.Equals(EAction.Create) && iAreaID > 0 && iBuildID == 0)
                {
                    IList<Model.Building> listBuild = lsAll.Where(s => s.FID == iAreaID).ToList();
                    if (!(listBuild == null || listBuild.Count <= 0))
                    {
                        if (listBuild.Count == 1)
                        {
                            iBuildID = listBuild[0].ID;
                        }
                    }
                }

                if (iAreaID > 0)
                {
                    iCount = 0;
                    iSelectIndex = 0;
                    IList<Model.Building> listBuild = lsAll.Where(s => s.FID == iAreaID).ToList();
                    if (!(listBuild == null || listBuild.Count <= 0))
                    {
                        foreach (Model.Building model in listBuild)
                        {
                            iCount++;
                            if (model.ID.Equals(iBuildID)) iSelectIndex = iCount;
                            cbBuild.Items.Add(new Model.ComboBoxItem(model.BName, model.ID));
                        }
                        cbBuild.SelectedIndex = iSelectIndex;
                    }

                    //新增时如果只有一个单元
                    if (this._CurrentAction.Equals(EAction.Create) && iBuildID > 0 && iUnitID == 0)
                    {
                        IList<Model.Building> listUnit = lsAll.Where(s => s.FID == iBuildID).ToList();
                        if (!(listUnit == null || listUnit.Count <= 0))
                        {
                            if (listUnit.Count == 1)
                            {
                                iUnitID = listUnit[0].ID;
                            }
                        }
                    }

                    if (iBuildID > 0)
                    {
                        iCount = 0;
                        iSelectIndex = 0;
                        IList<Model.Building> listUnit = lsAll.Where(s => s.FID == iBuildID).ToList();
                        if (!(listUnit == null || listUnit.Count <= 0))
                        {
                            foreach (Model.Building model in listUnit)
                            {
                                iCount++;
                                if (model.ID.Equals(iUnitID)) iSelectIndex = iCount;
                                cbUnit.Items.Add(new Model.ComboBoxItem(model.BName, model.ID));
                            }
                            cbUnit.SelectedIndex = iSelectIndex;
                        }
                    }
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

                IList<Model.Building> lsAll;
                IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
                if (string.Compare(strCbName, "cbArea", true) == 0)
                {
                    if (cbBuild.Visible)
                    {
                        cbBuild.Items.Clear();
                        cbBuild.Items.Add(new Model.ComboBoxItem("请选择楼栋", 0));
                    }
                    if (cbUnit.Visible)
                    {
                        cbUnit.Items.Clear();
                        cbUnit.Items.Add(new Model.ComboBoxItem("请选择单元", 0));
                    }

                    //初始化楼栋
                    if (iCurrentID > 0 && cbBuild.Visible)
                    {
                        lsAll = objDAL.GetListByWhere(-1, 100000, "1=1");
                        IList<Model.Building> listBuild = lsAll.Where(s => s.FID == iCurrentID).ToList();
                        if (!(listBuild == null || listBuild.Count <= 0))
                        {
                            foreach (Model.Building model in listBuild)
                            {
                                cbBuild.Items.Add(new Model.ComboBoxItem(model.BName, model.ID));
                            }
                        }
                    }
                    cbBuild.SelectedIndex = 0;
                }
                else if (string.Compare(strCbName, "cbBuild", true) == 0)
                {
                    if (cbUnit.Visible)
                    {
                        cbUnit.Items.Clear();
                        cbUnit.Items.Add(new Model.ComboBoxItem("请选择单元", 0));
                    }

                    //初始化单元
                    if (iCurrentID > 0 && cbUnit.Visible)
                    {
                        lsAll = objDAL.GetListByWhere(-1, 100000, "1=1");
                        IList<Model.Building> listUnit = lsAll.Where(s => s.FID == iCurrentID).ToList();
                        if (!(listUnit == null || listUnit.Count <= 0))
                        {
                            foreach (Model.Building model in listUnit)
                            {
                                cbUnit.Items.Add(new Model.ComboBoxItem(model.BName, model.ID));
                            }
                        }
                    }
                    cbUnit.SelectedIndex = 0;
                }
            }
        }

        private void MaskSerialNoText(object sender, KeyPressEventArgs e)
        {
            //a-f:97-102
            //A-F:65-70

            //判断输入的值是否为数字、16进制字符、冒号或删除键或粘贴键22或Copy键3
            if (char.IsDigit(e.KeyChar) || (e.KeyChar >= 97 && e.KeyChar <= 102) || (e.KeyChar >= 65 && e.KeyChar <= 70) || e.KeyChar == 8 || e.KeyChar == 58 || e.KeyChar == 22 || e.KeyChar == 3)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private bool CheckSerialNo(string strSerialNo)
        {
            bool bIfOk = false;
            if (!string.IsNullOrEmpty(strSerialNo))
            {
                strSerialNo = strSerialNo.Trim();
                if (!strSerialNo.Equals("") && strSerialNo.Length == 32)
                {
                    try
                    {
                        byte[] bArrSerialNo = ScaleConverter.HexStr2ByteArr(strSerialNo);
                        if (bArrSerialNo.Length == 16) bIfOk = true;
                    }
                    catch
                    {

                    }
                }
            }
            return bIfOk;
        }
    }
}
