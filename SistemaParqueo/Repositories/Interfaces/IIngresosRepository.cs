using SistemaParqueo.Models;
using System;
using System.Collections.Generic;

namespace SistemaParqueo.Repositories.Interfaces;

public interface IIngresosRepository
{
    PrqIngresoautomovile ObtenerPorId(int id);
    decimal ObtenerPrecioPorHoraPorParqueo(int idParqueo);
    IEnumerable<PrqIngresoautomovile> ObtenerPorTipoYFechas(string tipo, DateTime fechaInicio, DateTime fechaFin);
    IEnumerable<PrqIngresoautomovile> ObtenerPorProvinciaYFechas(string nombreProvincia, DateTime fechaInicio, DateTime fechaFin);
    
    // Nuevos métodos de acción
    void Insertar(PrqIngresoautomovile ingreso);
    void Actualizar(PrqIngresoautomovile ingreso);
    void Borrar(int id);
}