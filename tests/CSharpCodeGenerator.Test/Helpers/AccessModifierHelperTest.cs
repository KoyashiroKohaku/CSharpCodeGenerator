using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test.Helpers
{
    [TestClass]
    public class AccessModifierHelperTest
    {
        private class TestCase
        {
            public AccessModifier AccessModifier { get; set; }
            public string TestString { get; set; }
        }

        private static readonly TestCase[] TestCases = new TestCase[]
        {
            new TestCase
            {
                AccessModifier = AccessModifier.Public,
                TestString = "public",
            },
            new TestCase
            {
                AccessModifier = AccessModifier.Protected,
                TestString = "protected",
            },
            new TestCase
            {
                AccessModifier = AccessModifier.Internal,
                TestString = "internal",
            },
            new TestCase
            {
                AccessModifier = AccessModifier.ProtectedInternal,
                TestString = "protected internal"
            },
            new TestCase
            {
                AccessModifier = AccessModifier.Private,
                TestString = "private",
            },
            new TestCase
            {
                AccessModifier = AccessModifier.PrivateProtected,
                TestString = "private protected",
            }
        };

        [TestMethod]
        public void IsAccessModifierTest()
        {
            Assert.IsFalse(AccessModifierHelper.IsAccessModifier(null));
            Assert.IsFalse(AccessModifierHelper.IsAccessModifier(string.Empty));
            Assert.IsFalse(AccessModifierHelper.IsAccessModifier(" "));
            Assert.IsFalse(AccessModifierHelper.IsAccessModifier("abcdefg"));

            foreach (var testCase in TestCases)
            {
                Assert.IsTrue(AccessModifierHelper.IsAccessModifier(testCase.TestString));
            }
        }

        [TestMethod]
        public void GetValueTest()
        {
            Assert.ThrowsException<InvalidEnumArgumentException>(() => AccessModifierHelper.GetValue((AccessModifier)int.MinValue));

            foreach (var testCase in TestCases)
            {
                var expected = testCase.TestString;
                var actual = AccessModifierHelper.GetValue(testCase.AccessModifier);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void TryGetValueTest()
        {
            string expected = default;
            Assert.IsFalse(AccessModifierHelper.TryGetValue((AccessModifier)int.MinValue, out var actual));
            Assert.AreEqual(expected, actual);

            foreach (var testCase in TestCases)
            {
                expected = testCase.TestString;
                Assert.IsTrue(AccessModifierHelper.TryGetValue(testCase.AccessModifier, out actual));
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
