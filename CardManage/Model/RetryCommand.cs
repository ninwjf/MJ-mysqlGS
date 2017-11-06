using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 需重试发的指令
    /// </summary>
    public class RetryCommand :ICloneable
    {
        /// <summary>
        /// 16进制指令字符串
        /// </summary>
        public string CommandHexStr { get; set; }

        /// <summary>
        /// 已重试次数
        /// </summary>
        public int TryTimes { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="strCommandHexStr">16进制指令字符串</param>
        /// <param name="iTryTimes">已重试次数</param>
        public RetryCommand(string strCommandHexStr, int iTryTimes)
        {
            this.CommandHexStr = strCommandHexStr;
            this.TryTimes = iTryTimes;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
