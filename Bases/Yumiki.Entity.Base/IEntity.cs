using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Entity.Base
{
    /// <summary>
    /// Contains all common fields which all tables have in Database
    /// </summary>
    public interface IEntity
    {
        Guid ID { get; set; }
        string Descriptions { get; set; }
        bool IsActive { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? LastUpdateDate { get; set; }
    }
}
