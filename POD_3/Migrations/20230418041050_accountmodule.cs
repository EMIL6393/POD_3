using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POD_3.Migrations
{
    public partial class accountmodule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialAccountType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialAccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSocialAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialAccountTypeId = table.Column<int>(type: "int", nullable: false),
                    LoginId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EncryptedPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SubscriptionName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSocialAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSocialAccount_SocialAccountType_SocialAccountTypeId",
                        column: x => x.SocialAccountTypeId,
                        principalTable: "SocialAccountType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialAccountTracker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialAccountTracker", x => x.Id);
                    table.CheckConstraint("chk_action", "Action IN ('AccountAdded', 'AccountRemoved', 'AccountPasswordChanged')");
                    table.ForeignKey(
                        name: "FK_SocialAccountTracker_UserSocialAccount_AccountId",
                        column: x => x.AccountId,
                        principalTable: "UserSocialAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SocialAccountType",
                columns: new[] { "Id", "AccountType" },
                values: new object[,]
                {
                    { 1, "Facebook" },
                    { 2, "Instagram" },
                    { 3, "Twitter" },
                    { 4, "Youtube" },
                    { 5, "LinkedIn" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialAccountTracker_AccountId",
                table: "SocialAccountTracker",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialAccount_SocialAccountTypeId",
                table: "UserSocialAccount",
                column: "SocialAccountTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialAccountTracker");

            migrationBuilder.DropTable(
                name: "UserSocialAccount");

            migrationBuilder.DropTable(
                name: "SocialAccountType");
        }
    }
}
