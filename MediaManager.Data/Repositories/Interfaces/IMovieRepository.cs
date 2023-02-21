using MediaManager.Domain.Entities;

namespace MediaManager.Data.Repositories.Interfaces
{
    public interface IMovieRepository : IBaseRepository
    {
        Task<ICollection<Movie>> SearchMoviesByTitle(string title);
        Task<ICollection<Movie>> GetFavoriteMovies();
        bool CheckForExistingMovie(string title);
        bool CheckForExistingMovie(int id);
        Task<int> GenerateMovieId();
        Task<ICollection<Movie>> GetAllAsync();
        Task<Movie> GetAsync(int id);
    }
}
