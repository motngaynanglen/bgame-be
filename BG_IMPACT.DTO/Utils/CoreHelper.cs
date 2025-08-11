using BG_IMPACT.DTO.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace BG_IMPACT.DTO.Utils
{
    public static class CoreHelper
    {
        public static TimeZoneInfo SystemTimeZoneInfo => GetTimeZoneInfo(Formattings.TimeZone);
        public static DateTimeOffset SystemTimeNow => DateTimeOffset.UtcNow;
        public static DateTime UtcToSystemTime(this DateTimeOffset dateTimeOffsetUtc)
        {
            return dateTimeOffsetUtc.UtcDateTime.UtcToSystemTime();
        }

        public static DateTime UtcToSystemTime(this DateTime dateTimeUtc)
        {
            var dateTimeWithTimeZone = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, SystemTimeZoneInfo);

            return dateTimeWithTimeZone;
        }
        public static DateTimeOffset UtcToOffsetSystemTime(this DateTimeOffset dateTimeOffsetUtc)
        {
            var localDateTime = dateTimeOffsetUtc.UtcDateTime.UtcToSystemTime();
            return new DateTimeOffset(localDateTime, SystemTimeZoneInfo.GetUtcOffset(localDateTime));
        }

        public static DateTimeOffset UtcToOffsetSystemTime(this DateTime dateTimeUtc)
        {
            var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, SystemTimeZoneInfo);
            return new DateTimeOffset(localDateTime, SystemTimeZoneInfo.GetUtcOffset(localDateTime));
        }
        public static TimeZoneInfo GetTimeZoneInfo(string timeZoneId)
        {
            return TZConvert.GetTimeZoneInfo(timeZoneId);
        }

    }
}
