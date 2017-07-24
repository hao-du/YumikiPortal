using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Entities;

namespace Yumiki.Commons.Helpers
{
    public static class EnumHelper
    {
        public static List<ExtendEnum> GetDatasource<T>()
        {
            List<ExtendEnum> extendEnums = new List<ExtendEnum>();

            Array values = Enum.GetValues(typeof(T));

            foreach (object value in values)
            {
                string displayText = value.ToString();

                //Retrieve Description attribute of value
                //E.g.
                //   [Description("Integer")]
                //   E_INT,
                FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    displayText = attributes[0].Description;
                }

                ExtendEnum extendEnum = new ExtendEnum
                {
                    Text = Enum.GetName(typeof(T), value),
                    Value = (int)value,
                    DisplayText = displayText
                };

                extendEnums.Add(extendEnum);
            }

            return extendEnums;
        }

        public static string GetMappingValue(Type type, string name)
        {
            MemberInfo[] memberInfo = type.GetMember(name);
            if (memberInfo != null && memberInfo.Length > 0)
            {
                MappingValue attr = Attribute.GetCustomAttribute(memberInfo[0], typeof(MappingValue)) as MappingValue;
                if (attr != null)
                {
                    return attr.Value;
                }
            }
            return null;
        }

        public static string GetMappingValue(object enm)
        {
            if (enm != null)
            {
                MemberInfo[] memberInfo = enm.GetType().GetMember(enm.ToString());
                if (memberInfo != null && memberInfo.Length > 0)
                {
                    MappingValue attr = Attribute.GetCustomAttribute(memberInfo[0], typeof(MappingValue)) as MappingValue;
                    if (attr != null)
                    {
                        return attr.Value;
                    }
                }
            }
            return null;
        }
    }
}
