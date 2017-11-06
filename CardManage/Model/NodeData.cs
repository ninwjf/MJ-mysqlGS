using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 树节点数据
    /// </summary>
    public class NodeData
    {
        /// <summary>
        /// 节点标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 类型:-1:根节点；0：区；1：栋；2：单元；3：房间；5：卡；10:用户类型；20:未归属；21:管理卡
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }

        public string SerialNo { get; set; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="iFlag">节点类型:-1:根节点；0：区；1：栋；2：单元；3：房间；5：卡；10:用户类型；20:未归属；21:管理卡</param>
        /// <param name="strTitle">节点标题</param>
        /// <param name="iID">节点值</param>
        public NodeData(int iFlag, string strTitle, int iID, string strSerialNo = "")
        {
            Flag = iFlag;
            Title = strTitle;
            ID = iID;
            SerialNo = strSerialNo;
        }
    }
}
