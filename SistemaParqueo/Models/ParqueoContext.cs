using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaParqueo.Models;

public partial class ParqueoContext : DbContext
{
    public ParqueoContext()
    {
    }

    public ParqueoContext(DbContextOptions<ParqueoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PrqAutomovile> PrqAutomoviles { get; set; }

    public virtual DbSet<PrqAutomovile1> PrqAutomoviles1 { get; set; }

    public virtual DbSet<PrqAutomovile2> PrqAutomoviles2 { get; set; }

    public virtual DbSet<PrqIngresoautomovile> PrqIngresoautomoviles { get; set; }

    public virtual DbSet<PrqParqueo> PrqParqueos { get; set; }

    public virtual DbSet<PrqParqueo1> PrqParqueos1 { get; set; }

    public virtual DbSet<PrqParqueo2> PrqParqueos2 { get; set; }

    public virtual DbSet<PrqParqueo3> PrqParqueos3 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:MySecretPostgresConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("user_08", "PRQTipoAutomovil", new[] { "sedan", "4x4", "moto" })
            .HasPostgresEnum("user_08", "match_kind", new[] { "competition", "friendly" })
            .HasPostgresEnum("user_08", "match_status", new[] { "scheduled", "live", "finished" })
            .HasPostgresEnum("user_08", "sport_type", new[] { "soccer", "basketball", "baseball", "volleyball", "rugby", "hockey" })
            .HasPostgresEnum("user_25", "match_kind", new[] { "friendly", "official" })
            .HasPostgresEnum("user_25", "match_status", new[] { "scheduled", "live", "finished" })
            .HasPostgresEnum("user_25", "sport", new[] { "soccer", "basketball", "baseball", "volleyball", "rugby", "hockey", "football" });

        modelBuilder.Entity<PrqAutomovile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prq_automoviles_pkey");

            entity.ToTable("prq_automoviles", "user_04");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(100)
                .HasColumnName("fabricante");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<PrqAutomovile1>(entity =>
        {
            entity.HasKey(e => e.IdAutomovil).HasName("prq_automoviles_pkey");

            entity.ToTable("prq_automoviles", "user_14");

            entity.Property(e => e.IdAutomovil).HasColumnName("id_automovil");
            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .HasColumnName("color");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(50)
                .HasColumnName("fabricante");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<PrqAutomovile2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prq_automoviles_pkey");

            entity.ToTable("prq_automoviles", "user_01");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('prq_automoviles_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Ano).HasColumnName("ano");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(100)
                .HasColumnName("fabricante");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<PrqIngresoautomovile>(entity =>
        {
            entity.HasKey(e => e.Consecutivo).HasName("prq_ingresoautomoviles_pkey");

            entity.ToTable("prq_ingresoautomoviles", "user_01");

            entity.Property(e => e.Consecutivo)
                .HasDefaultValueSql("nextval('prq_ingresoautomoviles_consecutivo_seq'::regclass)")
                .HasColumnName("consecutivo");
            entity.Property(e => e.FechaHoraEntrada)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_hora_entrada");
            entity.Property(e => e.FechaHoraSalida)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_hora_salida");
            entity.Property(e => e.IdAutomovil).HasColumnName("id_automovil");
            entity.Property(e => e.IdParqueo).HasColumnName("id_parqueo");

            entity.HasOne(d => d.IdAutomovilNavigation).WithMany(p => p.PrqIngresoautomoviles)
                .HasForeignKey(d => d.IdAutomovil)
                .HasConstraintName("fk_automovil");

            entity.HasOne(d => d.IdParqueoNavigation).WithMany(p => p.PrqIngresoautomoviles)
                .HasForeignKey(d => d.IdParqueo)
                .HasConstraintName("fk_parqueo");
        });

        modelBuilder.Entity<PrqParqueo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prq_parqueo_pkey");

            entity.ToTable("prq_parqueo", "user_12");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(100)
                .HasColumnName("nombre_provincia");
            entity.Property(e => e.PrecioPorHora)
                .HasPrecision(10, 2)
                .HasColumnName("precio_por_hora");
        });

        modelBuilder.Entity<PrqParqueo1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_prq_parqueo");

            entity.ToTable("prq_parqueo", "user_13", tb => tb.HasComment("Parqueos (estacionamientos) disponibles."));

            entity.Property(e => e.Id)
                .HasComment("Llave primaria del parqueo.")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasComment("Nombre del parqueo.")
                .HasColumnName("nombre");
            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(20)
                .HasComment("Provincia de Costa Rica donde se ubica el parqueo.")
                .HasColumnName("nombre_provincia");
            entity.Property(e => e.PrecioPorHora)
                .HasPrecision(10, 2)
                .HasComment("Tarifa por hora en colones.")
                .HasColumnName("precio_por_hora");
        });

        modelBuilder.Entity<PrqParqueo2>(entity =>
        {
            entity.HasKey(e => e.IdParqueo).HasName("prq_parqueo_pkey");

            entity.ToTable("prq_parqueo", "user_14");

            entity.Property(e => e.IdParqueo).HasColumnName("id_parqueo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(50)
                .HasColumnName("nombre_provincia");
            entity.Property(e => e.PrecioPorHora)
                .HasPrecision(10, 2)
                .HasColumnName("precio_por_hora");
        });

        modelBuilder.Entity<PrqParqueo3>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prq_parqueo_pkey");

            entity.ToTable("prq_parqueo", "user_01");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('prq_parqueo_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(100)
                .HasColumnName("nombre_provincia");
            entity.Property(e => e.PrecioPorHora)
                .HasPrecision(10, 2)
                .HasColumnName("precio_por_hora");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
