using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoezone.Data;
using Shoezone.Model;

namespace Shoezone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomer()
        {
            var allCustomer=await dbContext.Customers.ToListAsync();
                return Ok(allCustomer);

        }
        
        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int customerId)
        {
            var Customer = await dbContext.Customers.FindAsync(customerId);
            if (Customer == null)
            {
                return NotFound();
            }
           
            return Ok(Customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();
            return Ok(customer);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer(int customerId, Customer customer)
        {
            if(customerId!=customer.CustomerId)
            {
                return BadRequest();
                    

             }
            dbContext.Entry(customer).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int customerId)
        {
            var customer = await dbContext.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            dbContext.Customers.Remove(customer);
            await dbContext.SaveChangesAsync();
            return Ok(customer);
        }



    }
}
