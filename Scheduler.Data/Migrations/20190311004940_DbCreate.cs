﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduler.Data.Migrations
{
    public partial class DbCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamCategories",
                columns: table => new
                {
                    TeamCategoryId = table.Column<string>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true),
                    CategoryDescription = table.Column<string>(nullable: true),
                    CategoryEmail = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(2500)),
                    ChangeDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(2190)),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCategories", x => x.TeamCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleInitial = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    PrimaryPhoneNumber = table.Column<string>(nullable: true),
                    BackupPhoneNumber = table.Column<string>(nullable: true),
                    PrimaryEmail = table.Column<string>(nullable: true),
                    BackupEmail = table.Column<string>(nullable: true),
                    PreferredContactMethod = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(1000)),
                    ChangeDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 698, DateTimeKind.Utc).AddTicks(7710)),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserName);
                    table.UniqueConstraint("AK_Users_UserId", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<string>(nullable: false),
                    TeamName = table.Column<string>(nullable: true),
                    TeamDescription = table.Column<string>(nullable: true),
                    TeamLeader = table.Column<string>(nullable: true),
                    TeamEmail = table.Column<string>(nullable: true),
                    TeamCategoryId = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(7200)),
                    ChangeDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(6890)),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_TeamCategories_TeamCategoryId",
                        column: x => x.TeamCategoryId,
                        principalTable: "TeamCategories",
                        principalColumn: "TeamCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserDetailId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Characteristic = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProficiencyLevel = table.Column<int>(nullable: false),
                    CreateUserId = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(4270)),
                    LastUpdateUserId = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(4490)),
                    ChangeDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(3950)),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserDetailId);
                    table.ForeignKey(
                        name: "FK_UserDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<string>(nullable: false),
                    TeamId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    SupportLevel = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(5040)),
                    ChangeDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(4740)),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamUsers",
                columns: table => new
                {
                    TeamId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    TeamUserId = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(9800)),
                    ChangeDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(9500)),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUsers", x => new { x.TeamId, x.UserId });
                    table.UniqueConstraint("AK_TeamUsers_TeamUserId", x => x.TeamUserId);
                    table.ForeignKey(
                        name: "FK_TeamUsers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleNotes",
                columns: table => new
                {
                    ScheduleNoteId = table.Column<string>(nullable: false),
                    ScheduleId = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(7860)),
                    CreateUserId = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(8060)),
                    LastUpdateUserId = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(7550)),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleNotes", x => x.ScheduleNoteId);
                    table.ForeignKey(
                        name: "FK_ScheduleNotes_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleNotes_ScheduleId",
                table: "ScheduleNotes",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeamId_StartDate_EndDate",
                table: "Schedules",
                columns: new[] { "TeamId", "StartDate", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_UserId_StartDate_EndDate",
                table: "Schedules",
                columns: new[] { "UserId", "StartDate", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamCategoryId",
                table: "Teams",
                column: "TeamCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_UserId",
                table: "TeamUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleNotes");

            migrationBuilder.DropTable(
                name: "TeamUsers");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TeamCategories");
        }
    }
}
