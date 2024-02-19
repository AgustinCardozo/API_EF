using Microsoft.AspNetCore.Mvc;

namespace API_EF.Controllers
{
    [ApiController]
    [Route("API/productos")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository productoRepository;

        public ProductoController(IProductoRepository productoRepository)
        {
            this.productoRepository = productoRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await productoRepository.Get());
        }

        [HttpGet]
        [Route("{codigo}")]
        public async Task<IActionResult> Get(string codigo) 
        {
            var producto = await productoRepository.Get(codigo);
            if(producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }
    }
}
