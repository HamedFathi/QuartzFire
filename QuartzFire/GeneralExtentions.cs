using System;
using System.Threading.Tasks;

namespace Quartz
{
    public static class GeneralExtentions
    {
        public static Action<object> Convert<T>(this Action<T> myActionT)
        {
            if (myActionT == null) return null;
            else return new Action<object>(o => myActionT((T)o));
        }
    }
}