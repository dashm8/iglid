using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using iglid.Models;


namespace iglid.Data
{
    public class GameContext : DbContext    
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
            
        }
        public DbSet<Team> teams { get; set; }
        public DbSet<Match> matches { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);            
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityUserRole<string>>();
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<Match>();
            builder.Entity<Team>()
                .HasMany(x => x.players)
                .WithOne(x => x.team);

            builder.Entity<ApplicationUser>()
                .HasOne<Team>()
                .WithOne(x => x.Leader).HasForeignKey<Team>(x => x.LeaderId);
        }
    }
}
