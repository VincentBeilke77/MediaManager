using MediaManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Data
{
    public class MediaManagerContext : DbContext
    {
        private readonly IConfiguration _config;

        public MediaManagerContext(DbContextOptions options, IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<MediaImage> MediaImages { get; set; }
        public DbSet<Studio> Studios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("MediaCatalog"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddManyToManyRelationships(modelBuilder);
            AddTestDataModels(modelBuilder);
        }

        private static void AddTestDataModels(ModelBuilder modelBuilder)
        {
            var actor = new
            {
                Id = 1,
                FirstName = "Sylvester",
                LastName = "Stallone"
            };

            modelBuilder.Entity<Actor>().HasData(actor);

            var actorMovie = new { ActorId = 1, MovieId = 1 };

            modelBuilder.Entity<ActorMovie>().HasData(actorMovie);

            var rating = new
            {
                Id = 1,
                Name = "R",
                Description = "Under 17 requires accompanying parent or adult guardian. Contains " +
                "some adult material. Parents are urged to learn more about the film before taking " +
                "their young children with them."
            };

            modelBuilder.Entity<Rating>().HasData(rating);

            var movie = new
            {
                Id = 1,
                Title = "Rambo: The Fight Continues",
                ShortDescription = "The ultimate American action hero returns - with a vengeance!",
                LongDescription = "After spending several years in Northern Thailand operating a " +
                    "longboat on the Salween River, John Rambo reluctantly agrees to carry a group " +
                    "of Christian missionaries into war-torn Burma. But when the aid workers are " +
                    "captured by ruthless Nationalist Army soldiers, Rambo leads a group of " +
                    "battle-scarred, combat-hardened mercenaries on an epic last-ditch mission to " +
                    "rescue the prisoners - at all costs.",
                RunTime = 91,
                ReleaseYear = 2008,
                RatingId = 1,
                Favorite = true
            };

            modelBuilder.Entity<Movie>().HasData(movie);

            var director = new
            {
                Id = 1,
                FirstName = "Sylvestor",
                LastName = "Stallone"
            };

            modelBuilder.Entity<Director>().HasData(director);

            var directorMovie = new { DirectorId = 1, MovieId = 1 };

            modelBuilder.Entity<DirectorMovie>().HasData(directorMovie);

            var genre = new
            {
                Id = 1,
                Name = "Action Adventure",
                Description = "featuring characters involved in exciting and usually dangerous " +
                "activities and adventures The movie is closer to an action-adventure thriller than " +
                "a journalistic account, but energetic acting and vigorous directing make it work " +
                "harrowingly well on its own terms."
            };

            modelBuilder.Entity<Genre>().HasData(genre);

            var genreMovie = new { GenreId = 1, MovieId = 1 };

            modelBuilder.Entity<GenreMovie>().HasData(genreMovie);

            var mediaType = new
            {
                Id = 1,
                Name = "Blu-ray",
                Description = "a format of DVD designed for the storage of high-definition video and data."
            };

            modelBuilder.Entity<MediaType>().HasData(mediaType);

            var mediaTypeMovie = new { MediaTypeId = 1, MovieId = 1 };

            modelBuilder.Entity<MediaTypeMovie>().HasData(mediaTypeMovie);

            var studio = new
            {
                Id = 1,
                Name = "Lionsgate",
                Description = "The first major new studio in decades, Lionsgate is a global content " +
                "leader with a reputation for innovation whose films, television series, " +
                "location-based and live entertainment attractions and Starz premium pay platform " +
                "reach next generation audiences around the world."
            };

            modelBuilder.Entity<Studio>().HasData(studio);

            var studioMovie = new { MovieId = 1, StudioId = 1 };

            modelBuilder.Entity<StudioMovie>().HasData(studioMovie);
        }

        public static void AddManyToManyRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>()
                            .HasKey(am => new { am.ActorId, am.MovieId });
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
