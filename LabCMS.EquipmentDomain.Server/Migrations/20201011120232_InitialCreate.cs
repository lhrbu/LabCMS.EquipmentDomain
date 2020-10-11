using Microsoft.EntityFrameworkCore.Migrations;

namespace LabCMS.EquipmentDomain.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentHourlyRates",
                columns: table => new
                {
                    EquipmentNo = table.Column<string>(type: "TEXT", nullable: false),
                    EquipmentName = table.Column<string>(type: "TEXT", nullable: true),
                    MachineCategory = table.Column<string>(type: "TEXT", nullable: true),
                    HourlyRate = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentHourlyRates", x => x.EquipmentNo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentHourlyRates");
        }
    }
}
