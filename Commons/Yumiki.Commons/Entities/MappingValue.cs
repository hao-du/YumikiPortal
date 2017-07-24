using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Entities
{
    public class MappingValue : Attribute
    {
        public string Value { get; private set; }

        public MappingValue(string mappingValue)
        {
            this.Value = mappingValue;
        }
    }
}
