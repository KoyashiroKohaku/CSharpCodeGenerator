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

            var code = CodeGenerator.Generate(pocoClass);
        }
    }
}
