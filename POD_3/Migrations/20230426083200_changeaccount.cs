using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POD_3.Migrations
{
    public partial class changeaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "SupportTickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 26, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 25, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "SocialAccountTypeId",
                table: "SocialAccountTrackers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialAccountTrackers_SocialAccountTypeId",
                table: "SocialAccountTrackers",
                column: "SocialAccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialAccountTrackers_SocialAccountTypes_SocialAccountTypeId",
                table: "SocialAccountTrackers",
                column: "SocialAccountTypeId",
                principalTable: "SocialAccountTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialAccountTrackers_SocialAccountTypes_SocialAccountTypeId",
                table: "SocialAccountTrackers");

            migrationBuilder.DropIndex(
                name: "IX_SocialAccountTrackers_SocialAccountTypeId",
                table: "SocialAccountTrackers");

            migrationBuilder.DropColumn(
                name: "SocialAccountTypeId",
                table: "SocialAccountTrackers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "SupportTickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 25, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 26, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
