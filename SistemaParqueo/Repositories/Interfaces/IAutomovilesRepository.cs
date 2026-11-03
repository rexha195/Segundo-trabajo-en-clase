using SistemaParqueo.Models;
using System.Collections.Generic;

namespace SistemaParqueo.Repositories.Interfaces;

public interface IAutomovilesRepository
{
    // Búsqueda por llave primaria por default
    PrqAutomovile ObtenerPorId(int id);
    
    // Búsqueda por los criterios solicitados
    IEnumerable<PrqAutomovile> ObtenerPorCriterios(string color, int? anioInicio, int? anioFin, string fabricante, string tipo);
}