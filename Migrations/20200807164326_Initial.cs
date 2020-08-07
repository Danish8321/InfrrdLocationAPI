using Microsoft.EntityFrameworkCore.Migrations;

namespace LocationFilter.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "State", "Zip" },
                values: new object[] { 1, "Old Delhi Market", "New Delhi", "Delhi", "123456" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "State", "Zip" },
                values: new object[] { 2, "South Beach", "Miami", "Florida", "123457" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "State", "Zip" },
                values: new object[] { 3, "Fort Lauderdale", "Miami", "Florida", "123458" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "State", "Zip" },
                values: new object[] { 4, "Fort Worth", "Dallas", "Texas", "123459" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "State", "Zip" },
                values: new object[] { 5, "North Fort", "Miami Beach", "Florida", "123489" });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Address_City_State_Zip",
                table: "Locations",
                columns: new[] { "Address", "City", "State", "Zip" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
