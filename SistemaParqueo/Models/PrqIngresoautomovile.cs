using System;
using System.Collections.Generic;

namespace SistemaParqueo.Models;

public partial class PrqIngresoautomovile
{
    public int Consecutivo { get; set; }
    public int IdParqueo { get; set; }
    public int IdAutomovil { get; set; }
    public DateTime FechaHoraEntrada { get; set; }
    public DateTime? FechaHoraSalida { get; set; }

    public virtual PrqAutomovile? IdAutomovilNavigation { get; set; }
    public virtual PrqParqueo? IdParqueoNavigation { get; set; }
}