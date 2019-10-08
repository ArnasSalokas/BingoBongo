using System;

namespace Template.Entities.Database.Base
{
    public interface IModified
    {
        int? ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
