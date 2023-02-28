using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using MediaManager.Data.Repositories.Interfaces;
using MediaManager.Domain.Entities;

namespace MediaManager.Data.Repositories
{
    /// <summary>
    /// ActorRepository is a class used to access the data from a database context.
    /// </summary>
    public class ActorRepository : BaseRepository, IActorRepository
    {
        /// <summary>
        /// A constructor for passing a context and logger to the BaseRepository class.
        /// </summary>
        /// <param name="context"><code>MediaManagerContext</code> for access to the database.</param>
        /// <param name="logger"><code>ILogger</code> for logging information and errors.</param>
        public ActorRepository(MediaManagerContext context, ILogger<BaseRepository> logger) : base(context, logger)
        {}

        /// <summary>
        /// Returns all the actors in the database asynchronously.
        /// </summary>
        /// <returns>A <code>ICollection</code> of <code>Actor</code>s.</returns>
        public async Task<ICollection<Actor>> GetAllActorsAsync()
        {
            _logger.LogInformation($"Getting all actors info");

            IQueryable<Actor> query = _context.Actors
                .Include(actor => actor.Movies)
                .ThenInclude(actorMovie => actorMovie.Movie);

            query = query.OrderBy(actor => actor.FullName);

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// Returns the actor associated with the id passed in asynchronously.
        /// </summary>
        /// <param name="id">An <code>int</code> to hold the id.</param>
        /// <returns>A <code>Actor</code></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Actor> GetActorAsync(int actorId)
        {
            _logger.LogInformation($"Getting actor info for {actorId}");

            IQueryable<Actor> query = _context.Actors
                .Include(actor => actor.Movies)
                .ThenInclude(actorMovie => actorMovie.Movie);

            query = query.Where(actor => actor.Id == actorId);

            Actor? actor = await query.FirstOrDefaultAsync();

            if (actor == null) throw new NullReferenceException("No actor found with that Id");
            return actor;
        }

        /// <summary>
        /// Returns the actor associated with the first and last name passed in asynchronously.
        /// </summary>
        /// <param name="lastName">A <code>String</code> to hold the first name.</param>
        /// <param name="firstName">A <code>String</code> to hold the last name.</param>
        /// <returns>A <code>Actor</code></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Actor> GetActorByNameAsync(string lastName, string firstName)
        {
            _logger.LogInformation($"Getting actor info for {lastName}, {firstName}");

            IQueryable<Actor> query = _context.Actors
                .Include(actor => actor.Movies)
                .ThenInclude(actorMovie => actorMovie.Movie);

            query = query
                .Where(actor => actor.LastName == lastName && actor.FirstName == firstName);

            Actor? actor = await query.FirstOrDefaultAsync();

            if (actor == null) throw new NullReferenceException("No actor found with that name");
            return actor;
        }

        /// <summary>
        /// Generates and returns a new id from the database.
        /// </summary>
        /// <returns>An <code>int</code> containing the new id.</returns>
        public int GenerateActorId()
        {
            _logger.LogInformation("Generating identity for a actor.");

            var id = 0;

            using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "GetIdentitySeedForTable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter(
                "TableName", "Actor"));

            _context.Database.OpenConnection();

            var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                id = dr.GetInt32("Id");
            }

            return id;
        }

        /// <summary>
        /// Returns all the actors that are associated with the movie id.
        /// </summary>
        /// <param name="movieId">An <code>int</code> to hold the id.</param>
        /// <returns>A <code>Collection</code> of <code>Actor</code>s.</returns>
        public async Task<ICollection<Actor>> GetActorsByMovieIdAsync(int movieId)
        {
            _logger.LogInformation($"Getting director info for {movieId}");

            IQueryable<Actor> query = _context.Actors
                .Include(actor => actor.Movies)
                .ThenInclude(actorMovies => actorMovies.Movie);

            query = query.Where(actor => actor.Movies
                .Any(actorMovie => actorMovie.MovieId == movieId));

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// Returns a collection of actors with the search value in them.
        /// </summary>
        /// <param name="value">A <code>string</code> containing the search value.</param>
        /// <returns>A <code>Collection</code> of <code>Actor</code>s.</returns>
        public async Task<ICollection<Actor>> GetActorsByNameSearchValue(string value)
        {
            _logger.LogInformation($"Getting actors with ${value} in their name.");

            IQueryable<Actor> query = _context.Actors
                .Include(actor => actor.Movies)
                .ThenInclude(actorMovies => actorMovies.Movie);

            query = query.Where(actor => actor.FirstName.Contains(value) || actor.LastName.Contains(value));

            return await query.ToArrayAsync();
        }
    }
}