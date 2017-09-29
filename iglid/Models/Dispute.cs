using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models
{
    public class Dispute
    {

        public long Id { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string link { get; set; }

        [Required]
        [MaxLength(150)]
        public string massage { get; set; }

        public string matchid { get; set; }
    }
}
