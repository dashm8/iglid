using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TournamentsModels
{
    public class Team
    {
        public string id { get; set; }        
        public string team_name { get; set; }
        public ApplicationUser leader { get; set; }
        public IEnumerable<ApplicationUser> users { get; set; }
        public IEnumerable<ApplicationUser> invited { get; set; }
        public string place { get; set; }
        public List<int> placing { get; set; }
        public string tourid { get; set; }

        public Team() { }
    }
}
