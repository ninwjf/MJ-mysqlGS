using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CardManage.Model
{
    /// <summary>
    /// 卡片信息
    /// </summary>
    public class Card
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 区码
        /// </summary>
        public int RAreaCode { get; set; }
        /// <summary>
        /// 楼栋码
        /// </summary>
        public int RBuildCode { get; set; }
        /// <summary>
        /// 单元码
        /// </summary>
        public int RUnitCode { get; set; }
        /// <summary>
        /// 房间码
        /// </summary>
        public int RRoomCode { get; set; }
        /// <summary>
        /// 建筑ID
        /// </summary>
        public int BuildingID { get; set; }
        /// <summary>
        /// 建筑实体信息
        /// </summary>
        public Building BuildingInfo { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public uint CardNo { get; set; }
        /// <summary>
        /// 卡片类型(0：用户卡；1：巡更卡；2：管理卡；3:临时卡；4:住户键盘密码输入;5:公共键盘密码输入;6:非注册卡)
        /// </summary>
        public int CardType { get; set; }
        /// <summary>
        /// 卡片类型描述
        /// </summary>
        public string CardTypeDesc { get; set; }
        /// <summary>
        /// 卡片系列号,16进制表示，16个字节
        /// </summary>
        public string SerialNo { get; set; }
        /// <summary>
        /// 卡片有效期(Unix时间戳格式)
        /// </summary>
        public long ExpiryDate { get; set; }
        /// <summary>
        /// 是否被冻结
        /// </summary>
        public bool IfFrezen { get; set; }
        /// <summary>
        /// 发卡时间(Unix时间戳格式)
        /// </summary>
        public long CreateDate { get; set; }
        /// <summary>
        /// 持卡者姓名
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 持卡者电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        
        /// <summary>
        /// 房间编号
        /// </summary>
        public int RoomID { get; set; }        
        /// <summary>
        /// 房间编码
        /// </summary>
        public int RoomCode { get; set; }        
        /// <summary>
        /// 房间名称
        /// </summary>
        public string RoomName { get; set; }
        /// <summary>
        /// 单元编号
        /// </summary>
        public int UnitID { get; set; }
        /// <summary>
        /// 单元编码
        /// </summary>
        public int UnitCode { get; set; }
        /// <summary>
        /// 单元名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 楼栋编号
        /// </summary>
        public int BuildID { get; set; }
        /// <summary>
        /// 楼栋编码
        /// </summary>
        public int BuildCode { get; set; }
        /// <summary>
        /// 楼栋名称
        /// </summary>
        public string BuildName { get; set; }
        /// <summary>
        /// 小区编号
        /// </summary>
        public int AreaID { get; set; }
        /// <summary>
        /// 小区编码
        /// </summary>
        public int AreaCode { get; set; }
        /// <summary>
        /// 小区名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 扇区信息
        /// ChsInfo[0][]表示空扇区
        /// ChsInfo[1][]表示公司使用扇区
        /// ChsInfo[2][]表示其它使用扇区
        /// </summary>
        public ArrayList[] listChsInfo = new ArrayList[3];

        public Card()
        {
            for (int i = 0; i < 3; i++)
            {
                listChsInfo[i] = new ArrayList();
            }
        }
    }
}
