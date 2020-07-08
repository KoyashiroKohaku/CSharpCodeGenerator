using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class TypeResolverTest
    {
        [TestMethod]
        public void GetTypeAliasTest()
        {
            Assert.AreEqual("bool", TypeResolver.GetTypeString(typeof(bool)));
            Assert.AreEqual("byte", TypeResolver.GetTypeString(typeof(byte)));
            Assert.AreEqual("sbyte", TypeResolver.GetTypeString(typeof(sbyte)));
            Assert.AreEqual("char", TypeResolver.GetTypeString(typeof(char)));
            Assert.AreEqual("decimal", TypeResolver.GetTypeString(typeof(decimal)));
            Assert.AreEqual("double", TypeResolver.GetTypeString(typeof(double)));
            Assert.AreEqual("float", TypeResolver.GetTypeString(typeof(float)));
            Assert.AreEqual("int", TypeResolver.GetTypeString(typeof(int)));
            Assert.AreEqual("uint", TypeResolver.GetTypeString(typeof(uint)));
            Assert.AreEqual("long", TypeResolver.GetTypeString(typeof(long)));
            Assert.AreEqual("ulong", TypeResolver.GetTypeString(typeof(ulong)));
            Assert.AreEqual("short", TypeResolver.GetTypeString(typeof(short)));
            Assert.AreEqual("ushort", TypeResolver.GetTypeString(typeof(ushort)));
            Assert.AreEqual("string", TypeResolver.GetTypeString(typeof(string)));
            Assert.AreEqual("object", TypeResolver.GetTypeString(typeof(object)));
            Assert.AreEqual("void", TypeResolver.GetTypeString(typeof(void)));
            Assert.AreEqual("int[]", TypeResolver.GetTypeString(typeof(int[])));
            Assert.AreEqual("string[]", TypeResolver.GetTypeString(typeof(string[])));
            Assert.AreEqual("List<int>", TypeResolver.GetTypeString(typeof(List<int>)));
            Assert.AreEqual("List<string>", TypeResolver.GetTypeString(typeof(List<string>)));
            Assert.AreEqual("Dictionary<int, string>", TypeResolver.GetTypeString(typeof(Dictionary<int, string>)));
        }
    }
}
