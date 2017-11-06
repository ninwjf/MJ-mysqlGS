using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

using CardManage.DBUtility;
using CardManage.IDAL;
using CardManage.Model;
using CardManage.Tools;
namespace CardManage.SQLServerDAL
{
    /// <summary>
    /// 数据访问类: 用户
    /// </summary>
    public class UserInfo : IUserInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        private string _TableName = "UserInfo";

        public bool ExistsByWhere(string SqlWhere)
        {
            MySqlParameter[] parameters = {
				new MySqlParameter("@SelNum", MySqlDbType.Int32),
                new MySqlParameter("@Where_String",  MySqlDbType.VarChar)
				};
            parameters[0].Value = 1;
            parameters[1].Value = SqlWhere;
            DataSet ds = DbHelperSQL.RunProcedure(string.Format("{0}_GetBywhere_Num", this._TableName), parameters, "Ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Dispose();
                return true;
            }
            else
            {
                ds.Dispose();
                return false;
            }
        }

        public int Add(CardManage.Model.UserInfo model,out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("@ErrorInfo",  MySqlDbType.VarChar, 500),
					new MySqlParameter("@NewID", MySqlDbType.Int32),
                    new MySqlParameter("@Tag", MySqlDbType.Int32),//类型 
					new MySqlParameter("@iID", MySqlDbType.Int32),
					new MySqlParameter("@iUserName",  MySqlDbType.VarChar),
					new MySqlParameter("@iUserPwd",  MySqlDbType.VarChar),
					new MySqlParameter("@iFlag", MySqlDbType.Int32),
					new MySqlParameter("@iIfFrezen", MySqlDbType.Int32),
					new MySqlParameter("@iMemo",  MySqlDbType.VarChar)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = 0;//新增
            parameters[3].Value = 0;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.UserPwd;
            parameters[6].Value = model.Flag;
            parameters[7].Value = model.IfFrezen;
            parameters[8].Value = model.Memo;

            try
            {
                DbHelperSQL.RunProcedure(string.Format("{0}_ADD_UPDATE", this._TableName), parameters, out int rowsAffected);
                strErrorInfo = Functions.FormatString(parameters[0].Value);
                int iNewID = Functions.FormatInt(parameters[1].Value);
                return iNewID;
            }
            catch (MySqlException ex)
            {
                strErrorInfo = ex.Message;
                return 0;
            }
        }

        public bool Update(CardManage.Model.UserInfo model, out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("@ErrorInfo",  MySqlDbType.VarChar, 500),
					new MySqlParameter("@NewID", MySqlDbType.Int32),
                    new MySqlParameter("@Tag", MySqlDbType.Int32),//类型 
					new MySqlParameter("@iID", MySqlDbType.Int32),
					new MySqlParameter("@iUserName",  MySqlDbType.VarChar),
					new MySqlParameter("@iUserPwd",  MySqlDbType.VarChar),
					new MySqlParameter("@iFlag", MySqlDbType.Int32),
					new MySqlParameter("@iIfFrezen", MySqlDbType.Int32),
					new MySqlParameter("@iMemo",  MySqlDbType.VarChar)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = 1;//修改
            parameters[3].Value = model.ID;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.UserPwd;
            parameters[6].Value = model.Flag;
            parameters[7].Value = model.IfFrezen;
            parameters[8].Value = model.Memo;

            try
            {
                DbHelperSQL.RunProcedure(string.Format("{0}_ADD_UPDATE", this._TableName), parameters, out int rowsAffected);
                strErrorInfo = Functions.FormatString(parameters[0].Value);
                return (string.IsNullOrEmpty(strErrorInfo) || strErrorInfo.Equals(""));
            }
            catch (MySqlException ex)
            {
                strErrorInfo = ex.Message;
                return false;
            }
        }

        public bool Delete(int ID)
        {

            MySqlParameter[] parameters = {
                    new MySqlParameter("@iID", MySqlDbType.Int32)
                };
            parameters[0].Value = ID;

            try
            {
                DbHelperSQL.RunProcedure(string.Format("{0}_DeleteByID", this._TableName), parameters, out int rowsAffected);
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public CardManage.Model.UserInfo GetModel(int ID)
        {
            CardManage.Model.UserInfo model = null;

            MySqlParameter[] parameters = {
					new MySqlParameter("@SelNum", MySqlDbType.Int32),
                new MySqlParameter("@Where_String",  MySqlDbType.VarChar)
				};
            parameters[0].Value = 1;
            parameters[1].Value = string.Format("ID={0}", ID);

            DataSet ds = DbHelperSQL.RunProcedure(string.Format("{0}_GetBywhere_Num", this._TableName), parameters, "ds");
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                model = new Model.UserInfo()
                {
                    ID = ID
                };
                DataRow dRow = ds.Tables[0].Rows[0];
                model.UserName = Functions.FormatString(dRow["UserName"]);
                model.Flag = Functions.FormatInt(dRow["Flag"]);
                model.FlagDesc = Functions.FormatString(dRow["FlagDesc"]);
                model.UserPwd = Functions.FormatString(dRow["UserPwd"]);
                model.IfFrezen = Convert.ToBoolean(dRow["IfFrezen"]);
                model.Memo = Functions.FormatString(dRow["Memo"]);
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<CardManage.Model.UserInfo> GetListByWhere(int SelNum, string SqlWhere)
        {
            IList<CardManage.Model.UserInfo> listData = new List<CardManage.Model.UserInfo>();

            MySqlParameter[] parameters = {
					new MySqlParameter("@SelNum", MySqlDbType.Int32),
                	new MySqlParameter("@Where_String",  MySqlDbType.VarChar)
				};
            parameters[0].Value = SelNum;
            parameters[1].Value = SqlWhere;
            DataSet ds = DbHelperSQL.RunProcedure(string.Format("{0}_GetBywhere_Num", this._TableName), parameters, "ds");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dRow in ds.Tables[0].Rows)
                {
                    CardManage.Model.UserInfo model = new Model.UserInfo()
                    {
                        ID = Functions.FormatInt(dRow["ID"]),
                        UserName = Functions.FormatString(dRow["UserName"]),
                        Flag = Functions.FormatInt(dRow["Flag"]),
                        FlagDesc = Functions.FormatString(dRow["FlagDesc"]),
                        UserPwd = Functions.FormatString(dRow["UserPwd"]),
                        IfFrezen = Convert.ToBoolean(dRow["IfFrezen"]),
                        Memo = Functions.FormatString(dRow["Memo"])
                    };
                    listData.Add(model);
                }
            }
            return listData;
        }


        public int Login(string strUserName, string strPassword, out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("oID", MySqlDbType.Int32),
					new MySqlParameter("iUserName",  MySqlDbType.VarChar),
					new MySqlParameter("iUserPwd",  MySqlDbType.VarChar)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = strUserName;
            parameters[2].Value = strPassword;

            try
            {
                DbHelperSQL.RunProcedure(string.Format("{0}_Login", this._TableName), parameters, out int rowsAffected);
                int iCurrentID = Functions.FormatInt(parameters[0].Value);
                return iCurrentID;
            }
            catch (MySqlException ex)
            {
                strErrorInfo = ex.Message;
                return 0;
            }
        }

        public bool ChangePassword(int iUserID, string strOldPassword, string strNewPassword, out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("@IfSucc", SqlDbType.Bit),
					new MySqlParameter("@ErrorInfo",  MySqlDbType.VarChar, 500),
					new MySqlParameter("@UserID", MySqlDbType.Int32),
					new MySqlParameter("@OldPasword",  MySqlDbType.VarChar),
					new MySqlParameter("@NewPasword",  MySqlDbType.VarChar)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = iUserID;
            parameters[3].Value = strOldPassword;
            parameters[4].Value = strNewPassword;

            try
            {
                DbHelperSQL.RunProcedure(string.Format("{0}_ChangePassword", this._TableName), parameters, out int rowsAffected);
                bool bIfSucc = Convert.ToBoolean(parameters[0].Value);
                strErrorInfo = Functions.FormatString(parameters[1].Value);
                return bIfSucc;
            }
            catch (MySqlException ex)
            {
                strErrorInfo = ex.Message;
                return false;
            }
        }
    }
}