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

    public decimal ObtenerPrecioPorHoraPorParqueo(int idParqueo)
    {
        var parqueo = _context.PrqParqueos.Find(idParqueo);
        return parqueo?.PrecioPorHora ?? 0;
    }

    public IEnumerable<PrqIngresoautomovile> ObtenerPorTipoYFechas(string tipo, DateTime fechaInicio, DateTime fechaFin)
    {
        var ingresos = _context.PrqIngresoautomoviles
            .Include(i => i.IdAutomovilNavigation)
            .Include(i => i.IdParqueoNavigation)
            .Where(i => i.IdAutomovilNavigation.Tipo.Contains(tipo) && 
                        i.FechaHoraEntrada >= fechaInicio && 
                        i.FechaHoraEntrada <= fechaFin)
            .ToList();

        foreach (var ingreso in ingresos)
        {
            decimal precio = ObtenerPrecioPorHoraPorParqueo(ingreso.IdParqueo);
            if (ingreso.IdParqueoNavigation != null)
            {
                ingreso.IdParqueoNavigation.PrecioPorHora = precio;
                ingreso.ParqueoAsociado = ingreso.IdParqueoNavigation;
            }
        }
        return ingresos;
    }

    public IEnumerable<PrqIngresoautomovile> ObtenerPorProvinciaYFechas(string nombreProvincia, DateTime fechaInicio, DateTime fechaFin)
    {
        var ingresos = _context.PrqIngresoautomoviles
            .Include(i => i.IdAutomovilNavigation)
            .Include(i => i.IdParqueoNavigation)
            .Where(i => i.IdParqueoNavigation.NombreProvincia.Contains(nombreProvincia) && 
                        i.FechaHoraEntrada >= fechaInicio && 
                        i.FechaHoraEntrada <= fechaFin)
            .ToList();

        foreach (var ingreso in ingresos)
        {
            decimal precio = ObtenerPrecioPorHoraPorParqueo(ingreso.IdParqueo);
            if (ingreso.IdParqueoNavigation != null)
            {
                ingreso.IdParqueoNavigation.PrecioPorHora = precio;
                ingreso.ParqueoAsociado = ingreso.IdParqueoNavigation;
            }
        }
        return ingresos;
    }

    // --- NUEVOS MÉTODOS PUNTO 9 ---

    public void Insertar(PrqIngresoautomovile ingreso)
    {
        _context.PrqIngresoautomoviles.Add(ingreso);
        _context.SaveChanges();
    }

    public void Actualizar(PrqIngresoautomovile ingreso)
    {
        _context.ChangeTracker.Clear();
        _context.PrqIngresoautomoviles.Update(ingreso);
        _context.SaveChanges();
    }

    public void Borrar(int id)
    {
        var ingreso = _context.PrqIngresoautomoviles.Find(id);
        if (ingreso != null)
        {
            _context.PrqIngresoautomoviles.Remove(ingreso);
            _context.SaveChanges();
        }
    }
}