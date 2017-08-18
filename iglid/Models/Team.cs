﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models
{
    public class Team
    {
        public long ID { get; set; }
        public string TeamName { get; set; }
        public ApplicationUser Leader { get; set; }
        public List<ApplicationUser> players { get; set; }
        public List<Requests> requests { get; set; }        
        public int score { get; set; }
        public bool CanPlay { get; set; }

        public Team()
        {
        }
    }
}