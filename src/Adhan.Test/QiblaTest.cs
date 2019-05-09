using Microsoft.VisualStudio.TestTools.UnitTesting;

using Batoulapps.Adhan;
using Batoulapps.Adhan.Internal;
using Adhan.Test.Internal;

namespace Adhan.Test
{
    [TestClass]
    public class QiblaTest
    {
        [TestMethod]
        public void QiblaTestNAWashingtonDC()
        {
            Coordinates washingtonDC = new Coordinates(38.9072, -77.0369);
            Assert.IsTrue((new Qibla(washingtonDC).Direction).IsWithin(0.001, 56.560));
        }

        [TestMethod]
        public void QiblaTestNANYC()
        {
            Coordinates nyc = new Coordinates(40.7128, -74.0059);
            Assert.IsTrue((new Qibla(nyc).Direction).IsWithin(0.001, 58.481));
        }

        [TestMethod]
        public void QiblaTestNASanFrancisco()
        {
            Coordinates sanFrancisco = new Coordinates(37.7749, -122.4194);
            Assert.IsTrue((new Qibla(sanFrancisco).Direction).IsWithin(0.001, 18.843));
        }

        [TestMethod]
        public void QiblaTestNAAnchorage()
        {
            Coordinates anchorage = new Coordinates(61.2181, -149.9003);
            Assert.IsTrue((new Qibla(anchorage).Direction).IsWithin(0.001, 350.883));              
        }

        [TestMethod]
        public void QiblaTestSouthPacificSydney()
        {
            Coordinates sydney = new Coordinates(-33.8688, 151.2093);
            Assert.IsTrue((new Qibla(sydney).Direction).IsWithin(0.001, 277.499));
        }

        [TestMethod]
        public void QiblaTestSouthPacificAuckland()
        {
            Coordinates auckland = new Coordinates(-36.8485, 174.7633);
            Assert.IsTrue((new Qibla(auckland).Direction).IsWithin(0.001, 261.197));
        }

        [TestMethod]
        public void QiblaTestEuropeLondon()
        {
            Coordinates london = new Coordinates(51.5074, -0.1278);
            Assert.IsTrue((new Qibla(london).Direction).IsWithin(0.001, 118.987));
        }

        [TestMethod]
        public void QiblaTestEuropeParis()
        {
            Coordinates paris = new Coordinates(48.8566, 2.3522);
            Assert.IsTrue((new Qibla(paris).Direction).IsWithin(0.001, 119.163));
        }

        [TestMethod]
        public void QiblaTestEuropeOslo()
        {
            Coordinates oslo = new Coordinates(59.9139, 10.7522);
            Assert.IsTrue((new Qibla(oslo).Direction).IsWithin(0.001, 139.027));
        }

        [TestMethod]
        public void QiblaTestAsiaIslamabad()
        {
            Coordinates islamabad = new Coordinates(33.7294, 73.0931);
            Assert.IsTrue((new Qibla(islamabad).Direction).IsWithin(0.001, 255.882));
        }

        [TestMethod]
        public void QiblaTestAsiaTokyo()
        {
            Coordinates tokyo = new Coordinates(35.6895, 139.6917);
            Assert.IsTrue((new Qibla(tokyo).Direction).IsWithin(0.001, 293.021));
        }

        [TestMethod]
        public void QiblaTestAfricaCapeTown()
        {
            Coordinates capeTown = new Coordinates(33.9249, 18.4241);
            Assert.IsTrue((new Qibla(capeTown).Direction).IsWithin(0.001, 118.004));
        }

        [TestMethod]
        public void QiblaTestAfricaCairo()
        {
            Coordinates cairo = new Coordinates(30.0444, 31.2357);
            Assert.IsTrue((new Qibla(cairo).Direction).IsWithin(0.001, 136.137));
        }
    }    
}
