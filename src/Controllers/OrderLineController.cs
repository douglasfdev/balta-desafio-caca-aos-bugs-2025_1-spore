using BugStore.Infrastructure.Context;
using BugStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderLineController(BugStoreContext _context) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
        => Ok(new {
            Customers =
                _context.OrderLines.ToList()
        });
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
        => Ok(new {
            Customer =
                await _context.OrderLines.FindAsync(id)
        });
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderLine customer)
    {
        await _context.OrderLines.AddAsync(customer);
        await _context.SaveChangesAsync();
        return Ok(new { customer });
    }
}