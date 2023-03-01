using MediaManager.Data.Repositories.Interfaces;
using MediaManager.Data.Repositories;
using MediaManager.Data.Factories;

namespace MediaManager.Data.Services.Extensions
{
    public static class MediaCatalogRepositoryCollectionExtensions
    {
        public static IServiceCollection AddMediaManagerRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            //services.AddScoped<IMediaTypeRepository, MediaTypeRepository>();
            services.AddScoped<IStudioRepository, StudioRepository>();

            return services;
        }
    }
}