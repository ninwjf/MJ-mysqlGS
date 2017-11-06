using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CardManage.Tools
{
    /// <summary>
    /// 对话框类
    /// </summary>
    public class CMessageBox
    {
        /// <summary>
        /// 显示警告对话框
        /// </summary>
        /// <param name="strContent"></param>
        /// <param name="strTitle"></param>
        public static void ShowWaring(string strContent, string strTitle)
        {
            MessageBox.Show(strContent, strTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示错误对话框
        /// </summary>
        /// <param name="strContent"></param>
        /// <param name="strTitle"></param>
        public static void ShowError(string strContent, string strTitle)
        {
            MessageBox.Show(strContent, strTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示成功对话框
        /// </summary>
        /// <param name="strContent"></param>
        /// <param name="strTitle"></param>
        public static void ShowSucc(string strContent, string strTitle)
        {
            MessageBox.Show(strContent, strTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
