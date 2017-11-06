using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 卡片模块相关搜索条件
    /// </summary>
    public class CardSearchCondition : ICloneable
    {
        /// <summary>
        /// 卡片类型,-1;:全部类型,0：用户卡；1：巡更卡；2：管理卡；
        /// </summary>
        public int CardType { get; set; }
        /// <summary>
        /// 卡片有效性,-1;:全部类型,0：未过期；1：已过期；
        /// </summary>
        public int CardValid { get; set; }
        /// <summary>
        /// 设备类型,-1;:全部类型,1.管理机;2.交换机;3.切换器;4.围墙刷卡头;5.围墙机;6.门口机;7.二次门口机
        /// </summary>
        public int DeviceType { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceNo { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 系列号
        /// </summary>
        public string SerialNo { get; set; }
        /// <summary>
        /// 持卡者姓名
        /// </summary>
        public string Contact { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
