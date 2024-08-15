using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class SecondSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Grounds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BlockedTimeSlots",
                columns: table => new
                {
                    BlockedTimeSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroundId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedTimeSlots", x => x.BlockedTimeSlotId);
                    table.ForeignKey(
                        name: "FK_BlockedTimeSlots_Grounds_GroundId",
                        column: x => x.GroundId,
                        principalTable: "Grounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grounds_AdminId",
                table: "Grounds",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedTimeSlots_GroundId",
                table: "BlockedTimeSlots",
                column: "GroundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grounds_Users_AdminId",
                table: "Grounds",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grounds_Users_AdminId",
                table: "Grounds");

            migrationBuilder.DropTable(
                name: "BlockedTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_Grounds_AdminId",
                table: "Grounds");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Grounds");
        }
    }
}
