using Microsoft.VisualStudio.TestTools.UnitTesting;

using Batoulapps.Adhan;
using Batoulapps.Adhan.Internal;
using System.IO;
using Adhan.Test.Data;
using System;
using Adhan.Test.Internal;

namespace Adhan.Test.Internal
{
    [TestClass]
    public class AstronomicalTest
    {
        [TestMethod]
        public void SolarCoordinates()
        {

            // values from Astronomical Algorithms page 165

            double jd = CalendricalHelper.JulianDay(/* year */ 1992, /* month */ 10, /* day */ 13);
            SolarCoordinates solar = new SolarCoordinates(/* JulianDay */ jd);

            double T = CalendricalHelper.JulianCentury(/* JulianDay */ jd);
            double L0 = Astronomical.MeanSolarLongitude(/* julianCentury */ T);
            double ε0 = Astronomical.MeanObliquityOfTheEcliptic(/* julianCentury */ T);
            double εapp = Astronomical.ApparentObliquityOfTheEcliptic(
                /* julianCentury */ T, /* meanObliquityOfTheEcliptic */ ε0);
            double M = Astronomical.MeanSolarAnomaly(/* julianCentury */ T);
            double C = Astronomical.SolarEquationOfTheCenter(
                /* julianCentury */ T, /* meanAnomaly */ M);
            double λ = Astronomical.ApparentSolarLongitude(
                /* julianCentury */ T, /* meanLongitude */ L0);
            double δ = solar.Declination;
            double α = DoubleUtil.UnwindAngle(solar.RightAscension);

            Assert.IsTrue(T.IsWithin(0.00000000001, (-0.072183436)));
            Assert.IsTrue(L0.IsWithin(0.00001, (201.80720)));
            Assert.IsTrue(ε0.IsWithin(0.00001, (23.44023)));
            Assert.IsTrue(εapp.IsWithin(0.00001, (23.43999)));
            Assert.IsTrue(M.IsWithin(0.00001, (278.99397)));
            Assert.IsTrue(C.IsWithin(0.00001, (-1.89732)));

            // lower accuracy than desired
            Assert.IsTrue(λ.IsWithin(0.00002, (199.90895)));
            Assert.IsTrue(δ.IsWithin(0.00001, (-7.78507)));
            Assert.IsTrue(α.IsWithin(0.00001, (198.38083)));

            // values from Astronomical Algorithms page 88

            jd = CalendricalHelper.JulianDay(/* year */ 1987, /* month */ 4, /* day */ 10);
            solar = new SolarCoordinates(/* JulianDay */ jd);
            T = CalendricalHelper.JulianCentury(/* JulianDay */ jd);

            double θ0 = Astronomical.MeanSiderealTime(/* julianCentury */ T);
            double θapp = solar.ApparentSiderealTime;
            double Ω = Astronomical.AscendingLunarNodeLongitude(/* julianCentury */ T);
            ε0 = Astronomical.MeanObliquityOfTheEcliptic(/* julianCentury */ T);
            L0 = Astronomical.MeanSolarLongitude(/* julianCentury */ T);
            double Lp = Astronomical.MeanLunarLongitude(/* julianCentury */ T);
            double ΔΨ = Astronomical.NutationInLongitude(/* julianCentury */ T,
                /* solarLongitude */ L0, /* lunarLongitude */ Lp, /* ascendingNode */ Ω);
            double Δε = Astronomical.NutationInObliquity(/* julianCentury */ T,
                /* solarLongitude */ L0, /* lunarLongitude */ Lp, /* ascendingNode */ Ω);
            double ε = ε0 + Δε;

            Assert.IsTrue(θ0.IsWithin(0.000001, (197.693195)));
            Assert.IsTrue(θapp.IsWithin(0.0001, (197.6922295833)));

            // values from Astronomical Algorithms page 148

            Assert.IsTrue(Ω.IsWithin(0.0001, (11.2531)));
            Assert.IsTrue(ΔΨ.IsWithin(0.0001, (-0.0010522)));
            Assert.IsTrue(Δε.IsWithin(0.00001, (0.0026230556)));
            Assert.IsTrue(ε0.IsWithin(0.000001, (23.4409463889)));
            Assert.IsTrue(ε.IsWithin(0.00001, (23.4435694444)));
        }

        [TestMethod]
        public void RightAscensionEdgeCase()
        {
            SolarTime previousTime = null;
            Coordinates coordinates = new Coordinates(35 + 47.0 / 60.0, -78 - 39.0 / 60.0);
            for (int i = 0; i < 365; i++)
            {
                SolarTime time = new SolarTime(
                    TestUtils.MakeDateWithOffset(2016, 1, 1, i), coordinates);
                if (i > 0)
                {
                    // transit from one day to another should not differ more than one minute
                    Assert.IsTrue(Math.Abs(time.Transit - previousTime.Transit) < (1.0 / 60.0));

                    // sunrise and sunset from one day to another should not differ more than two minutes
                    Assert.IsTrue(Math.Abs(time.Sunrise - previousTime.Sunrise) < (2.0 / 60.0));
                    Assert.IsTrue(Math.Abs(time.Sunset - previousTime.Sunset) < (2.0 / 60.0));
                }
                previousTime = time;
            }
        }

        [TestMethod]
        public void AltitudeOfCelestialBody()
        {
            double φ = 38 + (55 / 60.0) + (17.0 / 3600);
            double δ = -6 - (43 / 60.0) - (11.61 / 3600);
            double H = 64.352133;
            double h = Astronomical.AltitudeOfCelestialBody(
                /* observerLatitude */ φ, /* declination */ δ, /* localHourAngle */ H);
            Assert.IsTrue(h.IsWithin(0.0001, 15.1249));
        }

        [TestMethod]
        public void TransitAndHourAngle()
        {
            // values from Astronomical Algorithms page 103
            double longitude = -71.0833;
            double Θ = 177.74208;
            double α1 = 40.68021;
            double α2 = 41.73129;
            double α3 = 42.78204;
            double m0 = Astronomical.ApproximateTransit(longitude,
                /* siderealTime */ Θ, /* rightAscension */ α2);

            Assert.IsTrue(m0.IsWithin(0.00001, 0.81965));

            double transit = Astronomical.CorrectedTransit(
                /* approximateTransit */ m0, longitude, /* siderealTime */ Θ,
                /* rightAscension */ α2, /* previousRightAscension */ α1,
                /* nextRightAscension */ α3) / 24;

            Assert.IsTrue(transit.IsWithin(0.00001, 0.81980));

            double δ1 = 18.04761;
            double δ2 = 18.44092;
            double δ3 = 18.82742;

            double rise = Astronomical.CorrectedHourAngle(/* approximateTransit */ m0,
                /* angle */ -0.5667, new Coordinates(/* latitude */ 42.3333, longitude),
                /* afterTransit */ false, /* siderealTime */ Θ,
                /* rightAscension */ α2, /* previousRightAscension */ α1,
                /* nextRightAscension */ α3, /* declination */ δ2,
                /* previousDeclination */ δ1, /* nextDeclination */ δ3) / 24;
            Assert.IsTrue(rise.IsWithin(0.00001, 0.51766));
        }

        [TestMethod]
        public void SolarTime()
        {
            /*
             * Comparison values generated from
             * http://aa.usno.navy.mil/rstt/onedaytable?form=1&ID=AA&year=2015&month=7&day=12&state=NC&place=raleigh
             */

            Coordinates coordinates = new Coordinates(35 + 47.0 / 60.0, -78 - 39.0 / 60.0);
            SolarTime solar = new SolarTime(TestUtils.MakeDate(2015, 7, 12), coordinates);

            double transit = solar.Transit;
            double sunrise = solar.Sunrise;
            double sunset = solar.Sunset;
            double twilightStart = solar.HourAngle(-6, /* afterTransit */ false);
            double twilightEnd = solar.HourAngle(-6, /* afterTransit */ true);
            double invalid = solar.HourAngle(-36, /* afterTransit */ true);
            Assert.IsTrue(TimeString(twilightStart) == "9:38");
            Assert.IsTrue(TimeString(sunrise) == "10:08");
            Assert.IsTrue(TimeString(transit) == "17:20");
            Assert.IsTrue(TimeString(sunset) == "24:32");
            Assert.IsTrue(TimeString(twilightEnd) == "25:02");
            Assert.IsTrue(TimeString(invalid) == "");
        }

        [TestMethod]
        public void CalendricalDate()
        {
            // generated from http://aa.usno.navy.mil/data/docs/RS_OneYear.php for KUKUIHAELE, HAWAII
            Coordinates coordinates = new Coordinates(
                /* latitude */ 20 + 7.0 / 60.0, /* longitude */ -155.0 - 34.0 / 60.0);
            SolarTime day1solar = new SolarTime(TestUtils.MakeDate(2015, 4, /* day */ 2), coordinates);
            SolarTime day2solar = new SolarTime(TestUtils.MakeDate(2015, 4, 3), coordinates);

            double day1 = day1solar.Sunrise;
            double day2 = day2solar.Sunrise;

            Assert.IsTrue(TimeString(day1) == "16:15");
            Assert.IsTrue(TimeString(day2) == "16:14");
        }

        [TestMethod]
        public void Interpolation()
        {
            // values from Astronomical Algorithms page 25
            double interpolatedValue = Astronomical.Interpolate(/* value */ 0.877366,
                /* previousValue */ 0.884226, /* nextValue */ 0.870531, /* factor */ 4.35 / 24);
            Assert.IsTrue(interpolatedValue.IsWithin(0.000001, 0.876125));

            double i1 = Astronomical.Interpolate(
                /* value */ 1, /* previousValue */ -1, /* nextValue */ 3, /* factor */ 0.6);
            Assert.IsTrue(i1.IsWithin(0.000001, 2.2));
        }

        [TestMethod]
        public void AngleInterpolation()
        {
            double i1 = Astronomical.InterpolateAngles(/* value */ 1, /* previousValue */ -1,
                /* nextValue */ 3, /* factor */ 0.6);
            Assert.IsTrue(i1.IsWithin(0.000001, 2.2));

            double i2 = Astronomical.InterpolateAngles(/* value */ 1, /* previousValue */ 359,
                /* nextValue */ 3, /* factor */ 0.6);
            Assert.IsTrue(i2.IsWithin(0.000001, 2.2));
        }

        [TestMethod]
        public void JulianDay()
        {
            /*
             * Comparison values generated from http://aa.usno.navy.mil/data/docs/JulianDate.php
             */

            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2010, /* month */ 1, /* day */ 2)
                .IsWithin(0.00001, 2455198.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2011, /* month */ 2, /* day */ 4)
                .IsWithin(0.00001, 2455596.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2012, /* month */ 3, /* day */ 6)
                .IsWithin(0.00001, 2455992.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2013, /* month */ 4, /* day */ 8)
                .IsWithin(0.00001, 2456390.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2014, /* month */ 5, /* day */ 10)
                .IsWithin(0.00001, 2456787.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2015, /* month */ 6, /* day */ 12)
                .IsWithin(0.00001, 2457185.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2016, /* month */ 7, /* day */ 14)
                .IsWithin(0.00001, 2457583.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2017, /* month */ 8, /* day */ 16)
                .IsWithin(0.00001, 2457981.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2018, /* month */ 9, /* day */ 18)
                .IsWithin(0.00001, 2458379.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2019, /* month */ 10, /* day */ 20)
                .IsWithin(0.00001, 2458776.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2020, /* month */ 11, /* day */ 22)
                .IsWithin(0.00001, 2459175.500000));
            Assert.IsTrue(CalendricalHelper.JulianDay(/* year */ 2021, /* month */ 12, /* day */ 24)
                .IsWithin(0.00001, 2459572.500000));

            double jdVal = 2457215.67708333;
            Assert.IsTrue(
                CalendricalHelper.JulianDay(/* year */ 2015, /* month */ 7, /* day */ 12, /* hours */ 4.25)
                .IsWithin(0.000001, jdVal));

            DateTime components = TestUtils.MakeDate(/* year */ 2015, /* month */ 7, /* day */ 12,
                /* hour */ 4, /* minute */ 15);
            Assert.IsTrue(CalendricalHelper.JulianDay(components).IsWithin(0.000001, jdVal));

            Assert.IsTrue(CalendricalHelper
                .JulianDay(/* year */ 2015, /* month */ 7, /* day */ 12, /* hours */ 8.0)
                .IsWithin(0.000001, 2457215.833333));
            Assert.IsTrue(CalendricalHelper
                .JulianDay(/* year */ 1992, /* month */ 10, /* day */ 13, /* hours */ 0.0)
                .IsWithin(0.000001, 2448908.5));
        }

        [TestMethod]
        public void JulianHours()
        {
            double j1 = CalendricalHelper.JulianDay(/* year */ 2010, /* month */ 1, /* day */ 3);
            double j2 = CalendricalHelper.JulianDay(/* year */ 2010,
                /* month */ 1, /* day */ 1, /* hours */ 48);
            Assert.IsTrue(j1.IsWithin(0.0000001, j2));
        }

        [TestMethod]
        public void LeapYear()
        {
            Assert.IsFalse(CalendarUtil.IsLeapYear(2015));
            Assert.IsTrue(CalendarUtil.IsLeapYear(2016));
            Assert.IsTrue(CalendarUtil.IsLeapYear(1600));
            Assert.IsTrue(CalendarUtil.IsLeapYear(2000));
            Assert.IsTrue(CalendarUtil.IsLeapYear(2400));
            Assert.IsFalse(CalendarUtil.IsLeapYear(1700));
            Assert.IsFalse(CalendarUtil.IsLeapYear(1800));
            Assert.IsFalse(CalendarUtil.IsLeapYear(1900));
            Assert.IsFalse(CalendarUtil.IsLeapYear(2100));
            Assert.IsFalse(CalendarUtil.IsLeapYear(2200));
            Assert.IsFalse(CalendarUtil.IsLeapYear(2300));
            Assert.IsFalse(CalendarUtil.IsLeapYear(2500));
            Assert.IsFalse(CalendarUtil.IsLeapYear(2600));
        }

        private string TimeString(double when)
        {
            TimeComponents components = TimeComponents.FromDouble(when);
            if (components == null)
            {
                return "";
            }

            int minutes = (int)(components.Minutes + Math.Round(components.Seconds / 60.0));
            return string.Format("{0}:{1}", components.Hours.ToString(), minutes.ToString("D2"));
        }
    }
}
