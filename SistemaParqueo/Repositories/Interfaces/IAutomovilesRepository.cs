using SistemaParqueo.Models;
using System.Collections.Generic;

namespace SistemaParqueo.Repositories.Interfaces;

public interface IAutomovilesRepository
{
    PrqAutomovile ObtenerPorId(int id);
    IEnumerable<PrqAutomovile> ObtenerPorCriterios(string color, int? anioInicio, int? anioFin, string fabricante, string tipo);
    
    // Nuevos métodos de acción
    void Insertar(PrqAutomovile automovil);
    void Actualizar(PrqAutomovile automovil);
    void Borrar(int id);
}using SistemaParqueo.Models;
using System.Collections.Generic;

namespace SistemaParqueo.Repositories.Interfaces;

public interface IAutomovilesRepository
{
    PrqAutomovile ObtenerPorId(int id);
    IEnumerable<PrqAutomovile> ObtenerPorCriterios(string color, int? anioInicio, int? anioFin, string fabricante, string tipo);
    
    // Nuevos métodos de acción
    void Insertar(PrqAutomovile automovil);
    void Actualizar(PrqAutomovile automovil);
    void Borrar(int id);
}