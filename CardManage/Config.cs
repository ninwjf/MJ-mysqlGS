using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage
{
    /// <summary>
    /// 系统配置类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 是否延迟加载数据，延迟加载表示界面显示后，系统用线程加载数据
        /// </summary>
        public const bool IfDelayLoadData = true;

        /// <summary>
        /// 软件名称
        /// </summary>
        public const string SoftName = "管理系统";

        /// <summary>
        /// 软件版本号
        /// </summary>
        public const string SoftVersion = "V3.1.3";

        /// <summary>
        /// 对话框标题
        /// </summary>
        public const string DialogTitle = "系统提示";

        /// <summary>
        /// Ini配置文件名称
        /// </summary>
        public const string IniFileName = "config.ini";

        /// <summary>
        /// 调试打开验证文件名
        /// </summary>
        public const string DebugSafeFileName = "Hello Jode,I Want To Debug.ini";

        /// <summary>
        /// 时间输出格式
        /// </summary>
        public const string TimeFormat = "yyyy-MM-dd";

        /// <summary>
        /// 长时间输出格式
        /// </summary>
        public const string LongTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 默认数据库名称
        /// </summary>
        public const string DefaultDBName = "doorsys";

        /// <summary>
        /// 加解密密钥,8位以上,不能包含中文
        /// </summary>
        public const string EncryptKey = "gogo1234";
    }
}
