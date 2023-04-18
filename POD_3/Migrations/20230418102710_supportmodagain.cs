using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POD_3.Migrations
{
    public partial class supportmodagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPlansLimit",
                table: "SubscriptionPlansLimit");

            migrationBuilder.RenameTable(
                name: "SubscriptionPlansLimit",
                newName: "SubscriptionPlansLimits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPlansLimits",
                table: "SubscriptionPlansLimits",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPlansLimits",
                table: "SubscriptionPlansLimits");

            migrationBuilder.RenameTable(
                name: "SubscriptionPlansLimits",
                newName: "SubscriptionPlansLimit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPlansLimit",
                table: "SubscriptionPlansLimit",
                column: "Id");
        }
    }
}
