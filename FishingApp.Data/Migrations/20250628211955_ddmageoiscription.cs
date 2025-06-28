using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ddmageoiscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Discriptions",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Discriptions");
        }
    }
}
