using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;
using System.Net;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var addresses = await _context.Addresses.Include(a => a.User).ToListAsync();
           
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var address = await _context.Addresses.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
            if (address == null)
            {
                return NotFound();
            }
            if (address == null)
                return NotFound();

            // Avoid accessing User if it is null
            if (address.User == null)
                address.User = new User { Name = "No User Linked" };
            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
        }
    }
}
