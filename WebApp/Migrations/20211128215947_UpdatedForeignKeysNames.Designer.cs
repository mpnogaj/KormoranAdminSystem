﻿// <auto-generated />
using KormoranAdminSystemRevamped.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KormoranAdminSystemRevamped.Migrations
{
    [DbContext(typeof(KormoranContext))]
    [Migration("20211128215947_UpdatedForeignKeysNames")]
    partial class UpdatedForeignKeysNames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("discipline_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("disciplines");
                });

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("match_id");

                    b.Property<int>("StateId")
                        .HasColumnType("int(11)")
                        .HasColumnName("state_id");

                    b.Property<int>("Team1Id")
                        .HasColumnType("int(11)")
                        .HasColumnName("team_1_id");

                    b.Property<int>("Team1Score")
                        .HasColumnType("int(11)")
                        .HasColumnName("team_1_score");

                    b.Property<int>("Team2Id")
                        .HasColumnType("int(11)")
                        .HasColumnName("team_2_id");

                    b.Property<int>("Team2Score")
                        .HasColumnType("int(11)")
                        .HasColumnName("team_2_score");

                    b.Property<int>("WinnerId")
                        .HasColumnType("int(11)")
                        .HasColumnName("winner_id");

                    b.HasKey("MatchId");

                    b.HasIndex("StateId");

                    b.HasIndex("Team1Id");

                    b.HasIndex("Team2Id");

                    b.HasIndex("WinnerId");

                    b.ToTable("matches");
                });

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("state_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("states");
                });

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("team_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int(11)")
                        .HasColumnName("tournament_id");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("teams");
                });

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("tournament_id");

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int(11)")
                        .HasColumnName("discipline_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<int>("StateId")
                        .HasColumnType("int(11)")
                        .HasColumnName("state_id");

                    b.Property<string>("TournamentType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("tournament_type");

                    b.Property<string>("TournamentTypeShort")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("tournament_type_short");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("StateId");

                    b.ToTable("tournaments");
                });

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("user_id");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("fullname");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("user");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("pass");

                    b.Property<string>("Permissions")
                        .HasColumnType("json")
                        .HasColumnName("permissions");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.Match", b =>
                {
                    b.HasOne("KormoranAdminSystemRevamped.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KormoranAdminSystemRevamped.Models.Team", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KormoranAdminSystemRevamped.Models.Team", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KormoranAdminSystemRevamped.Models.Team", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");

                    b.Navigation("Team1");

                    b.Navigation("Team2");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.Team", b =>
                {
                    b.HasOne("KormoranAdminSystemRevamped.Models.Tournament", "Tournament")
                        .WithMany("Teams")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.Tournament", b =>
                {
                    b.HasOne("KormoranAdminSystemRevamped.Models.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KormoranAdminSystemRevamped.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("State");
                });

            modelBuilder.Entity("KormoranAdminSystemRevamped.Models.Tournament", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}