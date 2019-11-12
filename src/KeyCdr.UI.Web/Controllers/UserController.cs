using KeyCdr.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KeyCdr.UI.Web.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage History(JWTToken token)
        {
            var userMgr = new KeyCdr.History.UserSessionHistory();
            IList<Data.Session> history = userMgr.GetSessionsByUser(new Data.KCUser() { UserId = Guid.Parse(token.UserId) });

            /* heirarchal result set */
            /*
            var historyItems = history.Select(h => new UserHistoryItemModel() {
                CreateDate = h.Created,
                UserId = h.UserId,
                SessionId = h.SessionId,
                KeySequences = h.KeySequences.Select(ks => new UserHistoryKeySequenceItemModel() {
                    Created = ks.Created,
                    KeySequenceId = ks.KeySequenceId,
                    SourceKey = ks.SourceKey,
                    SourceType = ks.SourceType.Name
                }).ToList()
            });*/

            var historyItems = history
                .SelectMany(h => h.KeySequences
                .Select(ks => new UserHistoryItemModel() {
                    CreateDate = h.Created,
                    UserId = h.UserId,
                    SessionId = h.SessionId,
                    SequenceCount = h.KeySequences.Count,
                    KeySequenceId = ks.KeySequenceId,
                    SourceKey = ks.SourceKey,
                    SourceType = ks.SourceType.Name
                })).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, historyItems);
        }
    }
}