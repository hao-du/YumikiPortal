using System.ComponentModel.DataAnnotations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.OnTime;

namespace Yumiki.Web.Ontime.Models
{
    public class MD_Project : MD_OnTimeBase<TB_Project>
    {
        public MD_Project()
        {
            this._internalItem = new TB_Project();
        }

        public MD_Project(TB_Project project)
        {
            _internalItem = project;
        }

        public string ProjectName
        {
            get
            {
                return _internalItem.ProjectName;
            }
            set
            {
                _internalItem.ProjectName = value;
            }
        }

        public int AssignedNumber
        {
            get
            {
                return _internalItem.AssignedNumber;
            }
            set
            {
                _internalItem.AssignedNumber = value;
            }
        }

        public string Prefix
        {
            get
            {
                return _internalItem.Prefix;
            }
            set
            {
                _internalItem.Prefix = value;
            }
        }

        public bool IsAssigned
        {
            get
            {
                return _internalItem.IsAssigned;
            }
            set
            {
                _internalItem.IsAssigned = value;
            }
        }

        public override TB_Project ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}