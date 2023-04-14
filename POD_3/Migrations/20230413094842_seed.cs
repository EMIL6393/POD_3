using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POD_3.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptions_SubscriptionPlans_SubscriptionPlanPlanId",
                table: "UserSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscriptions_SubscriptionPlanPlanId",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanPlanId",
                table: "UserSubscriptions");

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "PlanId", "Name", "PricePerMonth" },
                values: new object[] { 1, "basic", 10 });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "PlanId", "Name", "PricePerMonth" },
                values: new object[] { 2, "pro", 50 });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_PlanId",
                table: "UserSubscriptions",
                column: "PlanId");

            migrationBuilder.AddCheckConstraint(
                name: "chk_future_dates",
                table: "UserSubscriptions",
                sql: "SubscriptionStartDate > GETDATE() AND SubscriptionEndDate > GETDATE()");

            migrationBuilder.AddCheckConstraint(
                name: "chk_payment_modes",
                table: "UserSubscriptions",
                sql: "PaymentMode IN ('Card', 'NetBanking')");

            migrationBuilder.AddCheckConstraint(
                name: "chk_subscription_status",
                table: "UserSubscriptions",
                sql: "SubscriptionStatus IN ('New', 'Renewed', 'Cancelled')");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptions_SubscriptionPlans_PlanId",
                table: "UserSubscriptions",
                column: "PlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptions_SubscriptionPlans_PlanId",
                table: "UserSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscriptions_PlanId",
                table: "UserSubscriptions");

            migrationBuilder.DropCheckConstraint(
                name: "chk_future_dates",
                table: "UserSubscriptions");

            migrationBuilder.DropCheckConstraint(
                name: "chk_payment_modes",
                table: "UserSubscriptions");

            migrationBuilder.DropCheckConstraint(
                name: "chk_subscription_status",
                table: "UserSubscriptions");

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "PlanId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "PlanId",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionPlanPlanId",
                table: "UserSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_SubscriptionPlanPlanId",
                table: "UserSubscriptions",
                column: "SubscriptionPlanPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptions_SubscriptionPlans_SubscriptionPlanPlanId",
                table: "UserSubscriptions",
                column: "SubscriptionPlanPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
