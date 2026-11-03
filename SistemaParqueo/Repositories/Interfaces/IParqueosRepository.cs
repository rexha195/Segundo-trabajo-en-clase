using SistemaParqueo.Models;
using System.Collections.Generic;

namespace SistemaParqueo.Repositories.Interfaces;

public interface IParqueosRepository
{
    // Búsqueda por llave primaria por default
    PrqParqueo ObtenerPorId(int id);
    
    // Búsqueda por los criterios solicitados
    IEnumerable<PrqParqueo> ObtenerPorCriterios(string nombreProvincia, string nombre, decimal? precioMin, decimal? precioMax);
}