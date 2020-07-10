using KoyashiroKohaku.CSharpCodeGenerator.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class CodePartTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            int size = Marshal.SizeOf<CodeWord>();
            Assert.IsTrue(size <= 24);

            Assert.IsTrue(CodeWord.Abstract.Equals(CodeWord.Abstract));
            Assert.IsTrue(CodeWord.Abstract == CodeWord.Abstract);
            Assert.IsFalse(CodeWord.Abstract != CodeWord.Abstract);

            Assert.AreEqual("abstract", new CodeWord("abstract").GetAnyString());
            Assert.IsFalse(CodeWord.Abstract.Equals(new CodeWord("abstract")));
            Assert.IsFalse(CodeWord.Abstract == new CodeWord("abstract"));
            Assert.IsTrue(CodeWord.Abstract != new CodeWord("abstract"));

            Assert.IsTrue(new CodeWord("abstract").Equals(new CodeWord("abstract")));
            Assert.IsTrue(new CodeWord("abstract") == new CodeWord("abstract"));
            Assert.IsFalse(new CodeWord("abstract") != new CodeWord("abstract"));
        }
    }
}
