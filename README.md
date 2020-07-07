# CSharpCodeGenerator

## Sample

```cs
var pocoClass = new POCOClass("TestClass")
{
    Namepace = "TestOrganization.TestProduct",
    XmlComment = "TestClass XML Comment"
};

var testProperties = new ClassProperty[]
{
    new ClassProperty("TestProperty01", typeof(int))
    {
        XmlComment = "TestProperty01 XML Comment"
    },
    new ClassProperty("TestProperty02", typeof(string))
    {
        XmlComment = "TestProperty02 XML Comment"
    },
    new ClassProperty("TestProperty03", typeof(int[]))
    {
        XmlComment = "TestProperty03 XML Comment"
    },
    new ClassProperty("TestProperty04", typeof(List<string>))
    {
        XmlComment = "TestProperty04 XML Comment"
    }
};

pocoClass.Properties.AddRange(testProperties);

var code = CodeGenerator.Generate(pocoClass);

/*
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
    }
}
*/
```
