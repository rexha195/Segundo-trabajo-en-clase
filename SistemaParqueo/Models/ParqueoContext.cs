using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaParqueo.Models;

public partial class ParqueoContext : DbContext
{
    public ParqueoContext() { }
    
    public ParqueoContext(DbContextOptions<ParqueoContext> options) : base(options) { }

    public virtual DbSet<PrqAutomovile> PrqAutomoviles { get; set; } = null!;
    public virtual DbSet<PrqIngresoautomovile> PrqIngresoautomoviles { get; set; } = null!;
    public virtual DbSet<PrqParqueo> PrqParqueos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Name=ConnectionStrings:MySecretPostgresConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrqAutomovile>(entity =>
        {
            entity.ToTable("prq_automoviles");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.Ano).HasColumnName("ano");
            entity.Property(e => e.Fabricante).HasColumnName("fabricante");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
        });

        modelBuilder.Entity<PrqParqueo>(entity =>
        {
            entity.ToTable("prq_parqueo");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreProvincia).HasColumnName("nombre_provincia");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.PrecioPorHora).HasColumnName("precio_por_hora");
        });

        modelBuilder.Entity<PrqIngresoautomovile>(entity =>
        {
            entity.ToTable("prq_ingresoautomoviles");
            entity.HasKey(e => e.Consecutivo);
            entity.Property(e => e.Consecutivo).HasColumnName("consecutivo");
            entity.Property(e => e.IdParqueo).HasColumnName("id_parqueo");
            entity.Property(e => e.IdAutomovil).HasColumnName("id_automovil");
            entity.Property(e => e.FechaHoraEntrada).HasColumnName("fecha_hora_entrada");
            entity.Property(e => e.FechaHoraSalida).HasColumnName("fecha_hora_salida");

            entity.HasOne(d => d.IdAutomovilNavigation)
                .WithMany(p => p.PrqIngresoautomoviles)
                .HasForeignKey(d => d.IdAutomovil)
                .HasConstraintName("fk_automovil");

            entity.HasOne(d => d.IdParqueoNavigation)
                .WithMany(p => p.PrqIngresoautomoviles)
                .HasForeignKey(d => d.IdParqueo)
                .HasConstraintName("fk_parqueo");
        });
    }
}