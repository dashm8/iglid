using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models
{
    public class Team
    {
        public long ID { get; set; }
        public string TeamName { get; set; }
        public string LeaderId { get; set; }
        public ApplicationUser Leader { get; set; }
        public virtual List<ApplicationUser> players { get; set; }
        public virtual List<Requests> requests { get; set; }  
        public virtual List<Match> matches { get; set; }
        public int score { get; set; }
        public bool CanPlay { get; set; }

        public Team()
        {
           
        }
    }
}
