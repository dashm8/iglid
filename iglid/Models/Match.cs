using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models
{
    public class Match
    {
        public string Id { get; set; }    
        public virtual Team t1 { get; set; }  
        public virtual Team t2 { get; set; }
        public DateTime date { get; set; }
        public int t1score { get; set; }
        public int t2score { get; set; }
        public outcome outcome { get; set; }
        public BestOf bestof { get; set; }
        public Maps maps { get; set; }
        public Modes modes {get;set;}

        public Match()
        {

        }
        public Match(Team t1, Team t2, DateTime date,BestOf bestof,Modes modes, outcome outcome=outcome.wait)
        {
            this.t1 = t1;
            this.t2 = t2;
            this.bestof = bestof;
            this.modes = modes;
            this.date = date;
            this.outcome = outcome;
        }
        public Match(Team t1, DateTime date,BestOf bestof,Modes modes,outcome outcome = outcome.wait)
        {
            this.t1 = t1;
            this.date = date;
            this.bestof = bestof;
            this.modes = modes;
            this.outcome = outcome;
        }

    }
    public enum outcome { win, lose, progress, wait ,pending, dispute }//win and lose are reltive to t1
    //pending: wating for another team
    //progress: waiting for report 
    //wait: waiting for other team to report 
    //dispute: motherfuckres wont be nice
    public enum BestOf {one=1,three=3,five=5}
    public enum Maps {a,b,c}//needs to be not hardcoded
    public enum Modes { a,b,c}//needs to be not hardcoded
}
