using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CardManage.Model;
namespace CardManage.Forms
{
    /// <summary>
    /// 搜索条件
    /// </summary>
    public partial class CardSearchForm : SetFormBase
    {
        private EType _CurrentType;
        private CardSearchCondition _CurrentCondition;

        private CardSearchCondition _Condition;
        /// <summary>
        /// 当前用户选择的条件
        /// </summary>
        public CardSearchCondition Condition
        {
            get { return _Condition; }
        }

        protected CardSearchForm()
        {
            InitializeComponent();
        }

        public CardSearchForm(string strTitle, bool bIsModal, UserInfo objUserInfo, WindowSize objWindowSize = null, Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;

            this._CurrentType = (EType)objFlag.Keyword1;
            if (objFlag.Keyword2 != null) this._CurrentCondition = (CardSearchCondition)objFlag.Keyword2;

            //卡类型 0：用户卡；1：巡更卡；2：管理卡；
            cbCardType.Items.Add(new ComboBoxItem("全部类型", "-1"));
            cbCardType.Items.Add(new ComboBoxItem("用户卡", 0));
            cbCardType.Items.Add(new ComboBoxItem("巡更卡", 1));
            cbCardType.Items.Add(new ComboBoxItem("管理卡", 2));
            if (this._CurrentType == EType.CARDLOG)
            {
                cbCardType.Items.Add(new ComboBoxItem("临时卡", 3));
                cbCardType.Items.Add(new ComboBoxItem("住户键盘密码输入", 4));
                cbCardType.Items.Add(new ComboBoxItem("公共键盘密码输入", 5));
                cbCardType.Items.Add(new ComboBoxItem("非注册卡", 6));
            }
            cbCardType.SelectedIndex =0;

            //卡片有效性
            cbCardValid.Items.Add(new ComboBoxItem("全部", "-1"));
            cbCardValid.Items.Add(new ComboBoxItem("未过期", 0));
            cbCardValid.Items.Add(new ComboBoxItem("已过期", 1));
            cbCardValid.SelectedIndex = 0;

            //设备类型 
            cbDeviceType.Items.Add(new ComboBoxItem("全部类型", "-1"));
            cbDeviceType.Items.Add(new ComboBoxItem("管理机", 1));
            cbDeviceType.Items.Add(new ComboBoxItem("交换机", 2));
            cbDeviceType.Items.Add(new ComboBoxItem("切换器", 3));
            cbDeviceType.Items.Add(new ComboBoxItem("围墙刷卡头", 4));
            cbDeviceType.Items.Add(new ComboBoxItem("围墙机", 5));
            cbDeviceType.Items.Add(new ComboBoxItem("门口机", 6));
            cbDeviceType.Items.Add(new ComboBoxItem("二次门口机", 7));
            cbDeviceType.SelectedIndex = 0;
            //渲染界面
            RenderLayout();
        }

        private void CardSearchForm_Load(object sender, EventArgs e)
        {
            if (this._CurrentCondition != null)
            {
                cbCardType.SelectedIndex = 0;
                cbCardValid.SelectedIndex = 0;
                cbDeviceType.SelectedIndex = 0;

                int iCardType = this._CurrentCondition.CardType;
                if (this._CurrentType == EType.CARD)
                {
                    if (iCardType >= 0 && iCardType <= 2) cbCardType.SelectedIndex = iCardType + 1;
                }
                else
                {
                    if (iCardType >= 0 && iCardType <= 6) cbCardType.SelectedIndex = iCardType + 1;
                }

                int iCardValid = this._CurrentCondition.CardValid;
                if (iCardValid >= 0 && iCardValid <= 1) cbCardValid.SelectedIndex = iCardValid + 1;

                int iDeviceType = this._CurrentCondition.DeviceType;
                if (iDeviceType >= 1 && iDeviceType <= 7) cbDeviceType.SelectedIndex = iDeviceType;

                txtCardNo.Text = this._CurrentCondition.CardNo;
                txtContact.Text = this._CurrentCondition.Contact;
                txtDeviceNo.Text = this._CurrentCondition.DeviceNo;
                txtSerialNo.Text = this._CurrentCondition.SerialNo;
            }
        }

        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            _Condition = new CardSearchCondition()
            {
                CardType = Convert.ToInt32(((ComboBoxItem)cbCardType.SelectedItem).Value),
                CardValid = Convert.ToInt32(((ComboBoxItem)cbCardValid.SelectedItem).Value),
                DeviceType = Convert.ToInt32(((ComboBoxItem)cbDeviceType.SelectedItem).Value),
                CardNo = txtCardNo.Text.Trim(),
                Contact = txtContact.Text.Trim(),
                SerialNo = txtSerialNo.Text.Trim(),
                DeviceNo = txtDeviceNo.Text.Trim()
            };
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 渲染界面
        /// </summary>
        private void RenderLayout()
        {
            bool bIfShow = this._CurrentType.Equals(EType.CARDLOG);
            lbDeviceType.Visible = bIfShow;// (this._CurrentType.ToString() == EType.CARDLOG.ToString());
            cbDeviceType.Visible = bIfShow;
            lbDeviceNo.Visible = bIfShow;
            txtDeviceNo.Visible = bIfShow;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要重置吗？", Config.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            cbCardType.SelectedIndex = 0;
            cbCardValid.SelectedIndex = 0;
            cbCardValid.SelectedIndex = 0;

            txtCardNo.Text = "";
            txtContact.Text = "";
            txtDeviceNo.Text = "";
            txtSerialNo.Text = "";
        }
    }
}