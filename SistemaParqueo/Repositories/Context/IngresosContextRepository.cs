using Microsoft.EntityFrameworkCore;
using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaParqueo.Repositories.Context;

public class IngresosContextRepository : IIngresosRepository
{
    private readonly ParqueoContext _context;

    public IngresosContextRepository(ParqueoContext context)
    {
        _context = context;
    }

    public PrqIngresoautomovile ObtenerPorId(int id)
    {
        return _context.PrqIngresoautomoviles
            .Include(i => i.IdAutomovilNavigation)
            .Include(i => i.IdParqueoNavigation)
            .FirstOrDefault(i => i.Consecutivo == id);
    }

    // 1. Obtener Precio Por Hora Por Parqueo
    public decimal ObtenerPrecioPorHoraPorParqueo(int idParqueo)
    {
        var parqueo = _context.PrqParqueos.Find(idParqueo);
        return parqueo?.PrecioPorHora ?? 0;
    }

    // 2. Listado por tipo e intervalo de fechas
    public IEnumerable<PrqIngresoautomovile> ObtenerPorTipoYFechas(string tipo, DateTime fechaInicio, DateTime fechaFin)
    {
        var ingresos = _context.PrqIngresoautomoviles
            .Include(i => i.IdAutomovilNavigation)
            .Include(i => i.IdParqueoNavigation)
            .Where(i => i.IdAutomovilNavigation.Tipo.Contains(tipo) && 
                        i.FechaHoraEntrada >= fechaInicio && 
                        i.FechaHoraEntrada <= fechaFin)
            .ToList();

        // Se debe llamar a la función del punto 1
        foreach (var ingreso in ingresos)
        {
            decimal precio = ObtenerPrecioPorHoraPorParqueo(ingreso.IdParqueo);
            if (ingreso.IdParqueoNavigation != null)
            {
                ingreso.IdParqueoNavigation.PrecioPorHora = precio;
                ingreso.ParqueoAsociado = ingreso.IdParqueoNavigation; // Enlaza para calcular el monto a cancelar
            }
        }
        return ingresos;
    }

    // 3. Listado por provincia e intervalo de fechas
    public IEnumerable<PrqIngresoautomovile> ObtenerPorProvinciaYFechas(string nombreProvincia, DateTime fechaInicio, DateTime fechaFin)
    {
        var ingresos = _context.PrqIngresoautomoviles
            .Include(i => i.IdAutomovilNavigation)
            .Include(i => i.IdParqueoNavigation)
            .Where(i => i.IdParqueoNavigation.NombreProvincia.Contains(nombreProvincia) && 
                        i.FechaHoraEntrada >= fechaInicio && 
                        i.FechaHoraEntrada <= fechaFin)
            .ToList();

        // Se debe llamar a la función del punto 1
        foreach (var ingreso in ingresos)
        {
            decimal precio = ObtenerPrecioPorHoraPorParqueo(ingreso.IdParqueo);
            if (ingreso.IdParqueoNavigation != null)
            {
                ingreso.IdParqueoNavigation.PrecioPorHora = precio;
                ingreso.ParqueoAsociado = ingreso.IdParqueoNavigation; // Enlaza para calcular el monto a cancelar
            }
        }
        return ingresos;
    }
}