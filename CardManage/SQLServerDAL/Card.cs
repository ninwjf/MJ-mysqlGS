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
    /// 数据访问类: 卡片
    /// </summary>
    public class Card : ICard
    {
        /// <summary>
        /// 表名
        /// </summary>
        private string _TableName = "Card";

        public bool ExistsByWhere(string SqlWhere)
        {
            MySqlParameter[] parameters = {
				new MySqlParameter("@SelNum", MySqlDbType.Int32),
                new MySqlParameter("@Where_String", MySqlDbType.VarChar)
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

        public int Add(CardManage.Model.Card model, out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("@RtnInfo", MySqlDbType.VarChar, 500),
					new MySqlParameter("@NewID", MySqlDbType.Int32),
                    new MySqlParameter("@iTag", MySqlDbType.Int32),//类型 
					new MySqlParameter("@iID", MySqlDbType.Int32),
					new MySqlParameter("@iRAreaCode", MySqlDbType.Int32),
					new MySqlParameter("@iRBuildCode", MySqlDbType.Int32),
					new MySqlParameter("@iRUnitCode", MySqlDbType.Int32),
					new MySqlParameter("@iRRoomCode", MySqlDbType.Int32),
					new MySqlParameter("@iCardNo", MySqlDbType.UInt32),
					new MySqlParameter("@iCardType", MySqlDbType.Int32),
					new MySqlParameter("@iSerialNo", MySqlDbType.VarChar),
					new MySqlParameter("@iExpiryDate", MySqlDbType.Int64),
					new MySqlParameter("@iIfFrezen", MySqlDbType.Bit),
					new MySqlParameter("@iCreateDate", MySqlDbType.Int64),
					new MySqlParameter("@iContact", MySqlDbType.VarChar),
					new MySqlParameter("@iTel", MySqlDbType.VarChar),
					new MySqlParameter("@iMemo", MySqlDbType.VarChar)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = 0;//新增
            parameters[3].Value = 0;
            parameters[4].Value = model.RAreaCode;
            parameters[5].Value = model.RBuildCode;
            parameters[6].Value = model.RUnitCode;
            parameters[7].Value = model.RRoomCode;
            parameters[8].Value = model.CardNo;
            parameters[9].Value = model.CardType;
            parameters[10].Value = model.SerialNo;
            parameters[11].Value = model.ExpiryDate;
            parameters[12].Value = model.IfFrezen;
            parameters[13].Value = model.CreateDate;
            parameters[14].Value = model.Contact;
            parameters[15].Value = model.Tel;
            parameters[16].Value = model.Memo;

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

        public bool Update(CardManage.Model.Card model, out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("@RtnInfo", MySqlDbType.VarChar, 500),
					new MySqlParameter("@NewID", MySqlDbType.Int32),
                    new MySqlParameter("@iTag", MySqlDbType.Int32),//类型 
					new MySqlParameter("@iID", MySqlDbType.Int32),
					new MySqlParameter("@iRAreaCode", MySqlDbType.Int32),
					new MySqlParameter("@iRBuildCode", MySqlDbType.Int32),
					new MySqlParameter("@iRUnitCode", MySqlDbType.Int32),
					new MySqlParameter("@iRRoomCode", MySqlDbType.Int32),
					new MySqlParameter("@iCardNo", MySqlDbType.UInt32),
					new MySqlParameter("@iCardType", MySqlDbType.Int32),
					new MySqlParameter("@iSerialNo", MySqlDbType.VarChar),
					new MySqlParameter("@iExpiryDate", MySqlDbType.Int64),
					new MySqlParameter("@iIfFrezen", MySqlDbType.Bit),
					new MySqlParameter("@iCreateDate", MySqlDbType.Int64),
					new MySqlParameter("@iContact", MySqlDbType.VarChar),
					new MySqlParameter("@iTel", MySqlDbType.VarChar),
					new MySqlParameter("@iMemo", MySqlDbType.VarChar)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = 1;//修改
            parameters[3].Value = model.ID;
            parameters[4].Value = model.RAreaCode;
            parameters[5].Value = model.RBuildCode;
            parameters[6].Value = model.RUnitCode;
            parameters[7].Value = model.RRoomCode;
            parameters[8].Value = model.CardNo;
            parameters[9].Value = model.CardType;


            parameters[10].Value = model.SerialNo;
            parameters[11].Value = model.ExpiryDate;
            parameters[12].Value = model.IfFrezen;
            parameters[13].Value = model.CreateDate;
            parameters[14].Value = model.Contact;
            parameters[15].Value = model.Tel;
            parameters[16].Value = model.Memo;

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
            catch (MySqlException ex)
            {
                CMessageBox.ShowWaring(string.Format("数据库操作失败,错误如下：\r\n{0}！", ex.Message), Config.DialogTitle);
                return false;
            }
        }

        public CardManage.Model.Card GetModel(int ID)
        {
            CardManage.Model.Card model = null;

            MySqlParameter[] parameters = {
					new MySqlParameter("@SelNum", MySqlDbType.Int32),
                new MySqlParameter("@Where_String", MySqlDbType.VarChar)
				};
            parameters[0].Value = 1;
            parameters[1].Value = string.Format("ID={0}", ID);

            DataSet ds = DbHelperSQL.RunProcedure(string.Format("{0}_GetBywhere_Num", this._TableName), parameters, "ds");            
            if (ds.Tables[0].Rows.Count > 0)
            {
                model = new CardManage.Model.Card();
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
                model.ExpiryDate = Functions.FormatInt64(dRow["ExpiryDate"]);
                model.IfFrezen = Convert.ToBoolean(dRow["IfFrezen"]);
                model.CreateDate = Functions.FormatInt64(dRow["CreateDate"]);
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
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<CardManage.Model.Card> GetListByWhere(int SelNum, string SqlWhere)
        {
            IList<CardManage.Model.Card> listData = new List<CardManage.Model.Card>();

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
                    Model.Card model = new Model.Card()
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
                        ExpiryDate = Functions.FormatInt64(dRow["ExpiryDate"]),
                        IfFrezen = Convert.ToBoolean(dRow["IfFrezen"]),
                        CreateDate = Functions.FormatInt64(dRow["CreateDate"]),
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
                        AreaName = Functions.FormatString(dRow["AreaName"])
                    };
                    listData.Add(model);
                }
            }
            return listData;
        }
    }
}