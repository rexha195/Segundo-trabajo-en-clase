using SistemaParqueo.Models;
using System;
using System.Collections.Generic;

namespace SistemaParqueo.Repositories.Interfaces;

public interface IIngresosRepository
{
    // Búsqueda por llave primaria por default
    PrqIngresoautomovile ObtenerPorId(int id);
    
    // 1. Función para obtener el precio por hora por parqueo
    decimal ObtenerPrecioPorHoraPorParqueo(int idParqueo);
    
    // 2. Listado de automóviles por tipo en intervalo de fechas
    IEnumerable<PrqIngresoautomovile> ObtenerPorTipoYFechas(string tipo, DateTime fechaInicio, DateTime fechaFin);
    
    // 3. Listado de automóviles por provincia en intervalo de fechas
    IEnumerable<PrqIngresoautomovile> ObtenerPorProvinciaYFechas(string nombreProvincia, DateTime fechaInicio, DateTime fechaFin);
}