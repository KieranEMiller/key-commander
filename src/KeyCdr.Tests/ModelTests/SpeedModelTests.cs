using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Data;
using NUnit.Framework;
using KeyCdr.Models;

namespace KeyCdr.Tests.ModelTests
{
    [TestFixture]
    public class SpeedModelTests
    {
        [Test]
        public void precision_applied_to_all_properties()
        {
            decimal repeatingDec = (decimal)1 / (decimal)3;
            var model = new SpeedModel().Create(new AnalysisSpeed() {
                CharsPerSec = repeatingDec, 
                TotalTimeInMilliSec = repeatingDec,
                WordPerMin = repeatingDec,
            });

            int expectedPrecision = Constants.PRECISION_FOR_DECIMALS;

            //add 2 to account for decimal and leading 0 in ones place
            expectedPrecision += 2;

            Assert.That(model.CharsPerSec.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
            Assert.That(model.TotalTimeInMilliSec.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
            Assert.That(model.WordPerMin.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
        }

        [Test]
        public void create_sets_all_properties_accurately()
        {
            var model = new SpeedModel().Create(new AnalysisSpeed() {
                CharsPerSec = 1, 
                TotalTimeInMilliSec = 2,
                WordPerMin = 3,
            });

            Assert.That(model.CharsPerSec, Is.EqualTo(1M));
            Assert.That(model.TotalTimeInMilliSec, Is.EqualTo(2M));
            Assert.That(model.WordPerMin, Is.EqualTo(3M));
        }
    }
}
