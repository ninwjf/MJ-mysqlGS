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
using System.Threading;
namespace CardManage.Forms
{
    public partial class TreeFormBase : FormBase
    {
        //创建一个委托
        public delegate void InitTreeHandler();
        //定义一个委托变量
        public InitTreeHandler objInitTreeHandler;

        /// <summary>
        /// 表头列配置
        /// </summary>
        protected string LVHeaderSetting;//规则:名称1,宽度1|名称2,宽度2

        /// <summary>
        /// 当前条件
        /// </summary>
        protected string CurrentSqlWhere = "1=1";

        /// <summary>
        /// 窗体激活次数
        /// </summary>
        private int _FormActivatedTimes = 0;
        protected TreeFormBase()
        {
            InitializeComponent();
        }

        public TreeFormBase(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;

            //普通管理员没有修改功能
            bool bIsSupper = objUserInfo.Flag.Equals(0);
            newToolStripButton.Visible = bIsSupper;
            editToolStripButton.Visible = bIsSupper;
            deleteToolStripButton.Visible = bIsSupper;
        }

        private void TreeFormBase_Load(object sender, EventArgs e)
        {
            this.IfFormLoadOk = false;
            //实例化委托
            objInitTreeHandler = new InitTreeHandler(InitTree2);

            //this.tv.ImageList = this.imageList1;
            //this.tv.ImageList.ImageSize = new System.Drawing.Size(16, 16);
            //初始化树数据
#pragma warning disable CS0162 // 检测到无法访问的代码
            if (!Config.IfDelayLoadData) InitTree(tv);
#pragma warning restore CS0162 // 检测到无法访问的代码
                              //初始化表头
            this.lvContent.BeginUpdate();
            InitListViewHeader(this.lvContent);
            this.lvContent.EndUpdate();
            //初始化列表
#pragma warning disable CS0162 // 检测到无法访问的代码
            if (!Config.IfDelayLoadData) BindLVData();
#pragma warning restore CS0162 // 检测到无法访问的代码
            this.IfFormLoadOk = true;
        }

        private void TreeFormBase_Activated(object sender, EventArgs e)
        {
            //这个一定要有累计标识响应不然会出现系统错误
            if (Config.IfDelayLoadData)
            {
                this._FormActivatedTimes++;
                if (this._FormActivatedTimes == 1)
                {
                    RefreshTreeByThread();
                    RefreshLVByThread();
                }
            }
        }

        /// <summary>
        /// 通过线程刷新树
        /// </summary>
        protected void RefreshTreeByThread()
        {
            //this.Invoke(new EventHandler(delegate
            //{
            //    if (!splitContainer1.Panel1Collapsed)
            //    {
            //        tsbExpandAll.Enabled = false;
            //        ThreadPool.QueueUserWorkItem(new WaitCallback(InitTree1), null);
            //    }
            //}));

            this.Invoke(new EventHandler(delegate
            {
                if (!splitContainer1.Panel1Collapsed)
                {
                    tsbExpandAll.Enabled = false;
                    Thread objThread = new Thread(new ThreadStart(delegate
                    {
                        InitTree1(null);
                    }));
                    objThread.Start();
                }
            }));            
        }        

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTree(object state)
        {
            this.IfFormLoadOk = false;
            Thread.Sleep(500);
            if (this.tv != null)
            {
                this.Invoke(new EventHandler(delegate
                {
                    InitTree(this.tv);
                    tsbExpandAll.Enabled = true;
                }));
            }
            this.IfFormLoadOk = true;
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTree1(object state)
        {
            Thread.Sleep(500);
            this.BeginInvoke(objInitTreeHandler);
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTree2()
        {
            this.IfFormLoadOk = false;            
            if (this.tv != null)
            {
                InitTree(this.tv);
                tsbExpandAll.Enabled = true;
            }
            this.IfFormLoadOk = true;
        }

        /// <summary>
        /// 通过线程刷新列表
        /// </summary>
        protected void RefreshLVByThread()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(InitLVData), null);
        }

        /// <summary>
        /// 初始列表
        /// </summary>
        private void InitLVData(object state)
        {
            this.IfFormLoadOk = false;
            Thread.Sleep(500);
            this.Invoke(new EventHandler(delegate
            {
                BindLVData();
            }));
            this.IfFormLoadOk = true;
        }

        /// <summary>
        /// 设置创建按钮的显示文字
        /// </summary>
        /// <param name="strText"></param>
        protected void SetCreateButtonText(string strText)
        {
            string str = string.IsNullOrEmpty(strText) ? "" : strText;
            newToolStripButton.Text = str;
            newToolStripButton.ToolTipText = str;
        }

        /// <summary>
        /// 设置编辑按钮的显示文字
        /// </summary>
        /// <param name="strText"></param>
        protected void SetEditButtonText(string strText)
        {
            string str = string.IsNullOrEmpty(strText) ? "" : strText;
            editToolStripButton.Text = str;
            editToolStripButton.ToolTipText = str;
        }

        /// <summary>
        /// 设置删除按钮的显示文字
        /// </summary>
        /// <param name="strText"></param>
        protected void SetDeleteButtonText(string strText)
        {
            string str = string.IsNullOrEmpty(strText) ? "" : strText;
            deleteToolStripButton.Text = str;
            deleteToolStripButton.ToolTipText = str;
        }

        /// <summary>
        /// 隐藏左侧树
        /// </summary>
        protected void HideTree()
        {
            splitContainer1.Panel1Collapsed = true;
        }

        /// <summary>
        /// 显示左侧树
        /// </summary>
        protected void ShowTree()
        {
            splitContainer1.Panel1Collapsed = false;
        }

        /// <summary>
        /// 隐藏创建按钮
        /// </summary>
        protected void HideCreateButton()
        {
            newToolStripButton.Visible = false;
        }

        /// <summary>
        /// 显示创建按钮
        /// </summary>
        protected void ShowCreateButton()
        {
            newToolStripButton.Visible = true;
        }

        /// <summary>
        /// 隐藏编辑按钮
        /// </summary>
        protected void HideEditButton()
        {
            editToolStripButton.Visible = false;
        }

        /// <summary>
        /// 显示编辑按钮
        /// </summary>
        protected void ShowEditButton()
        {
            editToolStripButton.Visible = true;
        }

        /// <summary>
        /// 隐藏删除按钮
        /// </summary>
        protected void HideDeleteButton()
        {
            deleteToolStripButton.Visible = false;
        }

        /// <summary>
        /// 显示删除按钮
        /// </summary>
        protected void ShowDeleteButton()
        {
            deleteToolStripButton.Visible = true;
        }

        /// <summary>
        /// 隐藏搜索按钮
        /// </summary>
        protected void HideSearchButton()
        {
            toolStripSeparator1.Visible = false;
            searchToolStripButton.Visible = false;
        }

        /// <summary>
        /// 显示搜索按钮
        /// </summary>
        protected void ShowSearchButton()
        {
            toolStripSeparator1.Visible = true;
            searchToolStripButton.Visible = true;
        }

        /// <summary>
        /// 根据条件显示数据到列表
        /// </summary>
        /// <param name="selNum">数据条数</param>
        /// <param name="sqlWhereAndOrderBy">条件</param>
        protected virtual void BindLVData(int selNum = 0, string sqlWhereAndOrderBy = null)
        {

        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="objTV"></param>
        protected virtual void InitTree(TreeView objTV)
        {

        }

        /// <summary>
        /// 当选择一个树节点时
        /// </summary>
        protected virtual void OnSelectNode(TreeNode selectedNode)
        {

        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="selectedNode">当前选择的树节点</param>
        protected virtual void OnCreate(TreeNode selectedNode)
        {

        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="selectedItem">当前选择的行项</param>
        protected virtual void OnEdit(ListViewItem selectedItem)
        {

        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="selectedItems">当前选择的所有项</param>
        protected virtual void OnDelete(ListView.SelectedListViewItemCollection selectedItems)
        {

        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        protected virtual void OnRefresh()
        {
            BindLVData(0, this.CurrentSqlWhere);
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        protected virtual void OnSearch()
        {

        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        protected virtual void OnBeforeClose()
        {

        }

        private void Tv_Click(object sender, EventArgs e)
        {
            this.tv.Tag = "Click";
        }

        private void Tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.IfFormLoadOk && !(this.tv.Tag == null || string.Compare(this.tv.Tag.ToString(), "Click") != 0))
            {
                if (!(string.Compare(this.tv.SelectedNode.Name, "Loading") == 0))
                {
                    this.tv.Tag = "AfterSelect";
                    OnSelectNode(this.tv.SelectedNode);
                }
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            if (this.IfFormLoadOk && this.CurrentUserInfo.Flag.Equals(0))
                OnCreate(this.tv.SelectedNode);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (!this.CurrentUserInfo.Flag.Equals(0)) return;
            if (this.lvContent.FocusedItem == null)
            {
                CMessageBox.ShowWaring("请选择您要修改的信息！", Config.DialogTitle);
                return;
            }
            if (this.lvContent.SelectedItems.Count > 1)
            {
                CMessageBox.ShowWaring("请选择一个您要修改的信息！", Config.DialogTitle);
                return;
            }
            if (this.IfFormLoadOk) OnEdit(this.lvContent.FocusedItem);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (!this.CurrentUserInfo.Flag.Equals(0)) return;
            if (this.lvContent.FocusedItem == null)
            {
                CMessageBox.ShowWaring("请选择您要删除的信息！", Config.DialogTitle);
                return;
            }
            OnDelete(this.lvContent.SelectedItems);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            OnRefresh();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void CloseToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要关闭窗口吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            OnBeforeClose();
            this.Close();
        }

        private void TsbExpandAll_Click(object sender, EventArgs e)
        {
            if (this.tv.Tag == null || this.tv.Tag.ToString().Equals("Expand"))
            {
                this.tv.CollapseAll();
                this.tv.Tag = "Collapse";
            }
            else
            {
                this.tv.ExpandAll();
                this.tv.Tag = "Expand";
            }
        }

        /// <summary>
        /// 初始化表头
        /// </summary>
        /// <param name="objTV"></param>
        private void InitListViewHeader(ListView objLV)
        {
            //序号,60|用户名,100|级别,200|备注,400
            if (!(string.IsNullOrEmpty(this.LVHeaderSetting) || this.LVHeaderSetting.Equals("")))
            {
                string[] arrColumn = this.LVHeaderSetting.Split('|');
                if (arrColumn.Length > 0)
                {
                    foreach (string strColumn in arrColumn)
                    {
                        string[] arrItem = strColumn.Trim().Split(',');
                        if (arrItem.Length >= 2)
                        {
                            this.lvContent.Columns.Add(arrItem[0], Convert.ToInt32(arrItem[1]));
                        }
                    }
                }
            }
        }
    }
}
