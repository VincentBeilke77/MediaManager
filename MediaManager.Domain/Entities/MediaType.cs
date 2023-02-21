using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaManager.Domain.Entities
{
    /// <summary>   
    /// MediaType is an entity class for holding all the information related to a media type including movies
    /// associated with them.
    /// </summary>
    public class MediaType : BaseEntity
    {
        [Required]
        [StringLength(25, ErrorMessage = "Media type name can only be 25 characters.")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public ICollection<MediaTypeMovie> Movies { get; set; }

        #region MediaType Constructors
        public MediaType(int id, string name) : base(id)
        {
            Name = name;
            Movies = new List<MediaTypeMovie>();
        }

        public MediaType(int id, string name, string description) : this(id, name)
        {
            Description = description;
        }
        #endregion

        #region MediaType Overrides
        public override string ToString()
        {
            return $"{base.ToString()}:{Name}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var mediaType = (MediaType)obj;
            return mediaType.Id == Id && mediaType.Name == Name;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }
        #endregion
    }
}
