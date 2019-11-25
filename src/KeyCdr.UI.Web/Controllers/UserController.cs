using KeyCdr.Analytics;
using KeyCdr.Data;
using KeyCdr.History;
using KeyCdr.Models;
using KeyCdr.UI.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            var historyItems = history
                .SelectMany(h => h.KeySequence
                .Select(ks => new UserHistoryModel() {
                    CreateDate = h.Created,
                    UserId = h.UserId,
                    SessionId = h.SessionId,
                    SequenceCount = h.KeySequence.Count,
                    KeySequenceId = ks.KeySequenceId,
                    SourceKey = ks.SourceKey,
                    SourceType = ks.SourceType.Name
                })).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, historyItems);
        }

        [HttpPost]
        public HttpResponseMessage HistoryDetails([FromBody]JObject data)
        {
            JWTToken token = data["token"].ToObject<JWTToken>();
            string seqId = data["sequenceId"].ToString();

            Data.KCUser user = new Data.KCUser() { UserId = Guid.Parse(token.UserId) };
            var userMgr = new KeyCdr.History.UserSessionHistory();
            Data.KeySequence detail = userMgr.GetHistoryDetailsByKeySequence(Guid.Parse(seqId));

            AllTimeStatsCalculator allTimeStats = new AllTimeStatsCalculator(user);
            UserHistoryDetailsModel result = UserHistoryDetailsModel.Create(detail);
            result.AnalysisSpeedAllTime = allTimeStats.GetSpeedAllTime();
            result.AnalysisAccuracyAllTime = allTimeStats.GetAccuracyAllTime();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage HistoryAnalytics(JWTToken token)
        {
            var userMgr = new KeyCdr.History.UserSessionHistory();
            IList<Data.Session> history = userMgr.GetHistoryDetailsAllTime(new Data.KCUser() { UserId = Guid.Parse(token.UserId) });

            var analyses = history.SelectMany(ks => ks.KeySequence
                .SelectMany(ksa => ksa.KeySequenceAnalysis))
                .ToList();

            /* this may eventually need to be truncated to a specific 
             * period of time */
            var result = new UserHistoryAnalyticsModel();
            result.SpeedMeasurements = analyses
                .Where(a => a.AnalysisTypeId.Equals((int)AnalyticType.Speed))
                .Select(s => new SpeedModel().Create(s.AnalysisSpeed))
                .ToList();

            result.AccuracyMeasurements = analyses
                .Where(a => a.AnalysisTypeId.Equals((int)AnalyticType.Accuracy))
                .Select(s => new AccuracyModel().Create(s.AnalysisAccuracy))
                .ToList();
                    
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}