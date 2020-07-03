using System;
using System.Linq;

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

            CodeBuilder? codeBuilder = new CodeBuilder(pocoClass)
            {
                IndentStyle = IndentStyle.Space,
                IndentSize = 4,
                EndOfLine = EndOfLine.CRLF
            };

            codeBuilder.AppendClassDeclaration();

            codeBuilder.AppendCurlyBracket(CurlyBracket.Left);

            codeBuilder.DownIndent();

            if (pocoClass.Properties.Any())
            {
                codeBuilder.AppendProperties();
            }

            codeBuilder.UpIndent();

            codeBuilder.AppendCurlyBracket(CurlyBracket.Right);

            return codeBuilder.ToString();
        }
    }
}
