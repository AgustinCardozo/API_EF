using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_EF.Controllers
{
    [ApiController]
    [Route("API/empleados")]
    public class EmpleadoController : ControllerBase
    {
        private readonly DBContext dBContext;

        public EmpleadoController(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetEmpleados()
        {
            return Ok(await dBContext.Empleados.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetEmpleado(int id)
        {
            var empleado = await dBContext.Empleados.FirstOrDefaultAsync(empleado => empleado.EmplCodigo == id);
            if(empleado is null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }
    }
}
