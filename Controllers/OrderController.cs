using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoezone.Data;
using Shoezone.Model;

namespace Shoezone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrder()
        {
            var allOrder = await dbContext.Orders.ToListAsync();
            return Ok(allOrder);

        }

        [HttpGet("{Orderid}")]
        public async Task<ActionResult<Order>> GetOrderById(int Orderid)
        {
            var Order = await dbContext.Orders.FindAsync(Orderid);
            if (Order != null)
            {
                return NotFound();
            }
            return Ok(Order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> addOrder(Order Order)
        {
            await dbContext.Orders.AddAsync(Order);
            await dbContext.SaveChangesAsync();
            return Ok(Order);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder(int Orderid, Order Order)
        {
            if (Orderid != Order.OrderId)
            {
                return BadRequest();


            }
            dbContext.Entry(Order).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return Ok(Order);
        }

        [HttpDelete("{Orderid}")]
        public async Task<ActionResult<Product>> deleteOrder(int Orderid)
        {
            var Order = await dbContext.Products.FindAsync(Orderid);
            if (Order == null)
            {
                return NotFound();
            }
            dbContext.Products.Remove(Order);
            await dbContext.SaveChangesAsync();
            return Ok(Order);
        }



    }
}
