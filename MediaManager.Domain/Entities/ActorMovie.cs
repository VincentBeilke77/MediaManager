using System.ComponentModel.DataAnnotations;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// ActorMovie is a class used to create a link between Movies and Actors as they
    /// have a many-to-many relationship.
    /// </summary>
    public class ActorMovie
    {
        [Required]
        public int ActorId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Actor? Actor { get; set; }
        public Movie? Movie { get; set; }

        #region ActorMovie Constructor
        private ActorMovie() { }

        public ActorMovie(int actorId, int movieId) 
        {
            ActorId = actorId;
            MovieId = movieId;
        }
        #endregion

        #region ActorMovie Overrides
        public override string ToString()
        {
            return $"{ActorId}:{MovieId}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var actorMovie = (ActorMovie) obj;
            return actorMovie.ActorId == ActorId && actorMovie.MovieId == MovieId;
        }

        public override int GetHashCode()
        {
            return ActorId.GetHashCode() ^ MovieId.GetHashCode();
        }
        #endregion
    }
}
