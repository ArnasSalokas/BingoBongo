using System;

namespace Template.Entities.Database.Base
{
    public interface IAdded
    {
        int? AddedBy { get; set; }
        DateTime? AddedDate { get; set; }
    }
}
