using MediaManager.Data.Repositories.Interfaces;
using MediaManager.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MediaManager.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public MovieRepository(MediaManagerContext context, ILogger logger) : base(context, logger)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Movie>> GetAllAsync()
        {
            _logger.LogInformation($"Getting all Movies");

            IQueryable<Movie> query = _context.Movies
                .Include(m => m.Rating)
                .Include(gm => gm.Genres)
                .ThenInclude(g => g.Genre)
                .Include(am => am.Actors)
                .ThenInclude(a => a.Actor)
                .Include(dm => dm.Directors)
                .ThenInclude(d => d.Director)
                .Include(mtm => mtm.MediaTypes)
                .ThenInclude(mt => mt.MediaType)
                .Include(sm => sm.Studios)
                .ThenInclude(s => s.Studio);

            query = query.OrderBy(m => m.Title);

            return await query.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Movie> GetAsync(int id)
        {
            _logger.LogInformation($"Getting movie info for {id}");

            IQueryable<Movie> query = _context.Movies
                .Include(m => m.Rating)
                .Include(gm => gm.Genres)
                .ThenInclude(g => g.Genre)
                .Include(am => am.Actors)
                .ThenInclude(a => a.Actor)
                .Include(dm => dm.Directors)
                .ThenInclude(d => d.Director)
                .Include(mtm => mtm.MediaTypes)
                .ThenInclude(mt => mt.MediaType)
                .Include(sm => sm.Studios)
                .ThenInclude(s => s.Studio);

            query = query.Where(m => m.Id == id);

            Movie? movie = await query.FirstOrDefaultAsync();

            if (movie == null) throw new NullReferenceException("No movie found with that Id");
            return movie;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Movie>> GetFavoriteMovies()
        {
            _logger.LogInformation($"Getting movies that are marked as favorite.");

            IQueryable<Movie> query = _context.Movies
                .Include(m => m.Rating)
                .Include(gm => gm.Genres)
                .ThenInclude(g => g.Genre)
                .Include(am => am.Actors)
                .ThenInclude(a => a.Actor)
                .Include(dm => dm.Directors)
                .ThenInclude(d => d.Director)
                .Include(mtm => mtm.MediaTypes)
                .ThenInclude(mt => mt.MediaType)
                .Include(sm => sm.Studios)
                .ThenInclude(s => s.Studio);

            query = query.Where(m => m.Favorite);

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<ICollection<Movie>> SearchMoviesByTitle(string title)
        {
            _logger.LogInformation($"Getting movies with {title} in them");

            IQueryable<Movie> query = _context.Movies
                .Include(m => m.Rating)
                .Include(gm => gm.Genres)
                .ThenInclude(g => g.Genre)
                .Include(am => am.Actors)
                .ThenInclude(a => a.Actor)
                .Include(dm => dm.Directors)
                .ThenInclude(d => d.Director)
                .Include(mtm => mtm.MediaTypes)
                .ThenInclude(mt => mt.MediaType)
                .Include(sm => sm.Studios)
                .ThenInclude(s => s.Studio);

            query = query.Where(m => m.Title.Contains(title));

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool CheckForExistingMovie(string title)
        {
            var exists = _context.Movies.Any(m => m.Title == title);

            return exists;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckForExistingMovie(int id)
        {
            var exists = _context.Movies.Any(m => m.Id == id);

            return exists;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> GenerateMovieId()
        {
            _logger.LogInformation("Generating identity for a movie.");

            var id = 0;

            await using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "GetIdentitySeedForTable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter(
                "TableName", "Movie"));

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
