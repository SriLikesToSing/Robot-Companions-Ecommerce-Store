using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace robotCompanions.Migrations
{
    /// <inheritdoc />
    public partial class removedPesky2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cartDetails_RobotsId",
                table: "cartDetails");

            migrationBuilder.AlterColumn<int>(
                name: "RobotsId",
                table: "cartDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
