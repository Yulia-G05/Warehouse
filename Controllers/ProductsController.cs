using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;


namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController<Product, APIdbContext>
    {

        public ProductsController(APIdbContext context) : base(context) { }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound();
            }

            var productInfoList = new List<object>();

            foreach (var product in products)
            {
                var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentID == product.DepartmentID);

                if (department != null)
                {
                    var productInfo = new
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        DepartmentName = department.DepartmenName
                    };

                    productInfoList.Add(productInfo);
                }
            }

            return Ok(productInfoList);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProduct(int id)
        {
            var product = await GetEntityById<Product>(id);

            if (product == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentID == product.DepartmentID);
            if (department == null)
            {
                return NotFound();
            }

            var productInfo = new
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                DepartmentName = department.DepartmenName
            };

            return Ok(productInfo);
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(Product productModel)
        {
            return await UpdateEntity(productModel);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            return await CreateEntity(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return await DeleteEntity<Product>(id);
        }
    }
}
