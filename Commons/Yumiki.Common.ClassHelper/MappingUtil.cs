using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;

namespace Yumiki.Common.Helper
{
    public static class MappingUtil
    {
        public static T ConvertObject<T>(object source, T destination, List<Mapping> mappingFields)
        {
            foreach(Mapping mapping in mappingFields)
            {

            }

            return destination;
        }

        public static void GetMappings(string mappingName, string className)
        {
            XDocument root = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\" + mappingName + ".xml");

            List<Mapping> mappingFields = root.Elements().Where(c => c.HasAttributes && c.Attribute("name").Value == className)
                                                                .SelectMany(c => c.Element("maps").Descendants("field"))
                                                                .Select(c => new Mapping { Input = c.Attribute("input").Value, Output = c.Attribute("input").Value }).ToList();

            //return mappingFields;
        }
    }
}
