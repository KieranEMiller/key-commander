using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using KeyCdr.Analytics;

namespace KeyCdr.Tests.AnalyticsTests
{
    [TestFixture]
    public class BaseAnalyticTests
    {
        [TestCase(AnalyticType.Accuracy, typeof(Accuracy))]
        [TestCase(AnalyticType.Speed, typeof(Speed))]
        public void baseanalytic_factory_returns_correct_IAnalytic_by_type(AnalyticType type, Type result)
        {
            IAnalytic analytic = BaseAnalytic.Create(type, null);
            Assert.That(analytic, Is.TypeOf(result));
        }

        //TODO: is this necessary/worthwhile to test this?
        /*[Test]
        public void base_methods_expose_data_passed_in_ctor()
        { }*/
    }
}
