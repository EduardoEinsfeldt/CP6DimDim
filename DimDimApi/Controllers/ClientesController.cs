using DimDimApi.Data;
using DimDimApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DimDimApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        return await _context.Clientes.Include(c => c.Contas).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {
        var cliente = await _context.Clientes.Include(c => c.Contas)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null) return NotFound();
        return cliente;
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCliente(int id, Cliente cliente)
    {
        if (id != cliente.Id) return BadRequest();

        _context.Entry(cliente).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Clientes.AnyAsync(c => c.Id == id))
                return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return NotFound();

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
