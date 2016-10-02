using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Common.Helper;

namespace Yumiki.Business.Administration
{
    public static class User
    {

        public static void Test()
        {
            MappingUtil.GetMappings("AdministrationMapping", "user_mapping");
        }
    }
}
