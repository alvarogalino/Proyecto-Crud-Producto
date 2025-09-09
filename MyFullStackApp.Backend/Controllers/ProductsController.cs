using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFullStackApp.Backend.Data;
using MyFullStackApp.Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFullStackApp.Backend.Controllers
{
    [Route("api/[controller]")] // Define la ruta base para este controlador (e.g., /api/products)
    [ApiController] // Indica que esta clase es un controlador API
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        // Obtiene todos los productos de la base de datos.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        // Obtiene un producto específico por su ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(); // Retorna 404 si el producto no se encuentra
            }

            return product;
        }

        // POST: api/Products
        // Crea un nuevo producto.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Retorna 201 Created y la URL del nuevo recurso.
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT: api/Products/5
        // Actualiza un producto existente.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(); // Retorna 400 si el ID en la ruta no coincide con el ID del producto
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
                    return NotFound(); // Retorna 404 si el producto no existe
                }
                else
                {
                    throw; // Lanza la excepción para otros errores de concurrencia
                }
            }

            return NoContent(); // Retorna 204 No Content si la actualización fue exitosa
        }

        // DELETE: api/Products/5
        // Elimina un producto por su ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); // Retorna 404 si el producto no se encuentra
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content si la eliminación fue exitosa
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
