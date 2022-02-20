using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using BO;
using BlApi;
using DalApi;
using System.Runtime.CompilerServices;

namespace BL
{
    static class DeepCopyUtilities
    {
        [MethodImpl(MethodImplOptions.Synchronized)]

        public static void CopyPropertiesTo<T, S>(this S from, T to)
            {
                foreach (PropertyInfo propTo in to.GetType().GetProperties())
                {
                    PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                    if (propFrom == null)
                        continue;
                    var value = propFrom.GetValue(from, null);
                    if (value is ValueType || value is string)
                        propTo.SetValue(to, value);
                }
            }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public static object CopyPropertiesToNew<S>(this S from, Type type)
            {
                object to = Activator.CreateInstance(type); // new object of Type
                from.CopyPropertiesTo(to);
                return to;
            }
           
    }
    
}
