using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iglid.Data;

namespace iglid.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20170831091530_Update001")]
    partial class Update001
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

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.Property<int>("score");

                    b.Property<long?>("teamID");

                    b.HasKey("Id");

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

                    b.Property<string>("TeamName");

                    b.Property<int>("score");

                    b.HasKey("ID");

                    b.HasIndex("LeaderId")
                        .IsUnique();

                    b.ToTable("teams");
                });

            modelBuilder.Entity("iglid.Models.ApplicationUser", b =>
                {
                    b.HasOne("iglid.Models.Team", "team")
                        .WithMany("players")
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
                });
        }
    }
}
