using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.Classes;
namespace CardManage.Model
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 数据库配置
        /// </summary>
        public DBSetting DBSetting { get; set; }

        /// <summary>
        /// 写卡串口配置
        /// </summary>
        public ComProperty WriteComProperty { get; set; }

        /// <summary>
        /// 监控串口配置
        /// </summary>
        public ComProperty MonitorComProperty { get; set; }

        /// <summary>
        /// 其它配置
        /// </summary>
        public OtherSetting OtherSetting { get; set; }
    }
}
