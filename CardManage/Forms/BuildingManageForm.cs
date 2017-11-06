using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CardManage.Model;
using CardManage.Tools;
namespace CardManage.Forms
{
    /// <summary>
    /// 建筑管理
    /// </summary>
    public partial class BuildingManageForm : TreeFormBase
    {
        /// <summary>
        /// 0：区；1：栋；2：单元；3：房间；
        /// </summary>
        private int _DataType;

        private string _DataTypeName;

        protected BuildingManageForm()
        {
            InitializeComponent();
        }

        public BuildingManageForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
            HideSearchButton();

            this._DataType = (this.Flag == null) ? 0 : Functions.FormatInt(this.Flag.Keyword1);
            if (this._DataType < 0 || this._DataType > 4) this._DataType = 0;

            //模块表头
            switch (this._DataType)
            {
                case 0:
                    this._DataTypeName = "小区";
                    this.LVHeaderSetting = "序号,50|小区名称,100|小区编码,80|小区序列号,220|管理员姓名,100|管理员联系电话,110";
                    HideTree();
                    break;
                case 1:
                    this._DataTypeName = "楼栋";
                    this.LVHeaderSetting = "序号,50|楼栋名称,100|楼栋编码,80|所在小区,100|管理员姓名,100|管理员联系电话,110";
                    break;
                case 2:
                    this._DataTypeName = "单元";
                    this.LVHeaderSetting = "序号,50|单元名称,100|单元编码,80|所在楼栋,100|所在小区,120|管理员姓名,100|管理员联系电话,110";
                    break;
                case 3:
                    this._DataTypeName = "房间";
                    this.LVHeaderSetting = "序号,50|房间名称,100|房间编码,80|所在单元,100|所在楼栋,100|所在小区,120|管理员姓名,100|管理员联系电话,110";
                    break;
            }
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="objTV"></param>
        protected override void InitTree(TreeView objTV)
        {
            this.BindBuildTreeData(objTV, this._DataType - 1);
            objTV.ExpandAll();
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
                if (objNData.Flag.Equals(this._DataType - 1))
                {
                    strKey = string.Format("{0}", objNData.ID);
                }
            }
            OpenViewForm(EAction.Create, strKey);
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="selectedItem">当前选择的行项</param>
        protected override void OnEdit(ListViewItem selectedItem)
        {
            Building objModel = (Building)selectedItem.Tag;
            OpenViewForm(EAction.Edit, objModel.ID.ToString());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="selectedItems">当前选择的所有项</param>
        protected override void OnDelete(ListView.SelectedListViewItemCollection selectedItems)
        {
            if (MessageBox.Show(string.Format("删除{0}数据的同时会把下属的建筑数据以及卡片信息一起删除，请慎重选择，确定要删除当前选择的{1}数据吗？", this._DataTypeName, this._DataTypeName), Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int iSuccNum = 0;
            int iFaultNum = 0;
            foreach (ListViewItem selectedItem in selectedItems)
            {
                Building objModel = (Building)selectedItem.Tag;
                IDAL.IBuilding objDal = DALFactory.DALFactory.Building();
                if (objDal.Delete(objModel.ID))
                {
                    iSuccNum++;
                }
                else
                {
                    iFaultNum++;
                }
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
            if (iSuccNum > 0)
            {
                //重新刷新列表
                BindLVData(0, this.CurrentSqlWhere);
                //当建筑信息删除时，通知其他已开启的树
                Manager.GetInstance().BuildingDataChangeNotice();
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

                IList<Building> listData = null;
                IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
                listData = objDAL.GetListByWhere(this._DataType, selNum, sqlWhereAndOrderBy);
                if (!(listData == null || listData.Count <= 0))
                {
                    foreach (Building model in listData)
                    {
                        ListViewItem item = null;

                        //当前模块
                        switch (this._DataType)
                        {
                            case 0://小区
                                //序号,50|小区名称,100|小区编码,80|小区序列号,130|管理员姓名,100|管理员联系电话,110
                                item = new ListViewItem(new string[] { Convert.ToString(this.lvContent.Items.Count + 1), model.BName, FormatBuildingCode(model.Code), model.BuildingSerialNo, model.Contact, model.Tel });
                                break;
                            case 1://栋
                                //序号,50|楼栋名称,100|楼栋编码,80|所在小区,100|管理员姓名,100|管理员联系电话,110
                                item = new ListViewItem(new string[] { Convert.ToString(this.lvContent.Items.Count + 1), model.BName, FormatBuildingCode(model.Code), model.FatherInfo.BName, model.Contact, model.Tel });
                                break;
                            case 2://单元
                                //序号,50|单元名称,100|单元编码,80|所在楼栋,100|所在小区,120|管理员姓名,100|管理员联系电话,110
                                item = new ListViewItem(new string[] { Convert.ToString(this.lvContent.Items.Count + 1), model.BName, FormatBuildingCode(model.Code), model.FatherInfo.BName, model.FatherInfo.FatherInfo.BName, model.Contact, model.Tel });
                                break;
                            case 3://房间
                                //序号,50|房间名称,100|房间编码,80|所在单元,100|所在楼栋,100|所在小区,120|管理员姓名,100|管理员联系电话,110
                                item = new ListViewItem(new string[] { Convert.ToString(this.lvContent.Items.Count + 1), model.BName, FormatRoomCode(model.Code), model.FatherInfo.BName, model.FatherInfo.FatherInfo.BName, model.FatherInfo.FatherInfo.FatherInfo.BName, model.Contact, model.Tel });
                                break;
                        }
                        item.Tag = model;
                        item.Font = new Font ("宋体", 9, FontStyle.Regular);
                        this.lvContent.Items.Add(item);
                    }
                }                
            }
            catch (Exception)
            {

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
                BuildingViewForm objModalForm = new BuildingViewForm((eAction.Equals(EAction.Create) ? this._DataTypeName + "资料创建" : this._DataTypeName + "资料修改"), true, this.CurrentUserInfo, new WindowSize(530, 371), new Flag(eAction, this._DataType, strKey));
                if (objModalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //重新刷新列表
                    BindLVData(0, this.CurrentSqlWhere);
                }
            }
        }
    }
}
