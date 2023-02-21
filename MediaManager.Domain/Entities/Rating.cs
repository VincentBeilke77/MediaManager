using System.ComponentModel.DataAnnotations;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// Rating is an entity class for holding all the information related to a rating including movies
    /// associated with it.
    /// </summary>
    public class Rating : BaseEntity
    {
        [Required]
        [StringLength(25, ErrorMessage = "Rating name can only be 25 characters.")]
        public string Name { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }

        protected ICollection<Movie> Movies { get; set; }

        #region Rating Constructors
        public Rating(int id, string name) : base(id)
        {
            Name = name;
            ShortDescription = string.Empty;
            Description = string.Empty;
            Movies = new List<Movie>();
        }

        public Rating(int id, string name, string shortDescription) : this(id, name)
        {
            ShortDescription = shortDescription;
        }

        public Rating(int id, string name, string shortDescription, string description) : this(id, name, shortDescription)
        {
            Description = description;
        }

        public Rating(int id, string name, string shortDescription, string description, ICollection<Movie> movies) 
            : this(id, name, shortDescription, description)
        {
            Movies = movies;
        }
        #endregion

        #region Rating Overrides
        public override string ToString()
        {
            return $"{base.ToString()}:{Name}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var rating = (Rating) obj;
            return rating.Id == Id && rating.Name == Name;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }
        #endregion
    }
}
