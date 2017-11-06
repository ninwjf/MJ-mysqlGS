using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.Classes;
namespace CardManage.Model
{
    /// <summary>
    /// 串口信息
    /// </summary>
    public class ComInfo
    {
        /// <summary>
        /// 串口是否开启
        /// </summary>
        public bool IfOpen { get; set; }

        /// <summary>
        /// 串口属性
        /// </summary>
        public ComProperty ComProperty { get; set; }

        /// <summary>
        /// 总发送字节数
        /// </summary>
        public Int64 TotalSendCount { get; set; }

        /// <summary>
        /// 总接收字节数
        /// </summary>
        public Int64 TotalReceivedCount { get; set; }

        /// <summary>
        /// 最后一次发送指令时间
        /// </summary>
        public DateTime LastSendTime { get; set; }

        /// <summary>
        /// 最后一次接收时间
        /// </summary>
        public DateTime LastReceiveTime { get; set; }

        /// <summary>
        /// 最后一个错误信息
        /// </summary>
        public string LastErrorInfo { get; set; }
    }
}
