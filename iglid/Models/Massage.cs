using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models
{    
    public class Massage
    {
        public long id { get; set; }        
        public string content { get; set; }
        public ApplicationUser sender { get; set; }
        public Team team{ get; set; }

        public Massage()
        {

        }
        public Massage(string content, ApplicationUser sender, Team teamid)
        {
            this.content = content;
            this.sender = sender;
            this.team = teamid;
        }
    }

    
}
