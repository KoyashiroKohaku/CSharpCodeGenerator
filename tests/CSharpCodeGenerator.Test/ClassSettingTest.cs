using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class ClassSettingTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            try
            {
                new ClassSetting(null);

                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'className')", e.Message);
            }

            var classSetting = new ClassSetting("TestClass");

            Assert.AreEqual("TestClass", classSetting.ClassName);
        }
    }
}
