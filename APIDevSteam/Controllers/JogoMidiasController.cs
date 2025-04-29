using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDevSteam.Data;
using APIDevSteam.Models;

namespace APIDevSteam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoMidiasController : ControllerBase
    {
        private readonly APIDevSteamContext _context;

        public JogoMidiasController(APIDevSteamContext context)
        {
            _context = context;
        }

        // GET: api/JogoMidias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoMidia>>> GetJogosMidias()
        {
            return await _context.JogosMidias.ToListAsync();
        }

        // GET: api/JogoMidias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JogoMidia>> GetJogoMidia(Guid id)
        {
            var jogoMidia = await _context.JogosMidias.FindAsync(id);

            if (jogoMidia == null)
            {
                return NotFound();
            }

            return jogoMidia;
        }

        // PUT: api/JogoMidias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJogoMidia(Guid id, JogoMidia jogoMidia)
        {
            if (id != jogoMidia.JogoMidiaId)
            {
                return BadRequest();
            }

            _context.Entry(jogoMidia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JogoMidiaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/JogoMidias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JogoMidia>> PostJogoMidia(JogoMidia jogoMidia)
        {
            _context.JogosMidias.Add(jogoMidia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJogoMidia", new { id = jogoMidia.JogoMidiaId }, jogoMidia);
        }

        // DELETE: api/JogoMidias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogoMidia(Guid id)
        {
            var jogoMidia = await _context.JogosMidias.FindAsync(id);
            if (jogoMidia == null)
            {
                return NotFound();
            }

            _context.JogosMidias.Remove(jogoMidia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JogoMidiaExists(Guid id)
        {
            return _context.JogosMidias.Any(e => e.JogoMidiaId == id);
        }
    }
}
