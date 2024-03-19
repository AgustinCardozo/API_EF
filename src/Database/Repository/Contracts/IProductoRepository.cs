namespace API_EF.Database.Repository.Contracts
{
    public interface IProductoRepository
    {
        List<Producto> Get();
        Task<Producto> Get(string codigo);
    }
}
