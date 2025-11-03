using BugStore.Infrastructure.Context;
using BugStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(BugStoreContext _context) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
        => Ok(new {
            Customers =
                _context.Orders.ToList()
        });
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
        => Ok(new {
            Customer =
                await _context.Orders.FindAsync(id)
        });
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Order customer)
    {
        await _context.Orders.AddAsync(customer);
        await _context.SaveChangesAsync();
        return Ok(new { customer });
    }
}