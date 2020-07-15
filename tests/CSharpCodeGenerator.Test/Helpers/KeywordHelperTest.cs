using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test.Helpers
{
    [TestClass]
    public class KeywordHelperTest
    {
        private static readonly string[] TestCase = new string[]
        {
            "abstract",
            "as",
            "base",
            "bool",
            "break",
            "byte",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "decimal",
            "default",
            "delegate",
            "do",
            "double",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "goto",
            "if",
            "implicit",
            "in",
            "int",
            "interface",
            "internal",
            "is",
            "lock",
            "long",
            "namespace",
            "new",
            "null",
            "object",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sbyte",
            "sealed",
            "short",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "unsafe",
            "ushort",
            "using",
            "virtual",
            "void",
            "volatile",
            "while"
        };

        [TestMethod]
        public void IsKeywordTest()
        {
            Assert.IsFalse(KeywordHelper.IsKeyword(null));
            Assert.IsFalse(KeywordHelper.IsKeyword(string.Empty));
            Assert.IsFalse(KeywordHelper.IsKeyword(" "));
            Assert.IsFalse(KeywordHelper.IsKeyword("abcdefg"));

            foreach (var testCase in TestCase)
            {
                Assert.IsTrue(KeywordHelper.IsKeyword(testCase));
            }
        }

        [TestMethod]
        public void GetValueTest()
        {
            Assert.ThrowsException<InvalidEnumArgumentException>(() => KeywordHelper.GetValue((Keyword)int.MinValue));

            var keywords = Enum.GetValues(typeof(Keyword)).Cast<Keyword>().ToArray();

            foreach (var keyword in keywords)
            {
                var expected = Enum.GetName(typeof(Keyword), keyword).ToLower();
                var actual = KeywordHelper.GetValue(keyword);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void TryGetValueTest()
        {
            string expected = null;
            Assert.IsFalse(KeywordHelper.TryGetValue((Keyword)int.MinValue, out var actual));
            Assert.AreEqual(expected, actual);

            var keywords = Enum.GetValues(typeof(Keyword)).Cast<Keyword>().ToArray();

            foreach (var keyword in keywords)
            {
                expected = Enum.GetName(typeof(Keyword), keyword).ToLower();
                Assert.IsTrue(KeywordHelper.TryGetValue(keyword, out actual));
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
