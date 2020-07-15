using KoyashiroKohaku.CSharpCodeGenerator.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test.Builders
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            int size = Marshal.SizeOf<Token>();
            Assert.IsTrue(size <= 24);

            Assert.IsTrue(Token.Abstract.Equals(Token.Abstract));
            Assert.IsTrue(Token.Abstract == Token.Abstract);
            Assert.IsFalse(Token.Abstract != Token.Abstract);

            Assert.AreEqual("abstract", new Token("abstract").GetAnyString());
            Assert.IsFalse(Token.Abstract.Equals(new Token("abstract")));
            Assert.IsFalse(Token.Abstract == new Token("abstract"));
            Assert.IsTrue(Token.Abstract != new Token("abstract"));

            Assert.IsTrue(new Token("abstract").Equals(new Token("abstract")));
            Assert.IsTrue(new Token("abstract") == new Token("abstract"));
            Assert.IsFalse(new Token("abstract") != new Token("abstract"));
        }
    }
}
