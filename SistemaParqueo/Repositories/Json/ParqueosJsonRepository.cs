using Newtonsoft.Json;
using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SistemaParqueo.Repositories.Json;

public class ParqueosJsonRepository : IParqueosRepository
{
    private readonly string _filePath = @"..\Datos_Json\parqueos.json";
    private List<PrqParqueo> _parqueos;

    public ParqueosJsonRepository()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _parqueos = JsonConvert.DeserializeObject<List<PrqParqueo>>(json) ?? new List<PrqParqueo>();
        }
        else
        {
            _parqueos = new List<PrqParqueo>();
        }
    }

    public PrqParqueo ObtenerPorId(int id)
    {
        return _parqueos.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<PrqParqueo> ObtenerPorCriterios(string nombreProvincia, string nombre, decimal? precioMin, decimal? precioMax)
    {
        var query = _parqueos.AsQueryable();

        if (!string.IsNullOrEmpty(nombreProvincia)) query = query.Where(p => p.NombreProvincia.Contains(nombreProvincia, System.StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(nombre)) query = query.Where(p => p.Nombre.Contains(nombre, System.StringComparison.OrdinalIgnoreCase));
        
        if (precioMin.HasValue) query = query.Where(p => p.PrecioPorHora >= precioMin.Value);
        if (precioMax.HasValue) query = query.Where(p => p.PrecioPorHora <= precioMax.Value);

        return query.ToList();
    }

    // --- NUEVOS MÉTODOS PUNTO 9 (JSON) ---

    private void GuardarCambios()
    {
        string json = JsonConvert.SerializeObject(_parqueos, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public void Insertar(PrqParqueo parqueo)
    {
        parqueo.Id = _parqueos.Any() ? _parqueos.Max(p => p.Id) + 1 : 1;
        _parqueos.Add(parqueo);
        GuardarCambios();
    }

    public void Actualizar(PrqParqueo parqueo)
    {
        var existente = _parqueos.FirstOrDefault(p => p.Id == parqueo.Id);
        if (existente != null)
        {
            existente.NombreProvincia = parqueo.NombreProvincia;
            existente.Nombre = parqueo.Nombre;
            existente.PrecioPorHora = parqueo.PrecioPorHora;
            GuardarCambios();
        }
    }

    public void Borrar(int id)
    {
        var parqueo = _parqueos.FirstOrDefault(p => p.Id == id);
        if (parqueo != null)
        {
            _parqueos.Remove(parqueo);
            GuardarCambios();
        }
    }
}