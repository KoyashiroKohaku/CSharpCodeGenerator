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
            Assert.AreEqual("bool", TypeResolver.GetTypeAlias(typeof(bool)));
            Assert.AreEqual("byte", TypeResolver.GetTypeAlias(typeof(byte)));
            Assert.AreEqual("sbyte", TypeResolver.GetTypeAlias(typeof(sbyte)));
            Assert.AreEqual("char", TypeResolver.GetTypeAlias(typeof(char)));
            Assert.AreEqual("decimal", TypeResolver.GetTypeAlias(typeof(decimal)));
            Assert.AreEqual("double", TypeResolver.GetTypeAlias(typeof(double)));
            Assert.AreEqual("float", TypeResolver.GetTypeAlias(typeof(float)));
            Assert.AreEqual("int", TypeResolver.GetTypeAlias(typeof(int)));
            Assert.AreEqual("uint", TypeResolver.GetTypeAlias(typeof(uint)));
            Assert.AreEqual("long", TypeResolver.GetTypeAlias(typeof(long)));
            Assert.AreEqual("ulong", TypeResolver.GetTypeAlias(typeof(ulong)));
            Assert.AreEqual("short", TypeResolver.GetTypeAlias(typeof(short)));
            Assert.AreEqual("ushort", TypeResolver.GetTypeAlias(typeof(ushort)));
            Assert.AreEqual("string", TypeResolver.GetTypeAlias(typeof(string)));
            Assert.AreEqual("int[]", TypeResolver.GetTypeAlias(typeof(int[])));
            Assert.AreEqual("string[]", TypeResolver.GetTypeAlias(typeof(string[])));
            Assert.AreEqual("List<int>", TypeResolver.GetTypeAlias(typeof(List<int>)));
            Assert.AreEqual("List<string>", TypeResolver.GetTypeAlias(typeof(List<string>)));
            Assert.AreEqual("Dictionary<int, string>", TypeResolver.GetTypeAlias(typeof(Dictionary<int, string>)));
            Assert.AreEqual("object", TypeResolver.GetTypeAlias(typeof(object)));
        }
    }
}
