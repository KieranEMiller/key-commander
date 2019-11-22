using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Data;
using KeyCdr.Models;
using NUnit.Framework;

namespace KeyCdr.Tests.ModelTests
{
    [TestFixture]
    public class AccuracyModelTests
    {
        [Test]
        public void precision_applied_to_all_properties()
        {
            decimal repeatingDec = (decimal)6 / (decimal)9;
            var model = new AccuracyModel().Create(new AnalysisAccuracy() {
                Accuracy = repeatingDec,
            });

            double repeatingDouble = (double)4 / (double)7;
            model.NumChars = repeatingDouble;
            model.NumCorrectChars = repeatingDouble;
            model.NumExtraChars = repeatingDouble;
            model.NumIncorrectChars = repeatingDouble;
            model.NumShortChars = repeatingDouble;
            model.NumWords = repeatingDouble;

            int expectedPrecision = Constants.PRECISION_FOR_DECIMALS;

            //add 2 to account for decimal and leading 0 in ones place
            expectedPrecision += 2;

            Assert.That(model.Accuracy.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
            Assert.That(model.NumChars.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
            Assert.That(model.NumCorrectChars.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
            Assert.That(model.NumExtraChars.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
            Assert.That(model.NumIncorrectChars.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
            Assert.That(model.NumShortChars.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
            Assert.That(model.NumWords.ToString().Length, Is.LessThanOrEqualTo(expectedPrecision));
        }

        [Test]
        public void setting_properties_sets_correct()
        {
            var model = new AccuracyModel().Create(new AnalysisAccuracy() {
                Accuracy = 1,
                NumChars = 2,
                NumCorrectChars = 3,
                NumExtraChars = 4,
                NumIncorrectChars = 5,
                NumShortChars = 6, 
                NumWords = 7,
            });

            Assert.That(model.Accuracy, Is.EqualTo(1M));
            Assert.That(model.NumChars, Is.EqualTo(2));
            Assert.That(model.NumCorrectChars, Is.EqualTo(3));
            Assert.That(model.NumExtraChars, Is.EqualTo(4));
            Assert.That(model.NumIncorrectChars, Is.EqualTo(5));
            Assert.That(model.NumShortChars, Is.EqualTo(6));
            Assert.That(model.NumWords, Is.EqualTo(7));
        }
    }
}
