namespace API_EF.Database.Repository.Contracts
{
    public interface IProductoRepository
    {
        Task<List<Producto>> Get();
        Task<Producto> Get(string codigo);
    }
}
