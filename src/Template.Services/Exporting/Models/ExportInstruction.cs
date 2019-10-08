using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Template.Entities.Attributes;

namespace Template.Services.Exporting.Models
{
    public class ExportInstruction
    {
        public string Header { get; private set; }
        public string Property { get; private set; }
        public string Format { get; private set; }
        public int Order { get; private set; }

        public static IEnumerable<ExportInstruction> Construct<T>(T item)
        {
            var result = new List<ExportInstruction>();

            var excelProperties = item.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(XlsFormatAttribute)));

            foreach (var property in excelProperties)
            {
                var attribute = (XlsFormatAttribute)property.GetCustomAttributes(typeof(XlsFormatAttribute), false).SingleOrDefault();

                result.Add(ConstructInstruction(property, attribute));
            }

            return result.OrderBy(r => r.Order);
        }

        private static ExportInstruction ConstructInstruction(PropertyInfo propertyInfo, XlsFormatAttribute attribute)
        {
            return new ExportInstruction
            {
                Header = attribute.Header,
                Property = propertyInfo.Name,
                Format = attribute.Format,
                Order = attribute.Order
            };
        }
    }
}
