using System;
using System.ComponentModel;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public class CodeBuilder : CodeBuilderBase
    {
        public override ICodeBuilder AppendIndent()
        {
            Append(CurrentIndentStringWithDepth);

            return this;
        }

        public override ICodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket)
        {
            if (!Enum.IsDefined(typeof(CurlyBracket), curlyBracket))
            {
                throw new InvalidEnumArgumentException(nameof(curlyBracket), (int)curlyBracket, typeof(CurlyBracket));
            }

            _ = curlyBracket switch
            {
                CurlyBracket.Left => Append("{"),
                CurlyBracket.Right => Append("}"),
                _ => throw new InvalidEnumArgumentException(nameof(curlyBracket), (int)curlyBracket, typeof(CurlyBracket))
            };

            return this;
        }

        public override ICodeBuilder AppendDocumentationComment()
        {
            Append("/// ");

            return this;
        }

        public override ICodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag)
        {
            if (tagName == null)
            {
                throw new ArgumentNullException(nameof(tagName));
            }

            switch (xmlCommentTag)
            {
                case XmlCommentTag.StartTag:
                    Append("<").Append(tagName).Append(">");
                    break;
                case XmlCommentTag.EndTag:
                    Append("</").Append(tagName).Append(">");
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(xmlCommentTag), (int)xmlCommentTag, typeof(XmlCommentTag));
            }

            return this;
        }

        public override ICodeBuilder AppendUsingDirective(string namespaceString)
        {
            if (namespaceString == null)
            {
                throw new ArgumentNullException(nameof(namespaceString));
            }

            Append("using ").Append(namespaceString).Append(";");

            return this;
        }

        public override ICodeBuilder AppendNamespaceDeclaration(string namespaceName)
        {
            if (string.IsNullOrEmpty(namespaceName))
            {
                return this;
            }

            Append("namespace ").Append(namespaceName);

            return this;
        }
    }
}
