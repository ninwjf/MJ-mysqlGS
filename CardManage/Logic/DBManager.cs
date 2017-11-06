using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace CardManage.Logic
{
    /// <summary>
    /// 数据库管理器
    /// </summary>
    public class DBManager
    {
        /// <summary>
        /// 数据库IP
        /// </summary>
        private string _DBIP;
        /// <summary>
        /// 用户名
        /// </summary>
        private string _DBUserName;
        /// <summary>
        /// 密码
        /// </summary>
        private string _DBUserPassword;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="strDBIP">数据库IP</param>
        /// <param name="strUserName">用户名</param>
        /// <param name="strUserPwd">密码</param>
        public DBManager(string strDBIP,string strUserName, string strUserPwd)
        {
            this._DBIP = strDBIP;
            this._DBUserName = strUserName;
            this._DBUserPassword = strUserPwd;
        }

        /// <summary>
        /// 测试是否能连上配置的数据库服务器
        /// </summary>
        /// <returns></returns>
        public bool CheckCanLink()
        {
            bool bIfCan = false;
            MySqlConnection myCon = new MySqlConnection(string.Format("SERVER={0};Port={1};User ID={2};Password={3};Charset={4};Database=mysql;allow user variables=true", this._DBIP, "3308", this._DBUserName, this._DBUserPassword, System.Text.Encoding.Default.HeaderName.ToString()));
            try
            {
                myCon.Open();
                bIfCan = true;
            }
            catch
            {

            }
            finally
            {
                if (myCon != null && myCon.State == System.Data.ConnectionState.Open)
                {
                    myCon.Close();
                    myCon = null;
                }
            }
            return bIfCan;
        }

        /// <summary>
        /// 某数据库是否存在
        /// </summary>
        /// <param name="strDBName">数据库名</param>
        /// <returns></returns>
        public bool IsExists(string strDBName)
        {
            bool bIfExist = false;
            if (!string.IsNullOrEmpty(strDBName))
            {
                //SELECT * FROM information_schema.SCHEMATA where SCHEMA_NAME='aa';
                MySqlConnection myCon = new MySqlConnection(string.Format("SERVER={0};Port={1};User ID={2};Password={3};Charset={4};Database=mysql;allow user variables=true", this._DBIP, "3308", this._DBUserName, this._DBUserPassword, System.Text.Encoding.Default.HeaderName.ToString()));
                try
                {                    
                    myCon.Open();
                    MySqlCommand myCmd = new MySqlCommand(string.Format("SELECT * FROM sys.databases WHERE name='{0}'", strDBName), myCon);
                    object n = myCmd.ExecuteScalar();
                    bIfExist = (n != null);
                }
                catch{
                }
                finally{
                    if (myCon != null && myCon.State == System.Data.ConnectionState.Open)
                    {
                        myCon.Close();
                        myCon = null;
                    }
                }
            }
            return bIfExist;
        }

        /// <summary>
        /// 根据脚本创建数据库
        /// </summary>
        /// <param name="strDBName">数据库名</param>
        /// <param name="srSqlContent">sql脚本</param>
        /// <param name="strErrMessage">错误信息</param>
        /// <returns>是否成功</returns>
        public bool CreateBySql(string strDBName, string srSqlContent, out string strErrMessage)
        {
            bool bIfSucc = false;
            strErrMessage = "";
            if (!string.IsNullOrEmpty(strDBName) && !string.IsNullOrEmpty(srSqlContent))
            {
                MySqlConnection myConCreateDB = new MySqlConnection(string.Format("SERVER={0};Port={1};User ID={2};Password={3};Charset={4};Database=mysql;allow user variables=true", this._DBIP, "3308", this._DBUserName, this._DBUserPassword, System.Text.Encoding.Default.HeaderName.ToString(), this._DBUserName));
                MySqlConnection myConOther = new MySqlConnection(string.Format("SERVER={0};Port={1};User ID={2};Password={3};Charset={4};Database={5};allow user variables=true", this._DBIP, "3308", this._DBUserName, this._DBUserPassword, System.Text.Encoding.Default.HeaderName.ToString(), strDBName));
                try
                {
                    myConCreateDB.Open();
                    //1.创建数据库
                    //string sql = "CREATE DATABASE mydb ON PRIMARY" +"(name=test_data,filename = ‘C:\\mysql\\mydb_data.mdf’, size=3," +"maxsize=5,filegrowth=10%)log on" +"(name=mydbb_log,filename=‘C:\\mysql\\mydb_log.ldf’,size=3," +"maxsize=20,filegrowth=1)";
                    string strSql = string.Format("CREATE DATABASE {0} DEFAULT CHARACTER SET {1};", strDBName, System.Text.Encoding.Default.HeaderName.ToString());
                    MySqlCommand cmd = new MySqlCommand(strSql, myConCreateDB);
                    cmd.ExecuteNonQuery();

                    //2.创建表、视图、存储过程等
                    string[] arrSql = srSqlContent.Split('\n');
                    if (arrSql.Length > 0)
                    {
                        myConOther.Open();
                        MySqlCommand cmdSql = new MySqlCommand();
                        cmdSql.Connection = myConOther;
                        StringBuilder sb = new StringBuilder();
                        foreach (string strLine in arrSql)
                        {
                            String strLineTemp = strLine.Replace("\r", "").Trim();
                            if (!string.IsNullOrEmpty(strLineTemp))
                            {
                                if (strLineTemp.StartsWith("##GO")  && sb.Length > 0)
                                {
                                    cmdSql.CommandText = sb.ToString();
                                    cmdSql.ExecuteNonQuery();
                                    sb.Remove(0, sb.Length);
                                }
                                else if (!(strLineTemp.StartsWith("#")))
                                {
                                    sb.AppendLine(strLineTemp);
                                }
                            }
                        }
                    }
                    bIfSucc = true;
                }
                catch(Exception err)
                {
                    strErrMessage = err.Message;
                }
                finally
                {
                    if (myConCreateDB != null && myConCreateDB.State == System.Data.ConnectionState.Open)
                    {
                        myConCreateDB.Close();
                        myConCreateDB = null;
                    }
                    if (myConOther != null && myConOther.State == System.Data.ConnectionState.Open)
                    {
                        myConOther.Close();
                        myConOther = null;
                    }
                }
            }
            return bIfSucc;
        }
    }
}
