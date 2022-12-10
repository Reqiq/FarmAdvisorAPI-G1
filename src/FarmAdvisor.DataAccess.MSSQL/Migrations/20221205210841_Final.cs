using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAdvisor.DataAccess.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensorUser",
                columns: table => new
                {
                    SensorsSensorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorUser", x => new { x.SensorsSensorId, x.UsersUserID });
                    table.ForeignKey(
                        name: "FK_SensorUser_sensor_SensorsSensorId",
                        column: x => x.SensorsSensorId,
                        principalTable: "sensor",
                        principalColumn: "sensor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SensorUser_user_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorUser_UsersUserID",
                table: "SensorUser",
                column: "UsersUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorUser");
        }
    }
}
