using System;
using System.ComponentModel.DataAnnotations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.OnTime;

namespace Yumiki.Web.OnTime.Models
{
    public class MD_Task : MD_OnTimeBase<TB_Task>
    {
        public MD_Task()
        {
            this._internalItem = new TB_Task();
        }

        public MD_Task(TB_Task task)
        {
            if(task == null)
            {
                _internalItem = new TB_Task();
                return;
            }
            _internalItem = task;
        }

        public string TaskName
        {
            get
            {
                return _internalItem.TaskName;
            }
            set
            {
                _internalItem.TaskName = value;
            }
        }

        public string TaskNumber
        {
            get
            {
                string number = _internalItem.TaskNumber.ToString("D5");
                if (_internalItem.Project != null)
                {
                    return $"{_internalItem.Project.Prefix}-{number}";
                }
                return number;
            }
        }

        public Guid ProjectID
        {
            get
            {
                return _internalItem.ProjectID;
            }
            set
            {
                _internalItem.ProjectID = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return _internalItem.Project?.ProjectName;
            }
        }

        public Guid PhaseID
        {
            get
            {
                return _internalItem.PhaseID;
            }
            set
            {
                _internalItem.PhaseID = value;
            }
        }

        public string PhaseName
        {
            get
            {
                return _internalItem.Phase == null ? string.Empty : _internalItem.Phase.PhaseName;
            }
        }

        public int PhaseStatus
        {
            get
            {
                return (int)(_internalItem.Phase == null ? EN_PhaseStatus.E_BACKLOG : _internalItem.Phase.Status);
            }
        }

        public string AssignedUserID
        {
            get
            {
                return _internalItem.AssignedUserID.HasValue ? _internalItem.AssignedUserID.Value.ToString() : string.Empty;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _internalItem.AssignedUserID = Guid.Parse(value);
                }
            }
        }

        public string AssignedUserName
        {
            get
            {
                return $"{_internalItem.AssignedUser?.FirstName} {_internalItem.AssignedUser?.LastName}".Trim();
            }
        }

        public string StartDate
        {
            get
            {
                return _internalItem.StartDate.ToString(Formats.DateTime.LongDate);
            }
            set
            {
                _internalItem.StartDate = DateTime.Parse(value);
            }
        }

        public string EndDate
        {
            get
            {
                return _internalItem.EndDate.ToString(Formats.DateTime.LongDate);
            }
            set
            {
                _internalItem.EndDate = DateTime.Parse(value);
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
                _internalItem.Status = (EN_TaskStatus)value;
            }
        }

        public int Priority
        {
            get
            {
                return (int)_internalItem.Priority;
            }
            set
            {
                _internalItem.Priority = (EN_Priority)value;
            }
        }

        public string StatusUI
        {
            get
            {
                return EnumHelper.GetDescription((EN_PhaseStatus)_internalItem.Status);
            }
        }

        public string TaskDescriptions
        {
            get
            {
                return _internalItem.TaskDescriptions;
            }
            set
            {
                _internalItem.TaskDescriptions = value;
            }
        }

        

        public override TB_Task ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}