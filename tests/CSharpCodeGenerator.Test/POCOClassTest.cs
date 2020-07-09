using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class POCOClassTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            try
            {
                new POCOClass(null);

                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'className')", e.Message);
            }

            var pocoClass = new POCOClass("TestClass");

            Assert.AreEqual("TestClass", pocoClass.ClassName);
        }
    }
}
