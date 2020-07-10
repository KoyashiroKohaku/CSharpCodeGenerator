using System;
using System.Collections.Generic;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public class ClassSetting
    {
        public ClassSetting(string className)
        {
            ClassName = className ?? throw new ArgumentNullException(nameof(className));
        }

        public string ClassName { get; set; }

        public string? XmlComment { get; set; }

        public string? Namepace { get; set; }

        public List<PropertySetting> PropertySettings { get; } = new List<PropertySetting>();
    }
}
