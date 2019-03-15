﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scheduler.Data;

namespace Scheduler.Data.Migrations
{
    [DbContext(typeof(ScheduleContext))]
    [Migration("20190311024814_CreateDb")]
    partial class CreateDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("Scheduler.Domain.Schedule", b =>
                {
                    b.Property<string>("ScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("SupportLevel");

                    b.Property<string>("TeamId");

                    b.Property<string>("UserId");

                    b.HasKey("ScheduleId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("TeamId", "StartDate", "EndDate");

                    b.HasIndex("UserId", "StartDate", "EndDate");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Scheduler.Domain.ScheduleNote", b =>
                {
                    b.Property<string>("ScheduleNoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreateUserId");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("LastUpdateUserId");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Note");

                    b.Property<string>("ScheduleId");

                    b.HasKey("ScheduleNoteId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("ScheduleNoteId");

                    b.ToTable("ScheduleNotes");
                });

            modelBuilder.Entity("Scheduler.Domain.Team", b =>
                {
                    b.Property<string>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("TeamCategoryId");

                    b.Property<string>("TeamDescription");

                    b.Property<string>("TeamEmail");

                    b.Property<string>("TeamLeader");

                    b.Property<string>("TeamName");

                    b.HasKey("TeamId");

                    b.HasIndex("TeamCategoryId");

                    b.HasIndex("TeamId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Scheduler.Domain.TeamCategory", b =>
                {
                    b.Property<string>("TeamCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryDescription");

                    b.Property<string>("CategoryEmail");

                    b.Property<string>("CategoryName");

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.HasKey("TeamCategoryId");

                    b.HasIndex("TeamCategoryId");

                    b.ToTable("TeamCategories");
                });

            modelBuilder.Entity("Scheduler.Domain.TeamUser", b =>
                {
                    b.Property<string>("TeamId");

                    b.Property<string>("UserId");

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("TeamUserId")
                        .IsRequired();

                    b.HasKey("TeamId", "UserId");

                    b.HasAlternateKey("TeamUserId");

                    b.HasIndex("TeamUserId");

                    b.HasIndex("UserId");

                    b.HasIndex("TeamId", "UserId");

                    b.ToTable("TeamUsers");
                });

            modelBuilder.Entity("Scheduler.Domain.User", b =>
                {
                    b.Property<string>("UserName")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BackupEmail");

                    b.Property<string>("BackupPhoneNumber");

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleInitial");

                    b.Property<byte[]>("Photo");

                    b.Property<int?>("PreferredContactMethod");

                    b.Property<string>("PrimaryEmail");

                    b.Property<string>("PrimaryPhoneNumber");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("UserName");

                    b.HasAlternateKey("UserId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Scheduler.Domain.UserDetail", b =>
                {
                    b.Property<string>("UserDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ChangeDate");

                    b.Property<string>("Characteristic");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreateUserId");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<string>("LastUpdateUserId");

                    b.Property<int>("ProficiencyLevel");

                    b.Property<string>("UserId");

                    b.HasKey("UserDetailId");

                    b.HasIndex("UserDetailId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("Scheduler.Domain.Schedule", b =>
                {
                    b.HasOne("Scheduler.Domain.Team", "Team")
                        .WithMany("Schedules")
                        .HasForeignKey("TeamId");

                    b.HasOne("Scheduler.Domain.User", "User")
                        .WithMany("Schedules")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Scheduler.Domain.ScheduleNote", b =>
                {
                    b.HasOne("Scheduler.Domain.Schedule")
                        .WithMany("ScheduleNotes")
                        .HasForeignKey("ScheduleId");
                });

            modelBuilder.Entity("Scheduler.Domain.Team", b =>
                {
                    b.HasOne("Scheduler.Domain.TeamCategory", "TeamCategory")
                        .WithMany()
                        .HasForeignKey("TeamCategoryId");
                });

            modelBuilder.Entity("Scheduler.Domain.TeamUser", b =>
                {
                    b.HasOne("Scheduler.Domain.Team", "Team")
                        .WithMany("TeamUsers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Scheduler.Domain.User", "User")
                        .WithMany("TeamUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Scheduler.Domain.UserDetail", b =>
                {
                    b.HasOne("Scheduler.Domain.User")
                        .WithMany("UserDetails")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
