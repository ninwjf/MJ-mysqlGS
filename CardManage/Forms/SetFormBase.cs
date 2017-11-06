using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;
using CardManage.DBUtility;

using CardManage.Model;
namespace CardManage.Forms
{
    public partial class SetFormBase : FormBase
    {
        protected SetFormBase()
        {
            InitializeComponent();
        }

        public SetFormBase(string strTitle, bool bIsModal, CardManage.Model.UserInfo objUserInfo, CardManage.Model.WindowSize objWindowSize = null, CardManage.Model.Flag objFlag = null)
            : base(strTitle, bIsModal, objUserInfo, objWindowSize, objFlag)
        {
            InitializeComponent();
            this.Text = strTitle;
            
            if (this.Text == "数据库备份")
	        {
                this.label100.Text =  "备份时请选择您要保存的位置与备份文件名";
                this.btnOK.Text = "备份";
	        }
            else if (this.Text == "数据库还原")
            {
                this.label100.Text =  "还原时请选择您要还原的数据库备份文件";
                this.btnOK.Text = "还原";
            }
            else
            {
                this.label100.Text = "";
                this.label100.Visible = false;
            }
           // this.btnOK
             //   this.btnCancel
        }

        protected virtual void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        protected virtual void BtnOK_Click(object sender, EventArgs e)
        {
            if(this.Text != "数据库备份" && this.Text != "数据库还原")
            {
                this.DialogResult = DialogResult.OK;
                return;
            }

            FileDialog fileDialog;
            if (this.Text == "数据库备份")
            {
                fileDialog = new SaveFileDialog();
            }
            else
            {
                fileDialog = new OpenFileDialog();
            }

            fileDialog.InitialDirectory = "C://";
            fileDialog.Filter = "bak files (*.bak)|*.bak|All files (*.*)|*.*";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = CardManage.DBUtility.DbHelperSQL.Query("select @@basedir as basePath from dual");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("操作失败，未找到MySql数据库目录!");
                    return;
                }

                //还原MYSQL备份文件
                DBSetting objDb = RunVariable.CurrentSetting.DBSetting;
                
                var proc = new Process();
                //通过命令行的方式进行备份,因此需要运行cmd.exe 
                proc.StartInfo.FileName = "cmd.exe";
                //禁用系统Shell启动 
                proc.StartInfo.UseShellExecute = false;
                //设置工作目录到mysqldump程序所在目录
                proc.StartInfo.WorkingDirectory = ds.Tables[0].Rows[0].ItemArray[0].ToString() + "bin";
                //mysqldump的参数可参考mysql官网的注解,这里通过format的方式 
                //得到的命令参数,每个代表什么意思,从字面上已经很好理解,不解释,^_^ 
                if (this.Text == "数据库备份")
                {
                    proc.StartInfo.Arguments = string.Format("/c mysqldump.exe --default-character-set=utf8 -h {0} -P{1} -u {2} -p{3} {4} > \"{5}\"",
                        objDb.DB_IP, "3308", objDb.DB_User, objDb.DB_Password, objDb.DB_Name, fileDialog.FileName);
                }
                else
                {
                    proc.StartInfo.Arguments = string.Format("/c mysql.exe --force -h {0} -P{1} -u {2} -p{3} {4} < \"{5}\"",
                          objDb.DB_IP, "3308", objDb.DB_User, objDb.DB_Password, objDb.DB_Name, fileDialog.FileName);
                }
                //不新建窗口,相当于隐藏界面 
                proc.StartInfo.CreateNoWindow = false;
                //以下两句主要是为了调试目的,加不加都无所谓 
                //proc.StartInfo.RedirectStandardInput = true;
                //proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                proc.WaitForExit();
                proc.Close();

                MessageBox.Show("操作成功!");
            }
        }
    }
}
