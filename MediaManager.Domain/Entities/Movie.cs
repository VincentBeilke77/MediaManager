using System.ComponentModel.DataAnnotations;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// Movie is entity class for holding all the information related to a Movie.
    /// </summary>
    public class Movie : BaseEntity
    {

        [Required]
        [StringLength(50, ErrorMessage = "The movie title can only be 50 characters long.")]
        public string Title { get; set; }
        [StringLength(255, ErrorMessage = "The short description can only be 255 characters long.")]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int RunTime { get; set; }
        public int ReleaseYear { get; set; }
        public bool Favorite { get; set; }
        public int RatingId { get; set; }

        public Rating? Rating { get; set; }
        public MediaImage? Image { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Director> Directors { get; set; }
        public ICollection<Studio> Studios { get; set; }

        #region Movie Constructors
        public Movie(int id, string title) : base(id) 
        {
            Title = title;
            ShortDescription = string.Empty;
            LongDescription = string.Empty;
            Actors = new List<Actor>();
            Genres = new List<Genre>();
            Directors = new List<Director>();
            Studios = new List<Studio>();
        }

        public Movie(int id, string title, string shortDescription)
            : this(id, title)
        {
            ShortDescription = shortDescription;
        }

        public Movie(int id, string title, string shortDescription, string longDescription) 
            : this(id, title, shortDescription)
        {
            LongDescription = longDescription;
        }

        public Movie(int id, string title, string shortDescription, string longDescription
            , int runTime)
            : this(id, title, shortDescription, longDescription)
        {
            RunTime = runTime;
        }

        public Movie(int id, string title, string shortDescription, string longDescription,
            int runTime, int releaseYear)
            : this(id, title, shortDescription, longDescription, runTime)
        {
            ReleaseYear = releaseYear;
        }

        public Movie(int id, string title, string shortDescription, string longDescription,
            int runTime, int releaseYear, bool favorite)
            : this(id, title, shortDescription, longDescription, runTime, releaseYear)
        {
            Favorite = favorite;
        }

        public Movie(int id, string title, string shortDescription, string longDescription,
            int runTime, int releaseYear, bool favorite, int ratingId)
            : this(id, title, shortDescription, longDescription, runTime, releaseYear,
                favorite)
        {
            RatingId = ratingId;
        }
        #endregion

        #region Movie Overrides
        public override string ToString()
        {
            return $"{base.ToString()}:{Title}:{RunTime}:{ReleaseYear}:{Favorite}:{Rating?.Name}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var movie = (Movie) obj;
            return movie.Id == Id && movie.Title.Equals(Title);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Title.GetHashCode();
        }
        #endregion
    }
}
