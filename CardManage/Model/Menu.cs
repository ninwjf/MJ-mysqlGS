using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CardManage.Model
{
    /// <summary>
    /// 菜单类
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 名字，唯一性
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单标题,一横杠代表分隔符
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// 访问权限 0:高级管理员及以上能访问；1：普通管理员及以上能访问
        /// </summary>
        public int AccessLevel { get; set; }
        /// <summary>
        /// Form类名称
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 是否模态开启Form
        /// </summary>
        public bool IsModal { get; set; }
        /// <summary>
        /// Form尺寸
        /// </summary>
        public WindowSize WindowSize { get; set; }
        /// <summary>
        /// 其他参数
        /// </summary>
        public Flag Flag { get; set; }
        /// <summary>
        /// 子菜单列表
        /// </summary>
        public IList<Menu> ChildList { get; set; }
        
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="strName">名字，唯一性</param>
        /// <param name="strTitle">标题</param>
        /// <param name="strFormName">Form类名称</param>
        /// <param name="bIsModal">是否模态开启Form</param>
        /// <param name="objWindowSize">窗口尺寸</param>
        /// <param name="iAccessLevel">访问权限 0:高级管理员及以上能访问；1：普通管理员及以上能访问</param>
        /// <param name="objImage">图标</param>
        public Menu(string strName, string strTitle, string strFormName = null, bool bIsModal = false, WindowSize objWindowSize = null, int iAccessLevel = 1, Flag objFlag = null, Image objImage = null)
        {
            Name = strName;
            Title = strTitle;
            FormName = strFormName;
            IsModal = bIsModal;
            WindowSize = objWindowSize;
            AccessLevel = iAccessLevel;
            Flag = objFlag;
            Image = objImage;
            ChildList = null;
        }
    }
}
