using System;
using System.Linq;
using KoyashiroKohaku.CSharpCodeGenerator.Builders;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class CodeGenerator
    {
        public static string Generate(POCOClass pocoClass)
        {
            if (pocoClass == null)
            {
                throw new ArgumentNullException(nameof(pocoClass));
            }

            var codeBuilder = new CodeBuilder(pocoClass)
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                codeBuilder
                    .AppendNamespaceDeclaration()
                    .AppendCurlyBracket(CurlyBracket.Left)
                    .DownIndent();
            }

            codeBuilder
                .AppendClassDeclaration()
                .AppendCurlyBracket(CurlyBracket.Left)
                .DownIndent();

            if (pocoClass.Properties.Any())
            {
                codeBuilder.AppendProperties();
            }

            codeBuilder
                .UpIndent()
                .AppendCurlyBracket(CurlyBracket.Right);

            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                codeBuilder
                    .UpIndent()
                    .AppendCurlyBracket(CurlyBracket.Right);
            }

            return codeBuilder.ToString();
        }
    }
}
