using Newtonsoft.Json;
using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SistemaParqueo.Repositories.Json;

public class AutomovilesJsonRepository : IAutomovilesRepository
{
    private readonly string _filePath = @"..\Datos_Json\automoviles.json";
    private List<PrqAutomovile> _automoviles;

    public AutomovilesJsonRepository()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _automoviles = JsonConvert.DeserializeObject<List<PrqAutomovile>>(json) ?? new List<PrqAutomovile>();
        }
        else
        {
            _automoviles = new List<PrqAutomovile>();
        }
    }

    public PrqAutomovile ObtenerPorId(int id)
    {
        return _automoviles.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<PrqAutomovile> ObtenerPorCriterios(string color, int? anioInicio, int? anioFin, string fabricante, string tipo)
    {
        var query = _automoviles.AsQueryable();

        if (!string.IsNullOrEmpty(color)) query = query.Where(a => a.Color.Contains(color, System.StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(fabricante)) query = query.Where(a => a.Fabricante.Contains(fabricante, System.StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(tipo)) query = query.Where(a => a.Tipo.Contains(tipo, System.StringComparison.OrdinalIgnoreCase));

        if (anioInicio.HasValue) query = query.Where(a => a.Ano >= anioInicio.Value);
        if (anioFin.HasValue) query = query.Where(a => a.Ano <= anioFin.Value);

        return query.ToList();
    }

    // --- NUEVOS MÉTODOS PUNTO 9 (JSON) ---

    private void GuardarCambios()
    {
        string json = JsonConvert.SerializeObject(_automoviles, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public void Insertar(PrqAutomovile automovil)
    {
        automovil.Id = _automoviles.Any() ? _automoviles.Max(a => a.Id) + 1 : 1;
        _automoviles.Add(automovil);
        GuardarCambios();
    }

    public void Actualizar(PrqAutomovile automovil)
    {
        var existente = _automoviles.FirstOrDefault(a => a.Id == automovil.Id);
        if (existente != null)
        {
            existente.Color = automovil.Color;
            existente.Ano = automovil.Ano;
            existente.Fabricante = automovil.Fabricante;
            existente.Tipo = automovil.Tipo;
            GuardarCambios();
        }
    }

    public void Borrar(int id)
    {
        var automovil = _automoviles.FirstOrDefault(a => a.Id == id);
        if (automovil != null)
        {
            _automoviles.Remove(automovil);
            GuardarCambios();
        }
    }
}