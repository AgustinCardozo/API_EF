using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_EF.Controllers
{
    [ApiController]
    [Route("API/productos")]
    public class ProductoController : ControllerBase
    {
        private readonly DBContext dBContext;

        public ProductoController(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProductos()
        {
            return Ok(await dBContext.Productos.ToListAsync());
        }

        [HttpGet]
        [Route("{codigo}")]
        public async Task<IActionResult> GetProducto(string codigo)
        {
            var producto = await dBContext.Productos.FirstOrDefaultAsync(prod => prod.ProdCodigo == codigo);
            if(producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }
    }
}
