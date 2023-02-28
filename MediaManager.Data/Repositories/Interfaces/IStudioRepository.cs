using MediaManager.Domain.Entities;

namespace MediaManager.Data.Repositories.Interfaces
{
    public interface IStudioRepository : IBaseRepository
    {
        Task<ICollection<Studio>> GetAllStudiosAsync();

        Task<Studio> GetStudioAsync(int studioId);

        Task<ICollection<Studio>> GetStudiosByMovieIdAsync(int movieId);

        Task<int> GenerateStudioId();
    }
}
