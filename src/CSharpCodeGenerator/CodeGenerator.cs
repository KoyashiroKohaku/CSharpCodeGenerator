using System;
using System.Linq;
using KoyashiroKohaku.CSharpCodeGenerator.Builders;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class CodeGenerator
    {
        public static string Generate(POCOClass pocoClass) => Generate(pocoClass, new GenerateOption());

        public static string Generate(POCOClass pocoClass, GenerateOption generateOption)
        {
            if (pocoClass == null)
            {
                throw new ArgumentNullException(nameof(pocoClass));
            }

            if (generateOption == null)
            {
                throw new ArgumentNullException(nameof(generateOption));
            }

            var codeBuilder = new CodeBuilder()
            {
                IndentStyle = generateOption.IndentStyle,
                IndentSize = generateOption.IndentSize,
                EndOfLine = generateOption.EndOfLine
            };

            // namespace declaration (start)
            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                codeBuilder.AppendIndent().AppendNamespaceDeclaration(pocoClass.Namepace).AppendLine();
                codeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Left).AppendLine();
                codeBuilder.DownIndent();
            }

            // class declaration (start)
            if (pocoClass.XmlComment != null)
            {
                codeBuilder.AppendIndent().AppendXmlComment(pocoClass.XmlComment).AppendLine();
            }
            codeBuilder.AppendIndent().AppendClassDeclaration(pocoClass.ClassName).AppendLine();
            codeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Left).AppendLine();
            codeBuilder.DownIndent();

            // Properties
            if (pocoClass.Properties.Any())
            {
                var count = pocoClass.Properties.Count;

                foreach (var (property, index) in pocoClass.Properties.Select((p, i) => (p, i)))
                {
                    if (property.XmlComment != null)
                    {
                        codeBuilder.AppendIndent().AppendXmlComment(property.XmlComment).AppendLine();
                    }

                    codeBuilder.AppendIndent().AppendProperty(property).AppendLine();

                    if (count != index + 1)
                    {
                        codeBuilder.AppendLine();
                    }
                }
            }

            // class declaration (end)
            codeBuilder.UpIndent();
            codeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Right).AppendLine();

            // namespace declaration (end)
            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                codeBuilder.UpIndent();
                codeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Right).AppendLine();
            }

            return codeBuilder.ToString();
        }
    }
}
