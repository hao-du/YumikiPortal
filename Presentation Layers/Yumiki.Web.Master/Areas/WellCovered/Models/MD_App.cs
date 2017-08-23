
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
    public class MD_App : Model<TB_App>
    {
        public MD_App()
        {
            this._internalItem = new TB_App();
        }

        public MD_App(TB_App app)
        {
            _internalItem = app;
        }

        [Display(Name = "Application Name")]
        [Required]
        public string AppName
        {
            get
            {
                return _internalItem.AppName;
            }
            set
            {
                _internalItem.AppName = value;
            }
        }

        [Display(Name = "Is Shareable")]
        public bool IsShareable
        {
            get
            {
                return _internalItem.IsShareable;
            }
            set
            {
                _internalItem.IsShareable = value;
            }
        }

        public override string LastUpdateDateUI
        {
            get
            {
                return _internalItem.LastUpdateDateUI;
            }
        }


        public override TB_App ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}