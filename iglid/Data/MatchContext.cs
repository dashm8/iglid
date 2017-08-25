using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iglid.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace iglid.Data
{
    public class MatchContext : DbContext 
    {
        public MatchContext(DbContextOptions<MatchContext> options) : base(options)
        {

        }
        public DbSet<Match> matches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
