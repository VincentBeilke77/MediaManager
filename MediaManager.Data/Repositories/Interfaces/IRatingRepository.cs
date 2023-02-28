using MediaManager.Domain.Entities;

namespace MediaManager.Data.Repositories.Interfaces
{
    public interface IRatingRepository : IBaseRepository
    {
        Task<ICollection<Rating>> GetAllRatingsAsyc();

        Task<Rating> GetRatingByIdAsync(int ratingId);

        Task<Rating> GetRatingByNameAsync(string name);
    }
}
