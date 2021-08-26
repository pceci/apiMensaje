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
    [Authorize]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IapiContext _context;
        public UsuariosController(ILogger<UsuariosController> logger, IapiContext pApiContext)
        {
            _logger = logger;
            _context = pApiContext;
        }
        // GET: /Nota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetTodasNotas()
        {
            _logger.LogInformation("Get Usuarios");
            return await _context.Usuarios.ToListAsync();
        }
        // GET:
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            _logger.LogInformation("Get Usuario id: + " + id);
            var o = await _context.Usuarios.FindAsync(id);
            if (o == null)
            {
                return NotFound();
            }
            return o;
        }
        // DELETE: 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete Usuario id: + " + id);
            var o = await _context.Usuarios.FindAsync(id);
            if (o == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(o);
            await _context.context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario pObjeto)
        {
            _logger.LogInformation("Create Usuario");
            _context.Usuarios.Add(pObjeto);
            await _context.context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = pObjeto.UsuarioId },
                pObjeto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Usuario pObjeto)
        {
            _logger.LogInformation("Update Usuario id: " + id);
            if (id != pObjeto.UsuarioId)
            {
                return BadRequest();
            }

            var o = await _context.Usuarios.FindAsync(id);
            if (o == null)
            {
                return NotFound();
            }

            o.Nombre = pObjeto.Nombre;
            o.Apellido = pObjeto.Apellido;
            o.Mail = pObjeto.Mail;

            try
            {
                await _context.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ObjetoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        private bool ObjetoExists(int id) =>
             _context.Usuarios.Any(e => e.UsuarioId == id);
    }
}
