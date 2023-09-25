using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Updated_Supplier_Table_Added_New_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentsOrApprovalByCeo",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentsOrObservations",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseDetails",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MSMENumber",
                table: "Suppliers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MSMEStatus",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NegativeObservation",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PAN",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "POComments",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositiveObservation",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferredBy",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicesProvided",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangeDate",
                table: "Suppliers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusChangeReason",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentsOrApprovalByCeo",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CommentsOrObservations",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "LicenseDetails",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "MSMENumber",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "MSMEStatus",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "NegativeObservation",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PAN",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "POComments",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PositiveObservation",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ReferredBy",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ServicesProvided",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "StatusChangeDate",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "StatusChangeReason",
                table: "Suppliers");
        }
    }
}
