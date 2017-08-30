using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iglid.Data;
using iglid.Models;

namespace iglid.Migrations
{
    [DbContext(typeof(TeamContext))]
    [Migration("20170830140713_Update")]
    partial class Update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iglid.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PSN");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<long?>("TeamID");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.Property<int>("score");

                    b.Property<long?>("teamID");

                    b.HasKey("Id");

                    b.HasIndex("TeamID");

                    b.HasIndex("teamID");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("iglid.Models.Massage", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("content");

                    b.Property<string>("senderId");

                    b.Property<long?>("teamID");

                    b.HasKey("id");

                    b.HasIndex("senderId");

                    b.HasIndex("teamID");

                    b.ToTable("Massage");
                });

            modelBuilder.Entity("iglid.Models.Match", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("bestof");

                    b.Property<DateTime>("date");

                    b.Property<int>("maps");

                    b.Property<int>("modes");

                    b.Property<int>("outcome");

                    b.Property<long?>("t1ID");

                    b.Property<long?>("t2ID");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("t1ID");

                    b.HasIndex("t2ID");

                    b.ToTable("matches");
                });

            modelBuilder.Entity("iglid.Models.Requests", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("TeamID");

                    b.Property<string>("massage");

                    b.Property<string>("senderId");

                    b.HasKey("ID");

                    b.HasIndex("TeamID");

                    b.HasIndex("senderId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("iglid.Models.Team", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanPlay");

                    b.Property<string>("LeaderId");

                    b.Property<string>("MatchId");

                    b.Property<string>("TeamName");

                    b.Property<int>("score");

                    b.HasKey("ID");

                    b.HasIndex("LeaderId")
                        .IsUnique();

                    b.HasIndex("MatchId");

                    b.ToTable("teams");
                });

            modelBuilder.Entity("iglid.Models.ApplicationUser", b =>
                {
                    b.HasOne("iglid.Models.Team")
                        .WithMany("players")
                        .HasForeignKey("TeamID");

                    b.HasOne("iglid.Models.Team", "team")
                        .WithMany()
                        .HasForeignKey("teamID");
                });

            modelBuilder.Entity("iglid.Models.Massage", b =>
                {
                    b.HasOne("iglid.Models.ApplicationUser", "sender")
                        .WithMany("massages")
                        .HasForeignKey("senderId");

                    b.HasOne("iglid.Models.Team", "team")
                        .WithMany()
                        .HasForeignKey("teamID");
                });

            modelBuilder.Entity("iglid.Models.Match", b =>
                {
                    b.HasOne("iglid.Models.ApplicationUser")
                        .WithMany("matches")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("iglid.Models.Team", "t1")
                        .WithMany()
                        .HasForeignKey("t1ID");

                    b.HasOne("iglid.Models.Team", "t2")
                        .WithMany()
                        .HasForeignKey("t2ID");
                });

            modelBuilder.Entity("iglid.Models.Requests", b =>
                {
                    b.HasOne("iglid.Models.Team")
                        .WithMany("requests")
                        .HasForeignKey("TeamID");

                    b.HasOne("iglid.Models.ApplicationUser", "sender")
                        .WithMany()
                        .HasForeignKey("senderId");
                });

            modelBuilder.Entity("iglid.Models.Team", b =>
                {
                    b.HasOne("iglid.Models.ApplicationUser", "Leader")
                        .WithOne()
                        .HasForeignKey("iglid.Models.Team", "LeaderId");

                    b.HasOne("iglid.Models.Match")
                        .WithMany("temp")
                        .HasForeignKey("MatchId");
                });
        }
    }
}
