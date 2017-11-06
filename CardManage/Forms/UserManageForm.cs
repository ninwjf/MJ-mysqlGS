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
    /// 用户管理
    /// </summary>
    public partial class UserManageForm : TreeFormBase
    {
        protected UserManageForm()
        {
            InitializeComponent();
        }

        public UserManageForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
            this.HideSearchButton();
            //表头
            this.LVHeaderSetting = "序号,50|用户名,100|角色,180|备注,300";
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="objTV"></param>
        protected override void InitTree(TreeView objTV)
        {
            objTV.BeginUpdate();
            objTV.Nodes.Clear();
            TreeNode rootNode = this.CreateTreeNode(new NodeData(-1, "全部角色", -1));
            rootNode.Nodes.Add(this.CreateTreeNode(new NodeData(10, "超级管理员", 0)));
            rootNode.Nodes.Add(this.CreateTreeNode(new NodeData(10, "普通管理员", 1)));

            objTV.Nodes.Add(rootNode);
            objTV.ExpandAll();
            objTV.EndUpdate();
        }

        /// <summary>
        /// 当选择一个树节点时
        /// </summary>
        protected override void OnSelectNode(TreeNode selectedNode)
        {
            if (!(selectedNode == null || selectedNode.Tag == null))
            {
                string strSqlWhere = string.Format("ID <> 1 AND ID<>{0}", this.CurrentUserInfo.ID);
                NodeData objNData = (NodeData)selectedNode.Tag;
                if(objNData.Flag.Equals(10)){
                    strSqlWhere += string.Format(" AND Flag={0}", objNData.ID);
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
                if (objNData.Flag.Equals(10))
                {
                    strKey = string.Format("{0}",objNData.ID);
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
            UserInfo objModel = (UserInfo)selectedItem.Tag;
            OpenViewForm(EAction.Edit, string.Format("{0}", objModel.ID));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="selectedItems)">当前选择的所有项</param>
        protected override void OnDelete(ListView.SelectedListViewItemCollection selectedItems)
        {
            if (MessageBox.Show("确定要删除当前选择的用户吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int iSuccNum = 0;
            int iFaultNum = 0;
            foreach (ListViewItem selectedItem in selectedItems)
            {
                UserInfo objModel = (UserInfo)selectedItem.Tag;
                IDAL.IUserInfo objDal = DALFactory.DALFactory.UserInfo();
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
                if (string.IsNullOrEmpty(sqlWhereAndOrderBy) || sqlWhereAndOrderBy.Equals("")) sqlWhereAndOrderBy = string.Format("ID <> 1 AND ID<>{0}", this.CurrentUserInfo.ID);
                this.CurrentSqlWhere = sqlWhereAndOrderBy;

                IDAL.IUserInfo objDal = DALFactory.DALFactory.UserInfo();
                IList<UserInfo> listData = objDal.GetListByWhere(selNum, sqlWhereAndOrderBy);
                if (!(listData == null || listData.Count <= 0))
                {
                    foreach (UserInfo model in listData)
                    {
                        //序号,60|用户名,100|级别,180|备注,300
                        ListViewItem item = new ListViewItem(new string[] { Convert.ToString(this.lvContent.Items.Count + 1), model.UserName, model.FlagDesc, model.Memo })
                        {
                            Tag = model,
                            Font = new Font("宋体", 9, FontStyle.Regular)
                    };
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
                UserViewForm objModalForm = new UserViewForm((eAction.Equals(EAction.Create) ? "用户创建" : "用户修改"), true, this.CurrentUserInfo, new WindowSize(360, 320), new Flag(eAction, strKey));
                if (objModalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //重新刷新列表
                    BindLVData(0, this.CurrentSqlWhere);
                }
            }
        }
    }
}
