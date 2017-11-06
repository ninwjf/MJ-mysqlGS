using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo : ICloneable
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 用户类型(0:超级管理员；1：普通管理员)
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 用户类型描述
        /// </summary>
        public string FlagDesc { get; set; }
        /// <summary>
        /// 是否被冻结
        /// </summary>
        public bool IfFrezen { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
