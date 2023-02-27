using MediaManager.Domain.Entities;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IActorRepository
    {
        Task<ICollection<Actor>> GetAllActorsAsync();

        Task<Actor> GetActorAsync(int actorId);

        Task<ICollection<Actor>> GetActorsByMovieIdAsync(int movieId);

        Task<Actor> GetActorByNameAsync(string lastName, string firstName);

        int GenerateActorId();

        Task<ICollection<Actor>> GetActorsByNameSearchValue(string value);
    }
}