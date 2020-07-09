using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public class CodeBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder();
        private IndentStyle _indentStyle = IndentStyle.Space;
        private int _indentSize = 4;
        private int _indentDepth = 0;

        public string CurrentIndentString => GetIndentString(IndentStyle, IndentSize);

        public string CurrentIndentStringWithDepth => GetIndentStringWithDepth(CurrentIndentString, IndentDepth);

        public string CurrentEndOfLineString => GetEndOfLineString(EndOfLine);

        public IndentStyle IndentStyle
        {
            get => _indentStyle;
            set
            {
                var oldValue = IndentStyle switch
                {
                    IndentStyle.Space => GetIndentString(IndentStyle.Space, IndentSize),
                    IndentStyle.Tab => "\t",
                    _ => throw new InvalidEnumArgumentException(nameof(IndentStyle), (int)IndentStyle, typeof(IndentStyle))
                };

                var newValue = value switch
                {
                    IndentStyle.Space => GetIndentString(IndentStyle.Space, IndentSize),
                    IndentStyle.Tab => "\t",
                    _ => throw new InvalidEnumArgumentException(nameof(IndentStyle), (int)IndentStyle, typeof(IndentStyle))
                };

                _builder.Replace(oldValue, newValue);

                _indentStyle = value;
            }
        }

        public int IndentSize
        {
            get => _indentSize;
            set
            {
                if (IndentStyle == IndentStyle.Space)
                {
                    var oldValue = GetIndentString(IndentStyle.Space, IndentSize);

                    var newValue = GetIndentString(IndentStyle.Space, value);

                    _builder.Replace(oldValue, newValue);
                }

                _indentSize = value < 0 ? 0 : value;
            }
        }

        public int IndentDepth
        {
            get => _indentDepth;
            set
            {
                if (value < 0)
                {
                    _indentDepth = 0;
                }
                else
                {
                    _indentDepth = value;
                }
            }
        }

        public EndOfLine EndOfLine { get; set; } = EndOfLine.CRLF;

        public static string GetIndentString(IndentStyle indentStyle, int indentSize)
        {
            var indentString = indentStyle switch
            {
                IndentStyle.Space => string.Concat(Enumerable.Repeat(" ", indentSize)),
                IndentStyle.Tab => "\t",
                _ => throw new InvalidEnumArgumentException(nameof(indentStyle), (int)indentStyle, typeof(IndentStyle))
            };

            return indentString;
        }

        public static string GetIndentStringWithDepth(IndentStyle indentStyle, int indentSize, int indentDepth)
        {
            var indentString = GetIndentString(indentStyle, indentSize);

            return GetIndentStringWithDepth(indentString, indentDepth);
        }

        public static string GetIndentStringWithDepth(string indentString, int indentDepth)
        {
            if (indentString == null)
            {
                throw new ArgumentNullException(nameof(indentString));
            }

            return string.Concat(Enumerable.Repeat(indentString, indentDepth));
        }

        public static string GetEndOfLineString(EndOfLine endOfLine)
        {
            return endOfLine switch
            {
                EndOfLine.CR => "\r",
                EndOfLine.LF => "\n",
                EndOfLine.CRLF => "\r\n",
                _ => throw new InvalidEnumArgumentException(nameof(endOfLine), (int)endOfLine, typeof(EndOfLine))
            };
        }

        public CodeBuilder Indent()
        {
            if (IndentDepth < int.MaxValue)
            {
                _indentDepth++;
            }

            return this;
        }

        public CodeBuilder Unindent()
        {
            if (IndentDepth > 0)
            {
                _indentDepth--;
            }

            return this;
        }

        public CodeBuilder Append(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _builder.Append(value);

            return this;
        }

        public CodeBuilder Append(ReadOnlySpan<char> value)
        {
            _builder.Append(value);

            return this;
        }

        public CodeBuilder AppendLine()
        {
            Append(CurrentEndOfLineString);

            return this;
        }

        public CodeBuilder AppendLine(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Append(value).Append(CurrentEndOfLineString);

            return this;
        }

        public CodeBuilder AppendLine(ReadOnlySpan<char> value)
        {
            Append(value).Append(CurrentEndOfLineString);

            return this;
        }

        public CodeBuilder AppendIndent()
        {
            Append(CurrentIndentStringWithDepth);

            return this;
        }

        public CodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket)
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

        public CodeBuilder AppendDocumentationComment()
        {
            Append("/// ");

            return this;
        }

        public CodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag)
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

        public CodeBuilder AppendUsingDirective(string namespaceString)
        {
            if (namespaceString == null)
            {
                throw new ArgumentNullException(nameof(namespaceString));
            }

            Append("using ").Append(namespaceString).Append(";");

            return this;
        }

        public CodeBuilder AppendNamespaceDeclaration(string namespaceName)
        {
            if (string.IsNullOrEmpty(namespaceName))
            {
                return this;
            }

            Append("namespace ").Append(namespaceName);

            return this;
        }

        public CodeBuilder AppendClassDeclaration(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                return this;
            }

            Append("public class ").Append(className);

            return this;
        }

        public CodeBuilder AppendProperty(ClassProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            Append("public ").Append(TypeResolver.GetTypeString(property.PropertyType));

            if (property.Nullable)
            {
                Append("?");
            }

            Append(" ").Append(property.PropertyName).Append(" { get; set; }");

            return this;
        }

        public override string ToString()
        {
            return _builder.ToString();
        }

        public static IEnumerable<string> ExtractNamespace(IEnumerable<ClassProperty> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return properties
                .Where(p => !TypeResolver.ExistsTypeAlias(p.PropertyType))
                .Select(p => p.PropertyType.Namespace)
                .Distinct();
        }
    }
}
