using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TeamViewModels
{
    public class JoinViewModel
    {
        [Required]
        [Display(Name = "requests massage")]
        public string massage { get; set; }
    }
}
