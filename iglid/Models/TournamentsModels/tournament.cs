using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TournamentsModels
{
    public class tournament
    {
        public string id { get; set; }
        public DateTime date { get; set; }
        public int maxteams { get; set; }
        public int teamcount { get; set; }
        public TourEnums.GameModes gameModes { get; set; }
        public TourEnums.type type { get; set; }
        public TourEnums.bestof bestof { get; set; }
    }
}
