using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExemploAPI.Data;
using ExemploAPI.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ExemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VendasController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Vendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendas()
        {
            return await _context.Vendas.ToListAsync();
        }

        // GET: api/Vendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> GetVenda(Guid id)
        {
            var venda = await _context.Vendas.FindAsync(id);

            if (venda == null)
            {
                    return NotFound();
            }

            return venda;
        }

        // PUT: api/Vendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda(Guid id, Venda venda)
        {
            if (id != venda.VendaId)
            {
                return BadRequest();
            }

            _context.Entry(venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(id))
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

        [Authorize]
        // POST: api/Vendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Venda>> PostVenda(Venda venda)
        {
            var ultimaVenda = _context.Vendas.OrderBy(v => v.DataVenda).FirstOrDefaultAsync().Result;
            if (ultimaVenda == null)
            {
                venda.NumeroPedido = 1;
                venda.TotalVenda = 0;
            }
            else
            {
                venda.NumeroPedido = ultimaVenda.NumeroPedido += 1;
                venda.TotalVenda = 0;
            }

            // Obter o ID do usuário de forma mais segura
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Usuário não autenticado");
            }
            venda.UserId = userId;

            var cliente = await _context.Clientes.FindAsync(venda.ClienteId);
            venda.Cliente = cliente;

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVenda", new { id = venda.VendaId }, venda);
        }

        // DELETE: api/Vendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(Guid id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("filtrarPorData/{dataVenda}")]
        public async Task<IActionResult> BuscaDataVenda(string dataVenda)
        {
            var listaPedidos = _context.Vendas.Where(v =>
            v.DataVenda.ToString().Contains(dataVenda));
            if(listaPedidos.Count() > 0)
            {
                return Ok(listaPedidos);
            }
                return NotFound();
        }

        //busca por intervalo de datas/hora
        [HttpGet("filtrarIntervaloData/{dataInicio}/{dataFim}")]
        public async Task<IActionResult> BuscaPorIntervalo(DateTime
            dataInicio, DateTime dataFim)
        {
            var listaPedidos = await _context.Vendas.Where(v =>
            v.DataVenda >= dataInicio && v.DataVenda <= dataFim).ToListAsync();
            if (listaPedidos.Count > 0)
            {
                return Ok(listaPedidos);
            }
            return NotFound();
        }

        private bool VendaExists(Guid id)
        {
            return _context.Vendas.Any(e => e.VendaId == id);
        }
    }
}
