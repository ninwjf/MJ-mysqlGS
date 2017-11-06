using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.IDAL
{
    /// <summary>
    /// 系统管理接口
    /// </summary>
    public interface ISys
    {
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <param name="iFlag">删除的数据类型</param>
        /// <returns></returns>
        bool DeleteData(int Flag);
    }
}
