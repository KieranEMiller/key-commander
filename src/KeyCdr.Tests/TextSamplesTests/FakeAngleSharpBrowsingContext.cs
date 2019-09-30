using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Tests.TextSamplesTests
{
    //this was an attempted workaround for the extension method that
    //runs OpenAsync
    //abandoning this avenue...for now
    public class FakeAngleSharpBrowsingContext : AngleSharp.IBrowsingContext
    {
        public AngleSharp.Dom.IDocument Doc { get; set; }

        public virtual AngleSharp.Dom.IDocument OpenAsync(string address)
        {
            return Doc;
        }

        public AngleSharp.Dom.IWindow Current => throw new NotImplementedException();

        public AngleSharp.Dom.IDocument Active { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public AngleSharp.Browser.Dom.IHistory SessionHistory => throw new NotImplementedException();

        public AngleSharp.Browser.Sandboxes Security => throw new NotImplementedException();

        public AngleSharp.IBrowsingContext Parent => throw new NotImplementedException();

        public AngleSharp.Dom.IDocument Creator => throw new NotImplementedException();

        public IEnumerable<object> OriginalServices => throw new NotImplementedException();

        AngleSharp.Browser.Dom.IHistory AngleSharp.IBrowsingContext.SessionHistory => throw new NotImplementedException();

        AngleSharp.Browser.Sandboxes AngleSharp.IBrowsingContext.Security => throw new NotImplementedException();

        public void AddEventListener(string type, AngleSharp.Dom.DomEventHandler callback = null, bool capture = false)
        {
            throw new NotImplementedException();
        }

        public AngleSharp.IBrowsingContext CreateChild(string name, AngleSharp.Browser.Sandboxes security)
        {
            throw new NotImplementedException();
        }

        public bool Dispatch(AngleSharp.Dom.Events.Event ev)
        {
            throw new NotImplementedException();
        }

        public AngleSharp.IBrowsingContext FindChild(string name)
        {
            throw new NotImplementedException();
        }

        public T GetService<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetServices<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public void InvokeEventListener(AngleSharp.Dom.Events.Event ev)
        {
            throw new NotImplementedException();
        }

        public void RemoveEventListener(string type, AngleSharp.Dom.DomEventHandler callback = null, bool capture = false)
        {
            throw new NotImplementedException();
        }
    }
}
