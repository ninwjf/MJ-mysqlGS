using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using CardManage.Tools;
namespace CardManage.Forms
{
    public partial class DataBatchDeleteForm : SetFormBase
    {
        private bool _FormIfClose = false;
        private bool _DoCheck = false;

        /// <summary>
        /// 窗体激活次数
        /// </summary>
        private int _FormActivatedTimes = 0;
        protected DataBatchDeleteForm()
        {
            InitializeComponent();
        }

        public DataBatchDeleteForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

        private void DataBatchDeleteForm_Load(object sender, EventArgs e)
        {
            SetButtonEnabled(false);
            LoadTreeData(null);
        }

        private void DataBatchDeleteForm_Activated(object sender, EventArgs e)
        {
            //这个一定要有累计标识响应不然会出现系统错误
            if (Config.IfDelayLoadData)
            {
                this._FormActivatedTimes++;
                if (this._FormActivatedTimes == 1)
                {
                    RefreshTreeByThread();
                }
            }
        }

        private void DataBatchDeleteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._FormIfClose = true;
        }

        /// <summary>
        /// 通过线程刷新树
        /// </summary>
        protected void RefreshTreeByThread()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadTreeData), null);
            //Thread objThread = new Thread(InitTree);
            //objThread.IsBackground = true;
            //objThread.Start();
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="objTV"></param>
        protected void InitTree()
        {
            this.Invoke(new EventHandler(delegate
            {
                this.BindBuildTreeData(this.tv, 2);
                if (this.tv.Nodes.Count > 0) this.tv.Nodes[0].Expand();
            }));
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            List<Model.NodeData> listSelectedData = new List<Model.NodeData>();
            GetSelectedNodes(ref listSelectedData, this.tv.Nodes[0]);
            if (listSelectedData.Count > 0)
            {
                if (MessageBox.Show("删除后，数据将无法恢复，您确定要删除您勾选的那些数据吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                Model.SelecedTreeNodeData objSelecedTreeNodeData = new Model.SelecedTreeNodeData()
                {
                    SelectedDataList = listSelectedData
                };
                SetButtonEnabled(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(DeleteSelectedData), objSelecedTreeNodeData);
            }
            else
            {
                CMessageBox.ShowWaring("请勾选您要删除的数据！", Config.DialogTitle);
            }
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除后，数据将无法恢复，您确定要现在删除全部数据吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            SetButtonEnabled(false);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DeleteAllData), null);
        }

        /// <summary>
        /// 加载树数据
        /// </summary>
        private void LoadTreeData(object state)
        {
            SetButtonEnabled(false);
            InitTree();
            SetButtonEnabled(true);
        }

        /// <summary>
        /// 创建全部数据
        /// </summary>
        private void DeleteAllData(object state)
        {
            bool bIfSucc = false;
            string strErrMessag = "未知错误";
            SetButtonEnabled(false);
            try
            {
                if (this._FormIfClose) return;
                IDAL.ISys objDal = DALFactory.DALFactory.Sys();
                bIfSucc = objDal.DeleteData(0);
                if (bIfSucc) InitTree();
            }
            catch(Exception err)
            {
                strErrMessag = err.Message;
            }
            if (bIfSucc) RefreshTreeByThread();
            //SetButtonEnabled(true);
            if (bIfSucc)
            {
                CMessageBox.ShowSucc("恭喜您，删除成功！", Config.DialogTitle);
                //当建筑信息删除时，通知其他已开启的树
                //Manager.GetInstance().BuildingDataChangeNotice();
            }
            else
            {
                CMessageBox.ShowError(string.Format("对不起，删除失败，错误原因：\r\n{0}", strErrMessag), Config.DialogTitle);
            }
        }



        /// <summary>
        /// 创建选择的数据
        /// </summary>
        private void DeleteSelectedData(object state)
        {
            bool bIfSucc = false;
            string strErrMessag = "未知错误";
            SetButtonEnabled(false);
            try
            {
                if (this._FormIfClose) return;
                Model.SelecedTreeNodeData objSelecedTreeNodeData = (Model.SelecedTreeNodeData)state;
                if (!(objSelecedTreeNodeData.SelectedDataList == null || objSelecedTreeNodeData.SelectedDataList.Count == 0))
                {
                    IDAL.IBuilding objDalBuilding = DALFactory.DALFactory.Building();
                    IDAL.ISys objDalSys = DALFactory.DALFactory.Sys();
                    foreach (Model.NodeData objData in objSelecedTreeNodeData.SelectedDataList)
                    {
                        if (objData.Flag.Equals(-1))
                        {
                            //全部
                            bIfSucc = objDalSys.DeleteData(0);
                        }
                        else if (objData.Flag >=0 && objData.Flag <=3)
                        {
                            bool bSucc = objDalBuilding.Delete(objData.ID);
                            if (bSucc) bIfSucc = bSucc;
                        }
                    }
                }
                if (bIfSucc) InitTree();
            }
            catch (Exception err)
            {
                strErrMessag = err.Message;
            }
            if (bIfSucc) RefreshTreeByThread();
            SetButtonEnabled(true);
            if (bIfSucc)
            {
                CMessageBox.ShowSucc("恭喜您，删除成功！", Config.DialogTitle);
                //当建筑信息删除时，通知其他已开启的树
                Manager.GetInstance().BuildingDataChangeNotice();
            }
            else
            {
                CMessageBox.ShowError(string.Format("对不起，删除失败，错误原因：\r\n{0}", strErrMessag), Config.DialogTitle);
            }
        }

        private void SetButtonEnabled(bool bEnabled)
        {
            this.Invoke(new EventHandler(delegate
            {
                try
                {
                    btnOK.Enabled = bEnabled;
                    btnCancel.Enabled = bEnabled;
                    btnDeleteAll.Enabled = bEnabled;
                }
                catch
                {
                }
            }));
        }

        private void Tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (this._DoCheck) return;
            this._DoCheck = true;
            //label1.Text = string.Format("{0}:{1}", e.Node.Text, e.Node.Checked);
            SetNodeCheckStatus(e.Node, e.Node.Checked);
            this._DoCheck = false;
        }

        private void SetNodeCheckStatus(TreeNode nodeParent, bool bChecked)
        {
            if (!(nodeParent.Nodes == null || nodeParent.Nodes.Count == 0))
            {
                foreach (TreeNode node in nodeParent.Nodes)
                {
                    node.Checked = bChecked;
                    SetNodeCheckStatus(node, bChecked);
                }
            }
        }

        private void GetSelectedNodes(ref List<Model.NodeData> listNodeData, TreeNode nodeParent)
        {
            if (!(nodeParent.Nodes == null || nodeParent.Nodes.Count == 0))
            {
                foreach (TreeNode node in nodeParent.Nodes)
                {
                    if (node.Checked) listNodeData.Add((Model.NodeData)node.Tag);
                    GetSelectedNodes(ref listNodeData, node);
                }
            }
        }
    }
}
