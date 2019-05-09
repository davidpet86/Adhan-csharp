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
    public class MathTest
    {
        [TestMethod]
        public void AngleConversion()
        {
            Assert.IsTrue(MathHelper.ToDegrees(Math.PI).IsWithin(0.00001, 180.0));
            Assert.IsTrue(MathHelper.ToDegrees(Math.PI / 2).IsWithin(0.00001, 90.0));
        }

        [TestMethod]
        public void Normalizing()
        {
            Assert.IsTrue(DoubleUtil.NormalizeWithBound(2.0, -5).IsWithin(0.00001, -3));
            Assert.IsTrue(DoubleUtil.NormalizeWithBound(-4.0, -5.0).IsWithin(0.00001, -4));
            Assert.IsTrue(DoubleUtil.NormalizeWithBound(-6.0, -5.0).IsWithin(0.00001, -1));

            Assert.IsTrue(DoubleUtil.NormalizeWithBound(-1.0, 24).IsWithin(0.00001, 23));
            Assert.IsTrue(DoubleUtil.NormalizeWithBound(1.0, 24.0).IsWithin(0.00001, 1));
            Assert.IsTrue(DoubleUtil.NormalizeWithBound(49.0, 24).IsWithin(0.00001, 1));

            Assert.IsTrue(DoubleUtil.NormalizeWithBound(361.0, 360).IsWithin(0.00001, 1));
            Assert.IsTrue(DoubleUtil.NormalizeWithBound(360.0, 360).IsWithin(0.00001, 0));
            Assert.IsTrue(DoubleUtil.NormalizeWithBound(259.0, 360).IsWithin(0.00001, 259));
            Assert.IsTrue(DoubleUtil.NormalizeWithBound(2592.0, 360).IsWithin(0.00001, 72));

            Assert.IsTrue(DoubleUtil.UnwindAngle(-45.0).IsWithin(0.00001, 315));
            Assert.IsTrue(DoubleUtil.UnwindAngle(361.0).IsWithin(0.00001, 1));
            Assert.IsTrue(DoubleUtil.UnwindAngle(360.0).IsWithin(0.00001, 0));
            Assert.IsTrue(DoubleUtil.UnwindAngle(259.0).IsWithin(0.00001, 259));
            Assert.IsTrue(DoubleUtil.UnwindAngle(2592.0).IsWithin(0.00001, 72));

            Assert.IsTrue(DoubleUtil.NormalizeWithBound(360.1, 360).IsWithin(0.01, 0.1));
        }

        [TestMethod]
        public void ClosestAngle()
        {
            Assert.IsTrue(DoubleUtil.ClosestAngle(360.0).IsWithin(0.000001, 0));
            Assert.IsTrue(DoubleUtil.ClosestAngle(361.0).IsWithin(0.000001, 1));
            Assert.IsTrue(DoubleUtil.ClosestAngle(1.0).IsWithin(0.000001, 1));
            Assert.IsTrue(DoubleUtil.ClosestAngle(-1.0).IsWithin(0.000001, -1));
            Assert.IsTrue(DoubleUtil.ClosestAngle(-181.0).IsWithin(0.000001, 179));
            Assert.IsTrue(DoubleUtil.ClosestAngle(180.0).IsWithin(0.000001, 180));
            Assert.IsTrue(DoubleUtil.ClosestAngle(359.0).IsWithin(0.000001, -1));
            Assert.IsTrue(DoubleUtil.ClosestAngle(-359.0).IsWithin(0.000001, 1));
            Assert.IsTrue(DoubleUtil.ClosestAngle(1261.0).IsWithin(0.000001, -179));
            Assert.IsTrue(DoubleUtil.ClosestAngle(-360.1).IsWithin(0.01, -0.1));
        }

        [TestMethod]
        public void TimeComponent()
        {
            TimeComponents comps1 = TimeComponents.FromDouble(15.199);
            Assert.IsNotNull(comps1);
            Assert.IsTrue(comps1.Hours == 15);
            Assert.IsTrue(comps1.Minutes == 11);
            Assert.IsTrue(comps1.Seconds == 56);

            TimeComponents comps2 = TimeComponents.FromDouble(1.0084);
            Assert.IsNotNull(comps2);
            Assert.IsTrue(comps2.Hours == 1);
            Assert.IsTrue(comps2.Minutes == 0);
            Assert.IsTrue(comps2.Seconds == 30);

            TimeComponents comps3 = TimeComponents.FromDouble(1.0083);
            Assert.IsNotNull(comps3);
            Assert.IsTrue(comps3.Hours == 1);
            Assert.IsTrue(comps3.Minutes == 0);

            TimeComponents comps4 = TimeComponents.FromDouble(2.1);
            Assert.IsNotNull(comps4);
            Assert.IsTrue(comps4.Hours == 2);
            Assert.IsTrue(comps4.Minutes == 6);

            TimeComponents comps5 = TimeComponents.FromDouble(3.5);
            Assert.IsNotNull(comps5);
            Assert.IsTrue(comps5.Hours == 3);
            Assert.IsTrue(comps5.Minutes == 30);
        }

        [TestMethod]
        public void MinuteRounding()
        {
            DateTime comps1 = TestUtils.MakeDate(2015, 1, 1, 10, 2, 29);
            DateTime rounded1 = CalendarUtil.RoundedMinute(comps1);
            Assert.IsTrue(rounded1.Minute == 2);
            Assert.IsTrue(rounded1.Second == 0);

            DateTime comps2 = TestUtils.MakeDate(2015, 1, 1, 10, 2, 31);
            DateTime rounded2 = CalendarUtil.RoundedMinute(comps2);
            Assert.IsTrue(rounded2.Minute == 3);
            Assert.IsTrue(rounded2.Second == 0);
        }
    }
}
