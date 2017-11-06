using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 建筑信息
    /// </summary>
    public class Building
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string BName { get; set; }
        /// <summary>
        /// 建筑类型(0：区；1：栋；2：单元；3：房间；)
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 建筑类型描述
        /// </summary>
        public string FlagDesc { get; set; }
        /// <summary>
        /// 编码(范围：房间:0000~9999,其它：0x00~0x99)
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 父编号
        /// </summary>
        public int FID { get; set; }
        /// <summary>
        /// 父对象
        /// </summary>
        public Building FatherInfo { get; set; }
        /// <summary>
        /// 管理者/拥有者姓名
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 管理者/拥有者联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 建筑序列号
        /// </summary>
        public string BuildingSerialNo { get; set; }
    }
}
