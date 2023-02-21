using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// Studio is an entity class for holding all the information related to a studio including movies
    /// associated with them.
    /// </summary>
    public class Studio : BaseEntity
    {
        [Required]
        [StringLength(25, ErrorMessage = "Studio name can only be 25 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<StudioMovie> Movies { get; set; }

        #region Studio Constructors
        public Studio(int id, string name) : base(id)
        {
            Name = name;
            Movies = new List<StudioMovie>();
        }

        public Studio(int id, string name, string description) : this(id, name)
        {
            Description = description;
        }
        #endregion

        #region Studio Overrides
        public override string ToString()
        {
            return $"{base.ToString()}:{Name}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var studio = (Studio)obj;
            return studio.Id == Id && studio.Name == Name;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }
        #endregion
    }
}
