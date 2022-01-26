using Microsoft.EntityFrameworkCore.Migrations;

namespace XBank.Repository.Migrations
{
    public partial class UpdateTransactionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountEntityId",
                schema: "XBank",
                table: "Transactions");

            migrationBuilder.AlterColumn<long>(
                name: "AccountEntityId",
                schema: "XBank",
                table: "Transactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountEntityId",
                schema: "XBank",
                table: "Transactions",
                column: "AccountEntityId",
                principalSchema: "XBank",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountEntityId",
                schema: "XBank",
                table: "Transactions");

            migrationBuilder.AlterColumn<long>(
                name: "AccountEntityId",
                schema: "XBank",
                table: "Transactions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountEntityId",
                schema: "XBank",
                table: "Transactions",
                column: "AccountEntityId",
                principalSchema: "XBank",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
