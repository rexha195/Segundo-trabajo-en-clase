using Newtonsoft.Json;
using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SistemaParqueo.Repositories.Json;

public class IngresosJsonRepository : IIngresosRepository
{
    private readonly string _filePath = @"..\Datos_Json\ingreso_automoviles.json";
    private List<PrqIngresoautomovile> _ingresos;
    private readonly IAutomovilesRepository _automovilesRepo;
    private readonly IParqueosRepository _parqueosRepo;

    public IngresosJsonRepository(IAutomovilesRepository automovilesRepo, IParqueosRepository parqueosRepo)
    {
        _automovilesRepo = automovilesRepo;
        _parqueosRepo = parqueosRepo;

        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _ingresos = JsonConvert.DeserializeObject<List<PrqIngresoautomovile>>(json) ?? new List<PrqIngresoautomovile>();
            
            foreach (var ingreso in _ingresos)
            {
                ingreso.IdAutomovilNavigation = _automovilesRepo.ObtenerPorId(ingreso.IdAutomovil);
                ingreso.IdParqueoNavigation = _parqueosRepo.ObtenerPorId(ingreso.IdParqueo);
            }
        }
        else
        {
            _ingresos = new List<PrqIngresoautomovile>();
        }
    }

    public PrqIngresoautomovile ObtenerPorId(int id)
    {
        return _ingresos.FirstOrDefault(i => i.Consecutivo == id);
    }

    public decimal ObtenerPrecioPorHoraPorParqueo(int idParqueo)
    {
        var parqueo = _parqueosRepo.ObtenerPorId(idParqueo);
        return parqueo?.PrecioPorHora ?? 0;
    }

    public IEnumerable<PrqIngresoautomovile> ObtenerPorTipoYFechas(string tipo, DateTime fechaInicio, DateTime fechaFin)
    {
        var filtrados = _ingresos
            .Where(i => i.IdAutomovilNavigation != null && 
                        i.IdAutomovilNavigation.Tipo.Contains(tipo, StringComparison.OrdinalIgnoreCase) && 
                        i.FechaHoraEntrada >= fechaInicio && 
                        i.FechaHoraEntrada <= fechaFin)
            .ToList();

        foreach (var ingreso in filtrados)
        {
            decimal precio = ObtenerPrecioPorHoraPorParqueo(ingreso.IdParqueo);
            if (ingreso.IdParqueoNavigation != null)
            {
                ingreso.IdParqueoNavigation.PrecioPorHora = precio;
                ingreso.ParqueoAsociado = ingreso.IdParqueoNavigation;
            }
        }
        return filtrados;
    }

    public IEnumerable<PrqIngresoautomovile> ObtenerPorProvinciaYFechas(string nombreProvincia, DateTime fechaInicio, DateTime fechaFin)
    {
        var filtrados = _ingresos
            .Where(i => i.IdParqueoNavigation != null && 
                        i.IdParqueoNavigation.NombreProvincia.Contains(nombreProvincia, StringComparison.OrdinalIgnoreCase) && 
                        i.FechaHoraEntrada >= fechaInicio && 
                        i.FechaHoraEntrada <= fechaFin)
            .ToList();

        foreach (var ingreso in filtrados)
        {
            decimal precio = ObtenerPrecioPorHoraPorParqueo(ingreso.IdParqueo);
            if (ingreso.IdParqueoNavigation != null)
            {
                ingreso.IdParqueoNavigation.PrecioPorHora = precio;
                ingreso.ParqueoAsociado = ingreso.IdParqueoNavigation;
            }
        }
        return filtrados;
    }

    // --- NUEVOS MÉTODOS PUNTO 9 (JSON) ---

    private void GuardarCambios()
    {
        // Evitamos ciclos circulares al serializar relaciones
        var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        string json = JsonConvert.SerializeObject(_ingresos, Formatting.Indented, settings);
        File.WriteAllText(_filePath, json);
    }

    public void Insertar(PrqIngresoautomovile ingreso)
    {
        ingreso.Consecutivo = _ingresos.Any() ? _ingresos.Max(i => i.Consecutivo) + 1 : 1;
        _ingresos.Add(ingreso);
        GuardarCambios();
    }

    public void Actualizar(PrqIngresoautomovile ingreso)
    {
        var existente = _ingresos.FirstOrDefault(i => i.Consecutivo == ingreso.Consecutivo);
        if (existente != null)
        {
            existente.IdParqueo = ingreso.IdParqueo;
            existente.IdAutomovil = ingreso.IdAutomovil;
            existente.FechaHoraEntrada = ingreso.FechaHoraEntrada;
            existente.FechaHoraSalida = ingreso.FechaHoraSalida;
            GuardarCambios();
        }
    }

    public void Borrar(int id)
    {
        var ingreso = _ingresos.FirstOrDefault(i => i.Consecutivo == id);
        if (ingreso != null)
        {
            _ingresos.Remove(ingreso);
            GuardarCambios();
        }
    }
}