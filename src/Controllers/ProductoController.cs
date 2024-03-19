using Microsoft.AspNetCore.Mvc;
using QuestPDF.Infrastructure;

namespace API_EF.Controllers
{
    [ApiController]
    [Route("API/productos")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository productoRepository;
        private readonly IProductoService service;

        public ProductoController(IProductoRepository productoRepository, IProductoService service)
        {
            this.productoRepository = productoRepository;
            this.service = service;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return Ok(productoRepository.Get());
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

        [HttpGet]
        [Route("download")]
        public IActionResult Download()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var document = service.GeneratePDF();
            var result = new FileContentResult(document, "application/octet-stream");
            result.FileDownloadName = "example.pdf";
            return result;
        }
    }
}
