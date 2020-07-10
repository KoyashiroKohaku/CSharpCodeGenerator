using KoyashiroKohaku.CSharpCodeGenerator.Builders;
using System;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class CodeGenerator
    {
        public static string Generate(ClassSetting classSetting)
        {
            return Generate(classSetting, new GenerateOption());
        }

        public static string Generate(ClassSetting classSetting, GenerateOption generateOption)
        {
            if (classSetting == null)
            {
                throw new ArgumentNullException(nameof(classSetting));
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
            var namespaces = ClassCodeBuilder.ExtractNamespace(classSetting.PropertySettings).OrderBy(s => s).ToArray();
            if (namespaces.Any())
            {
                foreach (var namespaceString in namespaces)
                {
                    classCodeBuilder.AppendIndent().AppendUsingDirective(namespaceString).AppendLine();
                }

                classCodeBuilder.AppendLine();
            }

            // namespace declaration (start)
            if (!string.IsNullOrEmpty(classSetting.Namepace))
            {
                classCodeBuilder.AppendIndent().AppendNamespaceDeclaration(classSetting.Namepace).AppendLine();
                classCodeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Left).AppendLine();
                classCodeBuilder.Indent();
            }

            // class declaration (start)
            if (classSetting.XmlComment != null)
            {
                classCodeBuilder.AppendIndent().AppendDocumentationComment().AppendXmlCommentTag("summary", XmlCommentTag.StartTag).AppendLine();

                foreach (var line in EndOfLineHelper.Split(classSetting.XmlComment))
                {
                    classCodeBuilder.AppendIndent().AppendDocumentationComment().Append(line).AppendLine();
                }

                classCodeBuilder.AppendIndent().AppendDocumentationComment().AppendXmlCommentTag("summary", XmlCommentTag.EndTag).AppendLine();
            }
            classCodeBuilder.AppendIndent().AppendClassDeclaration(classSetting.ClassName).AppendLine();
            classCodeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Left).AppendLine();
            classCodeBuilder.Indent();

            // Fields
            var targetPropertySetting = classSetting.PropertySettings.Where(p => !p.AutoImplementedProperties ?? !classSetting.AutoImplementedProperties).ToArray();
            if (targetPropertySetting.Any())
            {
                foreach (var (propertySetting, index) in targetPropertySetting.Select((p, i) => (p, i)))
                {
                    classCodeBuilder.AppendIndent().AppendField(propertySetting, classSetting.FieldNamingConvention).AppendLine();
                }

                classCodeBuilder.AppendLine();
            }

            // Properties
            if (classSetting.PropertySettings.Any())
            {
                var count = classSetting.PropertySettings.Count;

                foreach (var (propertySetting, index) in classSetting.PropertySettings.Select((p, i) => (p, i)))
                {
                    if (propertySetting.XmlComment != null)
                    {
                        classCodeBuilder.AppendIndent().AppendDocumentationComment().AppendXmlCommentTag("summary", XmlCommentTag.StartTag).AppendLine();

                        foreach (var line in EndOfLineHelper.Split(propertySetting.XmlComment))
                        {
                            classCodeBuilder.AppendIndent().AppendDocumentationComment().Append(line).AppendLine();
                        }

                        classCodeBuilder.AppendIndent().AppendDocumentationComment().AppendXmlCommentTag("summary", XmlCommentTag.EndTag).AppendLine();
                    }

                    if (propertySetting.AutoImplementedProperties ?? classSetting.AutoImplementedProperties)
                    {
                        classCodeBuilder.AppendIndent().AppendAutoImplementedProperties(propertySetting).AppendLine();
                    }
                    else
                    {
                        classCodeBuilder.AppendIndent().AppendPropertyDeclaration(propertySetting).AppendLine();

                        var fieldName = NameConverter.Convert(propertySetting.PropertyName, propertySetting.FieldNamingConvention ?? classSetting.FieldNamingConvention);

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
            if (!string.IsNullOrEmpty(classSetting.Namepace))
            {
                classCodeBuilder.Unindent();
                classCodeBuilder.AppendIndent().AppendCurlyBracket(CurlyBracket.Right).AppendLine();
            }

            return classCodeBuilder.ToString();
        }
    }
}
