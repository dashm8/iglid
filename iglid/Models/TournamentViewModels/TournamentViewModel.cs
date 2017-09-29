using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iglid.Models.TournamentsModels;

namespace iglid.Models.TournamentViewModels
{
    public class TournamentViewModel
    {
        public List<TournamentsModels.Match> matches { get; set; }
        public List<TournamentsModels.Team> Teams { get; set; }
        public string tourname { get; set; }
        public TourEnums.type type { get; set; }
        public TournamentsModels.Team myteam { get; set; }
        public bool DidStart { get; set; }
        public int maxteams { get; set; }
    }
}
