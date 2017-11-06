using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CardManage.Model;
using CardManage.Tools;
using System.Threading;
namespace CardManage.Forms
{
    /// <summary>
    /// 发卡读卡
    /// </summary>
    public partial class CardManageForm : TreeFormBase
    {
        /// <summary>
        /// 搜索条件
        /// </summary>
        private CardSearchCondition _CardSearchCondition;

        protected CardManageForm()
        {
            InitializeComponent();
        }

        public CardManageForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
            this.SetCreateButtonText("发卡读卡");
            this.SetEditButtonText("编辑卡片");
            this.SetDeleteButtonText("删除卡片");
            this.LVHeaderSetting = "序号,50|卡号,80|卡片类型,70|小区编码,70|楼栋编码,70|单元编码,70|房间编码,80|卡片系列号,210|卡片有效期,100|发卡时间,100|持卡者姓名,100|联系电话,100|所在房间,100|所在单元,100|所在楼栋,100|所在小区,120";
        }

        private void CardManageForm_Load(object sender, EventArgs e)
        {
            Manager.GetInstance().OnBuildingDataChange += new Manager.BuildingDataChangeHandler(OnBuildingDataChange);
            Manager.GetInstance().OnCardDataChange += new Manager.CardDataChangeHandler(OnCardDataChange);
        }

        private void CardManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.GetInstance().OnBuildingDataChange -= new Manager.BuildingDataChangeHandler(OnBuildingDataChange);
            Manager.GetInstance().OnCardDataChange -= new Manager.CardDataChangeHandler(OnCardDataChange);
        }

        /// <summary>
        /// 回调方法-当建筑信息改变时
        /// </summary>
        /// <param name="strNotice"></param>
        private void OnBuildingDataChange()
        {
            if (Config.IfDelayLoadData)
            {
                this.RefreshTreeByThread();
                this.RefreshLVByThread();
            }
            else
            {
#pragma warning disable CS0162 // 检测到无法访问的代码
                InitTree(tv);
                BindLVData(DefaultPageSize, CurrentSqlWhere);
#pragma warning restore CS0162 // 检测到无法访问的代码
            }
        }

        /// <summary>
        /// 回调方法-当卡片信息改变时
        /// </summary>
        /// <param name="strNotice"></param>
        private void OnCardDataChange()
        {
            if (Config.IfDelayLoadData)
            {
                this.RefreshLVByThread();
            }
            else
            {
#pragma warning disable CS0162 // 检测到无法访问的代码
                BindLVData(DefaultPageSize, CurrentSqlWhere);
#pragma warning restore CS0162 // 检测到无法访问的代码
            }
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="objTV"></param>
        protected override void InitTree(TreeView objTV)
        {
            this.BindBuildTreeData(objTV, 4, true);
            if (objTV.Nodes.Count > 0) objTV.Nodes[0].Expand();
        }

        /// <summary>
        /// 当选择一个树节点时
        /// </summary>
        protected override void OnSelectNode(TreeNode selectedNode)
        {
            if (!(selectedNode == null || selectedNode.Tag == null))
            {
                string strSqlWhere = "1=1";
                NodeData objNData = (NodeData)selectedNode.Tag;
                switch (objNData.Flag)
                {
                    case 0://选择了区
                        strSqlWhere = string.Format("AreaID={0}", objNData.ID);
                        break;
                    case 1://选择了栋
                        strSqlWhere = string.Format("BuildID={0}", objNData.ID);
                        break;
                    case 2://选择了单元
                        strSqlWhere = string.Format("UnitID={0}", objNData.ID);
                        break;
                    case 3://选择了房间
                        strSqlWhere = string.Format("RoomID={0}", objNData.ID);
                        break;
                    case 20://选择了未归属
                        strSqlWhere = string.Format("BuildingID=0 OR (RoomID=0 AND AreaID=0)");
                        break;
                    case 21://选择了管理卡
                        strSqlWhere = string.Format("(CardType=1 OR CardType=2) AND (AreaID={0} OR SerialNo='{1}')", objNData.ID, objNData.SerialNo);
                        break;
                    default:
                        strSqlWhere = "1=1";
                        break;
                }
                BindLVData(0, strSqlWhere);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="selectedNode">当前选择的树节点</param>
        protected override void OnCreate(TreeNode selectedNode)
        {
            string strKey = null;
            if (selectedNode != null)
            {
                NodeData objNData = (NodeData)selectedNode.Tag;
                strKey = string.Format("{0}", objNData.ID);
            }
            OpenViewForm(EAction.Create, strKey);
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="selectedItem">当前选择的行项</param>
        protected override void OnEdit(ListViewItem selectedItem)
        {
            Card objModel = (Card)selectedItem.Tag;
            OpenViewForm(EAction.Edit, objModel.ID.ToString());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="selectedItems">当前选择的行项</param>
        protected override void OnDelete(ListView.SelectedListViewItemCollection selectedItems)
        {
            if (MessageBox.Show(string.Format("确定要删除当前选择的卡片信息吗？"), Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int iSuccNum = 0;
            int iFaultNum = 0;
            foreach (ListViewItem selectedItem in selectedItems)
            {
                Card objModel = (Card)selectedItem.Tag;
                IDAL.ICard objDal = DALFactory.DALFactory.Card();
                if (objDal.Delete(objModel.ID))
                {
                    iSuccNum++;
                }
                else
                {
                    iFaultNum++;
                }
            }
            
            if (iSuccNum > 0)
            {
                //重新刷新列表
                BindLVData(0, this.CurrentSqlWhere);
            }

            if (iSuccNum == 0)
            {
                CMessageBox.ShowError("对不起，删除失败！", Config.DialogTitle);
            }
            else if (iFaultNum == 0)
            {
                CMessageBox.ShowSucc("恭喜您，删除成功！", Config.DialogTitle);
            }
            else
            {
                CMessageBox.ShowSucc(string.Format("{0}个删除成功，{1}个删除失败", iSuccNum, iFaultNum), Config.DialogTitle);
            }
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        protected override void OnSearch()
        {
            CardSearchForm objModalForm = new CardSearchForm("搜索条件", true, this.CurrentUserInfo, new WindowSize(374, 316), new Flag(EType.CARD, this._CardSearchCondition));
            if (objModalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._CardSearchCondition = (CardSearchCondition)objModalForm.Condition.Clone();
                if (this._CardSearchCondition != null)
                {
                    this.CurrentSqlWhere = BuildSqlWhereByCondition(this._CardSearchCondition);
                    //重新刷新列表
                    BindLVData(0, this.CurrentSqlWhere);
                }
            }
        }

        /// <summary>
        /// 绑定数据到列表
        /// </summary>
        /// <param name="selNum">数据条数</param>
        /// <param name="sqlWhereAndOrderBy">条件</param>
        protected override void BindLVData(int selNum = 0, string sqlWhereAndOrderBy = null)
        {
            this.lvContent.BeginUpdate();
            this.lvContent.Items.Clear();
            try
            {
                if (selNum <= 0) selNum = this.DefaultPageSize;
                if (string.IsNullOrEmpty(sqlWhereAndOrderBy) || sqlWhereAndOrderBy.Equals("")) sqlWhereAndOrderBy = "1=1";
                this.CurrentSqlWhere = sqlWhereAndOrderBy;

                IList<Card> listData = null;
                IDAL.ICard objDAL = DALFactory.DALFactory.Card();
                listData = objDAL.GetListByWhere(selNum, sqlWhereAndOrderBy);
                if (!(listData == null || listData.Count <= 0))
                {
                    foreach (Card model in listData)
                    {
                        //序号,50|卡号,80|卡片类型,70|小区编码,60|楼栋编码,60|单元编码,60|房间编码,80|卡片系列号,210|卡片有效期,100|发卡时间,100|持卡者姓名,100|联系电话,100|所在房间,100|所在单元,100|所在楼栋,100|所在小区,120
                        ListViewItem item = new ListViewItem(new string[] { Convert.ToString(lvContent.Items.Count + 1), model.CardNo.ToString(), model.CardTypeDesc, FormatBuildingCode(model.RAreaCode), FormatBuildingCode(model.RBuildCode), FormatBuildingCode(model.RUnitCode), FormatRoomCode(model.RRoomCode), model.SerialNo, Functions.ConvertToNormalTime(model.ExpiryDate).ToString(Config.TimeFormat), Functions.ConvertToNormalTime(model.CreateDate).ToString(Config.TimeFormat), model.Contact, model.Tel,
                            model.RoomName, model.UnitName, model.BuildName, model.AreaName })
                        {
                            Tag = model,
                            Font = new Font("宋体", 9, FontStyle.Regular)
                    };
                        this.lvContent.Items.Add(item);
                    }
                }
            }
            catch (Exception err)
            {
                CMessageBox.ShowWaring(string.Format("系统错误,错误如下：\r\n{0}！", err.Message), Config.DialogTitle);
            }
            this.lvContent.EndUpdate();
        }

        /// <summary>
        /// 打开查看Form
        /// </summary>
        /// <param name="strAction">动作，只支持Create和Edit动作</param>
        /// <param name="strKey">附带值</param>
        private void OpenViewForm(EAction eAction, string strKey)
        {
            if (eAction.Equals(EAction.Create) || eAction.Equals(EAction.Edit))
            {
                WindowSize objSize = RunVariable.IfDebug ? new WindowSize(801, 596) : new WindowSize(801, 371);
                CardViewForm objModalForm = new CardViewForm((eAction.Equals(EAction.Create) ? "卡片资料创建" : "卡片资料修改"), true, this.CurrentUserInfo, objSize, new Flag(eAction, strKey));
                if (objModalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //重新刷新列表
                    BindLVData(0, this.CurrentSqlWhere);
                }
            }
        }

        /// <summary>
        /// 根据条件生成SqlWhere语句
        /// </summary>
        /// <param name="objCondition"></param>
        /// <returns></returns>
        private string BuildSqlWhereByCondition(CardSearchCondition objCondition)
        {
            string strSqlWhere = "1=1";
            if (objCondition != null)
            {
                //卡片类型
                if (objCondition.CardType >= 0 && objCondition.CardType <= 2) strSqlWhere += string.Format(" AND CardType={0}", objCondition.CardType);
                //卡片有效性
                Int64 iUnixTimeNow = Functions.ConvertToUnixTime(Convert.ToDateTime(string.Format("{0} 00:00:00", DateTime.Now.ToString("yyyy-MM-dd"))));
                if (objCondition.CardValid.Equals(0))
                {
                    strSqlWhere += string.Format(" AND ExpiryDate>={0}", iUnixTimeNow);
                }
                else if (objCondition.CardValid.Equals(1))
                {
                    strSqlWhere += string.Format(" AND ExpiryDate<{0}", iUnixTimeNow);
                }
                //卡号
                if (!objCondition.CardNo.Equals(""))
                {
                    strSqlWhere += string.Format(" AND CardNo LIKE '%{0}%'", objCondition.CardNo);
                }
                //系列号
                if (!objCondition.SerialNo.Equals(""))
                {
                    strSqlWhere += string.Format(" AND SerialNo LIKE '%{0}%'", objCondition.SerialNo);
                }
                //联系人
                if (!objCondition.Contact.Equals(""))
                {
                    strSqlWhere += string.Format(" AND Contact LIKE '%{0}%'", objCondition.Contact);
                }
            }
            return strSqlWhere;
        }
    }
}
