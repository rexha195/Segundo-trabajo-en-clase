using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaParqueo.Repositories.Context;

public class AutomovilesContextRepository : IAutomovilesRepository
{
    private readonly ParqueoContext _context;

    public AutomovilesContextRepository(ParqueoContext context)
    {
        _context = context;
    }

    public PrqAutomovile ObtenerPorId(int id)
    {
        return _context.PrqAutomoviles.Find(id);
    }

    public IEnumerable<PrqAutomovile> ObtenerPorCriterios(string color, int? anioInicio, int? anioFin, string fabricante, string tipo)
    {
        var query = _context.PrqAutomoviles.AsQueryable();

        // Búsquedas aproximadas por caracteres
        if (!string.IsNullOrEmpty(color)) query = query.Where(a => a.Color.Contains(color));
        if (!string.IsNullOrEmpty(fabricante)) query = query.Where(a => a.Fabricante.Contains(fabricante));
        if (!string.IsNullOrEmpty(tipo)) query = query.Where(a => a.Tipo.Contains(tipo));

        // Búsquedas exactas o rangos para números
        if (anioInicio.HasValue) query = query.Where(a => a.Ano >= anioInicio.Value);
        if (anioFin.HasValue) query = query.Where(a => a.Ano <= anioFin.Value);

        return query.ToList();
    }
}