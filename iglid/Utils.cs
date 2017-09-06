using iglid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iglid
{
    public static class Utils
    {
        public static string randommathid()
        {
            int length = 6;
            Random rnd = new Random();
            string charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
            string ret = "";
            for (int i = 0; i < length; i++)
            {
                ret += charset[rnd.Next(0, charset.Length-1)];
            }
            return ret;
        }

        //https://www.reddit.com/r/movies/comments/2tof9i/in_the_social_network_what_was_that_equation/
        //caculates the expected win for a (the excpected win for b would be (1-ret) as it is a probabilty value)
        public static double excepectwin(int a,int b)
        {
            return 1 / (1 + 10 ^ ((b - a) / 400));
        }

        public static bool UserInMatch(Match match,ApplicationUser user)
        {
            foreach (var player in match.t1.players)//both teams
            {
                if (player == user)
                    return true;
            }
            foreach (var player in match.t2.players)
            {
                if (player == user)
                    return true;
            }
            return false;
        }
    }
}
