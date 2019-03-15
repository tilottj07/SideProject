using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduler.Data.Migrations
{
    public partial class Indexing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(870),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(1000));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 787, DateTimeKind.Utc).AddTicks(9950),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 698, DateTimeKind.Utc).AddTicks(7710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "UserDetails",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(3950),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(4490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserDetails",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(3750),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "UserDetails",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(3440),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TeamUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(9090),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(9800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "TeamUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(8790),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(9500));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Teams",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(6470),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(7200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "Teams",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(6180),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(6890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TeamCategories",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(1560),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(2500));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "TeamCategories",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(1250),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(2190));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(4010),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(5040));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(3720),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(4740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "ScheduleNotes",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(6770),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(8060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ScheduleNotes",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(6570),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(7860));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "ScheduleNotes",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(6280),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(7550));

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserDetailId",
                table: "UserDetails",
                column: "UserDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_TeamUserId",
                table: "TeamUsers",
                column: "TeamUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_TeamId_UserId",
                table: "TeamUsers",
                columns: new[] { "TeamId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamId",
                table: "Teams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCategories_TeamCategoryId",
                table: "TeamCategories",
                column: "TeamCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ScheduleId",
                table: "Schedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleNotes_ScheduleNoteId",
                table: "ScheduleNotes",
                column: "ScheduleNoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_UserDetailId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_TeamUsers_TeamUserId",
                table: "TeamUsers");

            migrationBuilder.DropIndex(
                name: "IX_TeamUsers_TeamId_UserId",
                table: "TeamUsers");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_TeamCategories_TeamCategoryId",
                table: "TeamCategories");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ScheduleId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleNotes_ScheduleNoteId",
                table: "ScheduleNotes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(1000),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(870));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 698, DateTimeKind.Utc).AddTicks(7710),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 787, DateTimeKind.Utc).AddTicks(9950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "UserDetails",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(4490),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserDetails",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(4270),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(3750));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "UserDetails",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(3950),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(3440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TeamUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(9800),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(9090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "TeamUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(9500),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(8790));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Teams",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(7200),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(6470));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "Teams",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 703, DateTimeKind.Utc).AddTicks(6890),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 792, DateTimeKind.Utc).AddTicks(6180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TeamCategories",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(2500),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "TeamCategories",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(2190),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(1250));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(5040),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(4740),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(3720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "ScheduleNotes",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(8060),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(6770));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ScheduleNotes",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(7860),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(6570));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "ScheduleNotes",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 11, 0, 49, 39, 704, DateTimeKind.Utc).AddTicks(7550),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 11, 2, 30, 8, 793, DateTimeKind.Utc).AddTicks(6280));
        }
    }
}
