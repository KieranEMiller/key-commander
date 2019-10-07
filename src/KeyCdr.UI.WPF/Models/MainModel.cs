using KeyCdr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.UI.WPF.Models
{
    public class MainModel
    {
        public MainModel()
        { }

        public KCUser User { get; set; }
        public IList<Session> RecentSessions { get; set; }
    }
}
