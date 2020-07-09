using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class NameConverterTest
    {
        [TestMethod]
        public void ToCamelTest()
        {
            try
            {
                NameConverter.ToCamel(null);

                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'input')", e.Message);
            }

            Assert.AreEqual(string.Empty, NameConverter.ToCamel(string.Empty));
            Assert.AreEqual("a", NameConverter.ToCamel("A"));
            Assert.AreEqual("testName", NameConverter.ToCamel("TestName"));
        }

        [TestMethod]
        public void ToCamelWithUnderscoreInThePrefixTest()
        {
            try
            {
                NameConverter.ToCamelWithUnderscoreInThePrefix(null);

                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'input')", e.Message);
            }

            Assert.AreEqual(string.Empty, NameConverter.ToCamelWithUnderscoreInThePrefix(string.Empty));
            Assert.AreEqual("_a", NameConverter.ToCamelWithUnderscoreInThePrefix("A"));
            Assert.AreEqual("_testName", NameConverter.ToCamelWithUnderscoreInThePrefix("TestName"));
        }
    }
}
