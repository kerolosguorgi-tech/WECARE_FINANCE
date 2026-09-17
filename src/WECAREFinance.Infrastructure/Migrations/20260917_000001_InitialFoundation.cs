using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WECAREFinance.Domain.Entities;

#nullable disable

namespace WECAREFinance.Infrastructure.Migrations;

public partial class InitialFoundation : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AuditLogs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Action = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                EntityType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                EntityId = table.Column<Guid>(type: "uuid", nullable: true),
                OldValue = table.Column<string>(type: "text", nullable: true),
                NewValue = table.Column<string>(type: "text", nullable: true),
                Reason = table.Column<string>(type: "text", nullable: true),
                UserId = table.Column<Guid>(type: "uuid", nullable: true),
                UserName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                IpAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                CorrelationId = table.Column<string>(type: "text", nullable: true),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuditLogs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ReferenceSequences",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Prefix = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Year = table.Column<int>(type: "integer", nullable: false),
                LastSequence = table.Column<long>(type: "bigint", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReferenceSequences", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "SystemSettings",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                IsSensitive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SystemSettings", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "WorkstationSettings",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                WorkstationKey = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                DefaultPrinter = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                DefaultExportFolder = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                PaperSize = table.Column<string>(type: "text", nullable: false),
                Orientation = table.Column<string>(type: "text", nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkstationSettings", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AuditLogs_CorrelationId",
            table: "AuditLogs",
            column: "CorrelationId");

        migrationBuilder.CreateIndex(
            name: "IX_AuditLogs_EntityType",
            table: "AuditLogs",
            column: "EntityType");

        migrationBuilder.CreateIndex(
            name: "IX_AuditLogs_UserId",
            table: "AuditLogs",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ReferenceSequences_Prefix_Year",
            table: "ReferenceSequences",
            columns: new[] { "Prefix", "Year" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_SystemSettings_Key",
            table: "SystemSettings",
            column: "Key",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_WorkstationSettings_WorkstationKey",
            table: "WorkstationSettings",
            column: "WorkstationKey",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AuditLogs");

        migrationBuilder.DropTable(
            name: "ReferenceSequences");

        migrationBuilder.DropTable(
            name: "SystemSettings");

        migrationBuilder.DropTable(
            name: "WorkstationSettings");
    }
}
