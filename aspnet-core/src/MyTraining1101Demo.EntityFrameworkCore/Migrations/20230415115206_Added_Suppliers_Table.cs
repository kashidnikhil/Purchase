using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Added_Suppliers_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfEstablishment = table.Column<int>(type: "int", nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSTNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryBy = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    LegalEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_LegalEntity_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalTable: "LegalEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_LegalEntityId",
                table: "Suppliers",
                column: "LegalEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
