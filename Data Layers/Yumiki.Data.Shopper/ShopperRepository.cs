using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;

namespace Yumiki.Entity.Shopper
{
    public class ShopperRepository : BaseRepository<ShopperModel>
    {
        protected ShopperModel Context
        {
            get
            {
                if (context == null)
                    context = new ShopperModel();
                return context;
            }
        }
    }
}
