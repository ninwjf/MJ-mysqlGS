using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 监控通讯日志
    /// </summary>
    public class CommLog
    {
        /// <summary>
        /// 日志编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 数据包类型，0：上行(接收)；1：下行(发送)；
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 数据包类型描述
        /// </summary>
        public string FlagDesc { get; set; }
        /// <summary>
        /// 数据内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建时间(Unix时间戳格式)
        /// </summary>
        public int CreateDate { get; set; }
    }
}
