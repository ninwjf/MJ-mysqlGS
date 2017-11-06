using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.Model;
namespace CardManage.IDAL
{
    /// <summary>
    /// 接口层:通讯日志
    /// </summary>
    public interface ICommLog
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(CardManage.Model.CommLog model, out string strErrorInfo);
        /// <summary>
        /// 删除数据
        /// </summary>
        bool DeleteByWhere(string strSqlWhere);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        CardManage.Model.CommLog GetModel(int ID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<CardManage.Model.CommLog> GetListByWhere(int SelNum, string SqlWhere);
    }
}
