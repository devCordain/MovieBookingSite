using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieBookingSite.Migrations
{
    public partial class SimplifiedTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketsLeft",
                table: "Showings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketsLeft",
                table: "Showings");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    SeatOnRow = table.Column<int>(type: "int", nullable: false),
                    ShowingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Showings_ShowingId",
                        column: x => x.ShowingId,
                        principalTable: "Showings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ShowingId",
                table: "Tickets",
                column: "ShowingId");
        }
    }
}
