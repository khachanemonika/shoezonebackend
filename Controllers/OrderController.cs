using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoezone.Data;
using Shoezone.Model;

namespace Shoezone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrder()
        {
            var allOrder = await dbContext.Orders.ToListAsync();
            return Ok(allOrder);

        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order != null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(Order order)
        {
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return Ok(order);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder(int orderId, Order order)
        {
            if (orderId != order.OrderId)
            {
                return BadRequest();


            }
            dbContext.Entry(order).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return Ok(order);
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult<Order>> DeleteOrder(int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
            return Ok(order);
        }



    }
}
