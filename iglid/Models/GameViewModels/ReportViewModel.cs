using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.GameViewModels
{
    public class ReportViewModel
    {
        public int t1score { get; set; }
        public int t2score { get; set; }
        public string t1name { get; set; }
        public string t2name { get; set; }
        public ReportViewModel() { }
    }
}
