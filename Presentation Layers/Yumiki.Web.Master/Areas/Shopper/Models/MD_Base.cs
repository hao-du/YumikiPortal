using System.ComponentModel.DataAnnotations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;

namespace Yumiki.Web.Shopper.Models
{
    public abstract class MD_Base<T> : Model<T> where T : IEntity
    {
        public override string LastUpdateDateUI
        {
            get
            {
                return _internalItem.LastUpdateDate.HasValue ?
                            _internalItem.LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime)
                            : _internalItem.CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }
    }
}