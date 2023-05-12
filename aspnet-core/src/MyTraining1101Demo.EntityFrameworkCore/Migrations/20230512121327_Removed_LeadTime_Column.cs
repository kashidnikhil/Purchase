using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Removed_LeadTime_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfRate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LeadTime",
                table: "Items");

            migrationBuilder.AlterColumn<decimal>(
                name: "RatePerQuantity",
                table: "Items",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchaseValue",
                table: "Items",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OrderingRate",
                table: "Items",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<decimal>(
                name: "RateAsOnDate",
                table: "Items",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemRateRevisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatalogueNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderingQuantity = table.Column<int>(type: "int", nullable: false),
                    RatePerOrderingQuantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    StockQuantityPerOrderingUOM = table.Column<int>(type: "int", nullable: false),
                    RatePerStockUOM = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    StockUOMId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderingUOMId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_ItemRateRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemRateRevisions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemRateRevisions_Unit_OrderingUOMId",
                        column: x => x.OrderingUOMId,
                        principalTable: "Unit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemRateRevisions_Unit_StockUOMId",
                        column: x => x.StockUOMId,
                        principalTable: "Unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemSupplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_ItemSupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSupplier_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemSupplier_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Procurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatalogueNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatePerPack = table.Column<double>(type: "float", nullable: false),
                    QuantityPerOrderingUOM = table.Column<long>(type: "bigint", nullable: false),
                    RatePerStockUOM = table.Column<double>(type: "float", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Procurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procurements_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemRateRevisions_ItemId",
                table: "ItemRateRevisions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemRateRevisions_OrderingUOMId",
                table: "ItemRateRevisions",
                column: "OrderingUOMId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemRateRevisions_StockUOMId",
                table: "ItemRateRevisions",
                column: "StockUOMId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSupplier_ItemId",
                table: "ItemSupplier",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSupplier_SupplierId",
                table: "ItemSupplier",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Procurements_ItemId",
                table: "Procurements",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemRateRevisions");

            migrationBuilder.DropTable(
                name: "ItemSupplier");

            migrationBuilder.DropTable(
                name: "Procurements");

            migrationBuilder.DropColumn(
                name: "RateAsOnDate",
                table: "Items");

            migrationBuilder.AlterColumn<double>(
                name: "RatePerQuantity",
                table: "Items",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<long>(
                name: "PurchaseValue",
                table: "Items",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "OrderingRate",
                table: "Items",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRate",
                table: "Items",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeadTime",
                table: "Items",
                type: "datetime2",
                nullable: true);
        }
    }
}
