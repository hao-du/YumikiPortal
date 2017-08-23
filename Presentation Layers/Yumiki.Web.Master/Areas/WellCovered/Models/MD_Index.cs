
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
    public class MD_Index : Model<TB_Index>
    {
        public MD_Index()
        {
            this._internalItem = new TB_Index();
        }

        public MD_Index(TB_Index index)
        {
            _internalItem = index;
        }

        public Guid LiveID
        {
            get
            {
                return _internalItem.LiveID;
            }
        }

        public string DisplayContents
        {
            get
            {
                return _internalItem.DisplayContents;
            }
        }

        public override TB_Index ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}