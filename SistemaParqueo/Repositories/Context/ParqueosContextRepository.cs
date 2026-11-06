using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaParqueo.Repositories.Context;

public class ParqueosContextRepository : IParqueosRepository
{
    private readonly ParqueoContext _context;

    public ParqueosContextRepository(ParqueoContext context)
    {
        _context = context;
    }

    public PrqParqueo ObtenerPorId(int id)
    {
        return _context.PrqParqueos.Find(id);
    }

    public IEnumerable<PrqParqueo> ObtenerPorCriterios(string nombreProvincia, string nombre, decimal? precioMin, decimal? precioMax)
    {
        var query = _context.PrqParqueos.AsQueryable();

        if (!string.IsNullOrEmpty(nombreProvincia)) query = query.Where(p => p.NombreProvincia.Contains(nombreProvincia));
        if (!string.IsNullOrEmpty(nombre)) query = query.Where(p => p.Nombre.Contains(nombre));
        
        if (precioMin.HasValue) query = query.Where(p => p.PrecioPorHora >= precioMin.Value);
        if (precioMax.HasValue) query = query.Where(p => p.PrecioPorHora <= precioMax.Value);

        return query.ToList();
    }

    // --- NUEVOS MÉTODOS PUNTO 9 ---

    public void Insertar(PrqParqueo parqueo)
    {
        _context.PrqParqueos.Add(parqueo);
        _context.SaveChanges();
    }

    public void Actualizar(PrqParqueo parqueo)
    {
        _context.ChangeTracker.Clear();
        _context.PrqParqueos.Update(parqueo);
        _context.SaveChanges();
    }

    public void Borrar(int id)
    {
        var parqueo = _context.PrqParqueos.Find(id);
        if (parqueo != null)
        {
            _context.PrqParqueos.Remove(parqueo);
            _context.SaveChanges();
        }
    }
}