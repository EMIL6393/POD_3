using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POD_3.Migrations
{
    public partial class contntmodule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialAccountTracker_UserSocialAccount_AccountId",
                table: "SocialAccountTracker");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSocialAccount_SocialAccountType_SocialAccountTypeId",
                table: "UserSocialAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSocialAccount",
                table: "UserSocialAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialAccountType",
                table: "SocialAccountType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialAccountTracker",
                table: "SocialAccountTracker");

            migrationBuilder.RenameTable(
                name: "UserSocialAccount",
                newName: "UserSocialAccounts");

            migrationBuilder.RenameTable(
                name: "SocialAccountType",
                newName: "SocialAccountTypes");

            migrationBuilder.RenameTable(
                name: "SocialAccountTracker",
                newName: "SocialAccountTrackers");

            migrationBuilder.RenameIndex(
                name: "IX_UserSocialAccount_SocialAccountTypeId",
                table: "UserSocialAccounts",
                newName: "IX_UserSocialAccounts_SocialAccountTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialAccountTracker_AccountId",
                table: "SocialAccountTrackers",
                newName: "IX_SocialAccountTrackers_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSocialAccounts",
                table: "UserSocialAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialAccountTypes",
                table: "SocialAccountTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialAccountTrackers",
                table: "SocialAccountTrackers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SubscriptionPlansLimit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MonthlyScheduledPostLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlansLimit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsScheduledPost = table.Column<bool>(type: "bit", nullable: false),
                    PublishOnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishOnTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PostContentText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostAttachmentURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PostStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SocialNetworkType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPosts", x => x.Id);
                    table.CheckConstraint("chk_network_type", "SocialNetworkType IN ('Facebook','Instagram','Twitter','Youtube','LinkedIn')");
                    table.CheckConstraint("chk_post_status", "PostStatus IN ('Scheduled', 'Cancelled')");
                    table.CheckConstraint("chk_post_type", "PostType IN ('Text', 'Image', 'Video')");
                    table.CheckConstraint("chk_publish_date", "PublishOnDate >= GETDATE()");
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlansLimit",
                columns: new[] { "Id", "MonthlyScheduledPostLimit", "PlanName" },
                values: new object[] { 1, 5, "basic" });

            migrationBuilder.InsertData(
                table: "SubscriptionPlansLimit",
                columns: new[] { "Id", "MonthlyScheduledPostLimit", "PlanName" },
                values: new object[] { 2, 150, "Pro" });

            migrationBuilder.AddCheckConstraint(
                name: "chk_subscription_name",
                table: "UserSocialAccounts",
                sql: "SubscriptionName IN ('basic','pro')");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialAccountTrackers_UserSocialAccounts_AccountId",
                table: "SocialAccountTrackers",
                column: "AccountId",
                principalTable: "UserSocialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocialAccounts_SocialAccountTypes_SocialAccountTypeId",
                table: "UserSocialAccounts",
                column: "SocialAccountTypeId",
                principalTable: "SocialAccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialAccountTrackers_UserSocialAccounts_AccountId",
                table: "SocialAccountTrackers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSocialAccounts_SocialAccountTypes_SocialAccountTypeId",
                table: "UserSocialAccounts");

            migrationBuilder.DropTable(
                name: "SubscriptionPlansLimit");

            migrationBuilder.DropTable(
                name: "UserPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSocialAccounts",
                table: "UserSocialAccounts");

            migrationBuilder.DropCheckConstraint(
                name: "chk_subscription_name",
                table: "UserSocialAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialAccountTypes",
                table: "SocialAccountTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialAccountTrackers",
                table: "SocialAccountTrackers");

            migrationBuilder.RenameTable(
                name: "UserSocialAccounts",
                newName: "UserSocialAccount");

            migrationBuilder.RenameTable(
                name: "SocialAccountTypes",
                newName: "SocialAccountType");

            migrationBuilder.RenameTable(
                name: "SocialAccountTrackers",
                newName: "SocialAccountTracker");

            migrationBuilder.RenameIndex(
                name: "IX_UserSocialAccounts_SocialAccountTypeId",
                table: "UserSocialAccount",
                newName: "IX_UserSocialAccount_SocialAccountTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialAccountTrackers_AccountId",
                table: "SocialAccountTracker",
                newName: "IX_SocialAccountTracker_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSocialAccount",
                table: "UserSocialAccount",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialAccountType",
                table: "SocialAccountType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialAccountTracker",
                table: "SocialAccountTracker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialAccountTracker_UserSocialAccount_AccountId",
                table: "SocialAccountTracker",
                column: "AccountId",
                principalTable: "UserSocialAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocialAccount_SocialAccountType_SocialAccountTypeId",
                table: "UserSocialAccount",
                column: "SocialAccountTypeId",
                principalTable: "SocialAccountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
