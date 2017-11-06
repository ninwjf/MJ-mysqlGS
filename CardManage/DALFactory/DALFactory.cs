using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardManage.IDAL;
using CardManage.SQLServerDAL;
namespace CardManage.DALFactory
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class DALFactory
    {
        #region 用户
        private static IUserInfo dalUserInfo = null;
        private static readonly object lockUserInfo = new object();
        public static IUserInfo UserInfo()
        {
            if (dalUserInfo == null)
            {
                lock (lockUserInfo)//考虑多线程安全
                {
                    if (dalUserInfo == null)
                    {
                        dalUserInfo = new UserInfo();
                    }
                }
            }
            return dalUserInfo;
        }
        #endregion

        #region 建筑
        private static IBuilding dalBuilding = null;
        private static readonly object lockBuilding = new object();
        public static IBuilding Building()
        {
            if (dalBuilding == null)
            {
                lock (lockBuilding)//考虑多线程安全
                {
                    if (dalBuilding == null)
                    {
                        dalBuilding = new Building();
                    }
                }
            }
            return dalBuilding;
        }
        #endregion

        #region 卡片
        private static ICard dalCard = null;
        private static readonly object lockCard = new object();
        public static ICard Card()
        {
            if (dalCard == null)
            {
                lock (lockCard)//考虑多线程安全
                {
                    if (dalCard == null)
                    {
                        dalCard = new Card();
                    }
                }
            }
            return dalCard;
        }
        #endregion

        #region 刷卡日志
        private static ICardLog dalCardLog = null;
        private static readonly object lockCardLog = new object();
        public static ICardLog CardLog()
        {
            if (dalCardLog == null)
            {
                lock (lockCardLog)//考虑多线程安全
                {
                    if (dalCardLog == null)
                    {
                        dalCardLog = new CardLog();
                    }
                }
            }
            return dalCardLog;
        }
        #endregion

        #region 系统
        private static ISys dalSys = null;
        private static readonly object lockSys = new object();
        public static ISys Sys()
        {
            if (dalSys == null)
            {
                lock (lockSys)//考虑多线程安全
                {
                    if (dalSys == null)
                    {
                        dalSys = new Sys();
                    }
                }
            }
            return dalSys;
        }
        #endregion
    }
}
