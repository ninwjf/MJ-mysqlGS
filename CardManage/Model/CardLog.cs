using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 刷卡日志
    /// </summary>
    public class CardLog : Card
    {
        /// <summary>
        /// 设备类型1.管理机;2.交换机;3.切换器;4.围墙刷卡头;5.围墙机;6.门口机;7.二次门口机
        /// </summary>
        public int DeviceType { get; set; }
        /// <summary>
        /// 设备类型描述
        /// </summary>
        public string DeviceTypeDesc { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceNo { get; set; }
    }
}
