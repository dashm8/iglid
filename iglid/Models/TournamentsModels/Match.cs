using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TournamentsModels
{
    public class Match
    {
        public string matchid { get; set; }
        public TourEnums.status status { get; set; }
        public string t1id { get; set; }
        public TourEnums.outcome t1rep { get; set; }
        public string t2id { get; set; }
        public TourEnums.outcome t2rep { get; set; }
        public string tourid { get; set; }
        
    }
}
