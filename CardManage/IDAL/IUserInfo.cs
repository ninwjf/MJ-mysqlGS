using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.Model;
namespace CardManage.IDAL
{
    /// <summary>
    /// 接口层:用户
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool ExistsByWhere(string SqlWhere);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(CardManage.Model.UserInfo model, out string strErrorInfo);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(CardManage.Model.UserInfo model, out string strErrorInfo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int ID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        CardManage.Model.UserInfo GetModel(int ID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<CardManage.Model.UserInfo> GetListByWhere(int SelNum, string SqlWhere);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="strUserName">用户名</param>
        /// <param name="strPassword">密码（加密后的）</param>
        /// <param name="strErrorInfo"></param>
        /// <returns>返回用户编号，0表示无法获取</returns>
        int Login(string strUserName, string strPassword, out string strErrorInfo);

        /// <summary>
        /// 修改密码
        /// </summary>
        bool ChangePassword(int iUserID, string strOldPassword, string strNewPassword, out string strErrorInfo);
    }
}