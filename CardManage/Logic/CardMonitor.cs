using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CardManage.Tools;
using CardManage.Model;

namespace CardManage.Logic
{
    /// <summary>
    /// 监控器
    /// </summary>
    class CardMonitor : ConfigerBase
    {
        /// <summary>
        /// 同步时间频率，单位为秒
        /// </summary>
        private int _SyncTimeSeconds = 600;
        /// <summary>
        /// 最后一次同步时间
        /// </summary>
        private DateTime _LastSyncTime;
        /// <summary>
        /// 记录需要发送的指令集合
        /// </summary>
        private System.Collections.ArrayList _RetryCommandList;
        /// <summary>
        /// 重发最大次数
        /// </summary>
        private readonly int _ReSendMaxTimes = 3;
        /// <summary>
        /// 重发时间频率，单位为秒
        /// </summary>
        private readonly int _ReSendSeconds = 1;
        /// <summary>
        /// 最后一次重发时间
        /// </summary>
        private DateTime _LastReSendTime;

        private bool _TaskLoop = false;
        //静态变量
        private static object _LockObject = new object();
        private static CardMonitor _Configer;


        private ReceiveSwipingCardHandler _OnReceiveSwipingCard = null;
        public delegate void ReceiveSwipingCardHandler(SwipingCardData objSwipingCardData, string strErrInfo);
        /// <summary>
        /// 当处理完刷卡指令时
        /// </summary>
        public event ReceiveSwipingCardHandler OnReceiveSwipingCard
        {
            add
            {
                this._OnReceiveSwipingCard += value;
            }
            remove
            {
                this._OnReceiveSwipingCard -= value;
            }
        }

        private SyncTimeHandler _OnSyncTime = null;
        public delegate void SyncTimeHandler(SwipingCardData objSwipingCardData, string strErrInfo);
        /// <summary>
        /// 当下发同步时间时
        /// </summary>
        public event SyncTimeHandler OnSyncTime
        {
            add
            {
                this._OnSyncTime += value;
            }
            remove
            {
                this._OnSyncTime -= value;
            }
        }

        private PingNoticeHandler _OnPingNotice = null;
        public delegate void PingNoticeHandler(string strNotice, byte[] bArrBuffer);
        /// <summary>
        /// 当有Ping设备时
        /// </summary>
        public event PingNoticeHandler OnPingNotice
        {
            add
            {
                this._OnPingNotice += value;
            }
            remove
            {
                this._OnPingNotice -= value;
            }
        }

        private PingResponseHandler _OnPingResponse = null;
        public delegate void PingResponseHandler(DeviceAddress objDeviceAddress, string strErrInfo);
        /// <summary>
        /// 当Ping设备响应时
        /// </summary>
        public event PingResponseHandler OnPingResponse
        {
            add
            {
                this._OnPingResponse += value;
            }
            remove
            {
                this._OnPingResponse -= value;
            }
        }

        private MainDeviceACK4PingResponse _OnMainDeviceACK4PingResponse = null;
        public delegate void MainDeviceACK4PingResponse(DeviceAddress objDeviceAddress, string strErrInfo);
        /// <summary>
        /// 当收到Ping设备指令响应时
        /// </summary>
        public event MainDeviceACK4PingResponse OnMainDeviceACK4PingResponse
        {
            add
            {
                this._OnMainDeviceACK4PingResponse += value;
            }
            remove
            {
                this._OnMainDeviceACK4PingResponse -= value;
            }
        }
        
        private CardMonitor()
            : base()
        {
            this._LastSyncTime = DateTime.Now;
            this._LastReSendTime = DateTime.Now;
            this._RetryCommandList = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList());
        }

        public static CardMonitor GetInstance()
        {
            if (_Configer == null)
            {
                lock (_LockObject)
                {
                    if (_Configer == null)
                    {
                        _Configer = new CardMonitor();
                    }
                }
            }
            return _Configer;
        }

        /// <summary>
        /// 设置同步时间频率
        /// </summary>
        /// <param name="iPL">同步频率,最小值必须为60，单位为秒</param>
        public void SetSyncTimePL(int iPL)
        {
            if (iPL >= 60)
            {
                this._SyncTimeSeconds = iPL;
            }
        }


        /// <summary>
        /// 启动定时任务
        /// </summary>
        public void StartTask(int i)
        {
            //1开，0停
            StopTask();
            if (i == 1)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(TaskProcess), null);
            }
        }

        /// <summary>
        /// 关闭定时任务
        /// </summary>
        public void StopTask()
        {
            this._TaskLoop = false;
        }


        /// <summary>
        /// 监听消息端口,该函数为线程函数
        /// </summary>
        private void TaskProcess(object state)
        {
            Thread.Sleep(500);
            this._TaskLoop = true;
            string strErrInfo = "";
            while (this._TaskLoop)
            {
                //1.同步时间
                TimeSpan tSpan = DateTime.Now - this._LastSyncTime;
                if (tSpan.TotalSeconds >= this._SyncTimeSeconds)
                {
                    SyncTime(DateTime.Now, out strErrInfo);
                    this._LastSyncTime = DateTime.Now;
                }

                //2.重发指令
                //ReSendCommand();
            }
        }
        
        /// <summary>
        /// 发送同步时间指令
        /// </summary>
        public bool SyncTime(DateTime dt, out string strErrInfo)
        {
            bool bIfSucc = false;
            strErrInfo = "";
            if (dt != null)
            {
                //发送指令
                byte[] bArrBag = Package.BuildBag(Package.ECommandType.SyncTime_Send, null, null, dt);
                bIfSucc = SendCommand(bArrBag, out strErrInfo);
                DoNotice(string.Format("发送同步时间指令{0}", (bIfSucc ? "成功" : "失败")), bArrBag);
            }
            return bIfSucc;
        }

        /// <summary>
        /// Ping设备
        /// </summary>
        public bool Ping(DeviceAddress objDeviceAddress, out string strErrInfo)
        {
            bool bIfSucc = false;
            strErrInfo = "";
            if (!(objDeviceAddress == null || string.IsNullOrEmpty(objDeviceAddress.DeviceNo)))
            {
                //发送指令
                byte[] bArrBag = Package.BuildBag(Package.ECommandType.Ping, null, null, null, objDeviceAddress);
                bIfSucc = SendCommand(bArrBag, out strErrInfo);
                _OnPingNotice?.Invoke(string.Format("发送Ping指令{0}", (bIfSucc ? "成功" : "失败")), bArrBag);
            }
            return bIfSucc;
        }

        /// <summary>
        /// 重发指令
        /// </summary>
        private void ReSendCommand()
        {
            if (!(this._RetryCommandList == null || this._RetryCommandList.Count == 0))
            {
                bool bIfSucc = false;
                TimeSpan tSpan = DateTime.Now - this._LastReSendTime;
                if (tSpan.TotalSeconds >= this._ReSendSeconds)
                {
                    try
                    {
                        for (int i = this._RetryCommandList.Count - 1; i >= 0; i--)
                        {
                            Model.RetryCommand objCommand = (Model.RetryCommand)this._RetryCommandList[i];
                            if (objCommand != null)
                            {
                                if (objCommand.TryTimes < this._ReSendMaxTimes)
                                {
                                    byte[] bArrBag = ScaleConverter.HexStr2ByteArr(objCommand.CommandHexStr);
                                    bIfSucc = SendCommand(bArrBag, out string strErrMsg);
                                    DoNotice(string.Format("{0}{1}(重发第{2}次)", "发送主机应答指令", (bIfSucc ? "成功" : "失败"), objCommand.TryTimes), bArrBag);
                                    objCommand.TryTimes += 1;
                                    if (objCommand.TryTimes >= this._ReSendMaxTimes)
                                    {
                                        this._RetryCommandList.RemoveAt(i);
                                    }
                                    else
                                    {
                                        this._RetryCommandList[i] = objCommand.Clone();
                                    }
                                }
                                else
                                {
                                    this._RetryCommandList.RemoveAt(i);
                                }
                            }
                            else
                            {
                                this._RetryCommandList.RemoveAt(i);
                            }
                        }
                        this._LastReSendTime = DateTime.Now;
                    }
                    catch (Exception err)
                    {
                        string strErr = err.Message;
                    }
                }
            }
        }

        protected override void OnReceiveFrameData(byte[] bufFrame)
        {
            if (bufFrame == null || bufFrame.Length == 0) return;
            bool bIfSucc = ProcessOne(bufFrame, out string strMainErrMsg);
            //抖动处理
            if (!bIfSucc && bufFrame.Length > 2)
            {
                byte[] bufFrameTry = new byte[bufFrame.Length - 1];
                //1.尝试去掉头一个字节
                Array.Copy(bufFrame, 1, bufFrameTry, 0, bufFrameTry.Length);
                bIfSucc = ProcessOne(bufFrameTry, out string strInnerErrMsg);
                if (!bIfSucc)
                {
                    //2.尝试去掉最后一个字节
                    Array.Copy(bufFrame, 0, bufFrameTry, 0, bufFrameTry.Length);
                    bIfSucc = ProcessOne(bufFrameTry, out strInnerErrMsg);
                }
            }

            if (!bIfSucc)
            {
                DoNotice(string.Format("接收数据错误，错误原因：{0}", strMainErrMsg), bufFrame);
            }
        }

        private bool ProcessOne(byte[] bufFrame, out string strErrMsg)
        {
            bool bIfSucc = false;
            strErrMsg = "";
            Package.ECommandType eResponseType = Package.GetResponseType(bufFrame, out strErrMsg);
            switch (eResponseType)
            {
                case Package.ECommandType.SwipingCard:
                    SwipingCardData objSwipingCardData = new SwipingCardData();
                    IList<string> listResponseData = new List<string>();
                    bIfSucc = Package.ParseBag_SwipingCard(bufFrame, ref objSwipingCardData, out strErrMsg);
                    if (bIfSucc)
                    {
                        SwipingCardData objSent = (objSwipingCardData != null) ? (SwipingCardData)objSwipingCardData.Clone() : null;
                        _OnReceiveSwipingCard?.Invoke(objSent, strErrMsg);

                        DoNotice(string.Format("接收刷卡数据正确"), bufFrame);

                        //保存记录
                        SaveSwipingCardData(objSent);

                        //响应
                        byte[] bArrTargetAddress = new byte[5];
                        byte[] bArrOrigialAddress = new byte[5];
                        Array.Copy(bufFrame, 5, bArrTargetAddress, 0, bArrTargetAddress.Length);
                        Array.Copy(bufFrame, 10, bArrOrigialAddress, 0, bArrOrigialAddress.Length);

                        //55 AA 00 0B 20 01 00 00 00 00 06 12 34 00 01 1E  主机链路层ACK
                        byte[] bArrBag = Package.BuildBag_MainDeviceACK(bArrTargetAddress, bArrOrigialAddress);
                        bIfSucc = SendCommand(bArrBag, out strErrMsg);
                        DoNotice(string.Format("{0}{1}", "发送主机链路层ACK指令", (bIfSucc ? "成功" : "失败")), bArrBag);

                        /*
                        Thread.Sleep(50);//停顿数50毫秒

                        //55 FF 00 0C 21 06 12 34 00 01 01 00 00 00 00 00 xx 主机应答
                        byte[] bArrBag2 = Package.BuildBag_MainDeviceResponse(bArrTargetAddress, bArrOrigialAddress);
                        bIfSucc = SendCommand(bArrBag2, out strErrMsg);
                        DoNotice(string.Format("{0}{1}", "发送主机应答指令", (bIfSucc ? "成功" : "失败")), bArrBag2);
                        if (!(bArrBag2 == null || bArrBag2.Length == 0))
                        {
                            this._LastReSendTime = DateTime.Now;
                            this._RetryCommandList.Add(new Model.RetryCommand(ScaleConverter.ByteArr2HexStr(bArrBag2).Replace(" ", ""), 1));
                        }
                        //55 AA 00 0B 21 06 12 34 00 01 01 00 00 00 00 XX 刷卡头链路层ACK
                        */

                        bIfSucc = true;
                    }
                    break;
                case Package.ECommandType.CardHeadDeviceACK:
                    try
                    {
                        if (!(this._RetryCommandList == null || this._RetryCommandList.Count == 0))
                        {
                            string strBuffHexStr = "";
                            string strCommandHexStr = "";
                            for (int i = this._RetryCommandList.Count - 1; i >= 0; i--)
                            {
                                Model.RetryCommand objCommand = (Model.RetryCommand)this._RetryCommandList[i];
                                if (objCommand != null)
                                {
                                    strCommandHexStr = objCommand.CommandHexStr.Substring(10, 20);
                                    strBuffHexStr = ScaleConverter.ByteArr2HexStr(bufFrame).Replace(" ", "").Substring(10, 20);
                                    if (string.Compare(strCommandHexStr, strBuffHexStr) == 0)
                                    {
                                        this._RetryCommandList.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        strErrMsg = err.Message;
                    }
                    DoNotice(string.Format("接收刷卡头链路层ACK指令"), bufFrame);
                    bIfSucc = true;
                    break;
                case Package.ECommandType.PingResponse:
                    DeviceAddress objDeviceAddress = new DeviceAddress();
                    bIfSucc = Package.ParseBag_PingResponse(bufFrame, ref objDeviceAddress, out strErrMsg);
                    if (bIfSucc)
                    {
                        DeviceAddress objSent = (objDeviceAddress != null) ? (DeviceAddress)objDeviceAddress.Clone() : null;
                        _OnPingResponse?.Invoke(objSent, strErrMsg);
                        //响应
                        byte[] bArrTargetAddress = new byte[5];
                        byte[] bArrOrigialAddress = new byte[5];
                        Array.Copy(bufFrame, 5, bArrTargetAddress, 0, bArrTargetAddress.Length);
                        Array.Copy(bufFrame, 10, bArrOrigialAddress, 0, bArrOrigialAddress.Length);

                        //Thread.Sleep(50);//停顿数50毫秒

                        //55 FF 00 0D 93 01 00 00 00 00 06 12 34 00 00 41 98 FE  从机PING应答
                        //55 AA 00 0B 93 01 00 00 00 00 06 12 34 00 00 09 AA   主机链路层应答
                        
                        byte[] bArrBag = Package.BuildBag_MainDeviceACK4Ping(bArrTargetAddress, bArrOrigialAddress);
                        bIfSucc = SendCommand(bArrBag, out strErrMsg);
                        //DoNotice(string.Format("{0}{1}", "发送主机链路层ACK指令", (bIfSucc ? "成功" : "失败")), bArrBag);

                        //_OnPingNotice?.Invoke(string.Format("发送Ping指令{0}", (bIfSucc ? "成功" : "失败")), bArrBag);
                        //if (_OnPingNotice != null) _OnPingNotice(string.Format("发送主机链路层ACK指令{0}", (bIfSucc ? "成功" : "失败")), bArrBag);   
                    }
                    break;
                case Package.ECommandType.MainDeviceACK4PingResponse:
                    DeviceAddress objDeviceAddress1 = new DeviceAddress();
                    bIfSucc = Package.ParseBag_MainDeviceACK4PingResponse(bufFrame, ref objDeviceAddress1, out strErrMsg);
                    if (bIfSucc)
                    {
                        DeviceAddress objSent = (objDeviceAddress1 != null) ? (DeviceAddress)objDeviceAddress1.Clone() : null;
                        _OnMainDeviceACK4PingResponse?.Invoke(objSent, strErrMsg);
                    }
                    break;
                default:
                    strErrMsg = "无效数据";
                    bIfSucc = false;
                    break;
            }
            return bIfSucc;
        }

        protected override void OnErrorReceived(string strMessage)
        {
            DoNotice(string.Format("接收错误：{0}", strMessage));
        }

        /// <summary>
        /// 保存刷卡记录
        /// </summary>
        /// <param name="objData">刷卡实体</param>
        private void SaveSwipingCardData(SwipingCardData objData)
        {
            if (objData != null)
            {
                try
                {
                    IDAL.ICardLog objDAL = DALFactory.DALFactory.CardLog();
                    int iNewID = objDAL.Add(objData, out string strErrInfo);
                    if (!string.IsNullOrEmpty(strErrInfo))
                    {
                        DoNotice(string.Format("保存刷卡记录失败，错误原因：\r\n{0}", strErrInfo));
                    }
                }
                catch(Exception err)
                {
                    DoNotice(string.Format("保存刷卡记录失败，错误原因：\r\n{0}", err.Message));
                }
            }
        }
    }
}