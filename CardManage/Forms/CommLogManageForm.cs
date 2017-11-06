using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CardManage.Model;
namespace CardManage.Forms
{
    public partial class CommLogManageForm : TreeFormBase
    {        
        protected CommLogManageForm()
        {
            InitializeComponent();
        }

        public CommLogManageForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;

            this.LVHeaderSetting = "序号,50|创建时间,100|数据包类型,100|数据内容,400";
        }

        private void CommLogManageForm_Load(object sender, EventArgs e)
        {

        }

        private void CommLogManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="objTV"></param>
        protected override void InitTree(TreeView objTV)
        {
            TreeNode rootNode = this.CreateTreeNode(new NodeData(-1, "全部类型", -1));
            rootNode.Nodes.Add(this.CreateTreeNode(new NodeData(10, "上行(接收)", 0)));
            rootNode.Nodes.Add(this.CreateTreeNode(new NodeData(10, "下行(发送)", 0)));

            objTV.Nodes.Add(rootNode);
            objTV.ExpandAll();
        }
    }
}
