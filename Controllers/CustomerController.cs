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

        [HttpGet("{Customerid}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int Customerid)
        {
            var Customer = await dbContext.Customers.FindAsync(Customerid);
            if (Customer != null)
            {
                return NotFound();
            }
            return Ok(Customer);
        }

        [HttpPost]
        public async    Task<ActionResult<Customer>> addCustomer(Customer Customer)
        {
            await dbContext.Customers.AddAsync(Customer);
            await dbContext.SaveChangesAsync();
            return Ok(Customer);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer(int Customerid, Customer Customer)
        {
            if(Customerid!=Customer.CustomerId)
            {
                return BadRequest();
                    

             }
            dbContext.Entry(Customer).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return Ok(Customer);
        }

        [HttpDelete("{Customerid}")]
        public async Task<ActionResult<Product>> deleteCustomer(int Customerid)
        {
            var Customer = await dbContext.Products.FindAsync(Customerid);
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
