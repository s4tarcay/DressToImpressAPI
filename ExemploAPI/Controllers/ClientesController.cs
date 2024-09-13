using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExemploAPI.Data;
using ExemploAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExemploAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }


        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(Guid id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.ClienteId }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("alternar/{id}")]
        public async Task<IActionResult> AlternarCadastro(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            cliente.CadastroAtivo = !cliente.CadastroAtivo;
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }

        [HttpGet("/filtrar/{nome}")]
        public async Task<IActionResult> BuscarNome(string nome)
        {
            var listaClientes = _context.Clientes.Where(c =>
            c.ClienteNome.Contains(nome)).ToList();
            if (listaClientes.Count > 0)
            {
                return Ok(listaClientes);
            }
            return NotFound(nome);
        }

        [HttpGet("/paginacao/{numeroPagina}/{qtdadeRegistros}")]
        public async Task<IActionResult> Paginacao(int numeroPagina, int qtdadeRegistros)
        {
            var totalItens = await _context.Clientes.CountAsync();
            var pularRegistros = (numeroPagina - 1) * qtdadeRegistros;
            var listaClientes = await _context.Clientes.Skip
                (pularRegistros).Take(qtdadeRegistros).ToListAsync();
            if (listaClientes.Count >= 0)
            {

                return Ok(listaClientes);
            }
            return NotFound();
        }
    

        private bool ClienteExists(Guid id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
