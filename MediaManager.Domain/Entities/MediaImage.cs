using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MediaManager.Domain.Entities
{
    /// <summary>
    /// Actor is an entity class for holding all the information related to an actor including movies
    /// associated with them.
    /// </summary>
    public class MediaImage : BaseEntity
    {
        [Required]
        public byte[] ImageData { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "First name can only be 25 characters.")]
        public string ImageName { get; set; }

        [Required]
        public int MovieId { get; set; }

        #region MediaImage Constructor
        public MediaImage(int id, byte[] imageData, string imageName, int movieId) : base(id) 
        {
            ImageData = imageData;
            ImageName = imageName;
            MovieId = movieId;
        }
        #endregion

        #region MediaImage Overrides
        public override string ToString()
        {
            return $"{base.ToString()}:{ImageName}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var mediaImage = (MediaImage)obj;
            return mediaImage.Id == Id && mediaImage.ImageName == ImageName;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ ImageName.GetHashCode();
        }
        #endregion
    }
}
