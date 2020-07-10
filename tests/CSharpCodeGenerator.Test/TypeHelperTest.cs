using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using KoyashiroKohaku.CSharpCodeGenerator.Helpers;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class TypeHelperTest
    {
        [TestMethod]
        public void GetTypeStringTest()
        {
            Assert.AreEqual("bool", TypeHelper.GetTypeString(typeof(bool)));
            Assert.AreEqual("byte", TypeHelper.GetTypeString(typeof(byte)));
            Assert.AreEqual("sbyte", TypeHelper.GetTypeString(typeof(sbyte)));
            Assert.AreEqual("char", TypeHelper.GetTypeString(typeof(char)));
            Assert.AreEqual("decimal", TypeHelper.GetTypeString(typeof(decimal)));
            Assert.AreEqual("double", TypeHelper.GetTypeString(typeof(double)));
            Assert.AreEqual("float", TypeHelper.GetTypeString(typeof(float)));
            Assert.AreEqual("int", TypeHelper.GetTypeString(typeof(int)));
            Assert.AreEqual("uint", TypeHelper.GetTypeString(typeof(uint)));
            Assert.AreEqual("long", TypeHelper.GetTypeString(typeof(long)));
            Assert.AreEqual("ulong", TypeHelper.GetTypeString(typeof(ulong)));
            Assert.AreEqual("short", TypeHelper.GetTypeString(typeof(short)));
            Assert.AreEqual("ushort", TypeHelper.GetTypeString(typeof(ushort)));
            Assert.AreEqual("string", TypeHelper.GetTypeString(typeof(string)));
            Assert.AreEqual("object", TypeHelper.GetTypeString(typeof(object)));
            Assert.AreEqual("void", TypeHelper.GetTypeString(typeof(void)));
            Assert.AreEqual("int[]", TypeHelper.GetTypeString(typeof(int[])));
            Assert.AreEqual("string[]", TypeHelper.GetTypeString(typeof(string[])));
            Assert.AreEqual("List<int>", TypeHelper.GetTypeString(typeof(List<int>)));
            Assert.AreEqual("List<string>", TypeHelper.GetTypeString(typeof(List<string>)));
            Assert.AreEqual("Dictionary<int, string>", TypeHelper.GetTypeString(typeof(Dictionary<int, string>)));
        }

        [TestMethod]
        public void GetTypeAliasNameTest()
        {
            try
            {
                TypeHelper.GetTypeAliasName(typeof(List<>));

                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Specified argument was out of the range of valid values. (Parameter 'type')", e.Message);
            }

            Assert.AreEqual("bool", TypeHelper.GetTypeAliasName(typeof(bool)));
            Assert.AreEqual("byte", TypeHelper.GetTypeAliasName(typeof(byte)));
            Assert.AreEqual("sbyte", TypeHelper.GetTypeAliasName(typeof(sbyte)));
            Assert.AreEqual("char", TypeHelper.GetTypeAliasName(typeof(char)));
            Assert.AreEqual("decimal", TypeHelper.GetTypeAliasName(typeof(decimal)));
            Assert.AreEqual("double", TypeHelper.GetTypeAliasName(typeof(double)));
            Assert.AreEqual("float", TypeHelper.GetTypeAliasName(typeof(float)));
            Assert.AreEqual("int", TypeHelper.GetTypeAliasName(typeof(int)));
            Assert.AreEqual("uint", TypeHelper.GetTypeAliasName(typeof(uint)));
            Assert.AreEqual("long", TypeHelper.GetTypeAliasName(typeof(long)));
            Assert.AreEqual("ulong", TypeHelper.GetTypeAliasName(typeof(ulong)));
            Assert.AreEqual("short", TypeHelper.GetTypeAliasName(typeof(short)));
            Assert.AreEqual("ushort", TypeHelper.GetTypeAliasName(typeof(ushort)));
            Assert.AreEqual("string", TypeHelper.GetTypeAliasName(typeof(string)));
            Assert.AreEqual("object", TypeHelper.GetTypeAliasName(typeof(object)));
            Assert.AreEqual("void", TypeHelper.GetTypeAliasName(typeof(void)));
        }

        [TestMethod]
        public void TryGetTypeAliasNameTest()
        {
            Assert.IsFalse(TypeHelper.TryGetTypeAliasName(null, out string actual));
            Assert.AreEqual(null, actual);

            Assert.IsFalse(TypeHelper.TryGetTypeAliasName(typeof(List<>), out actual));
            Assert.AreEqual(null, actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(bool), out actual));
            Assert.AreEqual("bool", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(byte), out actual));
            Assert.AreEqual("byte", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(sbyte), out actual));
            Assert.AreEqual("sbyte", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(char), out actual));
            Assert.AreEqual("char", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(decimal), out actual));
            Assert.AreEqual("decimal", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(double), out actual));
            Assert.AreEqual("double", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(float), out actual));
            Assert.AreEqual("float", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(int), out actual));
            Assert.AreEqual("int", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(uint), out actual));
            Assert.AreEqual("uint", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(long), out actual));
            Assert.AreEqual("long", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(ulong), out actual));
            Assert.AreEqual("ulong", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(short), out actual));
            Assert.AreEqual("short", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(ushort), out actual));
            Assert.AreEqual("ushort", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(string), out actual));
            Assert.AreEqual("string", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(object), out actual));
            Assert.AreEqual("object", actual);

            Assert.IsTrue(TypeHelper.TryGetTypeAliasName(typeof(void), out actual));
            Assert.AreEqual("void", actual);
        }

        [TestMethod]
        public void ExistsTypeAliasTest()
        {
            Assert.IsFalse(TypeHelper.ExistsTypeAlias(null));
            Assert.IsFalse(TypeHelper.ExistsTypeAlias(typeof(List<>)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(sbyte)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(char)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(decimal)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(double)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(float)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(int)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(uint)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(long)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(ulong)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(short)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(ushort)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(string)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(object)));
            Assert.IsTrue(TypeHelper.ExistsTypeAlias(typeof(void)));
        }
    }
}
