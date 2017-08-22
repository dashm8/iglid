using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TeamViewModels
{
    public class ProfileViewModel
    {
        public Team team { get; set; }
        public bool IsCurrentUserLeader { get; set; }

        public ProfileViewModel() { }
    }
}
