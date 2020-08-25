using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace MovingApp.Utils
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}