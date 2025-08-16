using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankGuarantee",
                columns: table => new
                {
                    GuaranteeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GuaranteeAmount = table.Column<decimal>(type: "decimal(18,2)", maxLength: 20, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ValidityPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankGuarantee", x => x.GuaranteeId);
                });

            migrationBuilder.CreateTable(
                name: "Compliances",
                columns: table => new
                {
                    ComplianceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionReference = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ComplianceStatus = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: false),
                    LcId = table.Column<int>(type: "int", nullable: true),
                    GuaranteeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compliances", x => x.ComplianceId);
                    table.ForeignKey(
                        name: "FK_Compliances_BankGuarantee_GuaranteeId",
                        column: x => x.GuaranteeId,
                        principalTable: "BankGuarantee",
                        principalColumn: "GuaranteeId");
                    table.ForeignKey(
                        name: "FK_Compliances_LetterOfCredit_LcId",
                        column: x => x.LcId,
                        principalTable: "LetterOfCredit",
                        principalColumn: "LcId");
                });

            migrationBuilder.CreateTable(
                name: "RiskAssessments",
                columns: table => new
                {
                    RiskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionReference = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RiskFactors = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RiskScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssessmentDate = table.Column<DateTime>(type: "datetime2", maxLength: 10, nullable: false),
                    LcId = table.Column<int>(type: "int", nullable: true),
                    GuaranteeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskAssessments", x => x.RiskId);
                    table.ForeignKey(
                        name: "FK_RiskAssessments_BankGuarantee_GuaranteeId",
                        column: x => x.GuaranteeId,
                        principalTable: "BankGuarantee",
                        principalColumn: "GuaranteeId");
                    table.ForeignKey(
                        name: "FK_RiskAssessments_LetterOfCredit_LcId",
                        column: x => x.LcId,
                        principalTable: "LetterOfCredit",
                        principalColumn: "LcId");
                });

            migrationBuilder.CreateTable(
                name: "TradeDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    LcId = table.Column<int>(type: "int", nullable: true),
                    GuaranteeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_TradeDocuments_BankGuarantee_GuaranteeId",
                        column: x => x.GuaranteeId,
                        principalTable: "BankGuarantee",
                        principalColumn: "GuaranteeId");
                    table.ForeignKey(
                        name: "FK_TradeDocuments_LetterOfCredit_LcId",
                        column: x => x.LcId,
                        principalTable: "LetterOfCredit",
                        principalColumn: "LcId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compliances_GuaranteeId",
                table: "Compliances",
                column: "GuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Compliances_LcId",
                table: "Compliances",
                column: "LcId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessments_GuaranteeId",
                table: "RiskAssessments",
                column: "GuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessments_LcId",
                table: "RiskAssessments",
                column: "LcId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeDocuments_GuaranteeId",
                table: "TradeDocuments",
                column: "GuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeDocuments_LcId",
                table: "TradeDocuments",
                column: "LcId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compliances");

            migrationBuilder.DropTable(
                name: "RiskAssessments");

            migrationBuilder.DropTable(
                name: "TradeDocuments");

            migrationBuilder.DropTable(
                name: "BankGuarantee");
        }
    }
}
