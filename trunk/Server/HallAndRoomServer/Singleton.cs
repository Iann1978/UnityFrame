using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallAndRoomServer
{
    class Singleton<T> where T : class, new()
    {
        static T this_obj;
        static readonly object lockobj = new object();

        public static T me
        {
            get
            {
                if (this_obj == null)
                {
                    lock (lockobj)
                    {
                        if (this_obj == null)
                            this_obj = new T();
                    }
                }
                return this_obj;
            }
        }
    }


}