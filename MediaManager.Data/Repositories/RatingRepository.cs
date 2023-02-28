using MediaManager.Data.Repositories.Interfaces;
using MediaManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Data.Repositories
{
    /// <summary>
    /// RatingRepository is a class used to access the data from a database context.
    /// </summary>
    public class RatingRepository : BaseRepository, IRatingRepository
    {
        /// <summary>
        /// A constructor for passing a context and logger to the BaseRepository class.
        /// </summary>
        /// <param name="context"><code>MediaManagerContext</code> for access to the database.</param>
        /// <param name="logger"><code>ILogger</code> for logging information and errors.</param>
        public RatingRepository(MediaManagerContext context, ILogger logger) : base(context, logger)
        {}

        /// <summary>
        /// Returns all the actors in the database asynchronously.
        /// </summary>
        /// <returns>A <code>ICollection</code> of <code>Actor</code>s.</returns>
        public async Task<ICollection<Rating>> GetAllRatingsAsyc()
        {
            _logger.LogInformation($"Getting all ratings");

            var query = _context.Ratings;

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// Returns the rating associated with the id passed in asynchronously.
        /// </summary>
        /// <param name="id">An <code>int</code> to hold the id.</param>
        /// <returns>A <code>Rating</code></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Rating> GetRatingByIdAsync(int ratingId)
        {
            _logger.LogInformation($"Getting rating by id: {ratingId}");

            var query = _context.Ratings
                .Where(r => r.Id == ratingId);

            Rating? rating = await query.FirstOrDefaultAsync();

            if (rating == null) throw new NullReferenceException("There is no rating with that id.");
            return rating;
        }

        /// <summary>
        /// Returns the rating associated with the name passed in asynchronously.
        /// </summary>
        /// <param name="name">An <code>string</code> to hold the id.</param>
        /// <returns>A <code>Rating</code></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Rating> GetRatingByNameAsync(string name)
        {
            _logger.LogInformation($"Getting rating {name}");

            var query = _context.Ratings
                .Where(r => r.Name == name);

            Rating? rating = await query.FirstOrDefaultAsync();

            if (rating == null) throw new NullReferenceException("There is no rating with that id.");
            return rating;
        }
    }
}
