using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iglid.Models.TournamentsModels;

namespace iglid.Data
{
    public class TournamentContext : DbContext
    {
        public TournamentContext(DbContextOptions<TournamentContext> options) 
            : base(options)
        {

        }
        public DbSet<tournament> tours { get; set; }
        public DbSet<Team> Tteams { get; set; }
        public DbSet<Match> Tmatchs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Team>().Ignore(x => x.placing);
        }


    }
}
