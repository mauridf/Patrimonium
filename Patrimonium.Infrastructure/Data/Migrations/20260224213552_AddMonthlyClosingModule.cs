using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patrimonium.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthlyClosingModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "estimated_value",
                table: "properties",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "invested_value",
                table: "properties",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "monthly_closings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    month = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LockedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReopenedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IntegrityHash = table.Column<string>(type: "text", nullable: false),
                    Snapshot_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    financial_total_income = table.Column<decimal>(type: "numeric", nullable: false),
                    financial_total_expense = table.Column<decimal>(type: "numeric", nullable: false),
                    financial_total_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    financial_total_maintenance = table.Column<decimal>(type: "numeric", nullable: false),
                    financial_net_result = table.Column<decimal>(type: "numeric", nullable: false),
                    financial_cash_flow = table.Column<decimal>(type: "numeric", nullable: false),
                    op_active_properties = table.Column<int>(type: "integer", nullable: false),
                    op_vacant_properties = table.Column<int>(type: "integer", nullable: false),
                    op_active_contracts = table.Column<int>(type: "integer", nullable: false),
                    op_open_maintenances = table.Column<int>(type: "integer", nullable: false),
                    op_occupancy_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    op_vacancy_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    pat_total_estimated_value = table.Column<decimal>(type: "numeric", nullable: false),
                    pat_total_invested = table.Column<decimal>(type: "numeric", nullable: false),
                    pat_appreciation = table.Column<decimal>(type: "numeric", nullable: false),
                    pat_roi_accumulated = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monthly_closings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "monthly_snapshot_properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    property_id = table.Column<Guid>(type: "uuid", nullable: false),
                    property_internal_name = table.Column<string>(type: "text", nullable: false),
                    property_type = table.Column<string>(type: "text", nullable: false),
                    property_purpose = table.Column<string>(type: "text", nullable: false),
                    property_income = table.Column<decimal>(type: "numeric", nullable: false),
                    property_expense = table.Column<decimal>(type: "numeric", nullable: false),
                    property_net_result = table.Column<decimal>(type: "numeric", nullable: false),
                    monthly_closing_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monthly_snapshot_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_monthly_snapshot_properties_monthly_closings_monthly_closin~",
                        column: x => x.monthly_closing_id,
                        principalTable: "monthly_closings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "monthly_snapshot_transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    category = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    monthly_closing_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monthly_snapshot_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_monthly_snapshot_transactions_monthly_closings_monthly_clos~",
                        column: x => x.monthly_closing_id,
                        principalTable: "monthly_closings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_monthly_closing_user",
                table: "monthly_closings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ux_monthly_closing_user_period",
                table: "monthly_closings",
                columns: new[] { "UserId", "year", "month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_monthly_snapshot_properties_monthly_closing_id",
                table: "monthly_snapshot_properties",
                column: "monthly_closing_id");

            migrationBuilder.CreateIndex(
                name: "IX_monthly_snapshot_transactions_monthly_closing_id",
                table: "monthly_snapshot_transactions",
                column: "monthly_closing_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "monthly_snapshot_properties");

            migrationBuilder.DropTable(
                name: "monthly_snapshot_transactions");

            migrationBuilder.DropTable(
                name: "monthly_closings");

            migrationBuilder.DropColumn(
                name: "estimated_value",
                table: "properties");

            migrationBuilder.DropColumn(
                name: "invested_value",
                table: "properties");
        }
    }
}
