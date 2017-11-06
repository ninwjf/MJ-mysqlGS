using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CardManage.Model
{
    /// <summary>
    /// 卡数据，用于通讯交互
    /// </summary>
    public class CardData
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public uint CardNo { get; set; }
        /// <summary>
        /// 卡片类型(0：用户卡；1：巡更卡；2：管理卡；3:临时卡；4:住户键盘密码输入;5:公共键盘密码输入;6:非注册卡)
        /// </summary>
        public int CardType { get; set; }
        /// <summary>
        /// 卡片系列号
        /// </summary>
        public string SerialNo { get; set; }
        /// <summary>
        /// 区编码(范围：0x00~0x99)
        /// </summary>
        public int AreaCode { get; set; }
        /// <summary>
        /// 栋编码(范围：0x00~0x99)
        /// </summary>
        public int BuildCode { get; set; }
        /// <summary>
        /// 单元编码(范围：0x00~0x99)
        /// </summary>
        public int UnitCode { get; set; }
        /// <summary>
        /// 房间编码(范围：0x0000~0x9999)
        /// </summary>
        public int RoomCode { get; set; }
        /// <summary>
        /// 卡片有效期(Unix时间戳格式)
        /// </summary>
        public long ExpiryDate { get; set; }
        /// <summary>
        /// 开卡日期(Unix时间戳格式)
        /// </summary>
        public long BeginDate { get; set; }
        /// <summary>
        /// 卡片扇区信息
        /// listChsInfo[0] 表示空白扇区
        /// listChsInfo[1] 表示公司使用扇区
        /// listChsInfo[2] 表示其它使用扇区
        /// </summary>
        public ArrayList[] listChsInfo = new ArrayList[3];

        public CardData()
        {
            for(int i=0; i<3; i++)
            {
                listChsInfo[i] = new ArrayList();
            }
        }
    }
}
