using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class TypeResolverTest
    {
        [TestMethod]
        public void GetTypeAliasTest()
        {
            Assert.AreEqual("bool", TypeResolver.GetTypeAlias(typeof(System.Boolean)));
            Assert.AreEqual("byte", TypeResolver.GetTypeAlias(typeof(System.Byte)));
            Assert.AreEqual("sbyte", TypeResolver.GetTypeAlias(typeof(System.SByte)));
            Assert.AreEqual("char", TypeResolver.GetTypeAlias(typeof(System.Char)));
            Assert.AreEqual("decimal", TypeResolver.GetTypeAlias(typeof(System.Decimal)));
            Assert.AreEqual("double", TypeResolver.GetTypeAlias(typeof(System.Double)));
            Assert.AreEqual("float", TypeResolver.GetTypeAlias(typeof(System.Single)));
            Assert.AreEqual("int", TypeResolver.GetTypeAlias(typeof(System.Int32)));
            Assert.AreEqual("uint", TypeResolver.GetTypeAlias(typeof(System.UInt32)));
            Assert.AreEqual("long", TypeResolver.GetTypeAlias(typeof(System.Int64)));
            Assert.AreEqual("ulong", TypeResolver.GetTypeAlias(typeof(System.UInt64)));
            Assert.AreEqual("short", TypeResolver.GetTypeAlias(typeof(System.Int16)));
            Assert.AreEqual("ushort", TypeResolver.GetTypeAlias(typeof(System.UInt16)));
            Assert.AreEqual("string", TypeResolver.GetTypeAlias(typeof(System.String)));
            Assert.AreEqual(typeof(System.Object).ToString(), TypeResolver.GetTypeAlias(typeof(System.Object)));
        }
    }
}
