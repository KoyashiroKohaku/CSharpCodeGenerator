using System;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class CodeGenerator
    {
        public static string Generate(POCOClass pocoClass)
        {
            return Generate(pocoClass, new GenerateOption());
        }

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

            // using directive
            var namespaces = CodeBuilder.ExtractNamespace(pocoClass.Properties).OrderBy(s => s);
            if (namespaces.Any())
            {
                foreach (var namespaceString in namespaces)
                {
                    codeBuilder.AppendIndent().AppendUsingDirective(namespaceString).AppendLine();
                }

                codeBuilder.AppendLine();
            }

            // namespace declaration (start)
            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                codeBuilder.AppendIndent().AppendNamespaceDeclaration(pocoClass.Namepace).AppendLine();
                codeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Left).AppendLine();
                codeBuilder.Indent();
            }

            // class declaration (start)
            if (pocoClass.XmlComment != null)
            {
                codeBuilder.AppendIndent().AppendXmlComment().AppendXmlCommentTag("summary", XmlCommentTag.StartTag).AppendLine();

                foreach (var line in EndOfLineHelper.Split(pocoClass.XmlComment))
                {
                    codeBuilder.AppendIndent().AppendXmlComment().Append(line).AppendLine();
                }

                codeBuilder.AppendIndent().AppendXmlComment().AppendXmlCommentTag("summary", XmlCommentTag.EndTag).AppendLine();
            }
            codeBuilder.AppendIndent().AppendClassDeclaration(pocoClass.ClassName).AppendLine();
            codeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Left).AppendLine();
            codeBuilder.Indent();

            // Properties
            if (pocoClass.Properties.Any())
            {
                var count = pocoClass.Properties.Count;

                foreach (var (property, index) in pocoClass.Properties.Select((p, i) => (p, i)))
                {
                    if (property.XmlComment != null)
                    {
                        codeBuilder.AppendIndent().AppendXmlComment().AppendXmlCommentTag("summary", XmlCommentTag.StartTag).AppendLine();

                        foreach (var line in EndOfLineHelper.Split(property.XmlComment))
                        {
                            codeBuilder.AppendIndent().AppendXmlComment().Append(line).AppendLine();
                        }

                        codeBuilder.AppendIndent().AppendXmlComment().AppendXmlCommentTag("summary", XmlCommentTag.EndTag).AppendLine();
                    }

                    codeBuilder.AppendIndent().AppendProperty(property).AppendLine();

                    if (count != index + 1)
                    {
                        codeBuilder.AppendLine();
                    }
                }
            }

            // class declaration (end)
            codeBuilder.Unindent();
            codeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Right).AppendLine();

            // namespace declaration (end)
            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                codeBuilder.Unindent();
                codeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Right).AppendLine();
            }

            return codeBuilder.ToString();
        }
    }
}
