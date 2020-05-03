using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieBookingSite.Migrations
{
    public partial class imageupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageLarge",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageThumbnail",
                table: "Movies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<int>(nullable: false),
                    Seat = table.Column<int>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    ShowingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Showings_ShowingId",
                        column: x => x.ShowingId,
                        principalTable: "Showings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ShowingId",
                table: "Ticket",
                column: "ShowingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropColumn(
                name: "ImageLarge",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ImageThumbnail",
                table: "Movies");
        }
    }
}
