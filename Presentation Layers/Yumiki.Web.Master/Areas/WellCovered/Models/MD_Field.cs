﻿using System;
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
            this._interalItem = new TB_Field();
        }

        public MD_Field(TB_Field field)
        {
            _interalItem = field;
        }

        [Display(Name = "Field Name")]
        [Required]
        public string FieldName
        {
            get
            {
                return _interalItem.FieldName;
            }
            set
            {
                _interalItem.FieldName = value;
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

        public Guid ObjectID
        {
            get
            {
                return _interalItem.ObjectID;
            }
            set
            {
                _interalItem.ObjectID = value;
            }
        }

        public TB_Object Object
        {
            get
            {
                return _interalItem.Object;
            }
            set
            {
                _interalItem.Object = value;
            }
        }

        [Display(Name = "Field Type")]
        [Required]
        public EN_DataType FieldType
        {
            get
            {
                return _interalItem.FieldType;
            }
            set
            {
                _interalItem.FieldType = value;
            }
        }

        [IsRequiredBasedOnField(TB_Field.FieldNames.FieldType, EN_DataType.E_STRING)]
        [Display(Name = "Field Length")]
        public int? FieldLength
        {
            get
            {
                return _interalItem.FieldLength;
            }
            set
            {
                _interalItem.FieldLength = value;
            }
        }

        [Display(Name = "Is Required")]
        public bool IsRequired
        {
            get
            {
                return _interalItem.IsRequired;
            }
            set
            {
                _interalItem.IsRequired = value;
            }
        }
        
        [IsRequiredBasedOnField(TB_Field.FieldNames.FieldType, EN_DataType.E_DATASOURCE)]
        public string Datasource
        {
            get
            {
                return _interalItem.Datasource;
            }
            set
            {
                _interalItem.Datasource = value;
            }
        }

        [Display(Name = "Order")]
        public int? FieldOrder
        {
            get
            {
                return _interalItem.FieldOrder;
            }
            set
            {
                _interalItem.FieldOrder = value;
            }
        }

        [Display(Name = "Is Displayable")]
        public bool IsDisplayable
        {
            get
            {
                return _interalItem.IsDisplayable;
            }
            set
            {
                _interalItem.IsDisplayable = value;
            }
        }

        [Display(Name = "Is Seachable")]
        public bool CanIndex
        {
            get
            {
                return _interalItem.CanIndex;
            }
            set
            {
                _interalItem.CanIndex = value;
            }
        }

        public bool IsSystemField
        {
            get
            {
                return _interalItem.IsSystemField;
            }
        }

        public object Value
        {
            get
            {
                return _interalItem.Value;
            }
            set
            {
                _interalItem.Value = value;
            }
        }

        public override string LastUpdateDateUI
        {
            get
            {
                return _interalItem.LastUpdateDateUI;
            }
        }


        public override TB_Field ToObject()
        {
            _interalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}