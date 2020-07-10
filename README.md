# CSharpCodeGenerator

## Example

```cs
var classSetting = new ClassSetting("TestClass")
{
    Namepace = "TestOrganization.TestProduct",
    XmlComment = "TestClass XML Comment"
};

classSetting.PropertySettings.Add(new PropertySetting("TestProperty01", typeof(int))
{
    XmlComment = "TestProperty01 XML Comment"
});

classSetting.PropertySettings.Add(new PropertySetting("TestProperty02", typeof(string))
{
    XmlComment = "TestProperty02 XML Comment"
});

classSetting.PropertySettings.Add(new PropertySetting("TestProperty03", typeof(int[]))
{
    XmlComment = "TestProperty03 XML Comment"
});

classSetting.PropertySettings.Add(new PropertySetting("TestProperty04", typeof(List<string>))
{
    XmlComment = "TestProperty04 XML Comment"
});

classSetting.PropertySettings.Add(new PropertySetting("TestProperty05", typeof(DateTime))
{
    XmlComment = "TestProperty05 XML Comment"
});

classSetting.PropertySettings.Add(new PropertySetting("TestProperty06", typeof(int))
{
    XmlComment = "TestProperty06 XML Comment",
    Nullable = true
});

classSetting.PropertySettings.Add(new PropertySetting("TestProperty07", typeof(string))
{
    XmlComment = "TestProperty07 XML Comment",
    Nullable = true
});

var code = CodeGenerator.Generate(classSetting);

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
        
        /// <summary>
        /// TestProperty06 XML Comment
        /// </summary>
        public int? TestProperty06 { get; set; }

        /// <summary>
        /// TestProperty07 XML Comment
        /// </summary>
        public string? TestProperty07 { get; set; }
    }
}
*/
```
