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
    public class CuponsController : ControllerBase
    {
        private readonly APIDevSteamContext _context;

        public CuponsController(APIDevSteamContext context)
        {
            _context = context;
        }

        // GET: api/Cupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupom>>> GetCupons()
        {
            return await _context.Cupons.ToListAsync();
        }

        // GET: api/Cupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupom>> GetCupom(Guid id)
        {
            var cupom = await _context.Cupons.FindAsync(id);

            if (cupom == null)
            {
                return NotFound();
            }

            return cupom;
        }

        // PUT: api/Cupons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupom(Guid id, Cupom cupom)
        {
            if (id != cupom.CupomId)
            {
                return BadRequest();
            }

            _context.Entry(cupom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CupomExists(id))
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

        // POST: api/Cupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cupom>> PostCupom(Cupom cupom)
        {
            _context.Cupons.Add(cupom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCupom", new { id = cupom.CupomId }, cupom);
        }

        // DELETE: api/Cupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupom(Guid id)
        {
            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom == null)
            {
                return NotFound();
            }

            _context.Cupons.Remove(cupom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CupomExists(Guid id)
        {
            return _context.Cupons.Any(e => e.CupomId == id);
        }

    }
}
