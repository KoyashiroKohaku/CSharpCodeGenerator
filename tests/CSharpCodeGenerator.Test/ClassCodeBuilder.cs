using KoyashiroKohaku.CSharpCodeGenerator.Builders;
using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class ClassCodeBuilderTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var codeBuilder = new ClassCodeBuilder();

            Assert.AreEqual(IndentStyle.Space, codeBuilder.IndentStyle);
            Assert.AreEqual(4, codeBuilder.IndentSize);
            Assert.AreEqual(EndOfLine.CRLF, codeBuilder.EndOfLine);
        }

        [TestMethod]
        public void AppendClassDeclarationTest()
        {
            var testClassName = "TestClass";

            var codeBuilder = new ClassCodeBuilder()
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendClassDeclaration(testClassName);

            var expected = $"public class {testClassName}";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendPropertyDeclarationTest()
        {
            var testPropertySettings = new PropertySetting[]
            {
                new PropertySetting("TestProperty01", typeof(int)),
                new PropertySetting("TestProperty02", typeof(string)),
                new PropertySetting("TestProperty03", typeof(int[])),
                new PropertySetting("TestProperty04", typeof(string[])),
                new PropertySetting("TestProperty05", typeof(List<int>)),
                new PropertySetting("TestProperty06", typeof(List<string>)),
                new PropertySetting("TestProperty07", typeof(Dictionary<int, string>)),
                new PropertySetting("TestProperty08", typeof(int)) { Nullable = true },
                new PropertySetting("TestProperty09", typeof(string)) { Nullable = true }
            };

            var codeBuilder = new ClassCodeBuilder();

            foreach (var testProperty in testPropertySettings)
            {
                codeBuilder.AppendPropertyDeclaration(testProperty).AppendLine();
            }

            var expected = @"public int TestProperty01
public string TestProperty02
public int[] TestProperty03
public string[] TestProperty04
public List<int> TestProperty05
public List<string> TestProperty06
public Dictionary<int, string> TestProperty07
public int? TestProperty08
public string? TestProperty09
";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendAutoImplementedPropertiesTest()
        {
            var codeBuilder = new ClassCodeBuilder();

            codeBuilder.AppendAutoImplementedProperties(new PropertySetting("TestProperty", typeof(int)));

            var expected = "public int TestProperty { get; set; }";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExtractNamespaceTest()
        {
            var propertySettings = new PropertySetting[]
            {
                new PropertySetting("TestProperty01", typeof(int)),
                new PropertySetting("TestProperty02", typeof(List<int>)),
                new PropertySetting("TestProperty02", typeof(System.Text.StringBuilder))
            };

            var expected = propertySettings
                .Where(p => !TypeHelper.ExistsTypeAlias(p.PropertyType))
                .Select(p => p.PropertyType.Namespace)
                .ToArray();

            var actual = NamespaceHelper.ExtractNamespace(propertySettings).OrderBy(s => s).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
