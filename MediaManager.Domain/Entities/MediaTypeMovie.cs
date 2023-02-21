using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// ActorMovie is a class used to create a link between Movies and Media Types as they
    /// have a many-to-many relationship.
    /// </summary>
    public class MediaTypeMovie
    {
        [Required]
        public int MediaTypeId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public MediaType MediaType { get; set; }
        public Movie Movie { get; set; }

        #region MediaTypeMovie Constructor
        public MediaTypeMovie(MediaType mediaType, Movie movie)
        {
            MediaTypeId = mediaType.Id;
            MovieId = movie.Id;
            MediaType = mediaType;
            Movie = movie;
        }
        #endregion

        #region MediaTypeMovie Overrides
        public override string ToString()
        {
            return $"{MediaType.Name}:{Movie.Title}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var mediaTypeMovie = (MediaTypeMovie)obj;
            return mediaTypeMovie.MediaType.Equals(MediaType) && mediaTypeMovie.Movie.Equals(Movie);
        }

        public override int GetHashCode()
        {
            return MediaType.GetHashCode() ^ Movie.GetHashCode();
        }
        #endregion
    }
}
