using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 构造标记
    /// </summary>
    public class Flag : ICloneable
    {
        /// <summary>
        /// 参数1
        /// </summary>
        public object Keyword1 { get; set; }
        /// <summary>
        /// 参数2
        /// </summary>
        public object Keyword2 { get; set; }
        /// <summary>
        /// 参数3
        /// </summary>
        public object Keyword3 { get; set; }
        /// <summary>
        /// 参数4
        /// </summary>
        public object Keyword4 { get; set; }

        public Flag(object objKeyword1, object objKeyword2 = null, object objKeyword3 = null, object objKeyword4 = null)
        {
            Keyword1 = objKeyword1;
            Keyword2 = objKeyword2;
            Keyword3 = objKeyword3;
            Keyword4 = objKeyword4;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
