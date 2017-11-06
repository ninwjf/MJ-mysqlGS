using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 窗体尺寸
    /// </summary>
    public class WindowSize : ICloneable
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="iWidth">宽度</param>
        /// <param name="iHeight">高度</param>
        public WindowSize(int iWidth, int iHeight)
        {
            Width = iWidth;
            Height = iHeight;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
