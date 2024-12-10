using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderItemController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetOrderItems()
        {
            var orderItems = await _context.OrderItem.ToListAsync();
            return Ok(orderItems);
        }

        [HttpGet("{orderId}/{productId}")]
        public async Task<IActionResult> GetOrderItem(int orderId, int productId)
        {
            var orderItem = await _context.OrderItem.FindAsync(orderId, productId);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingItem = await _context.OrderItem.FirstOrDefaultAsync(oi => oi.OrderId == orderItem.OrderId && oi.ProductId == orderItem.ProductId);

            if (existingItem != null)
            {
                return BadRequest("OrderItem with the same OrderId and ProductId already exists.");
            }

            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { orderId = orderItem.OrderId, productId = orderItem.ProductId }, orderItem);
        }
    }
}
