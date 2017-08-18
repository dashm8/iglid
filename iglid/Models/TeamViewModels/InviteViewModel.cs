using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TeamViewModels
{
    public class InviteViewModel
    {
        public long ID { get; set; }
        public ApplicationUser invite;
        public string massage { get; set; }

    }
}
