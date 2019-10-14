using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using KeyCdr.Highlighting;
using KeyCdr.UI.WPF.ViewModels;
using NUnit.Framework;

namespace KeyCdr.UI.WPF.Tests
{
    [TestFixture]
    public class TextSequenceInputViewModelTests
    {
        //[Test]
        public void vm_test()
        {
            string text = "abc";
            IList<HighlightDetail> details = new List<HighlightDetail>() {
                new HighlightDetail() {
                    HighlightType=HighlightType.IncorrectChar,
                    IndexStart=1,
                    IndexEnd=2
                }
            };

            TextSequenceInputViewModel vm = new TextSequenceInputViewModel();
            IList<Inline> inlines = vm.ConvertHighlightsToInlines(text, details);

        }
    }
}
