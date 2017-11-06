using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.Model;
namespace CardManage.IDAL
{
    /// <summary>
    /// 接口层:卡片
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool ExistsByWhere(string SqlWhere);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(CardManage.Model.Card model, out string strErrorInfo);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(CardManage.Model.Card model, out string strErrorInfo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int ID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        CardManage.Model.Card GetModel(int ID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<CardManage.Model.Card> GetListByWhere(int SelNum, string SqlWhere);
    }
}
