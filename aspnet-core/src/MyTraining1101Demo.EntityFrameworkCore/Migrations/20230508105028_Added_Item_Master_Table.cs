using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class Added_Item_Master_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemCategory = table.Column<int>(type: "int", nullable: false),
                    GenericName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemType = table.Column<int>(type: "int", nullable: true),
                    AMCRequired = table.Column<int>(type: "int", nullable: true),
                    CalibrationRequirement = table.Column<int>(type: "int", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseValue = table.Column<long>(type: "bigint", nullable: true),
                    GST = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HSNCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderingRate = table.Column<double>(type: "float", nullable: false),
                    RatePerQuantity = table.Column<double>(type: "float", nullable: false),
                    DateOfRate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: true),
                    LeadTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedBy = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedBy = table.Column<long>(type: "bigint", nullable: false),
                    DiscardedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscardApprovedBy = table.Column<long>(type: "bigint", nullable: false),
                    DiscardedReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<long>(type: "bigint", nullable: true),
                    StorageCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MSL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CTQRequirement = table.Column<int>(type: "int", nullable: true),
                    CTQSpecifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryApplicable = table.Column<int>(type: "int", nullable: true),
                    MinimumOrderQuantity = table.Column<int>(type: "int", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationYear = table.Column<int>(type: "int", nullable: false),
                    SubjectCategory = table.Column<int>(type: "int", nullable: true),
                    PurchasedBy = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Items", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
