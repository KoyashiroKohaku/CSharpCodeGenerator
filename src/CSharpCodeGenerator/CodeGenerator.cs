using KoyashiroKohaku.CSharpCodeGenerator.Builders;
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

            var classCodeBuilder = new ClassCodeBuilder()
            {
                IndentStyle = generateOption.IndentStyle,
                IndentSize = generateOption.IndentSize,
                EndOfLine = generateOption.EndOfLine
            };

            // using directive
            var namespaces = ClassCodeBuilder.ExtractNamespace(pocoClass.Properties).OrderBy(s => s).ToArray();
            if (namespaces.Any())
            {
                foreach (var namespaceString in namespaces)
                {
                    classCodeBuilder.AppendIndent().AppendUsingDirective(namespaceString).AppendLine();
                }

                classCodeBuilder.AppendLine();
            }

            // namespace declaration (start)
            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                classCodeBuilder.AppendIndent().AppendNamespaceDeclaration(pocoClass.Namepace).AppendLine();
                classCodeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Left).AppendLine();
                classCodeBuilder.Indent();
            }

            // class declaration (start)
            if (pocoClass.XmlComment != null)
            {
                classCodeBuilder.AppendIndent().AppendDocumentationComment().AppendXmlCommentTag("summary", XmlCommentTag.StartTag).AppendLine();

                foreach (var line in EndOfLineHelper.Split(pocoClass.XmlComment))
                {
                    classCodeBuilder.AppendIndent().AppendDocumentationComment().Append(line).AppendLine();
                }

                classCodeBuilder.AppendIndent().AppendDocumentationComment().AppendXmlCommentTag("summary", XmlCommentTag.EndTag).AppendLine();
            }
            classCodeBuilder.AppendIndent().AppendClassDeclaration(pocoClass.ClassName).AppendLine();
            classCodeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Left).AppendLine();
            classCodeBuilder.Indent();

            // Fields
            if (pocoClass.Properties.Any(p => !p.AutoImplementedProperties))
            {
                foreach (var (property, index) in pocoClass.Properties.Where(p => !p.AutoImplementedProperties).Select((p, i) => (p, i)))
                {
                    classCodeBuilder.AppendIndent().AppendField(property).AppendLine();
                }

                classCodeBuilder.AppendLine();
            }

            // Properties
            if (pocoClass.Properties.Any())
            {
                var count = pocoClass.Properties.Count;

                foreach (var (property, index) in pocoClass.Properties.Select((p, i) => (p, i)))
                {
                    if (property.XmlComment != null)
                    {
                        classCodeBuilder.AppendIndent().AppendDocumentationComment().AppendXmlCommentTag("summary", XmlCommentTag.StartTag).AppendLine();

                        foreach (var line in EndOfLineHelper.Split(property.XmlComment))
                        {
                            classCodeBuilder.AppendIndent().AppendDocumentationComment().Append(line).AppendLine();
                        }

                        classCodeBuilder.AppendIndent().AppendDocumentationComment().AppendXmlCommentTag("summary", XmlCommentTag.EndTag).AppendLine();
                    }

                    if (property.AutoImplementedProperties)
                    {
                        classCodeBuilder.AppendIndent().AppendAutoImplementedProperties(property).AppendLine();
                    }
                    else
                    {
                        classCodeBuilder.AppendIndent().AppendPropertyDeclaration(property).AppendLine();

                        var fieldName = NameConverter.Convert(property.PropertyName, property.FieldNamingConvention);

                        classCodeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Left).AppendLine();

                        classCodeBuilder.Indent();

                        classCodeBuilder.AppendIndent().Append("get => ").Append(fieldName).Append(";").AppendLine();

                        classCodeBuilder.AppendIndent().Append("set => value = ").Append(fieldName).Append(";").AppendLine();

                        classCodeBuilder.Unindent();

                        classCodeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Right).AppendLine();
                    }

                    if (count != index + 1)
                    {
                        classCodeBuilder.AppendLine();
                    }
                }
            }

            // class declaration (end)
            classCodeBuilder.Unindent();
            classCodeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Right).AppendLine();

            // namespace declaration (end)
            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                classCodeBuilder.Unindent();
                classCodeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Right).AppendLine();
            }

            return classCodeBuilder.ToString();
        }
    }
}
