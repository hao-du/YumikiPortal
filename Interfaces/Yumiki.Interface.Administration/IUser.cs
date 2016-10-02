using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Interface.Administration
{
    public interface IUser
    {
        Guid ID { get; set; }
        string UserLoginName { get; set; }
        string CurrentPassword { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Descriptions { get; set; }
        string IsActive { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? LastUpdateDate { get; set; }
    }
}
