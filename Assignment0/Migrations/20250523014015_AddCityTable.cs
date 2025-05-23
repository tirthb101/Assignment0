using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment0.Migrations
{
    /// <inheritdoc />
    public partial class AddCityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityTables_StateTables_StateId",
                        column: x => x.StateId,
                        principalTable: "StateTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityTables_StateId",
                table: "CityTables",
                column: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityTables");
        }
    }
}
