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
                return _internalItem.Status;
            }
            set
            {
                _internalItem.Status = value;
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

        public DateTime? EstimatedStartDate
        {
            get
            {
                return _internalItem.EstimatedStartDate;
            }
            set
            {
                _internalItem.EstimatedStartDate = value;
            }
        }

        public DateTime? EstimatedEndDate
        {
            get
            {
                return _internalItem.EstimatedEndDate;
            }
            set
            {
                _internalItem.EstimatedEndDate = value;
            }
        }

        public DateTime? ActualStartDate
        {
            get
            {
                return _internalItem.ActualStartDate;
            }
            set
            {
                _internalItem.ActualStartDate = value;
            }
        }

        public DateTime? ActualEndDate
        {
            get
            {
                return _internalItem.ActualEndDate;
            }
            set
            {
                _internalItem.ActualEndDate = value;
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