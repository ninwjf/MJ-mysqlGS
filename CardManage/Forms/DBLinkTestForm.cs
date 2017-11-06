using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;
using CardManage.DBUtility;

namespace CardManage.Forms
{
    public partial class DBLinkTestForm : Form
    {
        private int _CurrentDotNum = 0;
        private int _MaxDotNum = 6;

        private bool _IfOK = false;

        private string _ConnectString;
        /// <summary>
        /// 是否测试结束
        /// </summary>
        private bool _IfTestOver = false;
        public DBLinkTestForm(string strConnectString)
        {
            InitializeComponent();
            this._ConnectString = strConnectString;
        }

        private void DBLinkTestForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            ThreadPool.QueueUserWorkItem(new WaitCallback(TryToLink), null);
        }

        private void DBLinkTestForm_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //Rectangle rect = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y,
            //    e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);

            //g.DrawRectangle(Pens.Blue, rect);
        }

        private void TryToLink(object state)
        {
            try
            {
                this._IfTestOver = false;
                using (MySqlConnection connection = new MySqlConnection(this._ConnectString))
                {
                    connection.Open();
                    this._IfOK = true;
                }
            }
            catch
            {
                this._IfOK = false;
            }
            finally
            {
                this._IfTestOver = true;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this._CurrentDotNum++;
            if (this._CurrentDotNum > this._MaxDotNum) this._CurrentDotNum = 1;

            string strT = "";
            lbProgress.Text = string.Format("数据库尝试连接中{0}", strT.PadLeft(this._CurrentDotNum, '.'));

            if (this._IfTestOver)
            {
                timer1.Enabled = false;
                if (this._IfOK)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.No;
                }
            }
        }

        private void DBLinkTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
