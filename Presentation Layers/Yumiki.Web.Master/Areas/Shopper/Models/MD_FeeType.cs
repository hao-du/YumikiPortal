using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Web.Shopper.Models
{
    public class MD_FeeType : MD_Base<TB_FeeType>
    {
        public MD_FeeType()
        {
            this._internalItem = new TB_FeeType();
        }

        public MD_FeeType(TB_FeeType feeType)
        {
            if(feeType == null)
            {
                _internalItem = new TB_FeeType();
                return;
            }

            _internalItem = feeType;
        }

        public string FeeTypeName
        {
            get
            {
                return _internalItem.FeeTypeName;
            }
            set
            {
                _internalItem.FeeTypeName = value;
            }
        }
        
        public override TB_FeeType ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}