using Microsoft.VisualStudio.TestTools.UnitTesting;

using Batoulapps.Adhan;
using Batoulapps.Adhan.Internal;
using Adhan.Test.Internal;

namespace Adhan.Test
{
    [TestClass]
    public class CalculationParametersTest
    {
        [TestMethod]
        public void NightPortionMiddleOfTheNight()
        {
            CalculationParameters calcParams = new CalculationParameters(18.0, 18.0);
            calcParams.HighLatitudeRule = HighLatitudeRule.MIDDLE_OF_THE_NIGHT;

            Assert.IsTrue(calcParams.NightPortions().Fajr.IsWithin(0.001, 0.5));
            Assert.IsTrue(calcParams.NightPortions().Isha.IsWithin(0.001, 0.5));
        }

        [TestMethod]
        public void NightPortionSeventhOfTheNight()
        {
            CalculationParameters calcParams = new CalculationParameters(18.0, 18.0);
            calcParams.HighLatitudeRule = HighLatitudeRule.SEVENTH_OF_THE_NIGHT;

            Assert.IsTrue(calcParams.NightPortions().Fajr.IsWithin(0.001, 1.0 / 7.0));
            Assert.IsTrue(calcParams.NightPortions().Isha.IsWithin(0.001, 1.0 / 7.0));
        }

        [TestMethod]
        public void NightPortionTwilightAngle()
        {
            CalculationParameters calcParams = new CalculationParameters(10.0, 15.0);
            calcParams.HighLatitudeRule = HighLatitudeRule.TWILIGHT_ANGLE;

            Assert.IsTrue(calcParams.NightPortions().Fajr.IsWithin(0.001, 10.0 / 60.0));
            Assert.IsTrue(calcParams.NightPortions().Isha.IsWithin(0.001, 15.0 / 60.0));
        }
    }
}
