using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomare.Models;
using Betacomare.Classes;

namespace Betacomare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public ProductsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/Products/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {

            if (_context.Products == null)
            {
                return NotFound();
            }
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                int code = System.Runtime.InteropServices.Marshal.GetExceptionCode();
                Log.ExceptionToDBLog(ex, _context);
                return NotFound();
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/GetProductColor/{color}
        [HttpGet("[action]/{color}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductColor(string color)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Color == color).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/GetProductColors
        [HttpGet("[action]")]
        // rita lista di tutti i possibili colori distintamente
        public async Task<ActionResult<IEnumerable<string?>>> GetProductColors()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Select(c => c.Color).Distinct().ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/GetProductPrice/{min}/{max}
        [HttpGet("[action]/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductPrice(int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.ListPrice >= min && c.ListPrice <= max).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/GetProductColPri/{color}/{min}/{max}
        [HttpGet("[action]/{color}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductColPri(string color, int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => (c.ListPrice >= min && c.ListPrice <= max) && c.Color == color).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/GetProductPrice/{min}/{max}
        [HttpGet("[action]/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductWeight(int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Weight >= min && c.Weight <= max).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("[action]/{min}/{max}/{min2}/{max2}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductWeiPri(int min, int max, int min2, int max2)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => (c.Weight >= min && c.Weight <= max) && (c.ListPrice >= min2 && c.ListPrice <= max2)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("[action]/{min}/{max}/{color}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductWeiCol(int min, int max, string color)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => (c.Weight >= min && c.Weight <= max) && (c.Color == color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("[action]/{min}/{max}/{min2}/{max2}/{color}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductWeiPriCol(int min, int max, int min2, int max2, string color)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => (c.Weight >= min && c.Weight <= max) && (c.ListPrice >= min2 && c.ListPrice <= max2) && (c.Color == color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductName(string name)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Name.Contains(name)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("[action]/{name}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductNamePr(string name, int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Name.Contains(name) && (c.ListPrice > min && c.ListPrice < max)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{name}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductNameWei(string name, int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Name.Contains(name) && (c.Weight > min && c.Weight < max)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("[action]/{name}/{color}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductNameColor(string name, string color)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Name.Contains(name) && (c.Color==color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{name}/{min}/{max}/{min2}/{max2}/{color}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductNameColPrWei(string name, int min, int max, int min2, int max2, string color)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Name.Contains(name)&&(c.Weight > min2 && c.Weight < max2) && (c.ListPrice > min && c.ListPrice < max) && (c.Color == color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{name}/{min}/{max}/{min2}/{max2}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductNamePrWei(string name, int min, int max, int min2, int max2)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Name.Contains(name) && (c.Weight > min2 && c.Weight < max2) && (c.ListPrice > min && c.ListPrice < max) ).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]")]
        // rita lista di tutti i possibili colori distintamente
        public async Task<ActionResult<IEnumerable<string?>>> GetProductsizes()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Select(c => c.Size).Distinct().ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSize(string size)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{color}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeColor(string size,string color)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.Color == color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;

        }
        [HttpGet("[action]/{size}/{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeName(string size, string name)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.Name.Contains(name))).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizePr(string size, int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.ListPrice >= min && c.ListPrice <= max)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeWei(string size, int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.Weight >= min && c.Weight <= max)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{color}/{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeColorNam(string size,string color,string name)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size &&( c.Name.Contains(name))&&(c.Color==color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{color}/{name}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeNameColPrSize(string size, string color, string name, int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.Name.Contains(name)) && (c.ListPrice >= min && c.ListPrice <= max) && (c.Color == color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{color}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeColorPr(string size,string color, int min,int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.ListPrice >= min && c.ListPrice <= max) && (c.Color == color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{color}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeColorWei(string size, string color, int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.Weight >= min && c.Weight <= max) && (c.Color == color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{name}/{min}/{max}/{min2}/{max2}/{size}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductNameSizeWeiPr(string name, int min, int max, int min2, int max2, string size)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Name == name && (c.Weight >= min2 && c.Weight <= max2) && (c.ListPrice >= min && c.ListPrice <= max) && (c.Size == size)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{min}/{max}/{min2}/{max2}/{color}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeColorWeiPr(string size, int min, int max,int min2,int max2, string color)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.Weight >= min2 && c.Weight <= max2) && (c.ListPrice >= min && c.ListPrice <= max) && (c.Color == color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{name}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeNamePr(string size, string name, int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.ListPrice >= min && c.ListPrice <= max) && (c.Name.Contains(name))).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{name}/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeNameWei(string size, string name, int min, int max)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.Weight >= min && c.Weight <= max) && (c.Name.Contains(name))).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{min}/{max}/{min2}/{max2}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizePrWei(string size, int min, int max, int min2, int max2)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Size == size && (c.Weight >= min2 && c.Weight <= max2) && (c.ListPrice >= min && c.ListPrice <= max)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet("[action]/{size}/{name}/{min}/{max}/{min2}/{max2}/{color}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSizeNameColPrWei(string size,string name, int min, int max, int min2, int max2, string color)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Where(c => c.Name.Contains(name)&&(c.Size==size) && (c.Weight >= min2 && c.Weight <= max2) && (c.ListPrice >= min && c.ListPrice <= max) && (c.Color == color)).ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AdventureWorksLt2019Context.Products'  is null.");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
