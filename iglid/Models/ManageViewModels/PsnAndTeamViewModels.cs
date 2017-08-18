using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.ManageViewModels
{
    public class ChangePsnViewModel
    {
        [Required]
        [Display(Name ="PlayStation Account")]
        public string PSN { get; set; }
    }

    public class SetPsnViewModel
    {
        [Required]
        [Display(Name = "PlayStation Account")]
        public string PSN { get; set; }
    }
}
