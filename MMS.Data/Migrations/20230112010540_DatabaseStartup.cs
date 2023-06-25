using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseStartup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressInformationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressInformationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bonuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributorId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CalculationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DistributorTotalSales = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    FirstLevelTotalSales = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    SecondLevelTotalSales = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    DistributorBonusShare = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    FirstLevelBonusShare = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    SecondLevelBonusShare = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    TotalBonus = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInformationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityDocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressTypeId = table.Column<int>(type: "int", nullable: false),
                    AddressInformationTypeId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressInformations_AddressInformationTypes_AddressInformationTypeId",
                        column: x => x.AddressInformationTypeId,
                        principalTable: "AddressInformationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactTypeId = table.Column<int>(type: "int", nullable: false),
                    ContactInformationTypeId = table.Column<int>(type: "int", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInformations_ContactInformationTypes_ContactInformationTypeId",
                        column: x => x.ContactInformationTypeId,
                        principalTable: "ContactInformationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IdentityDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityDocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    IdentityDocumentTypeModelId = table.Column<int>(type: "int", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityDocuments_IdentityDocumentTypes_IdentityDocumentTypeModelId",
                        column: x => x.IdentityDocumentTypeModelId,
                        principalTable: "IdentityDocumentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenderTypeId = table.Column<int>(type: "int", nullable: false),
                    PictureFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityDocumentId = table.Column<int>(type: "int", nullable: false),
                    ContactInformationId = table.Column<int>(type: "int", nullable: false),
                    AddressInformationId = table.Column<int>(type: "int", nullable: false),
                    RecommendationsCount = table.Column<int>(type: "int", nullable: false),
                    RecommendationAuthorDistributorId = table.Column<int>(type: "int", nullable: true),
                    RecommendationHierarchyLevel = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distributors_AddressInformations_AddressInformationId",
                        column: x => x.AddressInformationId,
                        principalTable: "AddressInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Distributors_ContactInformations_ContactInformationId",
                        column: x => x.ContactInformationId,
                        principalTable: "ContactInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Distributors_Distributors_RecommendationAuthorDistributorId",
                        column: x => x.RecommendationAuthorDistributorId,
                        principalTable: "Distributors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Distributors_GenderTypes_GenderTypeId",
                        column: x => x.GenderTypeId,
                        principalTable: "GenderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Distributors_IdentityDocuments_IdentityDocumentId",
                        column: x => x.IdentityDocumentId,
                        principalTable: "IdentityDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistributorId = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    IsCalculated = table.Column<bool>(type: "bit", nullable: false),
                    CalculationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Distributors_DistributorId",
                        column: x => x.DistributorId,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AddressInformationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Factual" },
                    { 2, "Registered" }
                });

            migrationBuilder.InsertData(
                table: "ContactInformationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Telephone" },
                    { 2, "Mobile" },
                    { 3, "Email" },
                    { 4, "Fax" }
                });

            migrationBuilder.InsertData(
                table: "GenderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "IdentityDocumentTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "IdCard" },
                    { 2, "Passport" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformations_AddressInformationTypeId",
                table: "AddressInformations",
                column: "AddressInformationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformations_ContactInformationTypeId",
                table: "ContactInformations",
                column: "ContactInformationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_AddressInformationId",
                table: "Distributors",
                column: "AddressInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_ContactInformationId",
                table: "Distributors",
                column: "ContactInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_GenderTypeId",
                table: "Distributors",
                column: "GenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_IdentityDocumentId",
                table: "Distributors",
                column: "IdentityDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_RecommendationAuthorDistributorId",
                table: "Distributors",
                column: "RecommendationAuthorDistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityDocuments_IdentityDocumentTypeModelId",
                table: "IdentityDocuments",
                column: "IdentityDocumentTypeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DistributorId",
                table: "Sales",
                column: "DistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AddressInformations");

            migrationBuilder.DropTable(
                name: "ContactInformations");

            migrationBuilder.DropTable(
                name: "GenderTypes");

            migrationBuilder.DropTable(
                name: "IdentityDocuments");

            migrationBuilder.DropTable(
                name: "AddressInformationTypes");

            migrationBuilder.DropTable(
                name: "ContactInformationTypes");

            migrationBuilder.DropTable(
                name: "IdentityDocumentTypes");
        }
    }
}
