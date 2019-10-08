using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Template.Common.Helpers
{
    public static class ReflectionHelper
    {
        public static PropertyInfo GetKeyProperty<T>() => GetKeyProperty(typeof(T));

        public static object GetKeyValue(object entity) => GetKeyProperty(entity.GetType()).GetValue(entity);

        public static PropertyInfo GetKeyProperty(Type entityType)
        {
            var properties = entityType.GetProperties();

            var keyProperty = properties.FirstOrDefault(pi => pi.GetCustomAttribute<KeyAttribute>(true) != null);

            return keyProperty ?? properties.FirstOrDefault(pi => pi.Name == "Id");
        }

        public static IEnumerable<Type> GetClassesOfType<T>()
        {
            var type = typeof(T);
            var result = type.Assembly.GetTypes().Where(v => type.IsAssignableFrom(v) && !v.IsAbstract && v.IsClass);
            return result;
        }

        public static object GetPropertyValue(object entity, string property) => entity?.GetType().GetProperty(property)?.GetValue(entity, null);
    }
}
