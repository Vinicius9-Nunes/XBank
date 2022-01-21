using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XBank.Repository.Migrations
{
    public partial class initXBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "XBank");

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "XBank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HolderCpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    DueDate = table.Column<int>(type: "int", nullable: false),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    CreatAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "XBank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    AccountEntityId = table.Column<long>(type: "bigint", nullable: true),
                    CreatAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountEntityId",
                        column: x => x.AccountEntityId,
                        principalSchema: "XBank",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_HolderCpf",
                schema: "XBank",
                table: "Accounts",
                column: "HolderCpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountEntityId",
                schema: "XBank",
                table: "Transactions",
                column: "AccountEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "XBank");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "XBank");
        }
    }
}
