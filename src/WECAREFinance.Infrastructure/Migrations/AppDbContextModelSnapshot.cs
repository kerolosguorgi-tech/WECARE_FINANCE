using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WECAREFinance.Infrastructure;

#nullable disable

namespace WECAREFinance.Infrastructure.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260917_000001_InitialFoundation")]
partial class InitialFoundationModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "8.0.8");
        modelBuilder.HasAnnotation("Relational:MaxIdentifierLength", 63);

        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity("WECAREFinance.Domain.Entities.AuditLog", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");
            b.Property<string>("Action")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");
            b.Property<DateTimeOffset>("CreatedAt")
                .HasColumnType("timestamp with time zone");
            b.Property<Guid?>("EntityId")
                .HasColumnType("uuid");
            b.Property<string>("EntityType")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");
            b.Property<string>("IpAddress")
                .HasMaxLength(50)
                .HasColumnType("character varying(50)");
            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");
            b.Property<string>("NewValue")
                .HasColumnType("text");
            b.Property<string>("OldValue")
                .HasColumnType("text");
            b.Property<string>("Reason")
                .HasColumnType("text");
            b.Property<DateTimeOffset?>("UpdatedAt")
                .HasColumnType("timestamp with time zone");
            b.Property<Guid?>("UserId")
                .HasColumnType("uuid");
            b.Property<string>("UserName")
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");
            b.Property<string>("CorrelationId")
                .HasColumnType("text");

            b.HasKey("Id");
            b.HasIndex("CorrelationId");
            b.HasIndex("EntityType");
            b.HasIndex("UserId");
            b.ToTable("AuditLogs");
        });

        modelBuilder.Entity("WECAREFinance.Domain.Entities.ReferenceSequence", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");
            b.Property<DateTimeOffset>("CreatedAt")
                .HasColumnType("timestamp with time zone");
            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");
            b.Property<long>("LastSequence")
                .HasColumnType("bigint");
            b.Property<string>("Prefix")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)");
            b.Property<DateTimeOffset?>("UpdatedAt")
                .HasColumnType("timestamp with time zone");
            b.Property<int>("Year")
                .HasColumnType("integer");

            b.HasKey("Id");
            b.HasIndex("Prefix", "Year")
                .IsUnique();
            b.ToTable("ReferenceSequences");
        });

        modelBuilder.Entity("WECAREFinance.Domain.Entities.SystemSetting", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");
            b.Property<DateTimeOffset>("CreatedAt")
                .HasColumnType("timestamp with time zone");
            b.Property<string>("Description")
                .HasMaxLength(500)
                .HasColumnType("character varying(500)");
            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");
            b.Property<bool>("IsSensitive")
                .HasColumnType("boolean");
            b.Property<string>("Key")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");
            b.Property<DateTimeOffset?>("UpdatedAt")
                .HasColumnType("timestamp with time zone");
            b.Property<string>("Value")
                .IsRequired()
                .HasMaxLength(2000)
                .HasColumnType("character varying(2000)");

            b.HasKey("Id");
            b.HasIndex("Key")
                .IsUnique();
            b.ToTable("SystemSettings");
        });

        modelBuilder.Entity("WECAREFinance.Domain.Entities.WorkstationSettings", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");
            b.Property<DateTimeOffset>("CreatedAt")
                .HasColumnType("timestamp with time zone");
            b.Property<string>("DefaultExportFolder")
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnType("character varying(500)");
            b.Property<string>("DefaultPrinter")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");
            b.Property<string>("DisplayName")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");
            b.Property<bool>("IsActive")
                .HasColumnType("boolean");
            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");
            b.Property<string>("Orientation")
                .IsRequired()
                .HasColumnType("text");
            b.Property<string>("PaperSize")
                .IsRequired()
                .HasColumnType("text");
            b.Property<DateTimeOffset?>("UpdatedAt")
                .HasColumnType("timestamp with time zone");
            b.Property<string>("WorkstationKey")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");

            b.HasKey("Id");
            b.HasIndex("WorkstationKey")
                .IsUnique();
            b.ToTable("WorkstationSettings");
        });
    }
}
