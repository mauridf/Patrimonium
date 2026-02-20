using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patrimonium.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentsAndMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialTransactions",
                table: "FinancialTransactions");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "properties");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "people");

            migrationBuilder.RenameTable(
                name: "Maintenances",
                newName: "maintenances");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "contracts");

            migrationBuilder.RenameTable(
                name: "FinancialTransactions",
                newName: "financial_transactions");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "users",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "users",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "people",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "people",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CpfCnpj",
                table: "people",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_properties",
                table: "properties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_people",
                table: "people",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_maintenances",
                table: "maintenances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contracts",
                table: "contracts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_financial_transactions",
                table: "financial_transactions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: true),
                    ContractId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    IsCover = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_media_properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_maintenances_PropertyId",
                table: "maintenances",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_maintenances_ResponsiblePersonId",
                table: "maintenances",
                column: "ResponsiblePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_PersonId",
                table: "contracts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_PropertyId",
                table: "contracts",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_financial_transactions_ContractId",
                table: "financial_transactions",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_financial_transactions_PropertyId",
                table: "financial_transactions",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_media_PropertyId",
                table: "media",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_contracts_people_PersonId",
                table: "contracts",
                column: "PersonId",
                principalTable: "people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contracts_properties_PropertyId",
                table: "contracts",
                column: "PropertyId",
                principalTable: "properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transactions_contracts_ContractId",
                table: "financial_transactions",
                column: "ContractId",
                principalTable: "contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transactions_properties_PropertyId",
                table: "financial_transactions",
                column: "PropertyId",
                principalTable: "properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_maintenances_people_ResponsiblePersonId",
                table: "maintenances",
                column: "ResponsiblePersonId",
                principalTable: "people",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_maintenances_properties_PropertyId",
                table: "maintenances",
                column: "PropertyId",
                principalTable: "properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contracts_people_PersonId",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_contracts_properties_PropertyId",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_financial_transactions_contracts_ContractId",
                table: "financial_transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_financial_transactions_properties_PropertyId",
                table: "financial_transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_maintenances_people_ResponsiblePersonId",
                table: "maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_maintenances_properties_PropertyId",
                table: "maintenances");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_Email",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_properties",
                table: "properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_people",
                table: "people");

            migrationBuilder.DropPrimaryKey(
                name: "PK_maintenances",
                table: "maintenances");

            migrationBuilder.DropIndex(
                name: "IX_maintenances_PropertyId",
                table: "maintenances");

            migrationBuilder.DropIndex(
                name: "IX_maintenances_ResponsiblePersonId",
                table: "maintenances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contracts",
                table: "contracts");

            migrationBuilder.DropIndex(
                name: "IX_contracts_PersonId",
                table: "contracts");

            migrationBuilder.DropIndex(
                name: "IX_contracts_PropertyId",
                table: "contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_financial_transactions",
                table: "financial_transactions");

            migrationBuilder.DropIndex(
                name: "IX_financial_transactions_ContractId",
                table: "financial_transactions");

            migrationBuilder.DropIndex(
                name: "IX_financial_transactions_PropertyId",
                table: "financial_transactions");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "properties",
                newName: "Properties");

            migrationBuilder.RenameTable(
                name: "people",
                newName: "People");

            migrationBuilder.RenameTable(
                name: "maintenances",
                newName: "Maintenances");

            migrationBuilder.RenameTable(
                name: "contracts",
                newName: "Contracts");

            migrationBuilder.RenameTable(
                name: "financial_transactions",
                newName: "FinancialTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "Users",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "People",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "People",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CpfCnpj",
                table: "People",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialTransactions",
                table: "FinancialTransactions",
                column: "Id");
        }
    }
}
