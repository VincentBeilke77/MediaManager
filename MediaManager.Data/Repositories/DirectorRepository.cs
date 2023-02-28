using MediaManager.Data.Repositories.Interfaces;
using MediaManager.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IO;

namespace MediaManager.Data.Repositories
{
    /// <summary>
    /// DirectorRepository is a class used to access the data from a database context.
    /// </summary>
    public class DirectorRepository : BaseRepository, IDirectorRepository
    {
        /// <summary>
        /// A constructor for passing a context and logger to the BaseRepository class.
        /// </summary>
        /// <param name="context"><code>MediaManagerContext</code> for access to the database.</param>
        /// <param name="logger"><code>ILogger</code> for logging information and errors.</param>
        public DirectorRepository(MediaManagerContext context, ILogger logger) : base(context, logger)
        { }

        /// <summary>
        /// Returns all the directors in the database asynchronously.
        /// </summary>
        /// <returns>A <code>ICollection</code> of <code>Director</code>s.</returns>
        public async Task<ICollection<Director>> GetAllDirectorsAsync()
        {
            _logger.LogInformation($"Getting all directors.");

            IQueryable<Director> query = _context.Directors
                .Include(director => director.Movies)
                .ThenInclude(directorMovies => directorMovies.Movie);

            query = query.OrderBy(d => d.FullName);

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// Returns the director associated with the id passed in asynchronously.
        /// </summary>
        /// <param name="id">An <code>int</code> to hold the id.</param>
        /// <returns>A <code>Director</code></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Director> GetDirectorAsync(int directorId)
        {
            _logger.LogInformation($"Getting director info for {directorId}");

            IQueryable<Director> query = _context.Directors
                .Include(director => director.Movies)
                .ThenInclude(directorMovies => directorMovies.Movie);

            query = query.Where(d => d.Id == directorId);
            
            Director? director = await query.FirstOrDefaultAsync();

            if (director == null) throw new NullReferenceException("No actor found with that Id");
            return director;
        }

        /// <summary>
        /// Returns the director associated with the first and last name passed in asynchronously.
        /// </summary>
        /// <param name="lastName">A <code>String</code> to hold the first name.</param>
        /// <param name="firstName">A <code>String</code> to hold the last name.</param>
        /// <returns>A <code>Director</code></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Director> GetDirectorByNameAsync(string lastName, string firstName)
        {
            _logger.LogInformation($"Getting director info for {lastName},{firstName}");

            IQueryable<Director> query = _context.Directors
                .Include(director => director.Movies)
                .ThenInclude(directorMovies => directorMovies.Movie);

            query = query.Where(d => d.LastName == lastName && d.FirstName == firstName);

            Director? director = await query.FirstOrDefaultAsync();

            if (director == null) throw new NullReferenceException("No director found with that name");
            return director;
        }

        /// <summary>
        /// Returns all the directors that are associated with the movie id.
        /// </summary>
        /// <returns>A <code>Collection</code> of <code>Directors</code>s.</returns>
        public async Task<ICollection<Director>> GetDirectorsByMovieIdAsync(int movieId)
        {
            _logger.LogInformation($"Getting director info for {movieId}");

            IQueryable<Director> query = _context.Directors
                .Include(director => director.Movies)
                .ThenInclude(directorMovies => directorMovies.Movie);

            query = query.Where(dm => dm.Movies.Any(m => m.MovieId == movieId));

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// Generates and returns a new id from the database.
        /// </summary>
        /// <returns>An <code>int</code> containing the new id.</returns>
        public async Task<int> GenerateDirectorId()
        {
            _logger.LogInformation("Generating identity for a movie.");

            var id = 0;

            await using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "GetIdentitySeedForTable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter(
                "TableName", "Director"));

            await _context.Database.OpenConnectionAsync();

            var dr = cmd.ExecuteReaderAsync();

            if (await dr.Result.ReadAsync())
            {
                id = dr.GetAwaiter().GetResult().GetInt32("Id");
            }

            return id;
        }
    }
}