using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test.Helpers
{
    [TestClass]
    public class NameHelperTest
    {
        private enum NameCase
        {
            LowerCamelCase,
            UpperCamelCase,
            LowerSnakeCase,
            UpperSnakeCase,
            None
        }

        private class TestCase
        {
            public string TestString { get; set; }
            public NameCase NameCase { get; set; }
            public string[] SplitResult { get; set; }
        }

        private static readonly TestCase[] TestCases = new TestCase[]
        {
            new TestCase
            {
                TestString = "abcDefgH1jkLmn0",
                NameCase = NameCase.LowerCamelCase,
                SplitResult = new string[] { "abc", "defg", "h1jk", "lmn0" }
            },
            new TestCase
            {
                TestString = "AbcDefgH1jkLmn0",
                NameCase = NameCase.UpperCamelCase,
                SplitResult = new string[] { "abc", "defg", "h1jk", "lmn0" }
            },
            new TestCase
            {
                TestString = "abc_defg_h1jk_lmn0",
                NameCase = NameCase.LowerSnakeCase,
                SplitResult = new string[] { "abc", "defg", "h1jk", "lmn0" }
            },
            new TestCase
            {
                TestString = "ABC_DEFG_H1JK_LMN0",
                NameCase = NameCase.UpperSnakeCase,
                SplitResult = new string[] { "abc", "defg", "h1jk", "lmn0" }
            },
            new TestCase
            {
                TestString = null,
                NameCase = NameCase.None,
                SplitResult = null
            },
            new TestCase
            {
                TestString = string.Empty,
                NameCase = NameCase.None,
                SplitResult = null
            },
            new TestCase
            {
                TestString = " ",
                NameCase = NameCase.None,
                SplitResult = null
            },
            new TestCase
            {
                TestString = "123aBcdeFg",
                NameCase = NameCase.None,
                SplitResult = null
            },
            new TestCase
            {
                TestString = "あいうえお",
                NameCase = NameCase.None,
                SplitResult = null
            },
        };

        [TestMethod]
        public void IsCamelTest()
        {
            foreach (var testCase in TestCases)
            {
                var expected = testCase.NameCase == NameCase.LowerCamelCase || testCase.NameCase == NameCase.UpperCamelCase;
                var actual = NameHelper.IsCamel(testCase.TestString);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void IsLowerCamelTest()
        {
            foreach (var testCase in TestCases)
            {
                var expected = testCase.NameCase == NameCase.LowerCamelCase;
                var actual = NameHelper.IsLowerCamel(testCase.TestString);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void IsUpperCamelTest()
        {
            foreach (var testCase in TestCases)
            {
                var expected = testCase.NameCase == NameCase.UpperCamelCase;
                var actual = NameHelper.IsUpperCamel(testCase.TestString);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void IsSnakeTest()
        {
            foreach (var testCase in TestCases)
            {
                var expected = testCase.NameCase == NameCase.LowerSnakeCase || testCase.NameCase == NameCase.UpperSnakeCase;
                var actual = NameHelper.IsSnake(testCase.TestString);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void IsLowerSnakeTest()
        {
            foreach (var testCase in TestCases)
            {
                var expected = testCase.NameCase == NameCase.LowerSnakeCase;
                var actual = NameHelper.IsLowerSnake(testCase.TestString);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void IsUpperSnakeTest()
        {
            foreach (var testCase in TestCases)
            {
                var expected = testCase.NameCase == NameCase.UpperSnakeCase;
                var actual = NameHelper.IsUpperSnake(testCase.TestString);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void SplitTest()
        {
            foreach (var testCase in TestCases)
            {
                if (testCase.NameCase == NameCase.None)
                {
                    if (testCase.TestString is null)
                    {
                        Assert.ThrowsException<ArgumentNullException>(() => NameHelper.Split(testCase.TestString));
                    }
                    else
                    {
                        Assert.ThrowsException<ArgumentException>(() => NameHelper.Split(testCase.TestString));
                    }
                }
                else
                {
                    var expected = testCase.SplitResult;
                    var actual = NameHelper.Split(testCase.TestString);
                    CollectionAssert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod]
        public void SplitCamelTest()
        {
            foreach (var testCase in TestCases.Where(tc => !(tc.NameCase == NameCase.LowerSnakeCase || tc.NameCase == NameCase.UpperSnakeCase)))
            {
                if (testCase.NameCase == NameCase.None)
                {
                    if (testCase.TestString is null)
                    {
                        Assert.ThrowsException<ArgumentNullException>(() => NameHelper.Split(testCase.TestString));
                    }
                    else
                    {
                        Assert.ThrowsException<ArgumentException>(() => NameHelper.Split(testCase.TestString));
                    }
                }
                else
                {
                    var expected = testCase.SplitResult;
                    var actual = NameHelper.SplitCamel(testCase.TestString);
                    CollectionAssert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod]
        public void SplitSnakeTest()
        {
            foreach (var testCase in TestCases.Where(tc => !(tc.NameCase == NameCase.LowerCamelCase || tc.NameCase == NameCase.UpperCamelCase)))
            {
                if (testCase.NameCase == NameCase.None)
                {
                    if (testCase.TestString is null)
                    {
                        Assert.ThrowsException<ArgumentNullException>(() => NameHelper.Split(testCase.TestString));
                    }
                    else
                    {
                        Assert.ThrowsException<ArgumentException>(() => NameHelper.Split(testCase.TestString));
                    }
                }
                else
                {
                    var expected = testCase.SplitResult;
                    var actual = NameHelper.SplitSnake(testCase.TestString);
                    CollectionAssert.AreEqual(expected, actual);
                }
            }
        }
    }
}
