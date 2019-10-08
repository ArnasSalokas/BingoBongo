using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Template.Common.Extensions;

namespace Template.Repositories.Base.Mapper
{
    public class FieldMapper<TModel> : IFieldMapper
    {
        #region Properties

        public IList<Field> Fields { get; private set; }
        public IList<Field> KeyFields { get; private set; }
        public IList<Field> SelectFields { get; private set; }
        public IList<Field> InsertFields { get; private set; }
        public IList<Field> UpdateFields { get; private set; }

        #endregion

        #region Constructors

        public FieldMapper()
        {
            var modelType = typeof(TModel);

            Fields = modelType.GetProperties().Where(p => p.GetSetMethod() != null).Select(p => new Field(p)).ToList();
            KeyFields = Fields.Where(f => f.IsKey).ToList();
            SelectFields = Fields.Where(f => f.IsSelectable).ToList();
            InsertFields = Fields.Where(f => f.IsInsertable).ToList();
            UpdateFields = Fields.Where(f => f.IsUpdatable).ToList();
        }

        #endregion

        #region Public Methods

        public string BuildSelectProjection(bool propertyNameOnly = false, string tableAlias = null)
        {
            tableAlias = tableAlias != null ? tableAlias + "." : string.Empty;

            if (propertyNameOnly)
                return string.Join(", ", SelectFields.Select(f => $"{tableAlias}{f.PropertyName.Bracketed()}"));

            return string.Join(", ", SelectFields.Select(f => f.ToProjectionString(tableAlias)));
        }

        #endregion
    }

    public static class FieldMapper
    {
        private static readonly ConcurrentDictionary<Type, IFieldMapper> mappers;

        static FieldMapper()
        {
            mappers = new ConcurrentDictionary<Type, IFieldMapper>();
        }

        public static FieldMapper<T> Get<T>()
        {
            return Get(typeof(T)) as FieldMapper<T>;
        }

        public static IFieldMapper Get(Type type)
        {
            if (mappers.ContainsKey(type)) return mappers[type];

            var mapperType = typeof(FieldMapper<>).MakeGenericType(type);
            var mapper = Activator.CreateInstance(mapperType) as IFieldMapper;

            mappers.TryAdd(type, mapper);

            return mappers[type];
        }
    }
}
