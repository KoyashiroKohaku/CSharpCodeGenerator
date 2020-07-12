using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test.Helpers
{
    [TestClass]
    public class AccessModifierHelperTest
    {
        private static readonly string[] TestCase = new string[]
        {
            "public",
            "protected",
            "internal",
            "protected internal",
            "private",
            "private protected"
        };

        [TestMethod]
        public void IsAccessModifierTest()
        {
            Assert.IsFalse(AccessModifierHelper.IsAccessModifier(null));
            Assert.IsFalse(AccessModifierHelper.IsAccessModifier(string.Empty));
            Assert.IsFalse(AccessModifierHelper.IsAccessModifier(" "));
            Assert.IsFalse(AccessModifierHelper.IsAccessModifier("abcdefg"));

            foreach (var testCase in TestCase)
            {
                Assert.IsTrue(AccessModifierHelper.IsAccessModifier(testCase));
            }
        }

        [TestMethod]
        public void GetValueTest()
        {
            Assert.ThrowsException<InvalidEnumArgumentException>(() => AccessModifierHelper.GetValue((AccessModifier)int.MinValue));

            var AccessModifiers = Enum.GetValues(typeof(AccessModifier)).Cast<AccessModifier>().ToArray();

            foreach (var AccessModifier in AccessModifiers)
            {
                var expected = Enum.GetName(typeof(AccessModifier), AccessModifier).Split(' ').First().ToLower();
                var actual = AccessModifierHelper.GetValue(AccessModifier);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void TryGetValueTest()
        {
            string expected = null;
            Assert.IsFalse(AccessModifierHelper.TryGetValue((AccessModifier)int.MinValue, out var actual));
            Assert.AreEqual(expected, actual);

            var AccessModifiers = Enum.GetValues(typeof(AccessModifier)).Cast<AccessModifier>().ToArray();

            foreach (var AccessModifier in AccessModifiers)
            {
                expected = Enum.GetName(typeof(AccessModifier), AccessModifier).Split(' ').First().ToLower();
                Assert.IsTrue(AccessModifierHelper.TryGetValue(AccessModifier, out actual));
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
