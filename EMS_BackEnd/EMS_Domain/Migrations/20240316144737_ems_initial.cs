using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_Domain.Migrations
{
    /// <inheritdoc />
    public partial class ems_initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "election_citys",
                columns: table => new
                {
                    election_city_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_city_name = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    election_state_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_city_active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_citys", x => x.election_city_id);
                });

            migrationBuilder.CreateTable(
                name: "election_constant_values_lists",
                columns: table => new
                {
                    election_constant_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_constant_key = table.Column<int>(type: "INTEGER", nullable: false),
                    election_constant_value = table.Column<string>(type: "TEXT", nullable: false),
                    group_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_constant_values_lists", x => x.election_constant_id);
                });

            migrationBuilder.CreateTable(
                name: "election_contesting_candidates",
                columns: table => new
                {
                    election_contest_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_party_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_user_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_state_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_city_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_contesting_candidates", x => x.election_contest_id);
                });

            migrationBuilder.CreateTable(
                name: "election_mp_seat_by_states",
                columns: table => new
                {
                    election_mp_seat_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_state_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_mp_seat_no = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_mp_seat_by_states", x => x.election_mp_seat_id);
                });

            migrationBuilder.CreateTable(
                name: "election_parties_lists",
                columns: table => new
                {
                    election_party_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_party_name = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    election_sym_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_party_active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_parties_lists", x => x.election_party_id);
                });

            migrationBuilder.CreateTable(
                name: "election_poll_datas",
                columns: table => new
                {
                    election_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_user_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_party_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_state_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_city_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_poll_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_poll_datas", x => x.election_poll_id);
                });

            migrationBuilder.CreateTable(
                name: "election_results",
                columns: table => new
                {
                    election_result_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_state_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_city_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_party_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_poll_count = table.Column<int>(type: "INTEGER", nullable: false),
                    election_winner_by_state_wise = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_results", x => x.election_result_id);
                });

            migrationBuilder.CreateTable(
                name: "election_states",
                columns: table => new
                {
                    election_state_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_state_name = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    election_state_active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_states", x => x.election_state_id);
                });

            migrationBuilder.CreateTable(
                name: "election_symbols_lists",
                columns: table => new
                {
                    election_sym_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_sym_name = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    election_sym_font_name = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    election_sym_active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_symbols_lists", x => x.election_sym_id);
                });

            migrationBuilder.CreateTable(
                name: "election_users",
                columns: table => new
                {
                    election_user_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_voter_id = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    election_voter_name = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    election_address = table.Column<string>(type: "TEXT", nullable: false),
                    election_state_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_city_id = table.Column<int>(type: "INTEGER", nullable: false),
                    election_voter_password = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false),
                    election_user_approve_status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_users", x => x.election_user_id);
                });

            migrationBuilder.CreateTable(
                name: "election_year_to_dates",
                columns: table => new
                {
                    election_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    election_name = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    election_start_date_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    election_end_date_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    election_current_status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_year_to_dates", x => x.election_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "election_citys");

            migrationBuilder.DropTable(
                name: "election_constant_values_lists");

            migrationBuilder.DropTable(
                name: "election_contesting_candidates");

            migrationBuilder.DropTable(
                name: "election_mp_seat_by_states");

            migrationBuilder.DropTable(
                name: "election_parties_lists");

            migrationBuilder.DropTable(
                name: "election_poll_datas");

            migrationBuilder.DropTable(
                name: "election_results");

            migrationBuilder.DropTable(
                name: "election_states");

            migrationBuilder.DropTable(
                name: "election_symbols_lists");

            migrationBuilder.DropTable(
                name: "election_users");

            migrationBuilder.DropTable(
                name: "election_year_to_dates");
        }
    }
}
