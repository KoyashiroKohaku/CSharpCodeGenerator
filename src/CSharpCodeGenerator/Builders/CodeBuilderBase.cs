using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public abstract class CodeBuilderBase : ICodeBuilder
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

        public ICodeBuilder Indent()
        {
            if (IndentDepth < int.MaxValue)
            {
                _indentDepth++;
            }

            return this;
        }

        public ICodeBuilder Unindent()
        {
            if (IndentDepth > 0)
            {
                _indentDepth--;
            }

            return this;
        }

        public ICodeBuilder Append(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _builder.Append(value);

            return this;
        }

        public ICodeBuilder Append(ReadOnlySpan<char> value)
        {
            _builder.Append(value);

            return this;
        }

        public ICodeBuilder AppendLine()
        {
            Append(CurrentEndOfLineString);

            return this;
        }

        public ICodeBuilder AppendLine(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Append(value).Append(CurrentEndOfLineString);

            return this;
        }

        public ICodeBuilder AppendLine(ReadOnlySpan<char> value)
        {
            Append(value).Append(CurrentEndOfLineString);

            return this;
        }

        public abstract ICodeBuilder AppendIndent();

        public abstract ICodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket);

        public abstract ICodeBuilder AppendDocumentationComment();

        public abstract ICodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag);

        public abstract ICodeBuilder AppendUsingDirective(string namespaceString);

        public abstract ICodeBuilder AppendNamespaceDeclaration(string namespaceName);

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}
