using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Updated_MaterialRequisitionItem_Table_Added_SubAssembly_Table_Reference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubAssemblyItemId",
                table: "MaterialRequisitionItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequisitionItems_SubAssemblyItemId",
                table: "MaterialRequisitionItems",
                column: "SubAssemblyItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialRequisitionItems_SubAssemblyItems_SubAssemblyItemId",
                table: "MaterialRequisitionItems",
                column: "SubAssemblyItemId",
                principalTable: "SubAssemblyItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialRequisitionItems_SubAssemblyItems_SubAssemblyItemId",
                table: "MaterialRequisitionItems");

            migrationBuilder.DropIndex(
                name: "IX_MaterialRequisitionItems_SubAssemblyItemId",
                table: "MaterialRequisitionItems");

            migrationBuilder.DropColumn(
                name: "SubAssemblyItemId",
                table: "MaterialRequisitionItems");
        }
    }
}
