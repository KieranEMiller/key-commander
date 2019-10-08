using System;
using KeyCdr.TextSamples;

namespace KeyCdr.UI.WPF.Models
{
    public class MainModelSessionView
    {
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
        public DateTime SessionCreated { get; set; }
        public int SequenceCount { get; set; }
        public TextSampleSourceType SequenceSource { get; set; }
        public DateTime SequenceCreated { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public string Name {
            get
            {
                return string.Format("Session from {0}, with id {1}", SessionCreated.ToString(), SessionId.ToString());
            }
            set
            {

            }
        }
    }
}
