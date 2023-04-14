using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POD_3.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PricePerMonth = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PlanId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubscriptionStatus = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    SubscriptionPlanPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriptions", x => x.SubscriptionId);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_SubscriptionPlans_SubscriptionPlanPlanId",
                        column: x => x.SubscriptionPlanPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionCancellations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCancellations_SubscriptionId",
                table: "SubscriptionCancellations",
                column: "SubscriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_SubscriptionPlanPlanId",
                table: "UserSubscriptions",
                column: "SubscriptionPlanPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionCancellations");

            migrationBuilder.DropTable(
                name: "UserSubscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
