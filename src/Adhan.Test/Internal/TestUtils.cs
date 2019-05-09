using Batoulapps.Adhan.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adhan.Test.Internal
{
    public class TestUtils
    {
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

        //static DateTime MakeDateWithOffset(int year, int month, int day, int offset, int unit)
        //{
        //    Calendar calendar = GregorianCalendar.getInstance(TimeZone.getTimeZone("UTC"));
        //    //noinspection MagicConstant
        //    calendar.set(year, month - 1, day);
        //    calendar.add(unit, offset);
        //    return calendar.getTime();
        //}
    }
}
