using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoezone.Data;
using Shoezone.Model;

namespace Shoezone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public CustomerController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomer()
        {
            var allCustomer=await dbContext.Customers.ToListAsync();
                return Ok(allCustomer);

        }

        [HttpGet("{CustomerId}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int CustomerId)
        {
            var Customer = await dbContext.Customers.FindAsync(CustomerId);
            if (Customer != null)
            {
                return NotFound();
            }
            return Ok(Customer);
        }

        [HttpPost]
        public async    Task<ActionResult<Customer>> AddCustomer(Customer Customer)
        {
            await dbContext.Customers.AddAsync(Customer);
            await dbContext.SaveChangesAsync();
            return Ok(Customer);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer(int CustomerId, Customer Customer)
        {
            if(CustomerId!=Customer.CustomerId)
            {
                return BadRequest();
                    

             }
            dbContext.Entry(Customer).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return Ok(Customer);
        }

        [HttpDelete("{CustomerId}")]
        public async Task<ActionResult<Product>> DeleteCustomer(int CustomerId)
        {
            var Customer = await dbContext.Products.FindAsync(CustomerId);
            if (Customer == null)
            {
                return NotFound();
            }
            dbContext.Products.Remove(Customer);
            await dbContext.SaveChangesAsync();
            return Ok(Customer);
        }



    }
}
