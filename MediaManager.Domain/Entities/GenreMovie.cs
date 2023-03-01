using System.ComponentModel.DataAnnotations;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// GenreMovie is a class used to create a link between Movies and Genres as they
    /// have a many-to-many relationship.
    /// </summary>
    public class GenreMovie
    {
        [Required]
        public int GenreId { get; set; }
        [Required]
        public int MovieId { get; set; }

        public Genre? Genre { get; set; }
        public Movie? Movie { get; set; }

        #region GenreMovie Constructor
        private GenreMovie() { }

        public GenreMovie(int genreId, int movieId) 
        { 
            GenreId = genreId;
            MovieId = movieId;
        }
        #endregion

        #region GenreMovie Overrides
        public override string ToString()
        {
            return $"{GenreId}:{MovieId}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var genreMovie = (GenreMovie)obj;
            return genreMovie.GenreId == GenreId && genreMovie.MovieId == MovieId;
        }

        public override int GetHashCode()
        {
            return GenreId.GetHashCode() ^ MovieId.GetHashCode();
        }
        #endregion
    }
}
