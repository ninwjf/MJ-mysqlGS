using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CardManage.Model;
using CardManage.Tools;

namespace CardManage.Forms
{
    public partial class AreaChooseForm : CardManage.Forms.SetFormBase
    {
        private Building _AreaData;

        /// <summary>
        /// 获得小区数据
        /// </summary>
        /// <returns></returns>
        public Building GetAreaData()
        {
            return this._AreaData;
        }

        protected AreaChooseForm()
        {
            InitializeComponent();
        }

        public AreaChooseForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

        private void AreaChooseForm_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            this.lvContent.BeginUpdate();
            this.lvContent.Items.Clear();
            IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
            IList<Building> listData = objDAL.GetListByWhere(0, this.DefaultPageSize, "1=1");
            if (!(listData == null || listData.Count <= 0))
            {
                foreach (Building model in listData)
                {
                    ListViewItem item = new ListViewItem(new string[] { Convert.ToString(lvContent.Items.Count + 1), model.BName, FormatBuildingCode(model.Code), model.BuildingSerialNo })
                    {
                        Tag = model,
                        Font = new Font("宋体", 9, FontStyle.Regular)

                    };
                    this.lvContent.Items.Add(item);
                }
            }
            this.lvContent.EndUpdate();
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            if (this.lvContent.FocusedItem == null)
            {
                CMessageBox.ShowWaring("请选择一个小区！", Config.DialogTitle);
                return;
            }
            this._AreaData = (Building)this.lvContent.FocusedItem.Tag;
            this.DialogResult = DialogResult.OK;
        }

        private void LvContent_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvContent.FocusedItem == null) return;
            this._AreaData = (Building)this.lvContent.FocusedItem.Tag;
            this.DialogResult = DialogResult.OK;
        }
    }
}
