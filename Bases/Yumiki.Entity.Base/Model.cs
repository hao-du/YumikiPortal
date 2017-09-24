using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Entity.Base
{
    public abstract class Model<T> where T : IEntity
    {
        protected T _internalItem;

        public Guid ID
        {
            get
            {
                return _internalItem.ID;
            }
            set
            {
                _internalItem.ID = value;
            }
        }

        public string Descriptions
        {
            get
            {
                return _internalItem.Descriptions;
            }
            set
            {
                _internalItem.Descriptions = value;
            }
        }

        [Display(Name = "Is Active")]
        public bool IsActive
        {
            get
            {
                return _internalItem.IsActive;
            }
            set
            {
                _internalItem.IsActive = value;
            }
        }

        [Display(Name = "Last Updated Date")]
        public virtual string LastUpdateDateUI { get; }

        public virtual T ToObject()
        {
            if (ID == Guid.Empty)
            {
                _internalItem.IsActive = true;
            }

            return _internalItem;
        } 
    }
}
