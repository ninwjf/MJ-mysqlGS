using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 其它配置
    /// </summary>
    public class OtherSetting
    {
        /// <summary>
        /// 时间同步频率，单位为分
        /// </summary>
        public int SyncTimePL;
        /// <summary>
        /// 时间同步状态
        /// 1 开启
        /// 0 关闭
        /// </summary>
        public int SyncAuto;
        /// <summary>
        /// 默认扇区
        /// </summary>
        public int Chs;
    }
}
