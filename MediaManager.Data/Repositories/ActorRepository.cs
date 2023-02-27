using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using MediaManager.Data.Repositories;
using MediaManager.Domain.Entities;
using MediaManager.Data;

namespace MediaCatalog.API.Data.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public class ActorRepository : BaseRepository, IActorRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public ActorRepository(MediaManagerContext context, ILogger<BaseRepository> logger) : base(context, logger)
        {}

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Actor>> GetAllActorsAsync()
        {
            _logger.LogInformation($"Getting all actors info");

            IQueryable<Actor> query = _context.Actors
                .Include(am => am.Movies)
                .ThenInclude(m => m.Movie);

            query = query.OrderBy(a => a.FullName);

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actorId"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Actor> GetActorAsync(int actorId)
        {
            _logger.LogInformation($"Getting actor info for {actorId}");

            IQueryable<Actor> query = _context.Actors
                .Include(am => am.Movies)
                .ThenInclude(m => m.Movie);

            query = query.Where(a => a.Id == actorId);

            Actor? actor = await query.FirstOrDefaultAsync();

            if (actor == null) throw new NullReferenceException("No actor found with that Id");
            return actor;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public async Task<Actor> GetActorByNameAsync(string lastName, string firstName)
        {
            _logger.LogInformation($"Getting actor info for {lastName}, {firstName}");

            IQueryable<Actor> query = _context.Actors
                .Include(am => am.Movies)
                .ThenInclude(m => m.Movie);

            query = query
                .Where(a => a.LastName == lastName && a.FirstName == firstName);

            Actor? actor = await query.FirstOrDefaultAsync();

            if (actor == null) throw new NullReferenceException("No actor found with that name");
            return actor;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<ICollection<Actor>> GetActorsByMovieIdAsync(int movieId)
        {
            _logger.LogInformation($"Getting actor info for {movieId}");

            IQueryable<Actor> query = _context.Actors
                .Include(am => am.Movies)
                .ThenInclude(m => m.Movie);

            query = query.Where(a => a.Movies.Any(am => am.MovieId == movieId));

            return await query.ToArrayAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<ICollection<Actor>> GetActorsByNameSearchValue(string value)
        {
            _logger.LogInformation($"Getting actors with ${value} in their name.");

            IQueryable<Actor> query = _context.Actors
                .Include(am => am.Movies)
                .ThenInclude(m => m.Movie);

            query = query.Where(a => a.FirstName.Contains(value) || a.LastName.Contains(value));

            return await query.ToArrayAsync();
        }
    }
}