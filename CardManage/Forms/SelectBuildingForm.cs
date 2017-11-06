using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using CardManage.Model;
namespace CardManage.Forms
{
    public partial class SelectBuildingForm : SetFormBase
    {
        private BuildingCode _BuildingCode;
        /// <summary>
        /// 当前用户选择的编码
        /// </summary>
        public BuildingCode BuildingCode
        {
            get { return _BuildingCode; }
        }

        protected SelectBuildingForm()
        {
            InitializeComponent();
        }

        public SelectBuildingForm(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
        }

        private void SelectBuildingForm_Load(object sender, EventArgs e)
        {
            this.IfFormLoadOk = false;
            InitData();
            this.IfFormLoadOk = true;
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            if (cbArea.SelectedIndex == 0 && cbBuild.SelectedIndex == 0 && cbUnit.SelectedIndex == 0 && cbRoom.SelectedIndex == 0)
            {
                Tools.CMessageBox.ShowError("请至少选择一级建筑", Config.DialogTitle);
                return;
            }
            if (MessageBox.Show("确定选择该设置吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            this._BuildingCode = new BuildingCode();
            ComboBoxItem cbItem;
            //小区
            cbItem = (ComboBoxItem)cbArea.SelectedItem;
            if (Convert.ToInt32(cbItem.Value) > 0) this._BuildingCode.AreaCode = cbItem.Text.Split('|')[1];
            //楼栋
            cbItem = (ComboBoxItem)cbBuild.SelectedItem;
            if (Convert.ToInt32(cbItem.Value) > 0) this._BuildingCode.BuildCode = cbItem.Text.Split('|')[1];
            //单元
            cbItem = (ComboBoxItem)cbUnit.SelectedItem;
            if (Convert.ToInt32(cbItem.Value) > 0) this._BuildingCode.UnitCode = cbItem.Text.Split('|')[1];
            //房间
            cbItem = (ComboBoxItem)cbRoom.SelectedItem;
            if (Convert.ToInt32(cbItem.Value) > 0) this._BuildingCode.RoomCode = cbItem.Text.Split('|')[1];

            this.DialogResult = DialogResult.OK;
        }
        
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
            //小区
            cbArea.Items.Add(new Model.ComboBoxItem("请选择小区", 0));
            cbBuild.Items.Add(new Model.ComboBoxItem("请选择楼栋", 0));
            cbUnit.Items.Add(new Model.ComboBoxItem("请选择单元", 0));
            cbRoom.Items.Add(new Model.ComboBoxItem("请选择房间", 0));
            cbArea.SelectedIndex = 0;
            cbBuild.SelectedIndex = 0;
            cbUnit.SelectedIndex = 0;
            cbRoom.SelectedIndex = 0;

            IList<Model.Building> lsAll = objDAL.GetListByWhere(-1, 100000, "1=1");
            //小区
            IList<Model.Building> listArea = lsAll.Where(s => s.FID == 0).ToList();
            if (!(listArea == null || listArea.Count <= 0))
            {
                foreach (Model.Building model in listArea)
                {
                    cbArea.Items.Add(new Model.ComboBoxItem(string.Format("{0}|{1}", model.BName.Replace("|",""), FormatBuildingCode(model.Code)), model.ID));
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

                if (iCurrentID > 0)
                {
                    IDAL.IBuilding objDAL = DALFactory.DALFactory.Building();
                    IList<Model.Building> lsAll = objDAL.GetListByWhere(-1, 100000, "1=1");
                    IList<Model.Building> listBuilding = lsAll.Where(s => s.FID == iCurrentID).ToList();
                    if (string.Compare(strCbName, "cbArea", true) == 0)
                    {
                        //楼栋
                        cbBuild.Items.Clear();
                        cbBuild.Items.Add(new Model.ComboBoxItem("请选择楼栋", 0));
                        //单元
                        cbUnit.Items.Clear();
                        cbUnit.Items.Add(new Model.ComboBoxItem("请选择单元", 0));
                        cbUnit.SelectedIndex = 0;
                        //房间
                        cbRoom.Items.Clear();
                        cbRoom.Items.Add(new Model.ComboBoxItem("请选择房间", 0));
                        cbRoom.SelectedIndex = 0;

                        //初始化楼栋
                        if (!(listBuilding == null || listBuilding.Count <= 0))
                        {
                            foreach (Model.Building model in listBuilding)
                            {
                                cbBuild.Items.Add(new Model.ComboBoxItem(string.Format("{0}|{1}", model.BName.Replace("|", ""), FormatBuildingCode(model.Code)), model.ID));
                            }
                        }
                        cbBuild.SelectedIndex = 0;
                    }
                    else if (string.Compare(strCbName, "cbBuild", true) == 0)
                    {
                        //单元
                        cbUnit.Items.Clear();
                        cbUnit.Items.Add(new Model.ComboBoxItem("请选择单元", 0));
                        //房间
                        cbRoom.Items.Clear();
                        cbRoom.Items.Add(new Model.ComboBoxItem("请选择房间", 0));
                        cbRoom.SelectedIndex = 0;

                        //初始化单元
                        if (!(listBuilding == null || listBuilding.Count <= 0))
                        {
                            foreach (Model.Building model in listBuilding)
                            {
                                cbUnit.Items.Add(new Model.ComboBoxItem(string.Format("{0}|{1}", model.BName.Replace("|", ""), FormatBuildingCode(model.Code)), model.ID));
                            }
                        }
                        cbUnit.SelectedIndex = 0;
                    }
                    else if (string.Compare(strCbName, "cbUnit", true) == 0)
                    {
                        //房间
                        cbRoom.Items.Clear();
                        cbRoom.Items.Add(new Model.ComboBoxItem("请选择房间", 0));
                        cbRoom.SelectedIndex = 0;

                        //初始化房间
                        if (!(listBuilding == null || listBuilding.Count <= 0))
                        {
                            foreach (Model.Building model in listBuilding)
                            {
                                cbRoom.Items.Add(new Model.ComboBoxItem(string.Format("{0}|{1}", model.BName.Replace("|", ""), FormatRoomCode(model.Code)), model.ID));
                            }
                        }
                        cbRoom.SelectedIndex = 0;
                    }
                }                
            }
        }
    }
}
