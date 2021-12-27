﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using BO;
using BlApi;
using DalApi;

namespace BL
{
    static class DeepCopyUtilities
    {
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
            public static object CopyPropertiesToNew<S>(this S from, Type type)
            {
                object to = Activator.CreateInstance(type); // new object of Type
                from.CopyPropertiesTo(to);
                return to;
            }
            //public static BO.StudentCourse CopyToStudentCourse(this DO.Course course, DO.StudentInCourse sic)
            //{
            //    BO.StudentCourse result = (BO.StudentCourse)course.CopyPropertiesToNew(typeof(BO.StudentCourse));
            //    // propertys' names changed? copy them here...
            //    result.Grade = sic.Grade;
            //    return result;
            //}
    }
    
}
