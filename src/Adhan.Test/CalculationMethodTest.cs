using Microsoft.VisualStudio.TestTools.UnitTesting;

using Batoulapps.Adhan;
using Batoulapps.Adhan.Internal;
using Adhan.Test.Internal;

namespace Adhan.Test
{
    [TestClass]
    public class CalculationMethodTest
    {
        [TestMethod]
        public void CalcuateMethodMuslimWorldLeague()
        {
            CalculationParameters calcParams = CalculationMethod.MUSLIM_WORLD_LEAGUE.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 18));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 17));
            Assert.AreEqual(calcParams.IshaInterval, 0);
            Assert.AreEqual(calcParams.Method, CalculationMethod.MUSLIM_WORLD_LEAGUE);
        }

        [TestMethod]
        public void CalcuateMethodEgyptian()
        {
            CalculationParameters calcParams = CalculationMethod.EGYPTIAN.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 20));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 18));
            Assert.AreEqual(calcParams.IshaInterval, 0);
            Assert.AreEqual(calcParams.Method, CalculationMethod.EGYPTIAN);
        }

        [TestMethod]
        public void CalcuateMethodKarachi()
        {
            CalculationParameters calcParams = CalculationMethod.KARACHI.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 18));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 18));
            Assert.AreEqual(calcParams.IshaInterval, 0);
            Assert.AreEqual(calcParams.Method, CalculationMethod.KARACHI);
        }

        [TestMethod]
        public void CalcuateMethodUmmAlQura()
        {
            CalculationParameters calcParams = CalculationMethod.UMM_AL_QURA.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 18.5));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 0));
            Assert.AreEqual(calcParams.IshaInterval, 90);
            Assert.AreEqual(calcParams.Method, CalculationMethod.UMM_AL_QURA);
        }

        [TestMethod]
        public void CalcuateMethodDubai()
        {
            CalculationParameters calcParams = CalculationMethod.DUBAI.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 18.2));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 18.2));
            Assert.AreEqual(calcParams.IshaInterval, 0);
            Assert.AreEqual(calcParams.Method, CalculationMethod.DUBAI);
        }

        [TestMethod]
        public void CalcuateMethodMoonSightingCommittee()
        {
            CalculationParameters calcParams = CalculationMethod.MOON_SIGHTING_COMMITTEE.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 18));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 18));
            Assert.AreEqual(calcParams.IshaInterval, 0);
            Assert.AreEqual(calcParams.Method, CalculationMethod.MOON_SIGHTING_COMMITTEE);
        }

        [TestMethod]
        public void CalcuateMethodNorthAmerica()
        {
            CalculationParameters calcParams = CalculationMethod.NORTH_AMERICA.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 15));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 15));
            Assert.AreEqual(calcParams.IshaInterval, 0);
            Assert.AreEqual(calcParams.Method, CalculationMethod.NORTH_AMERICA);
        }

        [TestMethod]
        public void CalcuateMethodKuwait()
        {
            CalculationParameters calcParams = CalculationMethod.KUWAIT.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 18));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 17.5));
            Assert.AreEqual(calcParams.IshaInterval, 0);
            Assert.AreEqual(calcParams.Method, CalculationMethod.KUWAIT);
        }

        [TestMethod]
        public void CalcuateMethodQatar()
        {
            CalculationParameters calcParams = CalculationMethod.QATAR.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 18));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 0));
            Assert.AreEqual(calcParams.IshaInterval, 90);
            Assert.AreEqual(calcParams.Method, CalculationMethod.QATAR);
        }

        [TestMethod]
        public void CalcuateMethodOther()
        {
            CalculationParameters calcParams = CalculationMethod.OTHER.GetParameters();
            Assert.IsTrue(calcParams.FajrAngle.IsWithin(0.000001, 0));
            Assert.IsTrue(calcParams.IshaAngle.IsWithin(0.000001, 0));
            Assert.AreEqual(calcParams.IshaInterval, 0);
            Assert.AreEqual(calcParams.Method, CalculationMethod.OTHER);
        }
    }
}
