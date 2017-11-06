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
    public partial class FormBase : Form
    {
        /// <summary>
        /// 动作类型
        /// </summary>
        public enum EAction
        {
            /// <summary>
            /// 未知类型
            /// </summary>
            UnKnown,
            /// <summary>
            /// 创建
            /// </summary>
            Create,
            /// <summary>
            /// 修改数据库
            /// </summary>
            Edit
        }

        /// <summary>
        /// 父类型
        /// </summary>
        public enum EType
        {
            CARD,
            CARDLOG
        }

        /// <summary>
        /// 默认宽度
        /// </summary>
        private int _DefaultWidth = 960;
        /// <summary>
        /// 默认高度
        /// </summary>
        private int _DefaultHeight = 500;

        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        protected UserInfo CurrentUserInfo { get; set; }

        /// <summary>
        /// 窗口初始尺寸
        /// </summary>
        protected WindowSize WindowSize { get; set; }

        /// <summary>
        /// 构造关键旗标
        /// </summary>
        protected Flag Flag { get; set; }

        /// <summary>
        /// 是否Form加载完毕
        /// </summary>
        protected bool IfFormLoadOk = false;

        /// <summary>
        /// 每页条数
        /// </summary>
        protected int DefaultPageSize = 1000000;

        protected FormBase()
        {
            InitializeComponent();
        }


     

        public FormBase(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
        {
            this.CurrentUserInfo = (objUserInfo == null) ? null : (UserInfo)objUserInfo.Clone();
            this.WindowSize = objWindowSize;
            this.Flag = objFlag;

            InitializeComponent();
            this.Text = strTitle;
            this.StartPosition = FormStartPosition.CenterParent;
            if (bIsModal)
            {
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                this.Width = (objWindowSize == null) ? this._DefaultWidth : objWindowSize.Width;
                this.Height = (objWindowSize == null) ? this._DefaultHeight : objWindowSize.Height;
                this.MaximumSize = this.Size;
                this.MinimumSize = this.Size;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// 绑定数据到树
        /// </summary>
        /// <param name="objTV">树控件</param>
        /// <param name="iDeep">树的深度,0:只输出到小区；1：只输出到楼栋；2：只输出到单元；3：只输出到房间；</param>
        /// <param name="bIfShowUnGroup">是否显示分类</param>
        protected void BindBuildTreeData(TreeView objTV, int iDeep, bool bIfShowUnGroup = false)
        {
            //objTV.SelectedNode = null;
            TreeNode root = CreateTreeNode(new Model.NodeData(-1, Config.SoftName, 0));
            if (bIfShowUnGroup) root.Nodes.Add(CreateTreeNode(new NodeData(20, "未归属卡片", 0)));
            IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
            IList<Model.Building> lsAll = objDAL.GetListByWhere(-1, this.DefaultPageSize, "1=1");
            if (!(lsAll == null || lsAll.Count <= 0))
            {
                BuildTree(root.Nodes, 0, lsAll, iDeep, bIfShowUnGroup);
            }
            objTV.BeginUpdate();
            objTV.Nodes.Clear();
            objTV.Nodes.Add(root);
            objTV.EndUpdate();
        }

        /// <summary>
        /// 创建树节点
        /// </summary>
        /// <param name="Nds">父节点</param>
        /// <param name="parentID">父ID</param>
        /// <param name="listBuilding">建筑集合</param>
        /// <param name="iDeep">树的深度,0:只输出到小区；1：只输出到楼栋；2：只输出到单元；3：只输出到房间；</param>
        private void BuildTree(TreeNodeCollection Nds, int parentID, IList<Model.Building> listBuilding, int iDeep, bool bIfShowUnGroup)
        {
            //递归初始化树
            //递归寻找子节点
            IList<Model.Building> newList = listBuilding.Where(s => s.FID == parentID).ToList();
            if (!(newList == null || newList.Count <= 0))
            {
                foreach (Model.Building model in newList)
                {
                    TreeNode tmpNd = CreateTreeNode(new Model.NodeData(model.Flag, model.BName, model.ID));
                    Nds.Add(tmpNd);
                    if (bIfShowUnGroup && model.Flag.Equals(0))
                    {
                        //如果是小区，则显示管理卡
                        tmpNd.Nodes.Add(CreateTreeNode(new Model.NodeData(21, "[管理卡]", model.ID, model.BuildingSerialNo)));
                    }
                    if (iDeep > model.Flag)
                    {
                        BuildTree(tmpNd.Nodes, model.ID, listBuilding, iDeep, bIfShowUnGroup);
                    }
                }
            }
        }

        /// <summary>
        /// 创建树节点
        /// </summary>
        /// <param name="objNodeData">节点数据</param>
        /// <returns></returns>
        protected TreeNode CreateTreeNode(Model.NodeData objNodeData, int iImageIndex = -1)
        {
            TreeNode objNode = new TreeNode(objNodeData.Title)
            {
                ImageIndex = (objNodeData.Flag.Equals(-1)) ? 0 : 1
            };
            objNode.SelectedImageIndex = objNode.ImageIndex;
            objNode.Tag = objNodeData;
            return objNode;
        }

        /// <summary>
        /// 取得校验描述
        /// </summary>
        /// <param name="parity"></param>
        /// <returns></returns>
        protected string GetParityDesc(System.IO.Ports.Parity parity)
        {
            string strDesc = "未知校验";
            switch (parity)
            {
                case System.IO.Ports.Parity.None:
                    strDesc = "校验无";
                    break;
                case System.IO.Ports.Parity.Even:
                    strDesc = "偶校验";
                    break;
                case System.IO.Ports.Parity.Odd:
                    strDesc = "奇校验";
                    break;
            }
            return strDesc;
        }


        /// <summary>
        /// 根据设备类型编号获得设备类型描述
        /// </summary>
        /// <param name="iDeviceType"></param>
        /// <returns></returns>
        protected string GetDeviceTypeDesc(int iDeviceType)
        {
            string strTypeDesc = "未知设备";
            switch (iDeviceType)
            {
                case 1://管理机
                    strTypeDesc = "管理机";
                    break;
                case 2://交换机
                    strTypeDesc = "交换机";
                    break;
                case 3://切换器
                    strTypeDesc = "切换器";
                    break;
                case 4://围墙刷卡头
                    strTypeDesc = "围墙刷卡头";
                    break;
                case 5://围墙机
                    strTypeDesc = "围墙机";
                    break;
                case 6://门口机
                    strTypeDesc = "门口机";
                    break;
                case 7://二次门口机
                    strTypeDesc = "二次门口机";
                    break;
                default:
                    strTypeDesc = "未知设备";
                    break;
            }
            return strTypeDesc;
        }

        public void MaskCodeText(object sender, KeyPressEventArgs e)
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

        /// <summary>
        /// 格式化建筑编码，补齐2位
        /// </summary>
        /// <param name="objCode"></param>
        /// <returns></returns>
        protected string FormatBuildingCode(object objCode)
        {
            string strRtn = "";
            if (objCode != null)
            {
                string strCode = objCode.ToString().Trim();
                strRtn = strCode.PadLeft(2, '0');
            }
            return strRtn;
        }

        /// <summary>
        /// 格式化房间编码，补齐4位
        /// </summary>
        /// <param name="objCode"></param>
        /// <returns></returns>
        protected string FormatRoomCode(object objCode)
        {
            string strRtn = "";
            if (objCode != null)
            {
                string strCode = objCode.ToString().Trim();
                strRtn = strCode.PadLeft(4, '0');
            }
            return strRtn;
        }
        
        /// <summary>
        /// 左补齐2位
        /// </summary>
        /// <param name="objCode"></param>
        /// <returns></returns>
        protected string BQ2(object objValue)
        {
            string strRtn = "";
            if (objValue != null)
            {
                string strValue = objValue.ToString().Trim();
                strRtn = strValue.PadLeft(2, '0');
            }
            return strRtn;
        }
    }
}
