using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.Model;
using CardManage.Tools;
namespace CardManage.Logic
{
    /// <summary>
    /// 卡片配置器
    /// </summary>
    public class CardConfiger : ConfigerBase
    {
        //静态变量
        private static object _LockObject = new object();
        private static CardConfiger _Configer;


        private WriteCardReponseHandler _OnWriteCardReponse = null;
        public delegate void WriteCardReponseHandler(uint iCardNo, string strErrInfo);
        /// <summary>
        /// 当写卡返回时
        /// </summary>
        public event WriteCardReponseHandler OnWriteCardReponse
        {
            add
            {
                this._OnWriteCardReponse += value;
            }
            remove
            {
                this._OnWriteCardReponse -= value;
            }
        }

        private ReadCardReponseHandler _OnReadCardReponse = null;
        public delegate void ReadCardReponseHandler(CardData objCardData, string strErrInfo);
        /// <summary>
        /// 当读卡返回时
        /// </summary>
        public event ReadCardReponseHandler OnReadCardReponse
        {
            add
            {
                this._OnReadCardReponse += value;
            }
            remove
            {
                this._OnReadCardReponse -= value;
            }
        }

        private ChsSelReponseHandler _OnChsSelReponse = null;
        public delegate void ChsSelReponseHandler(CardData objCardData, string strErrInfo);
        /// <summary>
        /// 当扇区查询返回时
        /// </summary>
        public event ChsSelReponseHandler OnChsSelReponse
        {
            add
            {
                this._OnChsSelReponse += value;
            }
            remove
            {
                this._OnChsSelReponse -= value;
            }
        }

        private ChsCleanReponseHandler _OnChsCleanReponse = null;
        public delegate void ChsCleanReponseHandler(uint iChs, string strErrInfo);
        /// <summary>
        /// 当重置扇区返回时
        /// </summary>
        public event ChsCleanReponseHandler OnChsCleanReponse
        {
            add
            {
                this._OnChsCleanReponse += value;
            }
            remove
            {
                this._OnChsCleanReponse -= value;
            }
        }

        private CardConfiger()
            : base()
        {

        }

        public static CardConfiger GetInstance()
        {
            if (_Configer == null)
            {
                lock (_LockObject)
                {
                    if (_Configer == null)
                    {
                        _Configer = new CardConfiger();
                    }
                }
            }
            return _Configer;
        }

        /// <summary>
        /// 发送写卡指令
        /// </summary>
        /// <param name="objCard">数据对象</param>
        public bool WriteCard(Card objCard, out string strErrInfo)
        {
            bool bIfSucc = false;
            strErrInfo = "";
            if (objCard != null)
            {
                int iAreaCode, iBuidCode, iUnitCode, iRoomCode;
                iAreaCode = objCard.RAreaCode;
                iBuidCode = objCard.RBuildCode;
                iUnitCode = objCard.RUnitCode;
                iRoomCode = objCard.RRoomCode;

                if ((iAreaCode >= 0 && iAreaCode <= 99) && (iBuidCode >= 0 && iBuidCode <= 99) && (iUnitCode >= 0 && iUnitCode <= 99) && (iRoomCode >= 0 && iRoomCode <= 9999))
                {
                    //发送写卡指令
                    byte[] bArrBag = Package.BuildBag(Package.ECommandType.WriteCard_Send, objCard);
                    bIfSucc = SendCommand(bArrBag, out strErrInfo);
                    DoNotice(string.Format("{0}{1}", "发送写卡指令", (bIfSucc ? "成功" : "失败")), bArrBag);
                }
                else
                {
                    strErrInfo = "发送的数据包编码不正确";
                }
            }
            else
            {
                strErrInfo = "发送的数据包为空";
            }
            return bIfSucc;
        }

        /// <summary>
        /// 发送读卡指令
        /// </summary>
        public bool ReadCard(out string strErrInfo)
        {
            bool bIfSucc = false;
            strErrInfo = "";
            //发送写卡指令
            byte[] bArrBag = Package.BuildBag(Package.ECommandType.ReadCard_Send);
            bIfSucc = SendCommand(bArrBag, out strErrInfo);
            DoNotice(string.Format("{0}{1}", "发送读卡指令", (bIfSucc ? "成功" : "失败")), bArrBag);
            return bIfSucc;
        }

        /// <summary>
        /// 发送扇区查询指令
        /// </summary>
        public bool ChsSel(out string strErrInfo)
        {
            bool bIfSucc = false;
            strErrInfo = "";
            //发送扇区查询指令
            byte[] bArrBag = Package.BuildBag(Package.ECommandType.ChsSel);
            bIfSucc = SendCommand(bArrBag, out strErrInfo);
            DoNotice(string.Format("{0}{1}", "发送扇区查询指令", (bIfSucc ? "成功" : "失败")), bArrBag);
            return bIfSucc;
        }

        /// <summary>
        /// 发送扇区重置指令
        /// </summary>
        public bool ChsClean(String ChsNo, out string strErrInfo)
        {
            bool bIfSucc = false;
            strErrInfo = "";
            //发送写卡指令
            byte[] bArrBag = Package.BuildBag(Package.ECommandType.ChsClean, null, ChsNo);
            bIfSucc = SendCommand(bArrBag, out strErrInfo);
            DoNotice(string.Format("{0}{1}", "发送扇区重置指令", (bIfSucc ? "成功" : "失败")), bArrBag);
            return bIfSucc;
        }

        protected override void OnReceiveFrameData(byte[] bufFrame)
        {
            if (bufFrame == null || bufFrame.Length == 0) return;

            DoNotice(string.Format("接收数据"), bufFrame);
            
            bool bIfSucc = ProcessOne(bufFrame, out string strErrMsg);
            //抖动处理
            if (!bIfSucc && bufFrame.Length > 2)
            {
                byte[] bufFrameTry = new byte[bufFrame.Length - 1];
                //1.尝试去掉头一个字节
                Array.Copy(bufFrame, 1, bufFrameTry, 0, bufFrameTry.Length);
                bIfSucc = ProcessOne(bufFrameTry, out strErrMsg);
                if (!bIfSucc)
                {
                    //2.尝试去掉最后一个字节
                    Array.Copy(bufFrame, 0, bufFrameTry, 0, bufFrameTry.Length);
                    bIfSucc = ProcessOne(bufFrameTry, out strErrMsg);
                }
            }
        }

        private bool ProcessOne(byte[] bufFrame, out string strErrMsg)
        {
            bool bIfSucc = false;
            strErrMsg = "";
            uint iCardNo;
            CardData objCardData = null;
            Package.ECommandType eResponseType = Package.GetResponseType(bufFrame, out strErrMsg);
            switch (eResponseType)
            {
                case Package.ECommandType.WriteCard_Response:
                    bIfSucc = Package.ParseBag_WriteCard(bufFrame, out iCardNo, out strErrMsg);
                    _OnWriteCardReponse?.Invoke(iCardNo, strErrMsg);
                    bIfSucc = true;
                    break;
                case Package.ECommandType.ReadCard_Response:
                    bIfSucc = Package.ParseBag_ReadCard(bufFrame, ref objCardData, out strErrMsg);
                    _OnReadCardReponse?.Invoke(objCardData, strErrMsg);
                    bIfSucc = true;
                    break;
                case Package.ECommandType.ChsSelResponse:
                    bIfSucc = Package.ParseBag_ChsSel(bufFrame, ref objCardData, out strErrMsg);
                    _OnChsSelReponse?.Invoke(objCardData, strErrMsg);
                    bIfSucc = true;
                    break;
                case Package.ECommandType.ChsCleanResponse:
                    bIfSucc = Package.ParseBag_ChsClean(bufFrame, out iCardNo, out strErrMsg);
                    this._OnChsCleanReponse?.Invoke(iCardNo, strErrMsg);
                    bIfSucc = true;
                    break;
                default:
                    
                    break;
            }
            return bIfSucc;
        }

        protected override void OnErrorReceived(string strMessage)
        {
            DoNotice(string.Format("接收数据错误：{0}", strMessage));
        }
    }
}
