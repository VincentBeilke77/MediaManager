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

        public MediaType? MediaType { get; set; }
        public Movie? Movie { get; set; }

        #region MediaTypeMovie Constructor
        private MediaTypeMovie() { }

        public MediaTypeMovie(int mediaTypeId, int movieId)
        {
            MediaTypeId = mediaTypeId;
            MovieId = movieId;
        }
        #endregion

        #region MediaTypeMovie Overrides
        public override string ToString()
        {
            return $"{MediaTypeId}:{MovieId}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var mediaTypeMovie = (MediaTypeMovie)obj;
            return mediaTypeMovie.MediaTypeId == MediaTypeId && mediaTypeMovie.MovieId == MovieId;
        }

        public override int GetHashCode()
        {
            return MediaTypeId.GetHashCode() ^ MovieId.GetHashCode();
        }
        #endregion
    }
}
