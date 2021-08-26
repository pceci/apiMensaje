using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using apiMensaje.Codigo;
using Microsoft.EntityFrameworkCore;
using apiMensaje.Entities;
using apiMensaje.Helpers;

namespace apiMensaje.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class NotasController : ControllerBase
    {
        private readonly ILogger<NotasController> _logger;
        private readonly IapiContext _context;
        public NotasController(ILogger<NotasController> logger, IapiContext pApiContext)
        {
            _logger = logger;
            _context = pApiContext;
        }
        // GET: /Nota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nota>>> GetTodasNotas()
        {
            _logger.LogInformation("Get Notas");
            return await _context.Notas.ToListAsync();
        }
        // GET: /Nota/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nota>> GetNota(int id)
        {
            _logger.LogInformation("Get Nota id");
            var oNota = await _context.Notas.FindAsync(id);
            if (oNota == null)
            {
                return NotFound();
            }
            return oNota;
        }
        // DELETE: /Nota/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNota(int id)
        {
            _logger.LogInformation("Delete Nota");
            var oNota = await _context.Notas.FindAsync(id);
            if (oNota == null)
            {
                return NotFound();
            }
            _context.Notas.Remove(oNota);
            await _context.context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Nota>> Create(Nota pNota)
        {
            _logger.LogInformation("Create Nota");
            _context.Notas.Add(pNota);
            await _context.context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetNota),
                new { id = pNota.NotaId },
                pNota);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Nota pNota)
        {
            _logger.LogInformation("Update Nota");
            if (id != pNota.NotaId)
            {
                return BadRequest();
            }

            var oNota = await _context.Notas.FindAsync(id);
            if (oNota == null)
            {
                return NotFound();
            }

            oNota.Contenido = pNota.Contenido;
            oNota.Título = pNota.Título;

            try
            {
                await _context.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!NotaExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        private bool NotaExists(int id) =>
             _context.Notas.Any(e => e.NotaId == id);
    }
}
