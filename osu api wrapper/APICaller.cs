using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using osu_api_wrapper.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace osu_api_wrapper
{
    public class APICaller
    {
        public static string Key { private get; set; }
        public static string BaseURL { get; set; } = "https://osu.ppy.sh/api";
        public static int QueueSize { get; set; }

        public static int RateLimitPerMin { get; set; } = 1000;
        private static readonly object Lock = new object();

        private static readonly object LockReplay = new object();
        public static int RateLimitPerMinReplay { get; set; } = 10;

        // await GetBeatmaps(since: null, beatmapsetId: null, beatmapId: null, user: null, type: null, mode: null, includeConverted: null, hash: null, limit: null, mods: null);
        public static async Task<List<Beatmap>> GetBeatmaps(DateTime? since = null, int? beatmapsetId = null, int? beatmapId = null, int? user = null, UsernameType? type = null, Mode? mode = null, bool? includeConverted = null, string hash = null, [Range(0, 500)] int? limit = null, Mods? mods = null)
        {
            QueueSize++;
            lock (Lock)
            {
                if (RateLimitPerMin > 0)
                {
                    int delay = (60000 + RateLimitPerMin - 1) / RateLimitPerMin;
                    Thread.Sleep(delay);
                }
            }
            QueueSize--;

            var call = BaseURL
                .AppendPathSegment("get_beatmaps")
                .SetQueryParam("k", Key);
            if (since != null)              call.SetQueryParam("since", since.ToString());
            if (beatmapsetId != null)       call.SetQueryParam("s",     beatmapsetId);
            if (beatmapId != null)          call.SetQueryParam("b",     beatmapId);
            if (user != null)               call.SetQueryParam("u",     user);
            if (type != null)               call.SetQueryParam("type",  type);
            if (mode != null)               call.SetQueryParam("m",     mode);
            if (includeConverted != null)   call.SetQueryParam("a",     includeConverted);
            if (hash != null)               call.SetQueryParam("h",     hash);
            if (limit != null)              call.SetQueryParam("limit", limit);
            if (mods != null)               call.SetQueryParam("mods",  mods);

            return await call
                .GetAsync()
                .ReceiveJson<List<Beatmap>>();
        }

        public static async Task<List<User>> GetUser(string user, Mode? mode = null, UsernameType? type = null, int? eventDays = null)
        {
            QueueSize++;
            lock (Lock)
            {
                if (RateLimitPerMin > 0)
                {
                    int delay = (60000 + RateLimitPerMin - 1) / RateLimitPerMin;
                    Thread.Sleep(delay);
                }
            }
            QueueSize--;

            var call = BaseURL
                .AppendPathSegment("get_user")
                .SetQueryParam("k", Key);
            if (user != null) call.SetQueryParam("u", user);
            if (mode != null) call.SetQueryParam("m", mode);
            if (type != null) call.SetQueryParam("type", type);
            if (eventDays != null) call.SetQueryParam("event_days ", eventDays);

            return await call
                .GetAsync()
                .ReceiveJson<List<User>>();
        }

        public static async Task<List<ScoreInfo>> GetScores(int beatmapId, string user = null, UsernameType? type = null, Mode? mode = null, Mods? mods = null, int? limit = null)
        {
            QueueSize++;
            lock (Lock)
            {
                if (RateLimitPerMin > 0)
                {
                    int delay = (60000 + RateLimitPerMin - 1) / RateLimitPerMin;
                    Thread.Sleep(delay);
                }
            }
            QueueSize--;

            var call = BaseURL
                .AppendPathSegment("get_scores")
                .SetQueryParam("k", Key)
                .SetQueryParam("b", beatmapId);
            if (user != null) call.SetQueryParam("u", user);
            if (type != null) call.SetQueryParam("type", type);
            if (mode != null) call.SetQueryParam("m", mode);
            if (mods != null) call.SetQueryParam("mods", mods);
            if (limit != null) call.SetQueryParam("limit", limit);

            return await call
                .GetAsync()
                .ReceiveJson<List<ScoreInfo>>();
        }

        public static async Task<List<ScoreInfo>> GetUserBest(string user, UsernameType? type = null, Mode? mode = null, int? limit = null)
        {
            QueueSize++;
            lock (Lock)
            {
                if (RateLimitPerMin > 0)
                {
                    int delay = (60000 + RateLimitPerMin - 1) / RateLimitPerMin;
                    Thread.Sleep(delay);
                }
            }
            QueueSize--;

            var call = BaseURL
                .AppendPathSegment("get_user_best")
                .SetQueryParam("k", Key)
                .SetQueryParam("u", user);
            if (type != null) call.SetQueryParam("type", type);
            if (mode != null) call.SetQueryParam("m", mode);
            if (limit != null) call.SetQueryParam("limit", limit);

            return await call
                .GetAsync()
                .ReceiveJson<List<ScoreInfo>>();
        }

        public static async Task<List<ScoreInfo>> GetUserRecent(string user, UsernameType? type = null, Mode? mode = null, int? limit = null)
        {
            QueueSize++;
            lock (Lock)
            {
                if (RateLimitPerMin > 0)
                {
                    int delay = (60000 + RateLimitPerMin - 1) / RateLimitPerMin;
                    Thread.Sleep(delay);
                }
            }
            QueueSize--;

            var call = BaseURL
                .AppendPathSegment("get_user_recent")
                .SetQueryParam("k", Key)
                .SetQueryParam("u", user);
            if (type != null) call.SetQueryParam("type", type);
            if (mode != null) call.SetQueryParam("m", mode);
            if (limit != null) call.SetQueryParam("limit", limit);

            return await call
                .GetAsync()
                .ReceiveJson<List<ScoreInfo>>();
        }

        public static async Task<MatchInfo> GetMatch(int matchId)
        {
            QueueSize++;
            lock (Lock)
            {
                if (RateLimitPerMin > 0)
                {
                    int delay = (60000 + RateLimitPerMin - 1) / RateLimitPerMin;
                    Thread.Sleep(delay);
                }
            }
            QueueSize--;

            var call = BaseURL
                .AppendPathSegment("get_match")
                .SetQueryParam("k", Key)
                .SetQueryParam("mp", matchId);

            return await call
                .GetAsync()
                .ReceiveJson<MatchInfo>();
        }

        /// <summary>
        /// Not intended for batch retrievals, max 10 per minute
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="beatmapId"></param>
        /// <param name="user"></param>
        /// <param name="type"></param>
        /// <param name="mods"></param>
        /// <returns>A byte array of the LZMA stream</returns>
        public static async Task<byte[]> GetReplay(Mode mode, int beatmapId, string user, UsernameType? type = null, Mods? mods = null)
        {
            QueueSize++;
            lock (LockReplay)
            {
                lock (Lock)
                {
                    if (RateLimitPerMin > 0)
                    {
                        int delay = (60000 + RateLimitPerMin - 1) / RateLimitPerMin;
                        Thread.Sleep(delay);
                    }
                }

                if (RateLimitPerMinReplay > 0)
                {
                    int delay = (60000 + RateLimitPerMinReplay - 1) / RateLimitPerMinReplay;
                    Thread.Sleep(delay);
                }
            }
            QueueSize--;

            var call = BaseURL
                .AppendPathSegment("get_replay")
                .SetQueryParam("k", Key)
                .SetQueryParam("m", mode)
                .SetQueryParam("b", beatmapId)
                .SetQueryParam("u", user);
            if (type != null) call.SetQueryParam("type", type);
            if (mods != null) call.SetQueryParam("mods", mods);

            string json =  await call
                .GetAsync().ReceiveString();
            string content = JObject.Parse(json).Value<string>("content");
            return Convert.FromBase64String(content);
        }

    }
}
