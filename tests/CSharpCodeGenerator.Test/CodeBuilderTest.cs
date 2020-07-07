using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoyashiroKohaku.CSharpCodeGenerator.Builders;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class CodeBuilderTest
    {
        [TestMethod]
        public void ConstructorTest()
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

            var codeBuilder = new CodeBuilder(pocoClass);

            Assert.AreEqual(IndentStyle.Space, codeBuilder.IndentStyle);
            Assert.AreEqual(4, codeBuilder.IndentSize);
            Assert.AreEqual(EndOfLine.CRLF, codeBuilder.EndOfLine);
        }

        [TestMethod]
        public void IndentStyleTest()
        {
            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"))
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4
            };

            Assert.AreEqual(IndentStyle.Space, codeBuilder.IndentStyle);
            Assert.AreEqual(4, codeBuilder.IndentSize);

            codeBuilder.AppendIndent().Append("A").AppendLine();
            codeBuilder.DownIndent();
            codeBuilder.AppendIndent().Append("B").AppendLine();
            codeBuilder.DownIndent();
            codeBuilder.AppendIndent().Append("C").AppendLine();
            codeBuilder.UpIndent();
            codeBuilder.AppendIndent().Append("D").AppendLine();
            codeBuilder.UpIndent();
            codeBuilder.AppendIndent().Append("E");

            Assert.AreEqual($"A{codeBuilder.GetEndOfLineString()}    B{codeBuilder.GetEndOfLineString()}        C{codeBuilder.GetEndOfLineString()}    D{codeBuilder.GetEndOfLineString()}E", codeBuilder.ToString());

            codeBuilder.IndentStyle = IndentStyle.Tab;
            Assert.AreEqual(IndentStyle.Tab, codeBuilder.IndentStyle);

            Assert.AreEqual($"A{codeBuilder.GetEndOfLineString()}\tB{codeBuilder.GetEndOfLineString()}\t\tC{codeBuilder.GetEndOfLineString()}\tD{codeBuilder.GetEndOfLineString()}E", codeBuilder.ToString());

            codeBuilder.IndentStyle = IndentStyle.Space;
            Assert.AreEqual(IndentStyle.Space, codeBuilder.IndentStyle);

            Assert.AreEqual($"A{codeBuilder.GetEndOfLineString()}    B{codeBuilder.GetEndOfLineString()}        C{codeBuilder.GetEndOfLineString()}    D{codeBuilder.GetEndOfLineString()}E", codeBuilder.ToString());
        }

        [TestMethod]
        public void IndentSizeTest()
        {
            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"))
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4
            };

            Assert.AreEqual(IndentStyle.Space, codeBuilder.IndentStyle);
            Assert.AreEqual(4, codeBuilder.IndentSize);

            codeBuilder.AppendIndent().Append("A").AppendLine();
            codeBuilder.DownIndent();
            codeBuilder.AppendIndent().Append("B").AppendLine();
            codeBuilder.DownIndent();
            codeBuilder.AppendIndent().Append("C").AppendLine();
            codeBuilder.UpIndent();
            codeBuilder.AppendIndent().Append("D").AppendLine();
            codeBuilder.UpIndent();
            codeBuilder.AppendIndent().Append("E");

            Assert.AreEqual($"A{codeBuilder.GetEndOfLineString()}    B{codeBuilder.GetEndOfLineString()}        C{codeBuilder.GetEndOfLineString()}    D{codeBuilder.GetEndOfLineString()}E", codeBuilder.ToString());

            codeBuilder.IndentSize = 2;
            Assert.AreEqual(2, codeBuilder.IndentSize);

            Assert.AreEqual($"A{codeBuilder.GetEndOfLineString()}  B{codeBuilder.GetEndOfLineString()}    C{codeBuilder.GetEndOfLineString()}  D{codeBuilder.GetEndOfLineString()}E", codeBuilder.ToString());

            codeBuilder.IndentSize = 8;
            Assert.AreEqual(8, codeBuilder.IndentSize);

            Assert.AreEqual($"A{codeBuilder.GetEndOfLineString()}        B{codeBuilder.GetEndOfLineString()}                C{codeBuilder.GetEndOfLineString()}        D{codeBuilder.GetEndOfLineString()}E", codeBuilder.ToString());
        }

        [TestMethod]
        public void DownIndentAndUpIndentTest()
        {
            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"));

            Assert.AreEqual(0, codeBuilder.IndentDepth);

            /* Indent Down */

            codeBuilder.DownIndent();
            Assert.AreEqual(1, codeBuilder.IndentDepth);

            codeBuilder.DownIndent();
            Assert.AreEqual(2, codeBuilder.IndentDepth);

            /* Indent Up */

            codeBuilder.UpIndent();
            Assert.AreEqual(1, codeBuilder.IndentDepth);

            codeBuilder.UpIndent();
            Assert.AreEqual(0, codeBuilder.IndentDepth);

            /* Indent Down (Max) */

            codeBuilder.GetType().GetProperty(nameof(codeBuilder.IndentDepth)).SetValue(codeBuilder, int.MaxValue);
            codeBuilder.DownIndent();
            Assert.AreEqual(int.MaxValue, codeBuilder.IndentDepth);

            /* Indent Up (Min) */

            codeBuilder.GetType().GetProperty(nameof(codeBuilder.IndentDepth)).SetValue(codeBuilder, 0);
            codeBuilder.UpIndent();
            Assert.AreEqual(0, codeBuilder.IndentDepth);
        }

        [TestMethod]
        public void GetIndentStringTest()
        {
            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"));

            /* 4 Spaces */

            codeBuilder.IndentStyle = IndentStyle.Space;
            codeBuilder.IndentSize = 4;
            Assert.AreEqual(string.Empty, codeBuilder.GetIndentString());

            codeBuilder.DownIndent();
            Assert.AreEqual("    ", codeBuilder.GetIndentString());

            codeBuilder.DownIndent();
            Assert.AreEqual("        ", codeBuilder.GetIndentString());

            codeBuilder.UpIndent();
            Assert.AreEqual("    ", codeBuilder.GetIndentString());

            codeBuilder.UpIndent();
            Assert.AreEqual(string.Empty, codeBuilder.GetIndentString());

            /* 2 Spaces */

            codeBuilder.IndentStyle = IndentStyle.Space;
            codeBuilder.IndentSize = 2;
            Assert.AreEqual(string.Empty, codeBuilder.GetIndentString());

            codeBuilder.DownIndent();
            Assert.AreEqual("  ", codeBuilder.GetIndentString());

            codeBuilder.DownIndent();
            Assert.AreEqual("    ", codeBuilder.GetIndentString());

            codeBuilder.UpIndent();
            Assert.AreEqual("  ", codeBuilder.GetIndentString());

            codeBuilder.UpIndent();
            Assert.AreEqual(string.Empty, codeBuilder.GetIndentString());

            /* Tab */

            codeBuilder.IndentStyle = IndentStyle.Tab;
            Assert.AreEqual(string.Empty, codeBuilder.GetIndentString());

            codeBuilder.DownIndent();
            Assert.AreEqual("\t", codeBuilder.GetIndentString());

            codeBuilder.DownIndent();
            Assert.AreEqual("\t\t", codeBuilder.GetIndentString());

            codeBuilder.UpIndent();
            Assert.AreEqual("\t", codeBuilder.GetIndentString());

            codeBuilder.UpIndent();
            Assert.AreEqual(string.Empty, codeBuilder.GetIndentString());
        }

        [TestMethod]
        public void GetEndOfLineStringTest()
        {
            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"));

            /* CR */

            codeBuilder.EndOfLine = EndOfLine.CR;
            Assert.AreEqual("\r", codeBuilder.GetEndOfLineString());

            /* LF */

            codeBuilder.EndOfLine = EndOfLine.LF;
            Assert.AreEqual("\n", codeBuilder.GetEndOfLineString());

            /* CRLF */

            codeBuilder.EndOfLine = EndOfLine.CRLF;
            Assert.AreEqual("\r\n", codeBuilder.GetEndOfLineString());
        }

        [TestMethod]
        public void AppendTest()
        {
            var appendString = "test string";

            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"));

            codeBuilder.Append(appendString);

            var expected = appendString;
            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendLineTest()
        {
            var appendLineString = "test string";

            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"));

            codeBuilder.AppendLine();
            codeBuilder.AppendLine(appendLineString);
            codeBuilder.AppendLine();

            var expected = $"{codeBuilder.GetEndOfLineString()}{appendLineString}{codeBuilder.GetEndOfLineString()}{codeBuilder.GetEndOfLineString()}";
            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendIndentTest()
        {
            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"));

            codeBuilder.AppendIndent().Append("A");
            codeBuilder.DownIndent();
            codeBuilder.AppendIndent().Append("B");
            codeBuilder.DownIndent();
            codeBuilder.AppendIndent().Append("C");
            codeBuilder.UpIndent();
            codeBuilder.AppendIndent().Append("D");
            codeBuilder.UpIndent();
            codeBuilder.AppendIndent().Append("E");

            var expected = $"A    B        C    DE";
            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CurlyBracketTest()
        {
            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"))
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendCurlyBracket(CurlyBracket.Left);
            codeBuilder.AppendCurlyBracket(CurlyBracket.Right);
            codeBuilder.AppendCurlyBracket(CurlyBracket.Left);
            codeBuilder.AppendCurlyBracket(CurlyBracket.Left);
            codeBuilder.AppendCurlyBracket(CurlyBracket.Right);
            codeBuilder.AppendCurlyBracket(CurlyBracket.Right);

            var expected = "{}{{}}";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendXmlCommentTest()
        {
            var codeBuilder = new CodeBuilder(new POCOClass("TestClass"))
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendIndent().AppendXmlComment("Test XML Comment 01").AppendLine();

            codeBuilder.DownIndent();

            codeBuilder.AppendIndent().AppendXmlComment("Test XML Comment 02").AppendLine();

            codeBuilder.UpIndent();

            codeBuilder.AppendIndent().AppendXmlComment("Test XML Comment 03").AppendLine();

            var expected = @"/// <summary>
/// Test XML Comment 01
/// </summary>
    /// <summary>
    /// Test XML Comment 02
    /// </summary>
/// <summary>
/// Test XML Comment 03
/// </summary>
";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendNamespaceDeclarationTest()
        {
            var testNamespace = "TestOrganization.TestProduct";

            var pocoClass = new POCOClass("TestClass")
            {
                Namepace = testNamespace
            };

            var codeBuilder = new CodeBuilder(pocoClass)
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendNamespaceDeclaration();

            var expected = $"namespace {testNamespace}";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendClassXmlCommentTest()
        {
            var testClassXmlComment = "TestClass XML Comment";

            var pocoClass = new POCOClass("TestClass")
            {
                XmlComment = testClassXmlComment
            };

            var codeBuilder = new CodeBuilder(pocoClass)
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendClassXmlComment();

            var expected = $@"/// <summary>
/// TestClass XML Comment
/// </summary>";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendClassDeclarationTest()
        {
            var testClassName = "TestClass";

            var codeBuilder = new CodeBuilder(new POCOClass(testClassName))
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendClassDeclaration();

            var expected = $"public class {testClassName}";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendPropertiesTest()
        {
            var pocoClass = new POCOClass("TestClass");

            var testProperties = new ClassProperty[]
            {
                new ClassProperty("TestProperty01", typeof(int)),
                new ClassProperty("TestProperty02", typeof(string))
            };

            pocoClass.Properties.AddRange(testProperties);

            var codeBuilder = new CodeBuilder(pocoClass);

            codeBuilder.AppendIndent().AppendProperties().AppendLine();
            codeBuilder.DownIndent();
            codeBuilder.AppendIndent().AppendProperties().AppendLine();
            codeBuilder.UpIndent();
            codeBuilder.AppendIndent().AppendProperties().AppendLine();

            var expected = $@"public {TypeResolver.GetTypeAlias(testProperties[0].PropertyType)} {testProperties[0].PropertyName} {{ get; set; }}

public {TypeResolver.GetTypeAlias(testProperties[1].PropertyType)} {testProperties[1].PropertyName} {{ get; set; }}
    public {TypeResolver.GetTypeAlias(testProperties[0].PropertyType)} {testProperties[0].PropertyName} {{ get; set; }}

    public {TypeResolver.GetTypeAlias(testProperties[1].PropertyType)} {testProperties[1].PropertyName} {{ get; set; }}
public {TypeResolver.GetTypeAlias(testProperties[0].PropertyType)} {testProperties[0].PropertyName} {{ get; set; }}

public {TypeResolver.GetTypeAlias(testProperties[1].PropertyType)} {testProperties[1].PropertyName} {{ get; set; }}
";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
