using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.GameViewModels
{
    public class CreateViewModel
    {
        public Team t1 { get; set; }
        public DateTime Date { get; set; }
        public BestOf bestof { get; set; }
        public Modes mode { get; set; }
    }
}
