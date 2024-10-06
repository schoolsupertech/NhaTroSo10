using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    room_id = table.Column<int>(type: "int", nullable: false),
                    room_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    max_quantity = table.Column<int>(type: "int", nullable: false),
                    price_of_room = table.Column<decimal>(type: "money", nullable: false),
                    cost_of_shared_room = table.Column<decimal>(type: "money", nullable: false),
                    cost_of_electric = table.Column<decimal>(type: "money", nullable: false),
                    cost_of_water = table.Column<decimal>(type: "money", nullable: false),
                    cost_of_services = table.Column<decimal>(type: "money", nullable: false),
                    payday = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.room_id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    invoice_id = table.Column<int>(type: "int", nullable: false),
                    room_id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    capacity_of_electric = table.Column<int>(type: "int", nullable: false),
                    price_of_electric = table.Column<decimal>(type: "money", nullable: true),
                    capacity_of_water = table.Column<int>(type: "int", nullable: false),
                    price_of_water = table.Column<decimal>(type: "money", nullable: true),
                    price_of_services = table.Column<decimal>(type: "money", nullable: false),
                    total_price = table.Column<decimal>(type: "money", nullable: true),
                    invoice_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.invoice_id);
                    table.ForeignKey(
                        name: "FK_Invoices_Rooms",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "room_id");
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    member_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    room_id = table.Column<int>(type: "int", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "FEMALE"),
                    identification_card = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    date_of_issue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    phone_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    permanent_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    temporary_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.member_id);
                    table.ForeignKey(
                        name: "FK_Members_Rooms",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "room_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_room_id",
                table: "Invoices",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Members_room_id",
                table: "Members",
                column: "room_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
