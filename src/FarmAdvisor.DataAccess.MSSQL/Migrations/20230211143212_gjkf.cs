using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAdvisor.DataAccess.MSSQL.Migrations
{
    public partial class gjkf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_farm_user_farm_id",
                table: "farm");

            migrationBuilder.AddForeignKey(
                name: "FK_farm_user_farm_id",
                table: "farm",
                column: "farm_id",
                principalTable: "user",
                principalColumn: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_farm_user_farm_id",
                table: "farm");

            migrationBuilder.AddForeignKey(
                name: "FK_farm_user_farm_id",
                table: "farm",
                column: "farm_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
