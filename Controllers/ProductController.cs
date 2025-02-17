using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoezone.Data;
using Shoezone.Model;

namespace Shoezone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment env;
        public ProductController(ApplicationDbContext dbContext,IWebHostEnvironment env)
        {
            this.dbContext = dbContext;
            this.env = env;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProduct()
        {
            var allProducts = await dbContext.Products.ToListAsync();
            return Ok(allProducts);

        }
        [HttpGet("{productid}")]
        public async Task<ActionResult<List<Product>>> GetProductById(int productid)
        {
            var product = await dbContext.Products.FindAsync(productid);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product addProduct)
        {
          
            await dbContext.Products.AddAsync(addProduct);
            await dbContext.SaveChangesAsync();
            return Ok(addProduct);

        }

        [HttpPut("{productid}")]
        public async Task<ActionResult<Product>> updateStudent(int productid, Product product)
        {
            if (productid != product.ProductID)
            {
                return BadRequest();
            }
            dbContext.Entry(product).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{productid}")]
        public async Task<ActionResult<Product>> deleteProduct(int productid)
        {
            var product = await dbContext.Products.FindAsync(productid);
            if (product == null)
            {
                return NotFound();
            }
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return Ok(product);
        }
        [Route("SaveFile")]
        [HttpPost]
        public async Task<ActionResult> saveFile(IFormFile file)
        {
            if(file== null)
            {
                return BadRequest("No file Upload");
            }
            var httpRequest=Request.Form;
            var postedFile = httpRequest.Files[0];
            string fileName = postedFile.FileName;
            var physicalPath = env.ContentRootPath + "/Photos/" + fileName;

            using(var stream=new FileStream(physicalPath,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            
              return Ok(fileName);
            
        
        }
        
    }
}
