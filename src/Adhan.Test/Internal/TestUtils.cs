using Batoulapps.Adhan.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Adhan.Test.Internal
{
    public class TestUtils
    {
        /// <summary>
        /// Gets a TimeZoneInfo object based on the current OS Platform 
        /// </summary>
        /// <param name="ianaTimezone">A IANA standard time zone string</param>
        /// <returns>TimeZoneInfo object if matched, otherwise null</returns>
        public static TimeZoneInfo GetTimeZone(string ianaTimezone)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                bool result = TimeZoneConverter.TZConvert.TryIanaToWindows(ianaTimezone, out string windowsTimeZoneId);
                if (result == false)
                {
                    return null;
                }

                return TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return TimeZoneInfo.FindSystemTimeZoneById(ianaTimezone);
            }

            return null;
        }

        public static DateTime MakeDate(int year, int month, int day)
        {
            return MakeDate(year, month, day, 0, 0, 0);
        }

        public static DateTime MakeDate(int year, int month, int day, int hour, int minute)
        {
            return MakeDate(year, month, day, hour, minute, 0);
        }

        public static DateTime MakeDate(int year, int month, int day, int hour, int minute, int second)
        {
            return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
        }

        public static int GetDayOfYear(DateTime date)
        {
            return date.DayOfYear;
        }

        public static DateComponents GetDateComponents(String date)
        {
            string[] pieces = date.Split("-");
            int year = int.Parse(pieces[0]);
            int month = int.Parse(pieces[1]);
            int day = int.Parse(pieces[2]);
            return new DateComponents(year, month, day);
        }

        public static DateTime AddSeconds(DateTime date, int seconds)
        {
            return date.AddSeconds(seconds);
        }

        public static DateTime MakeDateWithOffset(int year, int month, int day, int numberOfDayOffset)
        {
            DateTime calendar = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
            return calendar.AddDays(numberOfDayOffset);
        }
    }
}
