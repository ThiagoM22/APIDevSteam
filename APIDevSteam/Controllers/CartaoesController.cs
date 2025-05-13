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
    public class CartaoesController : ControllerBase
    {
        private readonly APIDevSteamContext _context;

        public CartaoesController(APIDevSteamContext context)
        {
            _context = context;
        }

        // GET: api/Cartaoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cartao>>> GetCartao()
        {
            return await _context.Cartao.ToListAsync();
        }

        // GET: api/Cartaoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cartao>> GetCartao(Guid id)
        {
            var cartao = await _context.Cartao.FindAsync(id);

            if (cartao == null)
            {
                return NotFound();
            }

            return cartao;
        }

        // PUT: api/Cartaoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartao(Guid id, Cartao cartao)
        {
            if (id != cartao.CartaoId)
            {
                return BadRequest();
            }

            _context.Entry(cartao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartaoExists(id))
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

        // POST: api/Cartaoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cartao>> PostCartao(Cartao cartao)
        {
            _context.Cartao.Add(cartao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCartao", new { id = cartao.CartaoId }, cartao);
        }

        // DELETE: api/Cartaoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartao(Guid id)
        {
            var cartao = await _context.Cartao.FindAsync(id);
            if (cartao == null)
            {
                return NotFound();
            }

            _context.Cartao.Remove(cartao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartaoExists(Guid id)
        {
            return _context.Cartao.Any(e => e.CartaoId == id);
        }

        // Método para ativar ou desativar um cartão
        [HttpPut("{id}/AtivarDesativar")]
        public async Task<IActionResult> AtivarDesativarCartao(Guid id, [FromBody] bool ativo)
        {
            var cartao = await _context.Cartao.FindAsync(id);

            if (cartao == null)
                return NotFound(new { Mensagem = "Cartão não encontrado." });

            cartao.Ativo = ativo;
            _context.Entry(cartao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensagem = ativo ? "Cartão ativado com sucesso." : "Cartão desativado com sucesso.",
                CartaoId = cartao.CartaoId,
                Ativo = cartao.Ativo
            });
        }
        public class PagamentoRequest
        {
            public Guid CarrinhoId { get; set; }
            public Guid CartaoId { get; set; }
            public string CVV { get; set; }
        }

        // Método para finalizar a compra
        [HttpPost("FinalizarCompra")]
        public async Task<IActionResult> FinalizarCompra([FromBody] PagamentoRequest request)
        {
            // Validação do carrinho
            var carrinho = await _context.Carrinhos
                .Include(c => c.ItensCarrinhos)
                .FirstOrDefaultAsync(c => c.CarrinhoId == request.CarrinhoId);

            if (carrinho == null)
                return NotFound(new { Mensagem = "Carrinho não encontrado." });

            if (carrinho.Finalizado == true)
                return BadRequest(new { Mensagem = "Carrinho já finalizado." });

            // Validação do cartão
            var cartao = await _context.Cartao.FirstOrDefaultAsync(c => c.CartaoId == request.CartaoId && c.Ativo);
            if (cartao == null)
                return NotFound(new { Mensagem = "Cartão não encontrado ou inativo." });

            // Validação do CVV (simulação, pois não se armazena CVV)
            if (string.IsNullOrWhiteSpace(request.CVV) || request.CVV.Length < 3)
                return BadRequest(new { Mensagem = "CVV inválido." });

            // Simulação de processamento do pagamento
            // Aqui você pode integrar com um gateway real, se necessário
            var pagamento = new Pagamento
            {
                PagamentoId = Guid.NewGuid(),
                CarrinhoId = carrinho.CarrinhoId,
                CartaoId = cartao.CartaoId,
                DataPagamento = DateTime.UtcNow,
                ValorPago = carrinho.ValorTotal,
                Status = "Aprovado"
            };
            _context.Pagamento.Add(pagamento);

            // Finaliza o carrinho
            carrinho.Finalizado = true;
            carrinho.DataFinalizacao = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensagem = "Pagamento realizado e compra finalizada com sucesso.",
                Pagamento = new
                {
                    pagamento.PagamentoId,
                    pagamento.ValorPago,
                    pagamento.Status,
                    pagamento.DataPagamento
                },
                Carrinho = new
                {
                    carrinho.CarrinhoId,
                    carrinho.Finalizado,
                    carrinho.DataFinalizacao
                }
            });
        }


    }
}
