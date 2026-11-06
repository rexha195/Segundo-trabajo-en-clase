using System;
using System.Collections.Generic;

namespace SistemaParqueo.Models;

public partial class PrqAutomovile
{
    public int Id { get; set; }
    public string Color { get; set; } = null!;
    public int Ano { get; set; }
    public string Fabricante { get; set; } = null!;
    public string Tipo { get; set; } = null!;

    public virtual ICollection<PrqIngresoautomovile> PrqIngresoautomoviles { get; set; } = new List<PrqIngresoautomovile>();
}