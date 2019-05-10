using Microsoft.VisualStudio.TestTools.UnitTesting;

using Batoulapps.Adhan;
using Batoulapps.Adhan.Internal;
using System.IO;
using Adhan.Test.Data;
using System;
using Adhan.Test.Internal;

namespace Adhan.Test
{
    [TestClass]
    public class TimingTest
    {
        private const string timingPath = "../../../../../shared/times/";

        [TestMethod]
        public void TestTimes()
        {
            string[] files = Directory.GetFiles(timingPath, "*.json");
            foreach(string file in files)
            {
                TestTimingFile(file);
            }
        }

        private void TestTimingFile(string file)
        {
            TimingFile timingFile = TimingFile.Load(file);
            Assert.IsNotNull(timingFile);

            Coordinates coordinates = new Coordinates(timingFile.Parameters.Latitude, timingFile.Parameters.Longitude);
            CalculationParameters parameters = ParseParameters(timingFile.Parameters);

            foreach(TimingInfo info in timingFile.Times)
            {
                DateComponents dateComponents = TestUtils.GetDateComponents(info.Date);
                PrayerTimes prayerTimes = new PrayerTimes(coordinates, dateComponents, parameters);

                long fajrDifference = GetDifferenceInMinutes(prayerTimes.Fajr, info.Date, info.Fajr, timingFile.Parameters.Timezone);
                Assert.IsTrue(fajrDifference.IsAtMost(timingFile.Variance));

                long sunriseDifference = GetDifferenceInMinutes(prayerTimes.Sunrise, info.Date, info.Sunrise, timingFile.Parameters.Timezone);
                Assert.IsTrue(sunriseDifference.IsAtMost(timingFile.Variance));

                long dhuhrDifference = GetDifferenceInMinutes(prayerTimes.Dhuhr, info.Date, info.Dhuhr, timingFile.Parameters.Timezone);
                Assert.IsTrue(dhuhrDifference.IsAtMost(timingFile.Variance));

                long asrDifference = GetDifferenceInMinutes(prayerTimes.Asr, info.Date, info.Asr, timingFile.Parameters.Timezone);
                Assert.IsTrue(asrDifference.IsAtMost(timingFile.Variance));

                long maghribDifference = GetDifferenceInMinutes(prayerTimes.Maghrib, info.Date, info.Maghrib, timingFile.Parameters.Timezone);
                Assert.IsTrue(maghribDifference.IsAtMost(timingFile.Variance));

                long ishaDifference = GetDifferenceInMinutes(prayerTimes.Isha, info.Date, info.Isha, timingFile.Parameters.Timezone);
                Assert.IsTrue(ishaDifference.IsAtMost(timingFile.Variance));
            }
        }

        private long GetDifferenceInMinutes(DateTime prayerTime, string jsonDate, string jsonTime, string ianaTimezone)
        {
            TimeZoneInfo timezone = TestUtils.GetTimeZone(ianaTimezone);
            if (timezone == null)
            {
                return 0;
            }

            DateTime parsedDate = DateTime.Parse($"{jsonDate} {jsonTime}");
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(parsedDate, timezone);

            return (long) prayerTime.Subtract(utcDateTime).TotalMinutes;
        }

        private CalculationParameters ParseParameters(TimingParameters timingParameters)
        {
            CalculationMethod method;
            switch (timingParameters.Method)
            {
                case "MuslimWorldLeague":
                    {
                        method = CalculationMethod.MUSLIM_WORLD_LEAGUE;
                        break;
                    }
                case "Egyptian":
                    {
                        method = CalculationMethod.EGYPTIAN;
                        break;
                    }
                case "Karachi":
                    {
                        method = CalculationMethod.KARACHI;
                        break;
                    }
                case "UmmAlQura":
                    {
                        method = CalculationMethod.UMM_AL_QURA;
                        break;
                    }
                case "Dubai":
                    {
                        method = CalculationMethod.DUBAI;
                        break;
                    }
                case "MoonsightingCommittee":
                    {
                        method = CalculationMethod.MOON_SIGHTING_COMMITTEE;
                        break;
                    }
                case "NorthAmerica":
                    {
                        method = CalculationMethod.NORTH_AMERICA;
                        break;
                    }
                case "Kuwait":
                    {
                        method = CalculationMethod.KUWAIT;
                        break;
                    }
                case "Qatar":
                    {
                        method = CalculationMethod.QATAR;
                        break;
                    }
                case "Singapore":
                    {
                        method = CalculationMethod.SINGAPORE;
                        break;
                    }
                default:
                    {
                        method = CalculationMethod.OTHER;
                        break;
                    }
            }

            CalculationParameters parameters = method.GetParameters();
            if ("Shafi".Equals(timingParameters.Madhab))
            {
                parameters.Madhab = Madhab.SHAFI;
            }
            else if ("Hanafi".Equals(timingParameters.Madhab))
            {
                parameters.Madhab = Madhab.HANAFI;
            }

            if ("SeventhOfTheNight".Equals(timingParameters.HighLatitudeRule))
            {
                parameters.HighLatitudeRule = HighLatitudeRule.SEVENTH_OF_THE_NIGHT;
            }
            else if ("TwilightAngle".Equals(timingParameters.HighLatitudeRule))
            {
                parameters.HighLatitudeRule = HighLatitudeRule.TWILIGHT_ANGLE;
            }
            else
            {
                parameters.HighLatitudeRule = HighLatitudeRule.MIDDLE_OF_THE_NIGHT;
            }

            return parameters;
        }
    }
}
