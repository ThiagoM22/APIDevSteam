using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDevSteam.Data;
using APIDevSteam.Models;

namespace APIDevSteamJau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponsCarrinhosController : ControllerBase
    {
        private readonly APIDevSteamContext _context;

        public CuponsCarrinhosController(APIDevSteamContext context)
        {
            _context = context;
        }

        // GET: api/CuponsCarrinhos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CupomCarrinho>>> GetCuponsCarrinho()
        {
            return await _context.CuponsCarrinhos.ToListAsync();
        }

        // GET: api/CuponsCarrinhos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CupomCarrinho>> GetCupomCarrinho(Guid id)
        {
            var cupomCarrinho = await _context.CuponsCarrinhos.FindAsync(id);

            if (cupomCarrinho == null)
            {
                return NotFound();
            }

            return cupomCarrinho;
        }

        // PUT: api/CuponsCarrinhos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupomCarrinho(Guid id, CupomCarrinho cupomCarrinho)
        {
            if (id != cupomCarrinho.CupomCarrinhoId)
            {
                return BadRequest();
            }

            _context.Entry(cupomCarrinho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CupomCarrinhoExists(id))
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

        // POST: api/CuponsCarrinhos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CupomCarrinho>> PostCupomCarrinho(CupomCarrinho cupomCarrinho)
        {
            _context.CuponsCarrinhos.Add(cupomCarrinho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCupomCarrinho", new { id = cupomCarrinho.CupomCarrinhoId }, cupomCarrinho);
        }

        // DELETE: api/CuponsCarrinhos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupomCarrinho(Guid id)
        {
            var cupomCarrinho = await _context.CuponsCarrinhos.FindAsync(id);
            if (cupomCarrinho == null)
            {
                return NotFound();
            }

            _context.CuponsCarrinhos.Remove(cupomCarrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CupomCarrinhoExists(Guid id)
        {
            return _context.CuponsCarrinhos.Any(e => e.CupomCarrinhoId == id);
        }

        // [HttpPost] : AplicarCupom
        [HttpPost("AplicarCupom")]
        public async Task<IActionResult> AplicarCupom(Guid idCupom, Guid idCarrinho)
        {

            // Verificar Se o idCarrinho exite no contexto Carrinhos;
            var carrinho = await _context.Carrinhos.FindAsync(idCarrinho);
            if (carrinho == null)
            {
                return NotFound("Carrinho não Encontrado!");
            }

            // Verificar se o idCupom exite no contexto Cupons;
            var cupom = await _context.Cupons.FindAsync(idCupom);
            if (cupom == null)
            {
                return NotFound("Cupom não Encontrado!");
            }

            // Verificar se o cupom já foi aplicado no carrinho
            var cupomCarrinho = await _context.CuponsCarrinhos
                .FirstOrDefaultAsync(cc => cc.CarrinhoId == idCarrinho && cc.CupomId == idCupom);
            if (cupomCarrinho != null)
            {
                return BadRequest("Cupom já aplicado no carrinho!");
            }

            // Verificar se o carrinho já foi finalizado
            if (carrinho.Finalizado == true)
            {
                return BadRequest("Carrinho já finalizado!");
            }

            // Verificando a data de validade do cupom, somente se a data de validade for diferente de nulo
            if (cupom.DataValidade != null)
            {
                if (cupom.DataValidade < DateTime.Now)
                {
                    // Se a data de validade for menor que a data atual, o cupom está expirado
                    cupom.Ativo = false;
                    return BadRequest("Cupom expirado!");
                }
            }

            // Verificando o limite de uso do cupom
            if (cupom.LimiteUso != null)
            {
                if (cupom.LimiteUso <= 0)
                {
                    return BadRequest("Cupom sem limite de uso!");
                }
            }

            // Verfiicando se o cupom esta ativo
            if (cupom.Ativo == false)
            {
                return BadRequest("Cupom desativado!");
            }

            // Aplicando o cupom no carrinho 
            cupomCarrinho = new CupomCarrinho
            {
                CupomCarrinhoId = Guid.NewGuid(),
                CarrinhoId = idCarrinho,
                CupomId = idCupom,
                Validade = DateTime.Now
            };

            _context.CuponsCarrinhos.Add(cupomCarrinho);

            // Atualizando o limite de uso do cupom, caso seja diferente de nulo
            // Se atingir o limite de uso, o cupom será desativado
            if (cupom.LimiteUso != null)
            {
                cupom.LimiteUso--;
                if (cupom.LimiteUso == 0)
                {
                    cupom.Ativo = false;
                }
            }
            _context.Entry(cupom).State = EntityState.Modified;

            await _context.SaveChangesAsync();


            return Ok(cupomCarrinho);
        }
    }
}