using DimDimApi.Data;
using DimDimApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DimDimApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ContasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Conta>>> GetContas()
    {
        return await _context.Contas.Include(c => c.Cliente).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Conta>> GetConta(int id)
    {
        var conta = await _context.Contas.Include(c => c.Cliente)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (conta == null) return NotFound();
        return conta;
    }

    [HttpPost]
    public async Task<ActionResult<Conta>> PostConta(Conta conta)
    {
        if (!await _context.Clientes.AnyAsync(c => c.Id == conta.ClienteId))
            return BadRequest("Cliente não encontrado");

        _context.Contas.Add(conta);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetConta), new { id = conta.Id }, conta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutConta(int id, Conta conta)
    {
        if (id != conta.Id) return BadRequest();

        _context.Entry(conta).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Contas.AnyAsync(c => c.Id == id))
                return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConta(int id)
    {
        var conta = await _context.Contas.FindAsync(id);
        if (conta == null) return NotFound();

        _context.Contas.Remove(conta);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
