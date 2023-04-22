using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Updated_ConactPerson_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactPersonName",
                table: "CompanyContactPerson",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "CompanyContactPerson",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                table: "CompanyContactPerson",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "CompanyContactPerson",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CompanyAddress",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPersonName",
                table: "CompanyContactPerson");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "CompanyContactPerson");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "CompanyContactPerson");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "CompanyContactPerson");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "CompanyAddress");
        }
    }
}
