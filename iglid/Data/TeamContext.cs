using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using iglid.Models;


namespace iglid.Data
{
    public class TeamContext : DbContext    
    {
        public TeamContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Team> teams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityUserRole<string>>();
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityUser<string>>();
            builder.Ignore<ApplicationUser>();
        }
    }
}
