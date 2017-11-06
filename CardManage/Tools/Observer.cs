using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Observers
{

    /// <summary>
    /// 抽象观察者
    /// 为所有的具体观察者定义一个接口，在得到主题通知时更新自己。
    /// </summary>
    public abstract class Observer
    {
        protected Observer(Subject sub)
        {
            //sub.NotifyEvent += Response;
        }

        //public abstract void Response();
    }

    public delegate void NotifyEventHandler();

    /// <summary>
    /// 抽象主题，被观察者
    /// 把所有观察者对象的引用保存到一个聚集里，每个主题都可以有任何数量的观察者。抽象主题提供一个接口，可以增加和删除观察者对象。
    /// </summary>
    public abstract class Subject
    {
        public event NotifyEventHandler NotifyEvent;

        public void Notify()
        {
            NotifyEvent?.Invoke();
        }

        public virtual void Run() { }
    }
}
