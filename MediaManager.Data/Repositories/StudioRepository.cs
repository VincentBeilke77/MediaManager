using MediaManager.Data.Repositories.Interfaces;
using MediaManager.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MediaManager.Data.Repositories
{
    /// <summary>
    /// StudioRepository is a class used to access the data from a database context.
    /// </summary>
    public class StudioRepository : BaseRepository, IStudioRepository
    {
        /// <summary>
        /// A constructor for passing a context and logger to the BaseRepository class.
        /// </summary>
        /// <param name="context"><code>MediaManagerContext</code> for access to the database.</param>
        /// <param name="logger"><code>ILogger</code> for logging information and errors.</param>
        public StudioRepository(MediaManagerContext context, ILogger logger) : base(context, logger)
        {}

        /// <summary>
        /// Returns all the studios in the database asynchronously.
        /// </summary>
        /// <returns>A <code>ICollection</code> of <code>Studio</code>s.</returns>
        public async Task<ICollection<Studio>> GetAllStudiosAsync()
        {
            _logger.LogInformation($"Getting all Studios");
            IQueryable<Studio> query = _context.Studios
                .Include(studio => studio.Movies)
                .ThenInclude(studioMovie => studioMovie.Movie);

            query = query.OrderBy(studio => studio.Name);

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// Returns the studio associated with the id passed in asynchronously.
        /// </summary>
        /// <param name="id">An <code>int</code> to hold the id.</param>
        /// <returns>A <code>Studio</code></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Studio> GetStudioAsync(int studioId)
        {
            IQueryable<Studio> query = _context.Studios
                .Include(studio => studio.Movies)
                .ThenInclude(studioMovie => studioMovie.Movie);

            query = query.Where(studio => studio.Id == studioId);

            Studio? studio = await query.FirstOrDefaultAsync();

            if (studio == null) throw new NullReferenceException("No studio found with that id.");
            return studio;
        }

        /// <summary>
        /// Returns all the studios related to a movie in the database asynchronously.
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>A <code>ICollection</code> of <code>Studio</code>s.</returns>
        public async Task<ICollection<Studio>> GetStudiosByMovieIdAsync(int movieId)
        {
            _logger.LogInformation($"Getting Studios for {movieId}");

            IQueryable<Studio> query = _context.Studios
                .Include(sm => sm.Movies)
                .ThenInclude(m => m.Movie);

            query = query.Where(s => s.Movies.Any(sm => sm.MovieId == movieId))
                .OrderBy(s => s.Name);

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// Generates and returns a new id from the database.
        /// </summary>
        /// <returns>An <code>int</code> containing the new id.</returns>
        public async Task<int> GenerateStudioId()
        {
            _logger.LogInformation("Generating identity for a studio.");

            var id = 0;

            await using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "GetIdentitySeedForTable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter(
                "TableName", "Studio"));

            await _context.Database.OpenConnectionAsync();

            var dataReader = cmd.ExecuteReaderAsync();

            if (await dataReader.Result.ReadAsync())
            {
                id = dataReader.GetAwaiter().GetResult().GetInt32("Id");
            }

            return id;
        }
    }
}
