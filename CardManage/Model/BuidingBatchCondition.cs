using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 批生成工具指令
    /// </summary>
    public class BuidingBatchCondition : ICloneable
    {
        /// <summary>
        /// 单元编号
        /// </summary>
        public int UnitID { get; set; }

        /// <summary>
        /// 起始房间编码
        /// </summary>
        public int BeginCode { get; set; }

        /// <summary>
        /// 结束房间编码
        /// </summary>
        public int EndCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUnitID">单元编号</param>
        /// <param name="iBeginCode">起始房间编码</param>
        /// <param name="iEndCode">结束房间编码</param>
        public BuidingBatchCondition(int iUnitID, int iBeginCode, int iEndCode)
        {
            UnitID = iUnitID;
            BeginCode = iBeginCode;
            EndCode = iEndCode;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
