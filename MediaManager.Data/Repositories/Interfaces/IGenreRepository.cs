using MediaManager.Domain.Entities;

namespace MediaManager.Data.Repositories.Interfaces
{
    public interface IGenreRepository : IBaseRepository
    {

        Task<ICollection<Genre>> GetAllGenresAsync();

        Task<Genre> GetGenreAsync(int id);

        Task<int> GenerateGenreId();

        bool CheckForExistingGenreName(string name);
    }
}
