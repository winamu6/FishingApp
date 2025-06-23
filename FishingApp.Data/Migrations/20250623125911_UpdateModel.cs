using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fishes_Fishings_FishingId",
                table: "Fishes");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Fishings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "FishingId",
                table: "Fishes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Fishes_Fishings_FishingId",
                table: "Fishes",
                column: "FishingId",
                principalTable: "Fishings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fishes_Fishings_FishingId",
                table: "Fishes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Fishings");

            migrationBuilder.AlterColumn<int>(
                name: "FishingId",
                table: "Fishes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fishes_Fishings_FishingId",
                table: "Fishes",
                column: "FishingId",
                principalTable: "Fishings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
