using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid.Models.TournamentsModels
{
    public static class TourEnums
    {
        public enum GameModes
        {
            variant,//all of them
            snd,
            hp,
            ctf
        }

        public enum type
        {
            onevone,//1v1
            twovtwo,//2v2
            threevthree,//3v3
            fourvfour//3v3   
        }

        public enum bestof
        {
            one,
            three,
            five
        }

        public enum status
        {
            start,//no one reported
            end, //everyone reported the same
            dispute,//everyone reported but not the same
            wait,//waiting for other team to report
        }

        public enum outcome
        {
            iwon,
            hewon
        }
    }
}
