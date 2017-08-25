using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iglid.Data;
using iglid.Models;

namespace iglid.Migrations.Match
{
    [DbContext(typeof(MatchContext))]
    partial class MatchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iglid.Models.Match", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("bestof");

                    b.Property<DateTime>("date");

                    b.Property<int>("maps");

                    b.Property<int>("modes");

                    b.Property<int>("outcome");

                    b.Property<int>("t1score");

                    b.Property<int>("t2score");

                    b.HasKey("Id");

                    b.ToTable("matches");
                });
        }
    }
}
