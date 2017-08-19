using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TeamViewModels
{
    public class IndexViewModel
    {        
        public IEnumerable<Team> teams { get; set; }
        public bool HasTeam { get; set; }
        public long TeamId { get; set; }
        public IndexViewModel() { }


    }
}
