using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test.Helpers
{
    [TestClass]
    public class ContextualKeywordHelperTest
    {
        private static readonly string[] TestCase = new string[]
        {
            "add",
            "alias",
            "ascending",
            "async",
            "await",
            "by",
            "descending",
            "dynamic",
            "equals",
            "from",
            "get",
            "global",
            "group",
            "into",
            "join",
            "let",
            "nameof",
            "on",
            "orderby",
            "partial",
            "partial",
            "remove",
            "select",
            "set",
            "unmanaged",
            "value",
            "var",
            "when",
            "where",
            "where",
            "yield"
        };

        [TestMethod]
        public void IsContextualKeywordTest()
        {
            Assert.IsFalse(ContextualKeywordHelper.IsContextualKeyword(null));
            Assert.IsFalse(ContextualKeywordHelper.IsContextualKeyword(string.Empty));
            Assert.IsFalse(ContextualKeywordHelper.IsContextualKeyword(" "));
            Assert.IsFalse(ContextualKeywordHelper.IsContextualKeyword("abcdefg"));

            foreach (var testCase in TestCase)
            {
                Assert.IsTrue(ContextualKeywordHelper.IsContextualKeyword(testCase));
            }
        }

        [TestMethod]
        public void GetValueTest()
        {
            Assert.ThrowsException<InvalidEnumArgumentException>(() => ContextualKeywordHelper.GetValue((ContextualKeyword)int.MinValue));

            var ContextualKeywords = Enum.GetValues(typeof(ContextualKeyword)).Cast<ContextualKeyword>().ToArray();

            foreach (var ContextualKeyword in ContextualKeywords)
            {
                var expected = NameHelper.Split(Enum.GetName(typeof(ContextualKeyword), ContextualKeyword)).First();
                var actual = ContextualKeywordHelper.GetValue(ContextualKeyword);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void TryGetValueTest()
        {
            string expected = null;
            Assert.IsFalse(ContextualKeywordHelper.TryGetValue((ContextualKeyword)int.MinValue, out var actual));
            Assert.AreEqual(expected, actual);

            var ContextualKeywords = Enum.GetValues(typeof(ContextualKeyword)).Cast<ContextualKeyword>().ToArray();

            foreach (var ContextualKeyword in ContextualKeywords)
            {
                expected = NameHelper.Split(Enum.GetName(typeof(ContextualKeyword), ContextualKeyword)).First();
                Assert.IsTrue(ContextualKeywordHelper.TryGetValue(ContextualKeyword, out actual));
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
