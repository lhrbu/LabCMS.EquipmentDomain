using Microsoft.EntityFrameworkCore.Migrations;

namespace LabCMS.FixtureDomain.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fixtures",
                columns: table => new
                {
                    ProjectNo = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Direction = table.Column<int>(type: "INTEGER", nullable: false),
                    SortId = table.Column<int>(type: "INTEGER", nullable: false),
                    LocationNo_StockNo = table.Column<int>(type: "INTEGER", nullable: true),
                    LocationNo_Floor = table.Column<int>(type: "INTEGER", nullable: true),
                    Remark = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => new { x.ProjectNo, x.Type, x.SortId, x.Direction });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fixtures");
        }
    }
}
