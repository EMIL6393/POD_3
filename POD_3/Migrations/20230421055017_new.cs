using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POD_3.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialAccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialAccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PricePerMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanSLAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExpectedSLAsInDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanSLAs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlansLimits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MonthlyScheduledPostLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlansLimits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupportTickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaisedByUserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 4, 21, 0, 0, 0, 0, DateTimeKind.Local)),
                    ExpectedResolutionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketSummary = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TicketDetails = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TicketStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "Open"),
                    TicketType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTickets", x => x.TicketId);
                    table.CheckConstraint("CK_SupportTicket_ExpectedResolutionOn", "ExpectedResolutionOn > GETDATE()");
                    table.CheckConstraint("CK_SupportTicket_TicketStatus", "TicketStatus IN ('Open', 'Closed')");
                    table.CheckConstraint("CK_SupportTicket_TicketType", "TicketType IN ('Subscription', 'Billing', 'PostManagement', 'Others')");
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSocialAccounts",
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
                    table.PrimaryKey("PK_UserSocialAccounts", x => x.Id);
                    table.CheckConstraint("chk_subscription_name", "SubscriptionName IN ('basic','pro')");
                    table.ForeignKey(
                        name: "FK_UserSocialAccounts_SocialAccountTypes_SocialAccountTypeId",
                        column: x => x.SocialAccountTypeId,
                        principalTable: "SocialAccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketSolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResolvedByUserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ResolvedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolutionDetails = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SupportTicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSolutions_SupportTickets_SupportTicketId",
                        column: x => x.SupportTicketId,
                        principalTable: "SupportTickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubscriptionStatus = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriptions", x => x.SubscriptionId);
                    table.CheckConstraint("chk_future_dates", "SubscriptionStartDate > GETDATE() AND SubscriptionEndDate > GETDATE()");
                    table.CheckConstraint("chk_payment_modes", "PaymentMode IN ('Card', 'NetBanking')");
                    table.CheckConstraint("chk_subscription_status", "SubscriptionStatus IN ('New', 'Renewed', 'Cancelled')");
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialAccountTrackers",
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
                    table.PrimaryKey("PK_SocialAccountTrackers", x => x.Id);
                    table.CheckConstraint("chk_action", "Action IN ('AccountAdded', 'AccountRemoved', 'AccountPasswordChanged')");
                    table.ForeignKey(
                        name: "FK_SocialAccountTrackers_UserSocialAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "UserSocialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionCancellations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    CancellationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancellationReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionCancellations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionCancellations_UserSubscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "UserSubscriptions",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SocialAccountTypes",
                columns: new[] { "Id", "AccountType" },
                values: new object[,]
                {
                    { 1, "Facebook" },
                    { 2, "Instagram" },
                    { 3, "Twitter" },
                    { 4, "Youtube" },
                    { 5, "LinkedIn" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlanSLAs",
                columns: new[] { "Id", "ExpectedSLAsInDays", "PlanName" },
                values: new object[,]
                {
                    { 1, 7, "basic" },
                    { 2, 1, "pro" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "PlanId", "Name", "PricePerMonth" },
                values: new object[,]
                {
                    { 1, "basic", 10 },
                    { 2, "pro", 25 }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlansLimits",
                columns: new[] { "Id", "MonthlyScheduledPostLimit", "PlanName" },
                values: new object[,]
                {
                    { 1, 5, "basic" },
                    { 2, 150, "Pro" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[] { 1, "admin123@gmail.com", "240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9", "SupportExecutive" });

            migrationBuilder.CreateIndex(
                name: "IX_SocialAccountTrackers_AccountId",
                table: "SocialAccountTrackers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCancellations_SubscriptionId",
                table: "SubscriptionCancellations",
                column: "SubscriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketSolutions_SupportTicketId",
                table: "TicketSolutions",
                column: "SupportTicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialAccounts_SocialAccountTypeId",
                table: "UserSocialAccounts",
                column: "SocialAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_PlanId",
                table: "UserSubscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_UserId",
                table: "UserSubscriptions",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialAccountTrackers");

            migrationBuilder.DropTable(
                name: "SubscriptionCancellations");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanSLAs");

            migrationBuilder.DropTable(
                name: "SubscriptionPlansLimits");

            migrationBuilder.DropTable(
                name: "TicketSolutions");

            migrationBuilder.DropTable(
                name: "UserPosts");

            migrationBuilder.DropTable(
                name: "UserSocialAccounts");

            migrationBuilder.DropTable(
                name: "UserSubscriptions");

            migrationBuilder.DropTable(
                name: "SupportTickets");

            migrationBuilder.DropTable(
                name: "SocialAccountTypes");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
