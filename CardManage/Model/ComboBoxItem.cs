using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    /// <summary>
    /// 下拉框项
    /// </summary>
    class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public ComboBoxItem(string strText, object objValue)
        {
            Text = strText;
            Value = objValue;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
