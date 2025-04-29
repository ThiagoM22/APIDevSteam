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
    public class CuponsCarrinhosController : ControllerBase
    {
        private readonly APIDevSteamContext _context;

        public CuponsCarrinhosController(APIDevSteamContext context)
        {
            _context = context;
        }

        // GET: api/CuponsCarrinhos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CupomCarrinho>>> GetCuponsCarrinhos()
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
        [HttpPost("AplicarDesconto")]
        public async Task<IActionResult> AplicarDesconto(Guid carrinhoId, Guid cupomId)
        {
            // Verifica se o carrinho existe
            var carrinho = await _context.Carrinhos
                .Include(c => c.ItensCarrinhos)
                .ThenInclude(ic => ic.Jogo)
                .FirstOrDefaultAsync(c => c.CarrinhoId == carrinhoId);

            if (carrinho == null)
                return NotFound(new { Mensagem = "Carrinho não encontrado." });

            // Verifica se o cupom existe
            var cupom = await _context.Cupons.FirstOrDefaultAsync(c => c.CupomId == cupomId);
            if (cupom == null)
                return NotFound(new { Mensagem = "Cupom não encontrado." });

            // Verifica se o cupom está ativo
            if (!cupom.Ativo)
                return BadRequest(new { Mensagem = "Cupom inativo." });

            // Verifica se o cupom está dentro do período de validade
            if (cupom.DataValidade.HasValue && cupom.DataValidade.Value < DateTime.UtcNow)
                return BadRequest(new { Mensagem = "Cupom expirado." });

            // Calcula o desconto
            var desconto = (carrinho.ValorTotal * cupom.Desconto) / 100;
            var valorComDesconto = carrinho.ValorTotal - desconto;

            // Atualiza o valor total do carrinho
            carrinho.ValorTotal = valorComDesconto;

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna o carrinho atualizado
            return Ok(new
            {
                Mensagem = "Desconto aplicado com sucesso.",
                Carrinho = new
                {
                    carrinho.CarrinhoId,
                    carrinho.ValorTotal,
                    DescontoAplicado = desconto,
                    ValorFinal = carrinho.ValorTotal
                }
            });
        }



    }
}
