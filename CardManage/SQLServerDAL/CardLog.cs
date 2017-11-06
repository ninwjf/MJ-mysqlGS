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
    /// 数据访问类: 刷卡日志
    /// </summary>
    public class CardLog : ICardLog
    {
        /// <summary>
        /// 表名
        /// </summary>
        private string _TableName = "CardLog";

        public int Add(CardManage.Model.SwipingCardData model, out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("@RtnInfo", MySqlDbType.VarChar, 500),
					new MySqlParameter("@NewID", MySqlDbType.Int32),
                    new MySqlParameter("@Tag", MySqlDbType.Int32),//类型 
					new MySqlParameter("@iID", MySqlDbType.Int32),
					new MySqlParameter("@iCardNo", MySqlDbType.UInt32),
					new MySqlParameter("@iCardType", MySqlDbType.Int32),
					new MySqlParameter("@iDeviceType", MySqlDbType.Int32),
					new MySqlParameter("@iDeviceNo", MySqlDbType.VarChar),
					new MySqlParameter("@iCreateDate", MySqlDbType.Int32)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = 0;//新增
            parameters[3].Value = 0;
            parameters[4].Value = model.CardNo;
            parameters[5].Value = model.CardType;
            parameters[6].Value = model.DeviceType;
            parameters[7].Value = model.DeviceNo;
            parameters[8].Value = Functions.ConvertToUnixTime(DateTime.Now);

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
            catch (MySqlException ex)
            {
                CMessageBox.ShowWaring(string.Format("数据库操作失败,错误如下：\r\n{0}！", ex.Message), Config.DialogTitle);
                return false;
            }
        }

        public CardManage.Model.CardLog GetModel(int ID)
        {
            CardManage.Model.CardLog model = null;

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
                model = new CardManage.Model.CardLog();
                DataRow dRow = ds.Tables[0].Rows[0];
                model.ID = ID;
                model.RAreaCode = Functions.FormatInt(dRow["RAreaCode"]);
                model.RBuildCode = Functions.FormatInt(dRow["RBuildCode"]);
                model.RUnitCode = Functions.FormatInt(dRow["RUnitCode"]);
                model.RRoomCode = Functions.FormatInt(dRow["RRoomCode"]);
                model.CardNo = Functions.FormatUInt(dRow["CardNo"]);
                model.CardType = Functions.FormatInt(dRow["CardType"]);
                model.CardTypeDesc = Functions.FormatString(dRow["CardTypeDesc"]);
                model.SerialNo = Functions.FormatString(dRow["SerialNo"]);
                model.DeviceType = Functions.FormatInt(dRow["DeviceType"]);
                model.DeviceTypeDesc = Functions.FormatString(dRow["DeviceTypeDesc"]);
                model.DeviceNo = Functions.FormatString(dRow["DeviceNo"]);
                model.CreateDate = Functions.FormatInt(dRow["CreateDate"]);
                model.Contact = Functions.FormatString(dRow["Contact"]);
                model.Tel = Functions.FormatString(dRow["Tel"]);
                model.Memo = Functions.FormatString(dRow["Memo"]);

                model.RoomID = Functions.FormatInt(dRow["RoomID"]);
                model.RoomCode = Functions.FormatInt(dRow["RoomCode"]);
                model.RoomName = Functions.FormatString(dRow["RoomName"]);
                model.UnitID = Functions.FormatInt(dRow["UnitID"]);
                model.UnitCode = Functions.FormatInt(dRow["UnitCode"]);
                model.UnitName = Functions.FormatString(dRow["UnitName"]);
                model.BuildID = Functions.FormatInt(dRow["BuildID"]);
                model.BuildCode = Functions.FormatInt(dRow["BuildCode"]);
                model.BuildName = Functions.FormatString(dRow["BuildName"]);
                model.AreaID = Functions.FormatInt(dRow["AreaID"]);
                model.AreaCode = Functions.FormatInt(dRow["AreaCode"]);
                model.AreaName = Functions.FormatString(dRow["AreaName"]);
                model.ExpiryDate = Functions.FormatInt(dRow["ExpiryDate"]);
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<CardManage.Model.CardLog> GetListByWhere(int SelNum, string SqlWhere)
        {
            IList<CardManage.Model.CardLog> listData = new List<CardManage.Model.CardLog>();

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
                    CardManage.Model.CardLog model = new Model.CardLog()
                    {
                        ID = Functions.FormatInt(dRow["ID"]),
                        RAreaCode = Functions.FormatInt(dRow["RAreaCode"]),
                        RBuildCode = Functions.FormatInt(dRow["RBuildCode"]),
                        RUnitCode = Functions.FormatInt(dRow["RUnitCode"]),
                        RRoomCode = Functions.FormatInt(dRow["RRoomCode"]),
                        CardNo = Functions.FormatUInt(dRow["CardNo"]),
                        CardType = Functions.FormatInt(dRow["CardType"]),
                        CardTypeDesc = Functions.FormatString(dRow["CardTypeDesc"]),
                        SerialNo = Functions.FormatString(dRow["SerialNo"]),
                        DeviceType = Functions.FormatInt(dRow["DeviceType"]),
                        DeviceTypeDesc = Functions.FormatString(dRow["DeviceTypeDesc"]),
                        DeviceNo = Functions.FormatString(dRow["DeviceNo"]),
                        CreateDate = Functions.FormatInt(dRow["CreateDate"]),
                        Contact = Functions.FormatString(dRow["Contact"]),
                        Tel = Functions.FormatString(dRow["Tel"]),
                        Memo = Functions.FormatString(dRow["Memo"]),

                        RoomID = Functions.FormatInt(dRow["RoomID"]),
                        RoomCode = Functions.FormatInt(dRow["RoomCode"]),
                        RoomName = Functions.FormatString(dRow["RoomName"]),
                        UnitID = Functions.FormatInt(dRow["UnitID"]),
                        UnitCode = Functions.FormatInt(dRow["UnitCode"]),
                        UnitName = Functions.FormatString(dRow["UnitName"]),
                        BuildID = Functions.FormatInt(dRow["BuildID"]),
                        BuildCode = Functions.FormatInt(dRow["BuildCode"]),
                        BuildName = Functions.FormatString(dRow["BuildName"]),
                        AreaID = Functions.FormatInt(dRow["AreaID"]),
                        AreaCode = Functions.FormatInt(dRow["AreaCode"]),
                        AreaName = Functions.FormatString(dRow["AreaName"]),
                        ExpiryDate = Functions.FormatInt(dRow["ExpiryDate"])
                    };
                    listData.Add(model);
                }
            }
            return listData;
        }
    }
}