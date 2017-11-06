using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    public class DeviceAddress : ICloneable
    {
        /// <summary>
        /// 数据包
        /// </summary>
        public byte[] Buffer { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int PingNum { get; set; }

        /// <summary>
        /// 设备类型 1.管理机;2.交换机;3.切换器;4.围墙刷卡头;5.围墙机;6.门口机;7.二次门口机
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        /// 源设备号码，未补齐的
        /// </summary>
        public string OriginalDeviceNo { get; set; }

        /// <summary>
        /// 设备号码，8位补齐后的
        /// </summary>
        public string DeviceNo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
