using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Modified_Item_Master_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StorageCondition",
                table: "Items",
                newName: "SupplierItemName");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemMobility",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaterialGradeId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderingUOMId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuantityPerOrderingUOM",
                table: "Items",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "StockUOMId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitOrderId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitStockId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CalibrationAgencies",
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
                    table.PrimaryKey("PK_CalibrationAgencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationAgencies_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CalibrationAgencies_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ItemAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAttachment_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemStorageCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hazardous = table.Column<int>(type: "int", nullable: false),
                    ThresholdQuantity = table.Column<long>(type: "bigint", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ItemStorageCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemStorageCondition_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_MaterialGradeId",
                table: "Items",
                column: "MaterialGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderingUOMId",
                table: "Items",
                column: "OrderingUOMId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_StockUOMId",
                table: "Items",
                column: "StockUOMId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SupplierId",
                table: "Items",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitOrderId",
                table: "Items",
                column: "UnitOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitStockId",
                table: "Items",
                column: "UnitStockId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationAgencies_ItemId",
                table: "CalibrationAgencies",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationAgencies_SupplierId",
                table: "CalibrationAgencies",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttachment_ItemId",
                table: "ItemAttachment",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStorageCondition_ItemId",
                table: "ItemStorageCondition",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MaterialGrade_MaterialGradeId",
                table: "Items",
                column: "MaterialGradeId",
                principalTable: "MaterialGrade",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Suppliers_SupplierId",
                table: "Items",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Unit_OrderingUOMId",
                table: "Items",
                column: "OrderingUOMId",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Unit_StockUOMId",
                table: "Items",
                column: "StockUOMId",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Unit_UnitOrderId",
                table: "Items",
                column: "UnitOrderId",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Unit_UnitStockId",
                table: "Items",
                column: "UnitStockId",
                principalTable: "Unit",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_MaterialGrade_MaterialGradeId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Suppliers_SupplierId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Unit_OrderingUOMId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Unit_StockUOMId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Unit_UnitOrderId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Unit_UnitStockId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "CalibrationAgencies");

            migrationBuilder.DropTable(
                name: "ItemAttachment");

            migrationBuilder.DropTable(
                name: "ItemStorageCondition");

            migrationBuilder.DropIndex(
                name: "IX_Items_MaterialGradeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderingUOMId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_StockUOMId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_SupplierId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_UnitOrderId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_UnitStockId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemMobility",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MaterialGradeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OrderingUOMId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "QuantityPerOrderingUOM",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "StockUOMId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UnitOrderId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UnitStockId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "SupplierItemName",
                table: "Items",
                newName: "StorageCondition");
        }
    }
}
