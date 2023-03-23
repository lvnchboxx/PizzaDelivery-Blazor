using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steamgames.Model
{
    public class Games
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Game
        {
            public int appid { get; set; }
            public string name { get; set; }
            public int playtime_forever { get; set; }
            public string img_icon_url { get; set; }
            public string img_logo_url { get; set; }
            public bool has_community_visible_stats { get; set; }
        }

        public class Response
        {
            public int game_count { get; set; }
            public List<Game> games { get; set; }
        }

        public class Root
        {
            public Response response { get; set; }
        }
    }
}
