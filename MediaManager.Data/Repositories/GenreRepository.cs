using MediaManager.Data.Repositories.Interfaces;
using MediaManager.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MediaManager.Data.Repositories
{
    /// <summary>
    /// GenreRepository is a class used to access the data from a database context.
    /// </summary>
    public class GenreRepository : BaseRepository, IGenreRepository
    {
        /// <summary>
        /// A constructor for passing a context and logger to the BaseRepository class.
        /// </summary>
        /// <param name="context"><code>MediaManagerContext</code> for access to the database.</param>
        /// <param name="logger"><code>ILogger</code> for logging information and errors.</param>
        public GenreRepository(MediaManagerContext context, ILogger logger) : base(context, logger)
        { }

        /// <summary>
        /// Returns all the actors in the database asynchronously.
        /// </summary>
        /// <returns>A <code>ICollection</code> of <code>Actor</code>s.</returns>
        public async Task<ICollection<Genre>> GetAllGenresAsync()
        {
            _logger.LogInformation($"Getting all Genres");

            IQueryable<Genre> query = _context.Genres;

            query = query.OrderBy(g => g.Name);

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// Returns the genre associated with the id passed in asynchronously.
        /// </summary>
        /// <param name="id">An <code>int</code> to hold the id.</param>
        /// <returns>A <code>Genre</code></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Genre> GetGenreAsync(int id)
        {
            _logger.LogInformation($"Getting Genre for id: {id}");

            IQueryable<Genre> query = _context.Genres
                .Include(gm => gm.Movies)
                .ThenInclude(m => m.Movie);

            query = query
                .Where(g => g.Id == id);

            Genre? genre = await query.FirstOrDefaultAsync();

            if (genre == null) throw new NullReferenceException("There is no genre with this id.");
            return genre;
        }

        /// <summary>
        /// Checks to see if the given genre name exist in the name.
        /// </summary>
        /// <param name="name">A <code>string</code> to contain the search name.</param>
        /// <returns>A <code>bool</code> containing if the name exists.</returns>
        public bool CheckForExistingGenreName(string name)
        {
            var exists = _context.Genres.Any(g => g.Name.ToLower() == name.ToLower());

            return exists;
        }

        /// <summary>
        /// Generates and returns a new id from the database.
        /// </summary>
        /// <returns>An <code>int</code> containing the new id.</returns>
        public async Task<int> GenerateGenreId()
        {
            _logger.LogInformation("Generating identity for a genre.");

            var id = 0;

            await using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "GetIdentitySeedForTable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter(
                "TableName", "Genre"));

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
