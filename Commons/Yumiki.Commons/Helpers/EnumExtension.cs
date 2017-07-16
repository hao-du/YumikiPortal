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
                    Value = int.Parse(value.ToString()),
                    DisplayText = displayText
                };

                extendEnums.Add(extendEnum);
            }

            return extendEnums;
        }
    }
}
