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
        public long teamid { get; set; }

        public Massage()
        {

        }
        public Massage(string content, ApplicationUser sender, long teamid)
        {
            this.content = content;
            this.sender = sender;
            this.teamid = teamid;
        }
    }

    
}
