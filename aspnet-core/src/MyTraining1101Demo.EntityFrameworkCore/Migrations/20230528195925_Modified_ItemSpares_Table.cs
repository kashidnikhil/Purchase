using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Modified_ItemSpares_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ItemSparesId",
                table: "ItemSpare",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpare_ItemSparesId",
                table: "ItemSpare",
                column: "ItemSparesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSpare_Items_ItemSparesId",
                table: "ItemSpare",
                column: "ItemSparesId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSpare_Items_ItemSparesId",
                table: "ItemSpare");

            migrationBuilder.DropIndex(
                name: "IX_ItemSpare_ItemSparesId",
                table: "ItemSpare");

            migrationBuilder.DropColumn(
                name: "ItemSparesId",
                table: "ItemSpare");
        }
    }
}
