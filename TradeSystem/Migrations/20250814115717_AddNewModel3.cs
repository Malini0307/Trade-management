using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "BankGuarantee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryName",
                table: "BankGuarantee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantName",
                table: "BankGuarantee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "LcId",
                table: "BankGuarantee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BankGuarantee_LcId",
                table: "BankGuarantee",
                column: "LcId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankGuarantee_LetterOfCredit_LcId",
                table: "BankGuarantee",
                column: "LcId",
                principalTable: "LetterOfCredit",
                principalColumn: "LcId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankGuarantee_LetterOfCredit_LcId",
                table: "BankGuarantee");

            migrationBuilder.DropIndex(
                name: "IX_BankGuarantee_LcId",
                table: "BankGuarantee");

            migrationBuilder.DropColumn(
                name: "LcId",
                table: "BankGuarantee");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BankGuarantee",
                type: "int",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryName",
                table: "BankGuarantee",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantName",
                table: "BankGuarantee",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
