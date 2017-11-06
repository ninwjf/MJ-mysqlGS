using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.Model;
namespace CardManage
{
    /// <summary>
    /// 运行时变量
    /// </summary>
    public class RunVariable
    {
        /// <summary>
        /// ini配置文件地址
        /// </summary>
        public static string IniPathAndFileName = "";

        /// <summary>
        /// 当前登录的用户
        /// </summary>
        public static UserInfo CurrentUserInfo;

        /// <summary>
        /// 系统配置
        /// </summary>
        public static Setting CurrentSetting;

        /// <summary>
        /// 当前数据库连接字符串
        /// </summary>
        public static string ConnectionString = "";

        public static bool IfDebug = false;
    }
}
