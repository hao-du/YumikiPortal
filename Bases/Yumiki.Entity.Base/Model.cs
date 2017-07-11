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
        protected T _interalItem;

        public Guid ID
        {
            get
            {
                return _interalItem.ID;
            }
            set
            {
                _interalItem.ID = value;
            }
        }

        public string Descriptions
        {
            get
            {
                return _interalItem.Descriptions;
            }
            set
            {
                _interalItem.Descriptions = value;
            }
        }

        [Display(Name = "Is Active")]
        public bool IsActive
        {
            get
            {
                return _interalItem.IsActive;
            }
            set
            {
                _interalItem.IsActive = value;
            }
        }

        [Display(Name = "Last Updated Date")]
        public virtual string LastUpdateDateUI { get; }

        public virtual T ToObject()
        {
            return _interalItem;
        } 
    }
}
