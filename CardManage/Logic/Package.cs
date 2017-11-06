using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.Tools;
using CardManage.Model;
namespace CardManage.Logic
{
    /// <summary>
    /// 数据包处理类
    /// </summary>
    public class Package
    {
        /// <summary>
        /// 指令类型
        /// </summary>
        public enum ECommandType
        {
            /// <summary>
            /// 未知类型
            /// </summary>
            UnKnown,
            /// <summary>
            /// 写卡发送指令
            /// </summary>
            WriteCard_Send,
            /// <summary>
            /// 写卡接收指令
            /// </summary>
            WriteCard_Response,
            /// <summary>
            /// 读卡发送指令
            /// </summary>
            ReadCard_Send,
            /// <summary>
            /// 读卡接收指令
            /// </summary>
            ReadCard_Response,
            /// <summary>
            /// 同步时间发送指令
            /// </summary>
            SyncTime_Send,
            /// <summary>
            /// 刷卡
            /// </summary>
            SwipingCard,
            /// <summary>
            /// 主机链路层ACK
            /// </summary>
            MainDeviceACK,
            /// <summary>
            /// 主机应答
            /// </summary>
            MainDeviceResponse,
            /// <summary>
            /// 刷卡头链路层ACK
            /// </summary>
            CardHeadDeviceACK,
            /// <summary>
            /// Ping指令
            /// </summary>
            Ping,
            /// <summary>
            /// Ping指令应答
            /// </summary>
            PingResponse,
            /// <summary>
            /// Ping结束
            /// </summary>
            MainDeviceACK4PingResponse,
            /// <summary>
            /// 扇区查询
            /// </summary>
            ChsSel,
            /// <summary>
            /// 扇区查询应答
            /// </summary>
            ChsSelResponse,
            /// <summary>
            /// 扇区重置
            /// </summary>
            ChsClean,
            /// <summary>
            /// 扇区重置应答
            /// </summary>
            ChsCleanResponse
        }

        /// <summary>
        /// 编码
        /// </summary>
        private static Encoding CurrentEncoding = ASCIIEncoding.Default;

        /// <summary>
        /// 检测响应包类型
        /// </summary>
        /// <param name="bArrBag"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static ECommandType GetResponseType(byte[] bArrBag, out string strErrMsg)
        {
            ECommandType eResponseType = ECommandType.UnKnown;
            strErrMsg = "";
            //数据校验
            if (bArrBag != null && bArrBag.Length >= 6)
            {
                //1.数据CRC8校验
                byte bCRCResult = CRC8A.GetCRC8(bArrBag, 0, bArrBag.Length - 1);
                byte bCRCSource = bArrBag[bArrBag.Length - 1];

                if (bCRCSource.Equals(bCRCResult))
                {
                    //2.识别帧起始符
                    if (bArrBag[0].Equals(0x55) && bArrBag[1].Equals(0xFF))
                    {
                        //3.识别数据长度 
                        byte[] bArrDataLength = new byte[4];
                        bArrDataLength[0] = bArrBag[3];
                        bArrDataLength[1] = bArrBag[2];
                        uint iDataLength = Functions.ConverToUInt(bArrDataLength);//低字节在前，高字节在后
                        if (bArrBag.Length == (iDataLength + 2 + 2 + 1))
                        {
                            //4.识别命令包类型
                            eResponseType = GetCommandTypeByCode(bArrBag[4]);
                            if (eResponseType.Equals(ECommandType.UnKnown))
                            {
                                strErrMsg = "数据包指令位不正确！";
                            }
                            else if (eResponseType.Equals(ECommandType.SwipingCard))
                            {
                                if (bArrBag.Length < 23) eResponseType = ECommandType.UnKnown;
                            }
                            else if (eResponseType.Equals(ECommandType.PingResponse))
                            {
                                if (bArrBag.Length < 18) eResponseType = ECommandType.UnKnown;
                            }
                        }
                        else
                        {
                            strErrMsg = "数据长度位与数据包实际长度不一致！";
                        }
                    }
                    else if (bArrBag[0].Equals(0x55) && bArrBag[1].Equals(0xAA))
                    {
                        //刷卡头链路层ACK
                        eResponseType = ECommandType.UnKnown;
                        if (bArrBag.Length >= 16)
                        {
                            if (bArrBag[4].Equals(0x21)) eResponseType = ECommandType.CardHeadDeviceACK;
                            if (bArrBag[4].Equals(0x92)) eResponseType = ECommandType.MainDeviceACK4PingResponse;
                        }
                    }
                    else
                    {
                        strErrMsg = "帧起始符不正确！";
                    }
                }
                else
                {
                    strErrMsg = "CRC校验不合法！";
                }
            }
            else
            {
                strErrMsg = "数据长度不正确！";
            }
            return eResponseType;
        }


        /// <summary>
        /// 创建发送包
        /// </summary>
        /// <param name="CommandType">指令类型</param>
        /// <param name="objCard">写卡指令专用</param>
        /// <param name="dt">时间，同步时间专用</param>
        /// <param name="objDeviceAddress">地址，Ping指令专用</param>
        /// <param name="chs">扇区重置,写卡专用</param>
        /// <returns></returns>
        public static byte[] BuildBag(ECommandType CommandType, Card objCard = null, String chs = null, DateTime? dt = null, DeviceAddress objDeviceAddress = null)
        {
            byte[] arrRtn = new byte[0];
            byte[] arrTemp;
            switch (CommandType)
            {
                case ECommandType.WriteCard_Send:
                    arrTemp = BuildBag_WriteCard(objCard);
                    if (arrTemp.Length > 0)
                    {
                        arrRtn = new byte[arrTemp.Length];
                        Array.Copy(arrTemp, arrRtn, arrRtn.Length);
                    }
                    break;
                case ECommandType.ReadCard_Send:
                    arrRtn = new byte[6];
                    arrTemp = ScaleConverter.HexStr2ByteArr("55 FF 00 01 34");
                    Array.Copy(arrTemp, arrRtn, arrTemp.Length);
                    arrRtn[5] = CRC8A.GetCRC8(arrTemp);
                    break;
                case ECommandType.Ping:
                    if (objDeviceAddress != null)
                    {
                        arrTemp = BuildBag_Ping(objDeviceAddress);
                        if (arrTemp.Length > 0)
                        {
                            arrRtn = new byte[arrTemp.Length];
                            Array.Copy(arrTemp, arrRtn, arrRtn.Length);
                        }
                    }
                    break;
                case ECommandType.SyncTime_Send://同步时间
                    if (dt != null)
                    {
                        byte[] arrRtnTemp2 = BuildBag_SyncTime(Convert.ToDateTime(dt));
                        if (arrRtnTemp2.Length > 0)
                        {
                            arrRtn = new byte[arrRtnTemp2.Length];
                            Array.Copy(arrRtnTemp2, arrRtn, arrRtn.Length);
                        }
                    }
                    break;
                case ECommandType.ChsSel://扇区查询
                    arrTemp = BuildBag_ChsSel();
                    if (arrTemp.Length > 0)
                    {
                        arrRtn = new byte[arrTemp.Length];
                        Array.Copy(arrTemp, arrRtn, arrRtn.Length);
                    }
                    break;

                case ECommandType.ChsClean://扇区重置
                    arrTemp = BuildBag_ChsClean(chs);
                    if (arrTemp.Length > 0)
                    {
                        arrRtn = new byte[arrTemp.Length];
                        Array.Copy(arrTemp, arrRtn, arrRtn.Length);
                    }
                    break;
            }
            return arrRtn;
        }

        /// <summary>
        /// 解包(写卡响应包)
        /// </summary>
        /// <param name="bArrBag"></param>
        /// <returns></returns>
        public static bool ParseBag_WriteCard(byte[] bArrBag, out uint iCardNo, out string strErrMsg)
        {
            bool bIfSucc = false;
            iCardNo = uint.MaxValue;
            strErrMsg = "";
            try
            {
                //数据位
                byte bCode = bArrBag[5];
                strErrMsg = GetFaultInfoByCode(bCode);
                if (strErrMsg.Equals(""))
                {
                    //解析卡号
                    int iBeginIndex = 6;
                    byte[] bArrCardNo = new byte[4];
                    for (int i = bArrCardNo.Length - 1; i >= 0; i--)
                    {
                        bArrCardNo[i] = bArrBag[iBeginIndex++];
                    }
                    iCardNo = Functions.ConverToUInt(bArrCardNo);//低字节在前，高字节在后
                    bIfSucc = true;
                }
            }
            catch (Exception err)
            {
                strErrMsg = err.Message;
            }
            return bIfSucc;
        }
        
        /// <summary>
        /// 解包(读卡响应包)
        /// </summary>
        /// <param name="bArrBag"></param>
        /// <returns></returns>
        public static bool ParseBag_ReadCard(byte[] bArrBag, ref CardData objCardData, out string strErrMsg)
        {
            bool bIfSucc = false;
            objCardData = new CardData();
            strErrMsg = "";
            try
            {
                //数据位
                byte bCode = bArrBag[5];
                strErrMsg = GetFaultInfoByCode(bCode);
                if (strErrMsg.Equals(""))
                {
                    //解析卡号
                    int iBeginIndex = 6;
                    byte[] bArrCardNo = new byte[4];
                    for (int i = bArrCardNo.Length - 1; i >= 0; i--)
                    {
                        bArrCardNo[i] = bArrBag[iBeginIndex++];
                    }
                    objCardData.CardNo = Functions.ConverToUInt(bArrCardNo);//低字节在前，高字节在后

                    //空卡直接返回 年月日为零则是空卡
                    if (int.Parse(bArrBag[15].ToString()) + 
                        int.Parse(bArrBag[16].ToString()) + 
                        int.Parse(bArrBag[17].ToString()) + 
                        int.Parse(bArrBag[18].ToString()) == 0)
                    {
                        objCardData.CardType = 6;
                    }
                    else
                    {
                        //解析区编码
                        objCardData.AreaCode = Convert.ToInt32(ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]));
                        //解析栋编码
                        objCardData.BuildCode = Convert.ToInt32(ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]));
                        //解析单元编码
                        objCardData.UnitCode = Convert.ToInt32(ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]));
                        //解析楼层编码
                        int iFloorCode = Convert.ToInt32(ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]));
                        //解析房间编码
                        objCardData.RoomCode = (iFloorCode * 100) + Convert.ToInt32(ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]));

                        //解析日期
                        //解析年
                        string strYear = ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]) + ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]);
                        //解析月
                        string strMonth = ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]);
                        //解析日
                        string strDay = ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]);
                        objCardData.ExpiryDate = Functions.ConvertToUnixTime(Convert.ToDateTime(string.Format("{0}-{1}-{2} 00:00:00", strYear, strMonth, strDay)));

                        //解析卡片类型
                        objCardData.CardType = Convert.ToInt32(bArrBag[iBeginIndex++]);

                        //解析预留 6个字节
                        //iBeginIndex += 6;
                        //新卡预留信息为开卡时间与及当前写扇区,旧卡信息为空
                        //bArrBag[iBeginIndex].ToString();
                        string strBeginYear = "20" + ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]);
                        string strBeginMonth = ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]);
                        string strBeginDay = ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]);
                        string strBeginHour = ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]);
                        string strBeginMin = ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]);
                        string chsStr = bArrBag[iBeginIndex++].ToString();

                        //月份为00表示旧卡
                        if (strBeginMonth.CompareTo("00") != 0 && strBeginMonth.CompareTo("0") != 0)
                        {
                            objCardData.BeginDate = Functions.ConvertToUnixTime(Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:00", strBeginYear, strBeginMonth, strBeginDay, strBeginHour, strBeginMin)));
                        }

                        //扇区00也表示旧卡， 防止乱写情况还是做个判断
                        if (chsStr.CompareTo("00") != 0 && chsStr.CompareTo("0") != 0)
                        {
                            objCardData.listChsInfo[1].Add(Convert.ToInt32(chsStr));
                        }
                        else
                        {
                            objCardData.listChsInfo[1].Add(1);
                        }

                        //解析序号 16个字节
                        byte[] bArrSerialNo = new byte[16];
                        Array.Copy(bArrBag, iBeginIndex, bArrSerialNo, 0, bArrSerialNo.Length);
                        objCardData.SerialNo = ScaleConverter.ByteArr2HexStr(bArrSerialNo).Replace(" ", "");
                    }
                    bIfSucc = true;
                }
            }
            catch (Exception err)
            {
                strErrMsg = err.Message;
            }
            return bIfSucc;
        }

        /// <summary>
        /// 解包刷卡响应包
        /// </summary>
        /// <param name="bArrBag"></param>
        /// <returns></returns>
        public static bool ParseBag_SwipingCard(byte[] bArrBag, ref SwipingCardData objSwipingCardData, out string strErrMsg)
        {
            bool bIfSucc = false;
            strErrMsg = "";
            try
            {
                //55 FF 00 12 20 01 00 00 00 00 06 12 34 00 01 F0 00 1D 00 9C 00 00 6D 刷卡头刷卡上传
                //数据位
                byte bCode = bArrBag[5];
                if (bCode == 0x01)
                {
                    //解析源地址
                    //解析设备类型
                    byte bDeviceType = Convert.ToByte(bArrBag[10] & 0x7F); //01111111
                    objSwipingCardData.DeviceType = GetDeviceType(bDeviceType);
                    //解析设备地址
                    byte[] bArrDeviceNo = new byte[2];
                    Array.Copy(bArrBag, 11, bArrDeviceNo, 0, bArrDeviceNo.Length);
                    objSwipingCardData.DeviceNo = ScaleConverter.ByteArr2HexStr(bArrDeviceNo).Replace(" ","");

                    //解析卡类型
                    byte bCardType = bArrBag[15];
                    objSwipingCardData.CardType = GetCardType(bCardType);

                    int iBeginIndex = 16;
                    //解析卡号
                    byte[] bArrCardNo = new byte[4];
                    for (int i = bArrCardNo.Length - 1; i >= 0; i--)
                    {
                        bArrCardNo[i] = bArrBag[iBeginIndex++];
                    }
                    objSwipingCardData.CardNo = Functions.ConverToUInt(bArrCardNo);//低字节在前，高字节在后
                    
                    bIfSucc = true;
                }
            }
            catch (Exception err)
            {
                strErrMsg = err.Message;
            }
            return bIfSucc;
        }

        /// <summary>
        /// 解包-ping响应包
        /// </summary>
        /// <param name="bArrBag"></param>
        /// <returns></returns>
        public static bool ParseBag_PingResponse(byte[] bArrBag, ref DeviceAddress objDeviceAddress, out string strErrMsg)
        {
            bool bIfSucc = false;
            strErrMsg = "";
            try
            {
                //55 FF 00 0D 93 01 00 00 00 00 06 12 34 00 00 41 98 FE  从机PING应答
                //数据位
                byte bCode = bArrBag[5];
                if (bCode == 0x01)
                {
                    //解析源地址
                    //解析设备类型
                    byte bDeviceType = Convert.ToByte(bArrBag[10] & 0x7F); //01111111
                    objDeviceAddress.DeviceType = GetDeviceType(bDeviceType);
                    //解析设备地址
                    byte[] bArrDeviceNo = new byte[2];
                    Array.Copy(bArrBag, 11, bArrDeviceNo, 0, bArrDeviceNo.Length);
                    objDeviceAddress.OriginalDeviceNo = ScaleConverter.ByteArr2HexStr(bArrDeviceNo).Replace(" ", "");
                    objDeviceAddress.DeviceNo = string.Format("{0}0000", objDeviceAddress.OriginalDeviceNo);

                    int iBeginIndex = 15;
                    ////解析系列号
                    byte[] bArrPingNum = new byte[2];
                    for (int i = bArrPingNum.Length - 1; i >= 0; i--)
                    {
                        bArrPingNum[i] = bArrBag[iBeginIndex++];
                    }
                    objDeviceAddress.PingNum = Functions.ConverToInt(bArrPingNum);//低字节在前，高字节在后

                    //拷贝数据包
                    objDeviceAddress.Buffer = new byte[bArrBag.Length];
                    Array.Copy(bArrBag, objDeviceAddress.Buffer, objDeviceAddress.Buffer.Length);
                    bIfSucc = true;
                }
            }
            catch (Exception err)
            {
                strErrMsg = err.Message;
            }
            return bIfSucc;
        }

        /// <summary>
        /// 解包收到 ping指令 响应包
        /// </summary>
        /// <param name="bArrBag"></param>
        /// <returns></returns>
        public static bool ParseBag_MainDeviceACK4PingResponse(byte[] bArrBag, ref DeviceAddress objDeviceAddress, out string strErrMsg)
        {
            bool bIfSucc = false;
            strErrMsg = "";
            try
            {
                //55 AA 00 0B 92 06 01 01 00 00 01 00 00 00 00 57   从机PING应答
                //数据位
                byte bCode = bArrBag[10];
                if (bCode == 0x01)
                {
                    //解析源地址
                    //解析设备类型
                    byte bDeviceType = Convert.ToByte(bArrBag[5] & 0x7F); //01111111
                    objDeviceAddress.DeviceType = GetDeviceType(bDeviceType);
                    //解析设备地址
                    byte[] bArrDeviceNo = new byte[2];
                    Array.Copy(bArrBag, 6, bArrDeviceNo, 0, bArrDeviceNo.Length);
                    objDeviceAddress.OriginalDeviceNo = ScaleConverter.ByteArr2HexStr(bArrDeviceNo).Replace(" ", "");
                    objDeviceAddress.DeviceNo = string.Format("{0}0000", objDeviceAddress.OriginalDeviceNo);

                    //拷贝数据包
                    objDeviceAddress.Buffer = new byte[bArrBag.Length];
                    Array.Copy(bArrBag, objDeviceAddress.Buffer, objDeviceAddress.Buffer.Length);
                    bIfSucc = true;
                }
            }
            catch (Exception err)
            {
                strErrMsg = err.Message;
            }
            return bIfSucc;
        }

        /// <summary>
        /// 主机链路层ACK
        /// </summary>
        /// <param name="bArrTargetAddress">5字节目标地址</param>
        /// <param name="bArrOrigialAddress">5字节源地址</param>
        /// <returns></returns>
        public static byte[] BuildBag_MainDeviceACK(byte[] bArrTargetAddress, byte[] bArrOrigialAddress)
        {
            byte[] arrRtn = new byte[0];

            if (bArrTargetAddress.Length == 5 && bArrOrigialAddress.Length == 5)
            {
                //55 AA 00 0B 20 01 00 00 00 00 06 12 34 00 01 1E  主机链路层ACK
                byte[] arrRtnTemp = new byte[100];
                int iIndex = 0;
                //帧起始符 高位
                arrRtnTemp[iIndex++] = 0x55;
                //帧起始符 低位
                arrRtnTemp[iIndex++] = 0xAA;
                //数据长度 高位
                arrRtnTemp[iIndex++] = 0x00;
                //数据长度 低位
                arrRtnTemp[iIndex++] = 0x0B;
                //命令字
                arrRtnTemp[iIndex++] = 0x20;

                //目的地址照搬回去
                for (int i = 0; i <= 4; i++)
                {
                    arrRtnTemp[iIndex++] = bArrTargetAddress[i];
                }
                //源地址照搬回去
                for (int i = 0; i <= 4; i++)
                {
                    arrRtnTemp[iIndex++] = bArrOrigialAddress[i];
                }
                //CRC8校验
                arrRtnTemp[iIndex++] = CRC8A.GetCRC8(arrRtnTemp, 0, iIndex - 1);

                arrRtn = new byte[iIndex];
                Array.Copy(arrRtnTemp, arrRtn, arrRtn.Length);
            }
            return arrRtn;
        }

        

        /// <summary>
        /// 主机链路层ACK-响应ping
        /// </summary>
        /// <param name="bArrTargetAddress">5字节目标地址</param>
        /// <param name="bArrOrigialAddress">5字节源地址</param>
        /// <returns></returns>
        public static byte[] BuildBag_MainDeviceACK4Ping(byte[] bArrTargetAddress, byte[] bArrOrigialAddress)
        {
            byte[] arrRtn = new byte[0];

            if (bArrTargetAddress.Length == 5 && bArrOrigialAddress.Length == 5)
            {
                //55 FF 00 0D 93 01 00 00 00 00 06 01 01 00 00 00 01 22
                byte[] arrRtnTemp = new byte[100];
                int iIndex = 0;
                //帧起始符 高位
                arrRtnTemp[iIndex++] = 0x55;
                //帧起始符 低位
                arrRtnTemp[iIndex++] = 0xAA;
                //数据长度 高位
                arrRtnTemp[iIndex++] = 0x00;
                //数据长度 低位
                arrRtnTemp[iIndex++] = 0x0B;
                //命令字
                arrRtnTemp[iIndex++] = 0x93;

                //目的地址照搬回去
                for (int i = 0; i <= 4; i++)
                {
                    arrRtnTemp[iIndex++] = bArrTargetAddress[i];
                }
                //源地址照搬回去
                for (int i = 0; i <= 4; i++)
                {
                    arrRtnTemp[iIndex++] = bArrOrigialAddress[i];
                }
                //CRC8校验
                arrRtnTemp[iIndex++] = CRC8A.GetCRC8(arrRtnTemp, 0, iIndex - 1);

                arrRtn = new byte[iIndex];
                Array.Copy(arrRtnTemp, arrRtn, arrRtn.Length);
            }
            return arrRtn;
        }

        /// <summary>
        /// 主机应答
        /// </summary>
        /// <param name="bArrTargetAddress">5字节目标地址</param>
        /// <param name="bArrOrigialAddress">5字节源地址</param>
        /// <returns></returns>
        public static byte[] BuildBag_MainDeviceResponse(byte[] bArrTargetAddress, byte[] bArrOrigialAddress)
        {
            byte[] arrRtn = new byte[0];

            if (bArrTargetAddress.Length == 5 && bArrOrigialAddress.Length == 5)
            {
                //55 FF 00 0C 21 06 12 34 00 01 01 00 00 00 00 00 xx 主机应答
                byte[] arrRtnTemp = new byte[1450];
                int iIndex = 0;
                //帧起始符 高位
                arrRtnTemp[iIndex++] = 0x55;
                //帧起始符 低位
                arrRtnTemp[iIndex++] = 0xFF;
                //数据长度 高位
                arrRtnTemp[iIndex++] = 0x00;
                //数据长度 低位
                arrRtnTemp[iIndex++] = 0x0C;
                //命令字
                arrRtnTemp[iIndex++] = 0x21;

                //源地址转成目的地址
                for (int i = 0; i <= 4; i++)
                {
                    arrRtnTemp[iIndex++] = bArrOrigialAddress[i];
                }
                //目的地址转成源地址
                for (int i = 0; i <= 4; i++)
                {
                    arrRtnTemp[iIndex++] = bArrTargetAddress[i];
                }

                //未定义
                arrRtnTemp[iIndex++] = 0x00;

                //CRC8校验
                arrRtnTemp[iIndex++] = CRC8A.GetCRC8(arrRtnTemp, 0, iIndex - 1);

                arrRtn = new byte[iIndex];
                Array.Copy(arrRtnTemp, arrRtn, arrRtn.Length);
            }
            return arrRtn;
        }

        /// <summary>
        /// 创建包-写卡
        /// </summary>
        /// <param name="objCard"></param>
        /// <returns></returns>
        private static byte[] BuildBag_WriteCard(Card objCard)
        {
            byte[] arrRtn = new byte[0];
            if (objCard != null)
            {
                int iAreaCode, iBuidCode, iUnitCode, iRoomCode;
                iAreaCode = objCard.RAreaCode;
                iBuidCode = objCard.RBuildCode;
                iUnitCode = objCard.RUnitCode;
                iRoomCode = objCard.RRoomCode;

                if ((iAreaCode >= 0 && iAreaCode <= 99) && (iBuidCode >= 0 && iBuidCode <= 99) && (iUnitCode >= 0 && iUnitCode <= 99) && (iRoomCode >= 0 && iRoomCode <= 9999))
                {
                    byte[] arrRtnTemp = new byte[1450];
                    int iIndex = 0;
                    //帧起始符 高位
                    arrRtnTemp[iIndex++] = 0x55;
                    //帧起始符 低位
                    arrRtnTemp[iIndex++] = 0xFF;
                    //数据长度 高位
                    arrRtnTemp[iIndex++] = 0x00;
                    //数据长度 低位
                    arrRtnTemp[iIndex++] = 0x21;
                    //命令字
                    arrRtnTemp[iIndex++] = GetCommandCodeByType(ECommandType.WriteCard_Send);

                    //区编码
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(iAreaCode));
                    //栋编码
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(iBuidCode));
                    //单元编码
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(iUnitCode));

                    string strRoomCode = FormatRoomCode(iRoomCode);
                    //楼层编码
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(strRoomCode.Substring(0, 2)));
                    //房间编码
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(strRoomCode.Substring(2, 2)));

                    //过期时间
                    DateTime dt = Functions.ConvertToNormalTime(objCard.ExpiryDate);
                    string strYear = dt.Year.ToString();
                    //年-高位
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(strYear.Substring(0, 2));
                    //年-低位
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(strYear.Substring(2, 2));
                    //月
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(dt.Month));
                    //日
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(dt.Day));

                    //卡片类型
                    arrRtnTemp[iIndex++] = Convert.ToByte(objCard.CardType);

                    DateTime CreateDt = System.DateTime.Now;
                    //预留6个字节
                    //iIndex += 6;
                    //发卡时间 当前 年
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(CreateDt.Year.ToString().Substring(2, 2)));
                    //发卡时间 当前 月
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(CreateDt.Month));
                    //发卡时间 当前 日
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(CreateDt.Day));
                    //发卡时间 当前 时
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(CreateDt.Hour));
                    //发卡时间 当前 分
                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(CreateDt.Minute));
                    //发卡时间 当前 写扇区  先转成16进制再转成字符串 
                    string chsStr = "";
                    switch (Convert.ToInt32(objCard.listChsInfo[1][0]))
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            chsStr = objCard.listChsInfo[1][0].ToString();
                            break;
                        case 10:
                            chsStr = string.Format("A");
                            break;
                        case 11:
                            chsStr = string.Format("B");
                            break;
                        case 12:
                            chsStr = string.Format("C");
                            break;
                        case 13:
                            chsStr = string.Format("D");
                            break;
                        case 14:
                            chsStr = string.Format("E");
                            break;
                        case 15:
                            chsStr = string.Format("F");
                            break;
                    }

                    arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(chsStr));

                    //序号 16个字节
                    string strSerialNo = objCard.SerialNo.Replace(" ", "").PadLeft(32,'0');
                    for (int i = 1; i <= 16; i++)
                    {
                        arrRtnTemp[iIndex++] = ScaleConverter.OneHexStr2Byte(strSerialNo.Substring((i - 1) * 2, 2));
                    }

                    //CRC8校验
                    arrRtnTemp[iIndex++] = CRC8A.GetCRC8(arrRtnTemp, 0, iIndex-1);

                    arrRtn = new byte[iIndex];
                    Array.Copy(arrRtnTemp, arrRtn, arrRtn.Length);
                }
            }
            return arrRtn;
        }

        /// <summary>
        /// 创建包-同步时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static byte[] BuildBag_SyncTime(DateTime dt)
        {
            byte[] arrRtn = new byte[23];
            int iIndex = 0;
            //帧起始符 高位
            arrRtn[iIndex++] = 0x55;
            //帧起始符 低位
            arrRtn[iIndex++] = 0xFF;
            //数据长度 高位
            arrRtn[iIndex++] = 0x00;
            //数据长度 低位
            arrRtn[iIndex++] = 0x12;
            //命令字
            arrRtn[iIndex++] = 0x9A;

            //广播地址 5字节
            arrRtn[iIndex++] = 0xFF;
            arrRtn[iIndex++] = 0xFF;
            arrRtn[iIndex++] = 0xFF;
            arrRtn[iIndex++] = 0xFF;
            arrRtn[iIndex++] = 0xFF;
            //主管理机地址 5字节
            arrRtn[iIndex++] = 0x01;
            arrRtn[iIndex++] = 0x00;
            arrRtn[iIndex++] = 0x00;
            arrRtn[iIndex++] = 0x00;
            arrRtn[iIndex++] = 0x00;
            //时间 7字节
            string strYear = dt.Year.ToString();
            //年
            arrRtn[iIndex++] = ScaleConverter.OneHexStr2Byte(strYear.Substring(2, 2));
            //月
            arrRtn[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(dt.Month));
            //日
            arrRtn[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(dt.Day));
            //周
            arrRtn[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(Functions.GetWeekOfYear(dt)));
            //时
            arrRtn[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(dt.Hour));
            //分
            arrRtn[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(dt.Minute));
            //秒
            arrRtn[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(dt.Second));
            
            //CRC8校验
            arrRtn[iIndex++] = CRC8A.GetCRC8(arrRtn, 0, arrRtn.Length - 1);
            return arrRtn;
        }

        /// <summary>
        /// 创建包-ping指令
        /// </summary>
        /// <param name="bArrTargetAddress">目的地址5个字节</param>
        /// <returns></returns>
        private static byte[] BuildBag_Ping(DeviceAddress objDeviceAddress)
        {
            if (objDeviceAddress == null || string.IsNullOrEmpty(objDeviceAddress.DeviceNo)) return new byte[0];

            byte[] arrRtn = new byte[18];
            int iIndex = 0;
            //帧起始符 高位
            arrRtn[iIndex++] = 0x55;
            //帧起始符 低位
            arrRtn[iIndex++] = 0xFF;
            //数据长度 高位
            arrRtn[iIndex++] = 0x00;
            //数据长度 低位
            arrRtn[iIndex++] = 0x0D;
            //命令字
            arrRtn[iIndex++] = 0x92;

            //目的地址 5字节
            arrRtn[iIndex++] = GetDeviceTypeByte(objDeviceAddress.DeviceType);
            byte[] bArrDeviceNo = ScaleConverter.HexStr2ByteArr(objDeviceAddress.DeviceNo);
            for (int i = 0; i < 4; i++)
            {
                arrRtn[iIndex++] = bArrDeviceNo[i];
            }
            //源地址 5字节
            arrRtn[iIndex++] = 0x01;
            arrRtn[iIndex++] = 0x00;
            arrRtn[iIndex++] = 0x00;
            arrRtn[iIndex++] = 0x00;
            arrRtn[iIndex++] = 0x00;

            //序号 2字节 BCD码：0～9999（每次序号不一样）
            byte[] bArrPingNum = ScaleConverter.Int2ByteArr(objDeviceAddress.PingNum);
            arrRtn[iIndex++] = bArrPingNum[1];
            arrRtn[iIndex++] = bArrPingNum[0];
            //CRC8校验
            arrRtn[iIndex++] = CRC8A.GetCRC8(arrRtn, 0, arrRtn.Length - 1);
            return arrRtn;
        }

        /// <summary>
        /// 根据指令类型获得指令编码
        /// </summary>
        /// <param name="CommandType">指令类型</param>
        /// <returns>返回0x00为未知</returns>
        public static byte GetCommandCodeByType(ECommandType eCommandType)
        {
            byte bRtn = 0x00;
            switch (eCommandType)
            {
                case ECommandType.WriteCard_Send:
                    bRtn = 0x32;
                    break;
                case ECommandType.WriteCard_Response:
                    bRtn = 0x33;
                    break;
                case ECommandType.ReadCard_Send:
                    bRtn = 0x34;
                    break;
                case ECommandType.ReadCard_Response:
                    bRtn = 0x35;
                    break;
            }
            return bRtn;
        }

        /// <summary>
        /// 根据指令编码获得指令类型
        /// </summary>
        /// <param name="bCode">指令编码</param>
        /// <returns></returns>
        public static ECommandType GetCommandTypeByCode(byte bCode)
        {
            ECommandType eCommandType = ECommandType.UnKnown;
            switch (bCode)
            {
                case 0x20:
                    eCommandType = ECommandType.SwipingCard;
                    break;
                case 0x32:
                    eCommandType = ECommandType.WriteCard_Send;
                    break;
                case 0x33:
                    eCommandType = ECommandType.WriteCard_Response;
                    break;
                case 0x34:
                    eCommandType = ECommandType.ReadCard_Send;
                    break;
                case 0x35:
                    eCommandType = ECommandType.ReadCard_Response;
                    break;
                case 0x36:
                    eCommandType = ECommandType.ChsSel;
                    break;
                case 0x37:
                    eCommandType = ECommandType.ChsSelResponse;
                    break;
                case 0x38:
                    eCommandType = ECommandType.ChsClean;
                    break;
                case 0x39:
                    eCommandType = ECommandType.ChsCleanResponse;
                    break;
                case 0x93://PING应答
                    eCommandType = ECommandType.PingResponse;
                    break;
            }
            return eCommandType;
        }

        /// <summary>
        /// 获得设备类型ID
        /// </summary>
        /// <param name="bType"></param>
        /// <returns></returns>
        private static int GetDeviceType(byte bType)
        {
            int iDType = -1;
            switch (bType)
            {
                case 0x01://管理机
                    iDType = 1;
                    break;
                case 0x02://交换机
                    iDType = 2;
                    break;
                case 0x03://切换器
                    iDType = 3;
                    break;
                case 0x06://围墙刷卡头
                    iDType = 4;
                    break;
                case 0x07://围墙机
                    iDType = 5;
                    break;
                case 0x08://门口机
                    iDType = 6;
                    break;
                case 0x0B://二次门口机
                    iDType = 7;
                    break;
                default:
                    iDType = -1;
                    break;
            }
            return iDType;
        }


        /// <summary>
        /// 根据设备类型ID获得设备类型编码
        /// </summary>
        /// <param name="bType"></param>
        /// <returns></returns>
        private static byte GetDeviceTypeByte(int iType)
        {
            byte bDType = 0x01;
            switch (iType)
            {
                case 1://管理机
                    bDType = 0x01;
                    break;
                case 2://交换机
                    bDType = 0x02;
                    break;
                case 3://切换器
                    bDType = 0x03;
                    break;
                case 4://围墙刷卡头
                    bDType = 0x06;
                    break;
                case 5://围墙机
                    bDType = 0x07;
                    break;
                case 6://门口机
                    bDType = 0x08;
                    break;
                case 7://二次门口机
                    bDType = 0x0B;
                    break;
                default:
                    bDType = 0x01;
                    break;
            }
            return bDType;
        }
        

        /// <summary>
        /// 获得卡片类型ID
        /// </summary>
        /// <param name="bType"></param>
        /// <returns></returns>
        private static int GetCardType(byte bType)
        {
            int iDType = -1;
            switch (bType)
            {
                case 0x10://住户卡
                    iDType = 0;
                    break;
                case 0x20://巡更卡
                    iDType = 1;
                    break;
                case 0x30://临时卡
                    iDType = 3;
                    break;
                case 0x40://超级卡/管理卡
                    iDType = 2;
                    break;
                case 0xA0://住户键盘密码输入
                    iDType = 4;
                    break;
                case 0xB0://公共键盘密码输入
                    iDType = 5;
                    break;
                case 0xF0://非注册卡
                    iDType = 6;
                    break;
                default:
                    iDType = -1;
                    break;
            }
            return iDType;
        }

        /// <summary>
        /// 根据错误代码获得错误描述
        /// </summary>
        /// <param name="CommandType">指令类型</param>
        /// <returns>返回空表示正确</returns>
        public static string GetFaultInfoByCode(byte bCode)
        {
            //0x01	写控制位错误	
            //0x02	密码错误	
            //0x03	写日期错误	
            //0x04	写序列号错误	
            //0x05	写入数据校验错误	
            //0x06	写卡或读卡成功	
            //0x07	查询卡片扇区失败
            //0x08	查询卡片扇区成功	
            //0x09	扇区重置成功	
            //0x10	重置扇区失败	
            //0x11	无可使用扇区	

            string strRtn = "未知错误";

            switch (bCode)
            {
                case 0x01:
                    strRtn = "写控制位错误";
                    break;
                case 0x02:
                    strRtn = "密码错误";
                    break;
                case 0x03:
                    strRtn = "写日期错误";
                    break;
                case 0x04:
                    strRtn = "写序列号错误";
                    break;
                case 0x05:
                    strRtn = "写入数据校验错误";
                    break;
                case 0x06://写卡或读卡成功
                    strRtn = "";
                    break;
                case 0x07://查询卡片扇区失败
                    strRtn = "查询卡片扇区失败";
                    break;
                case 0x08://查询卡片扇区成功
                    strRtn = "";
                    break;
                case 0x09://扇区重置成功
                    strRtn = "";
                    break;
                case 0x10://重置扇区失败
                    strRtn = "重置扇区失败";
                    break;
                case 0x11://无可使用扇区
                    strRtn = "无可使用扇区";
                    break;
                default:
                    strRtn = "未知错误";
                    break;
            }
            return strRtn;
        }

        /// <summary>
        /// 左补齐2位
        /// </summary>
        /// <param name="objCode"></param>
        /// <returns></returns>
        private static string BQ2(object objValue)
        {
            string strRtn = "";
            if (objValue != null)
            {
                string strValue = objValue.ToString().Trim();
                strRtn = strValue.PadLeft(2, '0');
            }
            return strRtn;
        }

        /// <summary>
        /// 格式化房间编码，补齐4位
        /// </summary>
        /// <param name="objCode"></param>
        /// <returns></returns>
        private static string FormatRoomCode(object objCode)
        {
            string strRtn = "";
            if (objCode != null)
            {
                string strCode = objCode.ToString().Trim();
                strRtn = strCode.PadLeft(4, '0');
            }
            return strRtn;
        }


        /// <summary>
        /// 创建包-扇区查询
        /// </summary>
        /// <returns></returns>
        private static byte[] BuildBag_ChsSel()
        {
            byte[] arrRtn = new byte[6];
            int iIndex = 0;
            //帧起始符 高位
            arrRtn[iIndex++] = 0x55;
            //帧起始符 低位
            arrRtn[iIndex++] = 0xFF;
            //数据长度 高位
            arrRtn[iIndex++] = 0x00;
            //数据长度 低位
            arrRtn[iIndex++] = 0x01;
            //命令字
            arrRtn[iIndex++] = 0x36;

            //CRC8校验
            arrRtn[iIndex++] = CRC8A.GetCRC8(arrRtn, 0, arrRtn.Length - 1);
            return arrRtn;
        }

        /// <summary>
        /// 解包-扇区查询包
        /// </summary>
        /// <param name="bArrBag"></param>
        /// <returns></returns>
        public static bool ParseBag_ChsSel(byte[] bArrBag, ref CardData objCardData, out string strErrMsg)
        {
            bool bIfSucc = false;
            objCardData = new CardData();
            strErrMsg = "";

            try
            {
                //55 FF 00 12 20 01 00 00 00 00 06 12 34 00 01 F0 00 1D 00 9C 00 00 6D 刷卡头刷卡上传
                //数据位
                byte bCode = bArrBag[5];
                strErrMsg = GetFaultInfoByCode(bCode);
                if (strErrMsg.Equals(""))
                {
                    //解析扇区信息
                    int iBeginIndex = 6;
                    int j = 0;
                    for (int i = 0; i <= 15; i++)
                    {
                        j = Convert.ToInt32(ScaleConverter.Byte2HexStr(bArrBag[iBeginIndex++]));
                        objCardData.listChsInfo[j].Add(i);
                    }

                    //解析卡号
                    byte[] bArrCardNo = new byte[4];
                    for (int i = bArrCardNo.Length - 1; i >= 0; i--)
                    {
                        bArrCardNo[i] = bArrBag[iBeginIndex++];
                    }
                    objCardData.CardNo = Functions.ConverToUInt(bArrCardNo);//低字节在前，高字节在后

                    bIfSucc = true;
                }
            }
            catch (Exception err)
            {
                strErrMsg = err.Message;
            }
            return bIfSucc;
        }

        /// <summary>
        /// 创建包-扇区重置
        /// </summary>
        /// <param name="chs">需要重置的扇区</param>
        /// <returns></returns>
        private static byte[] BuildBag_ChsClean(String chs)
        {
            byte[] arrRtn = new byte[7];
            int iIndex = 0;
            //帧起始符 高位
            arrRtn[iIndex++] = 0x55;
            //帧起始符 低位
            arrRtn[iIndex++] = 0xFF;
            //数据长度 高位
            arrRtn[iIndex++] = 0x00;
            //数据长度 低位
            arrRtn[iIndex++] = 0x02;
            //命令字
            arrRtn[iIndex++] = 0x38;
            //数据 要重置的扇区

            string chsStr = "";
            switch (Convert.ToInt32(chs))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    chsStr = chs;
                    break;
                case 10:
                    chsStr = string.Format("A");
                    break;
                case 11:
                    chsStr = string.Format("B");
                    break;
                case 12:
                    chsStr = string.Format("C");
                    break;
                case 13:
                    chsStr = string.Format("D");
                    break;
                case 14:
                    chsStr = string.Format("E");
                    break;
                case 15:
                    chsStr = string.Format("F");
                    break;
            }
            arrRtn[iIndex++] = ScaleConverter.OneHexStr2Byte(BQ2(chsStr));

            //CRC8校验
            arrRtn[iIndex++] = CRC8A.GetCRC8(arrRtn, 0, arrRtn.Length - 1);
            return arrRtn;
        }


        /// <summary>
        /// 解包-扇区重置包
        /// </summary>
        /// <param name="bArrBag"></param>
        /// <returns></returns>
        public static bool ParseBag_ChsClean(byte[] bArrBag, out uint iCard, out string strErrMsg)
        {
            iCard = uint.MaxValue;
            bool bIfSucc = false;
            strErrMsg = "";
            try
            {
                //数据位
                byte bCode = bArrBag[5];
                strErrMsg = GetFaultInfoByCode(bCode);

                //解析卡号
                int iBeginIndex = 6;
                byte[] bArrCardNo = new byte[4];
                for (int i = bArrCardNo.Length - 1; i >= 0; i--)
                {
                    bArrCardNo[i] = bArrBag[iBeginIndex++];
                }
                iCard = Functions.ConverToUInt(bArrCardNo);//低字节在前，高字节在后
                if (strErrMsg.Equals(""))
                {
                    bIfSucc = true;
                }
            }
            catch (Exception err)
            {
                strErrMsg = err.Message;
            }
            return bIfSucc;
        }
    }
}
