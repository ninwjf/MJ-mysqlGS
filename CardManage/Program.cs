using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace CardManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool bIfExpiry = Manager.GetInstance().CheckSoftIfExpiry();
            bool isAppRunning = false;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Assembly.GetExecutingAssembly().FullName, out isAppRunning);
            if (!isAppRunning)
            {
                MessageBox.Show("本程序已经在运行了!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(1);
            }
            /*******************
            else if (bIfExpiry)
            {
                MessageBox.Show("软件版本可能过期", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            *******************/
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MDIParent1());
            }
        }
    }
}
