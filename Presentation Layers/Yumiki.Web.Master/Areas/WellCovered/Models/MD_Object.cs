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
            this._internalItem = new TB_Object();
        }

        public MD_Object(TB_Object obj)
        {
            _internalItem = obj;
        }

        [Display(Name = "Object Name")]
        [Required]
        public string ObjectName
        {
            get
            {
                return _internalItem.ObjectName;
            }
            set
            {
                _internalItem.ObjectName = value;
            }
        }

        [Display(Name = "Display Name")]
        [Required]
        public string DisplayName
        {
            get
            {
                return _internalItem.DisplayName;
            }
            set
            {
                _internalItem.DisplayName = value;
            }
        }

        [Display(Name = "Api Name")]
        [Required]
        public string ApiName
        {
            get
            {
                return _internalItem.ApiName;
            }
            set
            {
                _internalItem.ApiName = value;
            }
        }

        public Guid? AppID
        {
            get
            {
                return _internalItem.AppID;
            }
            set
            {
                _internalItem.AppID = value;
            }
        }

        public TB_App App
        {
            get
            {
                return _internalItem.App;
            }
            set
            {
                _internalItem.App = value;
            }
        }

        [Display(Name = "Application Name")]
        public string ApplicationName
        {
            get
            {
                return _internalItem.AppName;
            }
        }

        public override string LastUpdateDateUI
        {
            get
            {
                return _internalItem.LastUpdateDateUI;
            }
        }


        public override TB_Object ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}