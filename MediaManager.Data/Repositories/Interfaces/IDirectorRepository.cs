using MediaManager.Domain.Entities;

namespace MediaManager.Data.Repositories.Interfaces
{
    public interface IDirectorRepository : IBaseRepository
    {
        Task<ICollection<Director>> GetAllDirectorsAsync();

        Task<Director> GetDirectorAsync(int directorId);

        Task<ICollection<Director>> GetDirectorsByMovieIdAsync(int movieId);

        Task<Director> GetDirectorByNameAsync(string lastName, string firstName);

        Task<int> GenerateDirectorId();
    }
}