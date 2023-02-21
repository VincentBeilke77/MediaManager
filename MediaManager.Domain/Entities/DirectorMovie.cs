using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// DirectorMovie is a class used to create a link between Movies and Directors as they
    /// have a many-to-many relationship.
    /// </summary>
    public class DirectorMovie
    {
        [Required]
        public int DirectorId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Director Director { get; set; }
        public Movie Movie { get; set; }

        #region DirectorMovie Constructor
        public DirectorMovie(Director director, Movie movie)
        {
            DirectorId = director.Id;
            MovieId = movie.Id;
            Director = director;
            Movie = movie;
        }
        #endregion

        #region DirectorMovie Overrides
        public override string ToString()
        {
            return $"{Director.FullName}:{Movie.Title}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var directorMovie = (DirectorMovie)obj;
            return directorMovie.Director.Equals(Director) && directorMovie.Movie.Equals(Movie);
        }

        public override int GetHashCode()
        {
            return Director.GetHashCode() ^ Movie.GetHashCode();
        }
        #endregion
    }
}
