using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesTutorial.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductMigrationintoPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
