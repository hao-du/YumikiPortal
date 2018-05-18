using System;
using System.ComponentModel.DataAnnotations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.OnTime;

namespace Yumiki.Web.Ontime.Models
{
    public class MD_Phase : MD_OnTimeBase<TB_Phase>
    {
        public MD_Phase()
        {
            this._internalItem = new TB_Phase();
        }

        public MD_Phase(TB_Phase phase)
        {
            _internalItem = phase;
        }

        public string PhaseName
        {
            get
            {
                return _internalItem.PhaseName;
            }
            set
            {
                _internalItem.PhaseName = value;
            }
        }

        public int Status
        {
            get
            {
                return (int)_internalItem.Status;
            }
            set
            {
                _internalItem.Status = (EN_PhaseStatus)value;
            }
        }

        public string StatusUI
        {
            get
            {
                return EnumHelper.GetDescription((EN_PhaseStatus)_internalItem.Status);
            }
        }

        public string ReleaseVersion
        {
            get
            {
                return _internalItem.ReleaseVersion;
            }
            set
            {
                _internalItem.ReleaseVersion = value;
            }
        }

        public string EstimatedStartDate
        {
            get
            {
                return _internalItem.EstimatedStartDate.HasValue ? _internalItem.EstimatedStartDate.Value.ToString(Formats.DateTime.LongDate) : string.Empty;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _internalItem.EstimatedStartDate = DateTime.Parse(value);
                }
            }
        }

        public string EstimatedEndDate
        {
            get
            {
                return _internalItem.EstimatedEndDate.HasValue ? _internalItem.EstimatedEndDate.Value.ToString(Formats.DateTime.LongDate) : string.Empty;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _internalItem.EstimatedEndDate = DateTime.Parse(value);
                }
            }
        }

        public string ActualStartDate
        {
            get
            {
                return _internalItem.ActualStartDate.HasValue ? _internalItem.ActualStartDate.Value.ToString(Formats.DateTime.LongDate) : string.Empty;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _internalItem.ActualStartDate = DateTime.Parse(value);
                }
            }
        }

        public string ActualEndDate
        {
            get
            {
                return _internalItem.ActualEndDate.HasValue ? _internalItem.ActualEndDate.Value.ToString(Formats.DateTime.LongDate) : string.Empty;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _internalItem.ActualEndDate = DateTime.Parse(value);
                }
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

        public override TB_Phase ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}