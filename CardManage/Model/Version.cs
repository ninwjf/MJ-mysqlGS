using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    public class Version
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiryDate { get; set; }
    }
}
