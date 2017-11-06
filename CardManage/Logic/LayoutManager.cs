using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

using CardManage.Forms;
using System.Reflection;
using CardManage.Tools;
using CardManage.Model;
namespace CardManage.Logic
{
    /// <summary>
    /// 布局类
    /// </summary>
    public class LayoutManager
    {
        private CardManage.Model.Menu _Menu;
        private CardManage.Model.Menu _ToolMenu;

        //窗口
        private MDIParent1 _MainForm;
        //控件
        private MenuStrip _MenuStrip;
        private ToolStrip _ToolStrip;
        private StatusStrip _StatusStrip;

        //静态变量
        private static object _LockObject = new object();
        private static LayoutManager _Layout;

        private SubMenuClickHandler _OnSubMenuClick = null;
        public delegate void SubMenuClickHandler(object sender, EventArgs e);
        /// <summary>
        /// 当子菜单点击时
        /// </summary>
        public event SubMenuClickHandler OnSubMenuClick
        {
            add
            {
                this._OnSubMenuClick += value;
            }
            remove
            {
                this._OnSubMenuClick -= value;
            }
        }

        public static LayoutManager GetInstance()
        {
            if (_Layout == null)
            {
                lock (_LockObject)
                {
                    if (_Layout == null)
                    {
                        _Layout = new LayoutManager();
                    }
                }
            }
            return _Layout;
        }

        private LayoutManager()
        {
            //1.系统菜单
            //系统信息
            CardManage.Model.Menu SysMenu = new CardManage.Model.Menu("System", "系统信息")
            {
                ChildList = new List<Model.Menu>
                {
                    new CardManage.Model.Menu("System_Set", "系统设置", "SystemSetForm", true, new Model.WindowSize(460, 410))
                }
            };
            if (!(RunVariable.CurrentUserInfo == null || !RunVariable.CurrentUserInfo.Flag.Equals(0))) SysMenu.ChildList.Add(new CardManage.Model.Menu("User_Manage", "账户管理", "UserManageForm", true));
            SysMenu.ChildList.Add(new CardManage.Model.Menu("User_ChangePwd", "密码修改", "ChangePasswordForm", true, new Model.WindowSize(300, 210)));
            SysMenu.ChildList.Add(new CardManage.Model.Menu("SP1", "-"));
            SysMenu.ChildList.Add(new CardManage.Model.Menu("System_Quit", "退出"));
            //基础资料
            CardManage.Model.Menu BasicMenu = new CardManage.Model.Menu("BasicData", "基础数据")
            {
                ChildList = new List<CardManage.Model.Menu>()
            };
            BasicMenu.ChildList.Add(new CardManage.Model.Menu("Area_Manage", "小区资料管理", "BuildingManageForm", true, null, 1, new Model.Flag("0")));
            BasicMenu.ChildList.Add(new CardManage.Model.Menu("Build_Manage", "楼栋资料管理", "BuildingManageForm", true, null, 1, new Model.Flag("1")));
            BasicMenu.ChildList.Add(new CardManage.Model.Menu("Unit_Manage", "单元资料管理", "BuildingManageForm", true, null, 1, new Model.Flag("2")));
            BasicMenu.ChildList.Add(new CardManage.Model.Menu("Room_Manage", "房间资料管理", "BuildingManageForm", true, null, 1, new Model.Flag("3")));
            if (!(RunVariable.CurrentUserInfo == null || !RunVariable.CurrentUserInfo.Flag.Equals(0)))
            {
                BasicMenu.ChildList.Add(new CardManage.Model.Menu("SP2", "-"));
                BasicMenu.ChildList.Add(new CardManage.Model.Menu("Buiding_BatchAdd", "房间批量增加", "BuidingBatchForm", true, new Model.WindowSize(646, 437)));
                BasicMenu.ChildList.Add(new CardManage.Model.Menu("Data_BatchDelete", "数据批量删除", "DataBatchDeleteForm", true, new Model.WindowSize(532, 463)));
            }
            BasicMenu.ChildList.Add(new CardManage.Model.Menu("SP21", "-"));
            BasicMenu.ChildList.Add(new CardManage.Model.Menu("Card_Manage", "卡片管理", "CardManageForm", false));
            //系统日志
            CardManage.Model.Menu LogMenu = new CardManage.Model.Menu("Log", "系统日志")
            {
                ChildList = new List<CardManage.Model.Menu>()
            };
            LogMenu.ChildList.Add(new CardManage.Model.Menu("CardLog_Manage", "刷卡日志", "CardLogManageForm", false));
            //LogMenu.ChildList.Add(new CardManage.Model.Menu("CommLog_Manage", "通讯日志", "CommLogManageForm", false));
            //指令监控
            CardManage.Model.Menu CommunicationMenu = new CardManage.Model.Menu("Communication", "管理工具")
            {
                ChildList = new List<CardManage.Model.Menu>()
            };
            CommunicationMenu.ChildList.Add(new CardManage.Model.Menu("Ping", "Ping(设备检测)", "PingForm", true, new WindowSize(753, 422)));
            CommunicationMenu.ChildList.Add(new CardManage.Model.Menu("SyncTime", "远程校时", "SyncTimeForm", true, new WindowSize(427, 236)));
            CommunicationMenu.ChildList.Add(new CardManage.Model.Menu("Communication", "指令监控", "CommunicationForm", false));
            CommunicationMenu.ChildList.Add(new CardManage.Model.Menu("CHSSel", "查询空白扇区", "SelChsForm", true, new WindowSize(554, 343)));
            CommunicationMenu.ChildList.Add(new CardManage.Model.Menu("SP21", "-"));
            CommunicationMenu.ChildList.Add(new CardManage.Model.Menu("DBBackup", "数据库备份", "SetFormBase", true, new WindowSize(401, 255)));
            CommunicationMenu.ChildList.Add(new CardManage.Model.Menu("DBRestore", "数据库还原", "SetFormBase", true, new WindowSize(401, 255)));
            //视图
            CardManage.Model.Menu ViewMenu = new Model.Menu("View", "视图")
            {
                ChildList = new List<CardManage.Model.Menu>()
            };
            ViewMenu.ChildList.Add(new CardManage.Model.Menu("View_Tool", "工具栏"));
            ViewMenu.ChildList.Add(new CardManage.Model.Menu("View_Status", "状态栏"));
            //窗口
            CardManage.Model.Menu WindowMenu = new CardManage.Model.Menu("Window", "窗口")
            {
                ChildList = new List<CardManage.Model.Menu>()
            };
            WindowMenu.ChildList.Add(new CardManage.Model.Menu("Window_Cascade", "层叠(&C)"));
            WindowMenu.ChildList.Add(new CardManage.Model.Menu("Window_TileVertical", "垂直平铺(&V)"));
            WindowMenu.ChildList.Add(new CardManage.Model.Menu("Window_TileHorizontal", "水平平铺(&H)"));
            WindowMenu.ChildList.Add(new CardManage.Model.Menu("Window_CloseAll", "全部关闭(&L)"));
            //WindowMenu.ChildList.Add(new CardManage.Model.Menu("Window_ArrangeIcons", "排列图标(&A)"));

            this._Menu = new CardManage.Model.Menu("root","根节点");
            _Menu.ChildList = new List<Model.Menu>
            {
                SysMenu,
                BasicMenu,
                LogMenu,
                CommunicationMenu,
                ViewMenu,
                WindowMenu
            };
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="objForm"></param>
        public void InitLayout(MDIParent1 objMainForm)
        {
            if (objMainForm == null) return;
            this._MainForm = objMainForm;

            //this._MainForm.ima
            //2.快捷菜单
            _ToolMenu = new Model.Menu("root", "根节点")
            {
                ChildList = new List<Model.Menu>()
            };
            this._ToolMenu.ChildList.Add(new CardManage.Model.Menu("Card_Manage", "卡片管理", "CardManageForm", false, null, 1, null, this._MainForm.imageList1.Images[0]));
            if (!(RunVariable.CurrentUserInfo == null || !RunVariable.CurrentUserInfo.Flag.Equals(0)))
            {
                this._ToolMenu.ChildList.Add(new CardManage.Model.Menu("Card_View", "发卡读卡", "CardViewForm", true, (RunVariable.IfDebug ? new WindowSize(801, 596) : new WindowSize(801, 371)), 0, new Flag(CardManage.Forms.FormBase.EAction.Create), this._MainForm.imageList1.Images["CreateCard"]));
            }
            this._ToolMenu.ChildList.Add(new CardManage.Model.Menu("CardLog_Manage", "刷卡日志", "CardLogManageForm", false, null, 1, null, this._MainForm.imageList1.Images[1]));
            //this._ToolMenu.ChildList.Add(new CardManage.Model.Menu("CommLog_Manage", "通讯日志", "CommLogManageForm", false, null, 1, null, this._MainForm.imageList1.Images[2]));
            this._ToolMenu.ChildList.Add(new CardManage.Model.Menu("SP3", "-"));
            this._ToolMenu.ChildList.Add(new CardManage.Model.Menu("System_Quit", "退出", "", false, null, 1, null, this._MainForm.imageList1.Images[3]));

            //创建快捷菜单
            if (this._ToolStrip == null) this._MainForm.Controls.Add(CreateToolStrip());
            //创建菜单
            if (this._MenuStrip == null) this._MainForm.Controls.Add(CreateMenu());
            //创建状态栏
            if (this._StatusStrip == null) this._MainForm.Controls.Add(CreateStatusStrip());
        }

        /// <summary>
        /// 设置状态栏文字
        /// </summary>
        /// <param name="strMessage"></param>
        public void SetStatusDesc(string strMessage)
        {
            if (!(this._StatusStrip == null || this._StatusStrip.Items.Count <= 1))
            {
                ToolStripStatusLabel objTSLabel = (ToolStripStatusLabel)this._StatusStrip.Items[1];
                objTSLabel.Text = strMessage;
            }
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        private MenuStrip CreateMenu()
        {
            //定义一个主菜单
            this._MenuStrip = new MenuStrip();
            if (!(this._Menu == null || this._Menu.ChildList == null || this._Menu.ChildList.Count <= 0))
            {
                foreach (CardManage.Model.Menu firstMenu in this._Menu.ChildList)
                {
                    //创建一个菜单项
                    ToolStripMenuItem topMenu = new ToolStripMenuItem(firstMenu.Title);
                    CreateSubMenu(ref topMenu, firstMenu.ChildList);

                    //显示应用程序中已打开的 MDI 子窗体列表的菜单项
                    this._MenuStrip.MdiWindowListItem = topMenu;
                    //将递归附加好的菜单加到菜单根项上。
                    this._MenuStrip.Items.Add(topMenu);
                }
            }
            this._MenuStrip.Dock = DockStyle.Top;
            return this._MenuStrip;
        }

        /// <summary>
        /// 创建子菜单
        /// </summary>
        /// <param name="topMenu">父菜单项</param>
        /// <param name="ItemID">父菜单的ID</param>
        /// <param name="dt">所有菜单数据集</param>
        private void CreateSubMenu(ref ToolStripMenuItem topMenu, IList<CardManage.Model.Menu> listChild)
        {
            if (!(listChild == null || listChild.Count<=0))
            {
                foreach (CardManage.Model.Menu objMenu in listChild)
                {
                    //创建子菜单项
                    if (objMenu.Title.Equals("-"))
                    {
                        ToolStripSeparator subSeparator = new ToolStripSeparator();
                        //将菜单加到顶层菜单下。
                        topMenu.DropDownItems.Add(subSeparator);
                    }
                    else
                    {
                        ToolStripMenuItem subMenu = new ToolStripMenuItem()
                        {
                            Name = objMenu.Name,
                            Text = objMenu.Title
                        };
                        if (!(objMenu.ChildList == null || objMenu.ChildList.Count <= 0))
                        {
                            CreateSubMenu(ref subMenu, objMenu.ChildList);
                        }
                        else
                        {
                            //扩展属性可以加任何想要的值。这里用formName属性来加载窗体。
                            subMenu.Tag = objMenu;
                            if (string.Compare(objMenu.Name, "View_Tool", true) == 0 || string.Compare(objMenu.Name, "View_Status", true) == 0)
                            {
                                subMenu.Checked = true;
                            }
                            //给没有子菜单的菜单项加事件。
                            subMenu.Click += new EventHandler(SubMenu_Click);
                        }
                        //if (dv[i]["ImageName"].ToString().Length > 0)
                        //{
                        //    //设置菜单项前面的图票为16X16的图片文件。
                        //    Image img = Image.FromFile(@"..\..\Image\" + dv[i]["ImageName"].ToString());
                        //    subMenu.Image = img;
                        //    subMenu.Image.Tag = dv[i]["ImageName"].ToString();
                        //}

                        //将菜单加到顶层菜单下。
                        topMenu.DropDownItems.Add(subMenu);
                    }
                }
            }
        }

        /// <summary>
        /// 创建快捷菜单
        /// </summary>
        private ToolStrip CreateToolStrip()
        {
            //定义一个主菜单
            _ToolStrip = new ToolStrip()
            {
                ImageScalingSize = new Size(33, 33)
            };
            if (!(this._ToolMenu == null || this._ToolMenu.ChildList == null || this._ToolMenu.ChildList.Count <= 0))
            {
                foreach (CardManage.Model.Menu objMenu in this._ToolMenu.ChildList)
                {
                    //创建子菜单项
                    if (objMenu.Title.Equals("-"))
                    {
                        ToolStripSeparator subSeparator = new ToolStripSeparator();
                        this._ToolStrip.Items.Add(subSeparator);
                    }
                    else
                    {
                        ToolStripButton objTSB = new ToolStripButton(objMenu.Title);
                        if (objMenu.Image != null)
                        {
                            objTSB.Image = objMenu.Image;
                            objTSB.TextImageRelation = TextImageRelation.TextBeforeImage;
                            objTSB.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                            objTSB.TextImageRelation = TextImageRelation.ImageBeforeText;
                            objTSB.ImageTransparentColor = Color.Magenta;
                            objTSB.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                        }
                        objTSB.Tag = objMenu;
                        objTSB.Click += new EventHandler(SubMenu_Click);

                        this._ToolStrip.Items.Add(objTSB);
                    }
                }
            }
            this._ToolStrip.Dock = DockStyle.Top;
            return this._ToolStrip;
        }        

        /// <summary>
        /// 创建状态栏
        /// </summary>
        private StatusStrip CreateStatusStrip()
        {
            this._StatusStrip = new StatusStrip();
            ToolStripButton objTSButton = new ToolStripButton()
            {
                Text = "==串口管理==",
                AutoToolTip = true,
                ToolTipText = "打开或关闭串口",
                ForeColor = Color.White,
                BackColor = Color.DarkBlue
            };
            objTSButton.Click += new EventHandler(StatusButton_Click);
            ToolStripStatusLabel objTSLabel = new ToolStripStatusLabel()
            {
                Text = "系统状态"
            };
            this._StatusStrip.Items.Add(objTSButton);
            this._StatusStrip.Items.Add(objTSLabel);
            this._StatusStrip.Dock = DockStyle.Bottom;
            return this._StatusStrip;
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            ComManageForm objModalForm = new ComManageForm("串口管理", true, RunVariable.CurrentUserInfo, new WindowSize(469, 222));
            if (objModalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //弹出配置串口
                SystemSetForm objModalSysForm = new SystemSetForm("系统设置", true, RunVariable.CurrentUserInfo, new WindowSize(460, 410));
                objModalSysForm.ShowDialog();
            }
        }

        /// <summary>
        /// 菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem objItem = (ToolStripItem)sender;
            ToolStripMenuItem objMenuItem;

            CardManage.Model.Menu objMenuInfo = (CardManage.Model.Menu)objItem.Tag;
            switch (objMenuInfo.Name.ToLower())
            {
                case "view_tool":
                    objMenuItem = (ToolStripMenuItem)sender;
                    objMenuItem.Checked = !objMenuItem.Checked;
                    this._ToolStrip.Visible = objMenuItem.Checked;
                    break;
                case "view_status":
                    objMenuItem = (ToolStripMenuItem)sender;
                    objMenuItem.Checked = !objMenuItem.Checked;
                    this._StatusStrip.Visible = objMenuItem.Checked;
                    break;
                case "window_cascade"://层叠
                    this._MainForm.LayoutMdi(MdiLayout.Cascade);
                    break;
                case "window_tilevertical"://垂直平铺
                    this._MainForm.LayoutMdi(MdiLayout.TileVertical);
                    break;
                case "window_tilehorizontal"://水平平铺
                    this._MainForm.LayoutMdi(MdiLayout.TileHorizontal);
                    break;
                case "window_closeall"://全部关闭
                    foreach (Form childForm in this._MainForm.MdiChildren)
                    {
                        childForm.Close();
                    }
                    break;
                case "window_arrangeicons"://排列图标
                    this._MainForm.LayoutMdi(MdiLayout.ArrangeIcons);
                    break;
                case "system_quit" ://系统退出
                    if (MessageBox.Show("确定要退出系统吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    Application.Exit();
                    break;
                default:
                    if(!(string.IsNullOrEmpty(objMenuInfo.FormName) || objMenuInfo.FormName.Equals("")))
                        OpenForm(objMenuInfo);
                    //if (this._OnSubMenuClick != null) this._OnSubMenuClick(sender, e);
                    break;
            }
        }

        private FormBase GetFormByFormName(string strFormName)
        {
            FormBase objForm = null;
            foreach (FormBase childForm in this._MainForm.MdiChildren)
            {
                if (string.Compare(childForm.Name, strFormName, true) == 0)
                {
                    objForm = childForm;
                    break;
                }
            }
            return objForm;
        }

        private void OpenForm(CardManage.Model.Menu objMenuInfo)
        {
            if (objMenuInfo == null || string.IsNullOrEmpty(objMenuInfo.FormName)) return;

            try
            {
                FormBase objForm;
                string strFormName = string.Format("{0}Form", objMenuInfo.Name);
                objForm = GetFormByFormName(strFormName);
                if (objForm != null)
                {
                    if (objForm.WindowState == FormWindowState.Minimized || objForm.WindowState == FormWindowState.Normal)
                        objForm.WindowState = FormWindowState.Maximized;
                    objForm.Activate();
                }
                else
                {
                    objForm = (FormBase)Activator.CreateInstance(Type.GetType(string.Format("CardManage.Forms.{0}", objMenuInfo.FormName)), new object[] { objMenuInfo.Title, objMenuInfo.IsModal, RunVariable.CurrentUserInfo, objMenuInfo.WindowSize, objMenuInfo.Flag });
                    objForm.Name = strFormName;
                    if (objMenuInfo.IsModal)
                    {
                        objForm.ShowDialog();
                    }
                    else
                    {
                        objForm.MdiParent = this._MainForm;
                        objForm.Show();
                    }
                }
            }
             catch (Exception err)
            {
                CMessageBox.ShowError(string.Format("出现异常，异常信息如下：{0}", err.Message), Config.DialogTitle);
            }
        }
    }
}
