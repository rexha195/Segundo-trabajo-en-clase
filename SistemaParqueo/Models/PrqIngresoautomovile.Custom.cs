using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaParqueo.Models;

public partial class PrqIngresoautomovile
{
    [NotMapped]
    public PrqParqueo? ParqueoAsociado { get; set; }

    [NotMapped]
    public double? DuracionEstadiaMinutos
    {
        get
        {
            if (!FechaHoraSalida.HasValue) return null;
            return (FechaHoraSalida.Value - FechaHoraEntrada).TotalMinutes;
        }
    }

    [NotMapped]
    public double? DuracionEstadiaHoras
    {
        get
        {
            if (!FechaHoraSalida.HasValue) return null;
            return (FechaHoraSalida.Value - FechaHoraEntrada).TotalHours;
        }
    }

    [NotMapped]
    public decimal? MontoTotalPagar
    {
        get
        {
            if (!DuracionEstadiaHoras.HasValue || ParqueoAsociado == null) return null;
            var horasCobrables = (decimal)Math.Ceiling(DuracionEstadiaHoras.Value);
            return horasCobrables * ParqueoAsociado.PrecioPorHora;
        }
    }
}