using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_Domain.Migrations
{
    /// <inheritdoc />
    public partial class modifiedforAUTH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "election_users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "election_users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "election_users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "election_users");
        }
    }
}
