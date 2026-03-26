using Microsoft.AspNetCore.Mvc;
using PresupuestosAPI.Models;
using PresupuestosAPI.Services;

namespace PresupuestosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoSeccionController : ControllerBase
    {
        private readonly PresupuestoSeccionService _seccionService;

        public PresupuestoSeccionController(PresupuestoSeccionService seccionService)
        {
            _seccionService = seccionService;
        }

        [HttpGet("seccion/{id}")]
        public async Task<IActionResult> GetSeccionById(int id)
        {
            var seccion = await _seccionService.GetSeccionByIdAsync(id);

            if (seccion == null)
            {
                return NotFound();
            }

            return Ok(seccion);
        }

        [HttpGet("presupuesto/{presupuestoId}")]
        public async Task<IActionResult> GetSeccionesByPresupuestoId(int presupuestoId)
        {
            var secciones = await _seccionService.GetSeccionesByPresupuestoIdAsync(presupuestoId);
            if (!secciones.Any())
            {
                return NotFound();
            }
            return Ok(secciones);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeccion([FromBody] PresupuestoSeccion seccion)
        {
            var createdSeccion = await _seccionService.CreateSeccionAsync(seccion);

            return CreatedAtAction(
                nameof(GetSeccionById),
                new { id = createdSeccion.IdSection },
                createdSeccion
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeccion(int id, [FromBody] PresupuestoSeccion seccion)
        {
            var updatedSeccion = await _seccionService.UpdateSeccionAsync(id, seccion);
            if (updatedSeccion == null) 
            {
                return NotFound();
            }
            return Ok(updatedSeccion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeccion(int id)
        {
            var deleted = await _seccionService.DeleteSeccionAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
