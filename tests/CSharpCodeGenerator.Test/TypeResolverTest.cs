using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class TypeResolverTest
    {
        [TestMethod]
        public void GetTypeString()
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

        [TestMethod]
        public void GetTypeAliasNameTest()
        {
            try
            {
                TypeResolver.GetTypeAliasName(typeof(List<>));

                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Specified argument was out of the range of valid values. (Parameter 'type')", e.Message);
            }

            Assert.AreEqual("bool", TypeResolver.GetTypeAliasName(typeof(bool)));
            Assert.AreEqual("byte", TypeResolver.GetTypeAliasName(typeof(byte)));
            Assert.AreEqual("sbyte", TypeResolver.GetTypeAliasName(typeof(sbyte)));
            Assert.AreEqual("char", TypeResolver.GetTypeAliasName(typeof(char)));
            Assert.AreEqual("decimal", TypeResolver.GetTypeAliasName(typeof(decimal)));
            Assert.AreEqual("double", TypeResolver.GetTypeAliasName(typeof(double)));
            Assert.AreEqual("float", TypeResolver.GetTypeAliasName(typeof(float)));
            Assert.AreEqual("int", TypeResolver.GetTypeAliasName(typeof(int)));
            Assert.AreEqual("uint", TypeResolver.GetTypeAliasName(typeof(uint)));
            Assert.AreEqual("long", TypeResolver.GetTypeAliasName(typeof(long)));
            Assert.AreEqual("ulong", TypeResolver.GetTypeAliasName(typeof(ulong)));
            Assert.AreEqual("short", TypeResolver.GetTypeAliasName(typeof(short)));
            Assert.AreEqual("ushort", TypeResolver.GetTypeAliasName(typeof(ushort)));
            Assert.AreEqual("string", TypeResolver.GetTypeAliasName(typeof(string)));
            Assert.AreEqual("object", TypeResolver.GetTypeAliasName(typeof(object)));
            Assert.AreEqual("void", TypeResolver.GetTypeAliasName(typeof(void)));
        }

        [TestMethod]
        public void TryGetTypeAliasName()
        {
            Assert.IsFalse(TypeResolver.TryGetTypeAliasName(null, out string actual));
            Assert.AreEqual(string.Empty, actual);

            Assert.IsFalse(TypeResolver.TryGetTypeAliasName(typeof(List<>), out actual));
            Assert.AreEqual(string.Empty, actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(bool), out actual));
            Assert.AreEqual("bool", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(byte), out actual));
            Assert.AreEqual("byte", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(sbyte), out actual));
            Assert.AreEqual("sbyte", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(char), out actual));
            Assert.AreEqual("char", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(decimal), out actual));
            Assert.AreEqual("decimal", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(double), out actual));
            Assert.AreEqual("double", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(float), out actual));
            Assert.AreEqual("float", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(int), out actual));
            Assert.AreEqual("int", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(uint), out actual));
            Assert.AreEqual("uint", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(long), out actual));
            Assert.AreEqual("long", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(ulong), out actual));
            Assert.AreEqual("ulong", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(short), out actual));
            Assert.AreEqual("short", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(ushort), out actual));
            Assert.AreEqual("ushort", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(string), out actual));
            Assert.AreEqual("string", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(object), out actual));
            Assert.AreEqual("object", actual);

            Assert.IsTrue(TypeResolver.TryGetTypeAliasName(typeof(void), out actual));
            Assert.AreEqual("void", actual);
        }

        [TestMethod]
        public void ExistsTypeAliasTest()
        {
            Assert.IsFalse(TypeResolver.ExistsTypeAlias(null));
            Assert.IsFalse(TypeResolver.ExistsTypeAlias(typeof(List<>)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(sbyte)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(char)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(decimal)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(double)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(float)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(int)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(uint)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(long)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(ulong)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(short)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(ushort)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(string)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(object)));
            Assert.IsTrue(TypeResolver.ExistsTypeAlias(typeof(void)));
        }
    }
}
