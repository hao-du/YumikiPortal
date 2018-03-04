using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.WellCovered;
using Yumiki.Web.WellCovered.Validations;

namespace Yumiki.Web.WellCovered.Models
{
    public class MD_Field : Model<TB_Field>
    {
        public MD_Field()
        {
            this._internalItem = new TB_Field();
        }

        public MD_Field(TB_Field field)
        {
            _internalItem = field;
        }

        [Display(Name = "Field Name")]
        [Required]
        public string FieldName
        {
            get
            {
                return _internalItem.FieldName;
            }
            set
            {
                _internalItem.FieldName = value;
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

        public Guid ObjectID
        {
            get
            {
                return _internalItem.ObjectID;
            }
            set
            {
                _internalItem.ObjectID = value;
            }
        }

        public TB_Object Object
        {
            get
            {
                return _internalItem.Object;
            }
            set
            {
                _internalItem.Object = value;
            }
        }

        [Display(Name = "Field Type")]
        [Required]
        public EN_DataType FieldType
        {
            get
            {
                return _internalItem.FieldType;
            }
            set
            {
                _internalItem.FieldType = value;
            }
        }

        [IsRequiredBasedOnField(TB_Field.FieldNames.FieldType, EN_DataType.E_STRING)]
        [Display(Name = "Field Length")]
        public int? FieldLength
        {
            get
            {
                return _internalItem.FieldLength;
            }
            set
            {
                _internalItem.FieldLength = value;
            }
        }

        [Display(Name = "Is Required")]
        public bool IsRequired
        {
            get
            {
                return _internalItem.IsRequired;
            }
            set
            {
                _internalItem.IsRequired = value;
            }
        }
        
        [IsRequiredBasedOnField(TB_Field.FieldNames.FieldType, EN_DataType.E_DATASOURCE)]
        public string Datasource
        {
            get
            {
                return _internalItem.Datasource;
            }
            set
            {
                _internalItem.Datasource = value;
            }
        }

        [Display(Name = "Order")]
        public int? FieldOrder
        {
            get
            {
                return _internalItem.FieldOrder;
            }
            set
            {
                _internalItem.FieldOrder = value;
            }
        }

        [Display(Name = "Is Displayable")]
        public bool IsDisplayable
        {
            get
            {
                return _internalItem.IsDisplayable;
            }
            set
            {
                _internalItem.IsDisplayable = value;
            }
        }

        [Display(Name = "Is Seachable")]
        public bool CanIndex
        {
            get
            {
                return _internalItem.CanIndex;
            }
            set
            {
                _internalItem.CanIndex = value;
            }
        }

        [Display(Name = "Sort Data Priority")]
        public int? DataSortByOrder
        {
            get
            {
                return _internalItem.DataSortByOrder;
            }
            set
            {
                _internalItem.DataSortByOrder = value;
            }
        }

        public bool IsSystemField
        {
            get
            {
                return _internalItem.IsSystemField;
            }
        }

        public object Value
        {
            get
            {
                return _internalItem.Value;
            }
            set
            {
                _internalItem.Value = value;
            }
        }

        public override string LastUpdateDateUI
        {
            get
            {
                return _internalItem.LastUpdateDateUI;
            }
        }


        public override TB_Field ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}