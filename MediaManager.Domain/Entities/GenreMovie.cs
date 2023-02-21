using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Genre Genre { get; set; }
        public Movie Movie { get; set; }

        #region GenreMovie Constructor
        public GenreMovie(Genre genre, Movie movie) 
        { 
            Genre = genre;
            Movie = movie;
        }
        #endregion

        #region GenreMovie Overrides
        public override string ToString()
        {
            return $"{Genre.Name}:{Movie.Title}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var genreMovie = (GenreMovie)obj;
            return genreMovie.Genre.Equals(Genre) && genreMovie.Movie.Equals(Movie);
        }

        public override int GetHashCode()
        {
            return Genre.GetHashCode() ^ Movie.GetHashCode();
        }
        #endregion
    }
}
