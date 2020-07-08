# CSharpCodeGenerator

## Sample

```cs
var pocoClass = new POCOClass("TestClass")
{
    Namepace = "TestOrganization.TestProduct",
    XmlComment = "TestClass XML Comment"
};

pocoClass.Properties.Add(new ClassProperty("TestProperty01", typeof(int))
{
    XmlComment = "TestProperty01 XML Comment"
});

pocoClass.Properties.Add(new ClassProperty("TestProperty02", typeof(string))
{
    XmlComment = "TestProperty02 XML Comment"
});

pocoClass.Properties.Add(new ClassProperty("TestProperty03", typeof(int[]))
{
    XmlComment = "TestProperty03 XML Comment"
});

pocoClass.Properties.Add(new ClassProperty("TestProperty04", typeof(List<string>))
{
    XmlComment = "TestProperty04 XML Comment"
});

pocoClass.Properties.Add(new ClassProperty("TestProperty05", typeof(DateTime))
{
    XmlComment = "TestProperty05 XML Comment"
});

var code = CodeGenerator.Generate(pocoClass);

/* Result */
/*
using System;
using System.Collections.Generic;

namespace TestOrganization.TestProduct
{
    /// <summary>
    /// TestClass XML Comment
    /// </summary>
    public class TestClass
    {
        /// <summary>
        /// TestProperty01 XML Comment
        /// </summary>
        public int TestProperty01 { get; set; }

        /// <summary>
        /// TestProperty02 XML Comment
        /// </summary>
        public string TestProperty02 { get; set; }

        /// <summary>
        /// TestProperty03 XML Comment
        /// </summary>
        public int[] TestProperty03 { get; set; }

        /// <summary>
        /// TestProperty04 XML Comment
        /// </summary>
        public List<string> TestProperty04 { get; set; }

        /// <summary>
        /// TestProperty05 XML Comment
        /// </summary>
        public DateTime TestProperty05 { get; set; }
    }
}
*/
```
