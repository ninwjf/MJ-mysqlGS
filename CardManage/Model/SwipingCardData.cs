using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 刷卡上来的数据
    /// </summary>
    public class SwipingCardData : ICloneable
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public uint CardNo { get; set; }

        /// <summary>
        /// 卡类型0：用户卡；1：巡更卡；2：管理卡；3:临时卡；4:住户键盘密码输入;5:公共键盘密码输入;6:非注册卡
        /// </summary>
        public int CardType { get; set; }

        /// <summary>
        /// 设备类型 1.管理机;2.交换机;3.切换器;4.围墙刷卡头;5.围墙机;6.门口机;7.二次门口机
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        /// 设备号码
        /// </summary>
        public string DeviceNo { get; set; }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
