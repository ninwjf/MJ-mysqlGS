using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DBSetting
    {
        /// <summary>
        /// 数据库IP
        /// </summary>
        public string DB_IP { get; set; }
        
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DB_Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string DB_User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string DB_Password { get; set; }
    }
}
