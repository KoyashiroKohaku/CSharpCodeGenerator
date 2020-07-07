using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class CodeGeneratorTest
    {
        [TestMethod]
        public void GenerateTest()
        {
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
            };

            pocoClass.Properties.AddRange(testProperties);

            var expected = $@"namespace {pocoClass.Namepace}
{{
    /// <summary>
    /// {pocoClass.XmlComment}
    /// </summary>
    public class {pocoClass.ClassName}
    {{
        /// <summary>
        /// {testProperties[0].XmlComment}
        /// </summary>
        public {TypeResolver.GetTypeAlias(testProperties[0].PropertyType)} {testProperties[0].PropertyName} {{ get; set; }}

        /// <summary>
        /// {testProperties[1].XmlComment}
        /// </summary>
        public {TypeResolver.GetTypeAlias(testProperties[1].PropertyType)} {testProperties[1].PropertyName} {{ get; set; }}
    }}
}}
";

            var actual = CodeGenerator.Generate(pocoClass);

            Assert.AreEqual(expected, actual);
        }
    }
}
