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
    /// 数据访问类: 通讯日志
    /// </summary>
    public class CommLog : ICommLog
    {
        /// <summary>
        /// 表名
        /// </summary>
        private string _TableName = "CommLog";

        public int Add(CardManage.Model.CommLog model, out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("@RtnInfo", MySqlDbType.VarChar, 500),
					new MySqlParameter("@NewID", MySqlDbType.Int32),
                    new MySqlParameter("@Tag", MySqlDbType.Int32),//类型 
					new MySqlParameter("@ID", MySqlDbType.Int32),
					new MySqlParameter("@Flag", MySqlDbType.Int32),
					new MySqlParameter("@Content", SqlDbType.NText),
					new MySqlParameter("@CreateDate", MySqlDbType.Int32)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = 0;//新增
            parameters[3].Value = 0;
            parameters[4].Value = model.Flag;
            parameters[5].Value = model.Content;
            parameters[6].Value = model.CreateDate;

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

        public bool DeleteByWhere(string strSqlWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@Where_String", MySqlDbType.VarChar)
                };
            parameters[0].Value = strSqlWhere;

            try
            {
                DbHelperSQL.RunProcedure(string.Format("{0}_DeleteByWhere", this._TableName), parameters, out int rowsAffected);
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public CardManage.Model.CommLog GetModel(int ID)
        {
            CardManage.Model.CommLog model = null;

            MySqlParameter[] parameters = {
					new MySqlParameter("@SelNum", MySqlDbType.Int32),
                new MySqlParameter("@Where_String", MySqlDbType.VarChar)
				};
            parameters[0].Value = 1;
            parameters[1].Value = string.Format("ID={0}", ID);

            DataSet ds = DbHelperSQL.RunProcedure(string.Format("{0}_GetBywhere_Num", this._TableName), parameters, "ds");
            model.ID = ID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model = new CardManage.Model.CommLog();
                DataRow dRow = ds.Tables[0].Rows[0];
                model.Flag = Functions.FormatInt(dRow["Flag"]);
                model.FlagDesc = Functions.FormatString(dRow["FlagDesc"]);
                model.Content = Functions.FormatString(dRow["Content"]);
                model.CreateDate = Functions.FormatInt(dRow["CreateDate"]);
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<CardManage.Model.CommLog> GetListByWhere(int SelNum, string SqlWhere)
        {
            IList<CardManage.Model.CommLog> listData = new List<CardManage.Model.CommLog>();

            MySqlParameter[] parameters = {
					new MySqlParameter("@SelNum", MySqlDbType.Int32),
                	new MySqlParameter("@Where_String", MySqlDbType.VarChar)
				};
            parameters[0].Value = SelNum;
            parameters[1].Value = SqlWhere;
            DataSet ds = DbHelperSQL.RunProcedure(string.Format("{0}_GetBywhere_Num", this._TableName), parameters, "ds");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dRow in ds.Tables[0].Rows)
                {
                    CardManage.Model.CommLog model = new Model.CommLog()
                    {
                        ID = Functions.FormatInt(dRow["ID"]),
                        Flag = Functions.FormatInt(dRow["Flag"]),
                        FlagDesc = Functions.FormatString(dRow["FlagDesc"]),
                        Content = Functions.FormatString(dRow["Content"]),
                        CreateDate = Functions.FormatInt(dRow["CreateDate"])
                    };
                    listData.Add(model);
                }
            }
            return listData;
        }
    }
}