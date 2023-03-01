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

        public Director? Director { get; set; }
        public Movie? Movie { get; set; }

        #region DirectorMovie Constructor
        private DirectorMovie() { }

        public DirectorMovie(int directorId, int movieId)
        {
            DirectorId = directorId;
            MovieId = movieId;
        }
        #endregion

        #region DirectorMovie Overrides
        public override string ToString()
        {
            return $"{DirectorId}:{MovieId}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var directorMovie = (DirectorMovie)obj;
            return directorMovie.DirectorId == DirectorId && directorMovie.MovieId == MovieId;
        }

        public override int GetHashCode()
        {
            return DirectorId.GetHashCode() ^ MovieId.GetHashCode();
        }
        #endregion
    }
}
