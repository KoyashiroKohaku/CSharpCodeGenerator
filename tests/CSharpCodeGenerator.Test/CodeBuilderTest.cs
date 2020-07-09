using KoyashiroKohaku.CSharpCodeGenerator.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class CodeBuilderTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var codeBuilder = new CodeBuilder();

            Assert.AreEqual(IndentStyle.Space, codeBuilder.IndentStyle);
            Assert.AreEqual(4, codeBuilder.IndentSize);
            Assert.AreEqual(EndOfLine.CRLF, codeBuilder.EndOfLine);
        }

        [TestMethod]
        public void IndentStyleTest()
        {
            var codeBuilder = new CodeBuilder()
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4
            };

            Assert.AreEqual(IndentStyle.Space, codeBuilder.IndentStyle);
            Assert.AreEqual(4, codeBuilder.IndentSize);

            codeBuilder.AppendIndent().Append("A").AppendLine();
            codeBuilder.Indent();
            codeBuilder.AppendIndent().Append("B").AppendLine();
            codeBuilder.Indent();
            codeBuilder.AppendIndent().Append("C").AppendLine();
            codeBuilder.Unindent();
            codeBuilder.AppendIndent().Append("D").AppendLine();
            codeBuilder.Unindent();
            codeBuilder.AppendIndent().Append("E");

            Assert.AreEqual($"A{codeBuilder.CurrentEndOfLineString}    B{codeBuilder.CurrentEndOfLineString}        C{codeBuilder.CurrentEndOfLineString}    D{codeBuilder.CurrentEndOfLineString}E", codeBuilder.ToString());

            codeBuilder.IndentStyle = IndentStyle.Tab;
            Assert.AreEqual(IndentStyle.Tab, codeBuilder.IndentStyle);

            Assert.AreEqual($"A{codeBuilder.CurrentEndOfLineString}\tB{codeBuilder.CurrentEndOfLineString}\t\tC{codeBuilder.CurrentEndOfLineString}\tD{codeBuilder.CurrentEndOfLineString}E", codeBuilder.ToString());

            codeBuilder.IndentStyle = IndentStyle.Space;
            Assert.AreEqual(IndentStyle.Space, codeBuilder.IndentStyle);

            Assert.AreEqual($"A{codeBuilder.CurrentEndOfLineString}    B{codeBuilder.CurrentEndOfLineString}        C{codeBuilder.CurrentEndOfLineString}    D{codeBuilder.CurrentEndOfLineString}E", codeBuilder.ToString());
        }

        [TestMethod]
        public void IndentSizeTest()
        {
            var codeBuilder = new CodeBuilder()
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4
            };

            Assert.AreEqual(IndentStyle.Space, codeBuilder.IndentStyle);
            Assert.AreEqual(4, codeBuilder.IndentSize);

            codeBuilder.AppendIndent().Append("A").AppendLine();
            codeBuilder.Indent();
            codeBuilder.AppendIndent().Append("B").AppendLine();
            codeBuilder.Indent();
            codeBuilder.AppendIndent().Append("C").AppendLine();
            codeBuilder.Unindent();
            codeBuilder.AppendIndent().Append("D").AppendLine();
            codeBuilder.Unindent();
            codeBuilder.AppendIndent().Append("E");

            Assert.AreEqual($"A{codeBuilder.CurrentEndOfLineString}    B{codeBuilder.CurrentEndOfLineString}        C{codeBuilder.CurrentEndOfLineString}    D{codeBuilder.CurrentEndOfLineString}E", codeBuilder.ToString());

            codeBuilder.IndentSize = 2;
            Assert.AreEqual(2, codeBuilder.IndentSize);

            Assert.AreEqual($"A{codeBuilder.CurrentEndOfLineString}  B{codeBuilder.CurrentEndOfLineString}    C{codeBuilder.CurrentEndOfLineString}  D{codeBuilder.CurrentEndOfLineString}E", codeBuilder.ToString());

            codeBuilder.IndentSize = 8;
            Assert.AreEqual(8, codeBuilder.IndentSize);

            Assert.AreEqual($"A{codeBuilder.CurrentEndOfLineString}        B{codeBuilder.CurrentEndOfLineString}                C{codeBuilder.CurrentEndOfLineString}        D{codeBuilder.CurrentEndOfLineString}E", codeBuilder.ToString());
        }

        [TestMethod]
        public void IndentAndDeindentTest()
        {
            var codeBuilder = new CodeBuilder();

            Assert.AreEqual(0, codeBuilder.IndentDepth);

            /* Indent */

            codeBuilder.Indent();
            Assert.AreEqual(1, codeBuilder.IndentDepth);

            codeBuilder.Indent();
            Assert.AreEqual(2, codeBuilder.IndentDepth);

            /* Unindent */

            codeBuilder.Unindent();
            Assert.AreEqual(1, codeBuilder.IndentDepth);

            codeBuilder.Unindent();
            Assert.AreEqual(0, codeBuilder.IndentDepth);

            /* Indent (Max) */

            codeBuilder.IndentDepth = int.MaxValue;
            codeBuilder.Indent();
            Assert.AreEqual(int.MaxValue, codeBuilder.IndentDepth);

            /* Unindent (Min) */

            codeBuilder.IndentDepth = 0;
            codeBuilder.Unindent();
            Assert.AreEqual(0, codeBuilder.IndentDepth);
        }

        [TestMethod]
        public void GetIndentStringTest()
        {
            var codeBuilder = new CodeBuilder();

            /* 4 Spaces */

            codeBuilder.IndentStyle = IndentStyle.Space;
            codeBuilder.IndentSize = 4;
            Assert.AreEqual(string.Empty, codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Indent();
            Assert.AreEqual("    ", codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Indent();
            Assert.AreEqual("        ", codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Unindent();
            Assert.AreEqual("    ", codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Unindent();
            Assert.AreEqual(string.Empty, codeBuilder.CurrentIndentStringWithDepth);

            /* 2 Spaces */

            codeBuilder.IndentStyle = IndentStyle.Space;
            codeBuilder.IndentSize = 2;
            Assert.AreEqual(string.Empty, codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Indent();
            Assert.AreEqual("  ", codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Indent();
            Assert.AreEqual("    ", codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Unindent();
            Assert.AreEqual("  ", codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Unindent();
            Assert.AreEqual(string.Empty, codeBuilder.CurrentIndentStringWithDepth);

            /* Tab */

            codeBuilder.IndentStyle = IndentStyle.Tab;
            Assert.AreEqual(string.Empty, codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Indent();
            Assert.AreEqual("\t", codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Indent();
            Assert.AreEqual("\t\t", codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Unindent();
            Assert.AreEqual("\t", codeBuilder.CurrentIndentStringWithDepth);

            codeBuilder.Unindent();
            Assert.AreEqual(string.Empty, codeBuilder.CurrentIndentStringWithDepth);
        }

        [TestMethod]
        public void GetEndOfLineStringTest()
        {
            var codeBuilder = new CodeBuilder();

            /* CR */

            codeBuilder.EndOfLine = EndOfLine.CR;
            Assert.AreEqual("\r", codeBuilder.CurrentEndOfLineString);

            /* LF */

            codeBuilder.EndOfLine = EndOfLine.LF;
            Assert.AreEqual("\n", codeBuilder.CurrentEndOfLineString);

            /* CRLF */

            codeBuilder.EndOfLine = EndOfLine.CRLF;
            Assert.AreEqual("\r\n", codeBuilder.CurrentEndOfLineString);
        }

        [TestMethod]
        public void AppendTest()
        {
            var appendString = "test string";

            var codeBuilder = new CodeBuilder();

            codeBuilder.Append(appendString);

            var expected = appendString;
            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendLineTest()
        {
            var appendLineString = "test string";

            var codeBuilder = new CodeBuilder();

            codeBuilder.AppendLine();
            codeBuilder.AppendLine(appendLineString);
            codeBuilder.AppendLine();

            var expected = $"{codeBuilder.CurrentEndOfLineString}{appendLineString}{codeBuilder.CurrentEndOfLineString}{codeBuilder.CurrentEndOfLineString}";
            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendIndentTest()
        {
            var codeBuilder = new CodeBuilder();

            codeBuilder.AppendIndent().Append("A");
            codeBuilder.Indent();
            codeBuilder.AppendIndent().Append("B");
            codeBuilder.Indent();
            codeBuilder.AppendIndent().Append("C");
            codeBuilder.Unindent();
            codeBuilder.AppendIndent().Append("D");
            codeBuilder.Unindent();
            codeBuilder.AppendIndent().Append("E");

            var expected = $"A    B        C    DE";
            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CurlyBracketTest()
        {
            var codeBuilder = new CodeBuilder()
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
        public void AppendDocumentationCommentTest()
        {
            var codeBuilder = new CodeBuilder()
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendDocumentationComment();

            var actual = codeBuilder.ToString();

            var expected = "/// ";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendXmlCommentTagTest()
        {
            var codeBuilder = new CodeBuilder()
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendXmlCommentTag("TestTag01", XmlCommentTag.StartTag);
            codeBuilder.AppendXmlCommentTag("TestTag01", XmlCommentTag.EndTag);
            codeBuilder.AppendXmlCommentTag("TestTag02", XmlCommentTag.StartTag);
            codeBuilder.AppendXmlCommentTag("TestTag02", XmlCommentTag.EndTag);

            var actual = codeBuilder.ToString();

            var expected = "<TestTag01></TestTag01><TestTag02></TestTag02>";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendUsingDirectiveTest()
        {
            var namespaces = new string[]
            {
                "System",
                "System.Collections.Generic",
                "System.Text"
            };

            var codeBuilder = new CodeBuilder()
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            foreach (var namespaceString in namespaces)
            {
                codeBuilder.AppendUsingDirective(namespaceString).AppendLine();
            }

            var expected = $"using {namespaces[0]};\r\nusing {namespaces[1]};\r\nusing {namespaces[2]};\r\n";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AppendNamespaceDeclarationTest()
        {
            var testNamespace = "TestOrganization.TestProduct";

            var codeBuilder = new CodeBuilder()
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendNamespaceDeclaration(testNamespace);

            var expected = $"namespace {testNamespace}";

            var actual = codeBuilder.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
