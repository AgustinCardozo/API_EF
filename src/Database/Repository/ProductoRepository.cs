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

        public List<Producto> Get()
        {
            return dBContext.Productos.ToList();
        }

        public async Task<Producto> Get(string codigo)
        {
            return await dBContext.Productos.FirstOrDefaultAsync(prod => prod.ProdCodigo == codigo);
        }
    }
}
