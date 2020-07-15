using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test.Helpers
{
    [TestClass]
    public class ASCIICharHelperTest
    {
        private class TestCase
        {
            public ASCIIChar ASCIIChar { get; set; }
            public char TestChar { get; set; }
            public string TestString => TestChar.ToString();
            public bool IsASCIIChar { get; set; }
        }

        private static readonly TestCase[] TestCases = new TestCase[]
        {
            new TestCase
            {
                ASCIIChar = ASCIIChar.Space, TestChar = ' ', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.ExclamationMark, TestChar = '!', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.QuotationMark, TestChar = '"', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.NumberSign, TestChar = '#', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.DollarSign, TestChar = '$', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.PercentSign, TestChar = '%', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Ampersand, TestChar = '&', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Apostrophe, TestChar = '\'', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.LeftParentheses, TestChar = '(', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.RightParentheses, TestChar = ')', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Asterisk, TestChar = '*', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.PlusSign, TestChar = '+', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Commma, TestChar = ',', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.HyphenMinus, TestChar = '-', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.FullStop, TestChar = '.', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Slash, TestChar = '/', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Colon, TestChar = ':', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Semicolon, TestChar = ';', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.LessThanSign, TestChar = '<', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.EqualSign, TestChar = '=', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.GreaterThanSign, TestChar = '>', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.QuestionMark, TestChar = '?', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.AtSign, TestChar = '@', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.LeftSquareBracket, TestChar = '[', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Backslash, TestChar = '\\', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.RightSquareBracket, TestChar = ']', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.CircumflexAccent, TestChar = '^', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.LowLine, TestChar = '_', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.GraveAccent, TestChar = '`', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.LeftCurlyBracket, TestChar = '{', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.VerticalBar, TestChar = '|', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.RightCurlyBracket, TestChar = '}', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Space, TestChar = ' ', IsASCIIChar = true
            },
            new TestCase
            {
                ASCIIChar = ASCIIChar.Tilde, TestChar = '~', IsASCIIChar = true
            }
        };

        [TestMethod]
        public void IsASCIICharTest()
        {
            Assert.IsFalse(ASCIICharHelper.IsASCIIChar(null));
            Assert.IsFalse(ASCIICharHelper.IsASCIIChar(string.Empty));
            Assert.IsTrue(ASCIICharHelper.IsASCIIChar(" "));
            Assert.IsFalse(ASCIICharHelper.IsASCIIChar("abcdefg"));
            Assert.IsTrue(ASCIICharHelper.IsASCIIChar(' '));
            Assert.IsFalse(ASCIICharHelper.IsASCIIChar('a'));
            Assert.IsFalse(ASCIICharHelper.IsASCIIChar('b'));
            Assert.IsFalse(ASCIICharHelper.IsASCIIChar('c'));

            foreach (var testCase in TestCases)
            {
                Assert.IsTrue(ASCIICharHelper.IsASCIIChar(testCase.TestChar));
                Assert.IsTrue(ASCIICharHelper.IsASCIIChar(testCase.TestString));
            }
        }

        [TestMethod]
        public void GetValueTest()
        {
            Assert.ThrowsException<InvalidEnumArgumentException>(() => ASCIICharHelper.GetValue((ASCIIChar)int.MinValue));

            foreach (var testCase in TestCases)
            {
                var expected = testCase.TestChar;
                var actual = ASCIICharHelper.GetValue(testCase.ASCIIChar);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void TryGetValueTest()
        {
            char expected = default;
            Assert.IsFalse(ASCIICharHelper.TryGetValue((ASCIIChar)int.MinValue, out var actual));
            Assert.AreEqual(expected, actual);

            foreach (var testCase in TestCases)
            {
                expected = testCase.TestChar;
                Assert.IsTrue(ASCIICharHelper.TryGetValue(testCase.ASCIIChar, out actual));
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
