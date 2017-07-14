
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Web.WellCovered.Models
{
    public class MD_Object : Model<TB_Object>
    {
        public MD_Object()
        {
            this._interalItem = new TB_Object();
        }

        public MD_Object(TB_Object obj)
        {
            _interalItem = obj;
        }

        [Display(Name = "Object Name")]
        [Required]
        public string ObjectName
        {
            get
            {
                return _interalItem.ObjectName;
            }
            set
            {
                _interalItem.ObjectName = value;
            }
        }

        [Display(Name = "Display Name")]
        [Required]
        public string DisplayName
        {
            get
            {
                return _interalItem.DisplayName;
            }
            set
            {
                _interalItem.DisplayName = value;
            }
        }

        [Display(Name = "Api Name")]
        [Required]
        public string ApiName
        {
            get
            {
                return _interalItem.ApiName;
            }
            set
            {
                _interalItem.ApiName = value;
            }
        }

        public Guid? AppID
        {
            get
            {
                return _interalItem.AppID;
            }
            set
            {
                _interalItem.AppID = value;
            }
        }

        public TB_App App
        {
            get
            {
                return _interalItem.App;
            }
            set
            {
                _interalItem.App = value;
            }
        }

        [Display(Name = "Application Name")]
        public string ApplicationName
        {
            get
            {
                return _interalItem.AppName;
            }
        }

        public override string LastUpdateDateUI
        {
            get
            {
                return _interalItem.LastUpdateDateUI;
            }
        }


        public override TB_Object ToObject()
        {
            _interalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}