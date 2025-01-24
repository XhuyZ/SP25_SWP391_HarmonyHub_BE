using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceTypeWithNameInPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "packages");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "packages",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "packages");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "packages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
