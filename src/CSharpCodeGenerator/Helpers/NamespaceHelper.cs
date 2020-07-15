using System;
using System.Collections.Generic;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Helpers
{
    public static class NamespaceHelper
    {
        public static IEnumerable<string> ExtractNamespace(IEnumerable<PropertySetting> propertySettings)
        {
            if (propertySettings is null)
            {
                throw new ArgumentNullException(nameof(propertySettings));
            }

            return propertySettings
                .Where(p => !TypeHelper.ExistsTypeAlias(p.PropertyType))
                .Select(p => p.PropertyType.Namespace)
                .Distinct();
        }
    }
}
