using System.ComponentModel.DataAnnotations;

namespace MediaManager.Domain.Entities
{
    /// <summary>   
    /// Actor is an entity class for holding all the information related to an actor including movies
    /// associated with them.
    /// </summary>
    public class Actor : BaseEntity
    {
        [MaxLength(25, ErrorMessage = "First name can only be 25 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Last name can only be 25 characters.")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                var fullName = LastName;
                if (string.IsNullOrWhiteSpace(FirstName)) return fullName;
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    fullName += ", ";
                }
                fullName += FirstName;
                return fullName;
            }
        }

        public ICollection<ActorMovie> Movies { get; set; }

        #region Actor Constructors
        public Actor(int id, string lastName) : base(id)
        {
            LastName = lastName;
            FirstName = string.Empty;
            Movies = new List<ActorMovie>();
        }

        public Actor(int id, string lastName, string firstName) : this(id, lastName) 
        {
            FirstName = firstName;
        }
        #endregion

        #region Actor Overrides
        public override string ToString()
        {
            return $"{base.ToString()}:{FullName}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var actor = (Actor) obj;
            return actor.Id == Id && actor.FullName == FullName;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ FullName.GetHashCode();
        }
        #endregion
    }
}
