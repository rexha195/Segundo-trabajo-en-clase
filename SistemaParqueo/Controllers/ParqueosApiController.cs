using Microsoft.AspNetCore.Mvc;
using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using System.Collections.Generic;

namespace SistemaParqueo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParqueosApiController : ControllerBase
{
    private readonly IParqueosRepository _repository;

    public ParqueosApiController(IParqueosRepository repository)
    {
        _repository = repository;
    }

    // GET: api/ParqueosApi/5
    [HttpGet("{id}")]
    public ActionResult<PrqParqueo> GetById(int id)
    {
        var parqueo = _repository.ObtenerPorId(id);
        if (parqueo == null) return NotFound();
        return Ok(parqueo);
    }

    // GET: api/ParqueosApi/buscar
    [HttpGet("buscar")]
    public ActionResult<IEnumerable<PrqParqueo>> GetByCriterios([FromQuery] string? nombreProvincia, [FromQuery] string? nombre, [FromQuery] decimal? precioMin, [FromQuery] decimal? precioMax)
    {
        var resultados = _repository.ObtenerPorCriterios(nombreProvincia, nombre, precioMin, precioMax);
        return Ok(resultados);
    }

    // POST: api/ParqueosApi
    [HttpPost]
    public ActionResult Insertar([FromBody] PrqParqueo parqueo)
    {
        if (parqueo == null) return BadRequest();
        _repository.Insertar(parqueo);
        return CreatedAtAction(nameof(GetById), new { id = parqueo.Id }, parqueo);
    }

    // PUT: api/ParqueosApi/5
    [HttpPut("{id}")]
    public ActionResult Actualizar(int id, [FromBody] PrqParqueo parqueo)
    {
        if (id != parqueo.Id) return BadRequest("El ID de la ruta no coincide con el ID del objeto.");
        
        var existente = _repository.ObtenerPorId(id);
        if (existente == null) return NotFound();

        _repository.Actualizar(parqueo);
        return NoContent();
    }

    // DELETE: api/ParqueosApi/5
    [HttpDelete("{id}")]
    public ActionResult Borrar(int id)
    {
        var existente = _repository.ObtenerPorId(id);
        if (existente == null) return NotFound();

        _repository.Borrar(id);
        return NoContent();
    }
}