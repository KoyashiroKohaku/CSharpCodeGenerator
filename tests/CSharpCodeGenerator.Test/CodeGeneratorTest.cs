using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KoyashiroKohaku.CSharpCodeGenerator.Test
{
    [TestClass]
    public class CodeGeneratorTest
    {
        [TestMethod]
        public void GenerateTest()
        {
            var classSetting = new ClassSetting("TestClass")
            {
                Namepace = "TestOrganization.TestProduct",
                XmlComment = "TestClass XML Comment"
            };

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty01", typeof(int))
            {
                XmlComment = "TestProperty01 XML Comment"
            });

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty02", typeof(string))
            {
                XmlComment = "TestProperty02 XML Comment"
            });

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty03", typeof(int[]))
            {
                XmlComment = "TestProperty03 XML Comment"
            });

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty04", typeof(List<string>))
            {
                XmlComment = "TestProperty04 XML Comment"
            });

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty05", typeof(DateTime))
            {
                XmlComment = "TestProperty05 XML Comment"
            });

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty06", typeof(int))
            {
                XmlComment = "TestProperty06 XML Comment",
                Nullable = true
            });

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty07", typeof(string))
            {
                XmlComment = "TestProperty07 XML Comment",
                Nullable = true
            });

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty08", typeof(int))
            {
                XmlComment = "TestProperty08 XML Comment",
                AutoImplementedProperties = false
            });

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty09", typeof(int))
            {
                XmlComment = "TestProperty09 XML Comment",
                AutoImplementedProperties = false,
                FieldNamingConvention = FieldNamingConvention.CamelWithUnderscoreInThePrefix
            });

            classSetting.PropertySettings.Add(new PropertySetting("TestProperty10", typeof(int))
            {
                XmlComment = "TestProperty10 XML Comment",
                AutoImplementedProperties = false,
                FieldNamingConvention = FieldNamingConvention.Camel
            });

            var expected = @"using System;
using System.Collections.Generic;

namespace TestOrganization.TestProduct
{
    /// <summary>
    /// TestClass XML Comment
    /// </summary>
    public class TestClass
    {
        private int _testProperty08;
        private int _testProperty09;
        private int testProperty10;

        /// <summary>
        /// TestProperty01 XML Comment
        /// </summary>
        public int TestProperty01 { get; set; }

        /// <summary>
        /// TestProperty02 XML Comment
        /// </summary>
        public string TestProperty02 { get; set; }

        /// <summary>
        /// TestProperty03 XML Comment
        /// </summary>
        public int[] TestProperty03 { get; set; }

        /// <summary>
        /// TestProperty04 XML Comment
        /// </summary>
        public List<string> TestProperty04 { get; set; }

        /// <summary>
        /// TestProperty05 XML Comment
        /// </summary>
        public DateTime TestProperty05 { get; set; }

        /// <summary>
        /// TestProperty06 XML Comment
        /// </summary>
        public int? TestProperty06 { get; set; }

        /// <summary>
        /// TestProperty07 XML Comment
        /// </summary>
        public string? TestProperty07 { get; set; }

        /// <summary>
        /// TestProperty08 XML Comment
        /// </summary>
        public int TestProperty08
        {
            get => _testProperty08;
            set => value = _testProperty08;
        }

        /// <summary>
        /// TestProperty09 XML Comment
        /// </summary>
        public int TestProperty09
        {
            get => _testProperty09;
            set => value = _testProperty09;
        }

        /// <summary>
        /// TestProperty10 XML Comment
        /// </summary>
        public int TestProperty10
        {
            get => testProperty10;
            set => value = testProperty10;
        }
    }
}
";

            var actual = CodeGenerator.Generate(classSetting);

            Assert.AreEqual(expected, actual);
        }
    }
}
