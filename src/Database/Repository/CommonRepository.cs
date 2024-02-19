using Microsoft.EntityFrameworkCore;

namespace API_EF.Database.Repository
{
    public interface ICommonRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task SaveChangeAsync();
        void SaveChange();
        TEntity FindById(dynamic id);
        DbSet<TEntity> GetAll();
    }
    public class CommonRepository<TEntity> : ICommonRepository<TEntity> where TEntity : class
    {
        private readonly ILogger<CommonRepository<TEntity>> _logger;
        private readonly DBContext context;

        public CommonRepository(ILogger<CommonRepository<TEntity>> logger, DBContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public void Delete(TEntity entity)
        {
            try
            {
                context.Remove(entity);
            }
            catch (Exception)
            {
                _logger.LogError($"an error occurred while trying to delete a record");
                throw;
            }
        }

        public void Insert(TEntity entity)
        {
            try
            {
                context.Add(entity);
            }
            catch (Exception)
            {
                _logger.LogError($"an error occurred while trying to add a record");
                throw;
            }
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            try
            {
                context.AddRange(entities);
            }
            catch (Exception)
            {
                _logger.LogError($"an error occurred while trying to add a record");
                throw;
            }
        }

        public void SaveChange()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                _logger.LogError($"an error occurred while trying to persist in the database");
                throw;
            }
        }

        public async Task SaveChangeAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogError($"an error occurred while trying to persist in the database");
                throw;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                context.Attach(entity).State = EntityState.Modified;
                context.Update(entity);
            }
            catch (Exception)
            {
                _logger.LogError($"an error occurred while trying to update a record");
                throw;
            }
        }

        public TEntity FindById(dynamic id)
        {
            try
            {
                return context.Find<TEntity>(id);
            }
            catch (Exception)
            {
                _logger.LogError($"an error occurred while trying to find a record");
                throw;
            }
        }
        
        public DbSet<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }
    }
}
