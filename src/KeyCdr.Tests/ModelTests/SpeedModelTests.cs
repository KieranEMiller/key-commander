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
        public void precision_applied_to_all_properties_with_create()
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
        public void precision_applied_to_all_properties_manual_assignment()
        {
            decimal repeatingDec = (decimal)1 / (decimal)3;
            var model = new SpeedModel() { 
                CharsPerSec = repeatingDec, 
                TotalTimeInMilliSec = repeatingDec,
                WordPerMin = repeatingDec,
            };

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

        [Test]
        public void total_time_friendly_display_format_zero_millisec()
        {
            var model = new SpeedModel() { TotalTimeInMilliSec = 0 };
            string expected = "00m 00s 000ms (0ms total)";
            Assert.That(model.TotalTimeDisplayFriendly, Is.EqualTo(expected));
        }

        [Test]
        public void total_time_friendly_display_format_millisec_only()
        {
            var model = new SpeedModel() { TotalTimeInMilliSec = 150 };
            string expected = "00m 00s 150ms (150ms total)";
            Assert.That(model.TotalTimeDisplayFriendly, Is.EqualTo(expected));
        }

        [Test]
        public void total_time_friendly_display_format_sec_only()
        {
            var model = new SpeedModel() { TotalTimeInMilliSec = 2000 };
            string expected = "00m 02s 000ms (2,000ms total)";
            Assert.That(model.TotalTimeDisplayFriendly, Is.EqualTo(expected));
        }

        [Test]
        public void total_time_friendly_display_format_min_only()
        {
            var model = new SpeedModel() { TotalTimeInMilliSec = 120000 };
            string expected = "02m 00s 000ms (120,000ms total)";
            Assert.That(model.TotalTimeDisplayFriendly, Is.EqualTo(expected));
        }

        [TestCase(1500, "00m 01s 500ms (1,500ms total)")]
        [TestCase(2500, "00m 02s 500ms (2,500ms total)")]
        [TestCase(61001, "01m 01s 001ms (61,001ms total)")]
        public void total_time_friendly_display_format_multiple_broken_accurately(
            int totalMs, string expected
            )
        {
            var model = new SpeedModel() { TotalTimeInMilliSec = totalMs };
            Assert.That(model.TotalTimeDisplayFriendly, Is.EqualTo(expected));
        }
    }
}
