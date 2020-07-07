using System.Collections.Generic;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public class POCOClass
    {
        public POCOClass(string className)
        {
            ClassName = className;
        }

        public string ClassName { get; set; }

        public string? XmlComment { get; set; }

        public string? Namepace { get; set; }

        public List<ClassProperty> Properties { get; } = new List<ClassProperty>();
    }
}
