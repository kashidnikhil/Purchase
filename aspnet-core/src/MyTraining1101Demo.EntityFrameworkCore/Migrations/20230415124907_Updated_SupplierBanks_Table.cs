using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Updated_SupplierBanks_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountNumber",
                table: "SupplierBanks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "SupplierBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "SupplierBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "SupplierBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MICRCode",
                table: "SupplierBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMode",
                table: "SupplierBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RTGS",
                table: "SupplierBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "SupplierBanks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBanks_SupplierId",
                table: "SupplierBanks",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierBanks_Suppliers_SupplierId",
                table: "SupplierBanks",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierBanks_Suppliers_SupplierId",
                table: "SupplierBanks");

            migrationBuilder.DropIndex(
                name: "IX_SupplierBanks_SupplierId",
                table: "SupplierBanks");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "SupplierBanks");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "SupplierBanks");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "SupplierBanks");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "SupplierBanks");

            migrationBuilder.DropColumn(
                name: "MICRCode",
                table: "SupplierBanks");

            migrationBuilder.DropColumn(
                name: "PaymentMode",
                table: "SupplierBanks");

            migrationBuilder.DropColumn(
                name: "RTGS",
                table: "SupplierBanks");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "SupplierBanks");
        }
    }
}
