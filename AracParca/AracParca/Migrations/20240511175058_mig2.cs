using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AracParca.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ad",
                table: "Parcas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "Parcas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
