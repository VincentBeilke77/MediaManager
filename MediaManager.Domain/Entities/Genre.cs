using System.ComponentModel.DataAnnotations;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// Genre is an entity class for holding all the information related to a genre including movies
    /// associated with it.
    /// </summary>
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(25, ErrorMessage = "Genre name can only be 25 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<GenreMovie> Movies { get; set; }

        #region Genre Constructors
        public Genre(int id, string name) : base(id)
        {
            Name = name;
            Description = string.Empty;
            Movies = new List<GenreMovie>();
        }

        public Genre(int id, string name, string description) : this(id, name)
        {
            Description = description;
        }
        #endregion

        #region Genre Overrides
        public override string ToString()
        {
            return $"{base.ToString()}:{Name}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var genre = (Genre) obj;
            return genre.Id == Id && genre.Name == Name;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }
        #endregion
    }
}
