using BugStore.Infrastructure.Context;
using BugStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(BugStoreContext _context) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
        => Ok(new {
            Customers =
        _context.Customers.ToList()
    });
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
        => Ok(new {
            Customer =
        await _context.Customers.FindAsync(id)
    });
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return Ok(new { customer });
    }
}