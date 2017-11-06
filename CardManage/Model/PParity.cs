using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 校验位
    /// </summary>
    public class PParity
    {
        /// <summary>
        /// None(无)
        /// </summary>
        public const string None = "None(无)";
        /// <summary>
        /// Odd(偶校验)
        /// </summary>
        public const string Odd = "Odd(奇校验)";
        /// <summary>
        /// Even(奇校验)
        /// </summary>
        public const string Even = "Even(偶校验)";
        /// <summary>
        /// Mark(保持1)
        /// </summary>
        public const string Mark = "Mark(保持1)";
        /// <summary>
        /// Space(保持0)
        /// </summary>
        public const string Space = "Space(保持0)";
    }
}
