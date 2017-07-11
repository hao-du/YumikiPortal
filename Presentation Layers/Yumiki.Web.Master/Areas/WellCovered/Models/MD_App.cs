
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
            this._interalItem = new TB_App();
        }

        public MD_App(TB_App app)
        {
            _interalItem = app;
        }

        [Display(Name = "Application Name")]
        [Required]
        public string AppName
        {
            get
            {
                return _interalItem.AppName;
            }
            set
            {
                _interalItem.AppName = value;
            }
        }

        [Display(Name = "Is Shareable")]
        public bool IsShareable
        {
            get
            {
                return _interalItem.IsShareable;
            }
            set
            {
                _interalItem.IsShareable = value;
            }
        }

        public override string LastUpdateDateUI
        {
            get
            {
                return _interalItem.LastUpdateDateUI;
            }
        }


        public override TB_App ToObject()
        {
            _interalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}