using System;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public class PropertySetting
    {
        public PropertySetting(string propertyName, Type propertyType)
        {
            PropertyName = propertyName;
            PropertyType = propertyType;
        }

        public string PropertyName { get; set; }

        public string? XmlComment { get; set; }

        public Type PropertyType { get; set; }

        public bool Nullable { get; set; } = false;

        public bool AutoImplementedProperties { get; set; } = true;

        public FieldNamingConvention FieldNamingConvention { get; set; } = FieldNamingConvention.CamelWithUnderscoreInThePrefix;
    }
}
