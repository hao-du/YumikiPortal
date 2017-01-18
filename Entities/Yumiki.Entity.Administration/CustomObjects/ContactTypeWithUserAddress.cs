using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Entity.Administration.CustomObjects
{
    public class ContactTypeWithUserAddress
    {
        public Guid ID { get; set; }
        public string ContactTypeName { get; set; }
        public IEnumerable<TB_UserAddress> UserAddresses { get; set; }

        public class FieldName
        {
            public const string ContactTypeName = "ContactTypeName";
            public const string UserAddresses = "UserAddresses";
        }
    }
}
