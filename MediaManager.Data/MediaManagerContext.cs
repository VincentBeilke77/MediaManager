using MediaManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Data
{
    public class MediaManagerContext : DbContext
    {

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<MediaImage> MediaImages { get; set; }
        public DbSet<Studio> Studios { get; set; }

        private readonly IConfiguration _config;

        public MediaManagerContext(DbContextOptions options, IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("MediaManager"));
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddManyToManyRelationships(modelBuilder);
            AddTestDataModels(modelBuilder);
        }

        private static void AddTestDataModels(ModelBuilder modelBuilder)
        {
            var actor = new Actor
            (
                id: 1,
                firstName: "Sylvester",
                lastName: "Stallone"
            );

            modelBuilder.Entity<Actor>().HasData(actor);

            var rating = new Rating
            (
                id: 1,
                name: "R",
                description: "Under 17 requires accompanying parent or adult guardian. Contains " +
                "some adult material. Parents are urged to learn more about the film before taking " +
                "their young children with them."
            );

            modelBuilder.Entity<Rating>().HasData(rating);

            var movie = new Movie
            (
                id: 1,
                title: "Rambo: The Fight Continues",
                shortDescription: "The ultimate American action hero returns - with a vengeance!",
                longDescription: "After spending several years in Northern Thailand operating a " +
                "longboat on the Salween River, John Rambo reluctantly agrees to carry a group " +
                "of Christian missionaries into war-torn Burma. But when the aid workers are " +
                "captured by ruthless Nationalist Army soldiers, Rambo leads a group of " +
                "battle-scarred, combat-hardened mercenaries on an epic last-ditch mission to " +
                "rescue the prisoners - at all costs.",
                runTime: 91,
                releaseYear: 2008,
                favorite: true,
                ratingId: 1
            );

            modelBuilder.Entity<Movie>().HasData(movie);

            var actorMovie = new ActorMovie(actor.Id, movie.Id);
            modelBuilder.Entity<ActorMovie>().HasData(actorMovie);

            var director = new Director
            (
                id: 1,
                firstName: "Sylvestor",
                lastName: "Stallone"
            );

            modelBuilder.Entity<Director>().HasData(director);

            var directorMovie = new DirectorMovie(director.Id, movie.Id);
            modelBuilder.Entity<DirectorMovie>().HasData(directorMovie);

            var genre = new Genre
            (
                id: 1,
                name: "Action Adventure",
                description: "featuring characters involved in exciting and usually dangerous " +
                "activities and adventures The movie is closer to an action-adventure thriller than " +
                "a journalistic account, but energetic acting and vigorous directing make it work " +
                "harrowingly well on its own terms."
            );

            modelBuilder.Entity<Genre>().HasData(genre);

            var genreMovie = new GenreMovie(genre.Id, movie.Id);
            modelBuilder.Entity<GenreMovie>().HasData(genreMovie);

            var mediaType = new MediaType
            (
                id: 1,
                name: "Blu-ray",
                description: "a format of DVD designed for the storage of high-definition video and data."
            );

            modelBuilder.Entity<MediaType>().HasData(mediaType);

            var mediaTypeMovie = new MediaTypeMovie(mediaType.Id, movie.Id);
            modelBuilder.Entity<MediaTypeMovie>().HasData(mediaTypeMovie);

            var studio = new Studio
            (
                id: 1,
                name: "Lionsgate",
                description: "The first major new studio in decades, Lionsgate is a global content " +
                "leader with a reputation for innovation whose films, television series, " +
                "location-based and live entertainment attractions and Starz premium pay platform " +
                "reach next generation audiences around the world."
            );

            modelBuilder.Entity<Studio>().HasData(studio);

            var studioMovie = new StudioMovie(studio.Id, movie.Id);
            modelBuilder.Entity<StudioMovie>().HasData(studioMovie);
        }

        public static void AddManyToManyRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>()
                            .HasKey(am => new { am.MovieId, am.ActorId });
            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Actor)
                .WithMany(actor => actor.Movies);
            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Movie)
                .WithMany(movie => movie.Actors);

            modelBuilder.Entity<DirectorMovie>()
                .HasKey(dm => new { dm.DirectorId, dm.MovieId });
            modelBuilder.Entity<DirectorMovie>()
                .HasOne(dm => dm.Director)
                .WithMany(director => director.Movies);
            modelBuilder.Entity<DirectorMovie>()
                .HasOne(dm => dm.Movie)
                .WithMany(movie => movie.Directors);

            modelBuilder.Entity<GenreMovie>()
                .HasKey(gm => new { gm.GenreId, gm.MovieId });
            modelBuilder.Entity<GenreMovie>()
                .HasOne(gm => gm.Genre)
                .WithMany(genre => genre.Movies);
            modelBuilder.Entity<GenreMovie>()
                .HasOne(gm => gm.Movie)
                .WithMany(movie => movie.Genres);

            modelBuilder.Entity<MediaTypeMovie>()
                .HasKey(mtm => new { mtm.MediaTypeId, mtm.MovieId });
            modelBuilder.Entity<MediaTypeMovie>()
                .HasOne(mtm => mtm.MediaType)
                .WithMany(mediaType => mediaType.Movies);
            modelBuilder.Entity<MediaTypeMovie>()
                .HasOne(mtm => mtm.Movie)
                .WithMany(movie => movie.MediaTypes);

            modelBuilder.Entity<StudioMovie>()
                .HasKey(sm => new { sm.StudioId, sm.MovieId });
            modelBuilder.Entity<StudioMovie>()
                .HasOne(sm => sm.Studio)
                .WithMany(studio => studio.Movies);
            modelBuilder.Entity<StudioMovie>()
                .HasOne(sm => sm.Movie)
                .WithMany(movie => movie.Studios);
        }
    }
}
