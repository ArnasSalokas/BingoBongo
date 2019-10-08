using System.Collections.Generic;

namespace Template.Repositories.Base.Mapper
{
    public interface IFieldMapper
    {
        IList<Field> Fields { get; }
        IList<Field> KeyFields { get; }
        IList<Field> SelectFields { get; }
        IList<Field> InsertFields { get; }
        IList<Field> UpdateFields { get; }

        string BuildSelectProjection(bool propertyNameOnly = false, string tableAlias = null);
    }
}
