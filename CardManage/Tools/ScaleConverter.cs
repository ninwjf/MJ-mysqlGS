using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Tools
{
    /// <summary>
    /// 进制转换
    /// </summary>
    public abstract class ScaleConverter
    {
        public static uint ByteArr2UInt(byte[] bytes)
        {
            uint iRtn = uint.MaxValue;
            iRtn = BitConverter.ToUInt32(bytes, 0);
            return iRtn;
        }


        public static int ByteArr2Int(byte[] bytes)
        {
            int iRtn = int.MaxValue;
            iRtn = BitConverter.ToInt32(bytes, 0);
            return iRtn;
        }

        public static string ByteArr2String(byte[] bytes)
        {
            string strRtn = "";
            strRtn = BitConverter.ToString(bytes);
            return strRtn;
        }
        public static int HexTo10(byte[] bytes)
        {
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// 整形数值转字节
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static byte Int2Byte(int num)
        {
            string strHex = Convert.ToString(num, 16);
            if (strHex.Length == 1) strHex = "0" + strHex;//补齐
            return ScaleConverter.HexStr2ByteArr(strHex)[0];
        }

        /// <summary>
        /// 整形数值转字节数组，低字节在前高字节在后
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static byte[] UInt2ByteArr(uint num)
        {
            return System.BitConverter.GetBytes(num);
        }

        /// <summary>
        /// 整形数值转字节数组，低字节在前高字节在后
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static byte[] Int2ByteArr(int num)
        {
            return System.BitConverter.GetBytes(num);
        }

        /// <summary>  
        /// 字节转16进制字符
        /// </summary>  
        /// <param name="bytes"></param>  
        /// <returns></returns>  
        public static string Byte2HexStr(byte bVal)
        {
            string returnStr = "";
            returnStr = bVal.ToString("X2").PadLeft(2, '0');
            return returnStr;
        }

        /// <summary>  
        /// 字节数组转16进制字符串  
        /// </summary>  
        /// <param name="bytes"></param>  
        /// <returns></returns>  
        public static string ByteArr2HexStr(byte[] bytes)
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
        public static byte[] HexStr2ByteArr(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length == 1) hexString = "0" + hexString;
            if ((hexString.Length % 2) != 0) hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>  
        /// 16进制描述的两个字符转字节  
        /// </summary>  
        /// <param name="hexString"></param>  
        /// <returns></returns>  
        public static byte OneHexStr2Byte(string hexString)
        {
            hexString = hexString.PadLeft(2,'0');
            byte returnByte = Convert.ToByte(hexString, 16);
            return returnByte;
        }


        /// <summary>  
        /// 从汉字转换到16进制  
        /// </summary>  
        /// <param name="s"></param>  
        /// <param name="charset">编码,如"utf-8","gb2312"</param>  
        /// <param name="fenge">是否每字符用逗号分隔</param>  
        /// <returns></returns>  
        public static string ToHex(string s, string charset, bool fenge)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格  
                //throw new ArgumentException("s is not valid chinese string!");  
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            byte[] bytes = chs.GetBytes(s);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
                if (fenge && (i != bytes.Length - 1))
                {
                    str += string.Format("{0}", ",");
                }
            }
            return str.ToLower();
        }

        /// <summary>  
        /// 从16进制转换成汉字  
        /// </summary>  
        /// <param name="hex"></param>  
        /// <param name="charset">编码,如"utf-8","gb2312"</param>  
        /// <returns></returns>  
        public static string UnHex(string hex, string charset)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格  
            }
            // 需要将 hex 转换成 byte 数组。   
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。   
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message.   
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            return chs.GetString(bytes);
        }
    }
}
