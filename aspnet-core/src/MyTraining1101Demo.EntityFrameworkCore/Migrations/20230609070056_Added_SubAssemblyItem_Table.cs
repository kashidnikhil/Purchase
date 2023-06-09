using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Added_SubAssemblyItem_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubAssemblyItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubAssemblyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_SubAssemblyItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubAssemblyItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubAssemblyItems_SubAssemblies_SubAssemblyId",
                        column: x => x.SubAssemblyId,
                        principalTable: "SubAssemblies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubAssemblyItems_ItemId",
                table: "SubAssemblyItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubAssemblyItems_SubAssemblyId",
                table: "SubAssemblyItems",
                column: "SubAssemblyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubAssemblyItems");
        }
    }
}
