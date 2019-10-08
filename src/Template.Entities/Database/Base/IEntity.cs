using System.ComponentModel.DataAnnotations;

namespace Template.Entities.Database.Base
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
