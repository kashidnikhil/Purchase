using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class updated_CompanyAddress_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CompanyContactPerson",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CompanyAddress",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContactPerson_CompanyId",
                table: "CompanyContactPerson",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAddress_CompanyId",
                table: "CompanyAddress",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAddress_Companies_CompanyId",
                table: "CompanyAddress",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContactPerson_Companies_CompanyId",
                table: "CompanyContactPerson",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAddress_Companies_CompanyId",
                table: "CompanyAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContactPerson_Companies_CompanyId",
                table: "CompanyContactPerson");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContactPerson_CompanyId",
                table: "CompanyContactPerson");

            migrationBuilder.DropIndex(
                name: "IX_CompanyAddress_CompanyId",
                table: "CompanyAddress");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompanyContactPerson");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompanyAddress");
        }
    }
}
