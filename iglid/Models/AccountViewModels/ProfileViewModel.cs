using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.AccountViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser user { get; internal set; }
        public bool IsMe { get; internal set; }
    }
}
