using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.Domain.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        #region BaseEntity Constructor
        public BaseEntity(int id) 
        {
            Id = id;
        }
        #endregion

        #region BaseEntity Overrides
        public override string ToString()
        {
            return $"{Id}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var be = (BaseEntity) obj;
            return be.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
