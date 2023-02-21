using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// StudioMovie is a class used to create a link between Movies and Studios as they
    /// have a many-to-many relationship.
    /// </summary>
    public class StudioMovie
    {
        [Required]
        public int StudioId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Studio Studio { get; set; }
        public Movie Movie { get; set; }

        #region StudioMovie Constructor
        public StudioMovie(Studio studio, Movie movie)
        {
            StudioId = studio.Id;
            MovieId = movie.Id;
            Studio = studio;
            Movie = movie;
        }
        #endregion

        #region StudioMovie Overrides
        public override string ToString()
        {
            return $"{Studio.Name}:{Movie.Title}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var studioMovie = (StudioMovie)obj;
            return studioMovie.Studio.Equals(Studio) && studioMovie.Movie.Equals(Movie);
        }

        public override int GetHashCode()
        {
            return Studio.GetHashCode() ^ Movie.GetHashCode();
        }
        #endregion
    }
}
