using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace robotCompanions.Migrations
{
    /// <inheritdoc />
    public partial class removerobotsId3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "POOP",
                table: "cartDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "POOP",
                table: "cartDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
