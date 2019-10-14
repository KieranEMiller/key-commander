using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Highlighting;
using NUnit.Framework;

namespace KeyCdr.Tests.HighlightingTests
{
    [TestFixture]
    public class HighlightDetailTests
    {
        [Test]
        public void equality_fails_when_null()
        {
            HighlightDetail obj1 = new HighlightDetail() {
                HighlightType = HighlightType.ExtraChars, IndexEnd = 1, IndexStart = 0
            };
            HighlightDetail obj2 = null;
            Assert.That(obj1, Is.Not.EqualTo(obj2));
        }

        [Test]
        public void equality_fails_when_wrong_obj_type()
        {
            HighlightDetail obj1 = new HighlightDetail() {
                HighlightType = HighlightType.ExtraChars, IndexEnd = 1, IndexStart = 0
            };
            string obj2 = null;
            Assert.That(obj1, Is.Not.EqualTo(obj2));
        }

        [Test]
        public void equality_fails_when_one_property_incorrect()
        {
            HighlightDetail obj1 = new HighlightDetail() {
                HighlightType = HighlightType.ExtraChars, IndexEnd = 1, IndexStart = 0
            };
            HighlightDetail obj2 = new HighlightDetail() {
                HighlightType = HighlightType.ExtraChars, IndexEnd = 5, IndexStart = 0
            };
            Assert.That(obj1, Is.Not.EqualTo(obj2));
        }

        [Test]
        public void equality_true_when_all_properties_same()
        {
            HighlightDetail obj1 = new HighlightDetail() {
                HighlightType = HighlightType.ExtraChars, IndexEnd = 5, IndexStart = 0
            };
            HighlightDetail obj2 = new HighlightDetail() {
                HighlightType = HighlightType.ExtraChars, IndexEnd = 5, IndexStart = 0
            };
            Assert.That(obj1, Is.EqualTo(obj2));
        }
    }
}
