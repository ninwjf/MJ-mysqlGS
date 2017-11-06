using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

using CardManage.Tools;
namespace CardManage.Logic
{
    public class SysConfiger
    {
        private class KeySet
        {
            public const string SectionName_DB = "DB";
            public const string SectionName_COM1 = "COM1";
            public const string SectionName_COM2 = "COM2";
            public const string SectionName_OTHER = "OTHER";

            public const string DB_IP = "DB_IP";
            public const string DB_Name = "DB_Name";
            public const string DB_User = "DB_User";
            public const string DB_Password = "DB_Password";

            public const string PortName = "PortName";
            public const string BaudRate = "BaudRate";
            public const string StopBits = "StopBits";
            public const string DataBits = "DataBits";
            public const string Parity = "Parity";

            public const string SyncTimePL = "SyncTimePL";
            public const string SyncAuto = "SyncAuto";
            public const string Chs = "Chs";
        }

        /// <summary>
        /// 测试配置是否正确
        /// </summary>
        /// <param name="objSetting">配置对象</param>
        /// <param name="strErrInfo">错误信息</param>
        /// <returns></returns>
        public static bool CheckSettings(Model.Setting objSetting, out string strErrInfo)
        {
            bool bIfOk = false;
            strErrInfo = "";

            //1.数据库验证
            //地址
            if (string.IsNullOrEmpty(objSetting.DBSetting.DB_IP) || objSetting.DBSetting.DB_IP.Equals(""))
            {
                strErrInfo += "数据库地址为空！\r\n";
            }
            //名称
            if (string.IsNullOrEmpty(objSetting.DBSetting.DB_Name) || objSetting.DBSetting.DB_Name.Equals(""))
            {
                strErrInfo += "数据库名为空！\r\n";
            }
            //用户名
            if (string.IsNullOrEmpty(objSetting.DBSetting.DB_User) || objSetting.DBSetting.DB_User.Equals(""))
            {
                strErrInfo += "数据库用户名为空！\r\n";
            }

            double[] arrBaudRate = { 300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 43000, 56000, 57600, 115200 };
            double[] arrDataBits = { 5, 6, 7, 8 };
            //2.写卡COM验证
            //串口名
            //if (string.IsNullOrEmpty(objSetting.WriteComProperty.PortName) || objSetting.WriteComProperty.PortName.Equals(""))
            //{
            //    strErrInfo += "制卡COM的串口名为空！\r\n";
            //}
            //串口波特率
            if (Functions.IsInArr(arrBaudRate, objSetting.WriteComProperty.BaudRate))
            {
                strErrInfo += "制卡COM的波特率不在范围内，取值范围为：300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 43000, 56000, 57600, 115200！\r\n";
            }
            //串口数据位
            if (Functions.IsInArr(arrDataBits, objSetting.WriteComProperty.DataBits))
            {
                strErrInfo += "制卡COM的数据位不在范围内，取值范围为：5, 6, 7, 8！\r\n";
            }
            
            //3.监控COM验证
            //串口名
            //if (string.IsNullOrEmpty(objSetting.MonitorComProperty.PortName) || objSetting.MonitorComProperty.PortName.Equals(""))
            //{
            //    strErrInfo += "监控COM的串口名为空！\r\n";
            //}
            //串口波特率
            if (Functions.IsInArr(arrBaudRate, objSetting.MonitorComProperty.BaudRate))
            {
                strErrInfo += "监控COM的波特率不在范围内，取值范围为：300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 43000, 56000, 57600, 115200！\r\n";
            }
            //串口数据位
            if (Functions.IsInArr(arrDataBits, objSetting.MonitorComProperty.DataBits))
            {
                strErrInfo += "监控COM的数据位不在范围内，取值范围为：5, 6, 7, 8！\r\n";
            }

            if (strErrInfo.Equals(""))
            {
                if(!string.IsNullOrEmpty(objSetting.WriteComProperty.PortName) && objSetting.WriteComProperty.PortName.Equals(objSetting.MonitorComProperty.PortName))
                    strErrInfo += "制卡COM和监控COM不能用同一个串口！\r\n";
            }

            //4.同步时间频率验证
            if (!(objSetting.OtherSetting.SyncTimePL >= 1 && objSetting.OtherSetting.SyncTimePL <= 9999))
            {
                strErrInfo += "时间同步频率值不在1~9999范围内！\r\n";
            }

            //5.扇区验证
            if (!(objSetting.OtherSetting.Chs >= 1 && objSetting.OtherSetting.Chs <= 15))
            {
                strErrInfo += "扇区不在1~15范围内！\r\n";
            }

            if (strErrInfo.Equals("")) bIfOk = true;
            return bIfOk;
        }

        /// <summary>
        /// 载入配置
        /// </summary>
        /// <param name="strIniFilePath">Ini配置文件路径</param>
        /// <param name="strErrInfo">操作提示</param>
        /// <returns>返回配置对象，失败返回Null</returns>
        public static Model.Setting LoadSetting(string strIniFilePath, out string strErrInfo)
        {
            Model.Setting objSetting = null;
            strErrInfo = "";
            if (IOHelper.Exists(strIniFilePath))
            {
                IniFile ini = new IniFile(strIniFilePath);
                try
                {
                    objSetting = new Model.Setting();
                    //数据库
                    objSetting.DBSetting = new Model.DBSetting();
                    objSetting.DBSetting.DB_IP = Functions.FormatString(ini.IniReadValue(KeySet.SectionName_DB, KeySet.DB_IP));
                    objSetting.DBSetting.DB_Name = Functions.FormatString(ini.IniReadValue(KeySet.SectionName_DB, KeySet.DB_Name));
                    objSetting.DBSetting.DB_User = Functions.FormatString(ini.IniReadValue(KeySet.SectionName_DB, KeySet.DB_User));
                    objSetting.DBSetting.DB_Password = Functions.FormatString(ini.IniReadValue(KeySet.SectionName_DB, KeySet.DB_Password));

                    int iParity = 0;
                    //Com1
                    objSetting.WriteComProperty = new Classes.ComProperty();
                    objSetting.WriteComProperty.PortName = Functions.FormatString(ini.IniReadValue(KeySet.SectionName_COM1, KeySet.PortName));
                    objSetting.WriteComProperty.BaudRate = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_COM1, KeySet.BaudRate));
                    objSetting.WriteComProperty.DataBits = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_COM1, KeySet.DataBits));
                    objSetting.WriteComProperty.StopBits = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_COM1, KeySet.StopBits)).Equals(1) ? StopBits.One : StopBits.Two;
                    iParity = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_COM1, KeySet.Parity));
                    objSetting.WriteComProperty.Parity = iParity.Equals(0) ? Parity.None : (iParity.Equals(1) ? Parity.Odd : Parity.Even);

                    //Com2
                    objSetting.MonitorComProperty = new Classes.ComProperty();
                    objSetting.MonitorComProperty.PortName = Functions.FormatString(ini.IniReadValue(KeySet.SectionName_COM2, KeySet.PortName));
                    objSetting.MonitorComProperty.BaudRate = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_COM2, KeySet.BaudRate));
                    objSetting.MonitorComProperty.DataBits = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_COM2, KeySet.DataBits));
                    objSetting.MonitorComProperty.StopBits = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_COM2, KeySet.StopBits)).Equals(1) ? System.IO.Ports.StopBits.One : System.IO.Ports.StopBits.Two;
                    iParity = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_COM2, KeySet.Parity));
                    objSetting.MonitorComProperty.Parity = iParity.Equals(0) ? Parity.None : (iParity.Equals(1) ? Parity.Odd : Parity.Even);

                    //其它配置

                    objSetting.OtherSetting = new Model.OtherSetting();
                    objSetting.OtherSetting.SyncAuto = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_OTHER, KeySet.SyncAuto));
                    objSetting.OtherSetting.SyncTimePL = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_OTHER, KeySet.SyncTimePL));
                    if (objSetting.OtherSetting.SyncTimePL < 1 || objSetting.OtherSetting.SyncTimePL > 9999) objSetting.OtherSetting.SyncTimePL = 10;

                    objSetting.OtherSetting.Chs = Functions.FormatInt(ini.IniReadValue(KeySet.SectionName_OTHER, KeySet.Chs));
                    if (objSetting.OtherSetting.Chs < 1 || objSetting.OtherSetting.Chs > 15) objSetting.OtherSetting.Chs = 1;
                }
                catch (Exception err)
                {
                    objSetting = null;
                    strErrInfo = err.Message.ToString();
                }
                finally
                {
                    ini = null;
                }
            }
            else
            {
                strErrInfo = string.Format("{0}文件不存在，所以加载失败", strIniFilePath);
            }
            return objSetting;
        }

        /// <summary>
        /// 保存配置文件到指定目录
        /// </summary>
        /// <param name="objSetting">配置对象</param>
        /// <param name="strIniFilePath">Ini配置文件路径</param>
        /// <param name="strErrInfo">操作提示</param>
        /// <returns></returns>
        public static bool SaveSetting(Model.Setting objSetting, string strIniFilePath, out string strErrInfo)
        {
            bool bIfSucc = false;
            strErrInfo = "";
            try
            {
                if (!IOHelper.Exists(strIniFilePath)) IOHelper.CreateFile(strIniFilePath);
                if (IOHelper.Exists(strIniFilePath))
                {
                    IniFile ini = new IniFile(strIniFilePath);
                    ini.IniWriteValue(null, null, null);//删除ini文件下所有段落
                    //数据库
                    ini.IniWriteValue(KeySet.SectionName_DB, KeySet.DB_IP, objSetting.DBSetting.DB_IP);
                    ini.IniWriteValue(KeySet.SectionName_DB, KeySet.DB_Name, objSetting.DBSetting.DB_Name);
                    ini.IniWriteValue(KeySet.SectionName_DB, KeySet.DB_User, objSetting.DBSetting.DB_User);
                    ini.IniWriteValue(KeySet.SectionName_DB, KeySet.DB_Password, objSetting.DBSetting.DB_Password);

                    //Com1
                    ini.IniWriteValue(KeySet.SectionName_COM1, KeySet.PortName, objSetting.WriteComProperty.PortName);
                    ini.IniWriteValue(KeySet.SectionName_COM1, KeySet.BaudRate, objSetting.WriteComProperty.BaudRate.ToString());
                    ini.IniWriteValue(KeySet.SectionName_COM1, KeySet.DataBits, objSetting.WriteComProperty.DataBits.ToString());
                    ini.IniWriteValue(KeySet.SectionName_COM1, KeySet.StopBits, (objSetting.WriteComProperty.StopBits.Equals(StopBits.One) ? "1" : "2"));
                    ini.IniWriteValue(KeySet.SectionName_COM1, KeySet.Parity, (objSetting.WriteComProperty.Parity.Equals(Parity.None) ? "0" : (objSetting.WriteComProperty.Parity.Equals(Parity.Odd) ? "1" : "2")));

                    //Com2
                    ini.IniWriteValue(KeySet.SectionName_COM2, KeySet.PortName, objSetting.MonitorComProperty.PortName);
                    ini.IniWriteValue(KeySet.SectionName_COM2, KeySet.BaudRate, objSetting.MonitorComProperty.BaudRate.ToString());
                    ini.IniWriteValue(KeySet.SectionName_COM2, KeySet.DataBits, objSetting.MonitorComProperty.DataBits.ToString());
                    ini.IniWriteValue(KeySet.SectionName_COM2, KeySet.StopBits, (objSetting.MonitorComProperty.StopBits.Equals(StopBits.One) ? "1" : "2"));
                    ini.IniWriteValue(KeySet.SectionName_COM2, KeySet.Parity, (objSetting.MonitorComProperty.Parity.Equals(Parity.None) ? "0" : (objSetting.MonitorComProperty.Parity.Equals(Parity.Odd) ? "1" : "2")));

                    //其它配置
                    ini.IniWriteValue(KeySet.SectionName_OTHER, KeySet.SyncTimePL, objSetting.OtherSetting.SyncTimePL.ToString());
                    ini.IniWriteValue(KeySet.SectionName_OTHER, KeySet.SyncAuto, objSetting.OtherSetting.SyncAuto.ToString());
                    ini.IniWriteValue(KeySet.SectionName_OTHER, KeySet.Chs, objSetting.OtherSetting.Chs.ToString());
                    ini = null;

                    bIfSucc = true;
                }
                else
                {
                    strErrInfo = string.Format("{0}文件不存在，也无法自动创建", strIniFilePath);
                }
            }
            catch (Exception err)
            {
                strErrInfo = err.Message.ToString();
            }
            return bIfSucc;
        }
    }
}
