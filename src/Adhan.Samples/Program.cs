using System;

using Batoulapps.Adhan;
using Batoulapps.Adhan.Internal;

namespace Adhan.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Coordinates coordinates = new Coordinates(43.61, -79.70);
            DateComponents dateComponents = DateComponents.From(DateTime.Now);
            CalculationParameters parameters = CalculationMethod.NORTH_AMERICA.GetParameters();

            TimeZoneInfo easternTime = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            PrayerTimes prayerTimes = new PrayerTimes(coordinates, dateComponents, parameters);
            Console.WriteLine("Fajr   : " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Fajr, easternTime));
            Console.WriteLine("Sunrise: " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Sunrise, easternTime));
            Console.WriteLine("Dhuhr  : " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Dhuhr, easternTime));
            Console.WriteLine("Asr    : " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Asr, easternTime));
            Console.WriteLine("Maghrib: " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Maghrib, easternTime));
            Console.WriteLine("Isha   : " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Isha, easternTime));
        }
    }
}
