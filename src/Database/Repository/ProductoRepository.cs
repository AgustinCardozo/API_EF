using Microsoft.EntityFrameworkCore;

namespace API_EF.Database.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DBContext dBContext;

        public ProductoRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<List<Producto>> Get()
        {
            return await dBContext.Productos.ToListAsync();
        }

        public async Task<Producto> Get(string codigo)
        {
            return await dBContext.Productos.FirstOrDefaultAsync(prod => prod.ProdCodigo == codigo);
        }
    }
}
