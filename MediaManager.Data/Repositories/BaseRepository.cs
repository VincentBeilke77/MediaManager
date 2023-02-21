using MediaManager.Data.Repositories.Interfaces;

namespace MediaManager.Data.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly MediaManagerContext _context;
        protected readonly ILogger _logger;

        public BaseRepository(MediaManagerContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating an object of type {entity.GetType()} to the context.");
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
