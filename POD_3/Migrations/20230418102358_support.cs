using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POD_3.Migrations
{
    public partial class support : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "SupportTickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaisedByUserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Local)),
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
                name: "TicketSolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ResolvedByUserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ResolvedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolutionDetails = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SupportTicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSolutions_SupportTickets_Id",
                        column: x => x.Id,
                        principalTable: "SupportTickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlanSLAs",
                columns: new[] { "Id", "ExpectedSLAsInDays", "PlanName" },
                values: new object[] { 1, 7, "basic" });

            migrationBuilder.InsertData(
                table: "SubscriptionPlanSLAs",
                columns: new[] { "Id", "ExpectedSLAsInDays", "PlanName" },
                values: new object[] { 2, 1, "pro" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPlanSLAs");

            migrationBuilder.DropTable(
                name: "TicketSolutions");

            migrationBuilder.DropTable(
                name: "SupportTickets");
        }
    }
}
