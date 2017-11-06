using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using CardManage.Tools;

namespace CardManage.Classes
{
    /// <summary>
    /// 串口管理器
    /// </summary>
    public class ComManager
    {
        //常量设置
        private const int ReadTimeout = -1;//设置超时读取时间
        private const int ReadBufferSize = 1024;//数据包最大长度
        private const int WriteBufferSize = 1024;
        private const int OutputMilliseconds = 100;//单位为：毫秒，多少毫秒没有接收到数据就输出内容(表示一贞结束)

        //运行变量
        private DateTime _LastSendTime;//最后一次发送指令时间
        private DateTime _LastReceiveTime;//最后一次接收时间
        private long _Received_count = 0;//接收字节计数
        private long _Send_count = 0;//发送字节计数
        private string _LastErrorInfo = "";
        private ComProperty _ComProperty;
        private bool _IsComListening = false;//是否没有执行完invoke相关操作
        private bool _IsComCloseing = false;//是否正在关闭串口

        //缓冲池相关
        private byte[] _BackBuffer;
        private int _BackBufferInsertIndex = 0;//缓冲池数据插入索引

        //静态变量
        private SerialPort _SerialPort;
        private object lockThis = new object();

        private ComOpenSuccHandler _OnComOpenSucc = null;
        public delegate void ComOpenSuccHandler();
        /// <summary>
        /// 当串口成功打开时
        /// </summary>
        public event ComOpenSuccHandler OnComOpenSucc
        {
            add
            {
                this._OnComOpenSucc += value;
            }
            remove
            {
                this._OnComOpenSucc -= value;
            }
        }

        private ComOpenFaultHandler _OnComOpenFault = null;
        public delegate void ComOpenFaultHandler(string strErrorMessage);
        /// <summary>
        /// 当串口打开失败时
        /// </summary>
        public event ComOpenFaultHandler OnComOpenFault
        {
            add
            {
                this._OnComOpenFault += value;
            }
            remove
            {
                this._OnComOpenFault -= value;
            }
        }

        private ComCloseSuccHandler _OnComCloseSucc = null;
        public delegate void ComCloseSuccHandler();
        /// <summary>
        /// 当串口成功关闭时
        /// </summary>
        public event ComCloseSuccHandler OnComCloseSucc
        {
            add
            {
                this._OnComCloseSucc += value;
            }
            remove
            {
                this._OnComCloseSucc -= value;
            }
        }

        private ComCloseFaultHandler _OnComCloseFault = null;
        public delegate void ComCloseFaultHandler(string strErrorMessage);
        /// <summary>
        /// 当串口关闭失败时
        /// </summary>
        public event ComCloseFaultHandler OnComCloseFault
        {
            add
            {
                this._OnComCloseFault += value;
            }
            remove
            {
                this._OnComCloseFault -= value;
            }
        }

        private ReceiveFrameDataHandler _OnReceiveFrameData = null;
        public delegate void ReceiveFrameDataHandler(byte[] bArr);
        /// <summary>
        /// 当串口管理器接收一贞数据时
        /// </summary>
        public event ReceiveFrameDataHandler OnReceiveFrameData
        {
            add
            {
                this._OnReceiveFrameData += value;
            }
            remove
            {
                this._OnReceiveFrameData -= value;
            }
        }

        private ErrorReceivedHandler _OnErrorReceived = null;
        public delegate void ErrorReceivedHandler(string strErrorMessage);
        /// <summary>
        /// 当串口管理器接收错误时
        /// </summary>
        public event ErrorReceivedHandler OnErrorReceived
        {
            add
            {
                this._OnErrorReceived += value;
            }
            remove
            {
                this._OnErrorReceived -= value;
            }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComManager()
        {
            this._BackBuffer = new byte[ReadBufferSize];
            this._LastSendTime = DateTime.Now;
            this._LastReceiveTime = DateTime.Now;

            this._SerialPort = new SerialPort();

            this._SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            this._SerialPort.ErrorReceived += new SerialErrorReceivedEventHandler(SerialPort_ErrorReceived);

            ThreadPool.SetMaxThreads(3, 3);
        }

        /// <summary>
        /// 释放资源，系统退出时需调用释放资源
        /// </summary>
        public void Free()
        {
            try
            {
                if (this._SerialPort != null && this._SerialPort.IsOpen)
                {
                    this._SerialPort.Close();
                }
            }
            finally
            {
                this._SerialPort.DataReceived -= new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                this._SerialPort.ErrorReceived -= new SerialErrorReceivedEventHandler(SerialPort_ErrorReceived);

                this._SerialPort = null;
            }
        }

        /// <summary>
        /// 打开Com
        /// </summary>
        /// <param name="objComProperty">串口属性对象</param>
        public void OpenCom(ComProperty objComProperty)
        {
            bool bIfSucc = false;
            bool bIfReCreateCom = false;
            if (CheckPortPropertyValid(objComProperty, out string strErrorMessage))
            {
                try
                {
                    if (this._SerialPort.IsOpen)
                    {
                        this._IsComCloseing = true;
                        while (this._IsComListening) Application.DoEvents();
                        this._SerialPort.Close();
                    }
                    SetComProperty(objComProperty);
                    this._SerialPort.Open();
                    this._IsComCloseing = false;
                    ClearBufferAndReset();
                    bIfSucc = true;
                    _OnComOpenSucc?.Invoke();
                }
                catch (InvalidOperationException)
                {
                    strErrorMessage = "指定的串口已打开";
                }
                catch (ArgumentOutOfRangeException)
                {
                    strErrorMessage = "此实例的一个或多个属性无效";
                }
                catch (ArgumentException)
                {
                    strErrorMessage = "串口名称不是以“COM”开始的- 或 -串口的文件类型不受支持";
                }
                catch (System.IO.IOException)
                {
                    strErrorMessage = "此串口处于无效状态- 或 -尝试设置基础串口状态失败";
                    bIfReCreateCom = true;
                }
                catch (UnauthorizedAccessException)
                {
                    strErrorMessage = "对串口的访问被拒绝";
                }
                catch (Exception err)
                {
                    strErrorMessage = err.Message;
                }

                //串口对象失效，重新创建新对象
                if (bIfReCreateCom)
                {
                    this._SerialPort = new SerialPort();
                    if (this._ComProperty != null)
                        SetComProperty((ComProperty)this._ComProperty.Clone());
                }
            }
            else
            {
                //如果当前配置不对，则关闭当前串口
                if (this._SerialPort.IsOpen)
                {
                    this._IsComCloseing = true;
                    while (this._IsComListening) Application.DoEvents();
                    this._SerialPort.Close();
                }
                this._ComProperty = null;
            }

            this._LastErrorInfo = strErrorMessage;
            if (!bIfSucc && this._OnComOpenFault != null) this._OnComOpenFault(strErrorMessage);
        }

        /// <summary>
        /// 关闭Com
        /// </summary>
        public void CloseCom()
        {
            try
            {
                if (this._SerialPort != null && this._SerialPort.IsOpen)
                {
                    this._IsComCloseing = true;
                    while (this._IsComListening) Application.DoEvents();
                    this._SerialPort.Close();
                }
                _OnComCloseSucc?.Invoke();
            }
            catch(Exception err)
            {
                _OnComCloseFault?.Invoke(err.Message);
            }
        }

        /// <summary>
        /// 向Com口发送数据包
        /// </summary>
        /// <param name="bArrBag">指令包</param>
        /// <returns>返回1:代表成功；2：串口已关闭；3：数据包为空</returns>
        public int SendData(byte[] bArrBag)
        {
            int iResult = 1;
            if (this._SerialPort != null && this._SerialPort.IsOpen)
            {
                if (!(bArrBag == null || bArrBag.Length == 0))
                {
                    lock(lockThis)
                    {
                        this._SerialPort.Write(bArrBag, 0, bArrBag.Length);
                    }
                    this._Send_count += bArrBag.Length;//增加发送字节计数
                    this._LastSendTime = DateTime.Now;//最后一次发送指令时间
                    iResult = 1;
                }
                else
                {
                    iResult = 3;
                }
            }
            else
            {
                _OnComCloseSucc?.Invoke();
                iResult = 2;
            }
            return iResult;
        }

        /// <summary>
        /// 测试串口的属性是否合法
        /// </summary>
        /// <param name="objPortProperty">属性</param>
        /// <param name="strErrorMessage">错误信息</param>
        /// <returns>是否合法</returns>
        private bool CheckPortPropertyValid(ComProperty objPortProperty,out string strErrorMessage)
        {
            bool bIfValid = false;
            strErrorMessage = "";

            if (objPortProperty != null)
            {
                if (objPortProperty.PortName.Equals(""))
                {
                    strErrorMessage = "串口名为空";
                    return false;
                }
                if (objPortProperty.BaudRate <= 0)
                {
                    strErrorMessage = "波特率错误";
                    return false;
                }
                if (objPortProperty.DataBits <= 0)
                {
                    strErrorMessage = "数据位错误";
                    return false;
                }
                //if (objPortProperty.Parity == null)
                //{
                //    strErrorMessage = "奇偶校验位错误";
                //    return false;
                //}
                if (objPortProperty.StopBits <= 0)
                {
                    strErrorMessage = "停止位错误";
                    return false;
                }
                bIfValid = true;
            }
            else
            {
                strErrorMessage = "串口的属性设置为Null";
            }
            return bIfValid;
        }

        /// <summary>
        /// 设置串口的属性
        /// </summary>
        /// <param name="objComProperty">串口属性对象</param>
        private void SetComProperty(ComProperty objComProperty)
        {
            this._SerialPort.PortName = objComProperty.PortName;//设置串口名
            this._SerialPort.BaudRate = objComProperty.BaudRate;//设置串口的波特率
            this._SerialPort.StopBits = objComProperty.StopBits;//设置停止位                
            this._SerialPort.DataBits = objComProperty.DataBits;//设置数据位
            this._SerialPort.Parity = objComProperty.Parity;//设置奇偶校验位 Parity.Even
            this._SerialPort.DtrEnable = objComProperty.DtrEnable; //如果是RS232转RS485,此句必须要，否则不能通讯
            this._SerialPort.RtsEnable = objComProperty.RtsEnable;
            this._SerialPort.ReadTimeout = ReadTimeout;//设置超时读取时间
            this._SerialPort.ReadBufferSize = ReadBufferSize;
            this._SerialPort.WriteBufferSize = WriteBufferSize;
            this._SerialPort.ReceivedBytesThreshold = 1;//触发事件的字节数
            this._SerialPort.Encoding = objComProperty.Encoding;
            //sp.NewLine = "\r\n";  

            this._ComProperty = (ComProperty)objComProperty.Clone();
        }

        /// <summary>
        /// 获得当前串口属性配置
        /// </summary>
        /// <returns></returns>
        public ComProperty GetComProperty()
        {
            return this._ComProperty;
        }

        /// <summary>
        /// 获得Com口状态，开启为True，关闭为False
        /// </summary>
        /// <returns></returns>
        public bool GetStatus()
        {
            return (this._SerialPort != null) ? this._SerialPort.IsOpen : false;
        }

        /// <summary>
        /// 获得Com总共接收到的字节数
        /// </summary>
        /// <returns></returns>
        public Int64 GetTotalReceivedByte()
        {
            return this._Received_count;
        }

        /// <summary>
        /// 获得Com总共发送的字节数
        /// </summary>
        /// <returns></returns>
        public Int64 GetTotalSendByte()
        {
            return this._Send_count;
        }

        /// <summary>
        /// 获得最后一次错误信息
        /// </summary>
        /// <returns></returns>
        public string GetLastErrorInfo()
        {
            return this._LastErrorInfo;
        }

        /// <summary>
        /// 获得最后一次发送命令时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastSendTime()
        {
            return this._LastSendTime;
        }

        /// <summary>
        /// 获得最后一次接收命令时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastReceiveTime()
        {
            return this._LastReceiveTime;
        }

        private int times = 0;
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (this._IsComCloseing) return;//如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环
            try
            {
                int n = _SerialPort.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                lock (lockThis)
                {
                    this._SerialPort.Read(buf, 0, buf.Length);//读取缓冲数据
                }
                this._Received_count += buf.Length;//增加接收字节计数
                this._LastReceiveTime = DateTime.Now;//更新最后一次接收数据的时间
                
                Array.Copy(buf, 0, this._BackBuffer, this._BackBufferInsertIndex, buf.Length);
                this._BackBufferInsertIndex += buf.Length;
                times++;
                int iDataLength = 0;
                while (_BackBufferInsertIndex > 4)
                {
                    //判断是否是正常包
                    if (_BackBuffer[0].Equals(0x55) && (_BackBuffer[1].Equals(0xFF) || _BackBuffer[1].Equals(0xAA)))
                    {
                        byte[] bArrDataLength = new byte[4];
                        bArrDataLength[0] = _BackBuffer[3];
                        bArrDataLength[1] = _BackBuffer[2];
                        iDataLength = Functions.ConverToInt(bArrDataLength);//低字节在前，高字节在后
                        iDataLength += 5;
                        //判断一帧数据是否接收完全
                        //未接收完全则等待下一个信号
                        if (iDataLength <= _BackBufferInsertIndex)
                        {
                            //处理包
                            byte[] bufFrame = new byte[iDataLength];
                            Array.Copy(_BackBuffer, bufFrame, iDataLength);
                            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcessData), ByteArr2HexStr(bufFrame));

                            //删除已处理包
                            Array.Clear(this._BackBuffer, 0, this._BackBuffer.Length);
                            _BackBufferInsertIndex = 0;
                        }
                        else
                        {
                            if(iDataLength > 128) //长度位出错
                            {
                                Array.Clear(this._BackBuffer, 0, this._BackBuffer.Length);
                                _BackBufferInsertIndex = 0;
                            }
                            return;
                        }
                    }
                    else
                    {
                        byte[] bufFrame = new byte[--_BackBufferInsertIndex];
                        Array.Copy(_BackBuffer, 1, bufFrame, 0, _BackBufferInsertIndex);
                        Array.Clear(this._BackBuffer, 0, this._BackBuffer.Length);
                        Array.Copy(bufFrame, _BackBuffer, _BackBufferInsertIndex);
                    }
                }
            }
            finally
            {
                this._IsComListening = false;//我用完了，ui可以关闭串口了。                
            }
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _OnErrorReceived?.Invoke(e.ToString());
        }
       

        /// <summary>
        /// 数据包处理，独立线程处理
        /// </summary>
        /// <param name="objBufferHexStr">16进制字符串对象</param>
        private void ProcessData(object objBufferHexStr)
        {
            _OnReceiveFrameData?.Invoke(HexStr2ByteArr(objBufferHexStr.ToString()));
        }

        /// <summary>
        /// 清空输入/出缓冲区并下标至零
        /// </summary>
        private void ClearBufferAndReset()
        {
            if (this._SerialPort.IsOpen)
            {
                this._SerialPort.DiscardInBuffer();//清空输入缓冲区
                this._SerialPort.DiscardOutBuffer();//清空输出缓冲区
            }
            this._BackBufferInsertIndex = 0;//一定要置零,代表该贞结束
            Array.Clear(this._BackBuffer, 0, this._BackBuffer.Length);
        }

        /// <summary>  
        /// 字节数组转16进制字符串  
        /// </summary>  
        /// <param name="bytes"></param>  
        /// <returns></returns>  
        private string ByteArr2HexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2") + " ";
                }
            }
            return returnStr;
        }

        /// <summary>  
        /// 16进制字符串转字节数组  
        /// </summary>  
        /// <param name="hexString"></param>  
        /// <returns></returns>  
        private byte[] HexStr2ByteArr(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length == 1) hexString = "0" + hexString;
            if ((hexString.Length % 2) != 0) hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }

    /// <summary>
    /// 串口属性
    /// </summary>
    public class ComProperty : ICloneable
    {
        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// 奇偶校验位
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// 是否启用Dtr
        /// </summary>
        public bool DtrEnable { get; set; }

        /// <summary>
        /// 是否启用Rts
        /// </summary>
        public bool RtsEnable { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
