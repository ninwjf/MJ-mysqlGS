using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

using CardManage.IDAL;
using System.Data;
using CardManage.DBUtility;
namespace CardManage.SQLServerDAL
{
    /// <summary>
    /// 系统管理接口
    /// </summary>
    public class Sys : ISys
    {
        public bool DeleteData(int Flag)
        {
            MySqlParameter[] parameters = {
					new MySqlParameter("@Flag", SqlDbType.Int)
				};
            parameters[0].Value = Flag;

            try
            {
                DbHelperSQL.RunProcedure(string.Format("DeleteData"), parameters, out int rowsAffected);
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
    }
}
