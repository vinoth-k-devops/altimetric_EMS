using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_Domain.Migrations
{
    /// <inheritdoc />
    public partial class addedusertype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "election_user_type",
                table: "election_users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "election_user_type",
                table: "election_users");
        }
    }
}
