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

            pocoClass.Properties.Add(new ClassProperty("TestProperty01", typeof(int))
            {
                XmlComment = "TestProperty01 XML Comment"
            });

            pocoClass.Properties.Add(new ClassProperty("TestProperty02", typeof(string))
            {
                XmlComment = "TestProperty02 XML Comment"
            });

            var expected = $@"namespace TestOrganization.TestProduct
{{
    /// <summary>
    /// TestClass XML Comment
    /// </summary>
    public class TestClass
    {{
        /// <summary>
        /// TestProperty01 XML Comment
        /// </summary>
        public int TestProperty01 {{ get; set; }}

        /// <summary>
        /// TestProperty02 XML Comment
        /// </summary>
        public string TestProperty02 {{ get; set; }}
    }}
}}
";

            var actual = CodeGenerator.Generate(pocoClass);

            Assert.AreEqual(expected, actual);
        }
    }
}
