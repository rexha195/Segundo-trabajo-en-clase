using Microsoft.AspNetCore.Mvc;
using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace SistemaParqueo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IngresosApiController : ControllerBase
{
    private readonly IIngresosRepository _repository;

    public IngresosApiController(IIngresosRepository repository)
    {
        _repository = repository;
    }

    // GET: api/IngresosApi/5
    [HttpGet("{id}")]
    public ActionResult<PrqIngresoautomovile> GetById(int id)
    {
        var ingreso = _repository.ObtenerPorId(id);
        if (ingreso == null) return NotFound();
        return Ok(ingreso);
    }

    // GET: api/IngresosApi/buscarPorTipo
    [HttpGet("buscarPorTipo")]
    public ActionResult<IEnumerable<PrqIngresoautomovile>> GetByTipo([FromQuery] string tipo, [FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
    {
        var resultados = _repository.ObtenerPorTipoYFechas(tipo, fechaInicio, fechaFin);
        return Ok(resultados);
    }

    // GET: api/IngresosApi/buscarPorProvincia
    [HttpGet("buscarPorProvincia")]
    public ActionResult<IEnumerable<PrqIngresoautomovile>> GetByProvincia([FromQuery] string provincia, [FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
    {
        var resultados = _repository.ObtenerPorProvinciaYFechas(provincia, fechaInicio, fechaFin);
        return Ok(resultados);
    }

    // POST: api/IngresosApi
    [HttpPost]
    public ActionResult Insertar([FromBody] PrqIngresoautomovile ingreso)
    {
        if (ingreso == null) return BadRequest();
        _repository.Insertar(ingreso);
        return CreatedAtAction(nameof(GetById), new { id = ingreso.Consecutivo }, ingreso);
    }

    // PUT: api/IngresosApi/5
    [HttpPut("{id}")]
    public ActionResult Actualizar(int id, [FromBody] PrqIngresoautomovile ingreso)
    {
        if (id != ingreso.Consecutivo) return BadRequest("El ID de la ruta no coincide con el consecutivo del objeto.");
        
        var existente = _repository.ObtenerPorId(id);
        if (existente == null) return NotFound();

        _repository.Actualizar(ingreso);
        return NoContent();
    }

    // DELETE: api/IngresosApi/5
    [HttpDelete("{id}")]
    public ActionResult Borrar(int id)
    {
        var existente = _repository.ObtenerPorId(id);
        if (existente == null) return NotFound();

        _repository.Borrar(id);
        return NoContent();
    }
}