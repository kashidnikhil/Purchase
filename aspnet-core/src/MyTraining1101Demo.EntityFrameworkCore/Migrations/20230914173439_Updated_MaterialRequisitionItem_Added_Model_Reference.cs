using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Updated_MaterialRequisitionItem_Added_Model_Reference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ModelWiseItemId",
                table: "MaterialRequisitionItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequisitionItems_ModelWiseItemId",
                table: "MaterialRequisitionItems",
                column: "ModelWiseItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialRequisitionItems_ModelWiseItems_ModelWiseItemId",
                table: "MaterialRequisitionItems",
                column: "ModelWiseItemId",
                principalTable: "ModelWiseItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialRequisitionItems_ModelWiseItems_ModelWiseItemId",
                table: "MaterialRequisitionItems");

            migrationBuilder.DropIndex(
                name: "IX_MaterialRequisitionItems_ModelWiseItemId",
                table: "MaterialRequisitionItems");

            migrationBuilder.DropColumn(
                name: "ModelWiseItemId",
                table: "MaterialRequisitionItems");
        }
    }
}
