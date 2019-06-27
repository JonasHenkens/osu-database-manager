using Flurl;
using Flurl.Http;
using osu_api_wrapper.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace osu_api_wrapper
{
    class APICaller
    {
        public static string Key { private get; set; }
        static string BaseURL = "https://osu.ppy.sh/api";

        // await GetBeatmaps(since: null, beatmapsetId: null, beatmapId: null, userId: null, type: null, mode: null, includeConverted: null, hash: null, limit: null, mods: null);
        public static async Task<List<Beatmap>> GetBeatmaps(DateTime? since = null, int? beatmapsetId = null, int? beatmapId = null, int? userId = null, UsernameType? type = null, Mode? mode = null, bool? includeConverted = null, string hash = null, [Range(0, 500)] int? limit = null, Mods? mods = null)
        {
            var idkaname = BaseURL
                .AppendPathSegment("get_beatmaps")
                .SetQueryParam("k", Key);
            if (since != null)              idkaname.SetQueryParam("since", since.ToString());
            if (beatmapsetId != null)       idkaname.SetQueryParam("s",     beatmapsetId);
            if (beatmapId != null)          idkaname.SetQueryParam("b",     beatmapId);
            if (userId != null)             idkaname.SetQueryParam("u",     userId);
            if (type != null)               idkaname.SetQueryParam("type",  type);
            if (mode != null)               idkaname.SetQueryParam("m",     mode);
            if (includeConverted != null)   idkaname.SetQueryParam("a",     includeConverted);
            if (hash != null)               idkaname.SetQueryParam("h",     hash);
            if (limit != null)              idkaname.SetQueryParam("limit", limit);
            if (mods != null)               idkaname.SetQueryParam("mods",  mods);

            return await idkaname
                .GetAsync()
                .ReceiveJson<List<Beatmap>>();
        }

        public static async Task<List<User>> GetUser()
        {
            throw new NotImplementedException();
        }

        public static async Task<List<ScoreInfo>> GetScores()
        {
            throw new NotImplementedException();
        }

        public static async Task<List<ScoreInfo>> GetUserBest()
        {
            throw new NotImplementedException();
        }

        public static async Task<List<ScoreInfo>> GetUserRecent()
        {
            throw new NotImplementedException();
        }

        public static async Task<List<MatchInfo>> GetMatch()
        {
            throw new NotImplementedException();
        }

        public static async void GetReplay()
        {
            throw new NotImplementedException();
        }

    }
}
