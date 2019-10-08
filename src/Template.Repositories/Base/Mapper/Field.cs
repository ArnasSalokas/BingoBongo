using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Template.Common.Extensions;
using Template.Entities.Database.Base;
using Template.Entities.Database.Models;
using Template.Repositories.Base.Attributes;

namespace Template.Repositories.Base.Mapper
{
    public class Field
    {
        private readonly PropertyInfo property;

        #region Properties

        public string PropertyName { get; private set; }
        public string ColumnName { get; private set; }
        public string Formula { get; private set; }
        public string SetAlias { get; set; }

        public Func<object, object> Getter { get; private set; }

        public bool HasAlias
        {
            get { return PropertyName != ColumnName; }
        }

        public string PrefixedColumnName
        {
            get
            {
                return string.IsNullOrEmpty(SetAlias) ? ColumnName : SetAlias + "." + ColumnName.Bracketed();
            }
        }

        public bool IsKey { get; private set; }
        public bool IsSelectable { get; private set; }
        public bool IsInsertable { get; private set; }
        public bool IsUpdatable { get; private set; }
        public bool IsGeography { get; private set; }

        #endregion

        #region Constructors

        public Field()
        {
        }

        public Field(PropertyInfo property)
            : this()
        {
            this.property = property;
            PropertyName = property.Name;
            ColumnName = PropertyName;
            IsSelectable = true;
            IsInsertable = true;
            IsUpdatable = true;

            IsKey = property.GetCustomAttribute<KeyAttribute>() != null ||
                    (PropertyName == "Id" &&
                     (property.PropertyType == typeof(int) || property.PropertyType == typeof(Guid)));

            IsGeography = property.PropertyType == typeof(GeographyPoint);

            if (IsKey)
            {
                IsSelectable = true;
                IsInsertable = PropertyName != "Id";
                IsUpdatable = false;
            }

            else if (IsGeography)
            {
                IsSelectable = false;
                IsInsertable = true;
                IsUpdatable = true;
            }
            else
            {
                var ignoreAttribute = property.GetCustomAttribute<IgnoreAttribute>();
                if (ignoreAttribute != null)
                {
                    IsSelectable = (ignoreAttribute.Type & IgnoreTypeEnum.Select) == 0;
                    IsInsertable = (ignoreAttribute.Type & IgnoreTypeEnum.Insert) == 0;
                    IsUpdatable = (ignoreAttribute.Type & IgnoreTypeEnum.Update) == 0;
                }
            }

            Getter = BuildGetter(property);
        }

        private static Func<object, object> BuildGetter(PropertyInfo property)
        {
            var classType = property.DeclaringType;

            if (classType == null)
                return null;

            var model = Expression.Parameter(typeof(object), "model");

            var body = Expression.Property(Expression.Convert(model, classType), property.Name);

            var convertedBody = Expression.Convert(body, typeof(object));

            return Expression.Lambda<Func<object, object>>(convertedBody, model).Compile();
        }

        public Field(string setAlias, string columnName, string propertyName)
            : this()
        {
            SetAlias = setAlias;
            ColumnName = columnName;
            PropertyName = propertyName ?? columnName;
        }

        #endregion

        #region Public Methods

        public string ToProjectionString(string tableAlias = null)
        {
            return HasAlias
                ? $"{tableAlias}{PrefixedColumnName.Bracketed()} AS {PropertyName.Bracketed()}"
                : $"{tableAlias}{PrefixedColumnName.Bracketed()}";
        }

        public object GetColumnValue(object item) => property.GetValue(item);

        #endregion

        #region Overrides

        public override string ToString() => $"{PropertyName}";

        #endregion
    }
}
