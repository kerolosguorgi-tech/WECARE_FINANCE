using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WECARE.Finance.Api.Persistence.Migrations;

public partial class Foundation001 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "finance");
        migrationBuilder.CreateTable(
            name: "foundation_metadata",
            schema: "finance",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false),
                SchemaVersion = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_foundation_metadata", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "foundation_metadata", schema: "finance");
    }
}
