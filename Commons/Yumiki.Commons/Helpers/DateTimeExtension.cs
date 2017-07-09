using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Settings;

namespace Yumiki.Commons.Helpers
{
    public static class DateTimeExtension
    {
        public static DateTime GetSystemDatetime()
        {
            return DateTime.UtcNow;
        }

        public static DateTime GetLocalSystemDatetime()
        {
            return DateTime.Now;
        }

        public static DateTime GetSystemMinDate()
        {
            return DateTime.MinValue;
        }

        public static DateTime GetSystemMaxDate()
        {
            return DateTime.MaxValue;
        }

        public static DateTime GetDatabaseMinDate()
        {
            return SqlDateTime.MinValue.Value;
        }

        public static DateTime GetDatabaseMaxDate()
        {
            return SqlDateTime.MaxValue.Value;
        }

        public static IEnumerable<SystemTimeZone> GetAllTimeZone()
        {
            return TimeZoneInfo.GetSystemTimeZones().Select(c => new SystemTimeZone { ID = c.Id, DisplayName = c.DisplayName });
        }

        public static SystemTimeZone GetDefaultTimeZone()
        {
            return new SystemTimeZone { ID = TimeZoneInfo.Local.Id, DisplayName = TimeZoneInfo.Local.DisplayName };
        }

        public static SystemTimeZone GetTimeZoneById(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById(id);
                if (timezone != null)
                {
                    return new SystemTimeZone { ID = timezone.Id, DisplayName = timezone.DisplayName };
                }
            }
            return GetDefaultTimeZone();
        }

        /// <summary>
        /// Return datetime from UTC to a specific time zone.
        /// </summary>
        /// <param name="value">Datetime need to be converted.</param>
        /// <param name="timeZone">Destination time zone.</param>
        /// <returns></returns>
        public static DateTime GetZonedDateTimeFromUTC(this DateTime value, TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(value, timeZone);
        }

        /// <summary>
        /// Return datetime from UTC to a user http session time zone.
        /// </summary>
        /// <param name="value">Datetime need to be converted.</param>
        public static DateTime GetZonedDateTimeFromUTC(this DateTime value)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(value, TimeZoneInfo.FindSystemTimeZoneById(CurrentUser.TimeZone.ID));
        }

        public static DateTime GetStartDateOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime GetEndDateOfMonth(this DateTime value)
        {
            return GetStartDateOfMonth(value).AddMonths(1).AddMilliseconds(-1);
        }
    }
}
