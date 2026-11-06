using Microsoft.AspNetCore.Mvc;
using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using System.Collections.Generic;

namespace SistemaParqueo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutomovilesApiController : ControllerBase
{
    private readonly IAutomovilesRepository _repository;

    public AutomovilesApiController(IAutomovilesRepository repository)
    {
        _repository = repository;
    }

    // GET: api/AutomovilesApi/5
    [HttpGet("{id}")]
    public ActionResult<PrqAutomovile> GetById(int id)
    {
        var automovil = _repository.ObtenerPorId(id);
        if (automovil == null) return NotFound();
        return Ok(automovil);
    }

    // GET: api/AutomovilesApi/buscar
    [HttpGet("buscar")]
    public ActionResult<IEnumerable<PrqAutomovile>> GetByCriterios([FromQuery] string? color, [FromQuery] int? anioInicio, [FromQuery] int? anioFin, [FromQuery] string? fabricante, [FromQuery] string? tipo)
    {
        var resultados = _repository.ObtenerPorCriterios(color, anioInicio, anioFin, fabricante, tipo);
        return Ok(resultados);
    }

    // POST: api/AutomovilesApi
    [HttpPost]
    public ActionResult Insertar([FromBody] PrqAutomovile automovil)
    {
        if (automovil == null) return BadRequest();
        _repository.Insertar(automovil);
        return CreatedAtAction(nameof(GetById), new { id = automovil.Id }, automovil);
    }

    // PUT: api/AutomovilesApi/5
    [HttpPut("{id}")]
    public ActionResult Actualizar(int id, [FromBody] PrqAutomovile automovil)
    {
        if (id != automovil.Id) return BadRequest("El ID de la ruta no coincide con el ID del objeto.");
        
        var existente = _repository.ObtenerPorId(id);
        if (existente == null) return NotFound();

        _repository.Actualizar(automovil);
        return NoContent();
    }

    // DELETE: api/AutomovilesApi/5
    [HttpDelete("{id}")]
    public ActionResult Borrar(int id)
    {
        var existente = _repository.ObtenerPorId(id);
        if (existente == null) return NotFound();

        _repository.Borrar(id);
        return NoContent();
    }
}