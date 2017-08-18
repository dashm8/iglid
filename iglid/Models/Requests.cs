using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models
{
    public class Requests
    {
        public long ID { get; set; }
        public ApplicationUser sender { get; set; }
        public string massage { get; set; }

        public Requests(ApplicationUser sender,string massage)
        {
            this.sender = sender;
            this.massage = massage;
        }
    }
}
