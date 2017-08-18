using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TeamViewModels
{
    public class CreateViewModel
    { 
        [Required]
        public string team_name { get; set; }
    }
}
