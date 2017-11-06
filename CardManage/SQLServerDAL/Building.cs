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
    /// 数据访问类: 建筑
    /// </summary>
    public class Building : IBuilding
    {
        /// <summary>
        /// 表名
        /// </summary>
        private string _TableName = "Building";

        public bool ExistsByWhere(int Flag, string SqlWhere)
        {
            MySqlParameter[] parameters = {                
				new MySqlParameter("@Flag", MySqlDbType.Int32),
				new MySqlParameter("@SelNum", MySqlDbType.Int32),
                new MySqlParameter("@Where_String", MySqlDbType.VarChar)
				};
            parameters[0].Value = Flag;
            parameters[1].Value = 1;
            parameters[2].Value = SqlWhere;
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

        public int Add(CardManage.Model.Building model, out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("@RtnInfo", MySqlDbType.VarChar, 500),
					new MySqlParameter("@NewID", MySqlDbType.Int32),
                    new MySqlParameter("@Tag", MySqlDbType.Int32),//类型 
					new MySqlParameter("@iID", MySqlDbType.Int32),
					new MySqlParameter("@iBname", MySqlDbType.VarChar),
					new MySqlParameter("@iFlag", MySqlDbType.Int32),
					new MySqlParameter("@iCode", MySqlDbType.Int32),
					new MySqlParameter("@iFID", MySqlDbType.Int32),
					new MySqlParameter("@iContact", MySqlDbType.VarChar),
					new MySqlParameter("@iTel", MySqlDbType.VarChar),
					new MySqlParameter("@iBuildingSerialNo", MySqlDbType.VarChar)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = 0;//新增
            parameters[3].Value = 0;
            parameters[4].Value = model.BName;
            parameters[5].Value = model.Flag;
            parameters[6].Value = model.Code;
            parameters[7].Value = model.FID;
            parameters[8].Value = model.Contact;
            parameters[9].Value = model.Tel;
            parameters[10].Value = model.BuildingSerialNo;

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

        public bool Update(CardManage.Model.Building model, out string strErrorInfo)
        {
            strErrorInfo = "";
            MySqlParameter[] parameters = {
					new MySqlParameter("@RtnInfo", MySqlDbType.VarChar, 500),
					new MySqlParameter("@NewID", MySqlDbType.Int32),
                    new MySqlParameter("@Tag", MySqlDbType.Int32),//类型 
					new MySqlParameter("@iID", MySqlDbType.Int32),
					new MySqlParameter("@iBname", MySqlDbType.VarChar),
					new MySqlParameter("@iFlag", MySqlDbType.Int32),
					new MySqlParameter("@iCode", MySqlDbType.Int32),
					new MySqlParameter("@iFID", MySqlDbType.Int32),
					new MySqlParameter("@iContact", MySqlDbType.VarChar),
					new MySqlParameter("@iTel", MySqlDbType.VarChar),
					new MySqlParameter("@iBuildingSerialNo", MySqlDbType.VarChar)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = 1;//修改
            parameters[3].Value = model.ID;
            parameters[4].Value = model.BName;
            parameters[5].Value = model.Flag;
            parameters[6].Value = model.Code;
            parameters[7].Value = model.FID;
            parameters[8].Value = model.Contact;
            parameters[9].Value = model.Tel;
            parameters[10].Value = model.BuildingSerialNo;

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

        public CardManage.Model.Building GetModel(int ID)
        {
            CardManage.Model.Building model = null;
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)
				};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.RunProcedure(string.Format("{0}_GetByID", this._TableName), parameters, "ds");

            if (ds.Tables[0].Rows.Count > 0)
            {
                model = new CardManage.Model.Building();
                DataRow dRow = ds.Tables[0].Rows[0];
                model.ID = Functions.FormatInt(dRow["ID"]); ;
                model.BName = Functions.FormatString(dRow["BName"]);
                model.Flag = Functions.FormatInt(dRow["Flag"]);
                model.FlagDesc = Functions.FormatString(dRow["FlagDesc"]);
                model.Code = Functions.FormatInt(dRow["Code"]);
                model.Contact = Functions.FormatString(dRow["Contact"]);
                model.Tel = Functions.FormatString(dRow["Tel"]);
                model.BuildingSerialNo = Functions.FormatString(dRow["BuildingSerialNo"]);
                model.FID = Functions.FormatInt(dRow["FID"]);

                switch (model.Flag)
                {
                    case 1://楼栋->父：小区
                        model.FatherInfo = new Model.Building()
                        {
                            ID = model.FID,
                            Flag = model.Flag - 1,
                            BName = Functions.FormatString(dRow["AreaName"]),
                            Code = Functions.FormatInt(dRow["AreaCode"]),
                            FID = 0
                        };
                        break;
                    case 2://单元->父：楼栋
                        model.FatherInfo = new Model.Building()
                        {
                            ID = model.FID,
                            Flag = model.Flag - 1,
                            BName = Functions.FormatString(dRow["BuildName"]),
                            Code = Functions.FormatInt(dRow["BuildCode"]),
                            FID = Functions.FormatInt(dRow["AreaID"])
                        };

                        //楼栋->父：小区
                        model.FatherInfo.FatherInfo = new Model.Building()
                        {
                            ID = model.FatherInfo.FID,
                            Flag = model.FatherInfo.Flag - 1,
                            BName = Functions.FormatString(dRow["AreaName"]),
                            Code = Functions.FormatInt(dRow["AreaCode"]),
                            FID = 0
                        };
                        break;
                    case 3://房间->父：单元
                        model.FatherInfo = new Model.Building()
                        {
                            ID = model.FID,
                            Flag = model.Flag - 1,
                            BName = Functions.FormatString(dRow["UnitName"]),
                            Code = Functions.FormatInt(dRow["UnitCode"]),
                            FID = Functions.FormatInt(dRow["BuildID"]),

                            //单元->父：楼栋
                            FatherInfo = new Model.Building()
                        };
                        model.FatherInfo.FatherInfo.ID = model.FatherInfo.FID;
                        model.FatherInfo.FatherInfo.Flag = model.FatherInfo.Flag - 1;
                        model.FatherInfo.FatherInfo.BName = Functions.FormatString(dRow["BuildName"]);
                        model.FatherInfo.FatherInfo.Code = Functions.FormatInt(dRow["BuildCode"]);
                        model.FatherInfo.FatherInfo.FID = Functions.FormatInt(dRow["AreaID"]);

                        //楼栋->父：小区
                        model.FatherInfo.FatherInfo.FatherInfo = new Model.Building()
                        {
                            ID = model.FatherInfo.FatherInfo.FID,
                            Flag = model.FatherInfo.FatherInfo.Flag - 1,
                            BName = Functions.FormatString(dRow["AreaName"]),
                            Code = Functions.FormatInt(dRow["AreaCode"]),
                            FID = 0
                        };
                        break;
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public CardManage.Model.Building GetModel(int Flag, int ID)
        {
            CardManage.Model.Building model = null;
            MySqlParameter[] parameters = {
					new MySqlParameter("@Flag", MySqlDbType.Int32),
					new MySqlParameter("@SelNum", MySqlDbType.Int32),
                	new MySqlParameter("@Where_String", MySqlDbType.VarChar)
				};
            parameters[0].Value = Flag;
            parameters[1].Value = 1;
            parameters[2].Value = string.Format("ID={0}", ID);

            DataSet ds = DbHelperSQL.RunProcedure(string.Format("{0}_GetBywhere_Num", this._TableName), parameters, "ds");
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                model = new CardManage.Model.Building();
                DataRow dRow = ds.Tables[0].Rows[0];
                model.ID = Functions.FormatInt(dRow["ID"]); ;
                model.BName = Functions.FormatString(dRow["BName"]);
                model.Flag = Functions.FormatInt(dRow["Flag"]);
                model.FlagDesc = Functions.FormatString(dRow["FlagDesc"]);
                model.Code = Functions.FormatInt(dRow["Code"]);
                model.Contact = Functions.FormatString(dRow["Contact"]);
                model.Tel = Functions.FormatString(dRow["Tel"]);
                model.BuildingSerialNo = Functions.FormatString(dRow["BuildingSerialNo"]);
                model.FID = Functions.FormatInt(dRow["FID"]);

                switch (Flag)
                {
                    case 1://楼栋->父：小区
                        model.FatherInfo = new Model.Building()
                        {
                            ID = model.FID,
                            Flag = model.Flag - 1,
                            BName = Functions.FormatString(dRow["AreaName"]),
                            Code = Functions.FormatInt(dRow["AreaCode"]),
                            FID = 0
                        };
                        break;
                    case 2://单元->父：楼栋
                        model.FatherInfo = new Model.Building()
                        {
                            ID = model.FID,
                            Flag = model.Flag - 1,
                            BName = Functions.FormatString(dRow["BuildName"]),
                            Code = Functions.FormatInt(dRow["BuildCode"]),
                            FID = Functions.FormatInt(dRow["AreaID"]),

                            //楼栋->父：小区
                            FatherInfo = new Model.Building()
                        };
                        model.FatherInfo.FatherInfo.ID = model.FatherInfo.FID;
                        model.FatherInfo.FatherInfo.Flag = model.FatherInfo.Flag - 1;
                        model.FatherInfo.FatherInfo.BName = Functions.FormatString(dRow["AreaName"]);
                        model.FatherInfo.FatherInfo.Code = Functions.FormatInt(dRow["AreaCode"]);
                        model.FatherInfo.FatherInfo.FID = 0;
                        break;
                    case 3://房间->父：单元
                        model.FatherInfo = new Model.Building()
                        {
                            ID = model.FID,
                            Flag = model.Flag - 1,
                            BName = Functions.FormatString(dRow["UnitName"]),
                            Code = Functions.FormatInt(dRow["UnitCode"]),
                            FID = Functions.FormatInt(dRow["BuildID"])
                        };

                        //单元->父：楼栋
                        model.FatherInfo.FatherInfo = new Model.Building()
                        {
                            ID = model.FatherInfo.FID,
                            Flag = model.FatherInfo.Flag - 1,
                            BName = Functions.FormatString(dRow["BuildName"]),
                            Code = Functions.FormatInt(dRow["BuildCode"]),
                            FID = Functions.FormatInt(dRow["AreaID"]),

                            //楼栋->父：小区
                            FatherInfo = new Model.Building()
                        };
                        model.FatherInfo.FatherInfo.FatherInfo.ID = model.FatherInfo.FatherInfo.FID;
                        model.FatherInfo.FatherInfo.FatherInfo.Flag = model.FatherInfo.FatherInfo.Flag - 1;
                        model.FatherInfo.FatherInfo.FatherInfo.BName = Functions.FormatString(dRow["AreaName"]);
                        model.FatherInfo.FatherInfo.FatherInfo.Code = Functions.FormatInt(dRow["AreaCode"]);
                        model.FatherInfo.FatherInfo.FatherInfo.FID = 0;
                        break;
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<CardManage.Model.Building> GetListByWhere(int Flag, int SelNum, string SqlWhere)
        {
            IList<CardManage.Model.Building> listData = new List<CardManage.Model.Building>();

            MySqlParameter[] parameters = {
					new MySqlParameter("@Flag", MySqlDbType.Int32),
					new MySqlParameter("@SelNum", MySqlDbType.Int32),
                	new MySqlParameter("@Where_String", MySqlDbType.VarChar)
				};
            parameters[0].Value = Flag;
            parameters[1].Value = SelNum;
            parameters[2].Value = SqlWhere;
            DataSet ds = DbHelperSQL.RunProcedure(string.Format("{0}_GetBywhere_Num", this._TableName), parameters, "ds");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dRow in ds.Tables[0].Rows)
                {
                    CardManage.Model.Building model = new Model.Building()
                    {
                        ID = Functions.FormatInt(dRow["ID"]),
                        BName = Functions.FormatString(dRow["BName"]),
                        Flag = Functions.FormatInt(dRow["Flag"]),
                        FlagDesc = Functions.FormatString(dRow["FlagDesc"]),
                        Code = Functions.FormatInt(dRow["Code"]),
                        Contact = Functions.FormatString(dRow["Contact"]),
                        Tel = Functions.FormatString(dRow["Tel"]),
                        BuildingSerialNo = Functions.FormatString(dRow["BuildingSerialNo"]),
                        FID = Functions.FormatInt(dRow["FID"])
                    };
                    switch (Flag)
                    {
                        case 1://楼栋->父：小区
                            model.FatherInfo = new Model.Building()
                            {
                                ID = model.FID,
                                Flag = model.Flag - 1,
                                BName = Functions.FormatString(dRow["AreaName"]),
                                Code = Functions.FormatInt(dRow["AreaCode"]),
                                FID = 0
                            };
                            break;
                        case 2://单元->父：楼栋
                            model.FatherInfo = new Model.Building()
                            {
                                ID = model.FID,
                                Flag = model.Flag - 1,
                                BName = Functions.FormatString(dRow["BuildName"]),
                                Code = Functions.FormatInt(dRow["BuildCode"]),
                                FID = Functions.FormatInt(dRow["AreaID"])
                            };

                            //楼栋->父：小区
                            model.FatherInfo.FatherInfo = new Model.Building()
                            {
                                ID = model.FatherInfo.FID,
                                Flag = model.FatherInfo.Flag - 1,
                                BName = Functions.FormatString(dRow["AreaName"]),
                                Code = Functions.FormatInt(dRow["AreaCode"]),
                                FID = 0
                            };
                            break;
                        case 3://房间->父：单元
                            model.FatherInfo = new Model.Building()
                            {
                                ID = model.FID,
                                Flag = model.Flag - 1,
                                BName = Functions.FormatString(dRow["UnitName"]),
                                Code = Functions.FormatInt(dRow["UnitCode"]),
                                FID = Functions.FormatInt(dRow["BuildID"]),

                                //单元->父：楼栋
                                FatherInfo = new Model.Building()
                            };
                            model.FatherInfo.FatherInfo.ID = model.FatherInfo.FID;
                            model.FatherInfo.FatherInfo.Flag = model.FatherInfo.Flag - 1;
                            model.FatherInfo.FatherInfo.BName = Functions.FormatString(dRow["BuildName"]);
                            model.FatherInfo.FatherInfo.Code = Functions.FormatInt(dRow["BuildCode"]);
                            model.FatherInfo.FatherInfo.FID = Functions.FormatInt(dRow["AreaID"]);

                            //楼栋->父：小区
                            model.FatherInfo.FatherInfo.FatherInfo = new Model.Building()
                            {
                                ID = model.FatherInfo.FatherInfo.FID,
                                Flag = model.FatherInfo.FatherInfo.Flag - 1,
                                BName = Functions.FormatString(dRow["AreaName"]),
                                Code = Functions.FormatInt(dRow["AreaCode"]),
                                FID = 0
                            };
                            break;
                    }

                    listData.Add(model);
                }
            }
            return listData;
        }
    }
}
