using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace CardManage.Tools
{
    public abstract class Functions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsCorrectIP(string ip)
        {
            bool bIfCorrect = false;
            try
            {
                string pattrn = @"(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])";
                if (System.Text.RegularExpressions.Regex.IsMatch(ip, pattrn))
                {
                    char[] separator = { '.' };
                    String[] splitstrings = new string[100];
                    splitstrings = ip.Split(separator);
                    if (splitstrings.Length == 4)
                    {
                        int iFirst = Convert.ToInt32(splitstrings[0]);
                        int iSecond = Convert.ToInt32(splitstrings[1]);
                        int iThird = Convert.ToInt32(splitstrings[2]);
                        int iFour = Convert.ToInt32(splitstrings[3]);
                        if (!(iFirst == 0 && iSecond == 0 && iThird == 0 && iFour == 0 || (iFirst >= 255 || iSecond >= 255 || iThird >= 255 || iFour >= 255)))
                        {
                            bIfCorrect = true;
                        }
                    }
                }
            }
            catch
            {

            }
            return bIfCorrect;
        }

        /// <summary>
        /// 判断子网掩码合法性代码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCorrectMask(string str)
        {
            string pattrn = "(254|252|248|240|224|192|128|0)\\.0\\.0\\.0|" + "255\\.(254|252|248|240|224|192|128|0)\\.0\\.0|" + "255\\.255\\.(254|252|248|240|224|192|128|0)\\.0|" + "255\\.255\\.255\\.(254|252|248|240|224|192|128|0)";
            return System.Text.RegularExpressions.Regex.IsMatch(str, pattrn);
        }

        /// <summary>  
        /// 判断输入的字符串是否是一个超链接  
        /// </summary>  
        /// <param name="input"></param>  
        /// <returns></returns>  
        public static bool IsURL2(string input)
        {
            string pattern = @"^[a-zA-Z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串是否是一个超链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsURL(string input)
        {
            //bool bIfOk = Regex.IsMatch(input, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&%_\./-~-]*)?$", RegexOptions.IgnoreCase);
            bool bIfOk = Regex.IsMatch(input, @"(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&%_\./-~-]*)?$", RegexOptions.IgnoreCase);
            if (bIfOk)
            {
                if ((input.ToLower().IndexOf(":") >= 0 || input.ToLower().IndexOf("/") >= 0) && input.ToLower().IndexOf("http:") < 0) bIfOk = false;
            }
            return bIfOk;
        }



        /// <summary>
        /// 判断是否是Mac地址
        /// </summary>
        /// <param name="strMacAddress"></param>
        /// <returns></returns>
        public static bool IsMacAddress(string strMacAddress)
        {
            bool bIfOk = false;
            if (!(string.IsNullOrEmpty(strMacAddress) || strMacAddress.Equals("")))
            {
                string[] arrMacAddress = strMacAddress.Split(':');
                if (arrMacAddress.Length == 6)
                {
                    bIfOk = true;
                    foreach (string strItem in arrMacAddress)
                    {
                        if (!IsHexStr(strItem))
                        {
                            bIfOk = false;
                            break;
                        }
                    }
                }
            }
            return bIfOk;
        }

        /// <summary>  
        /// 判断是否是16进制字符串
        /// </summary>  
        /// <param name="hexString"></param>  
        /// <returns></returns>  
        public static bool IsHexStr(string hexString)
        {
            bool bIfOk = false;
            try
            {
                if (!(string.IsNullOrEmpty(hexString) || hexString.Equals("")))
                {
                    if (hexString.Length == 2)
                    {
                        byte returnByte = Convert.ToByte(hexString, 16);
                        bIfOk = true;
                    }
                }
            }
            catch
            {

            }
            return bIfOk;
        }

        /// <summary>
        /// 将unix时间戳转换为一般时间格式
        /// </summary>
        /// <param name="iUnixTime">unix时间戳</param>
        /// <returns></returns>
        public static DateTime ConvertToNormalTime(long iUnixTime)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            //TimeSpan toNow = new TimeSpan(iUnixTime);
            DateTime dtResult = dtStart.AddSeconds(iUnixTime);
            return dtResult;
        }

        /// <summary>
        /// 将时间转换为unix时间戳
        /// </summary>
        /// <param name="iUnixTime">unix时间戳</param>
        /// <returns></returns>
        public static long ConvertToUnixTime(DateTime dt)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(dt - dtStart).TotalSeconds;
        }

        /// <summary>
        /// 判断某数字是否在某数组中
        /// </summary>
        /// <param name="arrResource">数组</param>
        /// <param name="dValue">数字</param>
        /// <returns></returns>
        public static bool IsInArr(double[] arrResource, double dValue)
        {
            bool bIfIn = false;
            if (arrResource != null)
                bIfIn = Array.IndexOf(arrResource, dValue) == -1;
            return bIfIn;
        }

        /// <summary>
        /// 字节数组转成整数 
        /// </summary>
        /// <param name="bArrBag">字节数组(低字节在前，高字节在后)</param>
        /// <returns></returns>
        public static uint ConverToUInt(byte[] bArrBag)
        {
            uint iNum = 0;
            if (!(bArrBag == null || !(bArrBag.Length == 4 || bArrBag.Length == 2)))
            {
                if (bArrBag.Length == 2)
                {
                    byte[] bArrBagTemp = new byte[4];
                    bArrBagTemp[0] = bArrBag[0];
                    bArrBagTemp[1] = bArrBag[1];
                    bArrBagTemp[2] = 0X00;
                    bArrBagTemp[3] = 0X00;

                    iNum = ScaleConverter.ByteArr2UInt(bArrBagTemp);
                }
                else
                {
                    iNum = ScaleConverter.ByteArr2UInt(bArrBag);
                }
            }
            return iNum;
        }

        /// <summary>
        /// 字节数组转成整数 
        /// </summary>
        /// <param name="bArrBag">字节数组(低字节在前，高字节在后)</param>
        /// <returns></returns>
        public static int ConverToInt(byte[] bArrBag)
        {
            int iNum = 0;
            if (!(bArrBag == null || !(bArrBag.Length == 4 || bArrBag.Length == 2)))
            {
                if (bArrBag.Length == 2)
                {
                    byte[] bArrBagTemp = new byte[4];
                    bArrBagTemp[0] = bArrBag[0];
                    bArrBagTemp[1] = bArrBag[1];
                    bArrBagTemp[2] = 0X00;
                    bArrBagTemp[3] = 0X00;

                    iNum = ScaleConverter.ByteArr2Int(bArrBagTemp);
                }
                else
                {
                    iNum = ScaleConverter.ByteArr2Int(bArrBag);
                }
            }
            return iNum;
        }

        public static string FormatString(object obj)
        {
            return (obj == null) ? "" : obj.ToString().Trim();
        }

        public static int FormatInt(object obj)
        {
            int iRtn = 0;
            try
            {
                iRtn = Convert.ToInt32(obj);
            }
            catch
            {

            }
            return iRtn;
        }

        public static Int64 FormatInt64(object obj)
        {
            Int64 iRtn = 0;
            try
            {
                iRtn = Convert.ToInt64(obj);
            }
            catch
            {

            }
            return iRtn;
        }

        public static uint FormatUInt(object obj)
        {
            uint iRtn = 0;
            try
            {
                iRtn = Convert.ToUInt32(obj);
            }
            catch
            {

            }
            return iRtn;
        }

        /// <summary>
        /// 是否是整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsInt(object obj)
        {
            bool bIfOk = false;
            if (obj != null)
            {
                try
                {
                    int i = Convert.ToInt32(obj);
                    bIfOk = true;
                }
                catch
                {

                }
            }
            return bIfOk;
        }

        /// <summary>
        /// 是否是整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsUInt(object obj)
        {
            bool bIfOk = false;
            if (obj != null)
            {
                try
                {
                    uint i = Convert.ToUInt32(obj);
                    bIfOk = true;
                }
                catch
                {

                }
            }
            return bIfOk;
        }

        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5_32(string str)
        {
            string strRtn = "";
            byte[] result = Encoding.Default.GetBytes(str);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            strRtn = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密
            return strRtn;
        }

        /// <summary>
        /// 获得某日期是当年的第几周
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public static int GetWeekOfYear(DateTime dt)
        {
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            return gc.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
