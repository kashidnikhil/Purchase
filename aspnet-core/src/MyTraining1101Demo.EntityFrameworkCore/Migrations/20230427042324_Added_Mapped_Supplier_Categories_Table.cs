using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Added_Mapped_Supplier_Categories_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MappedSupplierCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_MappedSupplierCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MappedSupplierCategories_SupplierCategory_SupplierCategoryId",
                        column: x => x.SupplierCategoryId,
                        principalTable: "SupplierCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MappedSupplierCategories_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MappedSupplierCategories_SupplierCategoryId",
                table: "MappedSupplierCategories",
                column: "SupplierCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MappedSupplierCategories_SupplierId",
                table: "MappedSupplierCategories",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MappedSupplierCategories");
        }
    }
}
