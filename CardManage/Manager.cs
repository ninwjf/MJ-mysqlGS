using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardManage.Tools;

namespace CardManage
{
    /// <summary>
    /// 总管理器
    /// </summary>
    public class Manager
    {
        private readonly string _TempFileName = "";
        private readonly string _LimitFileName = "";
        //静态变量
        private static object _LockObject = new object();
        private static Manager _Manager;


        private SettingChangeHandler _OnSettingChange = null;
        public delegate void SettingChangeHandler();
        /// <summary>
        /// 当系统配置改变时
        /// </summary>
        public event SettingChangeHandler OnSettingChange
        {
            add
            {
                this._OnSettingChange += value;
            }
            remove
            {
                this._OnSettingChange -= value;
            }
        }

        private BuildingDataChangeHandler _OnBuildingDataChange = null;
        public delegate void BuildingDataChangeHandler();
        /// <summary>
        /// 当建筑信息改变时
        /// </summary>
        public event BuildingDataChangeHandler OnBuildingDataChange
        {
            add
            {
                this._OnBuildingDataChange += value;
            }
            remove
            {
                this._OnBuildingDataChange -= value;
            }
        }

        private CardDataChangeHandler _OnCardDataChange = null;
        public delegate void CardDataChangeHandler();
        /// <summary>
        /// 当卡片信息改变时
        /// </summary>
        public event CardDataChangeHandler OnCardDataChange
        {
            add
            {
                this._OnCardDataChange += value;
            }
            remove
            {
                this._OnCardDataChange -= value;
            }
        }

        private Manager()
        {
            this._TempFileName = string.Format("{0}\\tmp{1}.tmp", Environment.GetEnvironmentVariable("TEMP"), Encrypt.EncryptDES(Config.SoftVersion, Config.EncryptKey));
            this._LimitFileName = string.Format("{0}\\{1}.tmp", Environment.GetEnvironmentVariable("TEMP"), Encrypt.EncryptDES(Config.SoftVersion, Config.EncryptKey));
        }

        public static Manager GetInstance()
        {
            if (_Manager == null)
            {
                lock (_LockObject)
                {
                    if (_Manager == null)
                    {
                        _Manager = new Manager();
                    }
                }
            }
            return _Manager;
        }

        /// <summary>
        /// 建筑信息改变时通知
        /// </summary>
        public void BuildingDataChangeNotice()
        {
            try
            {
                _OnBuildingDataChange?.Invoke();
            }
            catch(Exception err)
            {
                Tools.CMessageBox.ShowError(string.Format("建筑信息改变通知处理错误，错误信息：\r\n{0}", err.Message), Config.DialogTitle);
            }
        }

        /// <summary>
        /// 卡片信息改变时通知
        /// </summary>
        public void CardDataChangeNotice()
        {
            try
            {
                _OnCardDataChange?.Invoke();
            }
            catch (Exception err)
            {
                Tools.CMessageBox.ShowError(string.Format("卡片信息改变通知处理错误，错误信息：\r\n{0}", err.Message), Config.DialogTitle);
            }
        }

        /// <summary>
        /// 设置改变时通知
        /// </summary>
        public void SettingChangeNotice()
        {
            try
            {
                _OnSettingChange?.Invoke();
            }
            catch (Exception err)
            {
                Tools.CMessageBox.ShowError(string.Format("设置改变通知处理错误，错误信息：\r\n{0}", err.Message), Config.DialogTitle);
            }
        }

        /// <summary>
        /// 保存用户名到临时文件
        /// </summary>
        /// <param name="strUserName"></param>
        public void SaveUserName(string strUserName)
        {
            if (!string.IsNullOrEmpty(strUserName))
            {
                try
                {
                    if (Tools.IOHelper.Exists(this._TempFileName)) Tools.IOHelper.DeleteFile(this._TempFileName);
                    Tools.IOHelper.WriteLine(this._TempFileName, Encrypt.EncryptDES(strUserName, Config.EncryptKey));                    
                }
                catch(Exception)
                {

                }
            }
        }

        /// <summary>
        /// 从临时文件中取得用户名
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            string strRtn = "";
            try
            {
                if (Tools.IOHelper.Exists(this._TempFileName))
                {
                    strRtn = Tools.IOHelper.ReadLine(this._TempFileName);
                    if (string.IsNullOrEmpty(strRtn))
                        strRtn = "";
                    else
                        strRtn = Encrypt.DecryptDES(strRtn, Config.EncryptKey);
                }
            }
            catch (Exception)
            {

            }
            return strRtn;
        }

        /// <summary>
        /// 判断软件是否过期
        /// </summary>
        /// <returns></returns>
        public bool CheckSoftIfExpiry()
        {
            bool bIfExpiry = true;
            try
            {
                DateTime dtNow = DateTime.Now;
                DateTime dtBegin = Convert.ToDateTime("2014-09-10 00:00:01");
                //DateTime dtEnd = Convert.ToDateTime("2014-09-11 23:59:59");
                //DateTime dtEnd = Convert.ToDateTime("2999-10-15 23:59:59");
                //DateTime dtEnd = DateTime.MinValue;
                DateTime dtEnd = DateTime.MaxValue;
                if (Tools.IOHelper.Exists(this._LimitFileName))
                {
                    //第一次使用时间
                    string strRtn = Tools.IOHelper.ReadLine(this._LimitFileName);
                    string strContent = Encrypt.DecryptDES(strRtn, Config.EncryptKey);
                    DateTime dtBeginLocal = Functions.ConvertToNormalTime(Convert.ToInt32(strContent));
                    if (!dtBeginLocal.ToString("yyyy-MM-dd").Equals(dtNow.ToString("yyyy-MM-dd")))
                    {
                        dtBeginLocal = dtBeginLocal.AddDays(1);
                    }
                    if (dtBeginLocal <= dtEnd)
                    {
                        if (Tools.IOHelper.Exists(this._LimitFileName)) Tools.IOHelper.DeleteFile(this._LimitFileName);
                        Tools.IOHelper.WriteLine(this._LimitFileName, Encrypt.EncryptDES(Functions.ConvertToUnixTime(dtBeginLocal).ToString(), Config.EncryptKey));
                        bIfExpiry = false;
                    }
                }
                else
                {                    
                    if (dtNow > dtBegin && dtNow < dtEnd)
                    {
                        dtBegin = dtNow;
                    }
                    if (Tools.IOHelper.Exists(this._LimitFileName)) Tools.IOHelper.DeleteFile(this._LimitFileName);
                    Tools.IOHelper.WriteLine(this._LimitFileName, Encrypt.EncryptDES(Functions.ConvertToUnixTime(dtBegin).ToString(), Config.EncryptKey));
                    bIfExpiry = false;
                }
            }
            catch(Exception)
            {
                
            }
            return bIfExpiry;
        }
    }
}
