using SistemaParqueo.Models;
using System.Collections.Generic;

namespace SistemaParqueo.Repositories.Interfaces;

public interface IParqueosRepository
{
    PrqParqueo ObtenerPorId(int id);
    IEnumerable<PrqParqueo> ObtenerPorCriterios(string nombreProvincia, string nombre, decimal? precioMin, decimal? precioMax);
    
    // Nuevos métodos de acción
    void Insertar(PrqParqueo parqueo);
    void Actualizar(PrqParqueo parqueo);
    void Borrar(int id);
}