using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TournamentsModels
{
    public static class TUtils
    {
        public static bool intourney(ApplicationUser usr,IQueryable<Team> teams,string tourid)
        {
            teams = teams.Where(x => x.tourid == tourid);
            foreach (var team in teams)
            {
                foreach (var user in team.users)
                {
                    if (user == usr)
                        return true;
                }
            }
            return false;
        }

        public static Team GetTeam(ApplicationUser usr, IQueryable<Team> teams, string tourid)
        {
            teams = teams.Where(x => x.tourid == tourid);
            foreach (var team in teams)
            {
                foreach (var user in team.users)
                {
                    if (user == usr)
                        return team;
                }
            }
            return null;
        }

        public static tournament GetTournament(string tourid,IEnumerable<tournament> tours)
        {            
            foreach (var tour in tours)
            {
                if (tour.id == tourid)
                    return tour;
            }
            return null;
        }
    }
}
