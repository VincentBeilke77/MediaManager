using System.ComponentModel.DataAnnotations;

namespace MediaManager.Domain.Entities
{
    public class ActorMovie
    {
        [Required]
        public int ActorId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Actor Actor { get; set; }
        public Movie Movie { get; set; }

        #region ActorMovie Constructor
        public ActorMovie(Actor actor, Movie movie) 
        {
            ActorId = actor.Id;
            MovieId = movie.Id;
            Actor = actor;
            Movie = movie;
        }
        #endregion

        #region ActorMovie Overrides
        public override string ToString()
        {
            return $"{Actor.FullName}:{Movie.Title}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var actorMovie = (ActorMovie) obj;
            return actorMovie.Actor.Equals(Actor) && actorMovie.Movie.Equals(Movie);
        }

        public override int GetHashCode()
        {
            return Actor.GetHashCode() ^ Movie.GetHashCode();
        }
        #endregion
    }
}
