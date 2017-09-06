using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iglid.Data;

namespace iglid.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20170819182610_teamid")]
    partial class teamid
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

                    b.Property<string>("Tname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.Property<int>("score");

                    b.HasKey("Id");

                    b.HasIndex("TeamID");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("iglid.Models.Massage", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("content");

                    b.Property<string>("senderId");

                    b.Property<long>("teamid");

                    b.HasKey("id");

                    b.HasIndex("senderId");

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

                    b.HasIndex("LeaderId");

                    b.ToTable("teams");
                });

            modelBuilder.Entity("iglid.Models.ApplicationUser", b =>
                {
                    b.HasOne("iglid.Models.Team")
                        .WithMany("players")
                        .HasForeignKey("TeamID");
                });

            modelBuilder.Entity("iglid.Models.Massage", b =>
                {
                    b.HasOne("iglid.Models.ApplicationUser", "sender")
                        .WithMany("massages")
                        .HasForeignKey("senderId");
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
                        .WithMany()
                        .HasForeignKey("LeaderId");
                });
        }
    }
}
