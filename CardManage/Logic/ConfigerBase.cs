using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.Classes;
using CardManage.Model;
using CardManage.Tools;
namespace CardManage.Logic
{
    /// <summary>
    /// 配置器抽象基类
    /// </summary>
    public abstract class ConfigerBase
    {
        /// <summary>
        /// DC开关状态
        /// </summary>
        public enum ESwitch
        {
            CLOSE,
            OPEN            
        }

        protected ESwitch _ESwitch;

        //常量设置
        private const bool DtrEnable = true;
        private const bool RtsEnable = true;
        private readonly Encoding _CurrentEncoding = System.Text.Encoding.Default;//ASCIIEncoding.Default;
        
        //运行变量
        private ComManager _ComManager;
        
        /// <summary>
        /// 配置器开关状态
        /// </summary>
        /// <returns></returns>
        public ESwitch Switch
        {
            get
            {
                return _ESwitch;
            }
        }

        /// <summary>
        /// 串口信息
        /// </summary>
        public ComInfo ComInfo
        {
            get
            {
                ComInfo objComInfo = new ComInfo();
                objComInfo.IfOpen = this._ComManager.GetStatus();
                if (this._ComManager.GetComProperty() != null)
                {
                    objComInfo.ComProperty = (ComProperty)this._ComManager.GetComProperty().Clone();
                }
                objComInfo.TotalSendCount = this._ComManager.GetTotalSendByte();
                objComInfo.TotalReceivedCount = this._ComManager.GetTotalReceivedByte();
                objComInfo.LastSendTime = this._ComManager.GetLastSendTime();
                objComInfo.LastReceiveTime = this._ComManager.GetLastReceiveTime();
                objComInfo.LastErrorInfo = this._ComManager.GetLastErrorInfo();
                return objComInfo;
            }
        }

        private StartSuccHandler _OnStartSucc = null;
        public delegate void StartSuccHandler();
        /// <summary>
        /// 当成功打开时
        /// </summary>
        public event StartSuccHandler OnStartSucc
        {
            add
            {
                this._OnStartSucc += value;
            }
            remove
            {
                this._OnStartSucc -= value;
            }
        }

        private StartFaultHandler _OnStartFault = null;
        public delegate void StartFaultHandler(string strErrorMessage);
        /// <summary>
        /// 当打开失败时
        /// </summary>
        public event StartFaultHandler OnStartFault
        {
            add
            {
                this._OnStartFault += value;
            }
            remove
            {
                this._OnStartFault -= value;
            }
        }

        private StopSuccHandler _OnStopSucc = null;
        public delegate void StopSuccHandler();
        /// <summary>
        /// 当成功停止时
        /// </summary>
        public event StopSuccHandler OnStopSucc
        {
            add
            {
                this._OnStopSucc += value;
            }
            remove
            {
                this._OnStopSucc -= value;
            }
        }

        private StopFaultHandler _OnStopFault = null;
        public delegate void StopFaultHandler(string strErrorMessage);
        /// <summary>
        /// 当停止失败时
        /// </summary>
        public event StopFaultHandler OnStopFault
        {
            add
            {
                this._OnStopFault += value;
            }
            remove
            {
                this._OnStopFault -= value;
            }
        }

        private NoticeHandler _OnNotice = null;
        public delegate void NoticeHandler(string strNotice, byte[] bArrBuffer);
        /// <summary>
        /// 当有处理信息时
        /// </summary>
        public event NoticeHandler OnNotice
        {
            add
            {
                this._OnNotice += value;
            }
            remove
            {
                this._OnNotice -= value;
            }
        }
        
        /// <summary>
        /// 私有构造函数
        /// </summary>
        protected ConfigerBase()
        {
            this._ComManager = new ComManager();
            this._ComManager.OnReceiveFrameData += new ComManager.ReceiveFrameDataHandler(OnReceiveFrameData);
            this._ComManager.OnErrorReceived += new ComManager.ErrorReceivedHandler(OnErrorReceived);
            this._ComManager.OnComOpenSucc += new ComManager.ComOpenSuccHandler(OnComOpenSucc);
            this._ComManager.OnComOpenFault += new ComManager.ComOpenFaultHandler(OnComOpenFault);
            this._ComManager.OnComCloseSucc += new ComManager.ComCloseSuccHandler(OnComCloseSucc);
            this._ComManager.OnComCloseFault += new ComManager.ComCloseFaultHandler(OnComCloseFault);

            this._ESwitch = ESwitch.CLOSE;
        }

        ~ConfigerBase()
        {
            //释放资源，系统退出时需调用释放资源
            try
            {
                this._ComManager.OnReceiveFrameData -= new ComManager.ReceiveFrameDataHandler(OnReceiveFrameData);
                this._ComManager.OnErrorReceived -= new ComManager.ErrorReceivedHandler(OnErrorReceived);
                this._ComManager.OnComOpenSucc -= new ComManager.ComOpenSuccHandler(OnComOpenSucc);
                this._ComManager.OnComOpenFault -= new ComManager.ComOpenFaultHandler(OnComOpenFault);
                this._ComManager.OnComCloseSucc -= new ComManager.ComCloseSuccHandler(OnComCloseSucc);
                this._ComManager.OnComCloseFault -= new ComManager.ComCloseFaultHandler(OnComCloseFault);
                this._ComManager.Free();
            }
            finally
            {

            }
        }

        /// <summary>
        /// 开启
        /// </summary>
        /// <param name="objPortProperty">串口配置</param>
        public void Start(ComProperty objPortProperty)
        {
            if (objPortProperty != null)
            {
                objPortProperty.Encoding = this._CurrentEncoding;
                objPortProperty.DtrEnable = DtrEnable;
                objPortProperty.RtsEnable = RtsEnable;
            }
            this._ComManager.OpenCom(objPortProperty);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Stop()
        {
            this._ComManager.CloseCom();
        }

        private void OnComOpenSucc()
        {
            if (this._ComManager.GetComProperty() != null)
            {
                ComProperty objComProperty = (ComProperty)this._ComManager.GetComProperty().Clone();
            }
            DoStartSucc();
        }

        private void OnComOpenFault(string strErrorMessage)
        {
            _ESwitch = ESwitch.CLOSE;
            if (this._OnStartFault != null) this._OnStartFault(strErrorMessage);
        }

        private void OnComCloseSucc()
        {
            ComProperty objComP = this._ComManager.GetComProperty();
            DoNotice((objComP != null) ? string.Format("串口({0})已关闭", objComP.PortName) : string.Format("串口已关闭"));
            DoStopSucc();
        }

        private void OnComCloseFault(string strErrorMessage)
        {
            if (this._OnStopFault != null) this._OnStopFault(strErrorMessage);
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="bArrBag">数据包</param>
        /// <param name="strErrInfo">错误信息</param>
        /// <returns>是否发送成功</returns>
        protected bool SendCommand(byte[] bArrBag, out string strErrInfo)
        {
            bool bIfSucc = false;
            strErrInfo = "";
            int iResult = this._ComManager.SendData(bArrBag);
            switch (iResult)
            {
                case 1:
                    bIfSucc = true;
                    break;
                case 2:
                    strErrInfo = "串口已关闭";
                    DoStopSucc();
                    break;
                case 3:
                    strErrInfo = "数据包为空";
                    break;
            }
            return bIfSucc;
        }

        protected void DoStartSucc()
        {
            _ESwitch = ESwitch.OPEN;
            if (this._OnStartSucc != null) this._OnStartSucc();
        }

        protected void DoStopSucc()
        {
            _ESwitch = ESwitch.CLOSE;
            if (this._OnStopSucc != null) this._OnStopSucc();
        }

        protected void DoNotice(string strNotice, byte[] bArrBuffer = null)
        {
            if (this._OnNotice != null) this._OnNotice(strNotice, bArrBuffer);
        }

        /// <summary>
        /// 收到数据包
        /// </summary>
        /// <param name="bufFrame"></param>
        protected abstract void OnReceiveFrameData(byte[] bufFrame);

        /// <summary>
        /// 收到错误的数据包
        /// </summary>
        /// <param name="strMessage"></param>
        protected abstract void OnErrorReceived(string strMessage);
    }
}
